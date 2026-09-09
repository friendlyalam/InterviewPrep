using InterviewPrep.DSA.DataStructures._01_Array.Problems._24_IntersectionOfTwoArrays.Solutions;

namespace InterviewPrep.Tests.Arrays
{
    public class IntersectionOfArrayOptimalTests
    {
        [Theory]
        [InlineData(
            new[] { 1, 2, 2, 1 },
            new[] { 2, 2 },
            new[] { 2, 2 })]

        [InlineData(
            new[] { 4, 9, 5 },
            new[] { 9, 4, 9, 8, 4 },
            new[] { 4, 9 })]

        [InlineData(
            new[] { 1, 2, 3 },
            new[] { 4, 5, 6 },
            new int[] { })]

        [InlineData(
            new[] { 1, 2, 2, 3, 4 },
            new[] { 2, 2 },
            new[] { 2, 2 })]

        [InlineData(
            new[] { 1, 1, 1, 2 },
            new[] { 1, 1, 3 },
            new[] { 1, 1 })]

        [InlineData(
            new[] { 1, 2, 3 },
            new[] { 1, 2, 3 },
            new[] { 1, 2, 3 })]

        [InlineData(
            new[] { 5 },
            new[] { 5 },
            new[] { 5 })]

        public void Intersect_ShouldReturnCommonElementsWithCorrectFrequency(
            int[] nums1,
            int[] nums2,
            int[] expected)
        {
            // Arrange
            int[] input1 = (int[])nums1.Clone();
            int[] input2 = (int[])nums2.Clone();

            // Act
            int[] result =
                IntersectionOfArrayOptimal.Intersect(input1, input2);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Intersect_NullNums1_ShouldThrowArgumentNullException()
        {
            // Arrange
            int[] nums1 = null!;
            int[] nums2 = { 1, 2 };

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                IntersectionOfArrayOptimal.Intersect(nums1, nums2));
        }

        [Fact]
        public void Intersect_NullNums2_ShouldThrowArgumentNullException()
        {
            // Arrange
            int[] nums1 = { 1, 2 };
            int[] nums2 = null!;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                IntersectionOfArrayOptimal.Intersect(nums1, nums2));
        }

        [Fact]
        public void Intersect_EmptyNums1_ShouldThrowArgumentException()
        {
            // Arrange
            int[] nums1 = Array.Empty<int>();
            int[] nums2 = { 1, 2 };

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                IntersectionOfArrayOptimal.Intersect(nums1, nums2));
        }

        [Fact]
        public void Intersect_EmptyNums2_ShouldThrowArgumentException()
        {
            // Arrange
            int[] nums1 = { 1, 2 };
            int[] nums2 = Array.Empty<int>();

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                IntersectionOfArrayOptimal.Intersect(nums1, nums2));
        }
    }
}
