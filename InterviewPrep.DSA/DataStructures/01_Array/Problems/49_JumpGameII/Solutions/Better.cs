namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._49_JumpGameII.Solutions
{

    //  Better: Dynamic Programming
    public static class JumpGameIIBetter
    {
        public static int MinJumps(int[] nums)
        {
            ValidateInput(nums);

            int n = nums.Length;

            if (n == 1)
                return 0;

            int[] jumps = new int[n];

            Array.Fill(jumps, int.MaxValue);

            jumps[0] = 0;

            for (int i = 0; i < n; i++)
            {
                if (jumps[i] == int.MaxValue)
                    continue;

                int farthest =
                    Math.Min(
                        i + nums[i],
                        n - 1);

                for (int next = i + 1;
                     next <= farthest;
                     next++)
                {
                    jumps[next] =
                        Math.Min(
                            jumps[next],
                            jumps[i] + 1);
                }
            }

            return jumps[n - 1];
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