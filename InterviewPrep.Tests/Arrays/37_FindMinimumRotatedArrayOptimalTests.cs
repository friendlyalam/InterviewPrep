namespace InterviewPrep.Tests.Arrays
{
    using InterviewPrep.DSA.DataStructures._01_Array.Problems._37_MaximumRotateSortedArray.Solutions;
    using Xunit;

    public class FindMinimumRotatedArrayOptimalTests
    {
        [Theory]
        [InlineData(
            new[] { 3, 4, 5, 1, 2 },
            1)]

        [InlineData(
            new[] { 4, 5, 6, 7, 0, 1, 2 },
            0)]

        [InlineData(
            new[] { 11, 13, 15, 17 },
            11)]

        [InlineData(
            new[] { 2, 1 },
            1)]

        [InlineData(
            new[] { 1, 2 },
            1)]

        [InlineData(
            new[] { 5 },
            5)]

        [InlineData(
            new[] { 6, 7, 8, 1, 2, 3, 4, 5 },
            1)]

        [InlineData(
            new[] { 5, 1, 2, 3, 4 },
            1)]

        [InlineData(
            new[] { 2, 3, 4, 5, 1 },
            1)]

        [InlineData(
            new[] { -3, -2, -1, -5, -4 },
            -5)]

        [InlineData(
            new[] { 3, 4, 5, -2, -1, 0, 1 },
            -2)]

        [InlineData(
            new[] { -10, -5, -2, -1, -20 },
            -20)]

        [InlineData(
            new[] { 100, 200, 300, 50, 70 },
            50)]

        public void FindMin_ShouldReturnMinimumValue(
            int[] nums,
            int expected)
        {
            int result =
                FindMinimumRotatedArrayOptimal.FindMin(nums);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void FindMin_ShouldThrowArgumentNullException_WhenArrayIsNull()
        {
            int[] nums = null!;

            Assert.Throws<ArgumentNullException>(
                () => FindMinimumRotatedArrayOptimal.FindMin(nums));
        }

        [Fact]
        public void FindMin_ShouldThrowArgumentException_WhenArrayIsEmpty()
        {
            int[] nums = Array.Empty<int>();

            Assert.Throws<ArgumentException>(
                () => FindMinimumRotatedArrayOptimal.FindMin(nums));
        }
    }
}
