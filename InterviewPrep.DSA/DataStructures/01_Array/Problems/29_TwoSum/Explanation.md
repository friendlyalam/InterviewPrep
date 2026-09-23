# Two Sum

## Problem

Given an integer array `nums` and an integer `target`, find two different elements whose sum equals `target`.

Return the indices of those two elements.

The problem guarantees exactly one valid pair.

---

# Brute Force Approach

## Idea

Check every possible pair.

For every element at index `i`, check every element after it using index `j`.

```text
i = 0
    j = 1
    j = 2
    j = 3
    ...

i = 1
    j = 2
    j = 3
    ...
```

If:

```text
nums[i] + nums[j] == target
```

return:

```text
[i, j]
```

## Time Complexity

```text
O(n²)
```

We potentially compare every pair of elements.

## Space Complexity

```text
O(1)
```

Apart from the two-element result, no additional data structure is required.

---

# Better Approach — Sorting + Two Pointers

## Idea

Because the original array is not sorted, we cannot directly use two pointers.

We can create pairs containing:

```text
(value, originalIndex)
```

Then sort the pairs by value.

After sorting:

```text
left  → smallest value
right → largest value
```

If:

```text
nums[left] + nums[right] < target
```

move `left`.

If:

```text
nums[left] + nums[right] > target
```

move `right`.

If they are equal, return their original indices.

## Example

```text
nums = [3,2,4]
target = 6
```

Store:

```text
(3,0)
(2,1)
(4,2)
```

After sorting:

```text
(2,1)
(3,0)
(4,2)
```

Now:

```text
2 + 4 = 6
```

Return:

```text
[1,2]
```

## Time Complexity

```text
O(n log n)
```

Sorting requires `O(n log n)`.

## Space Complexity

```text
O(n)
```

We need to preserve the original indices.

---

# Optimal Approach — Dictionary

## Why?

We need to find:

```text
nums[i] + nums[j] = target
```

Rearrange the equation:

```text
nums[j] = target - nums[i]
```

For every current number, calculate the number we need.

```text
needed = target - nums[i]
```

Then check whether that value has already been seen.

A Dictionary provides average `O(1)` lookup.

---

# Algorithm

For each element:

1. Calculate the required value.

```text
needed = target - nums[i]
```

2. Check whether `needed` exists in the Dictionary.

3. If it exists, return the stored index and current index.

4. Otherwise, store the current number and its index.

---

# Dry Run

Input:

```text
nums = [2,7,11,15]
target = 9
```

### Step 1

```text
i = 0
nums[i] = 2

needed = 9 - 2
       = 7
```

Dictionary does not contain `7`.

Store:

```text
2 → 0
```

---

### Step 2

```text
i = 1
nums[i] = 7

needed = 9 - 7
       = 2
```

Dictionary contains:

```text
2 → 0
```

Therefore:

```text
return [0,1]
```

---

# Why We Check Before Storing

Consider:

```text
nums = [3,3]
target = 6
```

At index `0`:

```text
needed = 3
```

It isn't in the Dictionary.

Store:

```text
3 → 0
```

At index `1`:

```text
needed = 3
```

Now `3` exists:

```text
3 → 0
```

Return:

```text
[0,1]
```

This also guarantees that we don't use the same element twice.

---

# Time Complexity

```text
O(n)
```

We traverse the array once.

Dictionary lookup is `O(1)` on average.

Therefore:

```text
n × O(1) = O(n)
```

---

# Space Complexity

```text
O(n)
```

In the worst case, we may store almost every element in the Dictionary.

---

# Edge Cases

## 1. Null array

```text
nums = null
```

Throw:

```text
ArgumentNullException
```

---

## 2. Fewer than two elements

```text
nums = [5]
```

Two different elements are required.

Throw:

```text
ArgumentException
```

---

## 3. Empty array

```text
nums = []
```

Throw:

```text
ArgumentException
```

---

## 4. Duplicate values

```text
nums = [3,3]
target = 6
```

Output:

```text
[0,1]
```

---

## 5. Negative numbers

```text
nums = [-3,4,2]
target = 1
```

Output:

```text
[0,1]
```

---

## 6. Negative target

```text
nums = [-5,-2,3]
target = -7
```

Output:

```text
[0,1]
```

---

# Key DSA Pattern

**Hashing / Complement Lookup**

The important transformation is:

```text
a + b = target

Therefore:

b = target - a
```

This complement technique is extremely important and appears in many array and hashing problems.

---

# Important DSA Lesson

Do not choose a technique only because its theoretical complexity looks better.

For example:

```text
Two Pointers → O(n)
```

looks better than:

```text
HashMap → O(n)
```

but two pointers require the appropriate structure, such as a sorted array.

For the standard unsorted Two Sum problem:

```text
HashMap → O(n) average
```

is the appropriate optimal technique.
