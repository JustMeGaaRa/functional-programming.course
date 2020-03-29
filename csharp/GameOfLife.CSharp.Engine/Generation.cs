namespace GameOfLife.CSharp.Engine
{
    public class Generation
    {
        private readonly IUniverse _world;

        private Generation(IUniverse world, uint number)
        {
            _world = world;
            Number = number;
        }

        public Cell this[int row, int column] => _world[row, column];

        public Size Size => _world.Size;

        public uint Number { get; }

        public static Generation Zero(PopulationPattern pattern) => new Generation(Universe.FromPattern(pattern), 0);

        public override string ToString() => $"Generation: {Number};";

        public Generation Next() => new Generation(_world.Evolve(), Number + 1);
    }
}
