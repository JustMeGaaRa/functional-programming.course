﻿using System;

namespace GameOfLife.CSharp.Engine
{
    public class MutableGrid : IMutableGrid
    {
        protected readonly Cell[,] _cells;

        protected MutableGrid(Cell[,] cells, Size size)
        {
            _cells = cells ?? throw new ArgumentNullException(nameof(cells));
            Size = size ?? throw new ArgumentNullException(nameof(size));
        }

        public static MutableGrid Empty => new MutableGrid(new Cell[0, 0], Size.None);

        public static MutableGrid FromState(Cell[,] cells)
        {
            int width = cells.GetLength(1);
            int height = cells.GetLength(0);
            var size = new Size(width, height);
            return new MutableGrid(cells, size);
        }

        public static MutableGrid FromSize(int width, int height)
        {
            Cell[,] cells = new Cell[width, height];
            var size = new Size(width, height);
            return new MutableGrid(cells, size);
        }

        public Size Size { get; }

        public void Set(int row, int column, Cell cell) => InternalSet(row, column, cell);

        public IImmutableGrid ToImmutable() => ImmutableGrid.FromState(_cells);

        protected void InternalSet(int row, int column, Cell cell)
        {
            if (ImmutableGridExtensions.IsValidIndex(Size, row, column)) _cells[row, column] = cell;
        }
    }
}