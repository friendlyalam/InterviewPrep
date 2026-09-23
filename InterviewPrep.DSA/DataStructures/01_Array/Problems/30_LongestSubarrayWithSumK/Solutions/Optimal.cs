namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._30_LongestSubarrayWithSumK.Solutions
{
    //Prefix Sum + Dictionary
    public static class LongestSubArrayWithSumKOptimal
    {
        public static int Find(int[] nums, int k)
        {
            if (nums is null)
                throw new ArgumentNullException(nameof(nums));

            if (nums.Length == 0)
                throw new ArgumentException(
                    "nums cannot be empty.",
                    nameof(nums));

            Dictionary<long, int> firstIndexByPrefixSum = new();

            // Prefix sum 0 exists before the array starts.
            // This allows us to detect subarrays starting at index 0.
            firstIndexByPrefixSum[0] = -1;//The -1 represents the position before the first array element.

            long currentSum = 0;
            int maxLength = 0;

            for (int i = 0; i < nums.Length; i++)
            {
                currentSum += nums[i];

                long requiredPrefixSum = currentSum - k;

                // If requiredPrefixSum was seen before,
                // the elements after that index up to i sum to k.
                if (firstIndexByPrefixSum.TryGetValue(
                    requiredPrefixSum,
                    out int previousIndex))
                {
                    int currentLength = i - previousIndex;

                    maxLength = Math.Max(maxLength,currentLength);
                }

                // Store only the first occurrence.
                // The earliest index gives the longest subarray.
                if (!firstIndexByPrefixSum.ContainsKey(currentSum))
                {
                    firstIndexByPrefixSum[currentSum] = i;
                }
            }

            return maxLength;
        }
    }
}
