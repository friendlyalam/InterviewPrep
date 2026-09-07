using InterviewPrep.DSA.DataStructures._01_Array.Problems._23_FindTheDuplicateNumber.Solutions;

namespace InterviewPrep.Tests.Arrays
{
    public class FindDuplicateOptimalTests
    {
        [Theory]
        [InlineData(
            new[] { 1, 3, 4, 2, 2 },
            2)]

        [InlineData(
            new[] { 3, 1, 3, 4, 2 },
            3)]

        [InlineData(
            new[] { 1, 1 },
            1)]

        [InlineData(
            new[] { 2, 2, 2, 2, 3 },
            2)]

        [InlineData(
            new[] { 1, 2, 3, 4, 5, 5 },
            5)]

        public void FindDuplicate_ShouldReturnDuplicateNumber(
            int[] nums,
            int expected)
        {
            // Arrange
            int[] input = (int[])nums.Clone();

            // Act
            int result = FindDuplicateOptimal.FindDuplicate(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void FindDuplicate_ShouldNotModifyInputArray()
        {
            // Arrange
            int[] nums = { 1, 3, 4, 2, 2 };
            int[] original = (int[])nums.Clone();

            // Act
            int result = FindDuplicateOptimal.FindDuplicate(nums);

            // Assert
            Assert.Equal(2, result);
            Assert.Equal(original, nums);
        }

        [Fact]
        public void FindDuplicate_NullInput_ShouldThrowArgumentNullException()
        {
            // Arrange
            int[] nums = null!;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                FindDuplicateOptimal.FindDuplicate(nums));
        }

        [Fact]
        public void FindDuplicate_OneElement_ShouldThrowArgumentException()
        {
            // Arrange
            int[] nums = { 1 };

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                FindDuplicateOptimal.FindDuplicate(nums));
        }

        [Fact]
        public void FindDuplicate_EmptyInput_ShouldThrowArgumentException()
        {
            // Arrange
            int[] nums = Array.Empty<int>();

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                FindDuplicateOptimal.FindDuplicate(nums));
        }
    }
}
