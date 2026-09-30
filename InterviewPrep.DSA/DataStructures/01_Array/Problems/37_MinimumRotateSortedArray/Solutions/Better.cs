namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._37_MaximumRotateSortedArray.Solutions
{
    //Better: Linear Search
    public static class FindMinimumRotatedArrayBetter
    {
        public static int FindMin(int[] nums)
        {
            if (nums is null)
                throw new ArgumentNullException(nameof(nums));

            if (nums.Length == 0)
                throw new ArgumentException(
                    "nums cannot be empty.",
                    nameof(nums));

            int minimum = nums[0];

            for (int i = 1; i < nums.Length; i++)
            {
                if (nums[i] < minimum)
                    minimum = nums[i];
            }

            return minimum;
        }
    }
}
