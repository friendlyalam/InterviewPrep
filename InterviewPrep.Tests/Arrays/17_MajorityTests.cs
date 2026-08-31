
using InterviewPrep.DSA.DataStructures._01_Array.Problems._17_MajorityElement.Solutions;

namespace InterviewPrep.Tests.Arrays
{
    public class MajorityTests
    {
        [Theory]
        [InlineData(new int[] { 3, 2, 3 }, 3)]
        [InlineData(new int[] { 2, 2, 1, 1, 1, 2, 2 }, 2)]
        [InlineData(new int[] { 1 }, 1)]
        [InlineData(new int[] { 3, 3, 3, 2, 2 }, 3)]
        [InlineData(new int[] { 2, 2, 3, 3, 3 }, 3)]
        [InlineData(new int[] { -1, -1, 2, 2, -1 }, -1)]
        public void Majority_ShouldReturnMajorityElement(
        int[] nums,
        int expected)
        {
            // Act
            int result = FindElementMajority.Majority(nums);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Majority_ShouldThrowArgumentNullException_WhenArrayIsNull()
        {
            // Arrange
            int[] nums = null!;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(
                () => FindElementMajority.Majority(nums));
        }

        [Fact]
        public void Majority_ShouldThrowArgumentException_WhenArrayIsEmpty()
        {
            // Arrange
            int[] nums = Array.Empty<int>();

            // Act & Assert
            Assert.Throws<ArgumentException>(
                () => FindElementMajority.Majority(nums));
        }
    }
}
