namespace GameOfLife.CSharp.Engine
{
    /// <summary>
    /// Represents the world concept with dimentions and position is space.
    /// </summary>
    public interface IWorld
    {
        /// <summary>
        /// Gets the cell at position relative to self.
        /// </summary>
        /// <param name="relativeRow">Row number relative to self.</param>
        /// <param name="relativeColumn">Column number relative to self.</param>
        /// <returns>The <see cref="Cell"/> instance.</returns>
        Cell this[int relativeRow, int relativeColumn] { get; }

        /// <summary>
        /// Gets the coordinates of top left corner of the world space.
        /// </summary>
        Offset TopLeft { get; }

        /// <summary>
        /// Gets the coordinates of the bottom right corner of the world space.
        /// </summary>
        Offset BottomRight { get; }

        /// <summary>
        /// Gets the size of the world space.
        /// </summary>
        Size Size { get; }

        /// <summary>
        /// Shifts current world to a new position in space.
        /// </summary>
        /// <param name="shiftLeft">Shift from left border.</param>
        /// <param name="shiftTop">Shift from top border.</param>
        /// <returns>Instance of <see cref="IWorld"/> implementation.</returns>
        IWorld Move(int shiftLeft, int shiftTop);

        /// <summary>
        /// Gets if cell at position relative to self is in <see cref="Population.Alive"/> state.
        /// </summary>
        /// <param name="relativeRow">Row number relative to self.</param>
        /// <param name="relativeColumn">Column number relative to self.</param>
        /// <returns>The cell state.</returns>
        bool IsCellAliveBySelfOffset(int relativeRow, int relativeColumn);

        /// <summary>
        /// Gets if cell at position absolute to outer world is in <see cref="Population.Alive"/> state.
        /// </summary>
        /// <param name="absoluteRow">Row number relative to outer world (absolute position).</param>
        /// <param name="absoluteColumn">Column number relative to outer world (absolute position).</param>
        /// <returns>The cell state.</returns>
        bool IsCellAliveByAbsoluteOffset(int absoluteRow, int absoluteColumn);
    }
}
