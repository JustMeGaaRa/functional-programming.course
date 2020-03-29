namespace GameOfLife.CSharp.Engine
{
    public static class CellExtensions
    {
        public static bool IsEmpty(this Cell cell) => cell?.Population == Population.None;

        public static bool IsAlive(this Cell cell) => cell?.Population == Population.Alive;

        public static bool IsDead(this Cell cell) => cell?.Population == Population.Dead;
    }
}
