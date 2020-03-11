using GameOfLife.CSharp.Web.Data;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameOfLife.CSharp.Web.Components
{
    public class LayoutBase : ComponentBase
    {
        [Inject] IPopulationDataService PopulationDataService { get; set; }

        protected IList<List<string>> _populationData;

        protected override async Task OnInitializedAsync()
        {
            _populationData = await PopulationDataService.GetPopulationData();
        }
    }
}
