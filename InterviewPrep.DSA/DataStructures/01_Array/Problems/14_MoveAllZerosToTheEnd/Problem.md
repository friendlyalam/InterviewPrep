Array — Problem 14: Move All Zeros to the End

Given an integer array, move all 0 values to the end of the array while maintaining the relative order of the non-zero elements.

The operation must be performed in place.

Examples
Input:  [0, 1, 0, 3, 12]
Output: [1, 3, 12, 0, 0]

Input:  [1, 2, 3]
Output: [1, 2, 3]

Input:  [0, 0, 1, 2]
Output: [1, 2, 0, 0]

Input:  [1, 0, 2, 0, 3]
Output: [1, 2, 3, 0, 0]

Input:  [0]
Output: [0]

Requirements:
Handle null appropriately.
Handle an empty array appropriately.
Perform the operation in place.
Maintain the relative order of non-zero elements.
Don't create another array.
Don't use LINQ.
Think about whether two pointers can help.