using GameOfLife.CSharp.Api.Models;
using GameOfLife.Engine;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace GameOfLife.CSharp.Api.Controllers
{
    [Route("api/world")]
    [ApiController]
    public class WorldPatternController : ControllerBase
    {
        private readonly IWorldPatternRepository _repository;

        public WorldPatternController(IWorldPatternRepository repository)
        {
            _repository = repository;
        }

        [ProducesResponseType(typeof(WorldPatternVM), 200)]
        [HttpGet]
        public IActionResult GetWorldPatterns(int userId)
        {
            var worldPatterns = _repository.GetUserPatterns(userId);
            var worldPatternVms = worldPatterns.Select(FromWorldPattern).ToList();
            return Ok(worldPatternVms);
        }

        [ProducesResponseType(typeof(WorldPatternVM), 200)]
        [HttpGet("{patternId:int}")]
        public IActionResult GetWorldPatternById(int patternId)
        {
            var worldPattern = _repository.GetPatternById(patternId);
            var worldPatternVm = FromWorldPattern(worldPattern);
            return Ok(worldPatternVm);
        }

        [ProducesResponseType(typeof(WorldPatternVM), 200)]
        [HttpPost]
        public IActionResult CreateWorldPattern([FromBody] WorldPatternVM pattern)
        {
            int userId = 0;
            var worldPattern = WorldPattern.FromSize(userId, pattern.Name, pattern.Width, pattern.Height);
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

        [ProducesResponseType(200)]
        [HttpPost("{patternId:int}/game")]
        public IActionResult StartGameFromPattern(int patternId)
        {
            var worldPattern = _repository.GetPatternById(patternId);
            var time = new Time();
            time.Start(worldPattern);
            return Ok();
        }

        private WorldPatternVM FromWorldPattern(WorldPattern pattern)
        {
            return new WorldPatternVM { Height = pattern.Height, Width = pattern.Width };
        }
    }
}