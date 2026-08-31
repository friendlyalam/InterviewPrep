
namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._15_FindTheMissingNumber.Solutions
{
    public class FindMissingNumberOptimal
    {
        public static int MissingNumber(int[] nums)
        {
            ArgumentNullException.ThrowIfNull(nums);

            

            // Start with n because n is part of the range [0, n].
            int result = nums.Length;

            // XOR every index with its corresponding array value.
            for (int i = 0; i < nums.Length; i++)
            {
                // XOR with the index.
                result ^= i;

                // XOR with the array value.
                // Matching numbers cancel each other because x ^ x = 0.
                result ^= nums[i];
            }

            // Only the missing number remains.
            return result;
        }
    }
}
