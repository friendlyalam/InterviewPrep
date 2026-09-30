namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._40_Insert_Interval.Solutions
{

    // Better: Sort and Merge
    public class InsertIntervalBetter
    {
        public static int[][] Insert(
            int[][] intervals,
            int[] newInterval)
        {
            // Validate the main input.
            ValidateIntervals(intervals);

            // Validate the new interval.
            ValidateInterval(newInterval, nameof(newInterval));

            // Create a list so we can insert the new interval.
            List<int[]> allIntervals = new();

            // Add all existing intervals.
            foreach (int[] interval in intervals)
            {
                allIntervals.Add(
                    new[] { interval[0], interval[1] });
            }

            // Add the new interval.
            allIntervals.Add(
                new[] { newInterval[0], newInterval[1] });

            // Sort all intervals by start time.
            allIntervals.Sort(
                (a, b) =>
                {
                    int comparison =
                        a[0].CompareTo(b[0]);

                    return comparison != 0
                        ? comparison
                        : a[1].CompareTo(b[1]);
                });

            List<int[]> result = new();

            // Process intervals in sorted order.
            foreach (int[] current in allIntervals)
            {
                // If result is empty, simply add the interval.
                if (result.Count == 0)
                {
                    result.Add(
                        new[] { current[0], current[1] });

                    continue;
                }

                // Get the last interval already processed.
                int[] previous = result[^1];

                // Check whether current overlaps previous.
                if (current[0] <= previous[1])
                {
                    // Extend the end if necessary.
                    previous[1] =
                        Math.Max(previous[1], current[1]);
                }
                else
                {
                    // No overlap, so keep it separately.
                    result.Add(
                        new[] { current[0], current[1] });
                }
            }

            return result.ToArray();
        }

        private static void ValidateIntervals(
            int[][] intervals)
        {
            if (intervals is null)
                throw new ArgumentNullException(
                    nameof(intervals));

            for (int i = 0; i < intervals.Length; i++)
            {
                ValidateInterval(
                    intervals[i],
                    $"intervals[{i}]");
            }
        }

        private static void ValidateInterval(
            int[] interval,
            string parameterName)
        {
            if (interval is null)
            {
                throw new ArgumentNullException(
                    parameterName);
            }

            if (interval.Length != 2)
            {
                throw new ArgumentException(
                    "Each interval must contain exactly two values.",
                    parameterName);
            }

            if (interval[0] > interval[1])
            {
                throw new ArgumentException(
                    "Interval start cannot be greater than interval end.",
                    parameterName);
            }
        }
    }
}
