using System.Collections.Generic;

namespace FP.Purity.SharedState
{
    public class Drawer
    {
        private readonly HashSet<string> _items = new HashSet<string>();

        public void Store(string item)
        {
            _items.Add(item);
        }

        public string Retreive(string name)
        {
            if (_items.TryGetValue(name, out string item))
            {
                _items.Remove(name);
            }

            return item;
        }
    }
}
