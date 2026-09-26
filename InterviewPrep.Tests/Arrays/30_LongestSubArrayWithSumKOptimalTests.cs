namespace InterviewPrep.Tests.Arrays
{
    using InterviewPrep.DSA.DataStructures._01_Array.Problems._30_LongestSubarrayWithSumK.Solutions;
    using Xunit;

    public class LongestSubArrayWithSumKOptimalTests
    {
        [Theory]
        [InlineData(
            new[] { 10, 5, 2, 7, 1, 9 },
            15,
            4)]
        [InlineData(
            new[] { 1, 2, 3 },
            10,
            0)]
        [InlineData(
            new[] { 1, 2, 3 },
            6,
            3)]
        [InlineData(
            new[] { 5 },
            5,
            1)]
        [InlineData(
            new[] { 5 },
            10,
            0)]
        [InlineData(
            new[] { 0, 0, 0, 0 },
            0,
            4)]
        [InlineData(
            new[] { -1, -2, 3, 1 },
            -3,
            2)]
        [InlineData(
            new[] { 1, -1, 5, -2, 3 },
            3,
            4)]
        [InlineData(
            new[] { 2, -2, 2, -2, 2 },
            0,
            4)]
        [InlineData(
            new[] { 3, 1, -1, 2, 4, -2 },
            5,
            4)]
        [InlineData(
            new[] { -2, -1, 2, 1 },
            0,
            4)]
        [InlineData(
            new[] { 1, 2, -1, 2, 3, -2, 1 },
            5,
            6)]
        public void Find_ShouldReturnLongestSubArrayLength(
            int[] nums,
            int k,
            int expected)
        {
            int result =
                LongestSubArrayWithSumKOptimal.Find(nums, k);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Find_ShouldThrowArgumentNullException_WhenArrayIsNull()
        {
            int[] nums = null!;

            Assert.Throws<ArgumentNullException>(
                () => LongestSubArrayWithSumKOptimal.Find(nums, 15));
        }

        [Fact]
        public void Find_ShouldThrowArgumentException_WhenArrayIsEmpty()
        {
            int[] nums = Array.Empty<int>();

            Assert.Throws<ArgumentException>(
                () => LongestSubArrayWithSumKOptimal.Find(nums, 15));
        }
    }
}
