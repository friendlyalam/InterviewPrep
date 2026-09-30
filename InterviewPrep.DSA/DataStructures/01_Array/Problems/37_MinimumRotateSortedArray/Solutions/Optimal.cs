namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._37_MaximumRotateSortedArray.Solutions
{
    //Optimal: Binary Search
    public static class FindMinimumRotatedArrayOptimal
    {
        public static int FindMin(int[] nums)
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
                int mid = left + (right - left) / 2;

                if (nums[mid] > nums[right])
                {
                    // Minimum is strictly to the right of mid.
                    left = mid + 1;
                }
                else
                {
                    // Minimum is at mid or somewhere to the left.
                    right = mid;
                }
            }

            return nums[left];
        }
    }
}
