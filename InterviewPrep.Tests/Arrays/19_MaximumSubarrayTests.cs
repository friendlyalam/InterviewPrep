
using InterviewPrep.DSA.DataStructures._01_Array.Problems._19_Maximum_Subarray.Solutions;

namespace InterviewPrep.Tests.Arrays
{
    public class MaximumSubarrayTests
    {
        [Theory]
        [InlineData(new int[] { -2, 1, -3, 4, -1, 2, 1, -5, 4 }, 6)]
        [InlineData(new int[] { 1 }, 1)]
        [InlineData(new int[] { 5, 4, -1, 7, 8 }, 23)]
        [InlineData(new int[] { -5, -2, -8, -1 }, -1)]
        [InlineData(new int[] { 1, 2, 3, 4 }, 10)]
        [InlineData(new int[] { -3, 2, 3 }, 5)]
        [InlineData(new int[] { 4, -1, 2, -3 }, 5)]
        [InlineData(new int[] { -2, -1 }, -1)]
        [InlineData(new int[] { -2, 1 }, 1)]
        public void MaxSubArray_ShouldReturnMaximumSubarraySum(
            int[] nums,
            int expected)
        {
            // Act
            int result = MaximumSubarrayOptimal.MaxSubArray(nums);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void MaxSubArray_ShouldThrowArgumentNullException_WhenArrayIsNull()
        {
            // Arrange
            int[] nums = null!;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(
                () => MaximumSubarrayOptimal.MaxSubArray(nums));
        }

        [Fact]
        public void MaxSubArray_ShouldThrowArgumentException_WhenArrayIsEmpty()
        {
            // Arrange
            int[] nums = Array.Empty<int>();

            // Act & Assert
            Assert.Throws<ArgumentException>(
                () => MaximumSubarrayOptimal.MaxSubArray(nums));
        }
    }
}
