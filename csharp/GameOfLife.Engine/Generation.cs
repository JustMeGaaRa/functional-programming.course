namespace GameOfLife.Engine
{
    public class Generation
    {
        private readonly World _world;

        private Generation(World world, uint number)
        {
            _world = world;
            Number = number;
        }

        public Cell this[int row, int column] => _world[row, column];

        public Size Size => _world.Size;

        public uint Number { get; }

        public static Generation Zero(int width, int height) => new Generation(World.FromSize(width, height), 0);

        public static Generation Zero(Population[,] states) => new Generation(World.FromPattern(states), 0);

        public Generation Next() => new Generation(_world.Evolve(), Number + 1);
    }
}
