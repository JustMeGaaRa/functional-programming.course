using System.Collections.Generic;

namespace GameOfLife.Engine
{
    public interface IWorldPatternRepository
    {
        PopulationPattern SavePattern(PopulationPattern pattern);

        PopulationPattern GetPatternById(int patternId);

        ICollection<PopulationPattern> GetUserPatterns(int userId);
    }
}
