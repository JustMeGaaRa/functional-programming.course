namespace GameOfLife.FSharp.Api

open System
open System.IO
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting

module App = 

    [<EntryPoint>]
    let main _ =
        let contentRoot = Directory.GetCurrentDirectory()
        let webRoot     = Path.Combine(contentRoot, "WebRoot")
        WebHostBuilder()
            .UseKestrel()
            .UseContentRoot(contentRoot)
            .UseIISIntegration()
            .UseWebRoot(webRoot)
            .Configure(Action<IApplicationBuilder> Startup.configureApp)
            .ConfigureServices(Startup.configureServices)
            .ConfigureLogging(Startup.configureLogging)
            .Build()
            .Run()
        0