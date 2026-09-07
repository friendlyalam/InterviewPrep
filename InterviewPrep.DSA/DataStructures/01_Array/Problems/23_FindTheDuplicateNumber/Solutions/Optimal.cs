
namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._23_FindTheDuplicateNumber.Solutions
{
    public static class FindDuplicateOptimal
    {
        //Optimal Solution — Floyd's Cycle Detection
        public static int FindDuplicate(int[] nums)
        {
            if (nums is null)
                throw new ArgumentNullException(nameof(nums));

            if (nums.Length < 2)
                throw new ArgumentException(
                    "nums must contain at least two elements.",
                    nameof(nums));

            // Phase 1: Find the intersection point inside the cycle.
            int slow = nums[0];
            int fast = nums[0];

            do
            {
                slow = nums[slow];
                fast = nums[nums[fast]];
            }
            while (slow != fast);

            // Phase 2: Find the entrance of the cycle.
            slow = nums[0];

            while (slow != fast)
            {
                slow = nums[slow];
                fast = nums[fast];
            }

            // The cycle entrance represents the duplicate number.
            return slow;
        }
    }
}
