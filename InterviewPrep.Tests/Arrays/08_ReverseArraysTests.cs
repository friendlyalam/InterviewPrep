using InterviewPrep.DSA.DataStructures._01_Array.Problems._08_ReverseArray.Solutions;

namespace InterviewPrep.Tests.Arrays
{
    public class ReverseAnArrayTests
    {
        [Fact]
        public void Reverse_ShouldThrowArgumentNullException_WhenInputIsNull()
        {
            int[] input = null!;

            Assert.Throws<ArgumentNullException>(
                () => ReverseAnArray.Reverse(input));
        }

        [Fact]
        public void Reverse_ShouldThrowArgumentException_WhenInputIsEmpty()
        {
            int[] input = Array.Empty<int>();

            Assert.Throws<ArgumentException>(
                () => ReverseAnArray.Reverse(input));
        }

        [Theory]
        [InlineData(new[] { 1 }, new[] { 1 })]
        [InlineData(new[] { 1, 2 }, new[] { 2, 1 })]
        [InlineData(new[] { 1, 2, 3 }, new[] { 3, 2, 1 })]
        [InlineData(new[] { 1, 2, 3, 4 }, new[] { 4, 3, 2, 1 })]
        [InlineData(new[] { 1, 2, 3, 4, 5 }, new[] { 5, 4, 3, 2, 1 })]
        [InlineData(new[] { -1, 0, 5, -10 }, new[] { -10, 5, 0, -1 })]
        [InlineData(new[] { 5, 5, 5 }, new[] { 5, 5, 5 })]
        public void Reverse_ShouldReturnReversedArray(
            int[] input,
            int[] expected)
        {
            int[] result = ReverseAnArray.Reverse(input);

            Assert.Equal(expected, result);
        }
    }
}
