

namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._19_Maximum_Subarray.Solutions
{
    public class MaximumSubarrayBetter
    {
        public static int MaxSubArray(int[] nums)
        {
            if (nums is null)
                throw new ArgumentNullException(nameof(nums));

            if (nums.Length == 0)
                throw new ArgumentException("nums cannot be empty.", nameof(nums));

            int maxSum = nums[0];

            // Choose every possible starting position.
            for (int start = 0; start < nums.Length; start++)
            {
                int currentSum = 0;

                // Expand the subarray from the current starting position.
                for (int end = start; end < nums.Length; end++)
                {
                    // Add the current element to the subarray sum.
                    currentSum += nums[end];

                    // Keep track of the largest subarray sum found so far.
                    if (currentSum > maxSum)
                    {
                        maxSum = currentSum;
                    }
                }
            }

            // Return the maximum sum of any contiguous subarray.
            return maxSum;
        }
    }
}
