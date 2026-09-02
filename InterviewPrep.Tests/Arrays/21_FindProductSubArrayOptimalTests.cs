using InterviewPrep.DSA.DataStructures._01_Array.Problems._21_MaximumProductSubarray.Solutions;

namespace InterviewPrep.Tests.Arrays
{
    public class FindProductSubArrayOptimalTests
    {
        [Theory]
        [InlineData(
            new int[] { 2, 3, -2, 4 },
            6)]

        [InlineData(
            new int[] { -2, 0, -1 },
            0)]

        [InlineData(
            new int[] { -2, 3, -4 },
            24)]

        [InlineData(
            new int[] { -2 },
            -2)]

        [InlineData(
            new int[] { 2, 3, 4 },
            24)]

        [InlineData(
            new int[] { -2, -3, -4 },
            12)]

        [InlineData(
            new int[] { 2, 3, 0, 4 },
            6)]

        [InlineData(
            new int[] { -2, 0, -1, 0 },
            0)]

        [InlineData(
            new int[] { -2, 3, -4, 5 },
            120)]

        [InlineData(
            new int[] { -1, -2, -3 },
            6)]

        [InlineData(
            new int[] { 0, 2 },
            2)]

        [InlineData(
            new int[] { 0, -2 },
            0)]
        public void MaxProduct_ShouldReturnMaximumProduct(
            int[] nums,
            int expected)
        {
            // Act
            int result = FindProductSubArrayOptimal.MaxProduct(nums);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void MaxProduct_ShouldThrowArgumentNullException_WhenArrayIsNull()
        {
            // Arrange
            int[] nums = null!;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(
                () => FindProductSubArrayOptimal.MaxProduct(nums));
        }

        [Fact]
        public void MaxProduct_ShouldThrowArgumentException_WhenArrayIsEmpty()
        {
            // Arrange
            int[] nums = Array.Empty<int>();

            // Act & Assert
            Assert.Throws<ArgumentException>(
                () => FindProductSubArrayOptimal.MaxProduct(nums));
        }
    }
}
