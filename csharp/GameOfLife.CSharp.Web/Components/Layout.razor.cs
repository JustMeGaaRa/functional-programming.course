using GameOfLife.CSharp.Web.Data;
using GameOfLife.CSharp.Web.Models;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace GameOfLife.CSharp.Web.Components
{
    public class LayoutBase : ComponentBase
    {
        [Inject]
        public IGameService GameService { get; set; }

        [Inject]
        public IPatternsService PatternsService { get; set; }

        public World World { get; private set; }

        public int Generation { get; private set; }

        public int UserId { get; private set; }
        
        protected override async Task OnInitializedAsync()
        {
            var userInfo = await PatternsService.CreateUser();

            UserId = userInfo.UserId;

            GameService.Subscribe(UpdateWorld);
            await GameService.Connect();            
        }

        public async Task HandleOnStartClick(int selectedPatternId)
        {
            await GameService.Start(UserId, selectedPatternId);
        }

        public async Task HandleOnStopClick()
        {
            await GameService.End(UserId);
        }

        public async Task HandleOnCreateClick(int selectedPatternId)
        {
            var world = await PatternsService.GetPatternView(UserId, selectedPatternId);

            World = world;
            StateHasChanged();
        }

        public async Task HandleOnPatternSelect(int selectedPatternId)
        {
            await GameService.End(UserId);
            var world = await PatternsService.GetPatternView(UserId, selectedPatternId);
            World = world;
            Generation = 0;
            StateHasChanged();
        }

        private void UpdateWorld(World world)
        {
            World = world;
            Generation++;
            StateHasChanged();
        }
    }
}
