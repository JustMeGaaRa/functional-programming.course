using Microsoft.AspNetCore.Components;

namespace GameOfLife.CSharp.Web.Components
{
    public class PopulationPatternCellBase : ComponentBase
    {
        public string Style => IsAlive ? "population alive" : "population dead";

        [Parameter]
        public int Column { get; set; }

        [Parameter]
        public int Row { get; set; }

        [Parameter]
        public bool IsAlive { get; set; }

        [Parameter]
        public bool Readonly { get; set; }
    }
}
