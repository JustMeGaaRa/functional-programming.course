using Microsoft.AspNetCore.Components;

namespace GameOfLife.CSharp.Web.Components
{
    public class CellBase : ComponentBase
    {
        [Parameter]
        public string ClassName { get; set; }

        protected override void OnInitialized() => ClassName ??= "population dead";
    }
}
