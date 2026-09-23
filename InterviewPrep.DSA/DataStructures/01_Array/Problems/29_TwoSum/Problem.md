Array Problem 29 — Two Sum
Problem Statement

Given an integer array nums and an integer target, return the indices of the two numbers whose sum is equal to target.

You may assume that exactly one valid pair exists.

You cannot use the same element twice.

Return the indices in any order.

Examples

Example 1

Input:  nums = [2,7,11,15], target = 9
Output: [0,1]

Example 2

Input:  nums = [3,2,4], target = 6
Output: [1,2]

Example 3

Input:  nums = [3,3], target = 6
Output: [0,1]
Requirements
Return exactly two indices.
The two indices must be different.
nums[i] + nums[j] == target.
The original array does not need to be modified.
Do not assume the array is sorted.
Constraints
2 <= nums.Length <= 100,000
-10^9 <= nums[i] <= 10^9
-10^9 <= target <= 10^9
Exactly one valid pair exists.