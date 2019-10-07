using GameOfLife.CSharp.Api.Models;
using GameOfLife.Engine;

namespace GameOfLife.CSharp.Api.Extensions
{
    public static class PopulationPatternExtensions
    {
        public static WorldPatternVM? ToWorldPatternVM(this PopulationPattern pattern)
        {
            return pattern == null
                ? null
                : new WorldPatternVM
                {
                    PatternId = pattern.PatternId,
                    Name = pattern.Name,
                    Height = pattern.Height,
                    Width = pattern.Width
                };
        }
    }
}
