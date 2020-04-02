namespace GameOfLife.CSharp.Engine
{
    public interface IMoveable<TResult> where TResult : IMoveable<TResult>
    {
        /// <summary>
        /// Gets the coordinates of top left corner of the world space.
        /// </summary>
        Offset TopLeft { get; }

        /// <summary>
        /// Shifts current world to a new position in space.
        /// </summary>
        /// <param name="shiftLeft">Shift from left border (on the X axis).</param>
        /// <param name="shiftTop">Shift from top border (on the Y axis).</param>
        /// <returns>Instance of <see cref="TResult"/> implementation.</returns>
        TResult Move(int shiftLeft, int shiftTop);
    }
}
