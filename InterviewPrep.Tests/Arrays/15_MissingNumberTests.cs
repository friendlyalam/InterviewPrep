
using InterviewPrep.DSA.DataStructures._01_Array.Problems._15_FindTheMissingNumber.Solutions;

namespace InterviewPrep.Tests.Arrays
{
    public class MissingNumberTests
    {
        [Theory]
        [InlineData(new int[] { 3, 0, 1 }, 2)]
        [InlineData(new int[] { 0, 1 }, 2)]
        [InlineData(new int[] { 9, 6, 4, 2, 3, 5, 7, 0, 1 }, 8)]
        [InlineData(new int[] { 1 }, 0)]
        [InlineData(new int[] { 0, 1, 2, 3 }, 4)]
        [InlineData(new int[] { 0, 1, 3, 4, 5 }, 2)]
        public void MissingNumber_ShouldReturnExpectedResult(
        int[] nums,
        int expected)
        {
            // Act
            int result = FindMissingNumberOptimal.MissingNumber(nums);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void MissingNumber_ShouldReturn0_WhenArrayIsEmpty()
        {
            // Arrange
            int[] nums = Array.Empty<int>();

            // Act
            int result = FindMissingNumberOptimal.MissingNumber(nums);

            // Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void MissingNumber_ShouldThrowArgumentNullException_WhenArrayIsNull()
        {
            // Arrange
            int[] nums = null!;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(
                () => FindMissingNumberOptimal.MissingNumber(nums));
        }
    }
}
