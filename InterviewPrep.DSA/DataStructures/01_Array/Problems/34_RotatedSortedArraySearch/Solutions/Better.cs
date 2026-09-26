namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._34_RotatedSortedArraySearch.Solutions
{
    //Better: Linear Search
    //The simplest approach is to check every element.
    public static class SearchRotatedArrayBetter
    {
        public static int Search(int[] nums, int target)
        {
            if (nums is null)
                throw new ArgumentNullException(nameof(nums));

            if (nums.Length == 0)
                throw new ArgumentException(
                    "nums cannot be empty.",
                    nameof(nums));

            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] == target)
                    return i;
            }

            return -1;
        }
    }
}
