
namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._06_FindSecondLargestElement.Solutions
{
    public static class FindSecondLargestBruteForce
    {
        public static int Find(int[] input)
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
            // Create a separate collection to remove duplicate values.
            List<int> distinctElements = new();

            foreach (int number in input)
            {
                // Contains() checks whether this value already exists.
                if (!distinctElements.Contains(number))
                {
                    distinctElements.Add(number);
                }
            }

            if (distinctElements.Count < 2)
            {
                throw new ArgumentException("Input must contain at least two distinct elements.");
            }
            // Sort in ascending order.
            distinctElements.Sort();

            // in modern C#,^2 means the second element from the end.
            // After ascending sort, this is the second largest element.
            return distinctElements[^2];//or we can use  distinctElements[distinctElements.Count-2)
        }
    }
}
