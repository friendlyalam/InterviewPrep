# Array Problem 19 — Maximum Subarray

## Problem

Given an integer array `nums`, find the contiguous subarray with the largest sum and return its sum.

A contiguous subarray contains consecutive elements from the original array.

Example:

Input:
[-2,1,-3,4,-1,2,1,-5,4]

Output:
6

The maximum subarray is:

[4,-1,2,1]

Sum:

4 + (-1) + 2 + 1 = 6

---

# Better Approach — Brute Force

## Why?

We need to consider every possible contiguous subarray.

A subarray can start at any index and end at any index after its starting position.

Therefore, we can:

1. Choose a starting index.
2. Expand the subarray one element at a time.
3. Keep calculating its sum.
4. Track the maximum sum.

## Idea

Use two loops:

- The outer loop chooses the starting position.
- The inner loop expands the subarray.

Instead of calculating every subarray sum from scratch, keep a running sum.

## Dry Run

Input:

[-2,1,-3,4,-1,2,1,-5,4]

Start at index containing `4`:

4

Then expand:

4 + (-1) = 3

4 + (-1) + 2 = 5

4 + (-1) + 2 + 1 = 6

Therefore:

Maximum sum = 6

## Time Complexity

O(n²)

### Why?

For each starting index, we may visit all elements after it.

The total number of possible contiguous subarrays is approximately:

n × (n + 1) / 2

Therefore:

O(n²)

## Space Complexity

O(1)

### Why?

Only variables such as:

- currentSum
- maxSum
- start
- end

are used.

No additional data structure is created.

---

# Optimal Approach — Kadane's Algorithm

## Why?

The brute-force approach examines many subarrays.

We can solve the problem in one traversal by asking a simple question for every element:

Should we:

1. Start a new subarray with the current element?
2. Add the current element to the previous subarray?

The better choice is whichever gives the larger sum.

## Idea

Maintain two values:

- `currentSum` — maximum sum of a subarray ending at the current position.
- `maxSum` — maximum sum found anywhere so far.

For every element:

currentSum = max(current element, currentSum + current element)

Then:

maxSum = max(maxSum, currentSum)

## Dry Run

Input:

[-2,1,-3,4,-1,2,1,-5,4]

Initial:

currentSum = -2
maxSum = -2

### Element = 1

Choose:

max(1, -2 + 1)

= max(1, -1)

= 1

currentSum = 1
maxSum = 1

### Element = -3

max(-3, 1 + -3)

= max(-3, -2)

= -2

currentSum = -2
maxSum = 1

### Element = 4

max(4, -2 + 4)

= max(4, 2)

= 4

currentSum = 4
maxSum = 4

### Element = -1

max(-1, 4 + -1)

= 3

currentSum = 3
maxSum = 4

### Element = 2

max(2, 3 + 2)

= 5

currentSum = 5
maxSum = 5

### Element = 1

max(1, 5 + 1)

= 6

currentSum = 6
maxSum = 6

Final answer:

6

## Why Does It Work?

At every position, `currentSum` represents the best possible subarray sum that MUST end at that position.

If adding the previous subarray produces a better result, continue it.

If the current element alone is better, discard the previous subarray and start a new one.

This guarantees that we always keep the best possible subarray ending at the current position.

## Time Complexity

O(n)

### Why?

Every element is processed exactly once.

There is only one loop through the array.

## Space Complexity

O(1)

### Why?

Only two main variables are required:

- currentSum
- maxSum

No additional array or collection is created.

---

# Edge Cases

## 1. Single Element

Input:

[5]

Output:

5

The subarray must contain at least one element.

---

## 2. All Negative Numbers

Input:

[-5,-2,-8,-1]

Output:

-1

We cannot return `0` because at least one element must be selected.

The best subarray is:

[-1]

---

## 3. All Positive Numbers

Input:

[1,2,3,4]

Output:

10

The entire array is the maximum subarray.

---

## 4. Positive and Negative Numbers

Input:

[-2,1,-3,4,-1,2,1,-5,4]

Output:

6

Maximum subarray:

[4,-1,2,1]

---

## 5. Maximum Subarray Starts at the Beginning

Input:

[4,-1,2,-3]

Output:

5

Maximum subarray:

[4,-1,2]

---

## 6. Maximum Subarray Ends at the End

Input:

[-3,2,3]

Output:

5

Maximum subarray:

[2,3]

---

## 7. Maximum Subarray Is the Entire Array

Input:

[5,4,-1,7,8]

Output:

23

---

## 8. Null Array

Input:

null

The method throws:

ArgumentNullException

This is defensive handling because the original problem constraints do not allow null.

---

## 9. Empty Array

Input:

[]

The method throws:

ArgumentException

This is defensive handling because the original problem requires at least one element.

---

# Key DSA Pattern

When you see:

- Find the maximum sum.
- Subarray must be contiguous.
- Elements can be positive or negative.

Think:

Kadane's Algorithm

Core decision:

currentSum = max(nums[i], currentSum + nums[i])

Complexity:

Time: O(n)
Space: O(1)