using InterviewPrep.DSA.DataStructures._01_Array.Problems._09_IsArraySorted.Solutions;

namespace InterviewPrep.Tests.Arrays
{
    public class IsArraySortedTests
    {
        [Fact]
        public void IsSorted_ShouldThrowArgumentNullException_WhenInputIsNull()
        {
            int[] input = null!;

            Assert.Throws<ArgumentNullException>(
                () => IsArraySorted.IsSorted(input));
        }

        [Fact]
        public void IsSorted_ShouldThrowArgumentException_WhenInputIsEmpty()
        {
            int[] input = Array.Empty<int>();

            Assert.Throws<ArgumentException>(
                () => IsArraySorted.IsSorted(input));
        }

        [Theory]
        [InlineData(new[] { 1 }, true)]
        [InlineData(new[] { 1, 2, 3, 4, 5 }, true)]
        [InlineData(new[] { 1, 2, 2, 4, 5 }, true)]
        [InlineData(new[] { -5, -3, -1, 0, 2 }, true)]
        [InlineData(new[] { 1, 3, 2, 4, 5 }, false)]
        [InlineData(new[] { 5, 4, 3, 2, 1 }, false)]
        [InlineData(new[] { 1, 5, 3 }, false)]
        [InlineData(new[] { 0, 0, 0 }, true)]
        [InlineData(new[] { -10, -20, -30 }, false)]
        [InlineData(new[] { int.MinValue, 0, int.MaxValue }, true)]
        public void IsSorted_ShouldReturnCorrectResult(
            int[] input,
            bool expected)
        {
            bool result = IsArraySorted.IsSorted(input);

            Assert.Equal(expected, result);
        }
    }
}
