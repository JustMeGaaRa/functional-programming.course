using System.Linq;

namespace GameOfLife.CSharp.Engine
{
    public static class ImmutableGridExtensions
    {
        public static bool IsValidIndex(Size size, int relativeRow, int relativeColumn)
        {
            return relativeRow >= 0
                && relativeColumn >= 0
                && relativeRow < size.Height
                && relativeColumn < size.Width;
        }

        public static void Evolve(this IImmutableGrid immutableGrid, IMutableGrid mutableGrid)
        {
            for (int row = 0; row < immutableGrid.Size.Height; row++)
            {
                for (int column = 0; column < immutableGrid.Size.Width; column++)
                {
                    var state = GetNextPopulationState(immutableGrid, row, column);
                    var cell = Cell.Create(state);
                    mutableGrid.Set(row, column, cell);
                }
            }
        }

        private static Population GetNextPopulationState(IImmutableGrid grid, int row, int column)
        {
            int aliveNeighbours = CountAliveNeighbours(grid, row, column);
            var state = (grid[row, column], aliveNeighbours) switch
            {
                // Any empty cell with should remain empty.
                ({ Population: Population.None }, _) => Population.None,

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

        private static int CountAliveNeighbours(IImmutableGrid grid, int row, int column)
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
            return indicies.Count(position => grid.Get(position.row, position.column).IsAlive());
        }
    }
}
