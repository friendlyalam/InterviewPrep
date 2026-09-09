
namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._24_IntersectionOfTwoArrays.Solutions
{
    public static class IntersectionOfArrayOptimal
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

            // Sort both arrays so that we can use two pointers.
            Array.Sort(nums1);
            Array.Sort(nums2);

            List<int> result = new();

            int first = 0;
            int second = 0;

            while (first < nums1.Length && second < nums2.Length)
            {
                if (nums1[first] == nums2[second])
                {
                    // Same value exists in both arrays.
                    result.Add(nums1[first]);

                    first++;
                    second++;
                }
                else if (nums1[first] < nums2[second])
                {
                    // Move first pointer because its value is smaller.
                    first++;
                }
                else
                {
                    // Move second pointer because its value is smaller.
                    second++;
                }
            }

            return result.ToArray();
        }
    }
}

//One correction to the word "optimal": if the original arrays must not be modified,
//use the Dictionary approach instead. The sorting approach modifies the input arrays.