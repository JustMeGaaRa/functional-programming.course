using GameOfLife.CSharp.Web.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace GameOfLife.CSharp.Web.Components
{
    public class PopulationPatternGridBase : ComponentBase
    {
        [Parameter]
        public ICollection<WorldRow> Rows { get; set; }
    }
}
