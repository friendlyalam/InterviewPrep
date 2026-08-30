Array — Problem 11: Find the First Occurrence of a Target

Given an integer array and a target integer, find the index of the first occurrence of the target.

If the target does not exist, return -1.

Examples
Input:  [10, 20, 30, 20, 40], target = 20
Output: 1

Input:  [5, 8, 5, 10, 5], target = 5
Output: 0

Input:  [10, 20, 30], target = 50
Output: -1

Input:  [-5, 10, -5, 20], target = -5
Output: 0

Requirements:

Handle null input appropriately.
Handle an empty array appropriately.
The target can be positive, negative, or zero.
If the target appears multiple times, return the first index.
If the target doesn't exist, return -1.
Do not use Array.IndexOf() or LINQ.