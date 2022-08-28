using GameOfLife.CSharp.Engine;
using System.Collections.Generic;
using System.Linq;

namespace GameOfLife.CSharp.Api.Infrastructure
{
    public class InMemoryWorldPatternRepository : IWorldPatternRepository
    {
        private readonly Dictionary<int, PopulationPattern> _patterns = new ();

        public InMemoryWorldPatternRepository()
        {
            int i = 0;
            _patterns = PopulationPatterns.AllPatterns.ToDictionary(
                key => i++,
                value => value);
        }

        public PopulationPattern? SavePattern(PopulationPattern pattern)
        {
            return pattern != null
                ? _patterns[pattern.PatternId] = pattern
                : null;
        }

        public PopulationPattern? GetPatternById(int patternId)
        {
            return _patterns.ContainsKey(patternId)
                ? _patterns[patternId]
                : null;
        }

        public ICollection<PopulationPattern> GetUserPatterns(int userId)
        {
            // TODO: add a reference to user and filter by userId
            return _patterns.Values;
        }
    }
}
