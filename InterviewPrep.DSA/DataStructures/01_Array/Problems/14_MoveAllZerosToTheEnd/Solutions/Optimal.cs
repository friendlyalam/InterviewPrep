
namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._14_MoveAllZerosToTheEnd.Solutions
{
    public class MoveAllZerosToTheEnd
    {
        public static int[] MoveZeroes(int[] nums)
        {
            ArgumentNullException.ThrowIfNull(nums);

            if (nums.Length == 0)
            {
                throw new ArgumentException(
                    "Input cannot be empty.",
                    nameof(nums));
            }

            int nonZeroIndex = 0;

            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] != 0)
                {
                    // Put the current non-zero element
                    // at the next available non-zero position.
                    int temp = nums[nonZeroIndex];
                    nums[nonZeroIndex] = nums[i];
                    nums[i] = temp;

                    nonZeroIndex++;
                }
            }
            return nums;
        }
    }
}
