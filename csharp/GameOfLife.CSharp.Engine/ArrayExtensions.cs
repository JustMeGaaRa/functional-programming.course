using System;

namespace GameOfLife.Engine
{
    public static class ArrayExtensions
    {
        public static TOutput[,] Select<TInput, TOutput>(this TInput[,] array, Func<TInput, int, int, TOutput> func)
        {
            int width = array.GetLength(1);
            int height = array.GetLength(0);
            TOutput[,] clone = new TOutput[height, width];

            for (int row = 0; row < height; row++)
            {
                for (int column = 0; column < width; column++)
                {
                    clone[row, column] = func(array[row, column], row, column);
                }
            }

            return clone;
        }

        public static TOutput[,] Select<TInput, TOutput>(this TInput[,] array, Func<TInput, TOutput> func)
        {
            return Select(array, (value, row, column) => func(value));
        }
    }
}
