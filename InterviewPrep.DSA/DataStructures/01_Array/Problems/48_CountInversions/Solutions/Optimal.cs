namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._48_CountInversions.Solutions
{
    //Optimal: Merge Sort
    public static class CountInversionsOptimal
    {
        public static long Count(int[] nums)
        {
            ValidateInput(nums);

            int[] temp = new int[nums.Length];

            return MergeSortAndCount(
                nums,
                temp,
                0,
                nums.Length - 1);
        }

        private static long MergeSortAndCount(
            int[] nums,
            int[] temp,
            int left,
            int right)
        {
            if (left >= right)
                return 0;

            int mid = left + (right - left) / 2;

            long count = 0;

            count += MergeSortAndCount(
                nums,
                temp,
                left,
                mid);

            count += MergeSortAndCount(
                nums,
                temp,
                mid + 1,
                right);

            count += MergeAndCount(
                nums,
                temp,
                left,
                mid,
                right);

            return count;
        }

        private static long MergeAndCount(
            int[] nums,
            int[] temp,
            int left,
            int mid,
            int right)
        {
            int i = left;
            int j = mid + 1;
            int k = left;

            long count = 0;

            while (i <= mid && j <= right)
            {
                if (nums[i] <= nums[j])
                {
                    temp[k++] = nums[i++];
                }
                else
                {
                    // Every remaining element in the left
                    // half is greater than nums[j].
                    count += mid - i + 1;

                    temp[k++] = nums[j++];
                }
            }

            while (i <= mid)
            {
                temp[k++] = nums[i++];
            }

            while (j <= right)
            {
                temp[k++] = nums[j++];
            }

            for (int index = left; index <= right; index++)
            {
                nums[index] = temp[index];
            }

            return count;
        }

        private static void ValidateInput(int[] nums)
        {
            if (nums is null)
                throw new ArgumentNullException(nameof(nums));

            if (nums.Length == 0)
                throw new ArgumentException(
                    "nums cannot be empty.",
                    nameof(nums));
        }
    }
}
