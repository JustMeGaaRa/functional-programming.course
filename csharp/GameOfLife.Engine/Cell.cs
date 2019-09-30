namespace GameOfLife.Engine
{
    public class Cell
    {
        private Cell(Population state) => Population = state;

        public Population Population { get; }

        public static Cell Empty => new Cell(Population.Dead);

        public static Cell Create(Population state) => new Cell(state);

        public Cell ChangeState(Population state) => new Cell(state);
    }
}
