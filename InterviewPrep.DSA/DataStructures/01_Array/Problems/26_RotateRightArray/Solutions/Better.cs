
namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._26_RotateRightArray.Solutions
{
    //Better Approach — Temporary Array
    public static class RotateArrayBetter
    {
        public static int[] RotateRight(int[] nums, int rightRotateBy)
        {
            if (nums is null)
                throw new ArgumentNullException(nameof(nums));

            if (nums.Length == 0)
                throw new ArgumentException(
                    "nums cannot be empty.",
                    nameof(nums));

            if (rightRotateBy < 0)
                throw new ArgumentException(
                    "rightRotateBy cannot be negative.",
                    nameof(rightRotateBy));

            int n = nums.Length;

            // If k is greater than the array length,
            // only the remainder actually matters.
            rightRotateBy %= n;

            if (rightRotateBy == 0)
                return nums;

            int[] result = new int[n];
            int index = 0;

            // Copy the last k elements to the beginning.
            for (int i = n - rightRotateBy; i < n; i++)
            {
                result[index] = nums[i];
                index++;
            }

            // Copy the remaining elements after them.
            for (int i = 0; i < n - rightRotateBy; i++)
            {
                result[index] = nums[i];
                index++;
            }

            // Copy the rotated result back into nums.
            Array.Copy(result, nums, n);

            return nums;
        }
    }
}
