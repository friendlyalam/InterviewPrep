namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._32_MaximumSumSubarray.Solutions
{
    //Optimal Solution- Sliding window
    public static class MaximumSumSubarrayOfSizeKOptimal
    {
        public static int Find(int[] nums, int k)
        {
            if (nums is null)
                throw new ArgumentNullException(nameof(nums));

            if (nums.Length == 0)
                throw new ArgumentException(
                    "nums cannot be empty.",
                    nameof(nums));

            if (k <= 0)
                throw new ArgumentException(
                    "k must be greater than zero.",
                    nameof(k));

            if (k > nums.Length)
                throw new ArgumentException(
                    "k cannot be greater than the array length.",
                    nameof(k));

            long currentSum = 0;

            // Build the first window of size k.
            for (int i = 0; i < k; i++)
            {
                currentSum += nums[i];
            }

            long maxSum = currentSum;

            // Slide the window.
            for (int i = k; i < nums.Length; i++)
            {
                // Remove the element leaving the window
                // and add the new element entering the window.
                //New Window Value = Old Window Value + Element Entering - Element Leaving
                currentSum = currentSum - nums[i - k] + nums[i];

                maxSum = Math.Max(maxSum, currentSum);
            }

            return checked((int)maxSum);//checked keyword is used to explicitly enable overflow checking for integral-type arithmetic operations and conversions.
        }
    }
}
