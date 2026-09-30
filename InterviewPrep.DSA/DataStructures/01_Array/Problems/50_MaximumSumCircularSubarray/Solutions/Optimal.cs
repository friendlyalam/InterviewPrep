namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._50_MaximumSumCircularSubarray.Solutions
{
    //Optimal: Kadane + Minimum Subarray
    public static class MaximumCircularSubarrayOptimal
    {
        public static int Find(int[] nums)
        {
            ValidateInput(nums);

            long totalSum = 0;

            long currentMax = nums[0];
            long maxSum = nums[0];

            long currentMin = nums[0];
            long minSum = nums[0];

            for (int i = 0; i < nums.Length; i++)
            {
                totalSum += nums[i];

                if (i > 0)
                {
                    // Standard Kadane for maximum subarray.
                    currentMax =
                        Math.Max(
                            nums[i],
                            currentMax + nums[i]);

                    maxSum =
                        Math.Max(
                            maxSum,
                            currentMax);

                    // Kadane for minimum subarray.
                    currentMin =
                        Math.Min(
                            nums[i],
                            currentMin + nums[i]);

                    minSum =
                        Math.Min(
                            minSum,
                            currentMin);
                }
            }

            // If every element is negative,
            // totalSum - minSum would represent
            // an empty subarray.
            if (maxSum < 0)
                return checked((int)maxSum);

            long circularSum =
                totalSum - minSum;

            return checked(
                (int)Math.Max(maxSum, circularSum));
        }

        private static void ValidateInput(int[] nums)
        {
            if (nums is null)
                throw new ArgumentNullException(nameof(nums));

            if (nums.Length == 0)
                throw new ArgumentException(
                    "nums cannot be empty.",
                    nameof(nums));
        }
    }
}
