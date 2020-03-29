using System;

namespace GameOfLife.CSharp.Engine
{
    public static class PopulationPatternExtensions
    {
        public static TOutput[,] Select<TOutput>(this PopulationPattern pattern, Func<bool, int, int, TOutput> func)
        {
            TOutput[,] clone = new TOutput[pattern.Height, pattern.Width];

            for (int row = 0; row < pattern.Height; row++)
            {
                for (int column = 0; column < pattern.Width; column++)
                {
                    clone[row, column] = func(pattern[row, column], row, column);
                }
            }

            return clone;
        }

        public static TOutput[,] Select<TOutput>(this PopulationPattern pattern, Func<bool, TOutput> func)
        {
            return Select(pattern, (value, row, column) => func(value));
        }
    }
}
