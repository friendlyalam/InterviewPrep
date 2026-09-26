namespace InterviewPrep.Tests.Arrays
{
    using InterviewPrep.DSA.DataStructures._01_Array.Problems._35_FindFirstLastPosition.Solutions;
    using Xunit;

    public class SearchRangeOptimalTests
    {
        [Theory]
        [InlineData(
            new[] { 5, 7, 7, 8, 8, 10 },
            8,
            new[] { 3, 4 })]

        [InlineData(
            new[] { 5, 7, 7, 8, 8, 10 },
            6,
            new[] { -1, -1 })]

        [InlineData(
            new[] { 1 },
            1,
            new[] { 0, 0 })]

        [InlineData(
            new[] { 1 },
            2,
            new[] { -1, -1 })]

        [InlineData(
            new[] { 2, 2, 2, 2, 2 },
            2,
            new[] { 0, 4 })]

        [InlineData(
            new[] { 1, 2, 3, 4, 5 },
            1,
            new[] { 0, 0 })]

        [InlineData(
            new[] { 1, 2, 3, 4, 5 },
            5,
            new[] { 4, 4 })]

        [InlineData(
            new[] { 1, 2, 2, 2, 3 },
            2,
            new[] { 1, 3 })]

        [InlineData(
            new[] { -5, -3, -3, -3, 0, 2 },
            -3,
            new[] { 1, 3 })]

        [InlineData(
            new[] { 0, 0, 1, 1, 1, 2 },
            1,
            new[] { 2, 4 })]

        [InlineData(
            new[] { 1, 1, 2, 2, 3, 3 },
            2,
            new[] { 2, 3 })]

        [InlineData(
            new[] { 1, 1, 1, 2, 2, 3, 3, 3 },
            3,
            new[] { 5, 7 })]

        public void SearchRange_ShouldReturnExpectedRange(
            int[] nums,
            int target,
            int[] expected)
        {
            int[] result =
                SearchRangeOptimal.SearchRange(nums, target);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void SearchRange_ShouldThrowArgumentNullException_WhenArrayIsNull()
        {
            int[] nums = null!;

            Assert.Throws<ArgumentNullException>(
                () => SearchRangeOptimal.SearchRange(nums, 5));
        }

        [Fact]
        public void SearchRange_ShouldThrowArgumentException_WhenArrayIsEmpty()
        {
            int[] nums = Array.Empty<int>();

            Assert.Throws<ArgumentException>(
                () => SearchRangeOptimal.SearchRange(nums, 5));
        }
    }
}
