using GameOfLife.CSharp.Api.Extensions;
using GameOfLife.CSharp.Api.Models;
using GameOfLife.CSharp.Api.Services;
using GameOfLife.CSharp.Engine;
using GameOfLife.Engine;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace GameOfLife.CSharp.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class PopulationPatternController : ControllerBase
    {
        private readonly IPopulationPatternRepository _repository;
        private readonly IGameOfLifeService _gameService;

        public PopulationPatternController(
            IPopulationPatternRepository repository,
            IGameOfLifeService gameService)
        {
            _repository = repository;
            _gameService = gameService;
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
            int patternId = PopulationPatternIdentity.GenerateIdentity();
            var populationPattern = PopulationPattern.FromSize(patternId, pattern.Name, pattern.Width, pattern.Height);
            populationPattern = _repository.SavePattern(populationPattern);
            var populationPatternVm = populationPattern.ToPatternInfoVM();
            return Ok(populationPatternVm);
        }

        [ProducesResponseType(typeof(PopulationPatternInfoVM), 200)]
        [HttpGet("{userId:int}/patterns")]
        public IActionResult GetPopulationPatternInfosByUserId(int userId)
        {
            var populationPattern = _repository.GetUserPatterns(userId);
            var populationPatternVms = populationPattern
                .Select(Extensions.PopulationPatternExtensions.ToPatternInfoVM)
                .ToList();
            return Ok(populationPatternVms);
        }

        [ProducesResponseType(typeof(PopulationPatternInfoVM), 200)]
        [HttpGet("{userId:int}/patterns/{patternId:int}/info")]
        public IActionResult GetPopulationPatternById(int userId, int patternId)
        {
            var populationPattern = _repository.GetPatternById(patternId);
            var populationPatternVm = populationPattern.ToPatternInfoVM();
            return Ok(populationPatternVm);
        }

        [ProducesResponseType(typeof(PopulationPatternViewVM), 200)]
        [HttpGet("{userId:int}/patterns/{patternId:int}/view")]
        public IActionResult GetPopulationPatternViewsByUserId(int userId, int patternId)
        {
            var generation = _gameService.CreateFromPattern(userId, patternId);
            var populationPatternVm = generation.ToPatternViewVM();
            return Ok(populationPatternVm);
        }

        [ProducesResponseType(typeof(PopulationPatternViewVM), 200)]
        [HttpPut("{userId:int}/patterns/{patternId:int}/view/cell")]
        public IActionResult SetPopulationPatternCellState(int userId, int patternId, [FromBody] PopulationPatternCellVM cell)
        {
            var populationPattern = _repository.GetPatternById(patternId);
            _ = populationPattern.TrySetCellState(cell.Row, cell.Column, cell.IsAlive);
            populationPattern = _repository.SavePattern(populationPattern);
            var populationPatternVm = Generation.Zero(populationPattern).ToPatternViewVM();
            return Ok(populationPatternVm);
        }
    }
}