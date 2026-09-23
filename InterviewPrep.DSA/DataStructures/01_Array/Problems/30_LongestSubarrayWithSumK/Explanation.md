# Longest Subarray with Sum K

## Why

We need to find the longest contiguous subarray whose sum is exactly `k`.

Because the array can contain positive numbers, negative numbers, and zero, we cannot simply use a sliding window based on whether the current sum is greater than or less than `k`.

The general optimal approach is Prefix Sum + Dictionary.

---

## Idea

Maintain a running prefix sum:

```text
currentSum = nums[0] + nums[1] + ... + nums[i]
```

Suppose an earlier prefix sum was:

```text
previousSum
```

Then the elements between that previous position and the current position have sum:

```text
currentSum - previousSum
```

We need this to equal `k`:

```text
currentSum - previousSum = k
```

Therefore:

```text
previousSum = currentSum - k
```

So for every position we check whether:

```text
currentSum - k
```

has already been seen.

The dictionary stores:

```text
prefixSum → earliest index
```

We store only the earliest occurrence because the earliest index produces the longest possible subarray.

---

## Important Initialization

We initialize:

```text
0 → -1
```

The `-1` represents the position immediately before the first element.

This allows us to correctly identify subarrays that start at index `0`.

---

## Dry Run

Input:

```text
nums = [10, 5, 2, 7, 1, 9]
k = 15
```

At index `4`:

```text
currentSum = 25
requiredPrefixSum = 25 - 15 = 10
```

Prefix sum `10` was seen at index `0`.

Therefore:

```text
length = 4 - 0 = 4
```

The subarray is:

```text
[5, 2, 7, 1]
```

Its sum is:

```text
5 + 2 + 7 + 1 = 15
```

Therefore the answer is:

```text
4
```

---

## Time Complexity

```text
O(n)
```

The array is traversed once.

Dictionary lookup and insertion take `O(1)` average time.

Therefore:

```text
n × O(1) = O(n)
```

---

## Space Complexity

```text
O(n)
```

In the worst case, every prefix sum can be different, so the dictionary can contain up to `n + 1` entries.

---

## Edge Cases

1. Null array
2. Empty array
3. No valid subarray
4. Entire array is the answer
5. Single-element answer
6. Multiple valid subarrays
7. Negative numbers
8. Zero values
9. `k = 0`
10. Subarray beginning at index `0`

---

## Key DSA Pattern

**Prefix Sum + HashMap**

The important transformation is:

```text
currentSum - previousSum = k
```

Therefore:

```text
previousSum = currentSum - k
```

This converts a subarray-sum problem into a fast dictionary lookup problem.
