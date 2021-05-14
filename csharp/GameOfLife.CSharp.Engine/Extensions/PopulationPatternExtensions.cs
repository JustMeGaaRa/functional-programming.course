using System;
using System.Collections.Generic;

namespace GameOfLife.CSharp.Engine
{
    public static class PopulationPatternExtensions
    {
        public static IEnumerable<TOutput> ToEnumerable<TOutput>(this PopulationPattern pattern, Func<bool, TOutput> func)
        {
            for (int row = 0; row < pattern.Height; row++)
            {
                for (int column = 0; column < pattern.Width; column++)
                {
                    yield return func(pattern[row, column]);
                }
            }
        }

        public static TOutput[,] ToArray<TOutput>(this PopulationPattern pattern, Func<bool, int, int, TOutput> func)
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

        public static TOutput[,] ToArray<TOutput>(this PopulationPattern pattern, Func<bool, TOutput> func)
        {
            return ToArray(pattern, (value, row, column) => func(value));
        }
    }
}
