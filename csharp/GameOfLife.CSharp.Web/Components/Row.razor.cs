using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace GameOfLife.CSharp.Web.Components
{
    public class RowBase : ComponentBase
    {
        [Parameter]
        public IList<string> Data { get; set; }

        protected override void OnInitialized() => Data = Data;
    }
}
