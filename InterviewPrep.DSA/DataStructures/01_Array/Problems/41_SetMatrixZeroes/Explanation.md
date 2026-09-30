


# Problem 41 — Set Matrix Zeroes

## Problem

Given an `m × n` matrix, if any element is `0`, set its entire row
and entire column to `0`.

The modification must be done in-place.

Example:

```text
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
Important Observation

We must be careful not to immediately change rows and columns while
we are still searching for the original zeroes.

For example:

[
    [1,1,1],
    [1,0,1],
    [1,1,1]
]

If we immediately zero row 1 and column 1, we create additional zeroes.

Those newly-created zeroes must NOT be treated as original zeroes.

Therefore:

First find where the ORIGINAL zeroes are.
Then perform the modifications.
Better Solution
Idea

Use two separate arrays:

zeroRows
zeroColumns

For example:

matrix:

1 1 1
1 0 1
1 1 1

The zero is at:

row = 1
column = 1

Therefore:

zeroRows[1] = true
zeroColumns[1] = true

Then:

Find all original zeroes.
Zero marked rows.
Zero marked columns.
Better Dry Run

Input:

1 1 1
1 0 1
1 1 1
Step 1 — Find zeroes

We find:

matrix[1][1] = 0

Therefore:

zeroRows:

false
true
false

and:

zeroColumns:

false
true
false
Step 2 — Zero marked rows

Row 1 becomes:

0 0 0

Matrix:

1 1 1
0 0 0
1 1 1
Step 3 — Zero marked columns

Column 1 becomes zero:

1 0 1
0 0 0
1 0 1

Final answer:

1 0 1
0 0 0
1 0 1
Better Complexity

If there are m rows and n columns:

Finding zeroes:

O(m × n)

Zeroing rows:

O(m × n)

Zeroing columns:

O(m × n)

Overall:

O(m × n)

Space:

O(m + n)

because we use:

bool[] zeroRows
bool[] zeroColumns
Optimal Solution
Main Idea

Can we avoid these two arrays?

Yes.

We can use the matrix itself as storage.

Use:

FIRST COLUMN → row markers
FIRST ROW    → column markers

For example:

1 1 1
1 0 1
1 1 1

When we find:

matrix[1][1] == 0

we mark:

matrix[1][0] = 0
matrix[0][1] = 0

Now the matrix becomes:

1 0 1
0 0 1
1 1 1

The first column tells us:

row 1 must become zero

The first row tells us:

column 1 must become zero
Why Do We Need Two Extra Boolean Variables?

There is one problem.

The first row and first column themselves are being used as markers.

Suppose:

0 1 2
3 4 5
6 7 8

The zero is already in the first row.

If we use the first row as a marker, we could lose the information
about whether the first row originally contained zero.

Therefore we separately remember:

firstRowHasZero
firstColumnHasZero

These are only two boolean variables.

Therefore the extra space remains:

O(1)
Optimal Step-by-Step
Step 1 — Check First Row

Check whether any value in row 0 is zero.

Store:

firstRowHasZero
Step 2 — Check First Column

Check whether any value in column 0 is zero.

Store:

firstColumnHasZero
Step 3 — Create Markers

Start from:

row = 1
column = 1

For every zero:

matrix[row][0] = 0
matrix[0][column] = 0

The first column marks the row.

The first row marks the column.

Step 4 — Apply Markers

Again start from row 1 and column 1.

For each cell:

if matrix[row][0] == 0
OR
matrix[0][column] == 0

set:

matrix[row][column] = 0
Step 5 — Process First Row

If:

firstRowHasZero == true

make the entire first row zero.

Step 6 — Process First Column

If:

firstColumnHasZero == true

make the entire first column zero.

Complete Dry Run

Input:

1 2 3 4
5 6 0 8
9 10 11 12

The original zero is:

row = 1
column = 2

Mark:

matrix[1][0] = 0
matrix[0][2] = 0

Matrix becomes:

1 2 0 4
0 6 0 8
9 10 11 12

Now process internal cells.

Because:

matrix[1][0] == 0

the entire row 1 becomes zero.

Because:

matrix[0][2] == 0

column 2 becomes zero.

Final:

1 2 0 4
0 0 0 0
9 10 0 12
Important Order

The order of operations matters.

Correct:

1. Remember first row
2. Remember first column
3. Create markers
4. Apply markers
5. Zero first row
6. Zero first column

Do NOT zero the first row/column too early.

They contain important marker information.

Time Complexity

We scan the matrix a constant number of times.

Each scan processes:

m × n

cells.

Therefore:

O(m × n)
Space Complexity

We only use:

firstRowHasZero
firstColumnHasZero

Therefore:

O(1)

extra space.

The matrix itself is used for storing the markers.

Edge Cases
1. Null matrix
null

Throw:

ArgumentNullException
2. Empty matrix
[]

Throw:

ArgumentException
3. Empty row
[
    []
]

Throw:

ArgumentException
4. Null row
[
    [1,2],
    null
]

Throw:

ArgumentException
5. Non-rectangular matrix
[
    [1,2,3],
    [4,5]
]

Throw:

ArgumentException
6. Zero in first row

Must correctly zero the entire first row.

7. Zero in first column

Must correctly zero the entire first column.

8. Zero in both

Example:

0 1 2
3 4 5
6 7 0

Both first-row/first-column handling must remain correct.

Key DSA Pattern

This problem teaches:

In-place Matrix Modification
        +
Using Existing Data as Markers

The major interview technique is:

Use first row and first column
as marker storage.

This converts:

O(m + n) space

into:

O(1) extra space

while keeping:

O(m × n)

time.