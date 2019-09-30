using System.Threading;
using System.Threading.Tasks;

namespace GameOfLife.Engine
{
    public class Time
    {
        private readonly CancellationTokenSource _cts;
        private Task<Time> _runningTask;

        private Time() => _cts = new CancellationTokenSource();

        public static Time None => new Time();

        public Task<Time> Start()
        {
            _runningTask = Flow(Generation.Zero(0, 0), _cts.Token);
            return _runningTask;
        }

        public Task<Time> Stop()
        {
            _cts.Cancel();
            return _runningTask;
        }

        private async Task<Time> Flow(Generation generation, CancellationToken cancellationToken)
        {
            await Task.Delay(1000);

            return cancellationToken.IsCancellationRequested
                ? (this)
                : await Flow(generation.Next(), cancellationToken);
        }
    }
}
