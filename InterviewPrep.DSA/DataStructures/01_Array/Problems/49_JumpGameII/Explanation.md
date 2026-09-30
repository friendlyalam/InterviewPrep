# Problem 49 — Jump Game II

## Problem

Given an array where nums[i] represents the maximum number of positions that can be jumped from index i, find the minimum number of jumps required to reach the last index.

Example:

[2,3,1,1,4]

Answer:

2

Path:

0 → 1 → 4

---

# Solution 1 — Better

Use Dynamic Programming.

For every index, calculate the minimum number of jumps required to reach that index.

For each position, update every position that can be reached from it.

## Time Complexity

O(n²)

Each index may update many future indices.

## Space Complexity

O(n)

We store the minimum jump count for every index.

---

# Solution 2 — Optimal

Use Greedy.

Instead of calculating the minimum jumps for every index, process the array as ranges.

For the current range, calculate the farthest position that can be reached.

When we reach the end of the current range, we must make another jump.

Then the new range ends at the farthest reachable position.

## Dry Run

Input:

[2,3,1,1,4]

Start:

jumps = 0
currentEnd = 0
farthest = 0

Index 0:

nums[0] = 2

farthest = 2

We reached currentEnd.

Make jump 1.

New range ends at index 2.

Index 1:

nums[1] = 3

farthest = 4

Index 2:

nums[2] = 1

farthest remains 4.

We reached currentEnd.

Make jump 2.

Index 4 is reachable.

Answer:

2

---

# Time Complexity

O(n)

Every index is processed once.

# Space Complexity

O(1)

Only a few variables are used.

---

# Edge Cases

- null array
- empty array
- single element → 0 jumps
- large jump at first index
- many small jumps
- all values zero except guaranteed reachable cases
- duplicate jump lengths

## Key DSA Pattern

Greedy Range Expansion

The important question is:

"Within the current jump range, which position allows me to reach the farthest?"


Optimal: Greedy

The key idea is to think in ranges.

For:

[2,3,1,1,4]

Initially:

index = 0
range = [0, 0]

From index 0, we can reach:

[1, 2]

Among those positions, index 1 can reach the farthest:

1 + 3 = 4

Therefore one jump takes us into a new range:

[1, 4]

And index 4 is the destination.