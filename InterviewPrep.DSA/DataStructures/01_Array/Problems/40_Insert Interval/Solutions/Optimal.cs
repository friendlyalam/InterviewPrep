namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._40_Insert_Interval.Solutions
{

    //Optimal-            Sorted Intervals +Merge + Three - Phase Scan

    //We divide the problem into exactly three phases:

    //Phase 1 → intervals completely before newInterval
    //Phase 2 → intervals overlapping newInterval
    //Phase 3 → intervals completely after newInterval
    public static class InsertIntervalOptimal
    {
        public static int[][] Insert(
            int[][] intervals,
            int[] newInterval)
        {
            // Step 1: Validate the existing intervals.
            ValidateIntervals(intervals);

            // Step 2: Validate the interval we want to insert.
            ValidateInterval(
                newInterval,
                nameof(newInterval));

            List<int[]> result = new();

            int i = 0;

            // ---------------------------------------------------------
            // PHASE 1:
            // Add intervals that are completely before newInterval.
            //
            // Example:
            //
            // [1,2] [3,5]    [6,8]
            //              ↑
            //          new interval
            //
            // If intervals[i][1] < newInterval[0],
            // there is no overlap.
            // ---------------------------------------------------------

            while (i < intervals.Length &&
                   intervals[i][1] < newInterval[0])
            {
                // This interval ends before the new interval starts.
                result.Add(
                    new[]
                    {
                    intervals[i][0],
                    intervals[i][1]
                    });

                i++;
            }

            // ---------------------------------------------------------
            // PHASE 2:
            // Merge all intervals that overlap newInterval.
            //
            // Overlap condition:
            //
            // intervals[i][0] <= newInterval[1]
            //
            // If the next interval starts before the current
            // merged interval ends, they overlap.
            // ---------------------------------------------------------

            while (i < intervals.Length &&
                   intervals[i][0] <= newInterval[1])
            {
                // Expand the start if the existing interval starts
                // earlier than the new interval.
                newInterval[0] =
                    Math.Min(
                        newInterval[0],
                        intervals[i][0]);

                // Expand the end if the existing interval ends later.
                newInterval[1] =
                    Math.Max(
                        newInterval[1],
                        intervals[i][1]);

                i++;
            }

            // ---------------------------------------------------------
            // At this point, newInterval contains the complete
            // merged interval.
            // ---------------------------------------------------------

            result.Add(
                new[]
                {
                newInterval[0],
                newInterval[1]
                });

            // ---------------------------------------------------------
            // PHASE 3:
            // Add all intervals that are completely after
            // the merged interval.
            // ---------------------------------------------------------

            while (i < intervals.Length)
            {
                result.Add(
                    new[]
                    {
                    intervals[i][0],
                    intervals[i][1]
                    });

                i++;
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

            // The problem requires the existing intervals
            // to be sorted and non-overlapping.
            for (int i = 1; i < intervals.Length; i++)
            {
                if (intervals[i][0] < intervals[i - 1][0])
                {
                    throw new ArgumentException(
                        "Intervals must be sorted by start time.",
                        nameof(intervals));
                }

                if (intervals[i][0] <= intervals[i - 1][1])
                {
                    throw new ArgumentException(
                        "Existing intervals must be non-overlapping.",
                        nameof(intervals));
                }
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
