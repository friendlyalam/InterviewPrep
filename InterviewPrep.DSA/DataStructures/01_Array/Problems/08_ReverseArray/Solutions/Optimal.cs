

namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._08_ReverseArray.Solutions
{
    public static class ReverseAnArray
    {
        public static int[] Reverse(int[] input)
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

            // Start from both ends and move towards the center.
            int left = 0;
            int right = input.Length - 1;

            while (left < right)
            {
                // Store the left value before replacing it.
                int temp = input[left];

                // Move the right value to the left.
                input[left] = input[right];

                // Move the original left value to the right.
                input[right] = temp;

                left++;
                right--;
            }

            return input;
        }
    }
}
