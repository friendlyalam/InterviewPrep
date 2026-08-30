Array — Problem 12: Find the Last Occurrence of a Target

Given an integer array and a target integer, find the index of the last occurrence of the target.

If the target does not exist, return -1.

Examples

Input:  [10, 20, 30, 20, 40]
Target: 20
Output: 3

Input:  [5, 8, 5, 10, 5]
Target: 5
Output: 4

Input:  [10, 20, 30]
Target: 50
Output: -1

Input:  [-5, 10, -5, 20, -5]
Target: -5
Output: 4

Requirements:
Handle null input appropriately.
Handle an empty array appropriately.
Target can be positive, negative, or zero.
If the target occurs multiple times, return the last index.
If the target doesn't exist, return -1.
Do not use Array.LastIndexOf() or LINQ.
Try to think about whether the search direction can help.