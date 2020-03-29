using System.Collections.Generic;

namespace GameOfLife.CSharp.Engine
{
    public interface IUniverse : IImmutableGrid, IMutableConverter<IMutableUniverse>, IEvolveable<IUniverse>, IMergeable<IUniverse>
    {
        /// <summary>
        /// Gets a collection of worlds that compose into a universe.
        /// </summary>
        ICollection<IImmutableGrid> Worlds { get; }
    }
}
