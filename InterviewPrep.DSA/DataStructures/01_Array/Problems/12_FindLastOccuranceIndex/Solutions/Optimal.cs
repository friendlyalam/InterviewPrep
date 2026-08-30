

namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._12_FindLastOccuranceIndex.Solutions
{
    public static class FindLastOccurrenceIndex
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

            // Start from the end because we need the last occurrence.
            for (int i = numbers.Length - 1; i >= 0; i--)
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
