

namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._24_IntersectionOfTwoArrays.Solutions
{
    public static class IntersectionOfArrayBetter
    {
        public static int[] Intersect(int[] nums1, int[] nums2)
        {
            if (nums1 is null)
                throw new ArgumentNullException(nameof(nums1));

            if (nums2 is null)
                throw new ArgumentNullException(nameof(nums2));

            if (nums1.Length == 0 || nums2.Length == 0)
                throw new ArgumentException(
                    "Both arrays must contain at least one element.");

            Dictionary<int, int> frequency = new();

            // Store the frequency of each number in nums1.
            foreach (int num in nums1)
            {
                if (frequency.ContainsKey(num))
                    frequency[num]++;
                else
                    frequency[num] = 1;
            }

            List<int> result = new();

            // Find common numbers while respecting their frequency.
            foreach (int num in nums2)
            {
                if (frequency.TryGetValue(num, out int count) && count > 0)
                {
                    result.Add(num);

                    // One occurrence has now been consumed.
                    frequency[num]--;
                }
            }

            return result.ToArray();
        }
    }
}
