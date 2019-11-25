using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace FP.Reactive.Phones
{
    public class ProactiveChannel : IChannel
    {
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();
        private readonly ConcurrentQueue<string> _messages = new ConcurrentQueue<string>();
        private Task _processingHandle;

        public ProactiveChannel()
        {
            Console.WriteLine();
            Console.WriteLine("Starting Proactive GSM Line...");
            Console.WriteLine("-------------------------------------");
        }

        public void Close()
        {
            _cts.Cancel();
            _processingHandle.Wait();
        }

        public IChannel Listen(Action<string> handle)
        {
            _processingHandle = Task.Run(() => ProcessMessages(handle));
            return this;
        }

        public IChannel Send(string message)
        {
            _messages.Enqueue(message);
            return this;
        }

        private void ProcessMessages(Action<string> handle)
        {
            while (!_cts.IsCancellationRequested || !_messages.IsEmpty)
            {
                string text = _messages.TryDequeue(out string message)
                    ? message
                    : "...";
                handle(text);
                Task.Delay(1000).Wait();
            }
        }
    }
}
