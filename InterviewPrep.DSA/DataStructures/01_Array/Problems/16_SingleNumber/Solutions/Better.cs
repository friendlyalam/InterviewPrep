
namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._16_SingleNumber.Solutions
{
    public class FindSingle
    {
        public static int Find(int[] nums)
        {
            if (nums is null)
                throw new ArgumentNullException(nameof(nums));

            Dictionary<int, int> mapping = new();

            // Store the frequency of every number.
            foreach (int num in nums)
            {
                if (mapping.ContainsKey(num))
                {
                    mapping[num]++;
                }
                else
                {
                    mapping[num] = 1;
                }
            }

            // Find the number whose frequency is 1.
            foreach (KeyValuePair<int, int> pair in mapping)
            {
                if (pair.Value == 1)
                {
                    return pair.Key;
                }
            }

            return -1;
        }
    }
}
