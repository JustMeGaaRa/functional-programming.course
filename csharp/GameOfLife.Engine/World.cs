using System.Linq;

namespace GameOfLife.Engine
{
    public class World
    {
        private readonly Cell[,] _cells;

        private World(Cell[,] cells, Size size)
        {
            _cells = cells;
            Size = size;
        }

        public static World Empty => new World(new Cell[0, 0], Size.None);

        public static World FromSize(int width, int height)
        {
            Size size = new Size(width, height);
            Cell[,] cells = new Cell[width, height];
            return new World(cells, size);
        }

        public static World FromPattern(Population[,] states)
        {
            Size size = new Size(states.GetLength(1), states.GetLength(0));
            Cell[,] cells = states.Select(Cell.Create);
            return new World(cells, size);
        }

        public Cell this[int row, int column] => _cells[row, column];

        public Size Size { get; }

        public World Evolve()
        {
            Cell[,] cells = _cells
                .Select((cell, row, column) => GetNextPopulationState(_cells, Size, row, column))
                .Select(Cell.Create);
            return new World(cells, Size);
        }

        private Population GetNextPopulationState(Cell[,] cells, Size size, int row, int column)
        {
            var cell = cells[row, column];
            int aliveNeighbours = CountAliveNeighbours(cells, size, row, column);
            var state = (cell, aliveNeighbours) switch
            {
                // Any live cell with fewer than two live neighbours dies, as if by underpopulation.
                ({ Population: Population.Alive }, var alive) when alive < 2 => Population.Dead,

                // Any live cell with two or three live neighbours lives on to the next generation.
                ({ Population: Population.Alive }, var alive) when alive < 4 => Population.Alive,

                // Any live cell with more than three live neighbours dies, as if by overpopulation.
                ({ Population: Population.Alive }, var alive) when alive > 3 => Population.Dead,

                // Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.
                ({ Population: Population.Dead }, 3) => Population.Alive,

                // Any other case it remains dead.
                _ => Population.Dead
            };

            return state;
        }

        private int CountAliveNeighbours(Cell[,] cells, Size size, int row, int column)
        {
            (int row, int column)[] indicies = new[]
            {
                (row - 1, column - 1),
                (row - 1, column),
                (row - 1, column + 1),
                (row, column - 1),
                (row, column + 1),
                (row + 1, column - 1),
                (row + 1, column),
                (row + 1, column + 1)
            };
            return indicies.Count(position => CheckPopulationSafely(cells, size, position.row, position.column));
        }

        private bool CheckPopulationSafely(Cell[,] cells, Size size, int row, int column)
        {
            return row >= 0 && column >= 0
                && row < size.Height && column < size.Width
                && cells[row, column].Population.IsAlive();
        }
    }
}
