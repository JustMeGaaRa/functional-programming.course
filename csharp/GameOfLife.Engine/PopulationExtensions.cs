namespace GameOfLife.Engine
{
    public static class PopulationExtensions
    {
        public static bool IsAlive(this Population population) => population == Population.Alive;

        public static bool IsDead(this Population population) => population == Population.Dead;
    }
}
