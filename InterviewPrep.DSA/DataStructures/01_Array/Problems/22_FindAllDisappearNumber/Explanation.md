# Array Problem 22 — Find All Numbers Disappeared in an Array

## Problem

Given an array `nums` containing `n` integers where every integer is in the range `[1, n]`, find all numbers in the range `[1, n]` that do not appear in the array.

Duplicates may exist.

---

# Example

Input:

[4,3,2,7,8,2,3,1]

Output:

[5,6]

Numbers from 1 to 8 should be:

1,2,3,4,5,6,7,8

Numbers actually present:

1,2,3,4,7,8

Therefore:

5,6

are missing.

---

# Better Approach

## Why?

We need to know which numbers from `1` to `n` actually appeared.

A formula can calculate the expected sum:

n * (n + 1) / 2

However, because duplicates can exist, comparing expected sum with actual sum is not enough to identify every missing number.

Therefore, we use a boolean array to track which numbers appeared.

---

## Idea

1. Calculate `n`.
2. Create a boolean array of size `n + 1`.
3. For every number in `nums`, mark that number as appeared.
4. Loop from `1` to `n`.
5. If a number was not marked, add it to the result.

The formula can also calculate the expected total sum, but it is not required for identifying all missing numbers when duplicates are possible.

---

## Dry Run

Input:

[4,3,2,7,8,2,3,1]

n = 8

Initially:

appeared = [false, false, false, false, false, false, false, false, false]

After processing the array:

1 -> true
2 -> true
3 -> true
4 -> true
5 -> false
6 -> false
7 -> true
8 -> true

Now loop from 1 to 8:

1 -> appeared
2 -> appeared
3 -> appeared
4 -> appeared
5 -> missing
6 -> missing
7 -> appeared
8 -> appeared

Result:

[5,6]

---

## Time Complexity

O(n)

### Why?

First loop:

O(n)

Second loop:

O(n)

Therefore:

O(n) + O(n) = O(n)

---

## Space Complexity

O(n)

### Why?

We create a boolean array of size `n + 1`.

Therefore, extra space is:

O(n)

---

# Optimal Approach — In-Place Marking

## Why?

The problem guarantees that every number is between `1` and `n`.

Therefore, every number can be mapped to an index:

number → number - 1

For example:

1 → index 0
2 → index 1
3 → index 2
4 → index 3

We can use the input array itself to store whether a number appeared.

This eliminates the need for an additional boolean array.

---

## Idea

For every number:

```text
index = Math.Abs(number) - 1

Make the value at that index negative.

Negative means:

"this number has appeared."

After processing all elements:

Negative value → number appeared
Positive value → number is missing

The missing number is:

index + 1
Dry Run

Input:

[4,3,2,7,8,2,3,1]

Process 4

Index:

4 - 1 = 3

Mark index 3:

[4,3,2,-7,8,2,3,1]

Process 3

Index:

3 - 1 = 2

[4,3,-2,-7,8,2,3,1]

Process 2

Index:

2 - 1 = 1

[4,-3,-2,-7,8,2,3,1]

Process 7

Index:

7 - 1 = 6

[4,-3,-2,-7,8,2,-3,1]

Continue for all elements.

Eventually, the positions corresponding to 5 and 6 remain positive.

Therefore:

index 4 → number 5
index 5 → number 6

Result:

[5,6]

Why Math.Abs()?

Suppose we encounter:

-3

This means number 3 was already encountered.

We still need to use 3 as an index.

Therefore:

Math.Abs(-3) = 3

Then:

3 - 1 = 2

Why Does This Work With Duplicates?

Suppose number 2 appears multiple times.

The first time:

nums[1] = -nums[1]

The second time, nums[1] is already negative.

We do not need to change it again.

Therefore, duplicates do not affect the result.

Time Complexity

O(n)

Why?

We perform two separate loops:

First loop marks the numbers → O(n)
Second loop finds positive positions → O(n)

Therefore:

O(n) + O(n) = O(n)

Space Complexity

O(1) extra space

Why?

We do not create another array, HashSet, or Dictionary.

We modify the input array itself.

The result list does contain the answer, but that is output space and is normally not counted as auxiliary space.

Therefore:

Extra Space = O(1)

Edge Cases
1. No Missing Numbers

Input:

[1,2,3,4,5]

Output:

[]

2. One Missing Number

Input:

[1,1]

Output:

[2]

3. Multiple Missing Numbers

Input:

[4,3,2,7,8,2,3,1]

Output:

[5,6]

4. All Values Are the Same

Input:

[2,2,2,2]

Output:

[1,3,4]

5. Duplicate Values

Input:

[1,1,2,2]

Output:

[3,4]

6. Single Element

Input:

[1]

Output:

[]

7. Null Input

The method throws:

ArgumentNullException

8. Empty Input

The method throws:

ArgumentException