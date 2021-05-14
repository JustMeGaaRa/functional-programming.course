using System.Collections.Generic;

namespace GameOfLife.CSharp.Engine
{
    public interface IUniverse : IImmutableGrid, IEvolveable<IUniverse>, ILinkable<IUniverse>
    {
        /// <summary>
        /// Gets a collection of worlds that compose into a universe.
        /// </summary>
        ICollection<IImmutableGrid> Worlds { get; }

        /// <summary>
        /// Gets a collection of cells that the universe consists of.
        /// </summary>
        IReadOnlyCollection<Cell> Cells { get; }
    }
}
