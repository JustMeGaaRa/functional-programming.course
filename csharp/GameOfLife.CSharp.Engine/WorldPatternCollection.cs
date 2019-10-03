using System.Collections.Generic;

namespace GameOfLife.Engine
{
    public static class WorldPatternCollection
    {
        private const bool dead = false;
        private const bool live = true;

        /// <summary>
        /// A collection of predefined patterns: Blinker, Toad, Beacon, Pulsar
        /// </summary>
        public static ICollection<WorldPattern> AllPatterns => new[]
        {
            Blinker,
            Toad,
            Beacon,
            Pulsar
        };

        /// <summary>
        /// Blinker Pattern - Period 2
        /// </summary>
        public static WorldPattern Blinker => WorldPattern.FromArray2D(0, "Blinker", new[,]
        {
            { dead, live, dead },
            { dead, live, dead },
            { dead, live, dead }
        });

        /// <summary>
        /// Toad Pattern - Period 2
        /// </summary>
        public static WorldPattern Toad => WorldPattern.FromArray2D(0, "Toad", new[,]
        {
            { dead, dead, dead, dead },
            { dead, live, live, live },
            { live, live, live, dead },
            { dead, dead, dead, dead }
        });

        /// <summary>
        /// Beacon Pattern - Period 2
        /// </summary>
        public static WorldPattern Beacon => WorldPattern.FromArray2D(0, "Beacon", new[,]
        {
            { live, live, dead, dead },
            { live, live, dead, dead },
            { dead, dead, live, live },
            { dead, dead, live, live }
        });

        /// <summary>
        /// Pulsar Pattern - Period 3
        /// </summary>
        public static WorldPattern Pulsar => WorldPattern.FromArray2D(0, "Pulsar", new[,]
        {
            { dead, dead, dead, dead, dead, dead, dead, dead, dead, dead, dead, dead, dead, dead, dead },
            { dead, dead, dead, live, live, live, dead, dead, dead, live, live, live, dead, dead, dead },
            { dead, dead, dead, dead, dead, dead, dead, dead, dead, dead, dead, dead, dead, dead, dead },
            { dead, live, dead, dead, dead, dead, live, dead, live, dead, dead, dead, dead, live, dead },
            { dead, live, dead, dead, dead, dead, live, dead, live, dead, dead, dead, dead, live, dead },
            { dead, live, dead, dead, dead, dead, live, dead, live, dead, dead, dead, dead, live, dead },
            { dead, dead, dead, live, live, live, dead, dead, dead, live, live, live, dead, dead, dead },
            { dead, dead, dead, dead, dead, dead, dead, dead, dead, dead, dead, dead, dead, dead, dead },
            { dead, dead, dead, live, live, live, dead, dead, dead, live, live, live, dead, dead, dead },
            { dead, live, dead, dead, dead, dead, live, dead, live, dead, dead, dead, dead, live, dead },
            { dead, live, dead, dead, dead, dead, live, dead, live, dead, dead, dead, dead, live, dead },
            { dead, live, dead, dead, dead, dead, live, dead, live, dead, dead, dead, dead, live, dead },
            { dead, dead, dead, dead, dead, dead, dead, dead, dead, dead, dead, dead, dead, dead, dead },
            { dead, dead, dead, live, live, live, dead, dead, dead, live, live, live, dead, dead, dead },
            { dead, dead, dead, dead, dead, dead, dead, dead, dead, dead, dead, dead, dead, dead, dead },
        });
    }
}
