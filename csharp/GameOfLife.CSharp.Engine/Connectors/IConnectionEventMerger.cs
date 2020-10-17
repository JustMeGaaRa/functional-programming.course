namespace GameOfLife.CSharp.Engine.Connectors
{
    public interface IConnectionEvent<TCell>
    {
        TCell Cell { get; }
    }

    public record ConnectorActivated<TCell>(TCell Cell) : IConnectionEvent<TCell>;

    public record ConnectorDeactivated<TCell>(TCell Cell) : IConnectionEvent<TCell>;

    public record MergeEvent<TCell>(TCell SourceCell, TCell TargetCell);

    public interface IConnectionEventMerger<TCell>
    {
        MergeEvent<TCell>? Enqueue(IConnectionEvent<TCell> connectionEvent);
    }
}
