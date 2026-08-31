
namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._16_SingleNumber.Solutions
{
    public class FindSingleNumber
    {
        public static int Find(int[] nums)
        {
            if (nums is null)
                throw new ArgumentNullException(nameof(nums));

            int result = 0;

            // XOR all numbers.
            // Duplicate numbers cancel each other.
            foreach (int num in nums)
            {
                result ^= num;
            }

            // The single number remains.
            // For an empty array, result naturally remains 0.
            return result;
        }
    }
}
