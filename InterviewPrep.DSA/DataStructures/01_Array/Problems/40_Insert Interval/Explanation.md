
# Problem 40 — Insert Interval

## Problem

Given a set of non-overlapping intervals sorted by their start time,
insert a new interval and merge overlapping intervals.

Example:

Input:

```text
[[1,3],[6,9]]
newInterval = [2,5]

Output:

[[1,5],[6,9]]
Important Input Property

The existing intervals are already:

Sorted by start time.
Non-overlapping.

Example:

[[1,2],[5,7],[10,12]]

This property is extremely important because it allows us to avoid sorting again.

Better Solution
Idea

The simpler approach is:

Existing intervals
        +
New interval
        ↓
Combine everything
        ↓
Sort
        ↓
Merge

This reuses the logic from Merge Intervals.

Step-by-Step

Suppose:

intervals =
[[1,3],[6,9]]

newInterval =
[2,5]
Step 1

Add the new interval:

[[1,3],[6,9],[2,5]]
Step 2

Sort:

[[1,3],[2,5],[6,9]]
Step 3

Merge [1,3] and [2,5].

They overlap because:

2 <= 3

Result:

[[1,5]]
Step 4

Compare [6,9].

Since:

6 > 5

there is no overlap.

Final:

[[1,5],[6,9]]
Better Complexity

Sorting:

O(n log n)

Merging:

O(n)

Overall:

O(n log n)

Extra space:

O(n)

because we create a new collection containing the intervals.

Optimal Solution
Key Observation

The existing intervals are already sorted.

Therefore:

DO NOT SORT AGAIN

Instead, process them from left to right.

There are exactly three groups.

Group 1:
Intervals before newInterval

Group 2:
Intervals overlapping newInterval

Group 3:
Intervals after newInterval
Phase 1 — Before

Example:

intervals =
[[1,2],[3,5],[6,7],[8,10]]

newInterval =
[4,8]

[1,2] is completely before [4,8].

Check:

2 < 4

Therefore add it directly.

Result:

[[1,2]]
Phase 2 — Overlapping

Now:

[3,5]

overlaps:

[4,8]

because:

3 <= 8

Merge:

start = min(3,4) = 3
end   = max(5,8) = 8

New merged interval:

[3,8]

Next interval:

[6,7]

also overlaps [3,8].

So:

start = min(3,6) = 3
end   = max(8,7) = 8

Still:

[3,8]

Next:

[8,10]

It also overlaps because:

8 <= 8

Merge:

[3,10]
Phase 3 — After

Any remaining intervals are completely after the merged interval.

They can simply be added.

Complete Dry Run

Input:

intervals =
[[1,2],[3,5],[6,7],[8,10],[12,16]]

newInterval =
[4,8]
Phase 1

[1,2]

Check:

2 < 4

Yes.

Add:

result = [[1,2]]
Phase 2

Current:

[3,5]

Check:

3 <= 8

Overlap.

Merge:

[3,8]

Current:

[6,7]

Check:

6 <= 8

Overlap.

Still:

[3,8]

Current:

[8,10]

Check:

8 <= 8

Overlap.

Merge:

[3,10]

Add merged interval:

result =
[
    [1,2],
    [3,10]
]
Phase 3

Remaining:

[12,16]

Add directly.

Final:

[
    [1,2],
    [3,10],
    [12,16]
]
Why The Optimal Solution Is O(n)

There is no sorting.

We scan the input only once.

Each interval is processed exactly once.

Therefore:

O(n)
Space Complexity

We store the output in result.

The algorithm itself uses only a few variables:

i
newInterval
result

Auxiliary working space is:

O(1)

excluding the output.

Edge Cases
1. New interval before everything
intervals =
[[5,7],[10,12]]

newInterval =
[1,3]

Output:

[[1,3],[5,7],[10,12]]
2. New interval after everything
intervals =
[[1,3],[5,7]]

newInterval =
[10,12]

Output:

[[1,3],[5,7],[10,12]]
3. New interval overlaps one interval
intervals =
[[1,3],[6,9]]

newInterval =
[2,5]

Output:

[[1,5],[6,9]]
4. New interval overlaps multiple intervals
intervals =
[[1,2],[3,5],[6,7],[8,10]]

newInterval =
[4,8]

Output:

[[1,2],[3,10]]
5. New interval completely contains everything
intervals =
[[2,3],[5,7],[9,10]]

newInterval =
[1,12]

Output:

[[1,12]]
6. Touching intervals
intervals =
[[1,3],[6,9]]

newInterval =
[3,6]

Because touching intervals are merged:

[1,3]
[3,6]
[6,9]

becomes:

[[1,9]]
Key DSA Pattern
Sorted Intervals
       ↓
Before
       ↓
Overlap
       ↓
After

The most important conditions are:

Before:
interval.end < newInterval.start

Overlap:
interval.start <= newInterval.end

After:
remaining intervals