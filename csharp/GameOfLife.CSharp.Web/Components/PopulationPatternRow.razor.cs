using GameOfLife.CSharp.Web.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace GameOfLife.CSharp.Web.Components
{
    public class PopulationPatternRowBase : ComponentBase
    {
        public int Number { get; set; }

        [Parameter]
        public bool Readonly { get;set;}

        [Parameter]
        public ICollection<WorldColumn> Columns { get; set; }

        [Parameter]
        public EventCallback<WorldColumn> OnCellClick { get; set; }
    }
}
