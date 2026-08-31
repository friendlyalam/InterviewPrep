# Array Problem 15 — Find the Missing Number

## Problem

Given an array `nums` containing `n` distinct numbers taken from the range `[0, n]`, return the only number missing from the range.

### Example

Input:
[3, 0, 1]

Output:
2

---

# Approach 1 — Better Approach

## Why?

The array should contain every number from `0` to `n`, except one number.

If we calculate:

1. The sum of all numbers from `0` to `n`.
2. The sum of all numbers actually present in the array.

Then:

`Expected Sum - Actual Sum = Missing Number`

---

## Idea

Use the mathematical formula:

`n * (n + 1) / 2`

to calculate the expected sum.

Then traverse the array and calculate the actual sum.

Finally:

`Missing Number = Expected Sum - Actual Sum`

---

## Dry Run

Input:

`nums = [3, 0, 1]`

Array length:

`n = 3`

### Expected Sum

`3 * (3 + 1) / 2`

`= 6`

Numbers should be:

`0 + 1 + 2 + 3 = 6`

### Actual Sum

`3 + 0 + 1 = 4`

### Missing Number

`6 - 4 = 2`

Answer:

`2`

---

## Time Complexity

`O(n)`

### Why?

We traverse the array once to calculate the actual sum.

---

## Space Complexity

`O(1)`

### Why?

We only use variables such as:

- `n`
- `expectedSum`
- `actualSum`

No additional array or collection is created.

---

## Important Point

Use `long` for the sum calculation to avoid integer overflow when the input size becomes large.

---

# Approach 2 — Optimal Approach

## Why?

XOR has two important properties:

`x ^ x = 0`

and

`x ^ 0 = x`

Every number that exists in the array also exists in the expected range.

When we XOR both sets of numbers, matching numbers cancel each other.

The only number left is the missing number.

---

## Idea

Start with `n`.

Then XOR:

- Every index from `0` to `n - 1`
- Every value in the array

The matching numbers cancel each other.

The missing number remains.

---

## Dry Run

Input:

`nums = [3, 0, 1]`

Start:

`result = 3`

### i = 0

`result = 3 ^ 0 ^ 3`

`result = 0`

### i = 1

`result = 0 ^ 1 ^ 0`

`result = 1`

### i = 2

`result = 1 ^ 2 ^ 1`

`result = 2`

Answer:

`2`

---

## Why Does XOR Work?

The complete set should be:

`0, 1, 2, 3`

The array contains:

`3, 0, 1`

XOR everything:

`0 ^ 1 ^ 2 ^ 3 ^ 3 ^ 0 ^ 1`

Rearrange:

`(0 ^ 0) ^ (1 ^ 1) ^ (3 ^ 3) ^ 2`

Since:

`x ^ x = 0`

we get:

`0 ^ 0 ^ 0 ^ 2`

Therefore:

`2`

is left.

---

## Time Complexity

`O(n)`

### Why?

We traverse the array exactly once.

---

## Space Complexity

`O(1)`

### Why?

We use only one variable, `result`.

No additional data structure is required.

---

## Key DSA Pattern

When you see:

- Numbers from `0` to `n`
- One number is missing
- Numbers are distinct

Think about:

1. Sum Formula
2. XOR

The XOR approach avoids sum overflow and is an important interview pattern.