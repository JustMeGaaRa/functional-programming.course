using GameOfLife.CSharp.Web.Data;
using GameOfLife.CSharp.Web.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameOfLife.CSharp.Web.Components
{
    public class SelectionBase : ComponentBase
    {
        [Inject]
        public IPatternsService PatternsService { get; set; }

        public ICollection<PatternInfo> Patterns { get; private set; }

        [Parameter]
        public string PatternName { get; set; }

        [Parameter]
        public int PatternWidth { get; set; }

        [Parameter]
        public int PatternHeight { get; set; }

        [Parameter]
        public int UserId { get; set; }

        [Parameter]
        public int SelectedPatternId { get; set; }

        [Parameter]
        public EventCallback<int> OnPatternSelectClick { get; set; }

        [Parameter]
        public EventCallback<int> OnCreateClick { get; set; }

        [Parameter]
        public EventCallback<int> OnStartClick { get; set; }

        [Parameter]
        public EventCallback OnStopClick { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Patterns = await PatternsService.GetPattersByUserId(UserId);

            InitializeDefaultState();
        }

        protected async Task HandleCreateClick()
        {
            var newPattern = new PatternInfo
            {
                PatternId = 0,
                Name = PatternName,
                Height = PatternHeight,
                Width = PatternWidth
            };

            Patterns.Add(newPattern);

            await OnCreateClick.InvokeAsync(SelectedPatternId);
        }

        protected async Task HandleStartClick()
        {            
            await OnStartClick.InvokeAsync(SelectedPatternId);
        }

        protected async Task HandleOnPatternSelect(ChangeEventArgs e)
        {
            SelectedPatternId = Convert.ToInt32(e.Value); 
            await OnPatternSelectClick.InvokeAsync(SelectedPatternId);
        }

        private void InitializeDefaultState()
        {
            PatternName = "New Pattern";
            PatternWidth = 10;
            PatternHeight = 10;
        }
    }
}
