
namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._23_FindTheDuplicateNumber.Solutions
{
    public class FindDuplicateNmber
    {
        //using hashset approach
        public static int FindDuplcate(int[] nums)
        {

            if (nums is null)
                throw new ArgumentNullException(nameof(nums));

            if (nums.Length < 2)
                throw new ArgumentException(
                    "nums must contain at least two elements.",
                    nameof(nums));
            HashSet<int> unique = new();
            foreach (int num in nums)
            {
                if (!unique.Add(num))
                {
                    return num;
                }
            }
            throw new InvalidOperationException("No duplicate found.");
        }
    }
}

