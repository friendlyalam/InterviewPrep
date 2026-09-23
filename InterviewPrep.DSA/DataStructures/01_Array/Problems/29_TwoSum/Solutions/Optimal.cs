namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._29_TwoSum.Solutions
{
    public static class TwoSumOptimal
    {
        public static int[] TwoSum(int[] nums, int target)
        {
            if (nums is null)
                throw new ArgumentNullException(nameof(nums));

            if (nums.Length < 2)
                throw new ArgumentException(
                    "nums must contain at least two elements.",
                    nameof(nums));

            Dictionary<int, int> seen = new();

            for (int i = 0; i < nums.Length; i++)
            {
                int needed = target - nums[i];

                // Check whether the required value
                // has already been seen.
                if (seen.TryGetValue(needed, out int previousIndex))
                {
                    return new[] { previousIndex, i };
                }

                // Store the current value and its index.
                seen[nums[i]] = i;
            }

            // The problem guarantees exactly one solution.
            throw new InvalidOperationException(
                "No two sum solution exists.");
        }
    }
}
