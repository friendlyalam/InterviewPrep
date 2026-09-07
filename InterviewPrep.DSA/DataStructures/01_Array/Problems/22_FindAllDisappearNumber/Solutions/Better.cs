

namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._22_FindAllDisappearNumber.Solutions
{
    public static class FindMissingNumbersBetter
    {
        public static IList<int> FindDisappearedNumbers(int[] nums)
        {
            if (nums is null)
                throw new ArgumentNullException(nameof(nums));

            if (nums.Length == 0)
                throw new ArgumentException(
                    "nums cannot be empty.",
                    nameof(nums));

            int n = nums.Length;

            // Formula for the sum of numbers from 1 to n.
            int expectedSum = n * (n + 1) / 2;

            // Calculate the actual sum.
            int actualSum = 0;

            // Track whether each number has appeared.
            bool[] appeared = new bool[n + 1];

            for (int i = 0; i < n; i++)
            {
                actualSum += nums[i];

                appeared[nums[i]] = true;
            }

            IList<int> result = new List<int>();

            // Find numbers that never appeared.
            for (int number = 1; number <= n; number++)
            {
                if (!appeared[number])
                {
                    result.Add(number);
                }
            }

            return result;
        }
    }
}
