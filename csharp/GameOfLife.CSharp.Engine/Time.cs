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

        public Task<Generation> StartAsync()
        {
            const int defaultDelay = 1000;
            _generationTask = Task.Run(() => Flow(_observable, _zero, defaultDelay, _cts.Token), _cts.Token);
            return Task.FromResult(_zero);
        }

        public Task<Generation> StopAsync()
        {
            InternalCancel();
            return _generationTask;
        }

        public void Dispose()
        {
            InternalCancel();
        }

        private void InternalCancel()
        {
            _disposable?.Dispose();
            _cts.Cancel();
            _cts.Dispose();
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
