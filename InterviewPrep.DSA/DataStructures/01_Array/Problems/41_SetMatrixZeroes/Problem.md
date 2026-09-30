Problem 41 — Set Matrix Zeroes

Given an m × n integer matrix, if an element is 0, set its entire row and entire column to 0.

You must modify the matrix in-place.

Example 1
Input:
[
    [1,1,1],
    [1,0,1],
    [1,1,1]
]

Output:
[
    [1,0,1],
    [0,0,0],
    [1,0,1]
]
Example 2
Input:
[
    [0,1,2,0],
    [3,4,5,2],
    [1,3,1,5]
]

Output:
[
    [0,0,0,0],
    [0,4,5,0],
    [0,3,1,0]
]
Requirements / Validation
matrix must not be null.
Matrix must not be empty.
Matrix must have at least one column.
Every row must be non-null.
The matrix must be rectangular; every row must have the same number of columns.
Modify the original matrix in-place.
Do not allow newly created zeroes to incorrectly cause additional rows/columns to be processed.