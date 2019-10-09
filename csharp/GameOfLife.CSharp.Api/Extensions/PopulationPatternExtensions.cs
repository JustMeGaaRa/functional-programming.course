using GameOfLife.CSharp.Api.Models;
using GameOfLife.Engine;

namespace GameOfLife.CSharp.Api.Extensions
{
    public static class PopulationPatternExtensions
    {
        public static PopulationPatternInfoVM? ToPatternInfoVM(this PopulationPattern pattern)
        {
            return pattern == null
                ? null
                : new PopulationPatternInfoVM
                {
                    PatternId = pattern.PatternId,
                    Name = pattern.Name,
                    Height = pattern.Height,
                    Width = pattern.Width
                };
        }
    }
}
