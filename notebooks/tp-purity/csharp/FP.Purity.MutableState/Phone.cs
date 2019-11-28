using System;
using System.Collections.Generic;

namespace FP.Purity.MutableState
{
    public class Phone
    {
        private readonly HashSet<string> _apps = new HashSet<string>();

        public IReadOnlyCollection<string> Apps => _apps;

        public Phone Install(string appName)
        {
            _apps.Add(appName);
            return this;
        }

        public Phone Remove(string appName)
        {
            _apps.Remove(appName);
            return this;
        }

        public Phone Run(string appName)
        {
            string app = _apps.Contains(appName) ? appName : "The app is not installed";
            Console.WriteLine(app);
            return this;
        }
    }
}
