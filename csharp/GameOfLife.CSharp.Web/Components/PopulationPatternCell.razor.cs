using GameOfLife.CSharp.Web.Models;
using Microsoft.AspNetCore.Components;

namespace GameOfLife.CSharp.Web.Components
{
    public class PopulationPatternCellBase : ComponentBase
    {
        public string Style => Column.IsAlive ? "population alive" : "population dead";

        [Parameter]
        public WorldColumn Column { get; set; }
    }
}
