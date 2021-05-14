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

    public class ConnectionEventMerger : IConnectionEventMerger<Cell>
    {
        private readonly IUniverse _universe;

        public ConnectionEventMerger(IUniverse universe)
        {
            _universe = universe ?? throw new System.ArgumentNullException(nameof(universe));
        }

        public MergeEvent<Cell>? Enqueue(IConnectionEvent<Cell> connectionEvent)
        {
            throw new System.NotImplementedException();
        }
    }
}
