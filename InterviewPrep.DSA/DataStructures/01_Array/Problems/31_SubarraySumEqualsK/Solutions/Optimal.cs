namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._31_SubarraySumEqualsK.Solutions
{
    public static class SubarraySumEqualsKOptimal
    {
        public static int Count(int[] nums, int k)
        {
            if (nums is null)
                throw new ArgumentNullException(nameof(nums));

            if (nums.Length == 0)
                throw new ArgumentException(
                    "nums cannot be empty.",
                    nameof(nums));

            // Stores:
            // prefix sum -> number of times it has appeared
            Dictionary<long, int> prefixFrequency = new();

            // Prefix sum 0 exists once before the array starts.
            // This handles subarrays that begin at index 0.
            prefixFrequency[0] = 1;

            long currentSum = 0;//Even though each individual value is an int, the accumulated prefix sum can become large.
            int count = 0;

            for (int i = 0; i < nums.Length; i++)
            {
                currentSum += nums[i];

                long requiredPrefixSum = currentSum - k;

                // Every previous occurrence of requiredPrefixSum
                // creates one valid subarray ending at index i.
                if (prefixFrequency.TryGetValue(
                    requiredPrefixSum,
                    out int frequency))
                {
                    count += frequency;
                }

                // Record the current prefix sum.
                if (prefixFrequency.ContainsKey(currentSum))
                {
                    prefixFrequency[currentSum]++;
                }
                else
                {
                    prefixFrequency[currentSum] = 1;
                }
            }

            return count;
        }
    }
}
