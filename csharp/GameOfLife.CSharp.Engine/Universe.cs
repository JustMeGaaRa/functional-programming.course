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
            Identity = Guid.NewGuid();
            Size = Size.None;
            Worlds = new List<IImmutableGrid>();
        }

        private Universe(Guid identity, List<ImmutableIsland> islands, Size size)
        {
            _islands = islands ?? throw new ArgumentNullException(nameof(islands));
            Size = size ?? throw new ArgumentNullException(nameof(size));
            Worlds = islands.Select(x => x.ImmutableGrid).ToList();
            Identity = identity;
        }

        public static IUniverse FromPattern(PopulationPattern pattern)
        {
            if (pattern is null) throw new ArgumentNullException(nameof(pattern));

            Cell[,] cells = pattern.Select(alive => Cell.Create(alive ? Population.Alive : Population.Dead));
            IImmutableGrid immutableGrid = ImmutableGrid.FromState(cells);
            List<ImmutableIsland> islands = new List<ImmutableIsland> { new ImmutableIsland(immutableGrid, Offset.None) };
            return new Universe(immutableGrid.Identity, islands, immutableGrid.Size);
        }

        #region Public Properties

        public static IUniverse Empty => new Universe();

        public Guid Identity { get; }

        public Cell this[int row, int column] => InternalFindAndGet(row, column) ?? Cell.Empty;

        public Size Size { get; }

        public ICollection<IImmutableGrid> Worlds { get; }

        #endregion

        #region Public Methods

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

        public IUniverse Join(IUniverse immutableGrid, Offset offset)
        {
            // TODO: Make offset for each island, not a general one
            if (immutableGrid is null) throw new ArgumentNullException(nameof(immutableGrid));

            var otherIslands = immutableGrid.Worlds.Select(grid => new ImmutableIsland(grid, offset));

            int left = Math.Min(0, otherIslands.Min(x => x.TopLeft.Left));
            int top = Math.Min(0, otherIslands.Min(x => x.TopLeft.Top));
            int right = Math.Max(Size.Width, otherIslands.Max(x => x.BottomRight.Left));
            int bottom = Math.Max(Size.Height, otherIslands.Max(x => x.BottomRight.Top));

            var shift = new Offset(Math.Abs(left), Math.Abs(top));
            var size = new Size(right - left, bottom - top);

            var mergedIslands = _islands
                .Concat(otherIslands)
                .Select(island => island.Move(shift.Left, shift.Top))
                .ToList();

            return new Universe(Guid.NewGuid(), mergedIslands, size);
        }

        public ICollection<IUniverse> Split(IUniverse immutableGrid)
        {
            var island = _islands.FirstOrDefault(island => island.ImmutableGrid.Identity == immutableGrid.Identity);

            if (island != null && _islands.Remove(island))
            {
                return new List<IUniverse>
                {
                    new Universe(Identity, _islands, Size),
                    new Universe(island.ImmutableGrid.Identity, new() { island }, island.ImmutableGrid.Size)
                };
            }

            return new List<IUniverse> { this };
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
            return new MutableGridAggregate(Identity, islands, Size);
        }

        #endregion

        #region Private Internal Types

        internal record ImmutableIsland(IImmutableGrid ImmutableGrid, Offset TopLeft) : 
            IMoveable<ImmutableIsland>, 
            IMutableConverter<MutableIsland>
        {
            public Offset BottomRight => new (TopLeft.Left + ImmutableGrid.Size.Width, TopLeft.Top + ImmutableGrid.Size.Height);

            public ImmutableIsland Move(int shiftLeft, int shiftTop)
            {
                var shiftedTopLeft = new Offset(TopLeft.Left + shiftLeft, TopLeft.Top + shiftTop);
                return new (ImmutableGrid, shiftedTopLeft);
            }

            public bool IsInBounds(int absoluteRow, int absoluteColumn)
            {
                return TopLeft.Left <= absoluteColumn
                    && TopLeft.Top <= absoluteRow
                    && absoluteColumn < BottomRight.Left
                    && absoluteRow < BottomRight.Top;
            }

            public MutableIsland ToMutable() => new (ImmutableGrid.ToMutable(), TopLeft);
        }

        internal record MutableIsland(IMutableGrid MutableGrid, Offset TopLeft) : 
            IImmutableConverter<ImmutableIsland>
        {
            public Offset BottomRight => new (TopLeft.Left + MutableGrid.Size.Width, TopLeft.Top + MutableGrid.Size.Height);

            public bool IsInBounds(int absoluteRow, int absoluteColumn)
            {
                return TopLeft.Left <= absoluteColumn
                    && TopLeft.Top <= absoluteRow
                    && absoluteColumn < BottomRight.Left
                    && absoluteRow < BottomRight.Top;
            }

            public ImmutableIsland ToImmutable() => new (MutableGrid.ToImmutable(), TopLeft);
        }

        internal class MutableGridAggregate : IMutableUniverse
        {
            private readonly List<MutableIsland> _islands = new List<MutableIsland>();

            public MutableGridAggregate(Guid identity, List<MutableIsland> islands, Size size)
            {
                _islands = islands ?? throw new ArgumentNullException(nameof(islands));
                Size = size ?? throw new ArgumentNullException(nameof(size));
                Identity = identity;
            }

            public Guid Identity { get; }

            public Size Size { get; }

            public void Set(int row, int column, Cell cell)
            {
                _islands
                    .Where(island => island.IsInBounds(row, column))
                    .ToList()
                    .ForEach(island => InternalSet(island, row, column, cell));
            }

            IUniverse IImmutableConverter<IUniverse>.ToImmutable() => ToImmutableGrid();

            IImmutableGrid IImmutableConverter<IImmutableGrid>.ToImmutable() => ToImmutableGrid();

            private Universe ToImmutableGrid()
            {
                var islands = _islands
                    .Select(island => island.ToImmutable())
                    .ToList();
                return new (Identity, islands, Size);
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
