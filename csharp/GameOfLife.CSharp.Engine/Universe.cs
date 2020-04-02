using System;
using System.Collections.Generic;
using System.Linq;

namespace GameOfLife.CSharp.Engine
{
    public class Universe : IUniverse
    {
        private readonly List<ImmutableIsland> _islands = new List<ImmutableIsland>();

        private Universe()
        {
            Size = Size.None;
            Worlds = new List<IImmutableGrid>();
        }

        private Universe(List<ImmutableIsland> islands, Size size)
        {
            _islands = islands;
            Size = size;
            Worlds = islands.Select(x => x.ImmutableGrid).ToList();
        }

        #region Public Properties

        public static IUniverse Empty => new Universe();

        public Cell this[int row, int column] => InternalFindAndGet(row, column) ?? Cell.Empty;

        public Size Size { get; }

        public ICollection<IImmutableGrid> Worlds { get; }

        #endregion

        #region Public Methods

        public static IUniverse FromPattern(PopulationPattern pattern)
        {
            if (pattern is null) throw new ArgumentNullException(nameof(pattern));

            Cell[,] cells = pattern.Select(alive => Cell.Create(alive ? Population.Alive : Population.Dead));
            return Empty.Join(ImmutableGrid.FromState(cells), Offset.None);
        }

        public Cell Get(int row, int column) => InternalFindAndGet(row, column) ?? Cell.Empty;

        IMutableUniverse IMutableConverter<IMutableUniverse>.ToMutable() => ToMutableGrid();

        IMutableGrid IMutableConverter<IMutableGrid>.ToMutable() => ToMutableGrid();

        public IUniverse Evolve()
        {
            IUniverse immutableUniverse = this;
            IMutableConverter<IMutableUniverse> mutableConverter = immutableUniverse;

            IMutableUniverse mutableUniverse = mutableConverter.ToMutable();
            IImmutableConverter<IUniverse> immutableConverter = mutableUniverse;

            immutableUniverse.Evolve(mutableUniverse);

            return immutableConverter.ToImmutable();
        }

        public IUniverse Join(IImmutableGrid immutableGrid, Offset offset)
        {
            if (immutableGrid is null) throw new ArgumentNullException(nameof(immutableGrid));

            var island = new ImmutableIsland(immutableGrid, offset);

            int left = Math.Min(0, island.TopLeft.Left);
            int top = Math.Min(0, island.TopLeft.Top);
            int right = Math.Max(Size.Width, island.BottomRight.Left);
            int bottom = Math.Max(Size.Height, island.BottomRight.Top);

            var shift = new Offset(Math.Abs(left), Math.Abs(top));
            var size = new Size(right - left, bottom - top);

            var islands = _islands
                .Concat(new ImmutableIsland[] { island })
                .Select(island => island.Move(shift.Left, shift.Top))
                .ToList();

            return new Universe(islands, size);
        }

        public ICollection<IUniverse> Split(IImmutableGrid immutableGrid)
        {
            var island = _islands.FirstOrDefault(island => island.ImmutableGrid == immutableGrid);

            if (island != null && _islands.Remove(island))
            {
                return new List<IUniverse>();
            }

            return new List<IUniverse>();
        }

        #endregion

        #region Private Methods

        private Cell? InternalFindAndGet(int row, int column)
        {
            return _islands
                .Where(island => island.IsInBounds(row, column))
                .Select(island => InternalGet(island, row, column))
                .FirstOrDefault();
        }

        private Cell? InternalGet(ImmutableIsland island, int row, int column)
        {
            int relativeRow = row - island.TopLeft.Top;
            int relativeColumn = column - island.TopLeft.Left;
            return island.ImmutableGrid[relativeRow, relativeColumn];
        }

        private MutableGridAggregate ToMutableGrid()
        {
            var islands = _islands
                .Select(island => island.ToMutable())
                .ToList();
            return new MutableGridAggregate(islands, Size);
        }

        #endregion

        #region Private Internal Types

        internal class ImmutableIsland : IMoveable<ImmutableIsland>, IMutableConverter<MutableIsland>
        {
            public ImmutableIsland(IImmutableGrid immutableGrid, Offset topLeft)
            {
                ImmutableGrid = immutableGrid ?? throw new ArgumentNullException(nameof(immutableGrid));
                TopLeft = topLeft ?? throw new ArgumentNullException(nameof(topLeft));
                BottomRight = new Offset(topLeft.Left + immutableGrid.Size.Width, topLeft.Top + immutableGrid.Size.Height);
            }

            public IImmutableGrid ImmutableGrid { get; }

            public Offset TopLeft { get; }

            public Offset BottomRight { get; }

            public ImmutableIsland Move(int shiftLeft, int shiftTop)
            {
                var shiftedTopLeft = new Offset(TopLeft.Left + shiftLeft, TopLeft.Top + shiftTop);
                return new ImmutableIsland(ImmutableGrid, shiftedTopLeft);
            }

            public bool IsInBounds(int absoluteRow, int absoluteColumn)
            {
                return TopLeft.Left <= absoluteColumn
                    && TopLeft.Top <= absoluteRow
                    && absoluteColumn < BottomRight.Left
                    && absoluteRow < BottomRight.Top;
            }

            public MutableIsland ToMutable() => new MutableIsland(ImmutableGrid.ToMutable(), TopLeft);
        }

        internal class MutableIsland : IImmutableConverter<ImmutableIsland>
        {
            public MutableIsland(IMutableGrid mutableGrid, Offset topLeft)
            {
                MutableGrid = mutableGrid ?? throw new ArgumentNullException(nameof(mutableGrid));
                TopLeft = topLeft ?? throw new ArgumentNullException(nameof(topLeft));
                BottomRight = new Offset(topLeft.Left + mutableGrid.Size.Width, topLeft.Top + mutableGrid.Size.Height);
            }

            public IMutableGrid MutableGrid { get; }

            public Offset TopLeft { get; }

            public Offset BottomRight { get; }

            public bool IsInBounds(int absoluteRow, int absoluteColumn)
            {
                return TopLeft.Left <= absoluteColumn
                    && TopLeft.Top <= absoluteRow
                    && absoluteColumn < BottomRight.Left
                    && absoluteRow < BottomRight.Top;
            }

            public ImmutableIsland ToImmutable() => new ImmutableIsland(MutableGrid.ToImmutable(), TopLeft);
        }

        internal class MutableGridAggregate : IMutableUniverse
        {
            private readonly List<MutableIsland> _islands = new List<MutableIsland>();

            public MutableGridAggregate(List<MutableIsland> islands, Size size)
            {
                _islands = islands ?? throw new ArgumentNullException(nameof(islands));
                Size = size ?? throw new ArgumentNullException(nameof(size));
            }

            public Size Size { get; }

            public void Set(int row, int column, Cell cell) => InternalFindAndSet(row, column, cell);

            IUniverse IImmutableConverter<IUniverse>.ToImmutable() => ToImmutableGrid();

            IImmutableGrid IImmutableConverter<IImmutableGrid>.ToImmutable() => ToImmutableGrid();

            private IUniverse ToImmutableGrid()
            {
                var islands = _islands
                    .Select(island => island.ToImmutable())
                    .ToList();
                return new Universe(islands, Size);
            }

            protected void InternalFindAndSet(int row, int column, Cell cell)
            {
                _islands
                    .Where(island => island.IsInBounds(row, column))
                    .ToList()
                    .ForEach(island => InternalSet(island, row, column, cell));
            }

            protected void InternalSet(MutableIsland island, int row, int column, Cell cell)
            {
                int relativeRow = row - island.TopLeft.Top;
                int relativeColumn = column - island.TopLeft.Left;
                var size = island.MutableGrid.Size;

                if (ImmutableGridExtensions.IsValidIndex(size, row, column))
                {
                    island.MutableGrid.Set(relativeRow, relativeColumn, cell);
                }
            }
        }

        #endregion
    }
}
