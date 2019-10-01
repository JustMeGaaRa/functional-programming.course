using System;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;

namespace GameOfLife.Engine
{
    public class Time
    {
        private readonly CancellationTokenSource _cts;
        private readonly ISubject<Generation> _observable;
        private Task<Generation> _generationTask;

        public Time()
        {
            _cts = new CancellationTokenSource();
            _observable = new Subject<Generation>();
        }

        public IObservable<Generation> Start(Population[,] pattern)
        {
            Generation generation0 = Generation.Zero(pattern);
            _generationTask = Task.Run(() => Flow(_observable, generation0, _cts.Token));
            return _observable;
        }

        public async Task<Generation> End()
        {
            _cts.Cancel();
            return await _generationTask;
        }

        private static async Task<Generation> Flow(ISubject<Generation> observable, Generation generation, CancellationToken token)
        {
            await Task.Delay(1000);

            if (token.IsCancellationRequested)
            {
                observable.OnCompleted();
                return generation;
            }

            var next = generation.Next();
            observable.OnNext(next);
            return await Flow(observable, next, token);
        }
    }
}
