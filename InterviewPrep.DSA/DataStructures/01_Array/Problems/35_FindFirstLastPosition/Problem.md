Problem 35 — Find First and Last Position of Element in Sorted Array

Problem Statement

Given a sorted integer array nums in non-decreasing order and an integer target, find the starting and ending position of target.

If target does not exist, return:

[-1, -1]

You must solve this using O(log n) time.

Examples
Input:
nums = [5,7,7,8,8,10]
target = 8

Output:
[3,4]

Input:
nums = [5,7,7,8,8,10]
target = 6

Output:
[-1,-1]

Input:
nums = []
target = 0

Output:
[-1,-1]
Requirements
Array is sorted in non-decreasing order.
Duplicate values are allowed.
Return [firstIndex, lastIndex].
Do not modify the array.
Required time complexity: O(log n).
Handle null and empty arrays appropriately.