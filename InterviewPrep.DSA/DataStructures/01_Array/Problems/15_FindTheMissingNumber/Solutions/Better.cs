
namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._15_FindTheMissingNumber.Solutions
{
    public class FindMissingNumberBetterApproach
    {
        public int MissingNumber(int[] nums)
        {
            ArgumentNullException.ThrowIfNull(nums);

            if (nums.Length == 0)
            {
                throw new ArgumentException(
                    "Input cannot be empty.",
                    nameof(nums));
            }

            // The array contains n numbers from the range [0, n].
            int n = nums.Length;

            // Calculate the expected sum of numbers from 0 to n.
            long expectedSum = (long)n * (n + 1) / 2;

            // Calculate the actual sum of elements present in the array.
            long actualSum = 0;

            foreach (int num in nums)
            {
                actualSum += num;
            }

            // The difference between expected and actual sum is the missing number.
            return (int)(expectedSum - actualSum);
        }
    }
}
