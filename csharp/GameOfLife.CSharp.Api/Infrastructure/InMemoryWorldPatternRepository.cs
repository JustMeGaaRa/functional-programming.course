using GameOfLife.Engine;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace GameOfLife.CSharp.Api.Infrastructure
{
    public class InMemoryWorldPatternRepository : IWorldPatternRepository
    {
        private readonly ICollection<PopulationPattern> _patterns;
        private int _identity = 0;

        public InMemoryWorldPatternRepository()
        {
            _patterns = new List<PopulationPattern>(PopulationPatterns.AllPatterns);
        }

        public PopulationPattern CreatePattern(PopulationPattern pattern)
        {
            _patterns.Add(pattern);
            Interlocked.Increment(ref _identity);
            pattern.WorldPatternId = _identity;
            return pattern;
        }

        public PopulationPattern GetPatternById(int patternId)
        {
            return _patterns.FirstOrDefault(x => x.WorldPatternId == patternId);
        }

        public ICollection<PopulationPattern> GetUserPatterns(int userId)
        {
            // TODO: add a reference to user and filter by userId
            return _patterns.ToList();
        }
    }
}
