using System;

namespace GameOfLife.Engine
{
    public static class ArrayExtensions
    {
        public static TInput[,] Change<TInput>(this TInput[,] array, int row, int column, TInput value)
        {
            int width = array.GetLength(1);
            int height = array.GetLength(0);
            TInput[,] clone = new TInput[height, width];

            for (int y = 0; y < array.GetLength(0); y++)
            {
                for (int x = 0; x < array.GetLength(1); x++)
                {
                    clone[y, x] = array[y, x];
                }
            }

            clone[row, column] = value;
            return clone;
        }

        public static TOutput[,] Select<TInput, TOutput>(this TInput[,] array, Func<TInput, int, int, TOutput> func)
        {
            int width = array.GetLength(1);
            int height = array.GetLength(0);
            TOutput[,] clone = new TOutput[height, width];

            for (int row = 0; row < array.GetLength(0); row++)
            {
                for (int column = 0; column < array.GetLength(1); column++)
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
