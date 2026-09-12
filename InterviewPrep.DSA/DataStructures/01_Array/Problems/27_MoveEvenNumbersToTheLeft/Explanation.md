# Move Even Numbers to the Left

## Problem

Given an integer array, rearrange the array so that all even numbers appear before all odd numbers.

The relative order of elements does not need to be preserved.

The rearrangement must be performed in-place.

---

## Example

### Input

```text
[3, 1, 2, 4]
```

### Possible Output

```text
[4, 2, 1, 3]
```

The exact order does not matter.

The only requirement is:

```text
Even numbers → Left
Odd numbers  → Right
```

---

# Better Approach

Use an additional array.

Traverse the original array:

* Put even numbers from the beginning.
* Put odd numbers from the end.

Example:

```text
Input:
[3, 1, 2, 4]
```

Even numbers:

```text
[2, 4]
```

Odd numbers:

```text
[3, 1]
```

A valid result can be:

```text
[2, 4, 1, 3]
```

## Time Complexity

```text
O(n)
```

The array is traversed once.

## Space Complexity

```text
O(n)
```

A second array of size `n` is created.

---

# Optimal Approach

Use two pointers.

```text
left  → starts at 0
right → starts at n - 1
```

The goal is:

```text
[ even | even | even | odd | odd | odd ]
    ↑                         ↑
   left                      right
```

## Rules

### Rule 1

If the left element is already even:

```text
nums[left] % 2 == 0
```

Move `left` forward.

---

### Rule 2

If the right element is already odd:

```text
nums[right] % 2 != 0
```

Move `right` backward.

---

### Rule 3

If:

```text
nums[left] = odd
nums[right] = even
```

They are on the wrong sides.

Swap them.

Then move both pointers.

---

# Why Two Pointers Work

The left side should contain even numbers.

The right side should contain odd numbers.

Whenever an element is already on the correct side, we simply move its pointer.

When both elements are on the wrong sides, one swap fixes both positions.

Therefore, every pointer movement makes progress toward the middle.

---

# Dry Run

Input:

```text
[3, 1, 2, 4]
```

Initial:

```text
left = 0
right = 3

[3, 1, 2, 4]
 ↑        ↑
 L        R
```

`3` is odd and `4` is even.

Both are on the wrong sides.

Swap:

```text
[4, 1, 2, 3]
```

Move both:

```text
left = 1
right = 2
```

Now:

```text
[4, 1, 2, 3]
    ↑  ↑
    L  R
```

`1` is odd and `2` is even.

Swap:

```text
[4, 2, 1, 3]
```

Move both:

```text
left = 2
right = 1
```

Now:

```text
left > right
```

Stop.

Final:

```text
[4, 2, 1, 3]
```

All even numbers are on the left and all odd numbers are on the right.

---

# Time Complexity

```text
O(n)
```

Each pointer moves only toward the middle.

Neither pointer moves backward.

Therefore, the total number of operations is proportional to `n`.

---

# Space Complexity

```text
O(1)
```

No additional array, list, dictionary, or other collection is created.

Only a few variables are used:

```text
left
right
```

Therefore, the auxiliary space is constant.

---

# Edge Cases

### 1. Null array

```text
nums = null
```

Throw:

```text
ArgumentNullException
```

---

### 2. Empty array

```text
nums = []
```

Throw:

```text
ArgumentException
```

---

### 3. All elements are even

```text
[2, 4, 6, 8]
```

The array is already valid.

---

### 4. All elements are odd

```text
[1, 3, 5, 7]
```

The array is already valid.

---

### 5. One element

```text
[4]
```

No rearrangement is required.

---

### 6. Negative numbers

Negative numbers can also be even or odd.

For example:

```text
[-4, -3, -2, 5]
```

The same logic works.

---

### 7. Zero

Zero is even:

```text
0 % 2 == 0
```

Therefore, zero belongs on the left side.

---

### 8. Mixed values

```text
[3, 8, 5, 2, 7, 4]
```

Any arrangement satisfying:

```text
[even, even, even, odd, odd, odd]
```

is valid.

---

# Key DSA Pattern

**Two Pointers — Partitioning**

This is a partitioning problem because we divide the array into two groups:

```text
[ Even Numbers | Odd Numbers ]
```

The same two-pointer partitioning idea is useful in problems involving:

* Positive vs negative numbers
* Even vs odd numbers
* Valid vs invalid elements
* Elements smaller/larger than a target
* Partitioning around a pivot
