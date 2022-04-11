using GameOfLife.CSharp.Engine;
using System;
using Xunit;

namespace GameOfLife.CSharp.Engine.Tests
{
    public class WorldTests
    {
        [Fact]
        public void World_Empty_ShouldCreateAnInstanceWithZeroWidthAndHeight()
        {
            // Arrange
            World world = World.Empty;

            // Act, Assert
            Assert.NotNull(world);
            Assert.Equal(Size.None, world.Size);
        }

        [Fact]
        public void FromSize_Width10AndHeight10_ShouldReturnInstanceOfRespectiveSize()
        {
            // Arrange, Act
            Size expectedSize = new Size(10, 10);
            World world = World.FromSize(10, 10);

            // Assert
            Assert.NotNull(world);
            Assert.Equal(expectedSize, world.Size);
        }

        [Fact]
        public void FromState_WithNullParameters_ShouldThrowArgumentNullException()
        {
            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => World.FromState(null, null, null));
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
            World world = World.FromPattern(pattern);

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

        [Theory]
        [InlineData(3, 7)]
        [InlineData(5, 0)]
        [InlineData(-3, -7)]
        public void Move_ShiftByLeft2Top4_ShouldChangeTopLeftRespectively(int left, int top)
        {
            // Arrange
            IWorld world = World.FromSize(10, 10);

            // Act
            IWorld actual = world.Move(left, top);

            // Assert
            Assert.Equal(left, actual.TopLeft.Left);
            Assert.Equal(top, actual.TopLeft.Top);
        }

        [Theory]
        [InlineData(3, 7, 4, 4, true)]
        [InlineData(3, 7, 10, 10, false)]
        [InlineData(3, 7, 7, 9, false)]
        [InlineData(5, 0, 4, 4, true)]
        [InlineData(5, 0, 10, 10, false)]
        [InlineData(5, 0, 9, 4, false)]
        public void IsCellAliveBySelfOffset_WithSize5By5_ShouldIgnoreTopLeft(int left, int top, int column, int row, bool expected)
        {
            // Arrange
            bool dead = false;
            bool live = true;
            PopulationPattern pattern = PopulationPattern.FromArray2D(1, "Test", new[,]
            {
                { dead, dead, dead, dead, dead },
                { dead, dead, dead, dead, dead },
                { dead, dead, dead, dead, dead },
                { dead, dead, dead, dead, dead },
                { dead, dead, dead, dead, live }
            });
            IWorld world = World.FromPattern(pattern);

            // Act
            bool actual = world.Move(left, top).IsCellAliveBySelfOffset(row, column);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(3, 7, 4, 4, false)]
        [InlineData(3, 7, 10, 10, false)]
        [InlineData(3, 7, 7, 9, true)]
        [InlineData(5, 0, 4, 4, false)]
        [InlineData(5, 0, 10, 10, false)]
        [InlineData(5, 0, 9, 4, false)]
        public void IsCellAliveByAbsoluteOffset_WithSize5By5_ShouldIncludeTopLeft(int left, int top, int column, int row, bool expected)
        {
            // Arrange
            bool dead = false;
            bool live = true;
            PopulationPattern pattern = PopulationPattern.FromArray2D(1, "Test", new[,]
            {
                { dead, dead, dead, dead, dead },
                { dead, dead, dead, dead, dead },
                { dead, dead, dead, dead, live },
                { dead, dead, dead, dead, dead },
                { dead, dead, dead, dead, dead }
            });
            IWorld world = World.FromPattern(pattern);

            // Act
            bool actual = world.Move(left, top).IsCellAliveByAbsoluteOffset(row, column);

            // Assert
            Assert.Equal(expected, actual);
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
            IWorld world = World.FromPattern(PopulationPatterns.Blinker);
            IWorld evolved = world.Evolve();

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
            IWorld world = World.FromPattern(PopulationPatterns.Toad);
            IWorld evolved = world.Evolve();

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
