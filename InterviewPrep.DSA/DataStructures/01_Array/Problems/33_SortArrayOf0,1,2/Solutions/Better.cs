namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._33_MaximumSumSubarrayAny.Solutions
{
    //Solution 1 — Better: Counting
    public static class SortColorsBetter
    {
        public static int[] Sort(int[] nums)
        {
            if (nums is null)
                throw new ArgumentNullException(nameof(nums));

            if (nums.Length == 0)
                throw new ArgumentException(
                    "nums cannot be empty.",
                    nameof(nums));

            int zeroCount = 0;
            int oneCount = 0;
            int twoCount = 0;

            // Count 0s, 1s and 2s.
            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] == 0)
                    zeroCount++;
                else if (nums[i] == 1)
                    oneCount++;
                else if (nums[i] == 2)
                    twoCount++;
                else
                    throw new ArgumentException(
                        "Array can contain only 0, 1 and 2.",
                        nameof(nums));
            }

            int index = 0;

            // Place all 0s.
            for (int i = 0; i < zeroCount; i++)
                nums[index++] = 0;

            // Place all 1s.
            for (int i = 0; i < oneCount; i++)
                nums[index++] = 1;

            // Place all 2s.
            for (int i = 0; i < twoCount; i++)
                nums[index++] = 2;

            return nums;
        }
    }
}
