namespace GameOfLife.FSharp.Api

open System
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Cors.Infrastructure
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Logging
open Microsoft.Extensions.DependencyInjection
open Giraffe
open GameOfLife.FSharp.Engine

module Startup =
    
    let indexHandler (name : string) =
        let model = Generation.zero PopulationPatterns.pulsar
        let view = Views.index model
        htmlView view
    
    let webApp =
        choose [
            GET >=>
                choose [
                    route "/" >=> indexHandler "Conway's Game Of Life"
                    routef "/hello/%s" indexHandler
                ]
            setStatusCode 404 >=> text "Not Found" ]

    let errorHandler (ex : Exception) (logger : ILogger) =
        logger.LogError(ex, "An unhandled exception has occurred while executing the request.")
        clearResponse >=> setStatusCode 500 >=> text ex.Message

    let configureCors (builder : CorsPolicyBuilder) =
        builder.WithOrigins("http://localhost:8080")
               .AllowAnyMethod()
               .AllowAnyHeader()
               |> ignore
               
    let configureLogging (builder : ILoggingBuilder) =
        builder.AddFilter(fun l -> l.Equals LogLevel.Error)
                .AddConsole()
                .AddDebug() |> ignore
    
    let configureServices (services : IServiceCollection) =
        services.AddCors()    |> ignore
        services.AddGiraffe() |> ignore
        
    let configureApp (app : IApplicationBuilder) =
        let env = app.ApplicationServices.GetService<IHostingEnvironment>()
        let builder =
            match env.IsDevelopment() with
            | true  -> app.UseDeveloperExceptionPage()
            | false -> app.UseGiraffeErrorHandler errorHandler
        builder.UseHttpsRedirection()
            .UseCors(configureCors)
            .UseStaticFiles()
            .UseGiraffe(webApp)
