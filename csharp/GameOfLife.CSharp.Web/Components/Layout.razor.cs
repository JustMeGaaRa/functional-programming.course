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

        public int UserId { get; private set; }

        public int SelectedPatternId { get; set; }

        public bool IsReadonly { get; private set; }

        protected override async Task OnInitializedAsync()
        {
            var userInfo = await PatternsService.CreateUser();
            UserId = userInfo.UserId;

            InitializeDefaultState();

            GameService.Subscribe((World world) =>
            {
                UpdateWorld(world);
                UpdateGeneration();
                StateHasChanged();
            });

            await GameService.Connect();
        }

        public async Task HandleOnStartClick(int selectedPatternId)
        {
            await GameService.Start(UserId, selectedPatternId);

            UpdateSelectedPattern(selectedPatternId);
            EnableReadonly();
        }

        public async Task HandleOnStopClick()
        {
            await GameService.End(UserId);

            DisableReadonly();
        }

        public async Task HandleOnCreateClick(int selectedPatternId)
        {
            var world = await PatternsService.GetPatternView(UserId, selectedPatternId);

            UpdateSelectedPattern(selectedPatternId);
            UpdateWorld(world);
            StateHasChanged();
        }

        public async Task HandleOnPatternSelect(int selectedPatternId)
        {
            await GameService.End(UserId);
            var world = await PatternsService.GetPatternView(UserId, selectedPatternId);

            UpdateSelectedPattern(selectedPatternId);
            UpdateWorld(world);
            DisableReadonly();
            UpdateGeneration();
            StateHasChanged();
        }

        protected async Task HandleOnCellClick(WorldCell cell)
        {
            var world = await PatternsService.GetPatternCell(UserId, SelectedPatternId, cell);

            UpdateWorld(world);
            StateHasChanged();
        }

        private void InitializeDefaultState()
        {
            UserId = 1;
            SelectedPatternId = 1;
            SelectedWorld = new World
            {
                Generation = 0,
                Height = 0,
                Width = 0,
                Rows = new List<WorldRow>()
            };
            UpdateGeneration();
        }

        private void UpdateGeneration()
        {
            Generation = SelectedWorld.Generation;
        }

        private void UpdateWorld(World world)
        {
            SelectedWorld = world;
        }

        private void UpdateSelectedPattern(int selectedPatternId)
        {
            SelectedPatternId = selectedPatternId;
        }

        private void EnableReadonly()
        {
            IsReadonly = true;
        }

        private void DisableReadonly()
        {
            IsReadonly = false;
        }
    }
}
