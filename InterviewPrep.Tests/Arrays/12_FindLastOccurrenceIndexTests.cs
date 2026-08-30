using InterviewPrep.DSA.DataStructures._01_Array.Problems._12_FindLastOccuranceIndex.Solutions;
namespace InterviewPrep.Tests.Arrays
{
    public class FindLastOccurrenceIndexTests
    {
        [Fact]
        public void Find_ShouldThrowArgumentNullException_WhenInputIsNull()
        {
            int[] numbers = null!;

            Assert.Throws<ArgumentNullException>(
                () => FindLastOccurrenceIndex.Find(numbers, 10));
        }

        [Fact]
        public void Find_ShouldThrowArgumentException_WhenInputIsEmpty()
        {
            int[] numbers = Array.Empty<int>();

            Assert.Throws<ArgumentException>(
                () => FindLastOccurrenceIndex.Find(numbers, 10));
        }

        [Theory]
        [InlineData(new[] { 10, 20, 30, 20, 40 }, 20, 3)]
        [InlineData(new[] { 5, 8, 5, 10, 5 }, 5, 4)]
        [InlineData(new[] { 10, 20, 30 }, 50, -1)]
        [InlineData(new[] { -5, 10, -5, 20, -5 }, -5, 4)]
        [InlineData(new[] { 10 }, 10, 0)]
        [InlineData(new[] { 10 }, 20, -1)]
        [InlineData(new[] { 7, 7, 7, 7 }, 7, 3)]
        [InlineData(new[] { 1, 2, 3, 4, 5 }, 5, 4)]
        [InlineData(new[] { 1, 2, 3, 4, 5 }, 1, 0)]
        [InlineData(new[] { -10, -20, -30 }, -30, 2)]
        [InlineData(new[] { 0, 5, 0, 10, 0 }, 0, 4)]
        [InlineData(new[] { int.MinValue, 0, int.MinValue }, int.MinValue, 2)]
        [InlineData(new[] { 1, int.MaxValue, 5, int.MaxValue }, int.MaxValue, 3)]
        public void Find_ShouldReturnLastOccurrenceIndex(
            int[] numbers,
            int target,
            int expected)
        {
            int result = FindLastOccurrenceIndex.Find(numbers, target);

            Assert.Equal(expected, result);
        }
    }
}
