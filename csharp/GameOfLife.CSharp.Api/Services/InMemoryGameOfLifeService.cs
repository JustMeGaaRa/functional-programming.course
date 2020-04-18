using GameOfLife.CSharp.Api.Extensions;
using GameOfLife.CSharp.Api.Hubs;
using GameOfLife.CSharp.Engine;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;

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

        public bool StartGame(int userId, Guid instanceId)
        {
            if (_activeGames.ContainsKey((userId, instanceId)))
            {
                _activeGames[(userId, instanceId)].Subscribe(PushGenerationToHub);
                _activeGames[(userId, instanceId)].Start();
                return true;
            }

            return false;
        }

        public bool EndGame(int userId, Guid instanceId)
        {
            if (_activeGames.Remove((userId, instanceId), out Time? time))
            {
                time?.Dispose();
            }

            return false;
        }

        public bool MergeGames(int userId, Guid firstId, Guid secondId)
        {
            throw new NotImplementedException();
        }

        private void PushGenerationToHub(Generation generation)
        {
            const string RemoteWebAppMethod = "UpdateGameWorld";
            _hubContext.Clients.All.SendAsync(RemoteWebAppMethod, generation.ToPatternViewVM());
        }
    }
}
