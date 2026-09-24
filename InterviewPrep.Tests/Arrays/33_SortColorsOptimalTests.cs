

using InterviewPrep.DSA.DataStructures._01_Array.Problems._33_MaximumSumSubarrayAny.Solutions;

namespace InterviewPrep.Tests.Arrays
{

    public class SortColorsOptimalTests
    {
        [Theory]
        [InlineData(
            new[] { 2, 0, 2, 1, 1, 0 },
            new[] { 0, 0, 1, 1, 2, 2 })]

        [InlineData(
            new[] { 2, 0, 1 },
            new[] { 0, 1, 2 })]

        [InlineData(
            new[] { 0, 0, 1, 2, 2 },
            new[] { 0, 0, 1, 2, 2 })]

        [InlineData(
            new[] { 2, 2, 2, 2 },
            new[] { 2, 2, 2, 2 })]

        [InlineData(
            new[] { 0, 0, 0, 0 },
            new[] { 0, 0, 0, 0 })]

        [InlineData(
            new[] { 1, 1, 1, 1 },
            new[] { 1, 1, 1, 1 })]

        [InlineData(
            new[] { 1, 0, 1, 0, 1, 0 },
            new[] { 0, 0, 0, 1, 1, 1 })]

        [InlineData(
            new[] { 2, 1, 2, 1, 2, 1 },
            new[] { 1, 1, 1, 2, 2, 2 })]

        [InlineData(
            new[] { 2, 0 },
            new[] { 0, 2 })]

        [InlineData(
            new[] { 1, 2, 0 },
            new[] { 0, 1, 2 })]

        [InlineData(
            new[] { 2, 1, 0, 2, 1, 0 },
            new[] { 0, 0, 1, 1, 2, 2 })]

        public void Sort_ShouldReturnSortedArray(
            int[] nums,
            int[] expected)
        {
            int[] result =
                SortColorsOptimal.Sort(nums);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Sort_ShouldThrowArgumentNullException_WhenArrayIsNull()
        {
            int[] nums = null!;

            Assert.Throws<ArgumentNullException>(
                () => SortColorsOptimal.Sort(nums));
        }

        [Fact]
        public void Sort_ShouldThrowArgumentException_WhenArrayIsEmpty()
        {
            int[] nums = Array.Empty<int>();

            Assert.Throws<ArgumentException>(
                () => SortColorsOptimal.Sort(nums));
        }

        [Fact]
        public void Sort_ShouldThrowArgumentException_WhenArrayContainsInvalidValue()
        {
            int[] nums = { 0, 1, 3, 2 };

            Assert.Throws<ArgumentException>(
                () => SortColorsOptimal.Sort(nums));
        }

        [Fact]
        public void Sort_ShouldThrowArgumentException_WhenArrayContainsNegativeValue()
        {
            int[] nums = { 0, -1, 2 };

            Assert.Throws<ArgumentException>(
                () => SortColorsOptimal.Sort(nums));
        }
    }
}
