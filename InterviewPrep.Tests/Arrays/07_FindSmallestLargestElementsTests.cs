

using InterviewPrep.DSA.DataStructures._01_Array.Problems._07_FindSmallestLargest.Solutions;

namespace InterviewPrep.Tests.Arrays
{
    public class FindSmallestLargestElementsTests
    {
        [Fact]
        public void Find_ShouldThrowArgumentNullException_WhenInputIsNull()
        {
            int[] input = null!;
            Assert.Throws<ArgumentNullException>(() => FindSmallestLargestElements.Find(input));
        }
        [Fact]
        public void Find_ShouldThrowArgumentException_WhenInputIsEmpty()
        {
            int[] input = Array.Empty<int>();
            Assert.Throws<ArgumentNullException>(() => FindSmallestLargestElements.Find(input));
        }

        [Theory]
        [InlineData(new[] { 10, 5, 20, 3, 15 }, 3, 20)]
        [InlineData(new[] { -10, -5, -20, 0 }, -20, 0)]
        [InlineData(new[] { 7 }, 7, 7)]
        [InlineData(new[] { 0, 0, 0 }, 0, 0)]
        [InlineData(new[] { -5, -2, -10, -1 }, -10, -1)]
        [InlineData(new[] { 100, 50, 75, 25 }, 25, 100)]
        [InlineData(new[] { int.MinValue, 0, int.MaxValue }, int.MinValue, int.MaxValue)]

        public void Find_ShouldReturnSmallestLargestElement(int[] input, int expectedSmallest, int expectedLargest)
        {
            var result = FindSmallestLargestElements.Find(input);
            Assert.Equal(expectedSmallest, result.smallest);
            Assert.Equal(expectedLargest, result.largest);
        }
    }
}
