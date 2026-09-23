using InterviewPrep.DSA.DataStructures._01_Array.Problems._31_SubarraySumEqualsK.Solutions;

namespace InterviewPrep.Tests.Arrays
{
    public class SubarraySumEqualsKOptimalTests
    {
        [Theory]
        [InlineData(
            new[] { 1, 1, 1 },
            2,
            2)]
        [InlineData(
            new[] { 1, 2, 3 },
            3,
            2)]
        [InlineData(
            new[] { 1, -1, 0 },
            0,
            3)]
        [InlineData(
            new[] { 3, 4, 7, 2, -3, 1, 4, 2 },
            7,
            4)]
        [InlineData(
            new[] { 1, 2, 3 },
            10,
            0)]
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
            10)]
        [InlineData(
            new[] { -1, -1, 1 },
            -1,
            3)]
        [InlineData(
            new[] { 1, -1, 1, -1 },
            0,
            4)]
        [InlineData(
            new[] { 2, -2, 2, -2 },
            0,
            4)]
        [InlineData(
            new[] { 1, 2, -1, 2, 3, -2, 1 },
            5,
            3)]
        public void Count_ShouldReturnNumberOfSubarraysWithTargetSum(
            int[] nums,
            int k,
            int expected)
        {
            int result =
                SubarraySumEqualsKOptimal.Count(nums, k);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Count_ShouldThrowArgumentNullException_WhenArrayIsNull()
        {
            int[] nums = null!;

            Assert.Throws<ArgumentNullException>(
                () => SubarraySumEqualsKOptimal.Count(nums, 5));
        }

        [Fact]
        public void Count_ShouldThrowArgumentException_WhenArrayIsEmpty()
        {
            int[] nums = Array.Empty<int>();

            Assert.Throws<ArgumentException>(
                () => SubarraySumEqualsKOptimal.Count(nums, 5));
        }
    }
}
