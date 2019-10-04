using GameOfLife.CSharp.Api.Models;
using GameOfLife.CSharp.Api.Services;
using GameOfLife.Engine;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace GameOfLife.CSharp.Api.Controllers
{
    [Route("api/world")]
    [ApiController]
    public class WorldPatternController : ControllerBase
    {
        private readonly IWorldPatternRepository _repository;
        private readonly IGameOfLifeService _gameOfLifeService;

        public WorldPatternController(IWorldPatternRepository repository, IGameOfLifeService gameOfLifeService)
        {
            _repository = repository;
            _gameOfLifeService = gameOfLifeService;
        }

        [ProducesResponseType(typeof(WorldPatternVM), 200)]
        [HttpGet]
        public IActionResult GetWorldPatterns(int userId)
        {
            var worldPatterns = _repository.GetUserPatterns(userId);
            var worldPatternVms = worldPatterns.Select(ToWorldPatternVM).ToList();
            return Ok(worldPatternVms);
        }

        [ProducesResponseType(typeof(WorldPatternVM), 200)]
        [HttpGet("{patternId:int}")]
        public IActionResult GetWorldPatternById(int patternId)
        {
            var worldPattern = _repository.GetPatternById(patternId);
            var worldPatternVm = ToWorldPatternVM(worldPattern);
            return Ok(worldPatternVm);
        }

        [ProducesResponseType(typeof(WorldPatternVM), 200)]
        [HttpPost]
        public IActionResult CreateWorldPattern([FromBody] WorldPatternVM pattern)
        {
            var worldPattern = PopulationPattern.FromSize(pattern.Name, pattern.Width, pattern.Height);
            return Ok(_repository.CreatePattern(worldPattern));
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [HttpPut("{patternId:int}/cell")]
        public IActionResult SetWorldPatternCellState(int patternId, [FromBody] WorldCellVM cell)
        {
            var worldPattern = _repository.GetPatternById(patternId);
            var success = worldPattern.TrySetCellState(cell.Row, cell.Column, cell.Alive);
            return success ? Ok() : StatusCode(500);
        }

        [ProducesResponseType(typeof(WorldPatternVM), 200)]
        [HttpPost("{patternId:int}/game")]
        public async Task<IActionResult> StartGameFromPattern(int patternId)
        {
            int userId = 0;
            var generation = await _gameOfLifeService.StartGameAsync(userId, patternId);
            var generationVm = ToGenerationVM(generation);
            return Ok(generationVm);
        }

        private WorldPatternVM ToWorldPatternVM(PopulationPattern pattern)
        {
            return new WorldPatternVM { Name = pattern.Name, Height = pattern.Height, Width = pattern.Width };
        }

        private GenerationVM ToGenerationVM(Generation generation)
        {
            return new GenerationVM { Height = generation.Size.Height, Width = generation.Size.Width };
        }
    }
}