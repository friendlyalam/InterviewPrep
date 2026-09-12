Array Problem 26 — Rotate Array
Problem Statement

Given an integer array nums, rotate the array to the right by k steps.

A right rotation by one step means the last element moves to the first position.

Examples

Example 1

Input:  nums = [1,2,3,4,5,6,7], k = 3
Output: [5,6,7,1,2,3,4]

Example 2

Input:  nums = [-1,-100,3,99], k = 2
Output: [3,99,-1,-100]

Example 3

Input:  nums = [1,2], k = 5
Output: [2,1]
Requirements
Rotate the array in-place.
Do not create another array for the result.
k can be greater than the array length.
Return the modified array.
Constraints
1 <= nums.Length <= 100,000
-2³¹ <= nums[i] <= 2³¹ - 1
0 <= k <= 10⁹



Example 1: k = 3
[1, 2, 3, 4, 5, 6, 7]

Move the last 3 elements to the front:

Last 3:       [5, 6, 7]
Remaining:    [1, 2, 3, 4]

Result:       [5, 6, 7, 1, 2, 3, 4]

So the split is:

[1, 2, 3, 4 | 5, 6, 7]
              ↑
          start here

That's why it starts after 4 elements.

Example 2: k = 2
[-1, -100, 3, 99]

Move the last 2 elements to the front:

Last 2:       [3, 99]
Remaining:    [-1, -100]

Result:       [3, 99, -1, -100]

So the split is:

[-1, -100 | 3, 99]
             ↑
         start here

That's why it starts after 2 elements.

The simple rule

If array length is n:

Start index = n - k

For the first example:

n = 7
k = 3

7 - 3 = 4

Start at index 4 → 5.

For the second:

n = 4
k = 2

4 - 2 = 2

Start at index 2 → 3.

So k tells us how many elements come from the end, while n - k tells us where that ending portion starts.
