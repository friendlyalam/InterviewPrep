

namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._06_FindSecondLargestElement.Solutions
{
    public class FindSecondLargestOptimal
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

            // null means we have not found any value yet.
            int? largest = null;

            // null means we have not found a second distinct value yet.
            int? secondLargest = null;

            foreach (int number in input)
            {
                if (largest is null || number > largest)
                {
                    // The old largest becomes the second largest.
                    secondLargest = largest;

                    // Current number becomes the new largest.
                    largest = number;
                }
                else if (number < largest &&
                         (secondLargest is null || number > secondLargest))
                {
                    // number is smaller than largest but larger
                    // than the current second largest.
                    secondLargest = number;
                }
            }

            // No second distinct value was found.
            if (secondLargest is null)
            {
                throw new ArgumentException(
                    "Input must contain at least 2 distinct elements.");
            }

            return secondLargest.Value;
        }
    }
}
