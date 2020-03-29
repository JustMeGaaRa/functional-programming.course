namespace GameOfLife.CSharp.Engine
{
    public interface IMutableUniverse : IMutableGrid, IImmutableConverter<IUniverse>
    {
    }
}
