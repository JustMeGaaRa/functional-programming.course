using GameOfLife.Engine;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace GameOfLife.CSharp.Api.Infrastructure
{
    public class InMemoryWorldPatternRepository : IWorldPatternRepository
    {
        private readonly ICollection<WorldPattern> _patterns;
        private int _identity = 0;

        public InMemoryWorldPatternRepository()
        {
            _patterns = new List<WorldPattern>();
        }

        public WorldPattern CreatePattern(WorldPattern pattern)
        {
            _patterns.Add(pattern);
            Interlocked.Increment(ref _identity);
            pattern.WorldPatternId = _identity;
            return pattern;
        }

        public WorldPattern GetPatternById(int patternId)
        {
            return _patterns.FirstOrDefault(x => x.WorldPatternId == patternId);
        }

        public ICollection<WorldPattern> GetUserPatterns(int userId)
        {
            return _patterns.Where(x => x.UserId == userId).ToList();
        }
    }
}
