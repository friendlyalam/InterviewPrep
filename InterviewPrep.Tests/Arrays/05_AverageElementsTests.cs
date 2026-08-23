
using InterviewPrep.DSA.DataStructures._01_Array.Problems._05_AverageArrayElements.Solutions;

namespace InterviewPrep.Tests.Arrays
{
    public class AverageElementsTests
    {
        [Fact]
        public void CalculateAverage_ShouldThrowArgumentNullException_WhenInputIsNull()
        {
            int[] input = null!;
            Assert.Throws<ArgumentNullException>(() => FindAverage.CalculateAverage(input));
        }

        [Fact]
        public void CalculateAverage_ShouldThrowArgumentException_WhenInputIsEmpty()
        {
            int[] input = Array.Empty<int>();
            Assert.Throws<ArgumentException>(() => FindAverage.CalculateAverage(input));
        }

        [Theory]
        [InlineData(new[] { 10 }, 10)]
        [InlineData(new[] { 10, 20, 30, 40 }, 25)]
        [InlineData(new[] { 5, 10, 15 }, 10)]
        [InlineData(new[] { -10, 20, 30 }, 13.333333333333334)]
        [InlineData(new[] { -10, -20, -30 }, -20)]
        [InlineData(new[] { 0, 0, 0 }, 0)]
        [InlineData(new[] { -5, 0, 5 }, 0)]

        public void CalculateAverage_ShouldReturnCorrectAverage(int[] input, double expected)
        {
            double result = FindAverage.CalculateAverage(input);
            Assert.Equal(expected, result,10);
        }

    }
}
