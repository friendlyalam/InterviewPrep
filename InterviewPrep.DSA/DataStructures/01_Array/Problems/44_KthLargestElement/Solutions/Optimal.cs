namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._44_KthLargestElement.Solutions
{
    //Optimal: Quickselect
    //Quickselect is based on the partition technique used in Quick Sort.
    //Instead of completely sorting the array, we partition it around a pivot.
    public static class KthLargestElementOptimal
    {
        public static int Find(int[] nums, int k)
        {
            ValidateInput(nums, k);

            int targetIndex = nums.Length - k;

            int left = 0;
            int right = nums.Length - 1;

            while (left <= right)
            {
                // Choose the middle element as pivot.
                int pivotIndex = left + (right - left) / 2;

                // Partition the current range.
                int finalPivotIndex =
                    Partition(nums, left, right, pivotIndex);

                if (finalPivotIndex == targetIndex)
                {
                    return nums[finalPivotIndex];
                }

                if (finalPivotIndex < targetIndex)
                {
                    // Target is on the right.
                    left = finalPivotIndex + 1;
                }
                else
                {
                    // Target is on the left.
                    right = finalPivotIndex - 1;
                }
            }

            // This point is unreachable for valid input.
            throw new InvalidOperationException(
                "Unable to find the kth largest element.");
        }

        private static int Partition(
            int[] nums,
            int left,
            int right,
            int pivotIndex)
        {
            int pivotValue = nums[pivotIndex];

            // Move pivot to the end temporarily.
            (nums[pivotIndex], nums[right]) =
                (nums[right], nums[pivotIndex]);

            int storeIndex = left;

            // Put values smaller than the pivot on the left.
            for (int i = left; i < right; i++)
            {
                if (nums[i] < pivotValue)
                {
                    (nums[i], nums[storeIndex]) =
                        (nums[storeIndex], nums[i]);

                    storeIndex++;
                }
            }

            // Put pivot into its final sorted position.
            (nums[storeIndex], nums[right]) =
                (nums[right], nums[storeIndex]);

            return storeIndex;
        }

        private static void ValidateInput(int[] nums, int k)
        {
            if (nums is null)
                throw new ArgumentNullException(nameof(nums));

            if (nums.Length == 0)
                throw new ArgumentException(
                    "nums cannot be empty.",
                    nameof(nums));

            if (k <= 0 || k > nums.Length)
                throw new ArgumentException(
                    "k must be between 1 and nums.Length.",
                    nameof(k));
        }
    }
}
