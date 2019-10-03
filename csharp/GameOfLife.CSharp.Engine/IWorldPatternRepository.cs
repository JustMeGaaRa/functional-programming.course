using System.Collections.Generic;

namespace GameOfLife.Engine
{
    public interface IWorldPatternRepository
    {
        WorldPattern CreatePattern(WorldPattern pattern);

        WorldPattern GetPatternById(int patternId);

        ICollection<WorldPattern> GetUserPatterns(int userId);
    }
}
