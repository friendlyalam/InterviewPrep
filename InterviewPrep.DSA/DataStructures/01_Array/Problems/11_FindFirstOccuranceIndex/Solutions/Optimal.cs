using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._11_FindFirstOccuranceIndex.Solutions
{
    public static class FindFirstOccurrenceIndex
    {
        public static int Find(int[] numbers, int target)
        {
            if (numbers is null)
            {
                throw new ArgumentNullException(nameof(numbers));
            }

            if (numbers.Length == 0)
            {
                throw new ArgumentException(
                    "Input cannot be empty.",
                    nameof(numbers));
            }

            // Search from left to right.
            // The first match is the first occurrence.
            for (int i = 0; i < numbers.Length; i++)
            {
                if (numbers[i] == target)
                {
                    return i;
                }
            }

            // Target was not found.
            return -1;
        }
    }
}
