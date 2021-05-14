using System;

namespace GameOfLife.CSharp.Engine
{
    public interface IImmutableGrid
    {
        /// <summary>
        /// Gets the unique identity of the grid instance.
        /// </summary>
        Guid Identity { get; }

        /// <summary>
        /// Gets the cell at position relative to self.
        /// </summary>
        /// <param name="relativeRow">Row number relative to self.</param>
        /// <param name="relativeColumn">Column number relative to self.</param>
        /// <returns>The <see cref="Cell"/> instance.</returns>
        Cell this[int relativeRow, int relativeColumn] { get; }

        /// <summary>
        /// Gets the size of the world space.
        /// </summary>
        Size Size { get; }
    }
}
