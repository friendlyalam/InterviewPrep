Array Problem 25 — Merge Sorted Array
Problem Statement

You are given two sorted integer arrays nums1 and nums2.

nums1 has a size of m + n.
The first m elements of nums1 contain valid values.
The remaining n elements of nums1 are 0 placeholders and should be ignored.
nums2 contains n sorted elements.

Merge nums2 into nums1 so that nums1 becomes one sorted array.

The merge must be performed in-place inside nums1.

Examples

Example 1

Input:
nums1 = [1,2,3,0,0,0]
m = 3
nums2 = [2,5,6]
n = 3

Output:
[1,2,2,3,5,6]

Example 2

Input:
nums1 = [1]
m = 1
nums2 = []
n = 0

Output:
[1]

Example 3

Input:
nums1 = [0]
m = 0
nums2 = [1]
n = 1

Output:
[1]

Constraints
nums1 is sorted in non-decreasing order.
nums2 is sorted in non-decreasing order.
nums1.Length == m + n.
nums2.Length == n.
The first m elements of nums1 are valid values.
The last n positions of nums1 are available for the merged result.
The final result must be stored inside nums1.
Do not create another array for the merged result.