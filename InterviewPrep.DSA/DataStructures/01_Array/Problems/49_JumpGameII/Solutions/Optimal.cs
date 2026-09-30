namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._49_JumpGameII.Solutions
{
    //Optimal: Merge Sort
    public static class JumpGameIIOptimal
    {
        public static int MinJumps(int[] nums)
        {
            ValidateInput(nums);

            if (nums.Length == 1)
                return 0;

            int jumps = 0;
            int currentEnd = 0;
            int farthest = 0;

            for (int i = 0; i < nums.Length - 1; i++)
            {
                // Find the farthest position reachable
                // from the current jump range.
                farthest = Math.Max(
                    farthest,
                    i + nums[i]);

                // We have reached the end of the
                // current jump range.
                if (i == currentEnd)
                {
                    jumps++;

                    // Start the next range.
                    currentEnd = farthest;

                    // Last index is already reachable.
                    if (currentEnd >= nums.Length - 1)
                        break;
                }
            }

            return jumps;
        }

        private static void ValidateInput(int[] nums)
        {
            if (nums is null)
                throw new ArgumentNullException(nameof(nums));

            if (nums.Length == 0)
                throw new ArgumentException(
                    "nums cannot be empty.",
                    nameof(nums));

            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] < 0)
                    throw new ArgumentException(
                        "Jump values cannot be negative.",
                        nameof(nums));
            }
        }
    }
}
