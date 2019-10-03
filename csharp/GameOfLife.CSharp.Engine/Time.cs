using System;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;

namespace GameOfLife.Engine
{
    public class Time : IObservable<Generation>
    {
        private readonly CancellationTokenSource _cts;
        private readonly ISubject<Generation> _observable;
        private Task<Generation> _generationTask;

        public Time()
        {
            _cts = new CancellationTokenSource();
            _observable = new Subject<Generation>();
        }

        public IDisposable Subscribe(IObserver<Generation> observer)
        {
            return _observable.Subscribe(observer);
        }

        public Task<Generation> Start(WorldPattern pattern)
        {
            Generation generation0 = Generation.Zero(pattern);
            _generationTask = Task.Run(() => Flow(_observable, generation0, _cts.Token));
            return _generationTask;
        }

        public async Task<Generation> End()
        {
            _cts.Cancel();
            return await _generationTask;
        }

        private static async Task<Generation> Flow(ISubject<Generation> observable, Generation generation, CancellationToken token)
        {
            observable.OnNext(generation);

            if (token.IsCancellationRequested)
            {
                observable.OnCompleted();
                return generation;
            }

            await Task.Delay(1000);
            return await Flow(observable, generation.Next(), token);
        }
    }
}
