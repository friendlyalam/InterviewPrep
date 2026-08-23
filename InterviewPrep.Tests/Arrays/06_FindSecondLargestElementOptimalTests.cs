
using InterviewPrep.DSA.DataStructures._01_Array.Problems._06_FindSecondLargestElement.Solutions;

namespace InterviewPrep.Tests.Arrays
{
    public class FindSecondLargestElementOptimalTests
    {
        [Fact]
        public void Find_ShouldThrowArgumentNullException_WhenInputIsNull()
        {
            int[] input = null!;

            Assert.Throws<ArgumentNullException>(
                () => FindSecondLargestOptimal.Find(input));
        }

        [Fact]
        public void Find_ShouldThrowArgumentException_WhenInputIsEmpty()
        {
            int[] input = Array.Empty<int>();

            Assert.Throws<ArgumentException>(
                () => FindSecondLargestOptimal.Find(input));
        }
        [Fact]
        public void Find_ShouldThrowArgumentException_WhenInputHasOneElement()
        {
            int[] input = { 10 };

            Assert.Throws<ArgumentException>(
                () => FindSecondLargestOptimal.Find(input));
        }

        [Fact]
        public void Find_ShouldThrowArgumentException_WhenNoSecondDistinctElementExists()
        {
            int[] input = { 5, 5, 5 };
            Assert.Throws<ArgumentException>(() => FindSecondLargestOptimal.Find(input));
        }

        [Theory]
        [InlineData(new[] { 10, 20, 30, 40, 50 }, 40)]
        [InlineData(new[] { 10, 50, 20, 50, 30 }, 30)]
        [InlineData(new[] { 10, 20 }, 10)]
        [InlineData(new[] { -10, -20, -30 }, -20)]
        [InlineData(new[] { -10, 5, -20, 5, 3 }, 3)]
        [InlineData(new[] { 10, 10, 20, 20, 30 }, 20)]
        [InlineData(new[] { 50, 50, 40, 40, 30 }, 40)]
        [InlineData(new[] { int.MinValue, 0, -10 }, -10)]
        [InlineData(new[] { int.MaxValue, 10, 20 }, 20)]
        [InlineData(new[] { -5, -5, -10, -10 }, -10)]
        public void Find_ShouldReturnSecondLargestDistinctElement(
        int[] input,
        int expected)
        {
            int result = FindSecondLargestOptimal.Find(input);

            Assert.Equal(expected, result);
        }
    }
}
