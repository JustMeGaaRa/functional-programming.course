using GameOfLife.CSharp.Api.Models;
using GameOfLife.Engine;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace GameOfLife.CSharp.Api.Controllers
{
    [Route("api/worlds")]
    [ApiController]
    public class WorldPatternController : ControllerBase
    {
        private readonly IWorldPatternRepository _repository;

        public WorldPatternController(IWorldPatternRepository repository)
        {
            _repository = repository;
        }

        [ProducesResponseType(typeof(WorldPatternVM), 200)]
        [HttpGet("~/api/users/{userId:int}/worlds")]
        public IActionResult GetWorldPatternsByUserId(int userId)
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
            worldPattern = _repository.CreatePattern(worldPattern);
            var worldPatternVm = ToWorldPatternVM(worldPattern);
            return Ok(worldPatternVm);
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

        private WorldPatternVM ToWorldPatternVM(PopulationPattern pattern)
        {
            return new WorldPatternVM
            {
                PatternId = pattern.PatternId,
                Name = pattern.Name,
                Height = pattern.Height,
                Width = pattern.Width
            };
        }
    }
}