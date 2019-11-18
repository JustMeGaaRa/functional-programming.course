using GameOfLife.Engine;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace GameOfLife.CSharp.Api.Infrastructure
{
    public class InMemoryWorldPatternRepository : IWorldPatternRepository
    {
        private readonly ICollection<PopulationPattern> _patterns;
        private volatile int _identity = 0;

        public InMemoryWorldPatternRepository()
        {
            _patterns = new List<PopulationPattern>(PopulationPatterns.AllPatterns);
            _identity = _patterns.Max(x => x.PatternId);
        }

        public PopulationPattern SavePattern(PopulationPattern pattern)
        {
            _patterns.Add(pattern);
            pattern.PatternId = Interlocked.Increment(ref _identity);
            return pattern;
        }

        public PopulationPattern GetPatternById(int patternId)
        {
            return _patterns.FirstOrDefault(x => x.PatternId == patternId);
        }

        public ICollection<PopulationPattern> GetUserPatterns(int userId)
        {
            // TODO: add a reference to user and filter by userId
            return _patterns.ToList();
        }
    }
}
