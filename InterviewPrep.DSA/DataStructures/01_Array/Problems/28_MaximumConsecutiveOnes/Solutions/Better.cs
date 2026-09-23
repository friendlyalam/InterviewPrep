namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._28_MaximumConsecutiveOnes.Solutions
{
    public static class FindMaximumConsecutiveOnesBetter
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

            foreach (int number in nums)
            {
                if (number == 1)
                {
                    currentCount++;

                    maxCount = Math.Max(maxCount, currentCount);
                }
                else
                {
                    currentCount = 0;
                }
            }

            return maxCount;
        }
    }
}
