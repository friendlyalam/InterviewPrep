Problem 39 — Merge Overlapping Intervals
Problem Statement

Given an array of intervals where:

intervals[i] = [start, end]

merge all overlapping intervals and return the resulting non-overlapping intervals.

Examples
Input:
[[1,3],[2,6],[8,10],[15,18]]

Output:
[[1,6],[8,10],[15,18]]
Input:
[[1,4],[4,5]]

Output:
[[1,5]]
Input:
[[1,2],[3,4],[5,6]]

Output:
[[1,2],[3,4],[5,6]]

Requirements / Validation
intervals must not be null.
intervals must contain at least one interval.
Every interval must contain exactly 2 values.
start cannot be greater than end.
The input may be unsorted.
Overlapping intervals must be merged.
Touching intervals such as [1,4] and [4,5] should be merged.
The returned intervals must be sorted by start time.


For Merge Intervals, remember this exact pattern:

1. Sort intervals by start.
2. Keep currentStart/currentEnd.
3. If nextStart <= currentEnd → merge.
4. Otherwise → save current interval and start a new one.