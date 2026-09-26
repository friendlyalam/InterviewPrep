namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._36_FindPeekElement.Solutions
{
    //Optimal: Binary Search
    public static class FindPeakElementOptimal
    {
        public static int FindPeak(int[] nums)
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

                // We are moving downhill.
                // A peak exists at mid or somewhere to the left.
                if (nums[mid] > nums[mid + 1])
                {
                    right = mid;
                }
                else
                {
                    // We are moving uphill.
                    // A peak must exist somewhere to the right.
                    left = mid + 1;
                }
            }

            return left;
        }
    }
}
