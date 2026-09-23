Array Problem 32 — Maximum Sum Subarray of Size K

Given an integer array nums and an integer k, find the maximum sum of any contiguous subarray of exactly size k.

Example 1
Input:
nums = [2, 1, 5, 1, 3, 2]
k = 3

Output:
9

Explanation:
[5, 1, 3] has the maximum sum:
5 + 1 + 3 = 9
Example 2
Input:
nums = [2, 3, 4, 1, 5]
k = 2

Output:
7

Explanation:
[3, 4] has the maximum sum:
3 + 4 = 7
Example 3
Input:
nums = [-5, -2, -3, -1]
k = 2

Output:

-4

Explanation:
[-2, -3] has the maximum sum:
-2 + (-3) = -5

[-3, -1] = -4

[-5, -2] = -7

Therefore the maximum is -4.
Requirements
Return the maximum sum of a contiguous subarray.
The subarray must contain exactly k elements.
The array can contain positive, negative, and zero values.
Do not modify the input array.
Handle invalid input appropriately.
Constraints
1 <= nums.Length <= 100,000
1 <= k <= nums.Length
-10,000 <= nums[i] <= 10,000