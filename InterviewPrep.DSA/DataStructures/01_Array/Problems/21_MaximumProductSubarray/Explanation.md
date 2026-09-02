# Array Problem 21 — Maximum Product Subarray

## Problem

Given an integer array `nums`, find the contiguous subarray that has the largest product and return that product.

The subarray must contain at least one element.

---

# Approach 1 — Better: Brute Force

## Why?

The simplest way to solve the problem is to generate every possible contiguous subarray and calculate its product.

## Idea

For every starting index `i`:

1. Start with `product = 1`.
2. Move `j` from `i` to the end.
3. Multiply `nums[j]` into `product`.
4. Update the maximum product.

Because we keep multiplying while moving `j`, we don't need to calculate the product from the beginning each time.

## Dry Run

Input:

```text
[2, 3, -2, 4]
```

Starting at index `0`:

```text
[2]          → 2
[2,3]        → 6
[2,3,-2]     → -12
[2,3,-2,4]   → -48
```

Starting at index `1`:

```text
[3]          → 3
[3,-2]       → -6
[3,-2,4]     → -24
```

Starting at index `2`:

```text
[-2]         → -2
[-2,4]       → -8
```

Starting at index `3`:

```text
[4]          → 4
```

Maximum:

```text
6
```

## Time Complexity

```text
O(n²)
```

## Why?

There are two nested loops.

The outer loop selects the starting index and the inner loop expands the subarray.

Therefore, in the worst case, we examine approximately:

```text
n × n
```

subarray extensions.

## Space Complexity

```text
O(1)
```

## Why?

Only a few variables are used. No additional array is required.

---

# Approach 2 — Optimal: Maximum and Minimum Product

## Why?

Unlike Maximum Subarray Sum, multiplication has an important complication:

```text
negative × negative = positive
```

Therefore, the smallest negative product can become the largest positive product when multiplied by another negative number.

So we cannot track only the maximum product.

We must track:

```text
maxProduct
minProduct
```

for the current position.

## Idea

At every element, there are three possibilities:

```text
1. Start a new subarray with current element.

2. Extend the previous maximum product.

3. Extend the previous minimum product.
```

Therefore:

```text
newMax =
max(
    current,
    previousMax × current,
    previousMin × current
)
```

And:

```text
newMin =
min(
    current,
    previousMax × current,
    previousMin × current
)
```

We also maintain `result` to store the maximum product found anywhere.

## Why Do We Need Minimum?

Consider:

```text
[-2, 3, -4]
```

After processing `3`:

```text
maxProduct = 3
minProduct = -6
```

When `-4` arrives:

```text
3 × -4  = -12
-6 × -4 = 24
```

The previous minimum `-6` becomes the maximum because:

```text
negative × negative = positive
```

Therefore, both maximum and minimum are necessary.

## Dry Run

Input:

```text
[-2, 3, -4]
```

### Index 0

```text
current = -2

maxProduct = -2
minProduct = -2
result = -2
```

### Index 1

```text
current = 3

previousMax = -2
previousMin = -2

possible products:

3
-2 × 3 = -6
-2 × 3 = -6
```

Therefore:

```text
maxProduct = 3
minProduct = -6
result = 3
```

### Index 2

```text
current = -4

possible products:

-4
3 × -4  = -12
-6 × -4 = 24
```

Therefore:

```text
maxProduct = 24
minProduct = -12
result = 24
```

Final answer:

```text
24
```

## Zero Handling

Consider:

```text
[2, 3, 0, 4]
```

When `0` is encountered:

```text
current = 0
```

The algorithm considers starting a new subarray with `0`.

Then the next element `4` can start another subarray.

This naturally handles zero without requiring a special reset variable.

## Time Complexity

```text
O(n)
```

## Why?

We process every element exactly once.

There is only one loop:

```text
n elements → O(n)
```

## Space Complexity

```text
O(1)
```

## Why?

We only maintain a fixed number of variables:

```text
maxProduct
minProduct
previousMax
previousMin
result
```

No additional array is required.

---

# Comparison

| Approach | Technique         |  Time | Space |
| -------- | ----------------- | ----: | ----: |
| Better   | Brute Force       | O(n²) |  O(1) |
| Optimal  | Max + Min Product |  O(n) |  O(1) |

---

# Edge Cases

## 1. Single Element

```text
Input:
[-5]

Output:
-5
```

The answer can be negative.

---

## 2. All Positive

```text
Input:
[2, 3, 4]

Output:
24
```

The entire array gives the maximum product.

---

## 3. Negative Numbers

```text
Input:
[-2, 3, -4]

Output:
24
```

Two negative numbers produce a positive product.

---

## 4. One Zero

```text
Input:
[2, 3, 0, 4]

Output:
6
```

The zero breaks the product sequence.

---

## 5. Multiple Zeroes

```text
Input:
[-2, 0, -1, 0]

Output:
0
```

---

## 6. All Negative Numbers

```text
Input:
[-2, -3, -4]

Output:
24
```

The product of all three is negative, so the best subarray is:

```text
[-2, -3]
```

with product:

```text
6
```

Actually, the correct maximum is `12` from:

```text
[-3, -4] → 12
```

---

## 7. Null Array

The method throws:

```text
ArgumentNullException
```

---

## 8. Empty Array

The method throws:

```text
ArgumentException
```

---

# Key DSA Pattern

## Track Maximum and Minimum

For multiplication problems involving negative numbers, do not track only the maximum.

Track both:

```text
Maximum product
Minimum product
```

because:

```text
minimum negative × negative
→ maximum positive
```

This is the key idea behind the optimal solution.
