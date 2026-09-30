namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._38_3Sum.Solutions
{
    public class ThreeSumOptimal
    {
        // Optimal: Sorting + Two Pointers
        public static IList<IList<int>> Find(int[] nums)
        {
            IList<IList<int>> result = new List<IList<int>>();

            if (nums == null)
                throw new ArgumentNullException("value can not be null", nameof(nums));
            if (nums.Length < 3)
                throw new ArgumentException("value must contain at least three elements", nameof(nums));

            // Step 1: Sort the array - O(n log n)
            Array.Sort(nums);

            // Step 2: Iterate through the array
            for (int i = 0; i < nums.Length - 2; i++)
            {
                // Early Exit: If the smallest fixed element is positive, 
                // three positive numbers can never sum to zero.
                if (nums[i] > 0)
                    break;

                // Skip duplicate values for the first element
                if (i > 0 && nums[i] == nums[i - 1])
                    continue;

                int left = i + 1;
                int right = nums.Length - 1;

                // Step 3: Two-pointer technique to find remaining two numbers
                while (left < right)
                {
                    int sum = nums[i] + nums[left] + nums[right];

                    if (sum == 0)
                    {
                        result.Add(new List<int> { nums[i], nums[left], nums[right] });

                        // Skip duplicates for the second element
                        while (left < right && nums[left] == nums[left + 1])
                            left++;

                        // Skip duplicates for the third element
                        while (left < right && nums[right] == nums[right - 1])
                            right--;

                        // Move both pointers inward after finding a triplet
                        left++;
                        right--;
                    }
                    else if (sum < 0)
                    {
                        // Sum is too small, increase the left pointer
                        left++;
                    }
                    else
                    {
                        // Sum is too large, decrease the right pointer
                        right--;
                    }
                }
            }

            return result;
        }
    }
}
