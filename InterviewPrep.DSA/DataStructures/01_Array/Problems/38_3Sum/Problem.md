Problem 38 — 3Sum
Problem Statement

Given an integer array nums, find all unique triplets [nums[i], nums[j], nums[k]] such that:

nums[i] + nums[j] + nums[k] = 0

The returned triplets must:

Contain three different indices.
Be unique — do not return duplicate triplets.
The order of triplets does not matter.
The order of numbers inside each triplet does not matter.
Examples
Input:
[-1, 0, 1, 2, -1, -4]

Output:
[
    [-1, -1, 2],
    [-1, 0, 1]
]

Input:
[0, 1, 1]

Output:
[]

Input:
[0, 0, 0]
Output:
[
    [0, 0, 0]
]
Requirements / Validation
nums must not be null.
nums must contain at least 3 elements.
Duplicate triplets must not be returned.
The input may contain negative, zero, and positive numbers.
Return an empty collection when no valid triplet exists.
The solution should handle duplicate values correctly.