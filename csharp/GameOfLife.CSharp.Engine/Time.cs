using System;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;

namespace GameOfLife.CSharp.Engine
{
    public class Time : IObservable<Generation>, IDisposable
    {
        private readonly CancellationTokenSource _cts = new ();
        private readonly ISubject<Generation> _observable = new Subject<Generation>();
        private IDisposable? _disposable;
        private Task<Generation>? _generationTask;

        public IDisposable Subscribe(IObserver<Generation> observer)
        {
            _disposable = _observable.Subscribe(observer);
            return _disposable;
        }

        public void Dispose()
        {
            _disposable?.Dispose();
            _cts.Cancel();
            _cts.Dispose();
            _generationTask?.Wait();
        }

        public Generation Start(PopulationPattern pattern)
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

            await Task.Delay(1000, token);
            return await Flow(observable, generation.Next(), token);
        }
    }
}
