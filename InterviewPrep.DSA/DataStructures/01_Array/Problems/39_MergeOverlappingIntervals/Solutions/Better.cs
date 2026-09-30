namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._39_MergeOverlappingIntervals.Solutions
{
    public static class MergeIntervalsBetter
    {
        public static int[][] Merge(int[][] intervals)
        {
            Validate(intervals);

            int[][] sortedIntervals = intervals
                .Select(interval => new[] { interval[0], interval[1] })
                .OrderBy(interval => interval[0])
                .ThenBy(interval => interval[1])
                .ToArray();

            List<int[]> result = new();

            foreach (int[] current in sortedIntervals)
            {
                if (result.Count == 0)
                {
                    result.Add(current);
                    continue;
                }

                int[] previous = result[^1];

                if (current[0] <= previous[1])
                {
                    previous[1] = Math.Max(previous[1], current[1]);
                }
                else
                {
                    result.Add(current);
                }
            }

            return result.ToArray();
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
