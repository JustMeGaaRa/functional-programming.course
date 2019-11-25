using System;
using System.Reactive.Subjects;

namespace FP.Reactive.Phones
{
    public class ReactiveChannel : IChannel
    {
        private readonly ISubject<string> _subject = new Subject<string>();
        private IDisposable _disposable;

        public ReactiveChannel()
        {
            Console.WriteLine();
            Console.WriteLine("Starting Reactive GSM Line...");
            Console.WriteLine("-------------------------------------");
        }

        public void Close()
        {
            _disposable.Dispose();
            _subject.OnCompleted();
        }

        public IChannel Listen(Action<string> handle)
        {
            _disposable = _subject.Subscribe(handle);
            return this;
        }

        public IChannel Send(string message)
        {
            _subject.OnNext(message);
            return this;
        }
    }
}
