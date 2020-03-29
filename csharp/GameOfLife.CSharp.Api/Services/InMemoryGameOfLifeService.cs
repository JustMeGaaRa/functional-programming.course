using GameOfLife.CSharp.Api.Extensions;
using GameOfLife.CSharp.Api.Hubs;
using GameOfLife.CSharp.Engine;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameOfLife.CSharp.Api.Services
{
    public class InMemoryGameOfLifeService : IGameOfLifeService
    {
        private readonly Dictionary<int, Time> _activeGames = new Dictionary<int, Time>();
        private readonly IPopulationPatternRepository _repository;
        private readonly IHubContext<GameOfLifeHub> _hubContext;

        public InMemoryGameOfLifeService(IPopulationPatternRepository repository, IHubContext<GameOfLifeHub> hubContext)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
        }

        public Task<Generation> StartGameAsync(int userId, int patternId)
        {
            PopulationPattern pattern = _repository.GetPatternById(patternId);
            _activeGames[userId] = new Time();
            _activeGames[userId].Subscribe(PushGenerationToHub);
            Generation generation = _activeGames[userId].Start(pattern);
            return Task.FromResult(generation);
        }

        public Task EndGameAsync(int userId)
        {
            _activeGames.Remove(userId, out Time? time);
            time?.Dispose();
            return Task.CompletedTask;
        }

        private void PushGenerationToHub(Generation generation)
        {
            const string RemoteWebAppMethod = "UpdateGameWorld";
            _hubContext.Clients.All.SendAsync(RemoteWebAppMethod, generation.ToPatternViewVM());
        }
    }
}
