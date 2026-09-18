namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._21_MaximumProductSubarray.Solutions
{
    public class MaxProductBest
    {
        //Prefix+Suffix technique
        public static int MaxProduct(int[] nums)
        {
            if (nums == null || nums.Length == 0)
                throw new ArgumentException("Array cannot be null or empty.");

            int prefixProduct = 1;
            int suffixProduct = 1;
            int maxProduct = nums[0];

            for (int i = 0; i < nums.Length; i++)
            {
                // Reset after zero
                if (prefixProduct == 0)
                    prefixProduct = 1;

                if (suffixProduct == 0)
                    suffixProduct = 1;

                // Calculate from left
                prefixProduct *= nums[i];

                // Calculate from right
                suffixProduct *= nums[nums.Length - 1 - i];//Take elements from the array from RIGHT → LEFT and multiply them.

                // Take maximum from both directions
                maxProduct = Math.Max(
                    maxProduct,
                    Math.Max(prefixProduct, suffixProduct));
            }

            return maxProduct;
        }
    }
}
