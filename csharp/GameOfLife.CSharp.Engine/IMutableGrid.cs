using System;

namespace GameOfLife.CSharp.Engine
{
    public interface IMutableGrid : IImmutableConverter<IImmutableGrid>
    {
        /// <summary>
        /// Gets the unique identity of the grid instance.
        /// </summary>
        Guid Identity { get; }

        /// <summary>
        /// Gets the size of the world space.
        /// </summary>
        Size Size { get; }

        /// <summary>
        /// Sets the cell state by row and column numbers.
        /// </summary>
        /// <param name="row">Row number relative to self.</param>
        /// <param name="column">Column number relative to self.</param>
        /// <param name="cell">The cell state to set.</param>
        /// <returns>The modified instance of <see cref="IImmutableGrid"/>.</returns>
        void Set(int row, int column, Cell cell);
    }
}
