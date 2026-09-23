namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._32_MaximumSumSubarray.Solutions
{
    public static class MaximumSumSubarrayOfSizeKPrefix
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

            long[] prefixSum = new long[nums.Length + 1];

            // Build prefix sum.
            // prefixSum[i] contains the sum of elements
            // before index i.
            for (int i = 0; i < nums.Length; i++)
            {
                prefixSum[i + 1] =
                    prefixSum[i] + nums[i];
            }

            long maxSum = long.MinValue;

            // Check every subarray of exactly k elements.
            for (int start = 0;
                 start + k <= nums.Length;
                 start++)
            {
                long currentSum =
                    prefixSum[start + k] - prefixSum[start];

                maxSum = Math.Max(maxSum, currentSum);
            }

            return checked((int)maxSum);
        }
    }
}
