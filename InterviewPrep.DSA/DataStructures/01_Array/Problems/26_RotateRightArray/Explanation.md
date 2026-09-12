# Rotate Array

## Problem

Given an integer array `nums`, rotate the array to the right by `k` positions.

The rotation must be performed in-place.

---

## Example

### Input

```text
nums = [1,2,3,4,5,6,7]
k = 3
```

### Output

```text
[5,6,7,1,2,3,4]
```

The last 3 elements move to the beginning.

```text
[1,2,3,4 | 5,6,7]
            ↓
[5,6,7 | 1,2,3,4]
```

---

# Better Approach

Use a temporary array.

### Idea

1. Calculate `k % n`.
2. Copy the last `k` elements into the temporary array.
3. Copy the remaining elements after them.
4. Copy the temporary array back into `nums`.

### Example

```text
nums = [1,2,3,4,5,6,7]
k = 3
```

Last 3 elements:

```text
[5,6,7]
```

Remaining elements:

```text
[1,2,3,4]
```

Combine:

```text
[5,6,7,1,2,3,4]
```

### Time Complexity

```text
O(n)
```

We visit the array elements a constant number of times.

### Space Complexity

```text
O(n)
```

A temporary array of size `n` is created.

---

# Optimal Approach

Use the reversal technique.

Three reversals are enough to rotate the array.

### Step 1

Reverse the entire array.

```text
[1,2,3,4,5,6,7]
        ↓
[7,6,5,4,3,2,1]
```

### Step 2

Reverse the first `k` elements.

```text
[7,6,5,4,3,2,1]
        ↓
[5,6,7,4,3,2,1]
```

### Step 3

Reverse the remaining elements.

```text
[5,6,7,4,3,2,1]
        ↓
[5,6,7,1,2,3,4]
```

The array is now rotated correctly.

---

# Why Does Reversal Work?

The last `k` elements need to move to the beginning.

Reversing the entire array brings those elements to the front, but their order is reversed.

We then reverse the first `k` elements to restore their original order.

Finally, we reverse the remaining elements to restore their original order.

Therefore, the final result is a right rotation.

---

# Important: k Greater Than Array Length

If:

```text
nums.Length = 4
k = 5
```

Rotating 5 times is equivalent to rotating 1 time.

Therefore:

```text
k = k % nums.Length
```

Example:

```text
5 % 4 = 1
```

So we only need one rotation.

---

# Dry Run

Input:

```text
nums = [1,2,3,4,5,6,7]
k = 3
```

### Original

```text
[1,2,3,4,5,6,7]
```

### Reverse entire array

```text
[7,6,5,4,3,2,1]
```

### Reverse first 3

```text
[5,6,7,4,3,2,1]
```

### Reverse remaining elements

```text
[5,6,7,1,2,3,4]
```

### Final result

```text
[5,6,7,1,2,3,4]
```

---

# Time Complexity

```text
O(n)
```

There are three reversal operations.

Each reversal takes at most `O(n)` time.

Therefore:

```text
O(n) + O(n) + O(n)
= O(3n)
= O(n)
```

Constants are ignored in Big-O notation.

---

# Space Complexity

```text
O(1)
```

The algorithm modifies the original array.

Only a few variables such as:

```text
left
right
k
```

are used.

No additional array or collection is created.

---

# Edge Cases

1. `nums == null`

   * Throw `ArgumentNullException`.

2. Empty array

   * Throw `ArgumentException`.

3. `k == 0`

   * Array remains unchanged.

4. `k == nums.Length`

   * Array remains unchanged.

5. `k > nums.Length`

   * Use `k % nums.Length`.

6. Negative `k`

   * Reject with `ArgumentException`.

7. Single-element array

   * Rotation produces the same array.

8. Duplicate values

   * Rotation still works correctly.

9. Negative numbers

   * Rotation is independent of element values.

---

# Key DSA Pattern

**Array Reversal / In-place Array Manipulation**

This pattern is useful when an array needs to be rearranged while using `O(1)` extra space.
