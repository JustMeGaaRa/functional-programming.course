using GameOfLife.CSharp.Engine;
using System;
using Xunit;

namespace GameOfLife.Engine.Tests
{
    public class UniverseTests
    {
        [Fact]
        public void World_Empty_ShouldCreateAnInstanceWithZeroWidthAndHeight()
        {
            // Arrange
            IUniverse world = Universe.Empty;

            // Act, Assert
            Assert.NotNull(world);
            Assert.Equal(Size.None, world.Size);
        }

        [Fact]
        public void FromState_WithNullParameters_ShouldThrowArgumentNullException()
        {
            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => Universe.FromPattern(null));
        }

        [Fact]
        public void FromPattern_FromBlinkerPeriod1Pattern_ReturnsFromBlinkerPeriod1()
        {
            // Arrange
            Size expectedSize = new Size(3, 4);
            bool dead = false;
            bool live = true;
            PopulationPattern pattern = PopulationPattern.FromArray2D(1, "Test", new[,]
            {
                { dead, live, dead },
                { dead, live, dead },
                { dead, live, dead },
                { dead, live, dead }
            });

            // Act
            IUniverse world = Universe.FromPattern(pattern);

            // Assert
            Assert.NotNull(world);
            Assert.Equal(expectedSize, world.Size);
            Assert.Equal(Population.Dead, world[0, 0].Population);
            Assert.Equal(Population.Dead, world[1, 0].Population);
            Assert.Equal(Population.Dead, world[2, 0].Population);
            Assert.Equal(Population.Dead, world[3, 0].Population);
            Assert.Equal(Population.Alive, world[0, 1].Population);
            Assert.Equal(Population.Alive, world[1, 1].Population);
            Assert.Equal(Population.Alive, world[2, 1].Population);
            Assert.Equal(Population.Alive, world[3, 1].Population);
            Assert.Equal(Population.Dead, world[0, 2].Population);
            Assert.Equal(Population.Dead, world[1, 2].Population);
            Assert.Equal(Population.Dead, world[2, 2].Population);
            Assert.Equal(Population.Dead, world[3, 0].Population);
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
            IUniverse world = Universe.FromPattern(PopulationPatterns.Blinker);
            IUniverse evolved = world.Evolve();

            // Assert
            Assert.NotNull(world);
            Assert.NotNull(evolved);
            Assert.NotEqual(world, evolved);
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
            IUniverse world = Universe.FromPattern(PopulationPatterns.Toad);
            IUniverse evolved = world.Evolve();

            // Assert
            Assert.NotNull(world);
            Assert.NotNull(evolved);
            Assert.NotEqual(world, evolved);
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
