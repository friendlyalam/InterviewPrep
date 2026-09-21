# Array Problem 24 — Intersection of Two Arrays II

## Problem

Given two integer arrays `nums1` and `nums2`, return their intersection.

Each element in the result must appear as many times as it appears in both arrays.

The order of the result does not matter.

---

# Example

```text
nums1 = [1,2,2,1]
nums2 = [2,2]

Output = [2,2]
```

The number `2` appears twice in both arrays, so it appears twice in the result.

---

# Better Approach — Dictionary Frequency

## Why?

A `HashSet` is not sufficient because a `HashSet` stores only unique values.

For example:

```text
[2,2,2]
```

becomes:

```text
[2]
```

Instead, we need to remember how many times each number appears.

A `Dictionary<int,int>` can store:

```text
number → frequency
```

---

## Idea

1. Store the frequency of every number in `nums1`.
2. Traverse `nums2`.
3. If the current number exists in the dictionary and its frequency is greater than zero:

   * add it to the result
   * decrease its frequency.
4. Continue until `nums2` is exhausted.

---

## Dry Run

```text
nums1 = [1,2,2,1]
nums2 = [2,2]
```

Frequency dictionary:

```text
1 → 2
2 → 2
```

Process first `2`:

```text
result = [2]
2 → 1
```

Process second `2`:

```text
result = [2,2]
2 → 0
```

Final result:

```text
[2,2]
```

---

# Optimal Approach — Sorting + Two Pointers

## Why?

After sorting both arrays, equal values are next to each other.

This allows us to compare the arrays using two pointers instead of repeatedly searching.

---

## Idea

Sort both arrays.

Use:

```text
first
second
```

to point to the current elements.

### If equal

```text
nums1[first] == nums2[second]
```

Add the value to the result and move both pointers.

### If nums1 is smaller

```text
nums1[first] < nums2[second]
```

Move `first`.

### Otherwise

Move `second`.

---

## Dry Run

Input:

```text
nums1 = [1,2,2,1]
nums2 = [2,2]
```

After sorting:

```text
nums1 = [1,1,2,2]
nums2 = [2,2]
```

Start:

```text
first = 0
second = 0
```

Compare:

```text
1 < 2
```

Move `first`.

```text
first = 1
second = 0
```

Compare:

```text
1 < 2
```

Move `first`.

```text
first = 2
second = 0
```

Compare:

```text
2 == 2
```

Add `2`.

```text
result = [2]

first = 3
second = 1
```

Compare:

```text
2 == 2
```

Add `2`.

```text
result = [2,2]
```

Both arrays are exhausted.

---

# Time Complexity

Sorting:

```text
O(n log n)
```

and:

```text
O(m log m)
```

Two-pointer traversal:

```text
O(n + m)
```

Overall:

```text
O(n log n + m log m)
```

---

# Why This Complexity Occurs

The dominant operation is sorting.

After sorting, each array is traversed only once.

Therefore:

```text
O(n log n + m log m) + O(n + m)
```

simplifies to:

```text
O(n log n + m log m)
```

---

# Space Complexity

If `Array.Sort()` is used and we consider the sorting algorithm's auxiliary stack/internal requirements,
the exact implementation-dependent memory is not strictly O(1).

For interview-level auxiliary-space analysis, the two-pointer technique itself requires:

```text
O(1)
```

extra pointer variables.

The result array/list is output space and is not normally counted as auxiliary space.

---

# Edge Cases

## 1. Duplicate Values

```text
nums1 = [1,2,2,1]
nums2 = [2,2]

Output = [2,2]
```

---

## 2. No Intersection

```text
nums1 = [1,2,3]
nums2 = [4,5,6]

Output = []
```

---

## 3. One Common Value

```text
nums1 = [1,2,3]
nums2 = [2,4,5]

Output = [2]
```

---

## 4. Different Array Sizes

```text
nums1 = [1,2,2,3,4]
nums2 = [2,2]

Output = [2,2]
```

---

## 5. All Elements Common

```text
nums1 = [1,2,3]
nums2 = [1,2,3]

Output = [1,2,3]
```

---

## 6. Null nums1

Throws:

```text
ArgumentNullException
```

---

## 7. Null nums2

Throws:

```text
ArgumentNullException
```

---

## 8. Empty Array

Throws:

```text
ArgumentException
```

---

# HashSet vs Dictionary vs Two Pointers

| Approach               |                 Time | Extra Space | Handles Duplicates |
| ---------------------- | -------------------: | ----------: | ------------------ |
| HashSet                |             O(n + m) |    O(n + m) | ❌                  |
| Dictionary             |             O(n + m) |        O(n) | ✅                  |
| Sorting + Two Pointers | O(n log n + m log m) |       O(1)* | ✅                  |

`*` Auxiliary space for the two-pointer technique; sorting implementation details may require additional stack/internal memory.

---

# Key DSA Pattern

## Two Pointers

Two pointers are useful when arrays are sorted and we need to efficiently compare elements.

General pattern:

```text
left = 0
right = 0

while left < n && right < m

    if values are equal
        process
        left++
        right++

    else if first value is smaller
        left++

    else
        right++
```

This pattern is commonly used for:

* array intersection
* merging sorted arrays
* pair problems
* comparing two sequences

---

# Final Complexity

```text
Time:
O(n log n + m log m)

Auxiliary Space:
O(1) for the two-pointer logic
```

The important point is that **duplicates are preserved** because matching elements advance both pointers one occurrence at a time.
