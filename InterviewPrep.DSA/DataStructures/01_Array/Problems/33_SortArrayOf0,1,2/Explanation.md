# Problem 33 — Sort an Array of 0s, 1s and 2s

## Problem

Given an array containing only 0, 1 and 2, sort the array in ascending order.

The array must be modified in-place.

Example:

Input:
[2, 0, 2, 1, 1, 0]

Output:
[0, 0, 1, 1, 2, 2]

---

# Solution 1 — Better

## Why

A simple way is to count the number of 0s, 1s and 2s.

Then overwrite the original array in sorted order.

For example:

Input:

[2, 0, 2, 1, 1, 0]

Counts:

0 -> 2
1 -> 2
2 -> 2

Rewrite:

[0, 0, 1, 1, 2, 2]

## Idea

1. Count all 0s.
2. Count all 1s.
3. Count all 2s.
4. Fill the array with 0s.
5. Fill the remaining positions with 1s.
6. Fill the remaining positions with 2s.

## Time Complexity

O(n)

There are several loops, but they are sequential:

O(n) + O(n) + O(n) = O(n)

## Space Complexity

O(1)

Only a few integer variables are used.

---

# Solution 2 — Optimal

## Why

We can solve the problem in one traversal using the Dutch National Flag Algorithm.

The array is divided into four regions:

0s | 1s | Unknown | 2s

We use three pointers:

low
mid
high

## Initial State

low = 0
mid = 0
high = n - 1

## Rules

### Case 1 — nums[mid] == 0

0 belongs on the left.

Swap:

nums[low] and nums[mid]

Then:

low++
mid++

---

### Case 2 — nums[mid] == 1

1 already belongs in the middle.

Simply:

mid++

---

### Case 3 — nums[mid] == 2

2 belongs on the right.

Swap:

nums[mid] and nums[high]

Then:

high--

Important:

Do NOT increment mid.

The value swapped from high is still unknown and must be examined.

---

# Dry Run

Input:

[2, 0, 2, 1, 1, 0]

Initial:

low = 0
mid = 0
high = 5

Array:

[2, 0, 2, 1, 1, 0]

---

nums[mid] = 2

Swap mid and high:

[0, 0, 2, 1, 1, 2]

high = 4

mid stays 0.

---

nums[mid] = 0

Swap low and mid.

Nothing changes:

[0, 0, 2, 1, 1, 2]

low = 1
mid = 1

---

nums[mid] = 0

Already belongs to left.

low = 2
mid = 2

---

nums[mid] = 2

Swap mid and high:

[0, 0, 1, 1, 2, 2]

high = 3

mid stays 2.

---

nums[mid] = 1

1 belongs in the middle.

mid = 3

---

nums[mid] = 1

mid = 4

Now:

mid > high

Stop.

Final:

[0, 0, 1, 1, 2, 2]

---

# Time Complexity

O(n)

Each element is processed a limited number of times.

---

# Space Complexity

O(1)

Only three pointers are used:

low
mid
high

No additional collection is required.

---

# Edge Cases

1. Null array
2. Empty array
3. One element
4. All 0s
5. All 1s
6. All 2s
7. Only 0s and 1s
8. Only 1s and 2s
9. Only 0s and 2s
10. Mixed 0s, 1s and 2s
11. Invalid values such as 3 or -1

---

# Key DSA Pattern

Dutch National Flag / 3-Way Partitioning

This pattern is useful when an array needs to be divided into three categories.

Important pattern:

0s | 1s | Unknown | 2s

Pointers:

low
mid
high