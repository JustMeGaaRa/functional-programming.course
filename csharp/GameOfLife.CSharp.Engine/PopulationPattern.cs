using System;

namespace GameOfLife.Engine
{
    public class PopulationPattern
    {
        private readonly bool[,] _pattern;

        private PopulationPattern(int patternId, string name, int width, int height)
        {
            _pattern = new bool[height, width];
            PatternId = patternId;
            Name = name;
            Width = width;
            Height = height;
        }

        public bool this[int row, int column] => _pattern[row, column];

        public int PatternId { get; }

        public string Name { get; }

        public int Width { get; }

        public int Height { get; }

        public bool IsNew() => PatternId < 0;

        public bool TrySetCellState(int row, int column, bool alive)
        {
            if (row >= 0 && column >= 0 && row < Width && column < Height)
            {
                return _pattern[row, column] = alive;
            }

            return false;
        }

        public bool IsCellAlive(int row, int column)
        {
            return row >= 0 && column >= 0 && row < Width && column < Height && _pattern[row, column];
        }

        public static PopulationPattern FromSize(int patternId, string name, int width, int height)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Parameter cannot be null, empty or whitespace.", nameof(name));
            }

            return new PopulationPattern(patternId, name, width, height);
        }

        public static PopulationPattern FromArray2D(int patternId, string name, bool[,] aliveCells)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Parameter cannot be null, empty or whitespace.", nameof(name));
            }

            int width = aliveCells.GetLength(0);
            int height = aliveCells.GetLength(1);
            var pattern = new PopulationPattern(patternId, name, width, height);

            for (int row = 0; row < height; row++)
            {
                for (int column = 0; column < width; column++)
                {
                    pattern.TrySetCellState(row, column, aliveCells[row, column]);
                }
            }

            return pattern;
        }
    }
}
