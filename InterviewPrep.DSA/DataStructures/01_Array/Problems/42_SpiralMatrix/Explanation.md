# Problem 42 — Spiral Matrix

## Problem

Given an `m × n` matrix, return all elements in spiral order.

Example:

```text
1 2 3
4 5 6
7 8 9

Spiral traversal:

1 → 2 → 3
          ↓
4 ← 5 ← 6
↑         ↓
7 ← 8 ← 9

Result:

[1,2,3,6,9,8,7,4,5]
Better Solution
Idea

Use a separate visited matrix.

For every cell:

Add its value to the result.
Mark it as visited.
Continue in the current direction.
If the next cell is outside the matrix or already visited,
change direction.

Directions:

Right
Down
Left
Up
Direction Representation

We can represent the four directions using arrays:

rowDirections    = [0, 1, 0, -1]
columnDirections = [1, 0, -1, 0]

Therefore:

Direction 0:
row + 0
column + 1

Right
Direction 1:
row + 1
column + 0

Down
Direction 2:
row + 0
column - 1

Left
Direction 3:
row - 1
column + 0

Up

When changing direction:

direction = (direction + 1) % 4
Better Dry Run

Input:

1 2 3
4 5 6
7 8 9

Start:

current = [0,0]
direction = Right

Visit:

1
2
3

The next cell would be outside the matrix.

Change direction:

Right → Down

Visit:

6
9

Change direction:

Down → Left

Visit:

8
7

Change direction:

Left → Up

Visit:

4

Change direction:

Up → Right

Visit:

5

Final:

[1,2,3,6,9,8,7,4,5]
Better Complexity

Every cell is visited exactly once.

Therefore:

Time = O(m × n)

However, we maintain:

bool[,] visited

which requires:

Space = O(m × n)
Optimal Solution
Main Idea

We don't actually need to remember every visited cell.

Instead, maintain the boundaries of the remaining unvisited rectangle:

top
bottom
left
right

Initially:

top = 0
bottom = rows - 1
left = 0
right = columns - 1
Four Traversals

Every spiral layer consists of four steps.

1. Left → Right
2. Top → Bottom
3. Right → Left
4. Bottom → Top

After each traversal, move the corresponding boundary inward.

Step 1 — Left to Right

Traverse:

matrix[top][left → right]

Then:

top++

because the top row has been completely processed.

Step 2 — Top to Bottom

Traverse:

matrix[top → bottom][right]

Then:

right--

because the right column has been completely processed.

Step 3 — Right to Left

Before doing this, check:

top <= bottom

Why?

Because the matrix might have only one remaining row.

Traverse:

matrix[bottom][right → left]

Then:

bottom--
Step 4 — Bottom to Top

Before doing this, check:

left <= right

Why?

Because the matrix might have only one remaining column.

Traverse:

matrix[bottom → top][left]

Then:

left++
Complete Dry Run

Input:

1  2  3  4
5  6  7  8
9 10 11 12

Initial:

top = 0
bottom = 2
left = 0
right = 3
First Layer
Left → Right
1 2 3 4

Result:

[1,2,3,4]

Now:

top = 1
Top → Bottom

Right column:

8
12

Result:

[1,2,3,4,8,12]

Now:

right = 2
Right → Left

Bottom row:

11 10 9

Result:

[1,2,3,4,8,12,11,10,9]

Now:

bottom = 1
Bottom → Top

Left column:

5

Result:

[1,2,3,4,8,12,11,10,9,5]

Now:

left = 1
Second Layer

Remaining rectangle:

6 7
10 11

Current boundaries:

top = 1
bottom = 1
left = 1
right = 2

Left → Right:

6 7

Then:

top = 2

The remaining rectangle is exhausted.

Final:

[1,2,3,4,8,12,11,10,9,5,6,7]
Why The Boundary Checks Matter

Consider a single-row matrix:

1 2 3 4

After processing:

Left → Right

we increment:

top++

Now:

top > bottom

There is no bottom row left.

Therefore we must NOT traverse right → left again.

Similarly, for:

1
2
3
4

there is only one column.

After processing the right column, we must not process the same column again from bottom → top.

Therefore:

if (top <= bottom)

and:

if (left <= right)

are important.

Optimal Complexity

Every matrix element is added to the result exactly once.

Therefore:

Time = O(m × n)

The algorithm maintains only:

top
bottom
left
right

Therefore auxiliary space is:

O(1)

The output itself naturally requires:

O(m × n)

space.

Edge Cases
1. Single element
[[1]]

Result:

[1]
2. Single row
[[1,2,3,4]]

Result:

[1,2,3,4]
3. Single column
[
 [1],
 [2],
 [3],
 [4]
]

Result:

[1,2,3,4]
4. Square matrix
3 × 3
4 × 4
5. Rectangular matrix
3 × 4
4 × 3
6. Negative values

Values can be negative.

The traversal logic does not depend on the values.

7. Duplicate values

Duplicate values are allowed.

The position of the cell, not its value, determines traversal.

Key DSA Pattern
2D Array
   ↓
Boundary Traversal
   ↓
Top / Bottom / Left / Right
   ↓
Shrink boundaries

The four operations to remember:

1. Left → Right
2. Top → Bottom
3. Right → Left
4. Bottom → Top

This boundary technique is useful in many matrix traversal
and simulation problems.