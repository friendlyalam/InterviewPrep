# Problem 43 — Next Greater Element II

## Problem

Given a circular array, find the next greater element for every element.

If no greater element exists, return `-1`.

Example:

Input:
[1, 2, 1]

Output:
[2, -1, 2]

---

# Solution 1 — Better

## Why

For every element, we can simply check the elements to its right.

Because the array is circular, we use:

(i + step) % n

This allows us to wrap from the end of the array back to the beginning.

## Idea

For every index:

1. Start checking the next element.
2. Continue up to `n - 1` positions.
3. Use `% n` to handle circular movement.
4. The first greater element is the answer.
5. If no greater element is found, keep `-1`.

## Dry Run

nums = [1, 2, 1]

For index 0:

Current = 1

Next:
2 → greater than 1

Answer = 2

For index 1:

Current = 2

Next:
1
1

No greater element.

Answer = -1

For index 2:

Current = 1

Next circular element:
1

Then:
2 → greater than 1

Answer = 2

Final:

[2, -1, 2]

## Time Complexity

O(n²)

## Why?

For each of the n elements, we may scan almost n other elements.

Therefore:

n × n = O(n²)

## Space Complexity

O(n)

## Why?

The result array contains n elements.

---

# Solution 2 — Optimal

## Key DSA Pattern

Monotonic Stack

---

## Why

The brute-force solution repeatedly checks the same elements.

For example, if several smaller elements are waiting for a greater element, we can process them together.

A monotonic decreasing stack allows us to remember indexes that are still waiting for their next greater element.

---

## Idea

We process the array twice.

Why twice?

Because the array is circular.

For:

[1, 2, 1]

The last `1` can find `2` by wrapping around.

Processing:

[1, 2, 1, 1, 2, 1]

simulates this circular behavior.

However, we don't actually create this second array.

We use:

i % n

to simulate it.

---

## Stack Rule

The stack contains indexes whose next greater element has not been found yet.

When:

nums[current] > nums[stack.Peek()]

the current value is the next greater element for the index on top of the stack.

So we:

1. Pop the index.
2. Set its answer to the current value.

---

## Dry Run

nums:

[1, 2, 1]

### i = 0

Current:

1

Stack:

[]

Push index 0.

Stack:

[0]

---

### i = 1

Current:

2

Top of stack:

nums[0] = 1

2 > 1

Therefore:

result[0] = 2

Pop 0.

Push 1.

Stack:

[1]

---

### i = 2

Current:

1

1 is not greater than nums[1] = 2.

Push 2.

Stack:

[1, 2]

---

### Second pass

Now we simulate circular movement.

Current values again:

1, 2, 1

---

Current = 1

Top:

nums[2] = 1

1 is not greater than 1.

No change.

---

Current = 2

Top:

nums[2] = 1

2 > 1

Therefore:

result[2] = 2

Pop 2.

Now top:

nums[1] = 2

2 is not greater than 2.

Stop.

Final result:

[2, -1, 2]

---

# Why We Only Push During the First Pass

We process the array twice to find circular answers.

But each index should only be inserted into the stack once.

Therefore:

if (i < n)

we push the index.

During the second pass, we only use existing indexes to resolve their answers.

---

# Time Complexity

O(n)

## Why?

There are 2n iterations.

2n is O(n).

More importantly:

- Every index is pushed at most once.
- Every index is popped at most once.

Therefore the total stack operations are O(n).

---

# Space Complexity

O(n)

## Why?

The stack can contain up to n indexes.

The result array also contains n elements.

Auxiliary stack space is O(n).

---

# Edge Cases

## 1. Single element

Input:

[5]

Output:

[-1]

There is no different element that can be greater.

---

## 2. All elements equal

Input:

[3, 3, 3]

Output:

[-1, -1, -1]

Equal values are not greater.

---

## 3. Strictly increasing

Input:

[1, 2, 3, 4]

Output:

[2, 3, 4, -1]

---

## 4. Strictly decreasing

Input:

[5, 4, 3, 2, 1]

Output:

[-1, 5, 5, 5, 5]

The smaller elements find 5 after wrapping around.

---

## 5. Negative numbers

Input:

[-3, -2, -5]

Output:

[-2, -1, -3]

---

# Important Learning

This problem introduces an important interview pattern:

## Circular Array + Monotonic Stack

When you see:

- Next Greater Element
- Next Smaller Element
- Previous Greater Element
- Previous Smaller Element

think about a Monotonic Stack.

When the array is circular, consider processing the array twice.