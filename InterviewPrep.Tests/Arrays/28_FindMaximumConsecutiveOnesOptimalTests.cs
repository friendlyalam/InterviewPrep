using InterviewPrep.DSA.DataStructures._01_Array.Problems._28_MaximumConsecutiveOnes.Solutions;

namespace InterviewPrep.Tests.Arrays
{
    public class FindMaximumConsecutiveOnesOptimalTests
    {
        [Theory]
        [InlineData(
            new[] { 1, 1, 0, 1, 1, 1 },
            3)]

        [InlineData(
            new[] { 1, 0, 1, 1, 0, 1 },
            2)]

        [InlineData(
            new[] { 0, 0, 0 },
            0)]

        [InlineData(
            new[] { 1, 1, 1, 1 },
            4)]

        [InlineData(
            new[] { 1 },
            1)]

        [InlineData(
            new[] { 0 },
            0)]

        [InlineData(
            new[] { 1, 1, 0, 1 },
            2)]

        [InlineData(
            new[] { 0, 1, 1, 1, 0, 1 },
            3)]

        [InlineData(
            new[] { 1, 0, 1, 1, 1, 0, 1, 1 },
            3)]

        public void Find_ShouldReturnMaximumConsecutiveOnes(
            int[] nums,
            int expected)
        {
            int result =
                FindMaximumConsecutiveOnesOptimal.Find(nums);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Find_ShouldThrowArgumentNullException_WhenArrayIsNull()
        {
            int[] nums = null!;

            Assert.Throws<ArgumentNullException>(
                () => FindMaximumConsecutiveOnesOptimal.Find(nums));
        }

        [Fact]
        public void Find_ShouldThrowArgumentException_WhenArrayIsEmpty()
        {
            int[] nums = Array.Empty<int>();

            Assert.Throws<ArgumentException>(
                () => FindMaximumConsecutiveOnesOptimal.Find(nums));
        }
    }
}
