Standard Array Validation

We'll use this checklist whenever it makes sense for an array problem.

A. Null array
input = null

Expected behavior: reject invalid input.

B. Empty array
input = []

Expected behavior: reject invalid input when the problem requires at least one element.

C. Single element
[50]

Expected:

50
D. Two elements
[10, 20]

Expected:

20
E. Positive numbers
[10, 25, 7, 90, 15]

Expected:

90
F. Negative numbers
[-10, -25, -7, -90]

Expected:

-7

This is particularly important because it catches the common mistake:

int largest = 0;
G. Mixed positive and negative
[-10, 25, -7, 90, -15]

Expected:

90
H. All elements equal
[5, 5, 5, 5]

Expected:

5
I. Maximum at first position
[100, 20, 30, 40]

Expected:

100
J. Maximum at last position
[10, 20, 30, 100]

Expected:

100
K. int.MinValue
[int.MinValue, -10, -100]

Expected:

-10
L. int.MaxValue
[10, int.MaxValue, 500]

Expected:

int.MaxValue

These boundary values are especially useful in C#.