namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._33_MaximumSumSubarrayAny.Solutions
{
    //Solution 2 — Optimal: Dutch National Flag Algorithm
    public static class SortColorsOptimal
    {
        public static int[] Sort(int[] nums)
        {
            if (nums is null)
                throw new ArgumentNullException(nameof(nums));

            if (nums.Length == 0)
                throw new ArgumentException(
                    "nums cannot be empty.",
                    nameof(nums));

            int low = 0;
            int mid = 0;
            int high = nums.Length - 1;

            while (mid <= high)
            {
                if (nums[mid] == 0)
                {
                    // Move 0 into the left region.
                    (nums[low], nums[mid]) =
                        (nums[mid], nums[low]);

                    low++;
                    mid++;
                }
                else if (nums[mid] == 1)
                {
                    // 1 belongs to the middle region.
                    mid++;
                }
                else if (nums[mid] == 2)
                {
                    // Move 2 into the right region.
                    (nums[mid], nums[high]) =
                        (nums[high], nums[mid]);

                    high--;

                    // Do NOT increment mid.
                    // The swapped value still needs to be checked.
                }
                else
                {
                    throw new ArgumentException(
                        "Array can contain only 0, 1 and 2.",
                        nameof(nums));
                }
            }

            return nums;
        }
    }
}
