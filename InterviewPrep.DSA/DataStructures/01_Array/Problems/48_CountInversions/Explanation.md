# Problem 48 — Count Inversions in an Array

## Problem

Given an integer array, count pairs `(i, j)` where:

i < j
AND
nums[i] > nums[j]

Such a pair is called an inversion.

Example:

Input:
[2, 4, 1, 3, 5]

Output:
3

Inversions:
(2,1)
(4,1)
(4,3)

---

# Solution 1 — Better

Use two nested loops.

For every `i`, compare `nums[i]` with every element after it.

If:

nums[i] > nums[j]

then we found an inversion.

## Time Complexity

O(n²)

Every element may need to be compared with every element after it.

## Space Complexity

O(1)

Only a counter and loop variables are used.

---

# Solution 2 — Optimal

Use Merge Sort.

During the merge step, both halves are already sorted.

Suppose:

Left  = [2, 4]
Right = [1, 3]

When comparing:

4 > 1

we know that all remaining elements in the left half are also greater than 1.

Therefore we can count multiple inversions at once.

Number of inversions:

mid - i + 1

This allows us to count inversions while performing merge sort.

## Time Complexity

O(n log n)

Merge sort has O(log n) levels and each level processes O(n) elements.

## Space Complexity

O(n)

A temporary array is required for merging.

---

# Dry Run

Input:

[2, 4, 1, 3, 5]

Split:

[2, 4] [1, 3, 5]

Sort left:

[2, 4]

Sort right:

[1, 3, 5]

Merge:

2 > 1

Remaining left elements:
[2, 4]

Both are greater than 1.

Therefore:

2 inversions

Then:

2 <= 3

Then:

4 > 3

Therefore:

1 inversion

Total:

3 inversions.

---

# Edge Cases

- null array
- empty array
- already sorted array → 0
- reverse sorted array
- duplicate values
- negative values
- large number of inversions

## Key DSA Pattern

Divide and Conquer + Merge Sort

The important idea is counting several inversions at once instead of checking every pair.