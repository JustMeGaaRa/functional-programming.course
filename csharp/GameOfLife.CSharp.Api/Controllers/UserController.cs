using GameOfLife.CSharp.Api.Extensions;
using GameOfLife.CSharp.Api.Models;
using GameOfLife.Engine;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace GameOfLife.CSharp.Api.Controllers
{
    [Route("api/users/{userId:int}")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IWorldPatternRepository _repository;

        public UserController(IWorldPatternRepository repository)
        {
            _repository = repository;
        }

        [ProducesResponseType(typeof(WorldPatternVM), 200)]
        [HttpGet("worlds")]
        public IActionResult GetWorldPatternsByUserId(int userId)
        {
            var worldPatterns = _repository.GetUserPatterns(userId);
            var worldPatternVms = worldPatterns
                .Select(PopulationPatternExtensions.ToWorldPatternVM)
                .ToList();
            return Ok(worldPatternVms);
        }
    }
}