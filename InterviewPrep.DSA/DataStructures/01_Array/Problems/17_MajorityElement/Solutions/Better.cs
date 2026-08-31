using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._17_MajorityElement.Solutions
{
    public class MajorityElement
    {
        public static int Majority(int[] nums)
        {
            if (nums is null)
                throw new ArgumentNullException(nameof(nums));

            if (nums.Length == 0)
                throw new ArgumentException("nums cannot be empty.", nameof(nums));

            Dictionary<int, int> mappings = new();

            // Count the frequency of every number.
            foreach (int num in nums)
            {
                if (mappings.ContainsKey(num))
                {
                    mappings[num]++;
                }
                else
                {
                    mappings[num] = 1;
                }
            }

            int maxNumber = 0;
            int maxCount = 0;

            // Find the number with the highest frequency.
            foreach (KeyValuePair<int, int> pair in mappings)
            {
                if (pair.Value > maxCount)
                {
                    maxCount = pair.Value;
                    maxNumber = pair.Key;
                }
            }

            return maxNumber;
        }
    }
}
