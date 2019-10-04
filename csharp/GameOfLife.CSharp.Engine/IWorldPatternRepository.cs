using System.Collections.Generic;

namespace GameOfLife.Engine
{
    public interface IWorldPatternRepository
    {
        PopulationPattern CreatePattern(PopulationPattern pattern);

        PopulationPattern GetPatternById(int patternId);

        ICollection<PopulationPattern> GetUserPatterns(int userId);
    }
}
