namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._34_RotatedSortedArraySearch.Solutions
{

    //Optimal: Modified Binary Search
    //At every step, at least one half of the current range is sorted.
    public static class SearchRotatedArrayOptimal
    {
        public static int Search(int[] nums, int target)
        {
            if (nums is null)
                throw new ArgumentNullException(nameof(nums));

            if (nums.Length == 0)
                throw new ArgumentException(
                    "nums cannot be empty.",
                    nameof(nums));

            int left = 0;
            int right = nums.Length - 1;

            while (left <= right)
            {
                int mid = left + (right - left) / 2;

                // Target found.
                if (nums[mid] == target)
                    return mid;

                // Left half is sorted.
                if (nums[left] <= nums[mid])
                {
                    // Target lies inside the sorted left half.
                    if (nums[left] <= target &&
                        target < nums[mid])
                    {
                        right = mid - 1;
                    }
                    else
                    {
                        // Target must be in the right half.
                        left = mid + 1;
                    }
                }
                // Right half is sorted.
                else
                {
                    // Target lies inside the sorted right half.
                    if (nums[mid] < target &&
                        target <= nums[right])
                    {
                        left = mid + 1;
                    }
                    else
                    {
                        // Target must be in the left half.
                        right = mid - 1;
                    }
                }
            }

            return -1;
        }
    }
}
