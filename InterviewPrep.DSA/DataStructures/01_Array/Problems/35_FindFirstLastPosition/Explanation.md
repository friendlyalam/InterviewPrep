# Problem 35 — Find First and Last Position of Element in Sorted Array

## Problem

Given a sorted integer array in non-decreasing order and a target,
return the first and last position of the target.

If the target does not exist, return:

[-1, -1]

Example:

Input:

nums = [5,7,7,8,8,10]
target = 8

Output:

[3,4]

---

# Solution 1 — Better

## Why

The simplest solution is to traverse the entire array.

Whenever the target is found:

- Store the first occurrence.
- Keep updating the last occurrence.

## Idea

Initialize:

firstIndex = -1
lastIndex = -1

Traverse the array.

When:

nums[i] == target

If firstIndex is still -1:

firstIndex = i

Always update:

lastIndex = i

At the end:

return [firstIndex, lastIndex]

## Time Complexity

O(n)

In the worst case, every element must be checked.

## Space Complexity

O(1)

Only a few integer variables are used.

---

# Solution 2 — Optimal

## Why

The array is sorted.

Therefore, we can use Binary Search.

However, a normal binary search only finds one occurrence.

We need two positions:

1. First occurrence
2. Last occurrence

Therefore, we perform two binary searches.

---

# Finding First Occurrence

When:

nums[mid] == target

we found a target.

But there may be another target further left.

Therefore:

firstIndex = mid

and continue searching left:

right = mid - 1

If:

nums[mid] < target

the target must be on the right:

left = mid + 1

If:

nums[mid] > target

the target must be on the left:

right = mid - 1

---

# Finding Last Occurrence

When:

nums[mid] == target

we found a target.

But there may be another target further right.

Therefore:

lastIndex = mid

and continue searching right:

left = mid + 1

If:

nums[mid] < target

the target must be on the right:

left = mid + 1

If:

nums[mid] > target

the target must be on the left:

right = mid - 1

---

# Dry Run

Input:

nums = [5,7,7,8,8,10]
target = 8

Indexes:

0  1  2  3  4  5

5  7  7  8  8  10

---

## First Occurrence

Binary search eventually finds:

mid = 3

nums[3] = 8

Store:

firstIndex = 3

Continue left:

right = 2

No earlier 8 exists.

Therefore:

firstIndex = 3

---

## Last Occurrence

Binary search finds:

mid = 3

nums[3] = 8

Store:

lastIndex = 3

Continue right.

Eventually:

mid = 4

nums[4] = 8

Update:

lastIndex = 4

No later 8 exists.

Therefore:

lastIndex = 4

Final:

[3,4]

---

# Important Binary Search Pattern

Normal Binary Search:

When target is found:

return mid

First Occurrence Binary Search:

When target is found:

save mid
continue LEFT

Last Occurrence Binary Search:

When target is found:

save mid
continue RIGHT

This is an important variation of Binary Search.

---

# Time Complexity

O(log n)

We perform two binary searches:

O(log n) + O(log n)

which simplifies to:

O(log n)

---

# Space Complexity

O(1)

Only constant extra variables are used.

---

# Edge Cases

1. Null array
2. Empty array
3. Target does not exist
4. Target occurs once
5. Target occurs twice
6. Target occurs many times
7. Target occurs at the beginning
8. Target occurs at the end
9. Entire array contains the target
10. Single-element array
11. Negative values
12. Duplicate values

---

# Key DSA Pattern

Binary Search — First/Last Occurrence

Core idea:

When target is found, do not immediately return.

For first occurrence:

Move LEFT.

For last occurrence:

Move RIGHT.

Pattern:

First:
found -> save -> left

Last:
found -> save -> right