# Problem 34 — Search in Rotated Sorted Array

## Problem

Given an array that was originally sorted in ascending order but rotated at an unknown position, find the index of a target value.

All elements are distinct.

Return -1 if the target does not exist.

Example:

nums = [4,5,6,7,0,1,2]
target = 0

Output:

4

---

# Solution 1 — Better

## Why

The simplest solution is to scan the entire array.

For every element:

- If nums[i] == target, return i.
- Otherwise continue searching.

If the target does not exist, return -1.

## Idea

Use a normal linear traversal.

## Time Complexity

O(n)

In the worst case, we may need to inspect every element.

For example:

[4,5,6,7,0,1,2]

If target = 10, every element must be checked.

## Space Complexity

O(1)

Only the loop variable is required.

---

# Solution 2 — Optimal

## Key Observation

Although the array has been rotated, at least one half of the current search range is always sorted.

Example:

[4,5,6,7,0,1,2]

If:

left = 0
right = 6
mid = 3

Then:

nums[mid] = 7

Left half:

[4,5,6,7]

is sorted.

Right half:

[0,1,2]

is also sorted in this particular case, but the important property is that we can identify a sorted half using:

nums[left] <= nums[mid]

---

# Main Idea

At every iteration:

1. Calculate mid.
2. Check whether nums[mid] is the target.
3. Determine which half is sorted.
4. Check whether the target belongs to that sorted half.
5. Search only the appropriate half.

This allows us to discard approximately half of the search space.

---

# Case 1 — Left Half Is Sorted

Condition:

nums[left] <= nums[mid]

Example:

[4,5,6,7,0,1,2]
 L     M

The left portion:

[4,5,6,7]

is sorted.

Now check:

nums[left] <= target < nums[mid]

If true:

The target is inside the sorted left half.

Therefore:

right = mid - 1

Otherwise:

The target must be in the right half.

Therefore:

left = mid + 1

---

# Case 2 — Right Half Is Sorted

If the left half is not sorted, then the right half must be sorted.

Example:

[6,7,0,1,2,4,5]
 L     M     R

Right portion:

[1,2,4,5]

is sorted.

Check:

nums[mid] < target <= nums[right]

If true:

The target belongs to the right half.

Therefore:

left = mid + 1

Otherwise:

The target must be in the left half.

Therefore:

right = mid - 1

---

# Dry Run

Input:

nums = [4,5,6,7,0,1,2]
target = 0

Initial:

left = 0
right = 6

---

## Iteration 1

mid = 3

nums[mid] = 7

Target:

0

7 != 0

Check left half:

nums[left] <= nums[mid]

4 <= 7

True.

Therefore left half is sorted:

[4,5,6,7]

Does target 0 belong here?

4 <= 0 < 7

False.

Therefore target must be on the right.

Move:

left = mid + 1

left = 4

---

## Iteration 2

Current range:

[0,1,2]

left = 4
right = 6

mid = 5

nums[mid] = 1

1 != 0

Left half:

[0,1]

is sorted.

Check:

nums[left] <= target < nums[mid]

0 <= 0 < 1

True.

Therefore:

right = mid - 1

right = 4

---

## Iteration 3

left = 4
right = 4

mid = 4

nums[mid] = 0

Target found.

Return:

4

---

# Time Complexity

O(log n)

Each iteration eliminates approximately half of the remaining search space.

This is the same fundamental reason normal binary search is O(log n).

---

# Space Complexity

O(1)

Only a few variables are used:

left
right
mid

No additional data structure is required.

---

# Edge Cases

1. Null array
2. Empty array
3. Single-element array
4. Target is the first element
5. Target is the last element
6. Target does not exist
7. Array is not rotated
8. Rotation occurs near the beginning
9. Rotation occurs near the end
10. Target is the rotation point
11. Array contains only two elements

---

# Key DSA Pattern

Modified Binary Search

Important concept:

At least one half of a rotated sorted array is sorted.

Pattern:

1. Find mid.
2. Identify the sorted half.
3. Check whether target belongs to that half.
4. Discard the other half.
5. Repeat.

Complexity:

O(log n) time
O(1) space