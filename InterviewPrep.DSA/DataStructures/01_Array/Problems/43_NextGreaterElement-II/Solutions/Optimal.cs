namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._43_NextGreaterElementCircular.Solutions
{
    //Optimal Solution — Approach: Monotonic Stack
    //We process the array twice because it is circular.
    //The stack stores indexes whose next greater element has not yet been found.
    public static class NextGreaterElementCircularOptimal
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

            Stack<int> stack = new();

            // Process the array twice to simulate circular behavior.
            for (int i = 0; i < 2 * n; i++)
            {
                int currentIndex = i % n;

                // Remove elements that are smaller than the current value.
                // Current value is their next greater element.
                while (stack.Count > 0 &&
                       nums[stack.Peek()] < nums[currentIndex])
                {
                    int index = stack.Pop();
                    result[index] = nums[currentIndex];
                }

                // Only indexes from the first pass should be added.
                // Otherwise the same index could be pushed twice.
                if (i < n)
                {
                    stack.Push(currentIndex);
                }
            }

            return result;
        }
    }
}
