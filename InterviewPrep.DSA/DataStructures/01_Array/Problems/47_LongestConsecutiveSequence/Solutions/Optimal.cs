namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._47_LongestConsecutiveSequence.Solutions
{
    //Optimal: HashSet
    public static class LongestConsecutiveSequenceOptimal
    {
        public static int Find(int[] nums)
        {
            ValidateInput(nums);

            if (nums.Length == 0)
                return 0;

            HashSet<int> numbers = new(nums);

            int longest = 0;

            foreach (int number in numbers)
            {
                // If number - 1 exists, this number is not
                // the beginning of a sequence.
                if (number != int.MinValue &&
                    numbers.Contains(number - 1))
                {
                    continue;
                }

                int currentNumber = number;
                int currentLength = 1;

                // Count the consecutive sequence.
                while (currentNumber != int.MaxValue &&
                       numbers.Contains(currentNumber + 1))
                {
                    currentNumber++;
                    currentLength++;
                }

                longest = Math.Max(
                    longest,
                    currentLength);
            }

            return longest;
        }

        private static void ValidateInput(int[] nums)
        {
            if (nums is null)
                throw new ArgumentNullException(nameof(nums));
        }
    }
}
