using InterviewPrep.DSA.DataStructures._01_Array.Problems._02_FindSmallestElement.Solutions;

namespace InterviewPrep.Tests.Arrays
{
    public class _02_FindSmallestElementTests
    {
        [Fact]
        public void Find_ShouldThrowNullException_whenInputIsNull()
        {
            int[] input = null!;
            Assert.Throws<ArgumentNullException>(() => FindSmallestElement.Find(input));
        }

        [Fact]
        public void Find_ShouldReturnException_WhenInputIsEmpty()
        {
            int[] input = Array.Empty<int>();
            Assert.Throws<ArgumentException>(() => FindSmallestElement.Find(input));
        }

        [Theory]
        [InlineData(new[] { 50 }, 50)]
        [InlineData(new[] { 10, 20 }, 10)]
        [InlineData(new[] { 10, 25, 7, 90, 15 }, 7)]
        [InlineData(new[] { -10, -25, -7, -90 }, -90)]
        [InlineData(new[] { -10, 25, -7, 90, -15 }, -15)]
        [InlineData(new[] { 5, 5, 5, 5 }, 5)]
        [InlineData(new[] { 1, 20, 30, 40 }, 1)]
        [InlineData(new[] { 10, 20, 30, 1 }, 1)]
        [InlineData(new[] { int.MinValue, -10, -100 }, int.MinValue)]
        [InlineData(new[] { 10, int.MaxValue, 500 }, 10)]
        [InlineData(new[] { 10, -90, 20, -90, 30 }, -90)]

        public void Find_ReturnValue(int[]input, int expected)
        {
            int result = FindSmallestElement.Find(input);
            Assert.Equal(expected, result);
        }
    }
}
