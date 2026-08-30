
using InterviewPrep.DSA.DataStructures._01_Array.Problems._14_MoveAllZerosToTheEnd.Solutions;

namespace InterviewPrep.Tests.Arrays
{
    public class MoveZeroesTests
    {
        [Fact]
        public void MoveZeroes_ShouldThrowArgumentNullException_WhenInputIsNull()
        {
            int[] nums = null!;

            Assert.Throws<ArgumentNullException>(
                () => MoveAllZerosToTheEnd.MoveZeroes(nums));
        }

        [Fact]
        public void MoveZeroes_ShouldThrowArgumentException_WhenInputIsEmpty()
        {
            int[] nums = Array.Empty<int>();

            Assert.Throws<ArgumentException>(
                () => MoveAllZerosToTheEnd.MoveZeroes(nums));
        }

        [Theory]
        [InlineData(
            new[] { 0, 1, 0, 3, 12 },
            new[] { 1, 3, 12, 0, 0 })]

        [InlineData(
            new[] { 1, 2, 3 },
            new[] { 1, 2, 3 })]

        [InlineData(
            new[] { 0, 0, 1, 2 },
            new[] { 1, 2, 0, 0 })]

        [InlineData(
            new[] { 1, 0, 2, 0, 3 },
            new[] { 1, 2, 3, 0, 0 })]

        [InlineData(
            new[] { 0 },
            new[] { 0 })]

        [InlineData(
            new[] { 5 },
            new[] { 5 })]

        [InlineData(
            new[] { 0, 0, 0 },
            new[] { 0, 0, 0 })]

        [InlineData(
            new[] { 1, 2, 3, 0, 0 },
            new[] { 1, 2, 3, 0, 0 })]

        [InlineData(
            new[] { -1, 0, -2, 0, 3 },
            new[] { -1, -2, 3, 0, 0 })]

        [InlineData(
            new[] { 5, 5, 0, 5, 0 },
            new[] { 5, 5, 5, 0, 0 })]

        public void MoveZeroes_ShouldMoveAllZerosToTheEnd(
            int[] nums,
            int[] expected)
        {
            MoveAllZerosToTheEnd.MoveZeroes(nums);
            Assert.Equal(expected, nums);
        }
    }
}
