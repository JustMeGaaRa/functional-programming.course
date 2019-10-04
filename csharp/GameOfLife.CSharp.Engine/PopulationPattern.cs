namespace GameOfLife.Engine
{
    public class PopulationPattern
    {
        private readonly bool[,] _pattern;

        public PopulationPattern(string name, int width, int height)
        {
            _pattern = new bool[height, width];
            Name = name;
            Width = width;
            Height = height;
        }

        public bool this[int row, int column] => _pattern[row, column];

        public int WorldPatternId { get; set; }

        public string Name { get; }

        public int Width { get; }

        public int Height { get; }

        public bool TrySetCellState(int row, int column, bool alive) => _pattern[row, column] = alive;

        public bool IsCellAlive(int row, int column) => _pattern[row, column];

        public static PopulationPattern FromSize(string name, int width, int height)
        {
            return new PopulationPattern(name, width, height);
        }

        public static PopulationPattern FromArray2D(string name, bool[,] aliveCells)
        {
            int width = aliveCells.GetLength(0);
            int height = aliveCells.GetLength(1);
            var pattern = new PopulationPattern(name, width, height);

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
