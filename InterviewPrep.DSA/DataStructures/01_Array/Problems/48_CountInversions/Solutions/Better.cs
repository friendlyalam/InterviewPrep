namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._48_CountInversions.Solutions
{

    //  Better: Brute Force
    public static class CountInversionsBetter
    {
        public static long Count(int[] nums)
        {
            ValidateInput(nums);

            long count = 0;

            for (int i = 0; i < nums.Length; i++)
            {
                for (int j = i + 1; j < nums.Length; j++)
                {
                    if (nums[i] > nums[j])
                    {
                        count++;
                    }
                }
            }

            return count;
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