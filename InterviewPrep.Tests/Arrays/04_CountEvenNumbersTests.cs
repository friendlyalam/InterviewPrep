using InterviewPrep.DSA.DataStructures._01_Array.Problems._04_CountEvenNumbers.Solutions;

namespace InterviewPrep.Tests.Arrays
{
    public class CountEvenNumbersTests
    {
        [Fact]
        public void Count_ShouldThrowArgumentNullException_WhenInputIsNull()
        {
            int[] input = null!;

            Assert.Throws<ArgumentNullException>(
                () => CountEvenNumbers.Count(input));
        }

        [Fact]
        public void Count_ShouldThrowArgumentException_WhenInputIsEmpty()
        {
            int[] input = Array.Empty<int>();

            Assert.Throws<ArgumentException>(
                () => CountEvenNumbers.Count(input));
        }

        [Theory]
        [InlineData(new[] { 1, 2, 4, 7, 10 }, 3)]
        [InlineData(new[] { 3, 5, 7 }, 0)]
        [InlineData(new[] { -2, -5, 0, 8 }, 3)]
        [InlineData(new[] { 2, 4, 6, 8 }, 4)]
        [InlineData(new[] { 1, 3, 5, 7 }, 0)]
        [InlineData(new[] { 2 }, 1)]
        [InlineData(new[] { 3 }, 0)]
        [InlineData(new[] { 0 }, 1)]
        [InlineData(new[] { -2, -4, -6 }, 3)]
        [InlineData(new[] { -1, -3, -5 }, 0)]
        [InlineData(new[] { 1, 2, 3, 4, 5, 6 }, 3)]
        public void Count_ShouldReturnCorrectNumberOfEvenElements(
            int[] input,
            int expected)
        {
            int result = CountEvenNumbers.Count(input);

            Assert.Equal(expected, result);
        }
    }
}
