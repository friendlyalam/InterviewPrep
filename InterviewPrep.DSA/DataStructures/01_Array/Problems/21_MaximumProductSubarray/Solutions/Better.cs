
namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._21_MaximumProductSubarray.Solutions
{
    public static class FindProductSubArrayBetter
    {
        public static int MaxProduct(int[] nums)
        {
            if (nums is null)
                throw new ArgumentNullException(nameof(nums));

            if (nums.Length == 0)
                throw new ArgumentException("nums cannot be empty.", nameof(nums));

            int maxProduct = nums[0];

            // Try every possible starting index.
            for (int i = 0; i < nums.Length; i++)
            {
                int product = 1;

                // Expand the subarray from index i to the right.
                for (int j = i; j < nums.Length; j++)
                {
                    product *= nums[j];

                    // Update the maximum product found so far.
                    maxProduct = Math.Max(maxProduct, product);
                }
            }

            return maxProduct;
        }
    }
}
