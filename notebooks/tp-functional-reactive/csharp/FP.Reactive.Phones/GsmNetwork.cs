using System.Collections.Generic;

namespace FP.Reactive.Phones
{
    public class GsmNetwork<TChannel> : IGsmNetwork where TChannel : IChannel, new()
    {
        private readonly Dictionary<(string, string), IChannel> _channels = new Dictionary<(string, string), IChannel>();

        public IChannel EstablishConnection(string from, string to)
        {
            IChannel channel = _channels.ContainsKey((to, from))
                ? _channels[(to, from)]
                : _channels[(from, to)] = new TChannel();
            return _channels.ContainsKey((from, to))
                ? _channels[(from, to)]
                : channel;
        }
    }
}
