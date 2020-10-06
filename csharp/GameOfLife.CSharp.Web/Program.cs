using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using GameOfLife.CSharp.Web.Data;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

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
