using GameOfLife.CSharp.Web.Models;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

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
        public bool IsReadonly { get; set; }

        [Parameter]
        public EventCallback<WorldCell> OnCellClick { get; set; }

        protected async Task HandleOnCellClick()
        {
            if (!IsReadonly)
            {
                var worldColumn = new WorldCell
                {
                    Row = Row,
                    Column = Column,
                    IsAlive = !IsAlive
                };

                await OnCellClick.InvokeAsync(worldColumn);
            }
        }
    }
}
