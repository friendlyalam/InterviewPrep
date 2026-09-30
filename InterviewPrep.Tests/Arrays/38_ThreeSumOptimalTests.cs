namespace InterviewPrep.Tests.Arrays
{
    using InterviewPrep.DSA.DataStructures._01_Array.Problems._38_3Sum.Solutions;
    using Xunit;

    public class ThreeSumOptimalTests
    {
        [Theory]
        [InlineData(
            new[] { -1, 0, 1, 2, -1, -4 },
            new[] { -1, -1, 2 },
            new[] { -1, 0, 1 })]

        [InlineData(
            new[] { 0, 0, 0 },
            new[] { 0, 0, 0 })]

        public void Find_ShouldReturnExpectedTriplets(
            int[] nums,
            params int[][] expected)
        {
            IList<IList<int>> result =
                ThreeSumOptimal.Find(nums);

            AssertTripletsEqual(expected, result);
        }

        [Fact]
        public void Find_ShouldReturnEmpty_WhenNoTripletExists()
        {
            int[] nums = { 0, 1, 1 };

            IList<IList<int>> result =
                ThreeSumOptimal.Find(nums);

            Assert.Empty(result);
        }

        [Fact]
        public void Find_ShouldHandleDuplicateValues()
        {
            int[] nums = { -2, 0, 0, 2, 2 };

            IList<IList<int>> result =
                ThreeSumOptimal.Find(nums);

            int[][] expected =
            {
            new[] { -2, 0, 2 }
        };

            AssertTripletsEqual(expected, result);
        }

        [Fact]
        public void Find_ShouldThrow_WhenNumsIsNull()
        {
            Assert.Throws<ArgumentNullException>(
                () => ThreeSumOptimal.Find(null!));
        }

        [Fact]
        public void Find_ShouldThrow_WhenArrayHasLessThanThreeElements()
        {
            int[] nums = { 1, 2 };

            Assert.Throws<ArgumentException>(
                () => ThreeSumOptimal.Find(nums));
        }

        private static void AssertTripletsEqual(
            int[][] expected,
            IList<IList<int>> actual)
        {
            var expectedSet = expected
                .Select(x => string.Join(",", x.OrderBy(v => v)))
                .OrderBy(x => x)
                .ToArray();

            var actualSet = actual
                .Select(x => string.Join(",", x.OrderBy(v => v)))
                .OrderBy(x => x)
                .ToArray();

            Assert.Equal(expectedSet, actualSet);
        }
    }
}
