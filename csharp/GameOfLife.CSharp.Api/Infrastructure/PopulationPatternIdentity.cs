using System.Threading;

namespace GameOfLife.Engine
{
    public class PopulationPatternIdentity
    {
        private static volatile int _identity = 0;

        public static int RestoreIdentity(int value) => Interlocked.Exchange(ref _identity, value);

        public static int GenerateIdentity() => Interlocked.Increment(ref _identity);
    }
}
