using GameOfLife.CSharp.Web.Data;
using GameOfLife.CSharp.Web.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace GameOfLife.CSharp.Web.Components
{
    public class LayoutBase : ComponentBase
    {
        [Inject]
        public IGameService GameService { get; set; }

        public World World { get; private set; }

        public int Generation { get; private set; }
        
        protected override async Task OnInitializedAsync()
        {
            GameService.Subscribe(UpdateWorld);
            await GameService.Connect();            
        }

        public async Task HandleOnStartClick()
        {
            await GameService.Start(1, 4);
        }

        public async Task HandleOnStopClick()
        {
            await GameService.End(1);
        }

        private void UpdateWorld(World message)
        {
            World = message;
            StateHasChanged();
            Generation++;
        }
    }
}
