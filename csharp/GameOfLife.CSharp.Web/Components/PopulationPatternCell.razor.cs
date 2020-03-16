using GameOfLife.CSharp.Web.Models;
using Microsoft.AspNetCore.Components;

namespace GameOfLife.CSharp.Web.Components
{
    public class PopulationPatternCellBase : ComponentBase
    {
        protected string _className { get; set; }

        [Parameter]
        public WorldColumn Column { get; set; }

        protected override void OnInitialized()
        {
            _className = Column.IsAlive ? "population alive" : "population dead";
        }
    }
}
