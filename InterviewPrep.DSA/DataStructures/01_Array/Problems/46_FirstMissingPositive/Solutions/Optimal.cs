namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._46_FirstMissingPositive.Solutions
{
    //Optimal: Cyclic Placement
    public static class FirstMissingPositiveOptimal
    {
        public static int Find(int[] nums)
        {
            ValidateInput(nums);

            int n = nums.Length;

            int i = 0;

            while (i < n)
            {
                int correctIndex = nums[i] - 1;

                // Place nums[i] into its correct position when:
                // 1. It is a valid positive number.
                // 2. Its target index is inside the array.
                // 3. The target position does not already contain
                //    the same value.
                if (nums[i] >= 1 &&
                    nums[i] <= n &&
                    nums[i] != nums[correctIndex])
                {
                    (nums[i], nums[correctIndex]) =
                        (nums[correctIndex], nums[i]);
                }
                else
                {
                    i++;
                }
            }

            // Find the first position containing the wrong value.
            for (i = 0; i < n; i++)
            {
                if (nums[i] != i + 1)
                    return i + 1;
            }

            // All values 1..n are present.
            return n + 1;
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
