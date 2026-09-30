namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._39_MergeOverlappingIntervals.Solutions
{
    public class MergeIntervalsOptimal
    {
        public static int[][] Merge(int[][] intervals)
        {
            Validate(intervals);
            if (intervals == null || intervals.Length <= 1)
                return intervals ?? Array.Empty<int[]>();

            // Step 1: Sort intervals based on starting times
            Array.Sort(intervals, (a, b) => a[0].CompareTo(b[0]));

            List<int[]> merged = new List<int[]>();
            int[] currentInterval = intervals[0];
            merged.Add(currentInterval);

            // Step 2: Iterate and merge linearly
            foreach (var interval in intervals)
            {
                int currentEnd = currentInterval[1];
                int nextStart = interval[0];
                int nextEnd = interval[1];

                if (nextStart <= currentEnd)
                {
                    // Overlap detected: expand current interval's end bound
                    currentInterval[1] = Math.Max(currentEnd, nextEnd);
                }
                else
                {
                    // No overlap: add the new interval as reference
                    currentInterval = interval;
                    merged.Add(currentInterval);
                }
            }

            return merged.ToArray();
        }


        private static void Validate(int[][] intervals)
        {
            if (intervals is null)
                throw new ArgumentNullException(nameof(intervals));

            if (intervals.Length == 0)
                throw new ArgumentException(
                    "intervals cannot be empty.",
                    nameof(intervals));

            for (int i = 0; i < intervals.Length; i++)
            {
                if (intervals[i] is null)
                {
                    throw new ArgumentException(
                        $"Interval at index {i} cannot be null.",
                        nameof(intervals));
                }

                if (intervals[i].Length != 2)
                {
                    throw new ArgumentException(
                        $"Interval at index {i} must contain exactly two values.",
                        nameof(intervals));
                }

                if (intervals[i][0] > intervals[i][1])
                {
                    throw new ArgumentException(
                        $"Interval at index {i} has start greater than end.",
                        nameof(intervals));
                }
            }
        }
    }
}
