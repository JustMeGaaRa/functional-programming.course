namespace GameOfLife.CSharp.Engine
{
    public interface IMutableConverter<out TResult>
    {
        /// <summary>
        /// Create a mutable instance of type <see cref="TResult"/>
        /// </summary>
        /// <returns>A mutable instance of type <see cref="TResult"/>.</returns>
        TResult ToMutable();
    }
}
