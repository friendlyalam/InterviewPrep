using InterviewPrep.DSA.DataStructures._01_Array.Problems._50_MaximumSumCircularSubarray.Solutions;

namespace InterviewPrep.Tests.Arrays
{
    public class MaximumCircularSubarrayTests
    {
        public static IEnumerable<object[]> TestCases()
        {
            yield return new object[]
            {
            new[] { 5, -3, 5 },
            10
            };

            yield return new object[]
            {
            new[] { 1, -2, 3, -2 },
            3
            };

            yield return new object[]
            {
            new[] { 5, -3, 5 },
            10
            };

            yield return new object[]
            {
            new[] { -3, -2, -3 },
            -2
            };

            yield return new object[]
            {
            new[] { 1, 2, 3, 4 },
            10
            };

            yield return new object[]
            {
            new[] { -1 },
            -1
            };

            yield return new object[]
            {
            new[] { 3, -1, 2, -1 },
            4
            };
        }

        [Theory]
        [MemberData(nameof(TestCases))]
        public void Better_ShouldReturnExpected(
            int[] nums,
            int expected)
        {
            int result =
                MaximumCircularSubarrayBetter.Find(nums);

            Assert.Equal(expected, result);
        }

        [Theory]
        [MemberData(nameof(TestCases))]
        public void Optimal_ShouldReturnExpected(
            int[] nums,
            int expected)
        {
            int result =
                MaximumCircularSubarrayOptimal.Find(nums);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Better_Null_ShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(
                () => MaximumCircularSubarrayBetter.Find(null!));
        }

        [Fact]
        public void Optimal_Null_ShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(
                () => MaximumCircularSubarrayOptimal.Find(null!));
        }

        [Fact]
        public void Better_Empty_ShouldThrow()
        {
            Assert.Throws<ArgumentException>(
                () => MaximumCircularSubarrayBetter.Find(
                    Array.Empty<int>()));
        }

        [Fact]
        public void Optimal_Empty_ShouldThrow()
        {
            Assert.Throws<ArgumentException>(
                () => MaximumCircularSubarrayOptimal.Find(
                    Array.Empty<int>()));
        }
    }
}