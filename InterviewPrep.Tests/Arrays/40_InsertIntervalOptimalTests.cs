using InterviewPrep.DSA.DataStructures._01_Array.Problems._40_Insert_Interval.Solutions;

namespace InterviewPrep.DSA.Techniques
{
    public class InsertIntervalOptimalTests
    {
        [Theory]
        [MemberData(nameof(InsertTestData))]
        public void Insert_ShouldReturnExpectedResult(
            int[][] intervals,
            int[] newInterval,
            int[][] expected)
        {
            int[][] result =
                InsertIntervalOptimal.Insert(
                    intervals,
                    newInterval);

            Assert.Equal(expected, result);
        }

        public static IEnumerable<object[]> InsertTestData()
        {
            yield return new object[]
            {
            new[]
            {
                new[] { 1, 3 },
                new[] { 6, 9 }
            },
            new[] { 2, 5 },
            new[]
            {
                new[] { 1, 5 },
                new[] { 6, 9 }
            }
            };

            yield return new object[]
            {
            new[]
            {
                new[] { 1, 2 },
                new[] { 3, 5 },
                new[] { 6, 7 },
                new[] { 8, 10 },
                new[] { 12, 16 }
            },
            new[] { 4, 8 },
            new[]
            {
                new[] { 1, 2 },
                new[] { 3, 10 },
                new[] { 12, 16 }
            }
            };

            yield return new object[]
            {
            new[]
            {
                new[] { 5, 7 },
                new[] { 10, 12 }
            },
            new[] { 1, 3 },
            new[]
            {
                new[] { 1, 3 },
                new[] { 5, 7 },
                new[] { 10, 12 }
            }
            };

            yield return new object[]
            {
            new[]
            {
                new[] { 1, 3 },
                new[] { 5, 7 }
            },
            new[] { 10, 12 },
            new[]
            {
                new[] { 1, 3 },
                new[] { 5, 7 },
                new[] { 10, 12 }
            }
            };

            yield return new object[]
            {
            new[]
            {
                new[] { 2, 3 },
                new[] { 5, 7 },
                new[] { 9, 10 }
            },
            new[] { 1, 12 },
            new[]
            {
                new[] { 1, 12 }
            }
            };

            yield return new object[]
            {
            new[]
            {
                new[] { 1, 3 },
                new[] { 6, 9 }
            },
            new[] { 3, 6 },
            new[]
            {
                new[] { 1, 9 }
            }
            };

            yield return new object[]
            {
            new[]
            {
                new[] { -10, -5 },
                new[] { 0, 3 },
                new[] { 7, 10 }
            },
            new[] { -7, 8 },
            new[]
            {
                new[] { -10, 10 }
            }
            };

            yield return new object[]
            {
            Array.Empty<int[]>(),
            new[] { 2, 5 },
            new[]
            {
                new[] { 2, 5 }
            }
            };
        }

        [Fact]
        public void Insert_ShouldThrow_WhenIntervalsIsNull()
        {
            Assert.Throws<ArgumentNullException>(
                () => InsertIntervalOptimal.Insert(
                    null!,
                    new[] { 1, 2 }));
        }

        [Fact]
        public void Insert_ShouldThrow_WhenNewIntervalIsNull()
        {
            int[][] intervals =
            {
            new[] { 1, 3 }
        };

            Assert.Throws<ArgumentNullException>(
                () => InsertIntervalOptimal.Insert(
                    intervals,
                    null!));
        }

        [Fact]
        public void Insert_ShouldThrow_WhenNewIntervalHasWrongLength()
        {
            int[][] intervals =
            {
            new[] { 1, 3 }
        };

            int[] newInterval = { 2, 4, 5 };

            Assert.Throws<ArgumentException>(
                () => InsertIntervalOptimal.Insert(
                    intervals,
                    newInterval));
        }

        [Fact]
        public void Insert_ShouldThrow_WhenNewIntervalStartIsGreaterThanEnd()
        {
            int[][] intervals =
            {
            new[] { 1, 3 }
        };

            int[] newInterval = { 5, 2 };

            Assert.Throws<ArgumentException>(
                () => InsertIntervalOptimal.Insert(
                    intervals,
                    newInterval));
        }

        [Fact]
        public void Insert_ShouldThrow_WhenExistingIntervalIsInvalid()
        {
            int[][] intervals =
            {
            new[] { 5, 2 }
        };

            int[] newInterval = { 1, 3 };

            Assert.Throws<ArgumentException>(
                () => InsertIntervalOptimal.Insert(
                    intervals,
                    newInterval));
        }

        [Fact]
        public void Insert_ShouldThrow_WhenExistingIntervalsAreNotSorted()
        {
            int[][] intervals =
            {
            new[] { 5, 7 },
            new[] { 1, 3 }
        };

            int[] newInterval = { 2, 4 };

            Assert.Throws<ArgumentException>(
                () => InsertIntervalOptimal.Insert(
                    intervals,
                    newInterval));
        }

        [Fact]
        public void Insert_ShouldThrow_WhenExistingIntervalsOverlap()
        {
            int[][] intervals =
            {
            new[] { 1, 5 },
            new[] { 3, 7 }
        };

            int[] newInterval = { 8, 10 };

            Assert.Throws<ArgumentException>(
                () => InsertIntervalOptimal.Insert(
                    intervals,
                    newInterval));
        }
    }
}
