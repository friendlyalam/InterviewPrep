# Problem 50 — Maximum Sum Circular Subarray

## Problem

Given a circular integer array, find the maximum sum of a non-empty subarray.

A circular subarray can wrap from the end of the array to the beginning.

Example:

[5,-3,5]

Answer:

10

The circular subarray is:

[5] + [5]

---

# Solution 1 — Better

Try every possible starting position.

For each start position, keep adding elements while moving circularly.

Use:

(start + length) % n

to wrap around the array.

## Time Complexity

O(n²)

There can be O(n) starting positions and O(n) lengths.

## Space Complexity

O(1)

Only a few variables are required.

---

# Solution 2 — Optimal

Use Kadane's algorithm twice.

First calculate:

Maximum normal subarray

Then calculate:

Minimum subarray

Also calculate:

Total array sum

There are two possibilities.

## Case 1 — Normal Subarray

Example:

[1,-2,3,4]

Maximum normal subarray:

[3,4]

Sum:

7

---

## Case 2 — Circular Subarray

Suppose:

[5,-3,5]

Total:

7

Minimum subarray:

[-3]

Therefore remove the minimum subarray:

7 - (-3)

= 10

So:

Circular maximum =
total sum - minimum subarray

---

# Important Edge Case

Consider:

[-5,-2,-8]

Total:

-15

Minimum subarray:

-15

Then:

total - minimum
=
-15 - (-15)
=
0

But 0 is invalid because the problem requires a NON-EMPTY subarray.

Therefore, when all numbers are negative, return the normal Kadane maximum.

Answer:

-2

---

# Dry Run

Input:

[5,-3,5]

Maximum Kadane:

5
2
7

maxSum = 7

Minimum Kadane:

5
-3
2

minSum = -3

Total:

7

Circular:

7 - (-3)
= 10

Answer:

10

---

# Time Complexity

O(n)

The array is traversed once.

# Space Complexity

O(1)

Only variables are used.

---

# Edge Cases

- null array
- empty array
- one element
- all negative values
- all positive values
- circular maximum larger than normal maximum
- duplicate values

## Key DSA Pattern

Kadane's Algorithm + Complementary Subarray

Important formula:

circular maximum =
total sum - minimum subarray sum

But this formula cannot be used when all elements are negative because it would create an empty subarray.