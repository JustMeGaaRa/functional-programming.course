namespace GameOfLife.CSharp.Engine
{
    public record Cell(Population Population)
    {
        public static Cell Empty => new (Population.Empty);

        public static Cell Create(Population state) => new (state);
    }
}
