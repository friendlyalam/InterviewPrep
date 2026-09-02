
namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._20_ProductOfArrayExceptSelf.Solutions
{
    public static class ProductOfArrayOptimal
    {
        public static int[] ProductArray(int[] nums)
        {
            if (nums is null)
                throw new ArgumentNullException(nameof(nums));

            if (nums.Length == 0)
                throw new ArgumentException("nums cannot be empty.", nameof(nums));

            int n = nums.Length;

            int[] left = new int[n];
            int[] right = new int[n];
            int[] productArray = new int[n];

            // Calculate product of all elements to the LEFT.
            left[0] = 1;

            for (int i = 1; i < n; i++)
            {
                left[i] = left[i - 1] * nums[i - 1];
            }

            // Calculate product of all elements to the RIGHT.
            right[n - 1] = 1;

            for (int i = n - 2; i >= 0; i--)
            {
                right[i] = right[i + 1] * nums[i + 1];
            }

            // Product except self = left product × right product.
            for (int i = 0; i < n; i++)
            {
                productArray[i] = left[i] * right[i];
            }

            return productArray;
        }
    }
}
