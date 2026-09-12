using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._26_RotateRightArray.Solutions
{
    public class RotateArrayOptimal
    {
        public static int[] RotateRight(int[] nums, int rightRotateBy)
        {
            if (nums is null)
                throw new ArgumentNullException(nameof(nums));

            if (nums.Length == 0)
                throw new ArgumentException(
                    "nums cannot be empty.",
                    nameof(nums));

            if (rightRotateBy < 0)
                throw new ArgumentException(
                    "rightRotateBy cannot be negative.",
                    nameof(rightRotateBy));

            int n = nums.Length;

            // Rotating by n positions gives the original array.
            // Therefore, only the remainder matters.
            rightRotateBy %= n;

            if (rightRotateBy == 0)
                return nums;

            // Step 1: Reverse the entire array.
            Reverse(nums, 0, n - 1);

            // Step 2: Reverse the first k elements.
            Reverse(nums, 0, rightRotateBy - 1);

            // Step 3: Reverse the remaining elements.
            Reverse(nums, rightRotateBy, n - 1);

            return nums;
        }

        private static void Reverse(int[] nums, int left, int right)
        {
            while (left < right)
            {
                // Swap the elements at both ends.
                (nums[left], nums[right]) = (nums[right], nums[left]);

                left++;
                right--;
            }
        }
    }
}
