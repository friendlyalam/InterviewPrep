
namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._19_Maximum_Subarray.Solutions
{
    public class MaximumSubarrayOptimal
    {

        //Optimal Approach — Kadane's Algorithm
        public static int MaxSubArray(int[] nums)
        {
            if (nums is null)
                throw new ArgumentNullException(nameof(nums));

            if (nums.Length == 0)
                throw new ArgumentException("nums cannot be empty.", nameof(nums));

            // Best subarray sum ending at the first element.
            int currentSum = nums[0];

            // Best subarray sum found so far.
            int maxSum = nums[0];

            // Start from the second element because the first element
            // has already been used to initialize currentSum and maxSum.
            for (int i = 1; i < nums.Length; i++)
            {
                // Decide whether to:
                // 1. Start a new subarray at the current element, or
                // 2. Extend the previous subarray.
                currentSum = Math.Max(nums[i], currentSum + nums[i]);

                // Update the overall maximum if the current subarray
                // has a larger sum.
                maxSum = Math.Max(maxSum, currentSum);
            }

            // Return the maximum sum of any contiguous subarray.
            return maxSum;
        }
    }
}
