Problem 36 — Find Peak Element

This is a new problem. We have not solved it before.

It introduces another important Binary Search pattern: using binary search on a condition rather than directly searching for a target.

Problem Statement

Given an integer array nums, find a peak element.

A peak element is an element that is strictly greater than its neighboring elements.

Return the index of any peak element.

Important rules
For the first element, only the right neighbor matters.
For the last element, only the left neighbor matters.
You may assume nums[-1] = -∞ and nums[n] = -∞.
The array may contain multiple peaks.
Return the index of any valid peak.
Required optimal complexity: O(log n).

Examples
Input:
[1, 2, 3, 1]

Output:
2

Because:

1 < 2 < 3 > 1
        ↑
       peak

Another example:

Input:
[1, 2, 1, 3, 5, 6, 4]

Output:
1

index 1 is a valid peak because 2 > 1 and 2 > 1.

Index 5 is also a valid peak.