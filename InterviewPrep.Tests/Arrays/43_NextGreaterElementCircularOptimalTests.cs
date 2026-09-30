using InterviewPrep.DSA.DataStructures._01_Array.Problems._43_NextGreaterElementCircular.Solutions;

namespace InterviewPrep.Tests.Arrays
{
    public class NextGreaterElementCircularOptimalTests
    {
        [Theory]
        [InlineData(
            new[] { 1, 2, 1 },
            new[] { 2, -1, 2 })]

        [InlineData(
            new[] { 1, 2, 3, 4, 3 },
            new[] { 2, 3, 4, -1, 4 })]

        [InlineData(
            new[] { 5, 4, 3, 2, 1 },
            new[] { -1, 5, 5, 5, 5 })]

        [InlineData(
            new[] { 3, 3, 3 },
            new[] { -1, -1, -1 })]

        [InlineData(
            new[] { 1, 2, 3 },
            new[] { 2, 3, -1 })]

        [InlineData(
            new[] { -3, -2, -5 },
            new[] { -2, -1, -3 })]

        public void Find_ShouldReturnExpectedResult(
            int[] nums,
            int[] expected)
        {
            int[] result =
                NextGreaterElementCircularOptimal.Find(nums);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Find_ShouldReturnMinusOne_ForSingleElement()
        {
            int[] nums = { 5 };

            int[] result =
                NextGreaterElementCircularOptimal.Find(nums);

            Assert.Equal(new[] { -1 }, result);
        }

        [Fact]
        public void Find_ShouldThrowArgumentNullException_WhenInputIsNull()
        {
            Assert.Throws<ArgumentNullException>(
                () => NextGreaterElementCircularOptimal.Find(null!));
        }

        [Fact]
        public void Find_ShouldThrowArgumentException_WhenInputIsEmpty()
        {
            Assert.Throws<ArgumentException>(
                () => NextGreaterElementCircularOptimal.Find(
                    Array.Empty<int>()));
        }
    }
}
