namespace InterviewPrep.Tests.Arrays
{
    using InterviewPrep.DSA.DataStructures._01_Array.Problems._44_KthLargestElement.Solutions;
    using Xunit;

    public class KthLargestElementOptimalTests
    {
        [Theory]
        [InlineData(
            new[] { 3, 2, 1, 5, 6, 4 },
            2,
            5)]

        [InlineData(
            new[] { 3, 2, 3, 1, 2, 4, 5, 5, 6 },
            4,
            4)]

        [InlineData(
            new[] { 3, 1, 5 },
            1,
            5)]

        [InlineData(
            new[] { 3, 1, 5 },
            3,
            1)]

        [InlineData(
            new[] { -1, -5, -2, -3 },
            2,
            -2)]

        [InlineData(
            new[] { 7, 7, 7, 7 },
            3,
            7)]

        [InlineData(
            new[] { 10 },
            1,
            10)]

        public void Find_ShouldReturnExpectedResult(
            int[] nums,
            int k,
            int expected)
        {
            int result =
                KthLargestElementOptimal.Find(nums, k);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Find_ShouldThrowArgumentNullException_WhenNumsIsNull()
        {
            Assert.Throws<ArgumentNullException>(
                () => KthLargestElementOptimal.Find(null!, 1));
        }

        [Fact]
        public void Find_ShouldThrowArgumentException_WhenNumsIsEmpty()
        {
            Assert.Throws<ArgumentException>(
                () => KthLargestElementOptimal.Find(
                    Array.Empty<int>(),
                    1));
        }

        [Fact]
        public void Find_ShouldThrowArgumentException_WhenKIsZero()
        {
            int[] nums = { 1, 2, 3 };

            Assert.Throws<ArgumentException>(
                () => KthLargestElementOptimal.Find(nums, 0));
        }

        [Fact]
        public void Find_ShouldThrowArgumentException_WhenKIsGreaterThanArrayLength()
        {
            int[] nums = { 1, 2, 3 };

            Assert.Throws<ArgumentException>(
                () => KthLargestElementOptimal.Find(nums, 4));
        }
    }
}
