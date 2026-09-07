using InterviewPrep.DSA.DataStructures._01_Array.Problems._22_FindAllDisappearNumber.Solutions;

namespace InterviewPrep.Tests.Arrays
{
    public class FindMissingNumbersOptimalTests
    {
        [Theory]
        [InlineData(
            new[] { 4, 3, 2, 7, 8, 2, 3, 1 },
            new[] { 5, 6 })]

        [InlineData(
            new[] { 1, 1 },
            new[] { 2 })]

        [InlineData(
            new[] { 1, 2, 3, 4, 5 },
            new int[] { })]

        [InlineData(
            new[] { 2, 2, 2, 2 },
            new[] { 1, 3, 4 })]

        [InlineData(
            new[] { 1, 1, 2, 2 },
            new[] { 3, 4 })]

        [InlineData(
            new[] { 1 },
            new int[] { })]

        public void FindDisappearedNumbers_ShouldReturnMissingNumbers(
            int[] nums,
            int[] expected)
        {
            // Arrange
            int[] input = (int[])nums.Clone();

            // Act
            IList<int> result =
                FindMissingNumbersOptimal.FindDisappearedNumbers(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void FindDisappearedNumbers_NullInput_ShouldThrowArgumentNullException()
        {
            // Arrange
            int[] nums = null!;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                FindMissingNumbersOptimal.FindDisappearedNumbers(nums));
        }

        [Fact]
        public void FindDisappearedNumbers_EmptyInput_ShouldThrowArgumentException()
        {
            // Arrange
            int[] nums = Array.Empty<int>();

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                FindMissingNumbersOptimal.FindDisappearedNumbers(nums));
        }
    }
}
