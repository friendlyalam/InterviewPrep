using InterviewPrep.DSA.DataStructures._01_Array.Problems._39_MergeOverlappingIntervals.Solutions;

namespace InterviewPrep.Tests.Arrays
{
    public class MergeIntervalsOptimalTests
    {
        [Theory]
        [MemberData(nameof(MergeTestData))]
        public void Merge_ShouldReturnExpectedResult(
            int[][] intervals,
            int[][] expected)
        {
            int[][] result =
                MergeIntervalsOptimal.Merge(intervals);

            Assert.Equal(
                expected,
                result);
        }

        public static IEnumerable<object[]> MergeTestData()
        {
            yield return new object[]
            {
            new[]
            {
                new[] { 1, 3 },
                new[] { 2, 6 },
                new[] { 8, 10 },
                new[] { 15, 18 }
            },
            new[]
            {
                new[] { 1, 6 },
                new[] { 8, 10 },
                new[] { 15, 18 }
            }
            };

            yield return new object[]
            {
            new[]
            {
                new[] { 1, 4 },
                new[] { 4, 5 }
            },
            new[]
            {
                new[] { 1, 5 }
            }
            };

            yield return new object[]
            {
            new[]
            {
                new[] { 1, 2 },
                new[] { 3, 4 },
                new[] { 5, 6 }
            },
            new[]
            {
                new[] { 1, 2 },
                new[] { 3, 4 },
                new[] { 5, 6 }
            }
            };

            yield return new object[]
            {
            new[]
            {
                new[] { 5, 7 },
                new[] { 1, 3 },
                new[] { 2, 6 }
            },
            new[]
            {
                new[] { 1, 7 }
            }
            };

            yield return new object[]
            {
            new[]
            {
                new[] { 1, 10 },
                new[] { 2, 3 },
                new[] { 4, 5 },
                new[] { 6, 8 }
            },
            new[]
            {
                new[] { 1, 10 }
            }
            };

            yield return new object[]
            {
            new[]
            {
                new[] { -10, -5 },
                new[] { -7, -2 },
                new[] { 1, 3 }
            },
            new[]
            {
                new[] { -10, -2 },
                new[] { 1, 3 }
            }
            };
        }

        [Fact]
        public void Merge_ShouldThrow_WhenIntervalsIsNull()
        {
            Assert.Throws<ArgumentNullException>(
                () => MergeIntervalsOptimal.Merge(null!));
        }

        [Fact]
        public void Merge_ShouldThrow_WhenIntervalsIsEmpty()
        {
            int[][] intervals = Array.Empty<int[]>();

            Assert.Throws<ArgumentException>(
                () => MergeIntervalsOptimal.Merge(intervals));
        }

        [Fact]
        public void Merge_ShouldThrow_WhenIntervalIsNull()
        {
            int[][] intervals =
            {
            new[] { 1, 3 },
            null!
        };

            Assert.Throws<ArgumentException>(
                () => MergeIntervalsOptimal.Merge(intervals));
        }

        [Fact]
        public void Merge_ShouldThrow_WhenIntervalDoesNotContainTwoValues()
        {
            int[][] intervals =
            {
            new[] { 1, 2, 3 }
        };

            Assert.Throws<ArgumentException>(
                () => MergeIntervalsOptimal.Merge(intervals));
        }

        [Fact]
        public void Merge_ShouldThrow_WhenStartIsGreaterThanEnd()
        {
            int[][] intervals =
            {
            new[] { 5, 2 }
        };

            Assert.Throws<ArgumentException>(
                () => MergeIntervalsOptimal.Merge(intervals));
        }
    }
}
