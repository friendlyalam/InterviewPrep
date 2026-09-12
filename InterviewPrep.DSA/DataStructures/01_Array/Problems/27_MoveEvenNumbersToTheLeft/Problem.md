Array Problem 27 — Move Even Numbers to the Left
Problem Statement

Given an integer array nums, rearrange the array so that all even numbers appear before all odd numbers.

The relative order of the even numbers and odd numbers does not need to be preserved.

Return the modified array.

Examples

Example 1

Input:  [3, 1, 2, 4]
Output: [4, 2, 1, 3]

Example 2

Input:  [1, 2, 3, 4, 5, 6]
Output: [6, 2, 4, 3, 5, 1]

Example 3

Input:  [2, 4, 6]
Output: [2, 4, 6]

Example 4

Input:  [1, 3, 5]
Output: [1, 3, 5]
Requirements
Perform the rearrangement in-place.
Do not create another array for the result.
All even numbers must be before all odd numbers.
Relative order does not matter.
Constraints
1 <= nums.Length <= 100,000
-10^9 <= nums[i] <= 10^9