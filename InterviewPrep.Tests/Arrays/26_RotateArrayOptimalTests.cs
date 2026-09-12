
namespace InterviewPrep.Tests.Arrays
{
    using InterviewPrep.DSA.DataStructures._01_Array.Problems._26_RotateRightArray.Solutions;

    public class RotateArrayOptimalTests
    {
        [Theory]
        [InlineData(
            new[] { 1, 2, 3, 4, 5, 6, 7 },
            3,
            new[] { 5, 6, 7, 1, 2, 3, 4 })]

        [InlineData(
            new[] { -1, -100, 3, 99 },
            2,
            new[] { 3, 99, -1, -100 })]

        [InlineData(
            new[] { 1, 2 },
            5,
            new[] { 2, 1 })]

        [InlineData(
            new[] { 1, 2, 3, 4 },
            0,
            new[] { 1, 2, 3, 4 })]

        [InlineData(
            new[] { 1, 2, 3, 4 },
            4,
            new[] { 1, 2, 3, 4 })]

        [InlineData(
            new[] { 1, 1, 2, 2 },
            2,
            new[] { 2, 2, 1, 1 })]

        [InlineData(
            new[] { -5, -2, 0, 3 },
            1,
            new[] { 3, -5, -2, 0 })]

        [InlineData(
            new[] { 10 },
            100,
            new[] { 10 })]

        public void RotateRight_ShouldReturnExpectedResult(
            int[] nums,
            int rightRotateBy,
            int[] expected)
        {
            int[] result =
                RotateArrayOptimal.RotateRight(nums, rightRotateBy);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void RotateRight_ShouldThrowArgumentNullException_WhenArrayIsNull()
        {
            int[] nums = null!;

            Assert.Throws<ArgumentNullException>(
                () => RotateArrayOptimal.RotateRight(nums, 2));
        }

        [Fact]
        public void RotateRight_ShouldThrowArgumentException_WhenArrayIsEmpty()
        {
            int[] nums = Array.Empty<int>();

            Assert.Throws<ArgumentException>(
                () => RotateArrayOptimal.RotateRight(nums, 2));
        }

        [Fact]
        public void RotateRight_ShouldThrowArgumentException_WhenKIsNegative()
        {
            int[] nums = { 1, 2, 3 };

            Assert.Throws<ArgumentException>(
                () => RotateArrayOptimal.RotateRight(nums, -1));
        }

        [Fact]
        public void RotateRight_ShouldModifyTheOriginalArrayInPlace()
        {
            int[] nums = { 1, 2, 3, 4, 5 };

            RotateArrayOptimal.RotateRight(nums, 2);

            Assert.Equal(
                new[] { 4, 5, 1, 2, 3 },
                nums);
        }
    }

}
