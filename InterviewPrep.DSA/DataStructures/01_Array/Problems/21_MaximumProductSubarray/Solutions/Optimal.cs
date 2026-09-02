
namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._21_MaximumProductSubarray.Solutions
{
    public static class FindProductSubArrayOptimal
    {
        public static int MaxProduct(int[] nums)
        {
            if (nums is null)
                throw new ArgumentNullException(nameof(nums));

            if (nums.Length == 0)
                throw new ArgumentException("nums cannot be empty.", nameof(nums));

            // Both maximum and minimum products start
            // with the first element.
            int maxProduct = nums[0];
            int minProduct = nums[0];

            // Overall maximum product found so far.
            int result = nums[0];

            for (int i = 1; i < nums.Length; i++)
            {
                int current = nums[i];

                // Store the previous values because maxProduct
                // and minProduct are both needed for calculation.
                int previousMax = maxProduct;
                int previousMin = minProduct;

                // Calculate all three possibilities:
                // 1. Start a new subarray with current.
                // 2. Extend the previous maximum product.
                // 3. Extend the previous minimum product.
                maxProduct = Math.Max(
                    current,
                    Math.Max(
                        previousMax * current,
                        previousMin * current));

                minProduct = Math.Min(
                    current,
                    Math.Min(
                        previousMax * current,
                        previousMin * current));

                // Update the best product found anywhere.
                result = Math.Max(result, maxProduct);
            }

            return result;
        }
    }
}
