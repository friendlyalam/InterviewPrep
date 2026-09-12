using InterviewPrep.DSA.DataStructures._01_Array.Problems._27_MoveEvenNumbersToTheLeft.Solutions;


namespace InterviewPrep.Tests.Arrays
{

    public class MoveEvenToLeftOptimalTests
    {
        [Theory]
        [InlineData(new[] { 3, 1, 2, 4 })]
        [InlineData(new[] { 1, 2, 3, 4, 5, 6 })]
        [InlineData(new[] { 2, 4, 6 })]
        [InlineData(new[] { 1, 3, 5 })]
        [InlineData(new[] { -5, -2, 0, 3, 4, 7 })]
        [InlineData(new[] { 0, 1, 2, 3, 4 })]
        [InlineData(new[] { 2 })]
        [InlineData(new[] { 1 })]
        public void MoveEvenToLeft_ShouldPlaceAllEvenNumbersBeforeOddNumbers(
            int[] nums)
        {
            int[] result =
                MoveEvenToLeftOptimal.MoveEvenToLeft(nums);

            bool foundOdd = false;

            foreach (int number in result)
            {
                if (number % 2 != 0)
                {
                    foundOdd = true;
                }
                else
                {
                    // Once an odd number has appeared,
                    // no even number should appear afterward.
                    Assert.False(
                        foundOdd,
                        $"Even number {number} appeared after an odd number.");
                }
            }
        }

        [Fact]
        public void MoveEvenToLeft_ShouldThrowArgumentNullException_WhenArrayIsNull()
        {
            int[] nums = null!;

            Assert.Throws<ArgumentNullException>(
                () => MoveEvenToLeftOptimal.MoveEvenToLeft(nums));
        }

        [Fact]
        public void MoveEvenToLeft_ShouldThrowArgumentException_WhenArrayIsEmpty()
        {
            int[] nums = Array.Empty<int>();

            Assert.Throws<ArgumentException>(
                () => MoveEvenToLeftOptimal.MoveEvenToLeft(nums));
        }

        [Fact]
        public void MoveEvenToLeft_ShouldKeepAllElements()
        {
            int[] nums = { 3, 8, 5, 2, 7, 4 };

            int[] original = (int[])nums.Clone();

            int[] result =
                MoveEvenToLeftOptimal.MoveEvenToLeft(nums);

            Assert.Equal(
                original.OrderBy(x => x),
                result.OrderBy(x => x));
        }

        [Fact]
        public void MoveEvenToLeft_ShouldHandleZeroAsEven()
        {
            int[] nums = { 1, 0, 3, 2 };

            int[] result =
                MoveEvenToLeftOptimal.MoveEvenToLeft(nums);

            bool foundOdd = false;

            foreach (int number in result)
            {
                if (number % 2 != 0)
                {
                    foundOdd = true;
                }
                else
                {
                    Assert.False(foundOdd);
                }
            }
        }
    }
}
