namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._31_SubarraySumEqualsK.Solutions
{
    public static class SubarraySumEqualsKBetter
    {
        public static int Count(int[] nums, int k)
        {
            if (nums is null)
                throw new ArgumentNullException(nameof(nums));

            if (nums.Length == 0)
                throw new ArgumentException(
                    "nums cannot be empty.",
                    nameof(nums));

            int count = 0;

            // Choose every possible starting position.
            for (int start = 0; start < nums.Length; start++)
            {
                long currentSum = 0;

                // Extend the subarray from the current position.
                for (int end = start; end < nums.Length; end++)
                {
                    currentSum += nums[end];

                    if (currentSum == k)
                    {
                        count++;
                    }
                }
            }

            return count;
        }
    }
}
