using GameOfLife.CSharp.Web.Data;
using GameOfLife.CSharp.Web.Models;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace GameOfLife.CSharp.Web.Components
{
    public class LayoutBase : ComponentBase
    {
        [Inject] 
        private IPopulationDataService PopulationDataService { get; set; }

        protected World _populationData;

        protected override async Task OnInitializedAsync()
        {
            _populationData = await PopulationDataService.GetPopulationData();
        }
    }
}
