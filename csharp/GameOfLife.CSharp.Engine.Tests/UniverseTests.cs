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

        [Fact]
        public void Join_WithNullParameters_ShouldThrowArgumentNullException()
        {
            // Arrange
            IUniverse universe = Universe.Empty;

            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => universe.Join(null, null));
        }

        [Fact]
        public void Join_WithImmutableGridOnEmpty_ShouldHaveSingleWorldAndSize5By5()
        {
            // Arrange
            Size expectedSize = new Size(5, 5);
            IUniverse universe = Universe.Empty;
            IUniverse other = Universe.FromPattern(PopulationPattern.FromSize(0, "Empty", 5, 5));

            // Act
            IUniverse modified = universe.Join(other, Offset.None);

            // Assert
            Assert.NotNull(modified);
            Assert.Equal(expectedSize, modified.Size);
            Assert.NotEmpty(modified.Worlds);
            Assert.Single(modified.Worlds);
        }

        [Theory]
        [InlineData(3, 1, 5, 5)]
        [InlineData(-2, 1, 5, 5)]
        [InlineData(3, -1, 5, 4)]
        [InlineData(2, 3, 4, 7)]
        public void Join_SecondImmutableGridWithOffset_ShouldHaveTwoWorldsAndSize5By5(int left, int top, int expectedWidth, int expectedHeight)
        {
            // Arrange
            Size expectedSize = new Size(expectedWidth, expectedHeight);
            int expectedWorldCount = 2;
            Offset firstOffset = Offset.None;
            Offset secondOffset = new Offset(left, top);
            IUniverse firstUniverse = Universe.FromPattern(PopulationPattern.FromSize(1, "First", 3, 3));
            IUniverse secondUniverse = Universe.FromPattern(PopulationPattern.FromSize(2, "Second", 2, 4));

            // Act
            IUniverse modified = Universe.Empty
                .Join(firstUniverse, firstOffset)
                .Join(secondUniverse, secondOffset);

            // Assert
            Assert.NotNull(modified);
            Assert.Equal(expectedSize, modified.Size);
            Assert.Equal(expectedWorldCount, modified.Worlds.Count);
        }

        [Fact]
        public void Evolve_WithTwoWorlds_ShouldEvolveAsIfSingleWorld()
        {
            // Arrange
            Size expectedSize = new Size(7, 5);
            int expectedWorldCount = 2;
            Offset firstOffset = Offset.None;
            Offset secondOffset = new Offset(3, 1);
            IUniverse firstUniverse = Universe.FromPattern(PopulationPatterns.Blinker);
            IUniverse secondUniverse = Universe.FromPattern(PopulationPatterns.Toad);

            // Act
            IUniverse modified = Universe.Empty
                .Join(firstUniverse, firstOffset)
                .Join(secondUniverse, secondOffset)
                .Evolve();

            // Assert
            Assert.NotNull(modified);
            Assert.Equal(expectedSize, modified.Size);
            Assert.Equal(expectedWorldCount, modified.Worlds.Count);
        }
    }
}
