
using InterviewPrep.DSA.DataStructures._01_Array.Problems._001_FindLargestElement.Solutions;

namespace InterviewPrep.Tests.Arrays
{
    public class _01_FindLargestElementTests
    {
        [Fact]
        public void Find_ShouldThrowArgumentNullException_WhenInputIsNull()
        {
            int[] input = null!;//The ! is called the null-forgiving operator.
            Assert.Throws<ArgumentNullException>(() => FindLargestElement.Find(input));
        }

        [Fact]
        public void Find_ShouldThrowArgumentException_WhenInputIsEmpty() {
            int[] input = Array.Empty<int>();
            Assert.Throws<ArgumentException>(()=>FindLargestElement.Find(input));
        }

        [Theory]
        [InlineData(new[] {50},50)]
        [InlineData(new[] {10,20 },20)]
        [InlineData(new[] { 10, 25, 7, 90, 15 },90)]
        [InlineData(new[] { -10, -25, -7, -90 }, -7)]
        [InlineData(new[] { -10, 25, -7, 90, -15 }, 90)]
        [InlineData(new[] { 5, 5, 5, 5 }, 5)]
        [InlineData(new[] { 100, 20, 30, 40 }, 100)]
        [InlineData(new[] { 10, 20, 30, 100 }, 100)]
        [InlineData(new[] { int.MinValue, -10, -100 }, -10)]
        [InlineData(new[] { 10, int.MaxValue, 500 }, int.MaxValue)]
        [InlineData(new[] { 10, 90, 20, 90, 30 }, 90)]
        public void Find_ShouldReturnCorrectLargestElement(int[] input, int expected)
        {
            int result = FindLargestElement.Find(input);
            Assert.Equal(expected, result);
        }
    }
}
