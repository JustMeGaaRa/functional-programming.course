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
        private readonly Dictionary<(int, Guid), Time> _activeGames = new();
        private readonly Dictionary<(int, Guid), IDisposable> _gameSubscriptions = new();
        private readonly IPopulationPatternRepository _repository;
        private readonly IHubContext<GameOfLifeHub> _hubContext;

        public InMemoryGameOfLifeService(IPopulationPatternRepository repository, IHubContext<GameOfLifeHub> hubContext)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
        }

        public Generation CreateFromPattern(int userId, int patternId)
        {
            var pattern = _repository.GetPatternById(patternId);
            var generation = Generation.Zero(pattern);

            InternalRegister(userId, generation.World.Identity, generation);

            return generation;
        }

        public Task<bool> StartGameAsync(int userId, Guid instanceId)
        {           
            return Task.FromResult(InternalStartGameAsync(userId, instanceId) != null);
        }

        public Task<bool> StopGameAsync(int userId, Guid instanceId)
        {
            return Task.FromResult(InternalStopGameAsync(userId, instanceId) != null);
        }

        public async Task<bool> RemoveGameAsync(int userId, Guid instanceId)
        {
            var key = (userId, instanceId);
            if (_activeGames.Remove(key, out Time? time)
                && _gameSubscriptions.Remove(key, out IDisposable? subscription))
            {
                subscription?.Dispose();
                await time.StopAsync();
                time?.Dispose();
                return true;
            }

            return false;
        }

        public async Task<Generation> MergeGamesAsync(int userId, Guid firstId, Guid secondId)
        {
            // TODO: when mergin two worlds - one active and other paused, should the merged one become active or paused?
            // TODO: Set a proper offset for the merging universe
            Generation firstGeneration = await InternalStopGameAsync(userId, firstId);
            Generation secondGeneration = await InternalStopGameAsync(userId, secondId);

            IUniverse universe = firstGeneration.World.Join(secondGeneration.World, Offset.None);
            Generation mergedGeneration = Generation.Zero(universe);
            Guid instanceId = mergedGeneration.World.Identity;

            InternalRegister(userId, instanceId, mergedGeneration);
            return await InternalStartGameAsync(userId, instanceId);
        }

        public Task<IReadOnlyCollection<Generation>> SplitGamesAsync(int userId, Guid firstId, Guid secondId)
        {
            // TODO: actually split into several games
            throw new NotImplementedException();
        }

        private Time InternalRegister(int userId, Guid instanceId, Generation generation)
        {
            var key = (userId, instanceId);
            _activeGames[key] = new Time(generation);
            _gameSubscriptions[key] = _activeGames[key].Subscribe(PushGenerationToHub);
            return _activeGames[key];
        }

        private async Task<Generation> InternalStartGameAsync(int userId, Guid instanceId)
        {
            var key = (userId, instanceId);
            return _activeGames.TryGetValue(key, out Time time)
                ? await time.StartAsync()
                : null;
        }

        private async Task<Generation> InternalStopGameAsync(int userId, Guid instanceId)
        {
            var key = (userId, instanceId);
            return _activeGames.TryGetValue(key, out Time time)
                ? await time.StopAsync()
                : null;
        }

        private void PushGenerationToHub(Generation generation)
        {
            const string RemoteWebAppMethod = "UpdateGameWorld";
            _hubContext.Clients.All.SendAsync(RemoteWebAppMethod, generation.ToPatternViewVM());
        }
    }
}
