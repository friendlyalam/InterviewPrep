Array Problem 30 — Longest Subarray with Sum K

Given an integer array nums and an integer k, find the length of the longest contiguous subarray whose sum is exactly k.

A subarray must contain contiguous elements.

Examples

Example 1

Input:
nums = [10, 5, 2, 7, 1, 9]
k = 15

Output:
4

Explanation:
[5, 2, 7, 1] has sum 15
Length = 4

Example 2

Input:
nums = [-1, 2, 3, -2, 5]
k = 3

Output:
4

Explanation:
[-1, 2, 3, -2, 5] has sum 7, not 3.

[2, 3, -2] has sum 3
Length = 3

Example 3

Input:
nums = [1, 2, 3]
k = 10

Output:
0

Example 4

Input:
nums = [5, -2, 3, 1, 2]
k = 4

Output:
4
Requirements
Return the maximum length of a contiguous subarray whose sum equals k.
If no such subarray exists, return 0.
The array may contain:
positive numbers
negative numbers
zero
Do not modify the input array.
Handle null and empty arrays appropriately.
Constraints
1 <= nums.Length <= 100,000
-10,000 <= nums[i] <= 10,000
-10^9 <= k <= 10^9