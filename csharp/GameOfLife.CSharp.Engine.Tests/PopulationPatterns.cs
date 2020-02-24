using GameOfLife.CSharp.Engine;
using System.Collections.Generic;

namespace GameOfLife.Engine
{
    public static class PopulationPatterns
    {
        private const bool dead = false;
        private const bool live = true;

        /// <summary>
        /// A collection of predefined patterns: Blinker, Toad, Beacon, Pulsar
        /// </summary>
        public static ICollection<PopulationPattern> AllPatterns => new[]
        {
            Blinker,
            Toad,
            Beacon,
            Pulsar
        };

        /// <summary>
        /// Blinker Pattern - Period 2
        /// </summary>
        public static PopulationPattern Blinker => PopulationPattern.FromArray2D(1, "Blinker", new[,]
        {
            { dead, live, dead },
            { dead, live, dead },
            { dead, live, dead }
        });

        /// <summary>
        /// Toad Pattern - Period 2
        /// </summary>
        public static PopulationPattern Toad => PopulationPattern.FromArray2D(2, "Toad", new[,]
        {
            { dead, dead, dead, dead },
            { dead, live, live, live },
            { live, live, live, dead },
            { dead, dead, dead, dead }
        });

        /// <summary>
        /// Beacon Pattern - Period 2
        /// </summary>
        public static PopulationPattern Beacon => PopulationPattern.FromArray2D(3, "Beacon", new[,]
        {
            { live, live, dead, dead },
            { live, live, dead, dead },
            { dead, dead, live, live },
            { dead, dead, live, live }
        });

        /// <summary>
        /// Pulsar Pattern - Period 3
        /// </summary>
        public static PopulationPattern Pulsar => PopulationPattern.FromArray2D(4, "Pulsar", new[,]
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
