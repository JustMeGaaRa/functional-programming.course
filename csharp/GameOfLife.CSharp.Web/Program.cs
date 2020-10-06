using GameOfLife.CSharp.Web.Data;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace GameOfLife.CSharp.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddHttpClient();
            builder.Services.AddSingleton<IGameService, GameService>();
            builder.Services.AddSingleton<IPatternsService, PatternsService>();

            await builder.Build().RunAsync();
        }
    }
}
