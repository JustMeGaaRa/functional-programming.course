namespace GameOfLife.Engine
{
    public static class PopulationPatterns
    {
        private static Population dead = Population.Dead;
        private static Population live = Population.Alive;

        /// <summary>
        /// A collection of predefined patterns: Blinker, Toad, Beacon, Pulsar
        /// </summary>
        public static Population[][,] AllPatterns => new[]
        {
            Blinker,
            Toad,
            Beacon,
            Pulsar
        };

        /// <summary>
        /// Blinker Pattern - Period 2
        /// </summary>
        public static Population[,] Blinker => new[,]
        {
            { dead, live, dead },
            { dead, live, dead },
            { dead, live, dead }
        };

        /// <summary>
        /// Toad Pattern - Period 2
        /// </summary>
        public static Population[,] Toad => new[,]
        {
            { dead, dead, dead, dead },
            { dead, live, live, live },
            { live, live, live, dead },
            { dead, dead, dead, dead }
        };

        /// <summary>
        /// Beacon Pattern - Period 2
        /// </summary>
        public static Population[,] Beacon => new[,]
        {
            { live, live, dead, dead },
            { live, live, dead, dead },
            { dead, dead, live, live },
            { dead, dead, live, live }
        };

        /// <summary>
        /// Pulsar Pattern - Period 3
        /// </summary>
        public static Population[,] Pulsar => new[,]
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
        };
    }
}
