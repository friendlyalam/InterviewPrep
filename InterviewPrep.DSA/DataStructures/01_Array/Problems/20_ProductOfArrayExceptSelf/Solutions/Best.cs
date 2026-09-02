
namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._20_ProductOfArrayExceptSelf.Solutions
{
    public static class ProductOfArrayBest
    {
        public static int[] ProductArray(int[] nums)
        {
            if (nums is null)
                throw new ArgumentNullException(nameof(nums));

            if (nums.Length == 0)
                throw new ArgumentException("nums cannot be empty.", nameof(nums));

            int[] productArray = new int[nums.Length];

            // Prefix product.
            // productArray[i] will initially contain
            // the product of all elements before i.
            int prefix = 1;

            for (int i = 0; i < nums.Length; i++)
            {
                productArray[i] = prefix;

                // Update prefix for the next index.
                prefix *= nums[i];
            }

            // Suffix product.
            // Multiply the prefix product by the product
            // of all elements after the current index.
            int suffix = 1;

            for (int i = nums.Length - 1; i >= 0; i--)
            {
                productArray[i] *= suffix;

                // Update suffix for the next index.
                suffix *= nums[i];
            }

            return productArray;
        }
    }
}
