using InterviewPrep.DSA.DataStructures._01_Array.Problems._32_MaximumSumSubarray.Solutions;

namespace InterviewPrep.Tests.Arrays
{
    public class MaximumSumSubarrayOfSizeKOptimalTests
    {
        [Theory]
        [InlineData(
            new[] { 2, 1, 5, 1, 3, 2 },
            3,
            9)]
        [InlineData(
            new[] { 2, 3, 4, 1, 5 },
            2,
            7)]
        [InlineData(
            new[] { -5, -2, -3, -1 },
            2,
            -4)]
        [InlineData(
            new[] { 1, 2, 3, 4, 5 },
            1,
            5)]
        [InlineData(
            new[] { 1, 2, 3, 4, 5 },
            5,
            15)]
        [InlineData(
            new[] { 0, 0, 0, 0 },
            2,
            0)]
        [InlineData(
            new[] { -1, -2, -3, -4 },
            2,
            -3)]
        [InlineData(
            new[] { 5, -1, 3, 2, -2, 4 },
            3,
            7)]
        [InlineData(
            new[] { -5, 10, -2, 8, -1 },
            2,
            8)]
        [InlineData(
            new[] { 4, 4, 4, 4 },
            2,
            8)]
        [InlineData(
            new[] { -10, -20, -5, -2 },
            2,
            -7)]
        [InlineData(
            new[] { 3, -2, 5, -1, 6 },
            3,
            10)]
        public void Find_ShouldReturnMaximumSumOfSubarrayOfSizeK(
            int[] nums,
            int k,
            int expected)
        {
            int result =
                MaximumSumSubarrayOfSizeKOptimal.Find(nums, k);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Find_ShouldThrowArgumentNullException_WhenArrayIsNull()
        {
            int[] nums = null!;

            Assert.Throws<ArgumentNullException>(
                () => MaximumSumSubarrayOfSizeKOptimal.Find(nums, 2));
        }

        [Fact]
        public void Find_ShouldThrowArgumentException_WhenArrayIsEmpty()
        {
            int[] nums = Array.Empty<int>();

            Assert.Throws<ArgumentException>(
                () => MaximumSumSubarrayOfSizeKOptimal.Find(nums, 2));
        }

        [Fact]
        public void Find_ShouldThrowArgumentException_WhenKIsZero()
        {
            int[] nums = { 1, 2, 3 };

            Assert.Throws<ArgumentException>(
                () => MaximumSumSubarrayOfSizeKOptimal.Find(nums, 0));
        }

        [Fact]
        public void Find_ShouldThrowArgumentException_WhenKIsNegative()
        {
            int[] nums = { 1, 2, 3 };

            Assert.Throws<ArgumentException>(
                () => MaximumSumSubarrayOfSizeKOptimal.Find(nums, -1));
        }

        [Fact]
        public void Find_ShouldThrowArgumentException_WhenKIsGreaterThanArrayLength()
        {
            int[] nums = { 1, 2, 3 };

            Assert.Throws<ArgumentException>(
                () => MaximumSumSubarrayOfSizeKOptimal.Find(nums, 4));
        }
    }
}
