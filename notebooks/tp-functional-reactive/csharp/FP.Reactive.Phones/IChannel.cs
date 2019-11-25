using System;

namespace FP.Reactive.Phones
{
    public interface IChannel
    {
        IChannel Send(string message);

        IChannel Listen(Action<string> handle);

        void Close();
    }
}
