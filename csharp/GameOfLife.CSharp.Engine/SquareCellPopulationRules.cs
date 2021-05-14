namespace GameOfLife.CSharp.Engine.Extensions
{
    public class SquareCellPopulationRules : IGameOfLifePopulationRules
    {
        public Population GetNextPopulationState(Cell cell, int aliveNeighbours)
        {
            return (cell, aliveNeighbours) switch
            {
                // Any empty cell with should remain empty.
                ({ Population: Population.None }, _) => Population.None,

                // Any live cell with fewer than two live neighbours dies, as if by underpopulation.
                ({ Population: Population.Alive }, < 2) => Population.Dead,

                // Any live cell with two or three live neighbours lives on to the next generation.
                ({ Population: Population.Alive }, < 4) => Population.Alive,

                // Any live cell with more than three live neighbours dies, as if by overpopulation.
                ({ Population: Population.Alive }, > 3) => Population.Dead,

                // Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.
                ({ Population: Population.Dead }, 3) => Population.Alive,

                // Any other case it remains dead.
                _ => Population.Dead
            };
        }
    }
}
