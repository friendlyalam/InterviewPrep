Problem 42 — Spiral Matrix

Given an m × n matrix, return all its elements in spiral order.

You must traverse the matrix:

Left → Right
Top → Bottom
Right → Left
Bottom → Top

and continue inward until every element has been visited.

Example 1
Input:

[
    [1, 2, 3],
    [4, 5, 6],
    [7, 8, 9]
]

Output:

[1, 2, 3, 6, 9, 8, 7, 4, 5]
Example 2
Input:

[
    [1, 2, 3, 4],
    [5, 6, 7, 8],
    [9, 10, 11, 12]
]

Output:

[1, 2, 3, 4, 8, 12, 11, 10, 9, 5, 6, 7]