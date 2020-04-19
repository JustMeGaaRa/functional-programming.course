namespace GameOfLife.CSharp.Engine
{
    public class Generation
    {
        private Generation(IUniverse world, uint number)
        {
            World = world ?? throw new System.ArgumentNullException(nameof(world));
            Number = number;
        }

        public static Generation Zero(PopulationPattern pattern) => new Generation(Universe.FromPattern(pattern), 0);

        public static Generation Zero(IUniverse universe) => new Generation(universe, 0);

        public Cell this[int row, int column] => World[row, column];

        public Size Size => World.Size;

        public uint Number { get; }

        public IUniverse World { get; }

        public Generation Next() => new Generation(World.Evolve(), Number + 1);

        public override string ToString() => $"Generation: {Number};";
    }
}
