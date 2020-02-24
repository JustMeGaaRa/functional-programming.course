namespace GameOfLife.CSharp.Engine
{
    public class Generation
    {
        private readonly IWorld _world;

        private Generation(IWorld world, uint number)
        {
            _world = world;
            Number = number;
        }

        public Cell this[int row, int column] => _world[row, column];

        public Size Size => _world.Size;

        public uint Number { get; }

        public static Generation Zero(int width, int height) => new Generation(World.FromSize(width, height), 0);

        public static Generation Zero(PopulationPattern pattern) => new Generation(World.FromPattern(pattern), 0);

        public override string ToString() => $"Generation: {Number};";

        public Generation Next() => new Generation(_world.Evolve(), Number + 1);
    }
}
