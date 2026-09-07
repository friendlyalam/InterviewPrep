
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
            left[0] = 1;//1 is the identity value for multiplication:

            for (int i = 1; i < n; i++)
            {

                // "To calculate the product for position i, take the previous left product and add the element immediately before i."
                left[i] = left[i - 1] * nums[i - 1];// num[i-1] because excluding the current element.
            }

            // Calculate product of all elements to the RIGHT.
            right[n - 1] = 1;//n - 1 is the last index.

            for (int i = n - 2; i >= 0; i--)
            {
                right[i] = right[i + 1] * nums[i + 1];// num[i+1] because excluding the current element.
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

//Easy rule to remember
//        LEFT     CURRENT     RIGHT
//          ↓         ↓          ↓
//       i - 1        i        i + 1

//Therefore, for left product:

//nums[i - 1]

//For right product:

//nums[i + 1]

//That's exactly why your right-side code uses:

//right[i] = right[i + 1] * nums[i + 1];

//i - 1 = look backward.
//i + 1 = look forward.Easy rule to remember
//        LEFT     CURRENT     RIGHT
//          ↓         ↓          ↓
//       i - 1        i        i + 1

//Therefore, for left product:

//nums[i - 1]

//For right product:

//nums[i + 1]

//That's exactly why your right-side code uses:

//right[i] = right[i + 1] * nums[i + 1];

//i - 1 = look backward.
//i + 1 = look forward.