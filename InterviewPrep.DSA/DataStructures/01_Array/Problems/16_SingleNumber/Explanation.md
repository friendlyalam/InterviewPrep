# Array Problem 16 — Single Number

## Problem

Given a non-empty integer array `nums`, every element appears exactly twice except for one element that appears exactly once.

Find the element that appears only once.

Example:

Input:
[4, 1, 2, 1, 2]

Output:
4

---

# Better Approach — Dictionary

## Why?

We need to find the number whose frequency is exactly `1`.

A Dictionary can store each number as a key and its frequency as the value.

For example:

[4, 1, 2, 1, 2]

Dictionary:

4 -> 1
1 -> 2
2 -> 2

The key whose value is `1` is the answer.

## Idea

1. Create a Dictionary.
2. Traverse the array.
3. Count the frequency of every number.
4. Traverse the Dictionary.
5. Return the key whose frequency is `1`.

## Dry Run

Input:

[4, 1, 2, 1, 2]

After processing:

4 -> 1
1 -> 2
2 -> 2

The frequency of `4` is `1`.

Therefore:

Answer = 4

## Time Complexity

O(n)

### Why?

The array is traversed once to build the Dictionary.

The Dictionary is then traversed to find the number with frequency `1`.

Therefore, the overall complexity is O(n).

## Space Complexity

O(n)

### Why?

The Dictionary can store up to `n` different numbers.

---

# Optimal Approach — XOR

## Why?

Every number appears twice except one.

XOR has two important properties:

x ^ x = 0

x ^ 0 = x

Therefore, duplicate numbers cancel each other.

The number that appears once remains.

## Idea

1. Start with `result = 0`.
2. XOR every element with `result`.
3. Duplicate values cancel each other.
4. The single value remains.

## Dry Run

Input:

[4, 1, 2, 1, 2]

Start:

result = 0

0 ^ 4 = 4

4 ^ 1 = 5

5 ^ 2 = 7

7 ^ 1 = 6

6 ^ 2 = 4

Answer = 4

We can also represent it as:

4 ^ 1 ^ 2 ^ 1 ^ 2

Rearrange:

4 ^ (1 ^ 1) ^ (2 ^ 2)

Since:

1 ^ 1 = 0
2 ^ 2 = 0

Therefore:

4 ^ 0 ^ 0 = 4

---

## Time Complexity

O(n)

### Why?

Every element is visited exactly once.

---

## Space Complexity

O(1)

### Why?

Only one variable, `result`, is used.

No additional collection is required.

---

# Edge Cases

## 1. Single Element

Input:

[1]

Output:

1

The only element is automatically the single number.

---

## 2. Negative Numbers

Input:

[-1, -2, -1]

Output:

-2

XOR works with negative integers as well.

---

## 3. Single Number at the Beginning

Input:

[4, 1, 2, 1, 2]

Output:

4

The position of the single number does not matter.

---

## 4. Single Number at the End

Input:

[1, 2, 3, 2, 1]

Output:

3

---

## 5. Null Array

Input:

null

The method throws:

ArgumentNullException

This is defensive handling because the original problem does not specify null input.

---

## 6. Empty Array

Input:

[]

Output:

0

The XOR implementation naturally returns `0`.

However, an empty array violates the original problem constraint because the problem requires a non-empty array.

Therefore, this is a defensive behavior rather than a valid problem case.

---

# Key DSA Pattern

When a problem says:

- Every element appears twice.
- One element appears once.
- Find the element appearing once.

Think about:

XOR

Important properties:

x ^ x = 0

x ^ 0 = x

Therefore, duplicate values cancel and the unique value remains.