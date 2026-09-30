namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._50_MaximumSumCircularSubarray.Solutions
{

    //  Better: Brute Force
    public static class MaximumCircularSubarrayBetter
    {
        public static int Find(int[] nums)
        {
            ValidateInput(nums);

            int n = nums.Length;

            long best = long.MinValue;

            for (int start = 0; start < n; start++)
            {
                long currentSum = 0;

                for (int length = 0; length < n; length++)
                {
                    int index =
                        (start + length) % n;

                    currentSum += nums[index];

                    best = Math.Max(
                        best,
                        currentSum);
                }
            }

            return checked((int)best);
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