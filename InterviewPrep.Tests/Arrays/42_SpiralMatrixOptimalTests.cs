using InterviewPrep.DSA.DataStructures._01_Array.Problems._42_SpiralMatrix.Solutions;

namespace InterviewPrep.Tests.Arrays
{

    public class SpiralMatrixOptimalTests
    {
        [Theory]
        [MemberData(nameof(SpiralTestData))]
        public void Traverse_ShouldReturnExpectedSpiralOrder(
            int[][] matrix,
            int[] expected)
        {
            IList<int> result =
                SpiralMatrixOptimal.Traverse(matrix);

            Assert.Equal(expected, result);
        }

        public static IEnumerable<object[]> SpiralTestData()
        {
            yield return new object[]
            {
            new[]
            {
                new[] { 1, 2, 3 },
                new[] { 4, 5, 6 },
                new[] { 7, 8, 9 }
            },
            new[]
            {
                1, 2, 3, 6, 9, 8, 7, 4, 5
            }
            };

            yield return new object[]
            {
            new[]
            {
                new[] { 1, 2, 3, 4 },
                new[] { 5, 6, 7, 8 },
                new[] { 9, 10, 11, 12 }
            },
            new[]
            {
                1, 2, 3, 4, 8, 12, 11, 10, 9, 5, 6, 7
            }
            };

            yield return new object[]
            {
            new[]
            {
                new[] { 1, 2, 3, 4 }
            },
            new[]
            {
                1, 2, 3, 4
            }
            };

            yield return new object[]
            {
            new[]
            {
                new[] { 1 },
                new[] { 2 },
                new[] { 3 },
                new[] { 4 }
            },
            new[]
            {
                1, 2, 3, 4
            }
            };

            yield return new object[]
            {
            new[]
            {
                new[] { 1 }
            },
            new[]
            {
                1
            }
            };

            yield return new object[]
            {
            new[]
            {
                new[] { 1, 2 },
                new[] { 3, 4 }
            },
            new[]
            {
                1, 2, 4, 3
            }
            };

            yield return new object[]
            {
            new[]
            {
                new[] { 1, 2, 3 },
                new[] { 4, 5, 6 }
            },
            new[]
            {
                1, 2, 3, 6, 5, 4
            }
            };

            yield return new object[]
            {
            new[]
            {
                new[] { 1, 2 },
                new[] { 3, 4 },
                new[] { 5, 6 },
                new[] { 7, 8 }
            },
            new[]
            {
                1, 2, 4, 6, 8, 7, 5, 3
            }
            };

            yield return new object[]
            {
            new[]
            {
                new[] { -1, -2, -3 },
                new[] { -4, -5, -6 },
                new[] { -7, -8, -9 }
            },
            new[]
            {
                -1, -2, -3, -6, -9, -8, -7, -4, -5
            }
            };
        }

        [Fact]
        public void Traverse_ShouldThrow_WhenMatrixIsNull()
        {
            Assert.Throws<ArgumentNullException>(
                () => SpiralMatrixOptimal.Traverse(null!));
        }

        [Fact]
        public void Traverse_ShouldThrow_WhenMatrixIsEmpty()
        {
            int[][] matrix = Array.Empty<int[]>();

            Assert.Throws<ArgumentException>(
                () => SpiralMatrixOptimal.Traverse(matrix));
        }

        [Fact]
        public void Traverse_ShouldThrow_WhenFirstRowIsNull()
        {
            int[][] matrix =
            {
            null!
        };

            Assert.Throws<ArgumentException>(
                () => SpiralMatrixOptimal.Traverse(matrix));
        }

        [Fact]
        public void Traverse_ShouldThrow_WhenMatrixHasEmptyRow()
        {
            int[][] matrix =
            {
            Array.Empty<int>()
        };

            Assert.Throws<ArgumentException>(
                () => SpiralMatrixOptimal.Traverse(matrix));
        }

        [Fact]
        public void Traverse_ShouldThrow_WhenMatrixIsNotRectangular()
        {
            int[][] matrix =
            {
            new[] { 1, 2, 3 },
            new[] { 4, 5 }
        };

            Assert.Throws<ArgumentException>(
                () => SpiralMatrixOptimal.Traverse(matrix));
        }

        [Fact]
        public void Traverse_ShouldNotModifyOriginalMatrix()
        {
            int[][] matrix =
            {
            new[] { 1, 2 },
            new[] { 3, 4 }
        };

            int[][] original =
            {
            new[] { 1, 2 },
            new[] { 3, 4 }
        };

            SpiralMatrixOptimal.Traverse(matrix);

            Assert.Equal(original, matrix);
        }
    }
}
