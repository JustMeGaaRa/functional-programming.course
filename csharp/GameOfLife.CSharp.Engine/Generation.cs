namespace GameOfLife.CSharp.Engine
{
    public record Generation(IWorld World, uint Number)
    {
        public Cell this[int row, int column] => World[row, column];

        public Size Size => World.Size;

        public static Generation Zero(int width, int height) => new (Engine.World.FromSize(width, height), 0);

        public static Generation Zero(PopulationPattern pattern) => new (Engine.World.FromPattern(pattern), 0);

        public override string ToString() => $"Generation: {Number};";

        public Generation Next() => new (World.Evolve(), Number + 1);
    }
}
