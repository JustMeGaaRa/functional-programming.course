namespace GameOfLife.CSharp.Engine
{
    public interface IEvolveable<TResult> where TResult : IEvolveable<TResult>, IImmutableGrid
    {
        /// <summary>
        /// Brings the universe onto the next stage of evolution.
        /// </summary>
        /// <returns>A modified instance of <see cref="IUniverse"/>.</returns>
        TResult Evolve();
    }
}
