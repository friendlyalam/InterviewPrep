namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._35_FindFirstLastPosition.Solutions
{
    //Better: Linear Search
    public static class SearchRangeBetter
    {
        public static int[] SearchRange(int[] nums, int target)
        {
            if (nums is null)
                throw new ArgumentNullException(nameof(nums));

            if (nums.Length == 0)
                throw new ArgumentException(
                    "nums cannot be empty.",
                    nameof(nums));

            int firstIndex = -1;
            int lastIndex = -1;

            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] == target)
                {
                    if (firstIndex == -1)
                        firstIndex = i;

                    lastIndex = i;
                }
            }

            return new[] { firstIndex, lastIndex };
        }
    }
}
