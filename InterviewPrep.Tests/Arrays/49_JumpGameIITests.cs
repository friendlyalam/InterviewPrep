using InterviewPrep.DSA.DataStructures._01_Array.Problems._49_JumpGameII.Solutions;

namespace InterviewPrep.Tests.Arrays
{
    public class JumpGameIITests
    {
        public static IEnumerable<object[]> TestCases()
        {
            yield return new object[]
            {
            new[] { 2, 3, 1, 1, 4 },
            2
            };

            yield return new object[]
            {
            new[] { 2, 3, 0, 1, 4 },
            2
            };

            yield return new object[]
            {
            new[] { 1, 1, 1, 1 },
            3
            };

            yield return new object[]
            {
            new[] { 3, 2, 1, 0, 4 },
            2
            };

            yield return new object[]
            {
            new[] { 1 },
            0
            };
        }

        [Theory]
        [MemberData(nameof(TestCases))]
        public void Better_ShouldReturnExpected(
            int[] nums,
            int expected)
        {
            int result =
                JumpGameIIBetter.MinJumps(nums);

            Assert.Equal(expected, result);
        }

        [Theory]
        [MemberData(nameof(TestCases))]
        public void Optimal_ShouldReturnExpected(
            int[] nums,
            int expected)
        {
            int result =
                JumpGameIIOptimal.MinJumps(nums);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Better_Null_ShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(
                () => JumpGameIIBetter.MinJumps(null!));
        }

        [Fact]
        public void Optimal_Null_ShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(
                () => JumpGameIIOptimal.MinJumps(null!));
        }

        [Fact]
        public void Better_Empty_ShouldThrow()
        {
            Assert.Throws<ArgumentException>(
                () => JumpGameIIBetter.MinJumps(
                    Array.Empty<int>()));
        }

        [Fact]
        public void Optimal_Empty_ShouldThrow()
        {
            Assert.Throws<ArgumentException>(
                () => JumpGameIIOptimal.MinJumps(
                    Array.Empty<int>()));
        }

        [Fact]
        public void Optimal_NegativeValue_ShouldThrow()
        {
            Assert.Throws<ArgumentException>(
                () => JumpGameIIOptimal.MinJumps(
                    new[] { 2, -1, 3 }));
        }
    }
}
