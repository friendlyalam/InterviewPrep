
namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._10_CountOccuranceOfGivenElement.Solutions
{
    public static class CountOccurrence
    {
        public static int Count(int[] input, int target)
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

            int count = 0;

            // Check each element once and increment count
            // whenever it matches the target.
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == target)
                {
                    count++;
                }
            }

            return count;
        }
    }
}
