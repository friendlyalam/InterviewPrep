
namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._25_MergeSortedArray.Solutions
{
    //Optimal Approach: Three-Pointer Strategy (From Right to Left)
    public static class MergeSortedArrayOptimal
    {
        public static int[] Merge(int[] nums1, int m, int[] nums2, int n)
        {
            if (nums1 is null)
                throw new ArgumentNullException(nameof(nums1));

            if (nums2 is null)
                throw new ArgumentNullException(nameof(nums2));

            if (m < 0 || n < 0)
                throw new ArgumentException("m and n cannot be negative.");

            if (nums1.Length != m + n)
                throw new ArgumentException(
                    "nums1 length must be equal to m + n.",
                    nameof(nums1));

            if (nums2.Length != n)
                throw new ArgumentException(
                    "nums2 length must be equal to n.",
                    nameof(nums2));

            int first = m - 1;//Pointer for nums1 valid elements
            int second = n - 1;//Pointer for nums2 elements
            int position = m + n - 1;//// Pointer for insertion at the end of nums1

            // Merge from the end so that valid nums1 elements
            // are not overwritten.
            //// Compare elements from the back and place the larger one at index position.
            while (first >= 0 && second >= 0)
            {
                if (nums1[first] > nums2[second])
                {
                    nums1[position] = nums1[first];
                    first--;
                }
                else
                {
                    nums1[position] = nums2[second];
                    second--;
                }

                position--;
            }

            // If nums2 still has elements, copy them.
            // Remaining nums1 elements are already in their correct positions.
            while (second >= 0)
            {
                nums1[position] = nums2[second];
                second--;
                position--;
            }

            return nums1;
        }
    }
}
