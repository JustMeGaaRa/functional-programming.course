using System.Collections.Generic;

namespace GameOfLife.CSharp.Engine
{
    public interface IPopulationPatternRepository
    {
        PopulationPattern SavePattern(PopulationPattern pattern);

        PopulationPattern GetPatternById(int patternId);

        ICollection<PopulationPattern> GetUserPatterns(int userId);
    }
}
