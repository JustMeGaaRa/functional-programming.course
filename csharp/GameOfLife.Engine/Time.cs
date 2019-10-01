using System;
using System.Collections.Generic;
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
            _observable = new DefaultSubject<Generation>();
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

        private interface ISubject<T> : IObservable<T>, IObserver<T>
        {
        }

        private class DefaultSubject<T> : ISubject<T>
        {
            private readonly List<IObserver<T>> _observers = new List<IObserver<T>>();

            public void OnCompleted() => _observers.ForEach(x => x.OnCompleted());

            public void OnError(Exception error) => _observers.ForEach(x => x.OnError(error));

            public void OnNext(T value) => _observers.ForEach(x => x.OnNext(value));

            public IDisposable Subscribe(IObserver<T> observer)
            {
                _observers.Add(observer);
                return new SubscriptionDisposable(_observers, observer);
            }

            private class SubscriptionDisposable : IDisposable
            {
                private readonly ICollection<IObserver<T>> _observers;
                private readonly IObserver<T> _subscriber;

                public SubscriptionDisposable(ICollection<IObserver<T>> observers, IObserver<T> subscriber)
                {
                    _observers = observers;
                    _subscriber = subscriber;
                }

                public void Dispose() => _observers.Remove(_subscriber);
            }
        }
    }
}
