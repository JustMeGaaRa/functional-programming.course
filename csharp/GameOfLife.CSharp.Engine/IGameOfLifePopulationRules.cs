namespace GameOfLife.CSharp.Engine.Extensions
{
    public interface IGameOfLifePopulationRules
    {
        Population GetNextPopulationState(Cell cell, int aliveNeighbours);
    }
}
