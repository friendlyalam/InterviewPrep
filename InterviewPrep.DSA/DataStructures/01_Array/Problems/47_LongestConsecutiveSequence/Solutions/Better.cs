namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._47_LongestConsecutiveSequence.Solutions
{

    // Better: Sort + Scan
    public static class LongestConsecutiveSequenceBetter
    {
        public static int Find(int[] nums)
        {
            ValidateInput(nums);

            if (nums.Length == 0)
                return 0;

            Array.Sort(nums);

            int longest = 1;
            int currentLength = 1;

            for (int i = 1; i < nums.Length; i++)
            {
                // Duplicate values do not break the sequence,
                // but they also do not increase its length.
                if (nums[i] == nums[i - 1])
                {
                    continue;
                }

                // Current value continues the sequence.
                if ((long)nums[i] == (long)nums[i - 1] + 1)
                {
                    currentLength++;
                    longest = Math.Max(longest, currentLength);
                }
                else
                {
                    // A gap exists, so start a new sequence.
                    currentLength = 1;
                }
            }

            return longest;
        }

        private static void ValidateInput(int[] nums)
        {
            if (nums is null)
                throw new ArgumentNullException(nameof(nums));
        }
    }
}