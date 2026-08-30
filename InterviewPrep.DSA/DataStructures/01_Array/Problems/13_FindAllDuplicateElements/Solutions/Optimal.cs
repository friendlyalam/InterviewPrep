
namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._13_FindAllDuplicateElements.Solutions
{
    public class FindAllDuplicateElements
    {
        public static int[] Find(int[] numbers)
        {
            ArgumentNullException.ThrowIfNull(numbers);

            if (numbers.Length == 0)
            {
                throw new ArgumentException("Input cannot be empty.", nameof(numbers));
            }

            HashSet<int> seen = new();
            HashSet<int> duplicates = new();

            foreach (int number in numbers)
            {
                // HashSet.Add returns false if the item was already in the set
                if (!seen.Add(number))
                {
                    duplicates.Add(number);
                }
            }

            return duplicates.ToArray();
        }
    }
}
