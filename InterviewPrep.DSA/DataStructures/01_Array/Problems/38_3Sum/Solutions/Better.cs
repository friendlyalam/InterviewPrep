namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._38_3Sum.Solutions
{

    //HashSet + sorting
    public static class ThreeSumBetter
    {
        public static IList<IList<int>> Find(int[] nums)
        {
            if (nums is null)
                throw new ArgumentNullException(nameof(nums));

            if (nums.Length < 3)
                throw new ArgumentException(
                    "nums must contain at least three elements.",
                    nameof(nums));

            Array.Sort(nums);

            List<IList<int>> result = new();

            for (int i = 0; i < nums.Length - 2; i++)
            {
                // Skip duplicate first values.
                if (i > 0 && nums[i] == nums[i - 1])
                    continue;

                HashSet<int> seen = new();

                for (int j = i + 1; j < nums.Length; j++)
                {
                    int required = -(nums[i] + nums[j]);

                    if (seen.Contains(required))
                    {
                        result.Add(new List<int>
                    {
                        nums[i],
                        required,
                        nums[j]
                    });

                        // Skip duplicate second values.
                        while (j + 1 < nums.Length &&
                               nums[j] == nums[j + 1])
                        {
                            j++;
                        }
                    }

                    seen.Add(nums[j]);
                }
            }

            return result;
        }
    }
}
