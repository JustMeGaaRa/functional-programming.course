namespace GameOfLife.Engine
{
    public static class PopulationPatterns
    {
        private static Population dead = Population.Dead;
        private static Population live = Population.Alive;

        /// <summary>
        /// A collection of predefined patterns: Blinker, Toad
        /// </summary>
        public static Population[][,] AllPatterns => new[]
        {
            Blinker,
            Toad
        };

        /// <summary>
        /// Blinker Pattern - Period 1
        /// </summary>
        public static Population[,] Blinker => new[,]
        {
            { dead, live, dead },
            { dead, live, dead },
            { dead, live, dead }
        };

        /// <summary>
        /// Toad Pattern - Period 1
        /// </summary>
        public static Population[,] Toad => new[,]
        {
            { dead, dead, dead, dead },
            { dead, live, live, live },
            { live, live, live, dead },
            { dead, dead, dead, dead }
        };
    }
}
