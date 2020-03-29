namespace GameOfLife.CSharp.Engine
{
    public interface IImmutableGrid : IMutableConverter<IMutableGrid>
    {
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

        /// <summary>
        /// Gets the cell state by row and column numbers.
        /// </summary>
        /// <param name="row">Row number relative to self.</param>
        /// <param name="column">Column number relative to self.</param>
        /// <returns>The modified instance of <see cref="IImmutableGrid"/>.</returns>
        Cell Get(int row, int column);
    }
}
