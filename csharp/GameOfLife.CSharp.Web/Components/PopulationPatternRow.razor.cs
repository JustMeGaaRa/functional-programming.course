using GameOfLife.CSharp.Web.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace GameOfLife.CSharp.Web.Components
{
    public class PopulationPatternRowBase : ComponentBase
    {
        public int Number { get; set; }

        [Parameter]
        public bool IsReadonly { get; set; }

        [Parameter]
        public ICollection<WorldCell> Columns { get; set; }

        [Parameter]
        public EventCallback<WorldCell> OnCellClick { get; set; }
    }
}
