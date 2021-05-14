using System.Collections.Generic;

namespace GameOfLife.CSharp.Engine
{
    public interface IMergeable<TResult> where TResult : IMergeable<TResult>, IImmutableGrid
    {
        /// <summary>
        /// Merges two universes together into a single one.
        /// </summary>
        /// <param name="universe">The universe to merge.</param>
        /// <param name="offset">The offset of the grid relative to others.</param>
        /// <returns>A modified instance of <see cref="TResult"/>.</returns>
        TResult Merge(TResult universe, Offset offset);

        /// <summary>
        /// Splits the universe into multiple ones by taking out one world.
        /// </summary>
        /// <param name="universe">The grid to get out of the universe.</param>
        /// <returns>A collection of resulting separate instances of <see cref="TResult"/>.</returns>
        ICollection<TResult> Split(TResult universe);
    }
}
