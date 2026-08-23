

namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._03_SumOfArrays.Solutions
{
    public class SumOfArrays
    {
        public static int Sum(int[] input)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }
            if (input.Length == 0)
            {
                throw new ArgumentException("input cannot be empty", nameof(input));
            }
            int sum = 0;
            for(int i=0;i<input.Length;i++)
            {
                sum += input[i];
            }
            return sum;
        }
    }
}
