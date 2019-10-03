using System;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;

namespace GameOfLife.Engine
{
    public class Time : IObservable<Generation>, IDisposable
    {
        private readonly CancellationTokenSource _cts;
        private readonly ISubject<Generation> _observable;
        private IDisposable _disposable;
        private Task<Generation> _generationTask;

        public Time()
        {
            _cts = new CancellationTokenSource();
            _observable = new Subject<Generation>();
        }

        public IDisposable Subscribe(IObserver<Generation> observer)
        {
            _disposable = _observable.Subscribe(observer);
            return _disposable;
        }

        public void Dispose()
        {
            _disposable.Dispose();
            _cts.Cancel();
            _generationTask.Wait();
        }

        public Generation Start(WorldPattern pattern)
        {
            var generation = Generation.Zero(pattern);
            _generationTask = Task.Run(() => Flow(_observable, generation, _cts.Token));
            return generation;
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
