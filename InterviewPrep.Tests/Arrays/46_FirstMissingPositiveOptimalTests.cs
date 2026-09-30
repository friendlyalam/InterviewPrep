namespace InterviewPrep.Tests.Arrays
{
    using InterviewPrep.DSA.DataStructures._01_Array.Problems._46_FirstMissingPositive.Solutions;
    using Xunit;

    public class FirstMissingPositiveOptimalTests
    {
        [Theory]
        [InlineData(
            new[] { 1, 2, 0 },
            3)]

        [InlineData(
            new[] { 3, 4, -1, 1 },
            2)]

        [InlineData(
            new[] { 7, 8, 9, 11, 12 },
            1)]

        [InlineData(
            new[] { 1, 2, 3 },
            4)]

        [InlineData(
            new[] { 2, 3, 4 },
            1)]

        [InlineData(
            new[] { 1, 1 },
            2)]

        [InlineData(
            new[] { -1, -2, -3 },
            1)]

        [InlineData(
            new[] { 0, 1, 2 },
            3)]

        [InlineData(
            new[] { 2, 2, 1, 3 },
            4)]

        [InlineData(
            new[] { 5, 4, 3, 2, 1 },
            6)]

        public void Find_ShouldReturnExpectedResult(
            int[] nums,
            int expected)
        {
            int result =
                FirstMissingPositiveOptimal.Find(nums);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Find_ShouldThrowArgumentNullException_WhenNumsIsNull()
        {
            Assert.Throws<ArgumentNullException>(
                () => FirstMissingPositiveOptimal.Find(null!));
        }

        [Fact]
        public void Find_ShouldThrowArgumentException_WhenNumsIsEmpty()
        {
            Assert.Throws<ArgumentException>(
                () => FirstMissingPositiveOptimal.Find(
                    Array.Empty<int>()));
        }
    }
}
