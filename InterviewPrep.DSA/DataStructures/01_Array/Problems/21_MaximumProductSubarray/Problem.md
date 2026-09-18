Array Problem 21 — Maximum Product Subarray

Given an integer array nums, find the contiguous subarray that has the largest product and return its product.

Examples

Example 1

Input:  nums = [2,3,-2,4]
Output: 6

Explanation:

[2,3] → 2 × 3 = 6

Example 2

Input:  nums = [-2,0,-1]
Output: 0

Example 3

Input:  nums = [-2,3,-4]
Output: 24

Explanation:

[-2,3,-4] → (-2) × 3 × (-4) = 24

Example 4

Input:  nums = [-2]
Output: -2
Constraints / Requirements
The subarray must be contiguous.
The subarray must contain at least one element.
nums can contain:
Positive numbers
Negative numbers
Zero
Return the maximum product, not the subarray itself.
Handle a single-element array.
Handle arrays containing multiple zeroes.
Handle arrays containing all negative numbers.