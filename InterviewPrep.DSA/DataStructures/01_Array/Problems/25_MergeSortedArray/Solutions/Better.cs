
namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._25_MergeSortedArray.Solutions
{
    //Better Approach — Temporary Array + Two Pointers
    public static class MergeSortedArrayBetter
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

            int[] result = new int[m + n];

            int first = 0;
            int second = 0;
            int index = 0;

            while (first < m && second < n)
            {
                if (nums1[first] <= nums2[second])
                {
                    result[index] = nums1[first];
                    first++;
                }
                else
                {
                    result[index] = nums2[second];
                    second++;
                }

                index++;
            }

            // Copy remaining nums1 elements.
            while (first < m)
            {
                result[index] = nums1[first];
                first++;
                index++;
            }

            // Copy remaining nums2 elements.
            while (second < n)
            {
                result[index] = nums2[second];
                second++;
                index++;
            }

            // Copy the merged result back into nums1.
            Array.Copy(result, nums1, result.Length);

            return nums1;
        }
    }
}
