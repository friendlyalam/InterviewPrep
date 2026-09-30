Problem 37 — Find the Minimum in a Rotated Sorted Array

This is a new problem. We have not solved it before.

It builds on the rotated-array idea from Problem 34, but the actual problem and binary-search logic are different.

Problem Statement

You are given an array of distinct integers that was originally sorted in ascending order and then rotated at an unknown position.

Find and return the minimum element.

Examples
Input:
[3, 4, 5, 1, 2]

Output:
1
Input:
[4, 5, 6, 7, 0, 1, 2]

Output:
0
Input:
[11, 13, 15, 17]

Output:
11
Requirements
All elements are distinct.
The original array was sorted in ascending order.
The array may or may not have been rotated.
Do not modify the array.
Handle null and empty arrays.
Optimal solution should be O(log n).