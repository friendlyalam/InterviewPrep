namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._29_TwoSum.Solutions
{
    public class TwoSumBruteForce
    {
        public static int[] TwoSum(int[] nums, int target)
        {
            if (nums is null)
                throw new ArgumentNullException(nameof(nums));
            if (nums.Length == 0)
                throw new ArgumentException(
                    "nums cannot be empty.",
                    nameof(nums));
            for (int i = 0; i < nums.Length; i++)
            {
                for (int j = i + 1; j < nums.Length; j++)
                {
                    if (nums[i] + nums[j] == target)
                    {
                        return new int[] { i, j };
                    }
                }
            }
            throw new InvalidOperationException(
                "No two sum solution exists.");
        }
    }
}
