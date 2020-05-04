using GameOfLife.CSharp.Web.Data;
using GameOfLife.CSharp.Web.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameOfLife.CSharp.Web.Components
{
    public class LayoutBase : ComponentBase
    {
        [Inject]
        public IGameService GameService { get; set; }

        [Inject]
        public IPatternsService PatternsService { get; set; }

        public World SelectedWorld { get; private set; }

        public uint Generation { get; private set; }

        public int UserId { get; private set; } = 1;

        public int SelectedPatternId { get; set; } = 1;

        public bool Readonly { get; private set; } = false;
        
        protected override async Task OnInitializedAsync()
        {
            SelectedWorld = new World
            {
                Generation = 0,
                Height = 0,
                Width = 0,
                Rows = new List<WorldRow>()
            };

            Generation = SelectedWorld.Generation;

            var userInfo = await PatternsService.CreateUser();

            UserId = userInfo.UserId;

            GameService.Subscribe(UpdateWorld);
            await GameService.Connect();            
        }

        public async Task HandleOnStartClick(int selectedPatternId)
        {
            SelectedPatternId = selectedPatternId;
            await GameService.Start(UserId, selectedPatternId);
            Readonly = true;
        }

        public async Task HandleOnStopClick()
        {
            await GameService.End(UserId);
            Readonly = false;
        }

        public async Task HandleOnCreateClick(int selectedPatternId)
        {
            SelectedPatternId = selectedPatternId;
            var world = await PatternsService.GetPatternView(UserId, selectedPatternId);

            SelectedWorld = world;
            StateHasChanged();
        }

        public async Task HandleOnPatternSelect(int selectedPatternId)
        {
            SelectedPatternId = selectedPatternId;
            await GameService.End(UserId);
            var world = await PatternsService.GetPatternView(UserId, selectedPatternId);
            SelectedWorld = world;
            Readonly = false;
            Generation = SelectedWorld.Generation;
            StateHasChanged();
        }

        protected async Task HandleOnCellClick(WorldColumn worldColumn)
        {
            var world = await PatternsService.GetPatternCell(UserId, SelectedPatternId, worldColumn);
            SelectedWorld = world;
            StateHasChanged();
        }

        private void UpdateWorld(World world)
        {
            SelectedWorld = world;
            Generation = SelectedWorld.Generation;
            StateHasChanged();
        }
    }
}
