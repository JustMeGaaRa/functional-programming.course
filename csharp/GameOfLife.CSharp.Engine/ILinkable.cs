namespace GameOfLife.CSharp.Engine
{
    public interface ILinkable<TResult> where TResult : ILinkable<TResult>, IImmutableGrid
    {
        /// <summary>
        /// Merges two universes together into a single one.
        /// </summary>
        /// <param name="first">The first cell to add a link to.</param>
        /// <param name="second">The second cell to add a link to.</param>
        /// <returns>A modified instance of <see cref="TResult"/>.</returns>
        TResult Join(Cell first, Cell second);

        /// <summary>
        /// Splits the universe into multiple ones by taking out one world.
        /// </summary>
        /// <param name="first">The first cell to remove the link from.</param>
        /// <param name="second">The second cell to remove the link from.</param>
        /// <returns>A collection of resulting separate instances of <see cref="TResult"/>.</returns>
        TResult Split(Cell first, Cell second);
    }
}
