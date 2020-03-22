using GameOfLife.CSharp.Web.Data;
using GameOfLife.CSharp.Web.Models;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace GameOfLife.CSharp.Web.Components
{
    public class LayoutBase : ComponentBase
    {
        [Inject]
        public IPopulationDataService PopulationDataService { get; set; }

        public World World { get; private set; }

        protected override async Task OnInitializedAsync()
        {
            World = await PopulationDataService?.GetPopulationData();
        }
    }
}
