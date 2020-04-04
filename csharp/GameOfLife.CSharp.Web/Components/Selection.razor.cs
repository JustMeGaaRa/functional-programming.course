using GameOfLife.CSharp.Web.Data;
using GameOfLife.CSharp.Web.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace GameOfLife.CSharp.Web.Components
{
    public class SelectionBase : ComponentBase
    {
        [Inject]
        public IPatternsService PatternsService { get; set; }

        [Inject]
        public HttpClient HttpClient { get; set; }

        public ICollection<Pattern> Patterns { get; private set; }

        [Parameter]
        public EventCallback OnStartClick { get; set; }

        [Parameter]
        public EventCallback OnStopClick { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Patterns = await PatternsService.GetPattersByUserId(1);
        }
    }
}
