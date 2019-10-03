using GameOfLife.CSharp.Api.Hubs;
using GameOfLife.CSharp.Api.Models;
using GameOfLife.Engine;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameOfLife.CSharp.Api.Services
{
    public class InMemoryGameOfLifeService : IGameOfLifeService
    {
        private readonly Dictionary<int, Time> _activeGames = new Dictionary<int, Time>();
        private readonly IWorldPatternRepository _repository;
        private readonly IHubContext<GameOfLifeHub> _hubContext;

        public InMemoryGameOfLifeService(IWorldPatternRepository repository, IHubContext<GameOfLifeHub> hubContext)
        {
            _repository = repository;
            _hubContext = hubContext;
        }

        public Task<Generation> StartGameAsync(int userId, int patternId)
        {
            WorldPattern pattern = _repository.GetPatternById(patternId);
            _activeGames[userId] = new Time();
            _activeGames[userId].Subscribe(PushGenerationToHub);
            Generation generation = _activeGames[userId].Start(pattern);
            return Task.FromResult(generation);
        }

        public Task EndGameAsync(int userId)
        {
            _activeGames[userId].Dispose();
            return Task.CompletedTask;
        }

        private void PushGenerationToHub(Generation generation)
        {
            const string RemoteWebAppMethod = "UpdateGameWorld";
            _hubContext.Clients.All.SendAsync(RemoteWebAppMethod, ToGenerationVM(generation));
        }

        private GenerationVM ToGenerationVM(Generation generation)
        {
            return new GenerationVM { Height = generation.Size.Height, Width = generation.Size.Width };
        }
    }
}
