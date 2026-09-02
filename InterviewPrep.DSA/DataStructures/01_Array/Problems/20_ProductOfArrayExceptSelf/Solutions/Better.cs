
namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._20_ProductOfArrayExceptSelf.Solutions
{
    public static class ProductOfArrayBetter
    {
        public static int[] ProductArray(int[] nums)
        {
            if (nums is null)
                throw new ArgumentNullException(nameof(nums));

            if (nums.Length == 0)
                throw new ArgumentException("nums cannot be empty.", nameof(nums));

            int[] productArray = new int[nums.Length];

            // Calculate the product for every index separately.
            for (int i = 0; i < nums.Length; i++)
            {
                int product = 1;

                // Multiply every element except nums[i].
                for (int j = 0; j < nums.Length; j++)
                {
                    if (i != j)
                    {
                        product *= nums[j];
                    }
                }

                // Store the product excluding nums[i].
                productArray[i] = product;
            }

            return productArray;
        }
    }
}

