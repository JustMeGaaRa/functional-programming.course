using GameOfLife.CSharp.Api.Extensions;
using GameOfLife.CSharp.Api.Models;
using GameOfLife.Engine;
using Microsoft.AspNetCore.Mvc;

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
        [HttpGet("{patternId:int}")]
        public IActionResult GetWorldPatternById(int patternId)
        {
            var worldPattern = _repository.GetPatternById(patternId);
            var worldPatternVm = worldPattern.ToWorldPatternVM();
            return Ok(worldPatternVm);
        }

        [ProducesResponseType(typeof(WorldPatternVM), 200)]
        [HttpPost]
        public IActionResult CreateWorldPattern([FromBody] WorldPatternVM pattern)
        {
            var worldPattern = PopulationPattern.FromSize(pattern.Name, pattern.Width, pattern.Height);
            worldPattern = _repository.CreatePattern(worldPattern);
            var worldPatternVm = worldPattern.ToWorldPatternVM();
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
    }
}