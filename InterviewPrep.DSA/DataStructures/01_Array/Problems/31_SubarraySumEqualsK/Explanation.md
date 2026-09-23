# Subarray Sum Equals K

## Problem

Given an integer array `nums` and an integer `k`, return the total number of contiguous subarrays whose sum is exactly `k`.

The array can contain positive numbers, negative numbers, and zero.

---

## Better Approach

Use two loops.

The first loop chooses the starting index.

The second loop extends the subarray while maintaining a running sum.

Whenever:

```text
currentSum == k
```

we increment the answer.

### Example

```text
nums = [1, 2, 3]
k = 3
```

Starting at index `0`:

```text
1
1 + 2 = 3  ← valid
1 + 2 + 3 = 6
```

Starting at index `1`:

```text
2
2 + 3 = 5
```

Starting at index `2`:

```text
3  ← valid
```

Answer:

```text
2
```

### Time Complexity

```text
O(n²)
```

For every starting position, we may scan the remaining elements.

### Space Complexity

```text
O(1)
```

Only a few variables are used.

---

# Optimal Approach

## Prefix Sum + Dictionary

Maintain a running prefix sum:

```text
currentSum
```

Suppose an earlier prefix sum was:

```text
previousSum
```

The sum of the subarray between them is:

```text
currentSum - previousSum
```

We need:

```text
currentSum - previousSum = k
```

Therefore:

```text
previousSum = currentSum - k
```

So at every index we calculate:

```text
requiredPrefixSum = currentSum - k
```

and check how many times that prefix sum has already appeared.

---

## Why do we store frequency?

Unlike the longest-subarray problem, we need to count **every valid subarray**.

Suppose:

```text
requiredPrefixSum
```

has appeared three times.

Then there are three different previous positions that can form a valid subarray ending at the current index.

Therefore:

```text
count += frequency
```

---

## Initialization

We initialize:

```text
prefixFrequency[0] = 1
```

This represents a prefix sum of zero before the first array element.

It is necessary for subarrays that start at index `0`.

### Example

```text
nums = [1, 2]
k = 3
```

At index `1`:

```text
currentSum = 3
requiredPrefixSum = 3 - 3
                  = 0
```

The dictionary already contains:

```text
0 → 1
```

Therefore we find one valid subarray:

```text
[1, 2]
```

---

## Dry Run

Input:

```text
nums = [1, 1, 1]
k = 2
```

Initial dictionary:

```text
0 → 1
```

### Index 0

```text
currentSum = 1
required = 1 - 2 = -1
```

`-1` does not exist.

Store:

```text
1 → 1
```

---

### Index 1

```text
currentSum = 2
required = 2 - 2 = 0
```

Dictionary contains:

```text
0 → 1
```

Therefore:

```text
count = 1
```

Store:

```text
2 → 1
```

---

### Index 2

```text
currentSum = 3
required = 3 - 2 = 1
```

Dictionary contains:

```text
1 → 1
```

Therefore:

```text
count = 2
```

Final answer:

```text
2
```

The two valid subarrays are:

```text
[1, 1]
[1, 1]
```

---

## Time Complexity

```text
O(n)
```

We traverse the array once.

Dictionary lookup and insertion are `O(1)` on average.

Therefore:

```text
n × O(1) = O(n)
```

---

## Space Complexity

```text
O(n)
```

In the worst case, there can be `O(n)` different prefix sums stored in the dictionary.

---

## Edge Cases

1. Null array
2. Empty array
3. No matching subarray
4. Entire array is a valid subarray
5. Single-element valid subarray
6. Multiple overlapping valid subarrays
7. Negative numbers
8. Zero values
9. `k = 0`
10. Repeated prefix sums
11. Multiple occurrences of the same prefix sum
12. Valid subarray beginning at index `0`

---

## Key DSA Pattern

**Prefix Sum + HashMap/Dictionary**

The key transformation is:

```text
currentSum - previousSum = k
```

Therefore:

```text
previousSum = currentSum - k
```

For counting problems:

```text
prefixSum → frequency
```

For longest-subarray problems:

```text
prefixSum → earliest index
```

This distinction is extremely important.
