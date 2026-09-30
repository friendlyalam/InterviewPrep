namespace InterviewPrep.Tests.Arrays
{
    using InterviewPrep.DSA.DataStructures._01_Array.Problems._45_TrappingRainWater.Solutions;
    using Xunit;

    public class TrappingRainWaterOptimalTests
    {
        [Theory]
        [InlineData(
            new[] { 0, 1, 0, 2, 1, 0, 1, 3, 2, 1, 2, 1 },
            6)]

        [InlineData(
            new[] { 4, 2, 0, 3, 2, 5 },
            9)]

        [InlineData(
            new[] { 1, 2, 3, 4 },
            0)]

        [InlineData(
            new[] { 4, 3, 2, 1 },
            0)]

        [InlineData(
            new[] { 3, 3, 3, 3 },
            0)]

        [InlineData(
            new[] { 1, 0, 1 },
            1)]

        [InlineData(
            new[] { 5, 0, 0, 0, 5 },
            15)]

        [InlineData(
            new[] { 2, 0, 2 },
            2)]

        [InlineData(
            new[] { 1 },
            0)]

        [InlineData(
            new[] { 1, 2 },
            0)]

        public void Calculate_ShouldReturnExpectedResult(
            int[] height,
            int expected)
        {
            int result =
                TrappingRainWaterOptimal.Calculate(height);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Calculate_ShouldThrowArgumentNullException_WhenHeightIsNull()
        {
            Assert.Throws<ArgumentNullException>(
                () => TrappingRainWaterOptimal.Calculate(null!));
        }

        [Fact]
        public void Calculate_ShouldThrowArgumentException_WhenHeightIsEmpty()
        {
            Assert.Throws<ArgumentException>(
                () => TrappingRainWaterOptimal.Calculate(
                    Array.Empty<int>()));
        }

        [Fact]
        public void Calculate_ShouldThrowArgumentException_WhenHeightContainsNegativeValue()
        {
            int[] height = { 1, 2, -1, 3 };

            Assert.Throws<ArgumentException>(
                () => TrappingRainWaterOptimal.Calculate(height));
        }
    }
}
