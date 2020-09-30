namespace GameOfLife.CSharp.Engine
{
    public record Generation(IUniverse World, uint Number)
    {
        public Cell this[int row, int column] => World[row, column];

        public Size Size => World.Size;

        public static Generation Zero(PopulationPattern pattern) => new (Universe.FromPattern(pattern), 0);

        public static Generation Zero(IUniverse universe) => new Generation(universe, 0);

        public override string ToString() => $"Generation: {Number};";

        public Generation Next() => new (World.Evolve(), Number + 1);
    }
}
