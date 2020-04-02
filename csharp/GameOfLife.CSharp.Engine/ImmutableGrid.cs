using System;

namespace GameOfLife.CSharp.Engine
{
    public class ImmutableGrid : IImmutableGrid
    {
        protected readonly Cell[,] _cells;

        protected ImmutableGrid(Cell[,] cells, Size size)
        {
            _cells = cells ?? throw new ArgumentNullException(nameof(cells));
            Size = size ?? throw new ArgumentNullException(nameof(size));
        }

        public static ImmutableGrid Empty => new ImmutableGrid(new Cell[0, 0], Size.None);

        public static ImmutableGrid FromPattern(PopulationPattern pattern)
        {
            if (pattern is null) throw new ArgumentNullException(nameof(pattern));

            Size size = new Size(pattern.Width, pattern.Height);
            Cell[,] cells = pattern.Select(alive => Cell.Create(alive ? Population.Alive : Population.Dead));
            return new ImmutableGrid(cells, size);
        }

        public static ImmutableGrid FromState(Cell[,] cells)
        {
            int width = cells.GetLength(1);
            int height = cells.GetLength(0);
            var size = new Size(width, height);
            return new ImmutableGrid(cells, size);
        }

        public static ImmutableGrid FromSize(int width, int height)
        {
            Cell[,] cells = new Cell[width, height];
            var size = new Size(width, height);
            return new ImmutableGrid(cells, size);
        }

        public Cell this[int relativeRow, int relativeColumn] => InternalGet(relativeRow, relativeColumn);

        public Size Size { get; }

        public Cell Get(int row, int column) => InternalGet(row, column);

        public IMutableGrid ToMutable() => MutableGrid.FromState((Cell[,])_cells.Clone());

        protected Cell InternalGet(int row, int column)
        {
            return ImmutableGridExtensions.IsValidIndex(Size, row, column) ? _cells[row, column] : Cell.Empty;
        }
    }
}
