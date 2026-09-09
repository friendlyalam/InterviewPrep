# Array Problem 25 — Merge Sorted Array

## Problem

Given two sorted integer arrays `nums1` and `nums2`, merge `nums2` into `nums1`.

`nums1` has enough space at the end to contain all elements from both arrays.

The final merged array must be stored inside `nums1`.

---

# Example

```text
nums1 = [1,2,3,0,0,0]
m = 3

nums2 = [2,5,6]
n = 3
```

Output:

```text
[1,2,2,3,5,6]
```

The zero values at the end of `nums1` are empty spaces, not actual elements.

---

# Better Approach — Temporary Array

## Why?

We can use the standard merge technique from Merge Sort.

Create a temporary array and use two pointers:

```text
first
second
```

Compare the current elements of both arrays and place the smaller one into the temporary array.

After merging, copy the result back into `nums1`.

---

## Idea

1. Create a result array of size `m + n`.
2. Compare valid elements of `nums1` and `nums2`.
3. Put the smaller element into the result.
4. Copy remaining elements from either array.
5. Copy the result back into `nums1`.

---

## Dry Run

```text
nums1 = [1,2,3,0,0,0]
nums2 = [2,5,6]
```

Compare:

```text
1 < 2
```

Result:

```text
[1]
```

Compare:

```text
2 <= 2
```

Result:

```text
[1,2]
```

Compare:

```text
3 > 2
```

Result:

```text
[1,2,2]
```

Then:

```text
3 < 5
5 < 6
```

Final:

```text
[1,2,2,3,5,6]
```

---

# Better Time Complexity

**O(m + n)**

Both arrays are traversed at most once.

---

# Better Space Complexity

**O(m + n)**

A separate result array is created.

---

# Optimal Approach — Merge From the End

## Why?

`nums1` already has enough free space at the end.

Therefore, we do not need another array.

The problem is that merging from the beginning could overwrite valid elements in `nums1`.

Instead, merge from the **end**.

---

## Idea

Use three pointers:

```text
first
second
position
```

Initialize:

```text
first = m - 1
second = n - 1
position = m + n - 1
```

`first` points to the last valid element of `nums1`.

`second` points to the last element of `nums2`.

`position` points to the last available position in `nums1`.

Compare the values at `first` and `second`.

Place the larger value at `position`.

Then move the corresponding pointer backward.

---

# Dry Run

Input:

```text
nums1 = [1,2,3,0,0,0]
m = 3

nums2 = [2,5,6]
n = 3
```

Initial:

```text
first = 2
second = 2
position = 5
```

### Step 1

Compare:

```text
nums1[2] = 3
nums2[2] = 6
```

6 is larger.

Put 6 at position 5:

```text
[1,2,3,0,0,6]
```

Move:

```text
second = 1
position = 4
```

---

### Step 2

Compare:

```text
3 and 5
```

5 is larger.

```text
[1,2,3,0,5,6]
```

Move:

```text
second = 0
position = 3
```

---

### Step 3

Compare:

```text
3 and 2
```

3 is larger.

Put 3 at position 3:

```text
[1,2,3,3,5,6]
```

Move:

```text
first = 1
position = 2
```

---

### Step 4

Compare:

```text
2 and 2
```

Take `nums2` value:

```text
[1,2,2,3,5,6]
```

Move:

```text
second = -1
```

The merge is complete.

---

# Why Do We Merge From the End?

Consider:

```text
nums1 = [1,2,3,0,0,0]
nums2 = [2,5,6]
```

If we start from the beginning and put `2` into index 1, we could overwrite the existing `2`.

Then we would also need to shift elements.

By starting from the end, we use the empty positions first.

Therefore, no shifting is required.

---

# Why Do We Only Copy Remaining nums2?

After the main loop finishes, there are two possibilities.

### nums1 still has elements

Those elements are already in their correct positions.

No action is required.

### nums2 still has elements

They must be copied into `nums1`.

Therefore:

```text
while (second >= 0)
```

is required.

---

# Time Complexity

**O(m + n)**

## Why?

Each element is processed at most once.

The main merge loop processes elements from both arrays.

The remaining `nums2` loop processes any elements that were not yet copied.

Therefore:

```text
O(m + n)
```

---

# Space Complexity

**O(1)** extra space.

## Why?

We only use three integer variables:

```text
first
second
position
```

No temporary array or collection is created.

The result is stored directly inside `nums1`.

Therefore:

```text
O(1)
```

extra space.

---

# Edge Cases

## 1. nums2 Is Empty

```text
nums1 = [1]
m = 1

nums2 = []
n = 0
```

Output:

```text
[1]
```

---

## 2. nums1 Has No Valid Elements

```text
nums1 = [0]
m = 0

nums2 = [1]
n = 1
```

Output:

```text
[1]
```

---

## 3. All nums1 Values Are Smaller

```text
nums1 = [1,2,3,0,0]
nums2 = [4,5]
```

Output:

```text
[1,2,3,4,5]
```

---

## 4. All nums2 Values Are Smaller

```text
nums1 = [4,5,6,0,0,0]
nums2 = [1,2,3]
```

Output:

```text
[1,2,3,4,5,6]
```

---

## 5. Duplicate Values

```text
nums1 = [1,2,2,0,0]
nums2 = [2,2]
```

Output:

```text
[1,2,2,2,2]
```

---

## 6. Negative Numbers

The algorithm also works with negative values because it only compares values.

```text
nums1 = [-5,-2,0,0]
nums2 = [-3,-1]
```

Output:

```text
[-5,-3,-2,-1]
```

---

## 7. Null nums1

Throws:

```text
ArgumentNullException
```

---

## 8. Null nums2

Throws:

```text
ArgumentNullException
```

---

## 9. Invalid nums1 Length

If:

```text
nums1.Length != m + n
```

the method throws:

```text
ArgumentException
```

---

## 10. Invalid nums2 Length

If:

```text
nums2.Length != n
```

the method throws:

```text
ArgumentException
```

---

# Better vs Optimal

| Approach        |     Time | Extra Space | Modifies nums1 |
| --------------- | -------: | ----------: | -------------- |
| Temporary Array | O(m + n) |    O(m + n) | Yes            |
| Merge From End  | O(m + n) |    **O(1)** | Yes            |

---

# Key DSA Pattern

## Two Pointers — Merge From End

When:

* two arrays are already sorted
* the destination array has extra space
* the result must be stored in the destination array

consider merging **from the end**.

Pattern:

```text
first = m - 1
second = n - 1
position = m + n - 1
```

Compare from right to left and place the larger value at `position`.

This prevents overwriting unprocessed elements.

---

# Final Complexity

```text
Time:  O(m + n)
Space: O(1)
```

This is the optimal approach because it achieves linear time and constant extra space while modifying `nums1` in-place.
