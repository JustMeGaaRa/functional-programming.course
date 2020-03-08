using System.Threading.Tasks;
using Microsoft.AspNetCore.Blazor.Hosting;
using Microsoft.Extensions.DependencyInjection;
using GameOfLife.CSharp.Web.Data;

namespace GameOfLife.CSharp.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.Services.AddTransient<IPopulationDataService, PopulationDataService>();
            builder.RootComponents.Add<App>("app");

            await builder.Build().RunAsync();
        }
    }
}
