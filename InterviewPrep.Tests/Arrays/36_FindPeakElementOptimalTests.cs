namespace InterviewPrep.Tests.Arrays
{
    using InterviewPrep.DSA.DataStructures._01_Array.Problems._36_FindPeekElement.Solutions;
    using Xunit;

    public class FindPeakElementOptimalTests
    {
        [Theory]
        [InlineData(new[] { 1, 2, 3, 1 })]
        [InlineData(new[] { 1, 2, 1, 3, 5, 6, 4 })]
        [InlineData(new[] { 1 })]
        [InlineData(new[] { 1, 2 })]
        [InlineData(new[] { 2, 1 })]
        [InlineData(new[] { 1, 2, 3, 4, 5 })]
        [InlineData(new[] { 5, 4, 3, 2, 1 })]
        [InlineData(new[] { 1, 3, 2, 4, 1 })]
        [InlineData(new[] { 2, 1, 3, 2, 5, 4 })]
        [InlineData(new[] { -5, -3, -4 })]
        [InlineData(new[] { -5, -4, -3 })]
        [InlineData(new[] { 10, 5, 8, 3, 7, 2 })]
        public void FindPeak_ShouldReturnValidPeakIndex(int[] nums)
        {
            int result =
                FindPeakElementOptimal.FindPeak(nums);

            Assert.InRange(
                result,
                0,
                nums.Length - 1);

            bool leftIsSmaller =
                result == 0 ||
                nums[result] > nums[result - 1];

            bool rightIsSmaller =
                result == nums.Length - 1 ||
                nums[result] > nums[result + 1];

            Assert.True(leftIsSmaller);
            Assert.True(rightIsSmaller);
        }

        [Fact]
        public void FindPeak_ShouldThrowArgumentNullException_WhenArrayIsNull()
        {
            int[] nums = null!;

            Assert.Throws<ArgumentNullException>(
                () => FindPeakElementOptimal.FindPeak(nums));
        }

        [Fact]
        public void FindPeak_ShouldThrowArgumentException_WhenArrayIsEmpty()
        {
            int[] nums = Array.Empty<int>();

            Assert.Throws<ArgumentException>(
                () => FindPeakElementOptimal.FindPeak(nums));
        }
    }
}
