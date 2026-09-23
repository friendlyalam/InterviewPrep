Array Problem 31 — Subarray Sum Equals K

Given an integer array nums and an integer k, return the total number of contiguous subarrays whose sum is exactly k.

A subarray must contain contiguous elements.

Example 1
Input:
nums = [1, 1, 1]
k = 2

Output:
2

Explanation:
[1, 1] at indices 0–1
[1, 1] at indices 1–2
Example 2
Input:
nums = [1, 2, 3]
k = 3

Output:
2

Explanation:
[1, 2]
[3]
Example 3
Input:
nums = [1, -1, 0]
k = 0

Output:
3

Explanation:
[-1]
[1, -1]
[1, -1, 0]
Example 4
Input:
nums = [3, 4, 7, 2, -3, 1, 4, 2]
k = 7

Output:
4
Requirements
Return the count of all contiguous subarrays whose sum equals k.
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