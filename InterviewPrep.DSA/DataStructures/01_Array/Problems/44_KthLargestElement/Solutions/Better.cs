namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._44_KthLargestElement.Solutions
{

    //Better: Min Heap
    //The idea is to keep only the k largest elements in a min heap.
    //The smallest element inside that heap will be the kth largest overall.
    public static class KthLargestElementBetter
    {
        public static int Find(int[] nums, int k)
        {
            ValidateInput(nums, k);

            PriorityQueue<int, int> minHeap = new();

            for (int i = 0; i < nums.Length; i++)
            {
                // Add the current element to the min heap.
                minHeap.Enqueue(nums[i], nums[i]);

                // Keep only the k largest elements.
                if (minHeap.Count > k)
                {
                    minHeap.Dequeue();
                }
            }

            // The smallest element among the k largest
            // elements is the kth largest element.
            return minHeap.Peek();
        }

        private static void ValidateInput(int[] nums, int k)
        {
            if (nums is null)
                throw new ArgumentNullException(nameof(nums));

            if (nums.Length == 0)
                throw new ArgumentException(
                    "nums cannot be empty.",
                    nameof(nums));

            if (k <= 0 || k > nums.Length)
                throw new ArgumentException(
                    "k must be between 1 and nums.Length.",
                    nameof(k));
        }
    }
}
