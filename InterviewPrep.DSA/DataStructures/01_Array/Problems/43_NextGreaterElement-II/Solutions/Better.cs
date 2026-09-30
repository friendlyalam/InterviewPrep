namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._43_NextGreaterElementCircular.Solutions
{

    //Approach: For every element, scan the next n - 1 positions.
    public static class NextGreaterElementCircularBetter
    {
        public static int[] Find(int[] nums)
        {
            if (nums is null)
                throw new ArgumentNullException(nameof(nums));

            if (nums.Length == 0)
                throw new ArgumentException(
                    "nums cannot be empty.",
                    nameof(nums));

            int n = nums.Length;
            int[] result = new int[n];

            Array.Fill(result, -1);

            for (int i = 0; i < n; i++)
            {
                // Check every following position.
                for (int step = 1; step < n; step++)
                {
                    // % n makes the array circular.
                    int nextIndex = (i + step) % n;

                    if (nums[nextIndex] > nums[i])
                    {
                        result[i] = nums[nextIndex];
                        break;
                    }
                }
            }

            return result;
        }
    }
}
