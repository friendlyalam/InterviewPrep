using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._04_CountEvenNumbers.Solutions
{
    public class CountEvenNumbers
    {
        public static int Count(int[] input)
        {
            int count = 0;

            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }
            if (input.Length == 0)
            {
                throw new ArgumentException("input cannot be empty", nameof(input));
            }
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] % 2 == 0)
                {
                    count++;
                }
            }
            return count;
        }
    }
}
