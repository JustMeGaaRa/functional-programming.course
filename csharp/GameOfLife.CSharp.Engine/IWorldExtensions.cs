using System;
using System.Linq;

namespace GameOfLife.CSharp.Engine
{
    public static class IWorldExtensions
    {
        public static bool IsInBounds(this IWorld world, int absoluteRow, int absoluteColumn)
        {
            if (world is null) throw new ArgumentNullException(nameof(world));

            return world.TopLeft.Left <= absoluteColumn
                && world.TopLeft.Top <= absoluteRow
                && absoluteColumn < world.BottomRight.Left
                && absoluteRow < world.BottomRight.Top;
        }

        public static bool IsValidIndex(this IWorld world, int relativeRow, int relativeColumn)
        {
            if (world is null) throw new ArgumentNullException(nameof(world));

            return relativeRow >= 0
                && relativeColumn >= 0
                && relativeRow < world.Size.Height
                && relativeColumn < world.Size.Width;
        }

        public static IWorld Evolve(this IWorld world)
        {
            Cell[,] clone = new Cell[world.Size.Height, world.Size.Width];

            for (int row = 0; row < world.Size.Height; row++)
            {
                for (int column = 0; column < world.Size.Width; column++)
                {
                    clone[row, column] = Cell.Create(GetNextPopulationState(world, row, column));
                }
            }

            return World.FromState(clone, world.TopLeft, world.Size);
        }

        private static Population GetNextPopulationState(IWorld grid, int row, int column)
        {
            int aliveNeighbours = CountAliveNeighbours(grid, row, column);
            var state = (grid[row, column], aliveNeighbours) switch
            {
                // Any empty cell with should remain empty.
                ({ Population: Population.Empty }, _) => Population.Empty,

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

        private static int CountAliveNeighbours(IWorld grid, int row, int column)
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
            return indicies.Count(position => grid.IsCellAliveBySelfOffset(position.row, position.column));
        }
    }
}
