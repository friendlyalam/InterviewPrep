namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._35_FindFirstLastPosition.Solutions
{
    //Optimal: Binary Search
    public static class SearchRangeOptimal
    {
        public static int[] SearchRange(int[] nums, int target)
        {
            if (nums is null)
                throw new ArgumentNullException(nameof(nums));

            if (nums.Length == 0)
                throw new ArgumentException(
                    "nums cannot be empty.",
                    nameof(nums));

            int firstIndex = FindFirstOccurrence(nums, target);

            if (firstIndex == -1)
                return new[] { -1, -1 };

            int lastIndex = FindLastOccurrence(nums, target);

            return new[] { firstIndex, lastIndex };
        }

        private static int FindFirstOccurrence(
            int[] nums,
            int target)
        {
            int left = 0;
            int right = nums.Length - 1;
            int firstIndex = -1;

            while (left <= right)
            {
                int mid = left + (right - left) / 2;

                if (nums[mid] == target)
                {
                    // Target found.
                    // Continue searching on the left
                    // because an earlier occurrence may exist.
                    firstIndex = mid;
                    right = mid - 1;
                }
                else if (nums[mid] < target)
                {
                    // Target must be on the right.
                    left = mid + 1;
                }
                else
                {
                    // Target must be on the left.
                    right = mid - 1;
                }
            }

            return firstIndex;
        }

        private static int FindLastOccurrence(
            int[] nums,
            int target)
        {
            int left = 0;
            int right = nums.Length - 1;
            int lastIndex = -1;

            while (left <= right)
            {
                int mid = left + (right - left) / 2;

                if (nums[mid] == target)
                {
                    // Target found.
                    // Continue searching on the right
                    // because a later occurrence may exist.
                    lastIndex = mid;
                    left = mid + 1;
                }
                else if (nums[mid] < target)
                {
                    // Target must be on the right.
                    left = mid + 1;
                }
                else
                {
                    // Target must be on the left.
                    right = mid - 1;
                }
            }

            return lastIndex;
        }
    }
}
