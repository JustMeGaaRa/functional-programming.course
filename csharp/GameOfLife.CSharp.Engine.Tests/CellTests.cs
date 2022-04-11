using GameOfLife.CSharp.Engine;
using Xunit;

namespace GameOfLife.CSharp.Engine.Tests
{
    public class CellTests
    {
        [Fact]
        public void Cell_Empty_ShouldNotBeNullAndShouldBeDead()
        {
            // Arrange
            Cell cell = Cell.Empty;

            // Act, Assert
            Assert.NotNull(cell);
            Assert.Equal(Population.Empty, cell.Population);
        }

        [Theory]
        [InlineData(Population.Empty)]
        [InlineData(Population.Alive)]
        [InlineData(Population.Dead)]
        public void Cell_CreateWithPopulationState_ShouldNotBeNullAndHaveSpecifiedState(Population population)
        {
            // Arrange
            Cell cell = Cell.Create(population);

            // Act, Assert
            Assert.NotNull(cell);
            Assert.Equal(population, cell.Population);
        }
    }
}
