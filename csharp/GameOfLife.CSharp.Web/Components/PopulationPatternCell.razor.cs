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
        public bool Readonly { get; set; }

        [Parameter]
        public EventCallback<WorldColumn> OnCellClick { get; set; }

        protected async Task HandleOnCellClick()
        {
            if (!Readonly)
            {
                var worldColumn = new WorldColumn
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
