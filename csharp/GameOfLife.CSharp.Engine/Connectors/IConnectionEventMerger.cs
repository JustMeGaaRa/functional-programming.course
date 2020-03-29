using System;

namespace GameOfLife.CSharp.Engine.Connectors
{
    public interface IConnectionEvent
    {
        object Cell { get; }
    }

    public sealed class ConnectorActivated : IConnectionEvent
    {
        public ConnectorActivated(object cell)
        {
            Cell = cell ?? throw new ArgumentNullException(nameof(cell));
        }

        public object Cell { get; }
    }

    public sealed class ConnectorDeactivated : IConnectionEvent
    {
        public ConnectorDeactivated(object cell)
        {
            Cell = cell ?? throw new ArgumentNullException(nameof(cell));
        }

        public object Cell { get; }
    }

    public class MergeEvent
    {
        public object SourceCell { get; set; }

        public object TargetCell { get; set; }
    }

    public interface IConnectionEventMerger
    {
        MergeEvent? Enqueue(IConnectionEvent connectionEvent);
    }
}
