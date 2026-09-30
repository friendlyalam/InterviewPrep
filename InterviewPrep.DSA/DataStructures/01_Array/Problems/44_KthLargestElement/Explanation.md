# Problem 44 — Kth Largest Element in an Array

## Problem

Given an integer array and k, find the kth largest element.

Example:

nums = [3, 2, 1, 5, 6, 4]
k = 2

Output:

5

---

# Solution 1 — Min Heap

## Why

We do not need to keep all elements.

We only need the k largest elements.

A min heap is useful because it always gives us the smallest element among the elements currently stored.

If the heap contains the k largest elements:

    [largest ... kth largest]

the smallest element in the heap is the kth largest element.

## Idea

For every number:

1. Add it to the min heap.
2. If the heap contains more than k elements:
   - remove the smallest element.
3. At the end, the heap contains the k largest elements.
4. The heap's minimum is the kth largest element.

## Example

nums:

[3, 2, 1, 5, 6, 4]

k = 2

After processing all elements, the heap contains:

[5, 6]

The smallest value is:

5

Therefore:

5 is the 2nd largest element.

## Time Complexity

O(n log k)

## Why?

Each element can be inserted into the heap.

Heap insertion/removal takes:

O(log k)

We process n elements.

Therefore:

O(n log k)

## Space Complexity

O(k)

## Why?

The heap never contains more than k elements.

---

# Solution 2 — Quickselect

## Key DSA Pattern

Quickselect / Partition

---

## Why

Sorting the entire array would take:

O(n log n)

But we don't actually need the entire array sorted.

We only need one position.

Quickselect allows us to find that position without completely sorting the array.

---

## Convert Kth Largest to Kth Position

Suppose:

nums = [3, 2, 1, 5, 6, 4]

Sorted ascending:

[1, 2, 3, 4, 5, 6]

For:

k = 2

The answer is:

5

Its zero-based index is:

6 - 2 = 4

General formula:

targetIndex = nums.Length - k

---

## Partition

Choose a pivot.

After partitioning:

    smaller values | pivot | larger values

The pivot is now in its correct sorted position.

For example:

    [1, 2, 3, 4, 5, 6]
             ^
           pivot

If the pivot's index is the target index, we found the answer.

Otherwise:

- If pivot index < target index:
  search the right side.

- If pivot index > target index:
  search the left side.

We do not process the irrelevant side.

---

## Dry Run

nums:

[3, 2, 1, 5, 6, 4]

k = 2

Length = 6

targetIndex:

6 - 2 = 4

We need the element at index 4 in sorted order.

After partitioning, suppose the pivot reaches:

index 2

Then:

2 < 4

So we only search the right side.

We ignore:

[0 ... 2]

and continue with:

[3 ... 5]

Eventually the target position becomes the pivot's final position.

The value at index 4 is:

5

---

# Time Complexity

Average:

O(n)

Worst case:

O(n²)

## Why?

Each partition scans the current range.

With good partitions, the search range decreases significantly.

This gives average:

O(n)

However, repeatedly choosing poor pivots can result in:

O(n²)

---

# Space Complexity

O(1) auxiliary space.

## Why?

Quickselect works directly inside the input array.

No additional array or collection proportional to n is required.

---

# Important Difference

## Min Heap

Time:

O(n log k)

Space:

O(k)

Does not require modifying the input array.

## Quickselect

Average time:

O(n)

Worst case:

O(n²)

Space:

O(1)

Modifies the input array.

---

# Edge Cases

## 1. k = 1

Find the largest element.

Example:

[3, 1, 5]

k = 1

Output:

5

---

## 2. k = nums.Length

Find the smallest element.

Example:

[3, 1, 5]

k = 3

Output:

1

---

## 3. Duplicate values

Example:

[3, 2, 3, 1, 2, 4, 5, 5, 6]

k = 4

Output:

4

---

## 4. Negative numbers

Example:

[-1, -5, -2, -3]

k = 2

Sorted:

[-5, -3, -2, -1]

Output:

-2

---

# Validation

The method validates:

1. nums cannot be null.
2. nums cannot be empty.
3. k must be at least 1.
4. k cannot be greater than nums.Length.

---

# Key DSA Patterns

- Heap / Priority Queue
- Partition
- Quickselect
- Selection problems

---

# Interview Learning

When you see:

"Find kth largest/smallest"

think about:

1. Sorting
2. Heap
3. Quickselect

For large inputs, Quickselect can provide average O(n) time.