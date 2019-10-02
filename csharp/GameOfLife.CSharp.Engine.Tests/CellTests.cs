using Xunit;

namespace GameOfLife.Engine.Tests
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
            Assert.Equal(Population.Dead, cell.Population);
        }

        [Theory]
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
