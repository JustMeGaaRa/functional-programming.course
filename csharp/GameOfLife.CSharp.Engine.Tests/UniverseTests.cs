using GameOfLife.CSharp.Engine;
using System;
using Xunit;

namespace GameOfLife.Engine.Tests
{
    public class UniverseTests
    {
        [Fact]
        public void Universe_Empty_ShouldCreateAnInstanceWithZeroWidthAndHeight()
        {
            // Arrange
            IUniverse universe = Universe.Empty;

            // Act, Assert
            Assert.NotNull(universe);
            Assert.Equal(Size.None, universe.Size);
        }

        [Fact]
        public void FromState_WithNullParameters_ShouldThrowArgumentNullException()
        {
            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => Universe.FromPattern(null));
        }

        [Fact]
        public void Universe_EmptyPrototype_ShouldReturnInstanceWithSizeNone()
        {
            // Arrange, Act
            IUniverse universe = Universe.Empty;

            // Assert
            Assert.NotNull(universe);
            Assert.Equal(Size.None, universe.Size);
        }

        [Fact]
        public void FromPattern_FromBlinkerPeriod1Pattern_ReturnsFromBlinkerPeriod1()
        {
            // Arrange
            Size expectedSize = new Size(3, 3);

            // Act
            IUniverse universe = Universe.FromPattern(PopulationPatterns.Blinker);

            // Assert
            Assert.NotNull(universe);
            Assert.Equal(expectedSize, universe.Size);
            Assert.Equal(Population.Dead, universe[0, 0].Population);
            Assert.Equal(Population.Dead, universe[1, 0].Population);
            Assert.Equal(Population.Dead, universe[2, 0].Population);
            Assert.Equal(Population.Alive, universe[0, 1].Population);
            Assert.Equal(Population.Alive, universe[1, 1].Population);
            Assert.Equal(Population.Alive, universe[2, 1].Population);
            Assert.Equal(Population.Dead, universe[0, 2].Population);
            Assert.Equal(Population.Dead, universe[1, 2].Population);
            Assert.Equal(Population.Dead, universe[2, 2].Population);
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

            // Act
            IUniverse universe = Universe.FromPattern(PopulationPatterns.Blinker);
            IUniverse evolved = universe.Evolve();

            // Assert
            Assert.NotNull(universe);
            Assert.NotNull(evolved);
            Assert.NotEqual(universe, evolved);
            Assert.Equal(expectedSize, evolved.Size);
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

            // Act
            IUniverse universe = Universe.FromPattern(PopulationPatterns.Toad);
            IUniverse evolved = universe.Evolve();

            // Assert
            Assert.NotNull(universe);
            Assert.NotNull(evolved);
            Assert.NotEqual(universe, evolved);
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
