

namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._05_AverageArrayElements.Solutions
{
    public class FindAverage
    {
        public static double CalculateAverage(int[] input)
        {
            if (input is null) throw new ArgumentNullException(nameof(input));

            if (input.Length == 0) throw new ArgumentException("input cannot be empty", nameof(input));

            long inputSum = 0;
            for (int i = 0; i < input.Length; i++)
            {
                inputSum += input[i];
            }
            return (double)inputSum / input.Length;
        }
    }
}
