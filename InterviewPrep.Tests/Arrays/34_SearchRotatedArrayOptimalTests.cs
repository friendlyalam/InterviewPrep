namespace InterviewPrep.Tests.Arrays
{
    using InterviewPrep.DSA.DataStructures._01_Array.Problems._34_RotatedSortedArraySearch.Solutions;
    using Xunit;

    public class SearchRotatedArrayOptimalTests
    {
        [Theory]
        [InlineData(
            new[] { 4, 5, 6, 7, 0, 1, 2 },
            0,
            4)]

        [InlineData(
            new[] { 4, 5, 6, 7, 0, 1, 2 },
            3,
            -1)]

        [InlineData(
            new[] { 1 },
            1,
            0)]

        [InlineData(
            new[] { 1 },
            2,
            -1)]

        [InlineData(
            new[] { 1, 2, 3, 4, 5, 6 },
            4,
            3)]

        [InlineData(
            new[] { 6, 1, 2, 3, 4, 5 },
            6,
            0)]

        [InlineData(
            new[] { 6, 7, 1, 2, 3, 4, 5 },
            1,
            2)]

        [InlineData(
            new[] { 6, 7, 8, 1, 2, 3, 4, 5 },
            5,
            7)]

        [InlineData(
            new[] { 3, 4, 5, 1, 2 },
            3,
            0)]

        [InlineData(
            new[] { 3, 4, 5, 1, 2 },
            2,
            4)]

        [InlineData(
            new[] { 2, 3, 4, 5, 1 },
            1,
            4)]

        [InlineData(
            new[] { 5, 1, 2, 3, 4 },
            5,
            0)]

        [InlineData(
            new[] { 5, 1, 2, 3, 4 },
            4,
            4)]

        public void Search_ShouldReturnExpectedIndex(
            int[] nums,
            int target,
            int expected)
        {
            int result =
                SearchRotatedArrayOptimal.Search(nums, target);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Search_ShouldThrowArgumentNullException_WhenArrayIsNull()
        {
            int[] nums = null!;

            Assert.Throws<ArgumentNullException>(
                () => SearchRotatedArrayOptimal.Search(nums, 5));
        }

        [Fact]
        public void Search_ShouldThrowArgumentException_WhenArrayIsEmpty()
        {
            int[] nums = Array.Empty<int>();

            Assert.Throws<ArgumentException>(
                () => SearchRotatedArrayOptimal.Search(nums, 5));
        }
    }
}
