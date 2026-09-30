# Problem 39 — Merge Overlapping Intervals

## Problem

Given intervals:

```text
[start, end]

merge all overlapping intervals.

Example:

Input:
[[1,3],[2,6],[8,10],[15,18]]

Output:
[[1,6],[8,10],[15,18]]
Why

Intervals can overlap in many different positions.

Checking every pair repeatedly is unnecessary.

The key observation is:

If intervals are sorted by start time, we only need to compare the current interval with the previously merged interval.

Idea

First sort by start:

[[1,3],[2,6],[8,10],[15,18]]

Now process from left to right.

Start with:

[1,3]

Next:

[2,6]

Since:

2 <= 3

they overlap.

Merge:

[1,6]

Next:

[8,10]

Since:

8 > 6

there is no overlap.

Store:

[1,6]

and start a new interval:

[8,10]
Better Solution
Approach

Create a sorted copy of the intervals.

Then maintain the last interval in the result.

For every new interval:

if current.start <= previous.end

merge them.

Otherwise add the current interval separately.

Better Dry Run

Input:

[[1,3],[2,6],[8,10],[15,18]]

Already sorted.

Step 1
result = [[1,3]]
Step 2

Current:

[2,6]

Check:

2 <= 3

Overlap.

Merge:

[1,6]
Step 3

Current:

[8,10]

Check:

8 <= 6

False.

Add separately:

[[1,6],[8,10]]
Step 4

Current:

[15,18]

Check:

15 <= 10

False.

Final:

[[1,6],[8,10],[15,18]]
Optimal Solution
Approach

Sort the original interval array by start time.

Maintain:

currentStart
currentEnd

For every next interval:

nextStart
nextEnd

If:

nextStart <= currentEnd

the intervals overlap.

Update:

currentEnd = max(currentEnd, nextEnd)

Otherwise:

Store the current interval.
Start processing the next interval.
Important Condition

The most important line is:

nextStart <= currentEnd

Why <= instead of <?

Because touching intervals are considered overlapping in this problem.

Example:

[1,4]
[4,5]

Since:

4 <= 4

merge them:

[1,5]
Dry Run

Input:

[[1,4],[2,5],[7,9],[8,10]]
Initial
current = [1,4]
[2,5]
2 <= 4

Overlap.

Update:

current = [1,5]
[7,9]
7 <= 5

False.

Store:

[1,5]

Start:

current = [7,9]
[8,10]
8 <= 9

Overlap.

Update:

current = [7,10]

Final:

[[1,5],[7,10]]
Time Complexity
Sorting

Sorting n intervals requires:

O(n log n)
Merge Pass

We scan every interval once:

O(n)

Therefore:

O(n log n) + O(n)
= O(n log n)
Space Complexity

The optimal implementation does not create a separate sorted copy.

Additional working variables are:

currentStart
currentEnd
nextStart
nextEnd

Therefore auxiliary working space is:

O(1)

The output itself requires:

O(n)

in the worst case.

Edge Cases
Null input
Empty input
One interval
No overlapping intervals
All intervals overlap
Completely contained intervals
Touching intervals
Duplicate intervals
Negative values
Unsorted intervals
Invalid interval with start > end
Null interval
Interval containing more than two values

Key DSA Pattern
Intervals
   ↓
Sort by Start
   ↓
Scan Left → Right
   ↓
Merge if Overlap

This is the fundamental:

Sorting + Interval Merging

pattern.

It is also useful for:

Meeting Rooms
Insert Interval
Employee Free Time
Calendar scheduling
Range merging
Time-window problems