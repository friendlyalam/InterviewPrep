# Array Problem 17 — Majority Element

## Problem

Given an integer array `nums` of size `n`, return the majority element.

The majority element is the element that appears more than `n / 2` times.

The problem guarantees that the majority element always exists.

Example:

Input:
[2, 2, 1, 1, 1, 2, 2]

Output:
2

---

# Better Approach — Dictionary

## Why?

We need to find the element that occurs more than `n / 2` times.

We can store each number and its frequency using a Dictionary.

Example:

[2, 2, 1, 1, 1, 2, 2]

Frequency:

2 -> 4
1 -> 3

The number with the highest frequency is `2`.

Because the majority element is guaranteed to exist, that number is the answer.

## Idea

1. Create a Dictionary.
2. Traverse the array.
3. Count the frequency of each number.
4. Traverse the Dictionary.
5. Track the number with the highest frequency.
6. Return that number.

## Dry Run

Input:

[2, 2, 1, 1, 1, 2, 2]

Dictionary after traversal:

2 -> 4
1 -> 3

Start:

maxCount = 0

Check `2`:

4 > 0

maxCount = 4
maxNumber = 2

Check `1`:

3 > 4 -> false

Answer:

2

## Time Complexity

O(n)

### Why?

The array is traversed once to build the frequency map.

The Dictionary is then traversed to find the highest frequency.

Both operations together are O(n).

## Space Complexity

O(n)

### Why?

The Dictionary can contain up to `n` different values.

---

# Optimal Approach — Boyer-Moore Voting Algorithm

## Why?

The problem guarantees that one element appears more than `n / 2` times.

This means the majority element appears more times than all other elements combined.

Therefore, we can cancel one occurrence of the majority element against one occurrence of a different element.

The majority element will still remain at the end.

## Idea

Maintain two variables:

- `candidate` — current possible majority element
- `count` — current vote count

Rules:

1. If `count == 0`, select the current number as the candidate.
2. If the current number equals the candidate, increase `count`.
3. Otherwise, decrease `count`.

Because the majority element has more occurrences than all other elements combined, it will be the final candidate.

## Dry Run

Input:

[2, 2, 1, 1, 1, 2, 2]

Start:

candidate = 0
count = 0

### Number = 2

count == 0

candidate = 2

2 == 2

count = 1

### Number = 2

2 == 2

count = 2

### Number = 1

1 != 2

count = 1

### Number = 1

1 != 2

count = 0

### Number = 1

count == 0

candidate = 1

1 == 1

count = 1

### Number = 2

2 != 1

count = 0

### Number = 2

count == 0

candidate = 2

2 == 2

count = 1

Final candidate:

2

Therefore:

Answer = 2

## Why Does It Work?

Every time we encounter a different value, we cancel one vote against the current candidate.

Since the majority element appears more than `n / 2` times, there are not enough non-majority elements to cancel all of its occurrences.

Therefore, the majority element survives as the final candidate.

## Time Complexity

O(n)

### Why?

We traverse the array exactly once.

Each element requires only constant-time operations.

## Space Complexity

O(1)

### Why?

Only two variables are used:

- `candidate`
- `count`

No additional Dictionary, array, or collection is required.

---

# Edge Cases

## 1. Single Element

Input:

[1]

Output:

1

The only element is automatically the majority element.

---

## 2. Two Elements

A valid input cannot contain two different elements because one element must appear more than `n / 2`.

Example:

[2, 2]

Output:

2

---

## 3. Majority Element at the Beginning

Input:

[3, 3, 3, 2, 2]

Output:

3

---

## 4. Majority Element at the End

Input:

[2, 2, 3, 3, 3]

Output:

3

---

## 5. Negative Numbers

Input:

[-1, -1, 2, 2, -1]

Output:

-1

The algorithm works with negative integers as well.

---

## 6. Null Array

Input:

null

The method throws:

ArgumentNullException

This is defensive handling because the original problem constraints do not allow null.

---

## 7. Empty Array

Input:

[]

The method throws:

ArgumentException

An empty array is invalid because the problem requires:

`1 <= nums.length`

---

# Key DSA Pattern

When a problem says:

- Find the element occurring more than `n / 2` times.
- The majority element is guaranteed to exist.

Think:

Boyer-Moore Voting Algorithm

Complexity:

Time: O(n)
Space: O(1)