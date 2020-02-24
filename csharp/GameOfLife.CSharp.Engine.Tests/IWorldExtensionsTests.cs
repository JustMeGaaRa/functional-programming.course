using System;
using Xunit;

namespace GameOfLife.CSharp.Engine.Tests
{
    public class IWorldExtensionsTests
    {
        [Fact]
        public void IsValidIndex_WithNullWorld_ShouldThrowArgumentNullException()
        {
            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => IWorldExtensions.IsValidIndex(null, 0, 0));
        }

        [Theory]
        [InlineData(3, 7, 4, 4, true)]
        [InlineData(3, 7, 10, 10, false)]
        [InlineData(3, 7, 7, 9, false)]
        [InlineData(5, 0, 4, 4, true)]
        [InlineData(5, 0, 10, 10, false)]
        [InlineData(5, 0, 9, 4, false)]
        public void IsValidIndex_WithSize5By5_ShouldIgnoreTopLeft(int left, int top, int column, int row, bool expected)
        {
            // Arrange
            IWorld world = World.FromSize(5, 5);

            // Act
            IWorld moved = world.Move(left, top);
            bool actual = IWorldExtensions.IsValidIndex(moved, row, column);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void IsInBounds_WithNullWorld_ShouldThrowArgumentNullException()
        {
            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => IWorldExtensions.IsInBounds(null, 0, 0));
        }

        [Theory]
        [InlineData(3, 7, 4, 4, false)]
        [InlineData(3, 7, 10, 10, false)]
        [InlineData(3, 7, 7, 9, true)]
        [InlineData(5, 0, 4, 4, false)]
        [InlineData(5, 0, 10, 10, false)]
        [InlineData(5, 0, 9, 4, true)]
        public void IsInBounds_WithSize5By5_ShouldIncludeTopLeft(int left, int top, int column, int row, bool expected)
        {
            // Arrange
            IWorld world = World.FromSize(5, 5);

            // Act
            IWorld moved = world.Move(left, top);
            bool actual = IWorldExtensions.IsInBounds(moved, row, column);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
