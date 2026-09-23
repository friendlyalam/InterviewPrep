namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._28_MaximumConsecutiveOnes.Solutions
{
    public static class FindMaximumConsecutiveOnesOptimal
    {
        public static int Find(int[] nums)
        {
            if (nums is null)
                throw new ArgumentNullException(nameof(nums));

            if (nums.Length == 0)
                throw new ArgumentException(
                    "nums cannot be empty.",
                    nameof(nums));

            int currentCount = 0;
            int maxCount = 0;

            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] == 1)
                {
                    currentCount++;

                    if (currentCount > maxCount)
                        maxCount = currentCount;
                }
                else
                {
                    // A zero breaks the consecutive sequence.
                    currentCount = 0;
                }
            }

            return maxCount;
        }
    }
}
