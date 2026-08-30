
using InterviewPrep.DSA.DataStructures._01_Array.Problems._11_FindFirstOccuranceIndex.Solutions;

namespace InterviewPrep.Tests.Arrays
{
    public class _11_FindFirstOccuranceIndexTests
    {
        public class FindFirstOccurrenceIndexTests
        {
            [Fact]
            public void Find_ShouldThrowArgumentNullException_WhenInputIsNull()
            {
                int[] numbers = null!;

                Assert.Throws<ArgumentNullException>(
                    () => FindFirstOccurrenceIndex.Find(numbers, 10));
            }

            [Fact]
            public void Find_ShouldThrowArgumentException_WhenInputIsEmpty()
            {
                int[] numbers = Array.Empty<int>();

                Assert.Throws<ArgumentException>(
                    () => FindFirstOccurrenceIndex.Find(numbers, 10));
            }

            [Theory]
            [InlineData(new[] { 10, 20, 30, 20, 40 }, 20, 1)]
            [InlineData(new[] { 5, 8, 5, 10, 5 }, 5, 0)]
            [InlineData(new[] { 10, 20, 30 }, 50, -1)]
            [InlineData(new[] { -5, 10, -5, 20 }, -5, 0)]
            [InlineData(new[] { 10 }, 10, 0)]
            [InlineData(new[] { 10 }, 20, -1)]
            [InlineData(new[] { 1, 2, 3, 4, 5 }, 5, 4)]
            [InlineData(new[] { 7, 7, 7, 7 }, 7, 0)]
            [InlineData(new[] { -10, -20, -30 }, -20, 1)]
            [InlineData(new[] { 0, 5, 0, 10 }, 0, 0)]
            [InlineData(new[] { int.MinValue, 0, int.MinValue }, int.MinValue, 0)]
            [InlineData(new[] { 1, int.MaxValue, 5 }, int.MaxValue, 1)]
            public void Find_ShouldReturnFirstOccurrenceIndex(
                int[] numbers,
                int target,
                int expected)
            {
                int result = FindFirstOccurrenceIndex.Find(numbers, target);

                Assert.Equal(expected, result);
            }
        }
    }
}
