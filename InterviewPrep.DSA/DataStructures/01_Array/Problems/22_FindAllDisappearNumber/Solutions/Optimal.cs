
namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._22_FindAllDisappearNumber.Solutions
{
    public static class FindMissingNumbersOptimal
    {
        public static IList<int> FindDisappearedNumbers(int[] nums)
        {
            if (nums is null)
                throw new ArgumentNullException(nameof(nums));

            if (nums.Length == 0)
                throw new ArgumentException(
                    "nums cannot be empty.",
                    nameof(nums));

            // Step 1:
            // Use each number as an index and mark that index negative.
            for (int i = 0; i < nums.Length; i++)
            {
                int index = Math.Abs(nums[i]) - 1;

                // Mark the corresponding number as appeared.
                if (nums[index] > 0)
                {
                    nums[index] = -nums[index];
                }
            }

            IList<int> result = new List<int>();

            // Step 2:
            // If a position is still positive,
            // that number never appeared.
            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] > 0)
                {
                    result.Add(i + 1);
                }
            }

            return result;
        }
    }
}
