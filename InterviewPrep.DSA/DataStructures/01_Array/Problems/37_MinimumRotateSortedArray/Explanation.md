# Problem 37 — Find the Minimum in a Rotated Sorted Array

## Problem

Given an array of distinct integers that was originally sorted in ascending
order and then rotated, find the minimum element.

Example:

[4,5,6,7,0,1,2]

Output:

0

---

# Solution 1 — Better

## Why

The simplest approach is to scan the complete array and keep track of the
smallest value found so far.

## Idea

Start with:

minimum = nums[0]

For every remaining element:

If nums[i] < minimum:

minimum = nums[i]

At the end, minimum contains the smallest value.

## Time Complexity

O(n)

Every element may need to be checked.

## Space Complexity

O(1)

Only one variable is used to store the minimum.

---

# Solution 2 — Optimal

## Why

The array was originally sorted.

After rotation, it contains two sorted portions.

Example:

[4,5,6,7,0,1,2]

The minimum is located around the rotation point.

We can use Binary Search to locate it.

---

# Key Observation

Compare:

nums[mid]

with:

nums[right]

There are two cases.

---

## Case 1

nums[mid] > nums[right]

Example:

[4,5,6,7,0,1,2]

mid = 3

nums[mid] = 7
nums[right] = 2

7 > 2

The minimum must be to the right of mid.

Therefore:

left = mid + 1

---

## Case 2

nums[mid] < nums[right]

Example:

[6,7,0,1,2,3,4]

nums[mid] = 0
nums[right] = 4

0 < 4

The portion from mid to right is sorted.

Therefore the minimum is either:

1. nums[mid]
2. somewhere to the left

So:

right = mid

Important:

We do NOT use:

right = mid - 1

because nums[mid] itself might be the minimum.

---

# Dry Run

Input:

[4,5,6,7,0,1,2]

Initial:

left = 0
right = 6

---

## Iteration 1

mid = 3

nums[mid] = 7
nums[right] = 2

7 > 2

Minimum is to the right.

left = 4

---

## Iteration 2

mid = 5

nums[mid] = 1
nums[right] = 2

1 < 2

Minimum is at mid or to the left.

right = 5

---

## Iteration 3

mid = 4

nums[mid] = 0
nums[right] = 1

0 < 1

Minimum is at mid or to the left.

right = 4

---

Now:

left = 4
right = 4

Return:

nums[4] = 0

---

# Why O(log n)?

At every iteration, approximately half of the possible positions are removed.

For example:

n
n/2
n/4
n/8
...

Therefore:

O(log n)

---

# Space Complexity

O(1)

Only:

left
right
mid

are used.

No additional array, HashSet or Dictionary is required.

---

# Edge Cases

1. Null array
2. Empty array
3. Single element
4. Array is not rotated
5. Rotation by one position
6. Minimum is at the beginning
7. Minimum is near the end
8. Two-element array
9. Negative values
10. Mixed positive and negative values

---

# Key DSA Pattern

Binary Search on a Rotated Sorted Array

Important comparison:

nums[mid] > nums[right]

    -> minimum is right of mid

nums[mid] < nums[right]

    -> minimum is at mid or left of mid

Remember:

right = mid

not:

right = mid - 1

because mid can itself be the minimum.