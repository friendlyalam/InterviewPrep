
namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._27_MoveEvenNumbersToTheLeft.Solutions
{
    public static class MoveEvenToLeftOptimal
    {
        public static int[] MoveEvenToLeft(int[] nums)
        {
            if (nums is null)
                throw new ArgumentNullException(nameof(nums));

            if (nums.Length == 0)
                throw new ArgumentException(
                    "nums cannot be empty.",
                    nameof(nums));

            int left = 0;
            int right = nums.Length - 1;

            while (left < right)
            {
                // Left side already contains an even number.
                if (nums[left] % 2 == 0)
                {
                    left++;
                }
                // Right side already contains an odd number.
                else if (nums[right] % 2 != 0)
                {
                    right--;
                }
                // Left is odd and right is even, so swap them.
                else
                {
                    (nums[left], nums[right]) =
                        (nums[right], nums[left]);

                    left++;
                    right--;
                }
            }

            return nums;
        }
    }
}
