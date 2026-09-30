Problem 47 — Find the Longest Consecutive Sequence

Given an unsorted integer array nums, return the length of the longest consecutive elements sequence.

A consecutive sequence contains numbers that follow each other without gaps.

The elements in the sequence do not need to appear next to each other in the original array.

Example 1
Input:
nums = [100, 4, 200, 1, 3, 2]

Output:
4

Explanation:

[1, 2, 3, 4]

Length = 4.

Example 2
Input:
nums = [0, 3, 7, 2, 5, 8, 4, 6, 0, 1]

Output:
9

Sequence:

[0, 1, 2, 3, 4, 5, 6, 7, 8]
Example 3
Input:
nums = [1, 2, 0, 1]

Output:
3

Sequence:

[0, 1, 2]

Duplicates do not increase the sequence length.

Constraints
0 <= nums.Length <= 100000
nums[i] can be negative or positive.
Duplicate values are allowed.
The optimal solution should run in O(n) average time.
