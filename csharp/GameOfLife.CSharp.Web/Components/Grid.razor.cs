using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace GameOfLife.CSharp.Web.Components
{
    public class GridBase: ComponentBase
    {
        [Parameter]
        public IList<List<string>> Data { get; set; }

        protected override void OnInitialized() => Data = Data;
    }
}
