namespace GameOfLife.CSharp.Engine
{
    public class Cell
    {
        private Cell(Population state) => Population = state;

        public Population Population { get; }

        public static Cell Empty => new Cell(Population.Empty);

        public static Cell Create(Population state) => new Cell(state);
    }
}
