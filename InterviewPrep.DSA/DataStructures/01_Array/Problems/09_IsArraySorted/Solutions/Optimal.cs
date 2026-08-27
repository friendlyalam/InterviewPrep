
namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._09_IsArraySorted.Solutions
{
    public static class IsArraySorted
    {
        public static bool IsSorted(int[] input)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (input.Length == 0)
            {
                throw new ArgumentException(
                    "Input cannot be empty.",
                    nameof(input));
            }

            // Compare each element with the previous element.
            // If the current element is smaller, the array is not sorted.
            for (int i = 1; i < input.Length; i++)
            {
                if (input[i] < input[i - 1])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
