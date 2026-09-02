# Problem 20 — Product of Array Except Self

## Problem

Given an integer array `nums`, return an array `answer` such that:

```text
answer[i] = product of all elements except nums[i]
```

Division is not allowed.

### Example

```text
Input:
[1, 2, 3, 4]

Output:
[24, 12, 8, 6]
```

---

# Approach 1 — Better: Brute Force

## Why?

For every element, we need the product of all other elements.

The simplest solution is to calculate this product separately for every index.

## Idea

For each index `i`:

1. Set `product = 1`.
2. Traverse the complete array.
3. Skip `nums[i]`.
4. Multiply all other elements.
5. Store the result.

## Dry Run

```text
nums = [1, 2, 3, 4]
```

For index `0`:

```text
2 × 3 × 4 = 24
```

For index `1`:

```text
1 × 3 × 4 = 12
```

For index `2`:

```text
1 × 2 × 4 = 8
```

For index `3`:

```text
1 × 2 × 3 = 6
```

Result:

```text
[24, 12, 8, 6]
```

## Time Complexity

```text
O(n²)
```

## Why?

For every element, we traverse the complete array.

```text
n elements × n traversal = n²
```

## Space Complexity

```text
O(n)
```

## Why?

The output array contains `n` elements.

---

# Approach 2 — Optimal: Left + Right Arrays

## Why?

The brute-force solution repeatedly calculates the same products.

For every index, we can divide the required product into two parts:

```text
product of elements on the LEFT
×
product of elements on the RIGHT
```

We can calculate both parts once.

## Idea

Create two arrays:

```text
left[]
right[]
```

### left[i]

Contains the product of all elements before index `i`.

### right[i]

Contains the product of all elements after index `i`.

Finally:

```text
productArray[i] = left[i] × right[i]
```

## Dry Run

Input:

```text
nums = [1, 2, 3, 4]
```

### Step 1 — Calculate Left Products

For index `0`, there is nothing on the left.

Therefore:

```text
left[0] = 1
```

Continue from left to right:

```text
left = [1, 1, 2, 6]
```

Meaning:

```text
index 0 → 1
index 1 → 1
index 2 → 1 × 2 = 2
index 3 → 1 × 2 × 3 = 6
```

### Step 2 — Calculate Right Products

For index `3`, there is nothing on the right.

Therefore:

```text
right[3] = 1
```

Continue from right to left:

```text
right = [24, 12, 4, 1]
```

Meaning:

```text
index 0 → 2 × 3 × 4 = 24
index 1 → 3 × 4 = 12
index 2 → 4
index 3 → 1
```

### Step 3 — Multiply Left × Right

```text
index 0:
left[0] × right[0]
= 1 × 24
= 24

index 1:
left[1] × right[1]
= 1 × 12
= 12

index 2:
left[2] × right[2]
= 2 × 4
= 8

index 3:
left[3] × right[3]
= 6 × 1
= 6
```

Final result:

```text
[24, 12, 8, 6]
```

## Time Complexity

```text
O(n)
```

## Why?

We make three linear passes:

```text
1. Calculate left products  → O(n)
2. Calculate right products → O(n)
3. Calculate answer          → O(n)
```

Therefore:

```text
O(n) + O(n) + O(n) = O(n)
```

## Space Complexity

```text
O(n)
```

## Why?

We create:

```text
left[]        → O(n)
right[]       → O(n)
productArray[] → O(n)
```

The output array is required, and the left/right arrays are additional memory.

---

# Approach 3 — Best: Prefix + Suffix

## Why?

The Optimal approach has `O(n)` time, but it uses separate `left[]` and `right[]` arrays.

We can reduce the extra space by:

* Storing the left products directly in `productArray`.
* Using one variable `suffix` to calculate the right products.

## Idea

### First Pass — Prefix

Store the product of elements to the left directly inside `productArray`.

For:

```text
nums = [1, 2, 3, 4]
```

After the first pass:

```text
productArray = [1, 1, 2, 6]
```

### Second Pass — Suffix

Start:

```text
suffix = 1
```

Traverse from right to left.

Multiply:

```text
productArray[i] × suffix
```

Then update:

```text
suffix *= nums[i]
```

## Dry Run

After prefix pass:

```text
productArray = [1, 1, 2, 6]
```

Start:

```text
suffix = 1
```

Index `3`:

```text
6 × 1 = 6
suffix = 1 × 4 = 4
```

Index `2`:

```text
2 × 4 = 8
suffix = 4 × 3 = 12
```

Index `1`:

```text
1 × 12 = 12
suffix = 12 × 2 = 24
```

Index `0`:

```text
1 × 24 = 24
suffix = 24 × 1 = 24
```

Final result:

```text
[24, 12, 8, 6]
```

## Time Complexity

```text
O(n)
```

## Why?

There are two linear passes:

```text
Prefix pass  → O(n)
Suffix pass  → O(n)
```

Therefore:

```text
O(n) + O(n) = O(n)
```

## Space Complexity

```text
O(1)
```

excluding the output array.

## Why?

We don't create separate `left[]` or `right[]` arrays.

We only use:

```text
prefix
suffix
```

Both are single variables, so they require constant space.

---

# Edge Cases

## 1. Single Element

```text
Input:
[5]

Output:
[1]
```

There are no other elements, so the product is `1`.

---

## 2. One Zero

```text
Input:
[1, 2, 0, 4]

Output:
[0, 0, 8, 0]
```

The element at index `2` has:

```text
1 × 2 × 4 = 8
```

Every other position includes the zero.

---

## 3. Multiple Zeroes

```text
Input:
[1, 0, 3, 0]

Output:
[0, 0, 0, 0]
```

---

## 4. Negative Numbers

```text
Input:
[-1, 2, 3]

Output:
[6, -3, -2]
```

---

## 5. No Zeroes

```text
Input:
[1, 2, 3, 4]

Output:
[24, 12, 8, 6]
```

---

## 6. Null Array

The method throws:

```text
ArgumentNullException
```

---

## 7. Empty Array

The method throws:

```text
ArgumentException
```

---

# Comparison

| Approach | Technique           |  Time | Extra Space |
| -------- | ------------------- | ----: | ----------: |
| Better   | Brute Force         | O(n²) |        O(n) |
| Optimal  | Left + Right Arrays |  O(n) |        O(n) |
| Best     | Prefix + Suffix     |  O(n) |       O(1)* |

`*` Excluding the required output array.

---

# Key DSA Pattern

## Prefix and Suffix

When a problem asks about:

```text
everything BEFORE the current index
+
everything AFTER the current index
```

think about:

```text
Prefix + Suffix
```

For this problem:

```text
answer[i]
=
product of LEFT elements
×
product of RIGHT elements
```

This pattern is especially important because it can reduce repeated work from:

```text
O(n²)
```

to:

```text
O(n)
```
