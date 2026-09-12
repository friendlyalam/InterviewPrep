using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._27_MoveEvenNumbersToTheLeft.Solutions
{
    public static class MoveEvenToLeftBetter
    {
        public static int[] MoveEvenToLeft(int[] nums)
        {
            if (nums is null)
                throw new ArgumentNullException(nameof(nums));

            if (nums.Length == 0)
                throw new ArgumentException(
                    "nums cannot be empty.",
                    nameof(nums));

            int[] result = new int[nums.Length];

            int left = 0;
            int right = nums.Length - 1;

            foreach (int number in nums)
            {
                if (number % 2 == 0)
                {
                    result[left] = number;
                    left++;
                }
                else
                {
                    result[right] = number;
                    right--;
                }
            }

            Array.Copy(result, nums, nums.Length);

            return nums;
        }
    }
}
