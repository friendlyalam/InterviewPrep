# Problem 38 — 3Sum

## Problem

Given an integer array `nums`, find all unique triplets:

```text
nums[i] + nums[j] + nums[k] = 0

The same triplet must not appear more than once.

Example:

Input:
[-1, 0, 1, 2, -1, -4]

Output:
[
    [-1, -1, 2],
    [-1, 0, 1]
]
Better Solution
Why

The brute-force approach checks every possible combination of three elements.

That requires:

O(n³)

We can improve this by fixing the first element and using a HashSet to find the required third value.

Idea

First sort the array.

For every i:

nums[i]

Then scan the remaining elements.

For the current value:

nums[j]

the required value is:

required = -(nums[i] + nums[j])

If required has already been seen, we found a valid triplet.

Example:

[-1, 0, 1, 2]

i = -1
j = 0

required = -(-1 + 0)
         = 1

1 exists in HashSet

Triplet:
[-1, 0, 1]

Duplicate values are skipped so that duplicate triplets are not returned.

Dry Run

Input:

[-1, 0, 1, 2, -1, -4]

After sorting:

[-4, -1, -1, 0, 1, 2]

Fix:

-4

No valid triplet.

Fix:

-1

Consider:

0

Required:

-(-1 + 0) = 1

1 exists.

Triplet:

[-1, 0, 1]

Consider:

1

Required:

-(-1 + 1) = 0

0 was already seen.

Triplet:

[-1, 0, 1]

Duplicate handling prevents returning it again.

For the second -1, we skip it because it is equal to the previous -1.

Another valid triplet:

[-1, -1, 2]

Final result:

[
    [-1, -1, 2],
    [-1, 0, 1]
]
Better Complexity
Time
O(n²)

Why?

The outer loop runs approximately n times.

For each outer iteration, the inner loop scans approximately n elements.

Therefore:

n × n = O(n²)

Sorting adds:

O(n log n)

But:

O(n²) + O(n log n) = O(n²)
Space
O(n)

Why?

The HashSet can contain up to n elements.

Optimal Solution
Why

The HashSet is not necessary after sorting.

Sorting gives us an important property:

smaller values → left
larger values  → right

Therefore we can use two pointers.

Idea

For every index i:

i = first element
left = i + 1
right = last element

Calculate:

sum = nums[i] + nums[left] + nums[right]
If sum == 0

We found a triplet.

Move both pointers:

left++
right--
If sum < 0

The sum is too small.

We need a larger value.

Move:

left++
If sum > 0

The sum is too large.

We need a smaller value.

Move:

right--
Dry Run

Input:

[-1, 0, 1, 2, -1, -4]

Sorted:

[-4, -1, -1, 0, 1, 2]

Take:

i = -1
left = -1
right = 2

Sum:

-1 + -1 + 2 = 0

Triplet:

[-1, -1, 2]

Move:

left++
right--

Now:

left = 0
right = 1

Sum:

-1 + 0 + 1 = 0

Triplet:

[-1, 0, 1]

Move pointers again.

No more valid combinations.

Duplicate Handling

This is one of the most important parts of 3Sum.

Suppose:

[-1, -1, -1, 0, 1]

If we process every -1, we could return the same triplet multiple times.

Therefore:

if (i > 0 && nums[i] == nums[i - 1])
    continue;

After finding a valid triplet, duplicate left and right values are also skipped.

Important Optimization

Because the array is sorted:

if (nums[i] > 0)
    break;

If the first number is already positive, the other two numbers are also positive.

Therefore their sum can never be zero.

Example:

[1, 2, 3, 4]

There is no possible triplet whose sum is zero.

Optimal Complexity
Time
O(n²)

Why?

Sorting:

O(n log n)

For each i, the two pointers move only forward/backward through the remaining array:

O(n)

Across approximately n values of i:

O(n × n)
= O(n²)

Therefore:

O(n log n) + O(n²)
= O(n²)
Space
O(1)

The algorithm uses only:

i
left
right

as additional working variables.

The returned triplets themselves require output space, but that is not counted as auxiliary space.

Edge Cases
nums == null
Empty array
Array contains fewer than 3 elements
No valid triplet
All zeros
Duplicate values
Negative numbers
Positive numbers only
Multiple unique triplets
Large number of duplicate values
Key DSA Pattern
Sorting + Two Pointers

3Sum is an important combination of:

Array
↓
Sorting
↓
Two Pointers
↓
Duplicate Handling

This pattern is frequently used in interview problems involving:

2Sum variants
3Sum
4Sum
Pair/triplet problems
Closest sum problems
Container problems