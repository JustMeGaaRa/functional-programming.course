using GameOfLife.Engine;
using System;
using Xunit;

namespace GameOfLife.CSharp.Engine.Tests
{
    public class ImmutableGridExtensionsTests
    {
        [Fact]
        public void IsValidIndex_WithNullWorld_ShouldThrowArgumentNullException()
        {
            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => ImmutableGridExtensions.IsValidIndex(null, 0, 0));
        }
        [Fact]
        public void FromPattern_FromBlinkerPeriod1Pattern_ReturnsFromBlinkerPeriod1()
        {
            // Arrange
            Size expectedSize = new Size(3, 3);

            // Act
            IImmutableGrid grid = ImmutableGrid.FromPattern(PopulationPatterns.Blinker);

            // Assert
            Assert.NotNull(grid);
            Assert.Equal(expectedSize, grid.Size);
            Assert.Equal(Population.Dead, grid[0, 0].Population);
            Assert.Equal(Population.Dead, grid[1, 0].Population);
            Assert.Equal(Population.Dead, grid[2, 0].Population);
            Assert.Equal(Population.Alive, grid[0, 1].Population);
            Assert.Equal(Population.Alive, grid[1, 1].Population);
            Assert.Equal(Population.Alive, grid[2, 1].Population);
            Assert.Equal(Population.Dead, grid[0, 2].Population);
            Assert.Equal(Population.Dead, grid[1, 2].Population);
            Assert.Equal(Population.Dead, grid[2, 2].Population);
        }

        /// <summary>
        /// Expected pattern for Blinker Period 2
        /// |   |   |   |
        /// -------------
        /// | + | + | + |
        /// -------------
        /// |   |   |   |
        /// </summary>
        [Fact]
        public void Evolve_FromBlinkerPeriod1Pattern_ReturnsWorldInBlinkerPeriod2()
        {
            // Arrange
            Size expectedSize = new Size(3, 3);
            IMutableGrid mutableGrid = MutableGrid.FromSize(3, 3);

            // Act
            IImmutableGrid grid = ImmutableGrid.FromPattern(PopulationPatterns.Blinker);
            grid.Evolve(mutableGrid);
            IImmutableGrid evolved = mutableGrid.ToImmutable();

            // Assert
            Assert.NotNull(grid);
            Assert.NotNull(mutableGrid);
            Assert.NotEqual(grid, evolved);
            Assert.Equal(expectedSize, mutableGrid.Size);
            Assert.Equal(Population.Dead, evolved[0, 0].Population);
            Assert.Equal(Population.Dead, evolved[0, 1].Population);
            Assert.Equal(Population.Dead, evolved[0, 2].Population);
            Assert.Equal(Population.Alive, evolved[1, 0].Population);
            Assert.Equal(Population.Alive, evolved[1, 1].Population);
            Assert.Equal(Population.Alive, evolved[1, 2].Population);
            Assert.Equal(Population.Dead, evolved[2, 0].Population);
            Assert.Equal(Population.Dead, evolved[2, 1].Population);
            Assert.Equal(Population.Dead, evolved[2, 2].Population);
        }

        /// <summary>
        /// Expected pattern for Toad Period 2
        /// |   |   | + |   |
        /// -----------------
        /// | + |   |   | + |
        /// -----------------
        /// | + |   |   | + |
        /// -----------------
        /// |   | + |   |   |
        /// </summary>
        [Fact]
        public void Evolve_FromToadPeriod1Pattern_ReturnsWorldInToadPeriod2()
        {
            // Arrange
            Size expectedSize = new Size(4, 4);
            IMutableGrid mutableGrid = MutableGrid.FromSize(4, 4);

            // Act
            IImmutableGrid grid = ImmutableGrid.FromPattern(PopulationPatterns.Toad);
            grid.Evolve(mutableGrid);
            IImmutableGrid evolved = mutableGrid.ToImmutable();

            // Assert
            Assert.NotNull(grid);
            Assert.NotNull(evolved);
            Assert.NotEqual(grid, evolved);
            Assert.Equal(expectedSize, evolved.Size);
            Assert.Equal(Population.Dead, evolved[0, 0].Population);
            Assert.Equal(Population.Dead, evolved[0, 1].Population);
            Assert.Equal(Population.Alive, evolved[0, 2].Population);
            Assert.Equal(Population.Dead, evolved[0, 3].Population);
            Assert.Equal(Population.Alive, evolved[1, 0].Population);
            Assert.Equal(Population.Dead, evolved[1, 1].Population);
            Assert.Equal(Population.Dead, evolved[1, 2].Population);
            Assert.Equal(Population.Alive, evolved[1, 3].Population);
            Assert.Equal(Population.Alive, evolved[2, 0].Population);
            Assert.Equal(Population.Dead, evolved[2, 1].Population);
            Assert.Equal(Population.Dead, evolved[2, 2].Population);
            Assert.Equal(Population.Alive, evolved[2, 3].Population);
            Assert.Equal(Population.Dead, evolved[3, 0].Population);
            Assert.Equal(Population.Alive, evolved[3, 1].Population);
            Assert.Equal(Population.Dead, evolved[3, 2].Population);
            Assert.Equal(Population.Dead, evolved[3, 3].Population);
        }
    }
}
