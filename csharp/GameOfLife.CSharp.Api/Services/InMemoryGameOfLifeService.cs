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
        private readonly Dictionary<(int, Guid), Time> _activeGames = new Dictionary<(int, Guid), Time>();
        private readonly IPopulationPatternRepository _repository;
        private readonly IHubContext<GameOfLifeHub> _hubContext;

        public InMemoryGameOfLifeService(IPopulationPatternRepository repository, IHubContext<GameOfLifeHub> hubContext)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
        }

        public Generation CreateFromPattern(int userId, int patternId)
        {
            PopulationPattern pattern = _repository.GetPatternById(patternId);
            Generation generation = Generation.Zero(pattern);
            _activeGames[(userId, generation.World.Identity)] = new Time(generation);
            return generation;
        }

        public Task<bool> StartGameAsync(int userId, Guid instanceId)
        {
            if (_activeGames.ContainsKey((userId, instanceId)))
            {
                _activeGames[(userId, instanceId)].Subscribe(PushGenerationToHub);
                _activeGames[(userId, instanceId)].StartAsync();
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }

        public Task<bool> EndGameAsync(int userId, Guid instanceId)
        {
            if (_activeGames.Remove((userId, instanceId), out Time? time))
            {
                time?.Dispose();
                Task.FromResult(true);
            }

            return Task.FromResult(false);
        }

        public async Task<Generation> MergeGamesAsync(int userId, Guid firstId, Guid secondId)
        {
            // TODO: Set a proper offset for the merging universe
            Generation firstGeneration = await _activeGames[(userId, firstId)].StopAsync();
            Generation secondGeneration = await _activeGames[(userId, secondId)].StopAsync();
            IUniverse universe = firstGeneration.World.Join(secondGeneration.World, Offset.None);
            Generation mergedGeneration = Generation.Zero(universe);
            Guid instanceId = mergedGeneration.World.Identity;

            _activeGames[(userId, instanceId)] = new Time(mergedGeneration);
            _activeGames[(userId, instanceId)].Subscribe(PushGenerationToHub);
            return await _activeGames[(userId, instanceId)].StartAsync();
        }

        private void PushGenerationToHub(Generation generation)
        {
            const string RemoteWebAppMethod = "UpdateGameWorld";
            _hubContext.Clients.All.SendAsync(RemoteWebAppMethod, generation.ToPatternViewVM());
        }
    }
}
