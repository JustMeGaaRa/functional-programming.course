using GameOfLife.CSharp.Api.Services;
using Microsoft.AspNetCore.SignalR;
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

        public async Task StartGameFromPattern(int userId, int patternId)
        {
            await _gameOfLifeService.StartGameAsync(userId, patternId);
        }

        public async Task EndUserGame(int userId)
        {
            await _gameOfLifeService.EndGameAsync(userId);
        }
    }
}
