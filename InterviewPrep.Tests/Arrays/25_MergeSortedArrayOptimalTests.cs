using InterviewPrep.DSA.DataStructures._01_Array.Problems._25_MergeSortedArray.Solutions;

namespace InterviewPrep.Tests.Arrays
{
    public class MergeSortedArrayOptimalTests
    {
        [Theory]
        [InlineData(
            new[] { 1, 2, 3, 0, 0, 0 },
            3,
            new[] { 2, 5, 6 },
            3,
            new[] { 1, 2, 2, 3, 5, 6 })]

        [InlineData(
            new[] { 1 },
            1,
            new int[] { },
            0,
            new[] { 1 })]

        [InlineData(
            new[] { 0 },
            0,
            new[] { 1 },
            1,
            new[] { 1 })]

        [InlineData(
            new[] { 1, 2, 3, 0, 0 },
            3,
            new[] { 4, 5 },
            2,
            new[] { 1, 2, 3, 4, 5 })]

        [InlineData(
            new[] { 4, 5, 6, 0, 0, 0 },
            3,
            new[] { 1, 2, 3 },
            3,
            new[] { 1, 2, 3, 4, 5, 6 })]

        [InlineData(
            new[] { 1, 2, 2, 0, 0 },
            3,
            new[] { 2, 2 },
            2,
            new[] { 1, 2, 2, 2, 2 })]

        [InlineData(
            new[] { -5, -2, 0, 0 },
            2,
            new[] { -3, -1 },
            2,
            new[] { -5, -3, -2, -1 })]

        public void Merge_ShouldReturnSortedMergedArray(
            int[] nums1,
            int m,
            int[] nums2,
            int n,
            int[] expected)
        {
            // Arrange
            int[] input1 = (int[])nums1.Clone();
            int[] input2 = (int[])nums2.Clone();

            // Act
            int[] result =
                MergeSortedArrayOptimal.Merge(
                    input1,
                    m,
                    input2,
                    n);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Merge_NullNums1_ShouldThrowArgumentNullException()
        {
            // Arrange
            int[] nums1 = null!;
            int[] nums2 = { 1 };

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                MergeSortedArrayOptimal.Merge(
                    nums1,
                    0,
                    nums2,
                    1));
        }

        [Fact]
        public void Merge_NullNums2_ShouldThrowArgumentNullException()
        {
            // Arrange
            int[] nums1 = { 1, 0 };
            int[] nums2 = null!;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                MergeSortedArrayOptimal.Merge(
                    nums1,
                    1,
                    nums2,
                    1));
        }

        [Fact]
        public void Merge_NegativeM_ShouldThrowArgumentException()
        {
            // Arrange
            int[] nums1 = { 1 };
            int[] nums2 = { 2 };

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                MergeSortedArrayOptimal.Merge(
                    nums1,
                    -1,
                    nums2,
                    1));
        }

        [Fact]
        public void Merge_NegativeN_ShouldThrowArgumentException()
        {
            // Arrange
            int[] nums1 = { 1 };
            int[] nums2 = { 2 };

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                MergeSortedArrayOptimal.Merge(
                    nums1,
                    1,
                    nums2,
                    -1));
        }

        [Fact]
        public void Merge_InvalidNums1Length_ShouldThrowArgumentException()
        {
            // Arrange
            int[] nums1 = { 1, 2, 0 };
            int[] nums2 = { 3, 4 };

            // m + n = 4, but nums1.Length = 3

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                MergeSortedArrayOptimal.Merge(
                    nums1,
                    2,
                    nums2,
                    2));
        }

        [Fact]
        public void Merge_InvalidNums2Length_ShouldThrowArgumentException()
        {
            // Arrange
            int[] nums1 = { 1, 2, 0, 0 };
            int[] nums2 = { 3 };

            // n = 2, but nums2.Length = 1

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                MergeSortedArrayOptimal.Merge(
                    nums1,
                    2,
                    nums2,
                    2));
        }
    }
}
