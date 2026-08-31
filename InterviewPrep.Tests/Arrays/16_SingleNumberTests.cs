using InterviewPrep.DSA.DataStructures._01_Array.Problems._16_SingleNumber.Solutions;

namespace InterviewPrep.Tests.Arrays
{
    public class SingleNumberTests
    {
        [Theory]
        [InlineData(new int[] { 2, 2, 1 }, 1)]
        [InlineData(new int[] { 4, 1, 2, 1, 2 }, 4)]
        [InlineData(new int[] { 1 }, 1)]
        [InlineData(new int[] { 7, 3, 5, 3, 5 }, 7)]
        [InlineData(new int[] { -1, -2, -1 }, -2)]
        [InlineData(new int[] { -5, 10, 10 }, -5)]
        public void Find_ShouldReturnSingleNumber(
        int[] nums,
        int expected)
        {
            // Act
            int result = FindSingleNumber.Find(nums);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Find_ShouldThrowArgumentNullException_WhenArrayIsNull()
        {
            // Arrange
            int[] nums = null!;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(
                () => FindSingleNumber.Find(nums));
        }

        [Fact]
        public void Find_ShouldHandleEmptyArray()
        {
            // Arrange
            int[] nums = Array.Empty<int>();

            // Act
            int result = FindSingleNumber.Find(nums);

            // Assert
            Assert.Equal(0, result);
        }
    }
}
