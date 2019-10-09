using GameOfLife.CSharp.Api.Extensions;
using GameOfLife.CSharp.Api.Models;
using GameOfLife.Engine;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace GameOfLife.CSharp.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class PopulationPatternController : ControllerBase
    {
        private readonly IWorldPatternRepository _repository;

        public PopulationPatternController(IWorldPatternRepository repository)
        {
            _repository = repository;
        }

        [ProducesResponseType(typeof(UserInfoVM), 200)]
        [HttpPost]
        public IActionResult RequestUserId()
        {
            var userInfoVm = new UserInfoVM { UserId = 1 };
            return Ok(userInfoVm);
        }

        [ProducesResponseType(typeof(PopulationPatternInfoVM), 200)]
        [HttpPost("{userId:int}/patterns")]
        public IActionResult CreatePopulationPattern(int userId, [FromBody] PopulationPatternInfoVM pattern)
        {
            var populationPattern = PopulationPattern.FromSize(pattern.Name, pattern.Width, pattern.Height);
            populationPattern = _repository.CreatePattern(populationPattern);
            var worldPatternVm = populationPattern.ToPatternInfoVM();
            return Ok(worldPatternVm);
        }

        [ProducesResponseType(typeof(PopulationPatternInfoVM), 200)]
        [HttpGet("{userId:int}/patterns")]
        public IActionResult GetPopulationPatternInfosByUserId(int userId)
        {
            var worldPatterns = _repository.GetUserPatterns(userId);
            var worldPatternVms = worldPatterns
                .Select(PopulationPatternExtensions.ToPatternInfoVM)
                .ToList();
            return Ok(worldPatternVms);
        }

        [ProducesResponseType(typeof(PopulationPatternInfoVM), 200)]
        [HttpGet("{userId:int}/patterns/{patternId:int}/info")]
        public IActionResult GetPopulationPatternById(int userId, int patternId)
        {
            var worldPattern = _repository.GetPatternById(patternId);
            var worldPatternVm = worldPattern.ToPatternInfoVM();
            return Ok(worldPatternVm);
        }

        [ProducesResponseType(typeof(PopulationPatternViewVM), 200)]
        [HttpGet("{userId:int}/patterns/{patternId:int}/view")]
        public IActionResult GetPopulationPatternViewsByUserId(int userId, int patternId)
        {
            var worldPattern = _repository.GetPatternById(patternId);
            var worldPatternVm = Generation.Zero(worldPattern).ToPatternViewVM();
            return Ok(worldPatternVm);
        }

        [ProducesResponseType(typeof(PopulationPatternViewVM), 200)]
        [HttpPut("{userId:int}/patterns/{patternId:int}/view/cell")]
        public IActionResult SetPopulationPatternCellState(int userId, int patternId, [FromBody] PopulationPatternCellVM cell)
        {
            var worldPattern = _repository.GetPatternById(patternId);
            _ = worldPattern.TrySetCellState(cell.Row, cell.Column, cell.IsAlive);
            var worldPatternVm = Generation.Zero(worldPattern).ToPatternViewVM();
            return Ok(worldPatternVm);
        }
    }
}