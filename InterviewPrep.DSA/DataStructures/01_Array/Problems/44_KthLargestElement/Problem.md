Problem 44 — Kth Largest Element in an Array

Given an integer array nums and an integer k, return the kth largest element in the array.

The array may contain duplicate values.

Example 1
Input:
nums = [3, 2, 1, 5, 6, 4]
k = 2

Output:
5

Explanation:

Sorted descending:

[6, 5, 4, 3, 2, 1]

The 2nd largest element is 5.

Example 2
Input:
nums = [3, 2, 3, 1, 2, 4, 5, 5, 6]
k = 4

Output:
4
Constraints
1 <= nums.Length <= 100000
1 <= k <= nums.Length
Values can be negative.
Duplicate values are allowed.