using System.Collections.Generic;

namespace GameOfLife.CSharp.Engine
{
    public interface IWorldPatternRepository
    {
        PopulationPattern SavePattern(PopulationPattern pattern);

        PopulationPattern GetPatternById(int patternId);

        ICollection<PopulationPattern> GetUserPatterns(int userId);
    }
}
