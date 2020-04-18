using System;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;

namespace GameOfLife.CSharp.Engine
{
    public sealed class Time : IObservable<Generation>, IDisposable
    {
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();
        private readonly ISubject<Generation> _observable = new Subject<Generation>();
        private readonly Generation _zero;
        private IDisposable? _disposable;
        private Task<Generation>? _generationTask;

        public Time(Generation zero)
        {
            _zero = zero ?? throw new ArgumentNullException(nameof(zero));
        }

        public IDisposable Subscribe(IObserver<Generation> observer)
        {
            _disposable = _observable.Subscribe(observer);
            return _disposable;
        }

        public void Start()
        {
            const int defaultDelay = 1000;
            _generationTask = Task.Run(() => Flow(_observable, _zero, defaultDelay, _cts.Token), _cts.Token);
        }

        public void Dispose()
        {
            _disposable?.Dispose();
            _cts.Cancel();
            _cts.Dispose();
            _generationTask?.Wait();
        }

        private static async Task<Generation> Flow(
            ISubject<Generation> observable,
            Generation generation,
            int delay,
            CancellationToken token)
        {
            observable.OnNext(generation);

            if (token.IsCancellationRequested)
            {
                observable.OnCompleted();
                return generation;
            }

            await Task.Delay(delay);
            return await Flow(observable, generation.Next(), delay, token);
        }
    }
}
