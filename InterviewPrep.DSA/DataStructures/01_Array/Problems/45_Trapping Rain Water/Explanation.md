# Problem 45 — Trapping Rain Water

## Problem

Given an array where height[i] represents the height of a bar,
calculate how much rainwater can be trapped.

Example:

height = [0,1,0,2,1,0,1,3,2,1,2,1]

Output:

6

---

# Core Idea

Water can be trapped at index i only when there is a taller
or equal boundary on both sides.

For each position:

    water[i] =
        min(max height on left,
            max height on right)
        - height[i]

Example:

    left boundary  = 3
    right boundary = 2
    current height = 1

Water:

    min(3, 2) - 1
    = 1

---

# Solution 1 — Prefix/Suffix Maximum

## Why

For every position we need:

1. Maximum height on the left.
2. Maximum height on the right.

We can calculate both in advance.

---

## Idea

Create:

leftMax[i]

which stores the maximum height from index 0 to i.

Create:

rightMax[i]

which stores the maximum height from index i to the end.

Then:

water[i] =
min(leftMax[i], rightMax[i]) - height[i]

---

## Dry Run

Consider:

[4, 2, 0, 3, 2, 5]

Left maximum:

[4, 4, 4, 4, 4, 5]

Right maximum:

[5, 5, 5, 5, 5, 5]

For index 2:

height = 0

left maximum = 4
right maximum = 5

Water:

min(4, 5) - 0

= 4

For index 4:

height = 2

left maximum = 4
right maximum = 5

Water:

min(4, 5) - 2

= 2

Total:

9

---

## Time Complexity

O(n)

## Why?

We make:

1. One pass to build leftMax.
2. One pass to build rightMax.
3. One pass to calculate water.

Therefore:

O(n) + O(n) + O(n)

= O(n)

---

## Space Complexity

O(n)

## Why?

We store two arrays:

leftMax
rightMax

Each contains n elements.

---

# Solution 2 — Two Pointers

## Why

The prefix/suffix solution uses extra arrays.

But we don't actually need to store the maximum values
for every position.

We can maintain:

leftMax
rightMax

while using two pointers.

---

## Idea

Use:

left = 0
right = n - 1

Maintain:

leftMax  = maximum height seen from the left
rightMax = maximum height seen from the right

At every step:

    if height[left] <= height[right]

we process the left side.

Otherwise:

we process the right side.

---

# Why Can We Process the Smaller Side?

Suppose:

height[left] <= height[right]

The right side currently has a boundary at least as high as
the left boundary.

Therefore, the water at the left position is determined by
leftMax.

We do not need to know the exact maximum on the right yet.

The right boundary is already high enough.

The same logic applies symmetrically when:

height[right] < height[left]

---

# Dry Run

height:

[4, 2, 0, 3, 2, 5]

Start:

left = 0
right = 5

leftMax = 0
rightMax = 0
water = 0

---

## Step 1

height[left] = 4

height[right] = 5

4 <= 5

Process left.

4 becomes leftMax.

left moves forward.

---

## Step 2

left height = 2

right height = 5

2 <= 5

leftMax = 4

Water:

4 - 2 = 2

---

## Step 3

left height = 0

leftMax = 4

Water:

4 - 0 = 4

---

Continue processing both sides.

Total trapped water:

9

---

# Time Complexity

O(n)

## Why?

Each pointer moves only toward the other pointer.

Each index is processed at most once.

Therefore:

O(n)

---

# Space Complexity

O(1)

## Why?

Only a few variables are maintained:

left
right
leftMax
rightMax
totalWater

No additional array or collection is required.

---

# Edge Cases

## 1. Empty array

Input:

[]

Invalid according to our method validation.

---

## 2. Null array

Input:

null

Throws:

ArgumentNullException

---

## 3. One or two bars

Example:

[1]

or:

[1, 2]

Output:

0

At least three bars are required to trap water.

---

## 4. Increasing heights

Input:

[1, 2, 3, 4]

Output:

0

There is no right-side valley.

---

## 5. Decreasing heights

Input:

[4, 3, 2, 1]

Output:

0

There is no left-side valley.

---

## 6. All equal

Input:

[3, 3, 3, 3]

Output:

0

---

## 7. Negative height

Negative heights are invalid.

The method throws ArgumentException.

---

# Key DSA Patterns

- Prefix Maximum
- Suffix Maximum
- Two Pointers
- Array traversal

---

# Important Interview Pattern

When you see a problem asking for:

    maximum on left
    maximum on right

first think about:

    Prefix/Suffix arrays

Then ask:

    Can I reduce the extra space?

If yes, look for:

    Two Pointers