using GameOfLife.CSharp.Api.Services;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace GameOfLife.CSharp.Api.Hubs
{
    public class GameOfLifeHub : Hub
    {
        private readonly IGameOfLifeService _gameOfLifeService;

        public GameOfLifeHub(IGameOfLifeService gameOfLifeService)
        {
            _gameOfLifeService = gameOfLifeService;
        }

        public async Task CreateFromPattern(int userId, int patternId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, userId.ToString());
            _gameOfLifeService.CreateFromPattern(userId, patternId);
        }

        public Task StartUserGame(int userId, string instanceId)
        {
            return Guid.TryParse(instanceId, out Guid id)
                ? _gameOfLifeService.StartGameAsync(userId, id)
                : Task.CompletedTask;
        }

        public Task EndUserGame(int userId, string instanceId)
        {
            return Guid.TryParse(instanceId, out Guid id)
                ? _gameOfLifeService.StopGameAsync(userId, id)
                : Task.CompletedTask;
        }
    }
}
