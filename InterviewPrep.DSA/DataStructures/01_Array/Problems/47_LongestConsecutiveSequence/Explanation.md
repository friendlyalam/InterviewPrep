# Problem 47 — Longest Consecutive Sequence

## Problem

Given an unsorted integer array, find the length of the longest
consecutive sequence.

The sequence elements do not need to be adjacent in the original
array.

Example:

[100, 4, 200, 1, 3, 2]

The longest sequence is:

[1, 2, 3, 4]

Answer:

4

---

# Solution 1 — Sorting

## Why

The array is unsorted.

If we sort it, consecutive numbers become adjacent.

Then we can scan from left to right.

---

## Idea

After sorting:

1. Ignore duplicates.
2. If current number is previous number + 1:
   - continue the sequence.
3. Otherwise:
   - start a new sequence.
4. Keep track of the longest sequence.

---

## Dry Run

Input:

[100, 4, 200, 1, 3, 2]

After sorting:

[1, 2, 3, 4, 100, 200]

Start:

currentLength = 1
longest = 1

---

### 1 → 2

2 = 1 + 1

currentLength:

2

longest:

2

---

### 2 → 3

3 = 2 + 1

currentLength:

3

longest:

3

---

### 3 → 4

4 = 3 + 1

currentLength:

4

longest:

4

---

### 4 → 100

100 is not 4 + 1.

Start a new sequence.

currentLength:

1

The longest remains:

4

---

Final answer:

4

---

## Duplicate Handling

Example:

[1, 2, 2, 3]

After sorting:

[1, 2, 2, 3]

When we encounter the second 2, we ignore it.

The sequence is still:

[1, 2, 3]

Length:

3

---

## Time Complexity

O(n log n)

## Why?

Sorting takes:

O(n log n)

The subsequent scan takes:

O(n)

Therefore:

O(n log n)

dominates.

---

## Space Complexity

O(log n) auxiliary space in the typical .NET sorting
implementation.

The input array is modified.

---

# Solution 2 — HashSet

## Key DSA Pattern

Hashing / Set Lookup

---

# Why

Sorting takes O(n log n).

We can do better using a HashSet.

A HashSet gives average O(1) lookup.

Therefore, we can quickly determine whether:

number - 1

or:

number + 1

exists.

---

# Important Observation

Suppose:

[1, 2, 3, 4]

We should not start counting from every number.

If we start from 1:

1 → 2 → 3 → 4

We find the complete sequence.

But if we later start from 2:

2 → 3 → 4

we are repeating work.

Therefore, only start counting when:

number - 1 does NOT exist.

---

# Example

nums:

[100, 4, 200, 1, 3, 2]

HashSet:

{100, 4, 200, 1, 3, 2}

---

## Number = 1

Check:

0 exists?

No.

Therefore 1 is the start of a sequence.

Check:

2 exists → yes

3 exists → yes

4 exists → yes

5 exists → no

Sequence:

1 → 2 → 3 → 4

Length:

4

---

## Number = 2

Check:

1 exists?

Yes.

Therefore 2 is NOT a sequence start.

Skip it.

---

## Number = 3

Check:

2 exists?

Yes.

Skip it.

---

## Number = 4

Check:

3 exists?

Yes.

Skip it.

---

## Number = 100

Check:

99 exists?

No.

Start sequence.

101 exists?

No.

Length:

1

---

## Number = 200

Check:

199 exists?

No.

Start sequence.

201 exists?

No.

Length:

1

---

Final answer:

4

---

# Why Is This O(n) Average?

Each number is inserted into the HashSet once.

Only sequence-start numbers perform the forward scan.

For a sequence:

1, 2, 3, 4

only 1 starts the scan.

2, 3 and 4 are skipped because their previous values exist.

Therefore, the same long sequence is not repeatedly scanned.

HashSet operations are O(1) on average.

Overall:

O(n) average

---

# Space Complexity

O(n)

## Why?

The HashSet stores the array's values.

In the worst case it contains n unique values.

---

# Edge Cases

## 1. Empty array

Input:

[]

Output:

0

---

## 2. Single element

Input:

[10]

Output:

1

---

## 3. Duplicates

Input:

[1, 2, 2, 3]

Output:

3

---

## 4. Negative numbers

Input:

[-3, -2, -1, 5]

Longest sequence:

[-3, -2, -1]

Output:

3

---

## 5. Mixed values

Input:

[100, 4, 200, 1, 3, 2]

Output:

4

---

## 6. All values identical

Input:

[5, 5, 5, 5]

Output:

1

---

## 7. Negative to positive sequence

Input:

[-2, -1, 0, 1, 2]

Output:

5

---

## 8. Integer boundaries

The implementation avoids overflowing when checking:

number - 1

and:

number + 1

for int.MinValue and int.MaxValue.

---

# Key DSA Patterns

- HashSet
- Constant-time lookup
- Sequence detection
- Start-of-sequence technique

---

# Interview Learning

When you see:

"Longest consecutive sequence"

think:

1. Sorting
2. HashSet

The important optimization is:

Do NOT start a sequence from every number.

Only start when:

number - 1 does not exist.

This converts the solution from repeated scanning into
an O(n) average-time solution.