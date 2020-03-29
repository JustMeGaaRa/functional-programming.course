namespace GameOfLife.CSharp.Engine
{
    public interface IImmutableConverter<out TResult>
    {
        /// <summary>
        /// Create an immutable instance of type <see cref="TResult"/>.
        /// </summary>
        /// <returns>An immutable instance of type <see cref="TResult"/>.</returns>
        TResult ToImmutable();
    }
}
