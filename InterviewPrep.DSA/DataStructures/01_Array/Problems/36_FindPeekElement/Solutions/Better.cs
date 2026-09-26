namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._36_FindPeekElement.Solutions
{
    //Better: Linear Search
    //Check whether the left neighbor is smaller.
    //Check whether the right neighbor is smaller.
    //If both conditions are satisfied, return that index.
    public static class FindPeakElementBetter
    {
        public static int FindPeak(int[] nums)
        {
            if (nums is null)
                throw new ArgumentNullException(nameof(nums));

            if (nums.Length == 0)
                throw new ArgumentException(
                    "nums cannot be empty.",
                    nameof(nums));

            for (int i = 0; i < nums.Length; i++)
            {
                bool leftIsSmaller =
                    i == 0 || nums[i] > nums[i - 1];

                bool rightIsSmaller =
                    i == nums.Length - 1 ||
                    nums[i] > nums[i + 1];

                if (leftIsSmaller && rightIsSmaller)
                    return i;
            }

            // A peak is guaranteed to exist according to the problem.
            return -1;
        }
    }
}
