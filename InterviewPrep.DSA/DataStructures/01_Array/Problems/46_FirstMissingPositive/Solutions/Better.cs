namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._46_FirstMissingPositive.Solutions
{

    // Better: sorting
    public static class FirstMissingPositiveBetter
    {
        public static int Find(int[] nums)
        {
            ValidateInput(nums);

            Array.Sort(nums);

            int expected = 1;

            for (int i = 0; i < nums.Length; i++)
            {
                // Ignore negative numbers and zero.
                if (nums[i] < expected)
                    continue;

                // If expected is present, look for the next positive.
                if (nums[i] == expected)
                {
                    expected++;
                }
                // If nums[i] is greater than expected,
                // expected is missing.
                else if (nums[i] > expected)
                {
                    return expected;
                }
            }

            return expected;
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