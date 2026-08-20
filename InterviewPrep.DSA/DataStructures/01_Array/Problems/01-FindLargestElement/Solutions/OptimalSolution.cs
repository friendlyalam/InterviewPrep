
namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._001_FindLargestElement.Solutions
{
    public class FindLargestElement
    {
        public static int Find(int[] input)
        {
            if (input == null) {
                throw new ArgumentNullException(nameof (input));
            }
            if (input.Length == 0) {
                throw new ArgumentException("Input can not be empty", nameof(input));
            }
            int largest = input[0];
            for (int i = 1; i < input.Length; i++)
            {
                if (input[i] > largest)
                {
                    largest = input[i];
                }
            }

            return largest;
        }
    }
}
