using GameOfLife.CSharp.Api.Hubs;
using GameOfLife.CSharp.Api.Infrastructure;
using GameOfLife.CSharp.Api.Services;
using GameOfLife.Engine;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace GameOfLife.CSharp.Api
{
    public class Startup
    {
        private const string SwaggerApiName = "Conway's Game of Life Api";
        private const string SwaggerApiVersion = "v1";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddMvc();
            services.AddSignalR();
            services.AddSwaggerGen(ConfigureSwaggerGenOptions);
            services.AddSingleton<IWorldPatternRepository, InMemoryWorldPatternRepository>();
            services.AddSingleton<IGameOfLifeService, InMemoryGameOfLifeService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<GameOfLifeHub>("/game");
            });
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", SwaggerApiName));
        }

        private static void ConfigureSwaggerGenOptions(SwaggerGenOptions options)
        {
            OpenApiInfo apiInfo = new OpenApiInfo { Title = SwaggerApiName, Version = SwaggerApiVersion };
            options.SwaggerDoc(SwaggerApiVersion, apiInfo);
        }
    }
}
