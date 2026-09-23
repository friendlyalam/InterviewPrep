namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._30_LongestSubarrayWithSumK.Solutions
{
    public static class LongestSubArrayWithSumKBetter
    {
        public static int Find(int[] nums, int k)
        {
            if (nums is null)
                throw new ArgumentNullException(nameof(nums));

            if (nums.Length == 0)
                throw new ArgumentException(
                    "nums cannot be empty.",
                    nameof(nums));

            int maxLength = 0;

            // Try every possible starting position.
            for (int start = 0; start < nums.Length; start++)
            {
                int currentSum = 0;

                // Extend the subarray from the current start.
                for (int end = start; end < nums.Length; end++)
                {
                    currentSum += nums[end];

                    if (currentSum == k)
                    {
                        int currentLength = end - start + 1;

                        maxLength = Math.Max(
                            maxLength,
                            currentLength);
                    }
                }
            }

            return maxLength;
        }

    }
}
