using InterviewPrep.DSA.DataStructures._01_Array.Problems._20_ProductOfArrayExceptSelf.Solutions;

namespace InterviewPrep.Tests.Arrays
{
    public class ProductOfArrayOptimalTests
    {
        [Theory]
        [InlineData(
            new int[] { 1, 2, 3, 4 },
            new int[] { 24, 12, 8, 6 })]

        [InlineData(
            new int[] { -1, 1, 0, -3, 3 },
            new int[] { 0, 0, 9, 0, 0 })]

        [InlineData(
            new int[] { 2, 3 },
            new int[] { 3, 2 })]

        [InlineData(
            new int[] { 5 },
            new int[] { 1 })]

        [InlineData(
            new int[] { 1, 2, 3 },
            new int[] { 6, 3, 2 })]

        [InlineData(
            new int[] { 1, 2, 0, 4 },
            new int[] { 0, 0, 8, 0 })]

        [InlineData(
            new int[] { 1, 0, 0, 4 },
            new int[] { 0, 0, 0, 0 })]

        [InlineData(
            new int[] { -1, 2, 3 },
            new int[] { 6, -3, -2 })]

        [InlineData(
            new int[] { -2, -3, -4 },
            new int[] { 12, 8, 6 })]
        public void ProductArray_ShouldReturnCorrectProduct(
            int[] nums,
            int[] expected)
        {
            // Act
            int[] result = ProductOfArrayOptimal.ProductArray(nums);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ProductArray_ShouldThrowArgumentNullException_WhenArrayIsNull()
        {
            // Arrange
            int[] nums = null!;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(
                () => ProductOfArrayOptimal.ProductArray(nums));
        }

        [Fact]
        public void ProductArray_ShouldThrowArgumentException_WhenArrayIsEmpty()
        {
            // Arrange
            int[] nums = Array.Empty<int>();

            // Act & Assert
            Assert.Throws<ArgumentException>(
                () => ProductOfArrayOptimal.ProductArray(nums));
        }
    }

}
