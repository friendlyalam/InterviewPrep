Problem 34 — Search in Rotated Sorted Array

This is a new problem and has not been solved in our previous 33 problems.

It introduces an important technique we haven't covered yet: Binary Search on a rotated sorted array.

Problem Statement

You are given an integer array nums that was originally sorted in ascending order, but it has been rotated at some unknown position.

Given a target, return the index of target if it exists.

Otherwise, return -1.

Important
All elements are distinct.
The array was originally sorted in ascending order.
The array may have been rotated.
You must not modify the array.
Examples
Input:
nums = [4,5,6,7,0,1,2]
target = 0

Output:
4
Input:
nums = [4,5,6,7,0,1,2]
target = 3

Output:

-1
Input:

nums = [1]
target = 1

Output:
0