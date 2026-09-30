using InterviewPrep.DSA.DataStructures._01_Array.Problems._48_CountInversions.Solutions;

namespace InterviewPrep.Tests.Arrays
{
    public class CountInversionsTests
    {
        public static IEnumerable<object[]> TestCases()
        {
            yield return new object[]
            {
            new[] { 2, 4, 1, 3, 5 },
            3L
            };

            yield return new object[]
            {
            new[] { 5, 4, 3, 2, 1 },
            10L
            };

            yield return new object[]
            {
            new[] { 1, 2, 3, 4, 5 },
            0L
            };

            yield return new object[]
            {
            new[] { 2, 3, 1 },
            2L
            };

            yield return new object[]
            {
            new[] { 1, 1, 1 },
            0L
            };
        }

        [Theory]
        [MemberData(nameof(TestCases))]
        public void Better_ShouldReturnExpected(
            int[] nums,
            long expected)
        {
            long result =
                CountInversionsBetter.Count(nums);

            Assert.Equal(expected, result);
        }

        [Theory]
        [MemberData(nameof(TestCases))]
        public void Optimal_ShouldReturnExpected(
            int[] nums,
            long expected)
        {
            long result =
                CountInversionsOptimal.Count(nums);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Better_Null_ShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(
                () => CountInversionsBetter.Count(null!));
        }

        [Fact]
        public void Optimal_Null_ShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(
                () => CountInversionsOptimal.Count(null!));
        }

        [Fact]
        public void Better_Empty_ShouldThrow()
        {
            Assert.Throws<ArgumentException>(
                () => CountInversionsBetter.Count(Array.Empty<int>()));
        }

        [Fact]
        public void Optimal_Empty_ShouldThrow()
        {
            Assert.Throws<ArgumentException>(
                () => CountInversionsOptimal.Count(Array.Empty<int>()));
        }
    }
}
