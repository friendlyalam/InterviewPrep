# Array Problem 23 — Find the Duplicate Number

In-place marking: O(n) / O(1), but modifies the array.
Floyd's Cycle Detection: O(n) / O(1), does not modify the array → actual optimal solution for the stated problem.

## Problem

Given an array `nums` containing `n + 1` integers where each integer is in the range `[1, n]`, find the one number that appears more than once.

There is exactly one duplicated number, but it may appear more than twice.

The array must not be modified.

---

# Optimal Approach — Floyd's Cycle Detection

## Why?

We need:

* O(n) time
* O(1) extra space
* No modification of the input array

A HashSet would use O(n) extra space.

In-place marking would use O(1) extra space but would modify the input array.

Therefore, the optimal approach that satisfies all requirements is **Floyd's Tortoise and Hare Cycle Detection**.

---

# Key Idea

Treat each array value as the next index.

For every index:

```text
index → nums[index]
```

Because every value is between `1` and `n`, every value points to another valid position.

Since there are `n + 1` elements but only `n` possible values, at least two positions must point to the same value.

That creates a cycle.

The duplicate number is the **entrance of that cycle**.

---

# Example

Input:

```text
[1,3,4,2,2]
```

Think of the array as links:

```text
0 → 1
1 → 3
2 → 4
3 → 2
4 → 2
```

Following the links:

```text
0 → 1 → 3 → 2 → 4 → 2 → 4 → 2 ...
```

The cycle is:

```text
2 → 4 → 2
```

The cycle entrance is:

```text
2
```

Therefore:

```text
Duplicate = 2
```

---

# Floyd's Algorithm

Floyd's algorithm has two phases.

## Phase 1 — Find the Intersection Point

Use two pointers:

```text
slow
fast
```

The slow pointer moves one step:

```text
slow = nums[slow]
```

The fast pointer moves two steps:

```text
fast = nums[nums[fast]]
```

Because a cycle exists, they will eventually meet inside the cycle.

---

## Phase 2 — Find the Cycle Entrance

After `slow` and `fast` meet:

Reset `slow` to the beginning:

```text
slow = nums[0]
```

Then move both one step at a time:

```text
slow = nums[slow]
fast = nums[fast]
```

When they meet again, that position is the cycle entrance.

That value is the duplicate number.

---

# Dry Run

Input:

```text
[1,3,4,2,2]
```

## Phase 1

Initial:

```text
slow = nums[0] = 1
fast = nums[0] = 1
```

First movement:

```text
slow = nums[1] = 3
fast = nums[nums[1]]
     = nums[3]
     = 2
```

Now:

```text
slow = 3
fast = 2
```

Next:

```text
slow = nums[3] = 2
fast = nums[nums[2]]
     = nums[4]
     = 2
```

They meet:

```text
slow = 2
fast = 2
```

The intersection is inside the cycle.

---

# Phase 2

Reset:

```text
slow = nums[0] = 1
fast = 2
```

Move both one step:

```text
slow = nums[1] = 3
fast = nums[2] = 4
```

Move again:

```text
slow = nums[3] = 2
fast = nums[4] = 2
```

They meet at:

```text
2
```

Therefore:

```text
Duplicate = 2
```

---

# Why Does the Duplicate Create a Cycle?

Suppose:

```text
nums = [3,1,3,4,2]
```

The links are:

```text
0 → 3
1 → 1
2 → 3
3 → 4
4 → 2
```

Following from index `0`:

```text
0 → 3 → 4 → 2 → 3 → 4 → 2 ...
```

The cycle is:

```text
3 → 4 → 2 → 3
```

The cycle entrance is:

```text
3
```

And `3` is the duplicate.

---

# Time Complexity

**O(n)**

## Why?

Phase 1 takes O(n) in the worst case.

Phase 2 also takes O(n) in the worst case.

Therefore:

```text
O(n) + O(n) = O(n)
```

Constant factors are ignored.

---

# Space Complexity

**O(1)**

## Why?

Only two variables are used:

```text
slow
fast
```

No:

* HashSet
* Dictionary
* additional array
* list for tracking

Therefore:

```text
O(1)
```

extra space.

---

# Edge Cases

## 1. Duplicate Appears Twice

```text
Input:
[1,3,4,2,2]

Output:
2
```

---

## 2. Duplicate Appears More Than Twice

```text
Input:
[2,2,2,2,3]

Output:
2
```

---

## 3. Duplicate Is 1

```text
Input:
[1,1]

Output:
1
```

---

## 4. Duplicate Is the Largest Possible Value

```text
Input:
[1,2,3,4,5,5]

Output:
5
```

---

## 5. Null Input

Throws:

```text
ArgumentNullException
```

---

## 6. Insufficient Input

An array containing fewer than two elements throws:

```text
ArgumentException
```

---

# Why Not In-Place Marking?

In-place marking can achieve:

```text
Time: O(n)
Space: O(1)
```

However, it modifies the input array.

The problem explicitly says:

```text
The array must not be modified.
```

Therefore, Floyd's Cycle Detection is the appropriate optimal solution.

---

# Better vs Optimal

| Approach                | Time | Extra Space | Modifies Input |
| ----------------------- | ---: | ----------: | -------------- |
| HashSet                 | O(n) |        O(n) | No             |
| In-Place Marking        | O(n) |        O(1) | Yes            |
| Floyd's Cycle Detection | O(n) |        O(1) | No             |

---

# Key DSA Pattern

## Floyd's Tortoise and Hare

Use Floyd's Cycle Detection when a problem can be represented as:

```text
value → next position/value
```

and there is a guaranteed cycle.

The two important phases are:

```text
Phase 1:
Find intersection inside cycle

Phase 2:
Find cycle entrance
```

For this problem:

```text
Cycle entrance = Duplicate Number
```

---

# Final Complexity

```text
Time:  O(n)
Space: O(1)
```

The solution does not modify the input array.
