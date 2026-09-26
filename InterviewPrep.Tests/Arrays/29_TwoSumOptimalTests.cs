using InterviewPrep.DSA.DataStructures._01_Array.Problems._29_TwoSum.Solutions;

namespace InterviewPrep.Tests.Arrays
{

    public class TwoSumOptimalTests
    {
        [Theory]
        [InlineData(
            new[] { 2, 7, 11, 15 },
            9,
            new[] { 0, 1 })]

        [InlineData(
            new[] { 3, 2, 4 },
            6,
            new[] { 1, 2 })]

        [InlineData(
            new[] { 3, 3 },
            6,
            new[] { 0, 1 })]

        [InlineData(
            new[] { -3, 4, 2 },
            1,
            new[] { 0, 1 })]

        [InlineData(
            new[] { -5, -2, 3 },
            -7,
            new[] { 0, 1 })]

        [InlineData(
            new[] { 1, 5, 3, 7 },
            10,
            new[] { 2, 3 })]

        [InlineData(
            new[] { 4, 6, 10, 2 },
            12,
            new[] { 2, 3 })]

        public void TwoSum_ShouldReturnExpectedIndices(
            int[] nums,
            int target,
            int[] expected)
        {
            int[] result =
                TwoSumOptimal.TwoSum(nums, target);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void TwoSum_ShouldThrowArgumentNullException_WhenArrayIsNull()
        {
            int[] nums = null!;

            Assert.Throws<ArgumentNullException>(
                () => TwoSumOptimal.TwoSum(nums, 9));
        }

        [Fact]
        public void TwoSum_ShouldThrowArgumentException_WhenArrayIsEmpty()
        {
            int[] nums = Array.Empty<int>();

            Assert.Throws<ArgumentException>(
                () => TwoSumOptimal.TwoSum(nums, 9));
        }

        [Fact]
        public void TwoSum_ShouldThrowArgumentException_WhenArrayHasOnlyOneElement()
        {
            int[] nums = { 5 };

            Assert.Throws<ArgumentException>(
                () => TwoSumOptimal.TwoSum(nums, 10));
        }

        [Fact]
        public void TwoSum_ShouldThrowInvalidOperationException_WhenNoSolutionExists()
        {
            int[] nums = { 1, 2, 3 };

            Assert.Throws<InvalidOperationException>(
                () => TwoSumOptimal.TwoSum(nums, 10));
        }
    }
}
