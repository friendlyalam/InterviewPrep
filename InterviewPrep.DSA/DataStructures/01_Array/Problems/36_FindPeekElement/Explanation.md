# Problem 36 — Find Peak Element

## Problem

Given an integer array, find the index of any peak element.

A peak element is an element that is strictly greater than its neighbors.

For boundary elements, the missing neighbor is considered negative infinity.

Example:

[1, 2, 3, 1]

Peak:

index 2

because:

3 > 2
3 > 1

---

# Solution 1 — Better

## Why

The simplest approach is to scan the entire array.

For each element, check whether it is greater than its neighbors.

If it is, return its index.

The problem allows us to return any peak.

## Idea

For every index i:

Left condition:

i == 0
OR
nums[i] > nums[i - 1]

Right condition:

i == nums.Length - 1
OR
nums[i] > nums[i + 1]

If both conditions are true, i is a peak.

## Time Complexity

O(n)

In the worst case, we may inspect every element.

## Space Complexity

O(1)

Only a constant number of variables are used.

---

# Solution 2 — Optimal

## Key Idea

Use Binary Search.

We do not search for a particular value.

Instead, we use the direction of the slope.

Compare:

nums[mid]

with:

nums[mid + 1]

---

# Case 1 — Going Uphill

If:

nums[mid] < nums[mid + 1]

the array is increasing at this point.

Example:

1, 2, 3, 4

Since we are going uphill, a peak must exist somewhere to the right.

Therefore:

left = mid + 1

---

# Case 2 — Going Downhill

If:

nums[mid] > nums[mid + 1]

the array is decreasing at this point.

Example:

4, 3, 2, 1

A peak must exist at mid or somewhere to the left.

Therefore:

right = mid

Important:

We use:

right = mid

not:

right = mid - 1

because mid itself may be the peak.

---

# Dry Run

Input:

[1, 2, 3, 1]

Initial:

left = 0
right = 3

---

## Iteration 1

mid = 1

nums[mid] = 2
nums[mid + 1] = 3

2 < 3

We are going uphill.

Move right:

left = 2

---

## Iteration 2

mid = 2

nums[mid] = 3
nums[mid + 1] = 1

3 > 1

We are going downhill.

A peak exists at mid or before it.

right = 2

---

Now:

left = 2
right = 2

Stop.

Return:

2

---

# Why Binary Search Works

At every step:

If:

nums[mid] < nums[mid + 1]

a peak must exist to the right.

If:

nums[mid] > nums[mid + 1]

a peak exists at mid or to the left.

Therefore, approximately half of the search space can be eliminated.

---

# Time Complexity

O(log n)

Each iteration reduces the search space approximately by half.

---

# Space Complexity

O(1)

Only left, right and mid are used.

---

# Edge Cases

1. Null array
2. Empty array
3. Single element
4. Two elements increasing
5. Two elements decreasing
6. Strictly increasing array
7. Strictly decreasing array
8. Multiple peaks
9. Peak at the beginning
10. Peak at the end

---

# Important DSA Pattern

Binary Search on a Condition

Unlike normal Binary Search, we are not searching for:

target == nums[mid]

Instead, we inspect the relationship:

nums[mid] vs nums[mid + 1]

Pattern:

nums[mid] < nums[mid + 1]
    -> move right

nums[mid] > nums[mid + 1]
    -> move left/current mid

Complexity:

O(log n) time
O(1) space