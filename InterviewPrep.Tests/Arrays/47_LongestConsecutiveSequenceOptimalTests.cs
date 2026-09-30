namespace InterviewPrep.Tests.Arrays
{
    using InterviewPrep.DSA.DataStructures._01_Array.Problems._47_LongestConsecutiveSequence.Solutions;
    using Xunit;

    public class LongestConsecutiveSequenceOptimalTests
    {
        [Theory]
        [InlineData(
            new[] { 100, 4, 200, 1, 3, 2 },
            4)]

        [InlineData(
            new[] { 0, 3, 7, 2, 5, 8, 4, 6, 0, 1 },
            9)]

        [InlineData(
            new[] { 1, 2, 0, 1 },
            3)]

        [InlineData(
            new[] { 1, 2, 3, 4 },
            4)]

        [InlineData(
            new[] { 5, 5, 5, 5 },
            1)]

        [InlineData(
            new[] { -3, -2, -1, 5 },
            3)]

        [InlineData(
            new[] { -2, -1, 0, 1, 2 },
            5)]

        [InlineData(
            new[] { 10 },
            1)]

        [InlineData(
            new[] { 10, 20, 30 },
            1)]

        [InlineData(
            new[] { 9, 1, 4, 7, 3, 2, 6, 5 },
            7)]

        public void Find_ShouldReturnExpectedResult(
            int[] nums,
            int expected)
        {
            int result =
                LongestConsecutiveSequenceOptimal.Find(nums);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Find_ShouldReturnZero_WhenArrayIsEmpty()
        {
            int result =
                LongestConsecutiveSequenceOptimal.Find(
                    Array.Empty<int>());

            Assert.Equal(0, result);
        }

        [Fact]
        public void Find_ShouldThrowArgumentNullException_WhenNumsIsNull()
        {
            Assert.Throws<ArgumentNullException>(
                () => LongestConsecutiveSequenceOptimal.Find(null!));
        }

        [Fact]
        public void Find_ShouldHandleIntegerBoundaries()
        {
            int[] nums =
            {
            int.MinValue,
            int.MinValue + 1,
            int.MaxValue
        };

            int result =
                LongestConsecutiveSequenceOptimal.Find(nums);

            Assert.Equal(2, result);
        }
    }
}
