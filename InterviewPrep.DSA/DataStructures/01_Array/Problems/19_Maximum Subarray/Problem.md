Array Problem 19 — Maximum Subarray

Given an integer array nums, find the contiguous subarray with the largest sum and return its sum.

A contiguous subarray must contain consecutive elements from the original array.

Examples

Example 1

Input:  nums = [-2,1,-3,4,-1,2,1,-5,4]
Output: 6

Explanation:

The subarray:

[4,-1,2,1]

has the maximum sum:

4 + (-1) + 2 + 1 = 6

Example 2

Input:  nums = [1]
Output: 1

Example 3

Input:  nums = [5,4,-1,7,8]
Output: 23

Because:

5 + 4 + (-1) + 7 + 8 = 23
Constraints
1 <= nums.length <= 10⁵
-10⁴ <= nums[i] <= 10⁴
The subarray must be contiguous.
At least one element must be selected.