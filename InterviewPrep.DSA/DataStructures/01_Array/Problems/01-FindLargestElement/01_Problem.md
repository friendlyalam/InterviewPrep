1. Problem Statement

Given an integer array, find the largest element in the array.

Example
Input:
[10, 25, 7, 90, 15]


Output:
90


Find Largest Element — Optimal Solution
1. Approach

Use a Linear Scan.

Validate the input.
Assume the first element is the largest.
Traverse the remaining elements.
If the current element is greater than largest, update it.
Return largest.



----------
Complete Array Validation

For this problem, we'll test:

| Case              | Input                     |       Expected |
| ----------------- | ------------------------- | -------------: |
| Null              | `null`                    |      Exception |
| Empty             | `[]`                      |      Exception |
| Single element    | `[50]`                    |           `50` |
| Two elements      | `[10,20]`                 |           `20` |
| Normal            | `[10,25,7,90,15]`         |           `90` |
| All negative      | `[-10,-25,-7,-90]`        |           `-7` |
| Mixed values      | `[-10,25,-7,90,-15]`      |           `90` |
| All equal         | `[5,5,5,5]`               |            `5` |
| Maximum first     | `[100,20,30,40]`          |          `100` |
| Maximum last      | `[10,20,30,100]`          |          `100` |
| `int.MinValue`    | `[int.MinValue,-10,-100]` |          `-10` |
| `int.MaxValue`    | `[10,int.MaxValue,500]`   | `int.MaxValue` |
| Duplicate maximum | `[10,90,20,90,30]`        |           `90` |
