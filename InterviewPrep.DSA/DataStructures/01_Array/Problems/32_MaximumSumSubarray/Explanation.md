# Maximum Sum Subarray of Size K

## Problem

Given an integer array `nums` and an integer `k`, find the maximum sum of any contiguous subarray containing exactly `k` elements.

---

# Approach 1 — Prefix Sum

## Why

Every candidate subarray contains exactly `k` elements.

A prefix-sum array allows us to calculate the sum of any range in `O(1)`.

---

## Idea

Build:

```text
prefixSum[i] = sum of elements before index i
```

For a window starting at `start` and containing `k` elements:

```text
start ... start + k - 1
```

Its sum is:

```text
prefixSum[start + k] - prefixSum[start]
```

We calculate this for every possible starting position and keep the maximum.

---

## Example

```text
nums = [2, 1, 5, 1, 3, 2]
k = 3
```

Prefix sum:

```text
[0, 2, 3, 8, 9, 12, 14]
```

First window:

```text
prefixSum[3] - prefixSum[0]
= 8 - 0
= 8
```

Second window:

```text
prefixSum[4] - prefixSum[1]
= 9 - 2
= 7
```

Third window:

```text
prefixSum[5] - prefixSum[2]
= 12 - 3
= 9
```

Fourth window:

```text
prefixSum[6] - prefixSum[3]
= 14 - 8
= 6
```

Maximum:

```text
9
```

---

## Time Complexity

```text
O(n)
```

We build the prefix array in `O(n)` and then examine all windows in another `O(n)` traversal.

Therefore:

```text
O(n) + O(n) = O(n)
```

---

## Space Complexity

```text
O(n)
```

The prefix-sum array contains `n + 1` elements.

---

# Approach 2 — Sliding Window

## Why

Every candidate has the same size `k`.

Therefore, when we move from one window to the next, we don't need to calculate the entire sum again.

We can:

1. Remove the element leaving the window.
2. Add the element entering the window.

Formula:

```text
newSum = oldSum - outgoingElement + incomingElement
```

---

## Example

```text
nums = [2, 1, 5, 1, 3, 2]
k = 3
```

First window:

```text
[2, 1, 5]
sum = 8
```

Move one position:

```text
[1, 5, 1]
```

Instead of calculating again:

```text
8 - 2 + 1 = 7
```

Move again:

```text
[5, 1, 3]
```

```text
7 - 1 + 3 = 9
```

Move again:

```text
[1, 3, 2]
```

```text
9 - 5 + 2 = 6
```

Maximum:

```text
9
```

---

## Time Complexity

```text
O(n)
```

Every element enters the window once and leaves the window at most once.

---

## Space Complexity

```text
O(1)
```

Only variables for the running sum and maximum sum are required.

---

# Why Sliding Window Is Optimal

Both approaches have:

```text
O(n)
```

time complexity.

However:

```text
Prefix Sum:
Time  = O(n)
Space = O(n)

Sliding Window:
Time  = O(n)
Space = O(1)
```

Therefore, Sliding Window is the optimal solution for this fixed-size-window problem.

---

# Edge Cases

1. Null array
2. Empty array
3. `k <= 0`
4. `k > nums.Length`
5. `k = 1`
6. `k = nums.Length`
7. All positive numbers
8. All negative numbers
9. Zero values
10. Mixed positive and negative values
11. Multiple windows having the same maximum sum

---

# Key DSA Pattern

**Fixed-Size Sliding Window**

The important formula is:

```text
newWindowSum =
    oldWindowSum
    - outgoingElement
    + incomingElement
```

---

# Important Comparison

| Problem Type                | Useful Technique        |
| --------------------------- | ----------------------- |
| Arbitrary subarray sum      | Prefix Sum              |
| Count subarrays with sum K  | Prefix Sum + Dictionary |
| Longest subarray with sum K | Prefix Sum + Dictionary |
| Fixed-size maximum sum      | Sliding Window          |
| Fixed-size sum queries      | Prefix Sum              |

The key recognition here is:

> When the problem says **exactly K consecutive elements**, immediately consider a **fixed-size sliding window**.
