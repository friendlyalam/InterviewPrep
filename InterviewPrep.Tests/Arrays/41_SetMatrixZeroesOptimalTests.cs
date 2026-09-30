using InterviewPrep.DSA.DataStructures._01_Array.Problems._41_SetMatrixZeroes.Solutions;

namespace InterviewPrep.Tests.Arrays
{
    public class SetMatrixZeroesOptimalTests
    {
        [Theory]
        [MemberData(nameof(MatrixTestData))]
        public void SetZeroes_ShouldModifyMatrixCorrectly(
            int[][] matrix,
            int[][] expected)
        {
            SetMatrixZeroesOptimal.SetZeroes(matrix);

            Assert.Equal(expected, matrix);
        }

        public static IEnumerable<object[]> MatrixTestData()
        {
            yield return new object[]
            {
            new[]
            {
                new[] { 1, 1, 1 },
                new[] { 1, 0, 1 },
                new[] { 1, 1, 1 }
            },
            new[]
            {
                new[] { 1, 0, 1 },
                new[] { 0, 0, 0 },
                new[] { 1, 0, 1 }
            }
            };

            yield return new object[]
            {
            new[]
            {
                new[] { 0, 1, 2, 0 },
                new[] { 3, 4, 5, 2 },
                new[] { 1, 3, 1, 5 }
            },
            new[]
            {
                new[] { 0, 0, 0, 0 },
                new[] { 0, 4, 5, 0 },
                new[] { 0, 3, 1, 0 }
            }
            };

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
                new[] { 1, 2, 3 },
                new[] { 4, 5, 6 },
                new[] { 7, 8, 9 }
            }
            };

            yield return new object[]
            {
            new[]
            {
                new[] { 0, 1, 2 },
                new[] { 3, 4, 5 },
                new[] { 6, 7, 8 }
            },
            new[]
            {
                new[] { 0, 0, 0 },
                new[] { 0, 4, 5 },
                new[] { 0, 7, 8 }
            }
            };

            yield return new object[]
            {
            new[]
            {
                new[] { 1, 2, 3 },
                new[] { 4, 5, 6 },
                new[] { 0, 8, 9 }
            },
            new[]
            {
                new[] { 0, 2, 3 },
                new[] { 0, 5, 6 },
                new[] { 0, 0, 0 }
            }
            };

            yield return new object[]
            {
            new[]
            {
                new[] { 0, 1 },
                new[] { 2, 3 }
            },
            new[]
            {
                new[] { 0, 0 },
                new[] { 0, 3 }
            }
            };

            yield return new object[]
            {
            new[]
            {
                new[] { 1, 0 },
                new[] { 2, 3 }
            },
            new[]
            {
                new[] { 0, 0 },
                new[] { 2, 0 }
            }
            };

            yield return new object[]
            {
            new[]
            {
                new[] { 1, 2 },
                new[] { 3, 4 },
                new[] { 5, 0 }
            },
            new[]
            {
                new[] { 1, 0 },
                new[] { 3, 0 },
                new[] { 0, 0 }
            }
            };

            yield return new object[]
            {
            new[]
            {
                new[] { 0, 1, 2 },
                new[] { 3, 4, 5 },
                new[] { 6, 7, 0 }
            },
            new[]
            {
                new[] { 0, 0, 0 },
                new[] { 0, 4, 0 },
                new[] { 0, 0, 0 }
            }
            };
        }

        [Fact]
        public void SetZeroes_ShouldThrow_WhenMatrixIsNull()
        {
            Assert.Throws<ArgumentNullException>(
                () => SetMatrixZeroesOptimal.SetZeroes(null!));
        }

        [Fact]
        public void SetZeroes_ShouldThrow_WhenMatrixIsEmpty()
        {
            int[][] matrix = Array.Empty<int[]>();

            Assert.Throws<ArgumentException>(
                () => SetMatrixZeroesOptimal.SetZeroes(matrix));
        }

        [Fact]
        public void SetZeroes_ShouldThrow_WhenMatrixHasEmptyRow()
        {
            int[][] matrix =
            {
            Array.Empty<int>()
        };

            Assert.Throws<ArgumentException>(
                () => SetMatrixZeroesOptimal.SetZeroes(matrix));
        }

        [Fact]
        public void SetZeroes_ShouldThrow_WhenRowIsNull()
        {
            int[][] matrix =
            {
            new[] { 1, 2 },
            null!
        };

            Assert.Throws<ArgumentException>(
                () => SetMatrixZeroesOptimal.SetZeroes(matrix));
        }

        [Fact]
        public void SetZeroes_ShouldThrow_WhenMatrixIsNotRectangular()
        {
            int[][] matrix =
            {
            new[] { 1, 2, 3 },
            new[] { 4, 5 }
        };

            Assert.Throws<ArgumentException>(
                () => SetMatrixZeroesOptimal.SetZeroes(matrix));
        }
    }
}
