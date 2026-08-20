
namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._02_FindSmallestElement.Solutions
{
    public class FindSmallestElement
    {
        public static int Find(int[] input)
        {

            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }
            if (input.Length == 0)
            {
                throw new ArgumentException("Input cannot be empty", nameof(input));
            }

            int smallest = input[0];

            for (int i = 1; i < input.Length; i++)
            {
                if (input[i] < smallest)
                {
                    smallest = input[i];
                }
            }
            return smallest;
        }
    }
}
