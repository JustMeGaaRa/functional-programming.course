using System;
using System.Collections.Generic;
using System.Linq;

namespace GameOfLife.CSharp.Engine
{
    public sealed class Universe : IWorld
    {
        private readonly ICollection<IWorld> _worlds;

        private Universe(ICollection<IWorld> worlds, Offset topLeft, Offset bottomRight, Size size)
        {
            _worlds = worlds ?? throw new ArgumentNullException(nameof(worlds));
            TopLeft = topLeft ?? throw new ArgumentNullException(nameof(topLeft));
            BottomRight = bottomRight ?? throw new ArgumentNullException(nameof(bottomRight));
            Size = size ?? throw new ArgumentNullException(nameof(size));
        }

        public Cell this[int relativeRow, int relativeColumn]
        {
            get
            {
                var world = _worlds.FirstOrDefault(world => world.IsInBounds(relativeRow, relativeColumn));
                return world == null ? Cell.Empty : world[relativeRow, relativeColumn];
            }
        }

        public Offset TopLeft { get; }

        public Offset BottomRight { get; }

        public Size Size { get; }

        public static IWorld Merge(IWorld first, IWorld second)
        {
            if (first is null) throw new ArgumentNullException(nameof(first));
            if (second is null) throw new ArgumentNullException(nameof(second));

            var worlds = new[] { first, second };
            int left = Math.Min(first.TopLeft.Left, second.TopLeft.Left);
            int top = Math.Min(first.TopLeft.Left, second.TopLeft.Left);
            int right = Math.Max(first.BottomRight.Left, second.BottomRight.Left);
            int bottom = Math.Max(first.BottomRight.Top, second.BottomRight.Top);
            var topLeft = new Offset(left, top);
            var bottomRight = new Offset(right, bottom);
            var size = new Size(right - left, bottom - top);
            return new Universe(worlds, topLeft, bottomRight, size);
        }

        public IWorld Move(int shiftLeft, int shiftTop)
        {
            return new Universe(_worlds, TopLeft.Shift(shiftLeft, shiftTop), BottomRight.Shift(shiftLeft, shiftTop), Size);
        }

        public bool IsCellAliveBySelfOffset(int relativeRow, int relativeColumn)
        {
            return _worlds.Any(world => world.IsValidIndex(relativeRow, relativeColumn) && world.IsCellAliveBySelfOffset(relativeRow, relativeColumn));
        }

        public bool IsCellAliveByAbsoluteOffset(int absoluteRow, int absoluteColumn)
        {
            int relativeRow = absoluteRow - TopLeft.Top;
            int relativeColumn = absoluteColumn - TopLeft.Left;
            return _worlds.Any(world => world.IsInBounds(relativeRow, relativeColumn) && world.IsCellAliveByAbsoluteOffset(relativeRow, relativeColumn));
        }
    }
}
