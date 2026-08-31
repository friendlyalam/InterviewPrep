
namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._17_MajorityElement.Solutions
{
    public class FindElementMajority
    {
        public static int Majority(int[] nums)
        {
            if (nums is null)
                throw new ArgumentNullException(nameof(nums));

            if (nums.Length == 0)
                throw new ArgumentException("nums cannot be empty.", nameof(nums));

            int candidate = 0;
            int count = 0;

            foreach (int num in nums)
            {
                // If count becomes zero, choose the current number
                // as the new candidate.
                if (count == 0)
                {
                    candidate = num;
                }

                // Same number increases the count.
                // Different number decreases the count.
                count += candidate == num ? 1 : -1;
            }

            // The problem guarantees that a majority element exists,
            // so the final candidate is the majority element.
            return candidate;
        }
    }
}
