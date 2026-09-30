# Problem 46 — First Missing Positive

## Problem

Given an unsorted integer array, find the smallest missing
positive integer.

Example:

[3, 4, -1, 1]

Output:

2

---

# Important Observation

For an array of length n, the answer must be between:

1 and n + 1.

Example:

[1, 2, 3]

All numbers from 1 to 3 exist.

Therefore the answer is:

4

It is impossible for the answer to be greater than n + 1.

---

# Solution 1 — Sorting

## Why

If we sort the array, positive numbers will appear in increasing
order.

Then we can start looking for:

1

then:

2

then:

3

and so on.

The first positive number that is missing is our answer.

---

## Dry Run

Input:

[3, 4, -1, 1]

After sorting:

[-1, 1, 3, 4]

Start:

expected = 1

Read -1:

Ignore it.

Read 1:

1 exists.

expected becomes:

2

Read 3:

3 is greater than expected 2.

Therefore 2 is missing.

Answer:

2

---

## Time Complexity

O(n log n)

## Why?

Sorting requires:

O(n log n)

The final scan requires:

O(n)

Therefore:

O(n log n) + O(n)

which becomes:

O(n log n)

---

## Space Complexity

O(log n) auxiliary space in typical .NET sorting
implementation due to sorting recursion.

The input array itself is modified.

---

# Solution 2 — Optimal

## Key DSA Pattern

Cyclic Sort / In-place Placement

---

# Core Idea

We want:

1 at index 0

2 at index 1

3 at index 2

4 at index 3

In general:

value x belongs at:

index x - 1

For example:

value = 3

correct index:

3 - 1 = 2

---

# Which Values Do We Care About?

For an array of length n, only values from:

1 to n

can directly occupy positions that matter.

Therefore we ignore:

- negative numbers
- zero
- numbers greater than n

---

# Why Duplicates Need Special Handling

Suppose:

[1, 1]

For the second 1:

correct index = 0

But index 0 already contains 1.

If we keep swapping, we could repeatedly swap the same
value.

Therefore we only swap when:

nums[i] != nums[correctIndex]

---

# Dry Run

Input:

[3, 4, -1, 1]

n = 4

---

## Step 1

i = 0

nums[0] = 3

Correct index:

3 - 1 = 2

Swap:

[ -1, 4, 3, 1 ]

---

## Step 2

i = 0

nums[0] = -1

Negative value.

Ignore it.

Move forward.

---

## Step 3

i = 1

nums[1] = 4

Correct index:

4 - 1 = 3

Swap:

[ -1, 1, 3, 4 ]

---

## Step 4

i = 1

nums[1] = 1

Correct index:

0

Swap:

[1, -1, 3, 4]

---

## Step 5

i = 1

nums[1] = -1

Ignore it.

---

Now:

[1, -1, 3, 4]

Scan the array.

Index 0:

expected = 1

nums[0] = 1

Correct.

Index 1:

expected = 2

nums[1] = -1

Wrong.

Therefore:

2 is the first missing positive.

Answer:

2

---

# Why Is the Time Complexity O(n)?

At first, the while loop appears to contain a nested operation.

However, every successful swap places a value into its
correct position.

An element cannot be moved into a new correct position
indefinitely.

The total number of useful swaps is bounded by O(n).

Then we perform one final O(n) scan.

Therefore:

O(n)

---

# Space Complexity

O(1)

## Why?

We modify the input array directly.

Only a few variables are used:

- i
- n
- correctIndex

No additional array, dictionary, set, or collection is created.

---

# Edge Cases

## 1. Missing 1

Input:

[2, 3, 4]

Output:

1

---

## 2. All positive numbers present

Input:

[1, 2, 3]

Output:

4

---

## 3. Negative numbers

Input:

[-1, -2, -3]

Output:

1

---

## 4. Zero

Input:

[0, 1, 2]

Output:

3

---

## 5. Duplicates

Input:

[1, 1]

Output:

2

---

## 6. Mixed values

Input:

[7, 8, 9, 11, 12]

Output:

1

---

## 7. Null

Throws:

ArgumentNullException

---

## 8. Empty array

Throws:

ArgumentException

---

# Key DSA Pattern

Cyclic Sort

The important relationship is:

value x → index x - 1

This pattern is useful for problems involving:

- Missing numbers
- Duplicate numbers
- Numbers in the range 1..n
- Finding the first missing positive
- In-place array rearrangement

---

# Interview Tip

When you see:

"Find missing/duplicate number"

and the values are related to:

1..n

ask yourself:

Can I place each value at its correct index?

If yes, consider:

Cyclic Sort / In-place Placement.