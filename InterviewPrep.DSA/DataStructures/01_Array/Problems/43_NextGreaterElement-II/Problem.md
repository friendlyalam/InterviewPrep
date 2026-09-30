Problem 43 — Next Greater Element II

Given a circular array nums, for every element, find the next greater element.

The next greater element of nums[i] is the first element greater than nums[i] encountered while moving to the right.

Because the array is circular, after reaching the last element, continue from the beginning.

If no greater element exists, return -1 for that position.

Example 1
Input:
nums = [1, 2, 1]

Output:
[2, -1, 2]

Explanation:

1 → next greater is 2
2 → no greater element → -1
1 → circularly reaches 2 → 2
Example 2
Input:
nums = [1, 2, 3, 4, 3]

Output:
[2, 3, 4, -1, 4]
Example 3
Input:
nums = [5, 4, 3, 2, 1]

Output:
[-1, 5, 5, 5, 5]
Constraints
1 <= nums.Length <= 10⁵
-10⁹ <= nums[i] <= 10⁹
Return an array of the same length as nums.