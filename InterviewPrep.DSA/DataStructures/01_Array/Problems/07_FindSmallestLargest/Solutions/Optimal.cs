

namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._07_FindSmallestLargest.Solutions
{
    public class FindSmallestLargestElements
    {
        public static (int smallest, int largest) Find(int[] input)
        {
            if (input is null) throw new ArgumentNullException(nameof(input));
            if (input.Length == 0) throw new ArgumentException("Input cannot be empty", nameof(input));
            // Start with the first element because we need
            // an actual array value to compare against.
            int smallest = input[0];
            int largest = input[0];

            for (int i = 1; i < input.Length; i++)
            {
                if (input[i] > largest)
                {
                    largest = input[i];
                }
                if (input[i] < smallest)
                {
                    smallest = input[i];
                }
            }

            return (smallest, largest);
        }
    }
}
