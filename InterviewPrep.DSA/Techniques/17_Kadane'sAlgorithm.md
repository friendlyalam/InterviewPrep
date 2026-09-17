Kadane's Algorithm — Complete Technique

Since you're learning DSA systematically, learn Kadane's Algorithm as a reusable technique, not just as one LeetCode solution.

1. What problem does Kadane's Algorithm solve?

The classic Kadane's Algorithm finds the maximum sum of a contiguous subarray.

Example
Input:
[-2, 1, -3, 4, -1, 2, 1, -5, 4]

The subarray:

[4, -1, 2, 1]

has sum:

4 + (-1) + 2 + 1 = 6

So:

Maximum subarray sum = 6

The important word is contiguous.

That means you cannot skip elements.

2. What does "contiguous" mean?

Given:

[1, 2, 3, 4, 5]

Valid subarrays:

[1, 2]
[2, 3, 4]
[3, 4, 5]
[1, 2, 3, 4, 5]

Invalid:

[1, 3, 5]

because elements were skipped.

3. Why do we need Kadane's Algorithm?

A brute-force solution could examine every possible subarray.

For:

[1, -2, 3, 4]

Possible subarrays include:

[1]
[1,-2]
[1,-2,3]
[1,-2,3,4]

[-2]
[-2,3]
[-2,3,4]

[3]
[3,4]

[4]

There are approximately:

n(n + 1) / 2

subarrays.

So brute force is:

O(n²)

Kadane's Algorithm solves the maximum-sum problem in:

O(n)

with:

O(1)

extra space.

4. The fundamental idea

The key question at every element is:

Should I continue the previous subarray, or should I start a new subarray here?

Suppose we have:

previousSum
currentNumber

There are two choices:

Choice 1 — Continue
previousSum + currentNumber
Choice 2 — Start fresh
currentNumber

We choose the larger:

currentSum = Math.Max(currentNumber,
                      previousSum + currentNumber);

This is the heart of Kadane's Algorithm.

5. Two variables

We generally maintain:

currentSum
maxSum
currentSum

The maximum sum of a subarray ending at the current position.

This phrase is extremely important.

maxSum

The maximum sum found anywhere so far.

6. Example

Consider:

[-2, 1, -3, 4, -1, 2, 1, -5, 4]

Let's process it.

| Number | Current Sum | Maximum Sum |
| -----: | ----------: | ----------: |
|     -2 |          -2 |          -2 |
|      1 |           1 |           1 |
|     -3 |          -2 |           1 |
|      4 |           4 |           4 |
|     -1 |           3 |           4 |
|      2 |           5 |           5 |
|      1 |           6 |           6 |
|     -5 |           1 |           6 |
|      4 |           5 |           6 |


Final:

maxSum = 6
7. Let's understand the important decisions

Start:

[-2]

Current:

currentSum = -2
maxSum = -2

Next:

1

We have two possibilities:

continue:
-2 + 1 = -1

start new:
1

Choose:

1

because:

1 > -1

So:

currentSum = 1

This is the key idea.

The previous negative sum is hurting us, so we discard it.

8. Why can we discard the previous sum?

Suppose:

currentSum = -5

and next number is:

10

Options:

continue:
-5 + 10 = 5

start new:
10

Clearly:

10 > 5

So carrying -5 forward would only make the future subarray worse.

Therefore:

If the previous running sum hurts the current element, start a new subarray.

This is the central intuition behind Kadane's Algorithm.

9. The formula

At every element:

currentSum = max(
    currentNumber,
    currentSum + currentNumber
)

Then:

maxSum = max(maxSum, currentSum)

In C#:

currentSum = Math.Max(nums[i], currentSum + nums[i]);

maxSum = Math.Max(maxSum, currentSum);
10. C# implementation

For your DSA project:

public static int MaxSubArray(int[] nums)
{
    int currentSum = nums[0];
    int maxSum = nums[0];

    for (int i = 1; i < nums.Length; i++)
    {
        currentSum = Math.Max(nums[i], currentSum + nums[i]);

        maxSum = Math.Max(maxSum, currentSum);
    }

    return maxSum;
}
11. Why initialize with nums[0]?

You might see this:

int currentSum = 0;
int maxSum = 0;

That can create a problem when all numbers are negative.

Consider:

[-5, -2, -8]

Correct answer:

-2

because the maximum subarray is:

[-2]

If we initialize:

maxSum = 0

we might incorrectly return:

0

But 0 isn't even a subarray sum here.

Therefore:

int currentSum = nums[0];
int maxSum = nums[0];

is the safer standard approach when the input is non-empty.

12. All-negative example

Input:

[-5, -2, -8]

Start:

currentSum = -5
maxSum = -5

Process -2:

continue:
-5 + -2 = -7

start new:
-2

Choose:

-2

Now:

currentSum = -2
maxSum = -2

Process -8:

continue:
-2 + -8 = -10

start new:
-8

Choose:

-8

But:

maxSum = max(-2, -8)
       = -2

Answer:

-2

Correct.

13. Another important example
[5, -10, 6, 7]

Start:

current = 5
max = 5

Next:

5 + (-10) = -5

vs:

-10

Choose:

-5

Next 6:

continue = -5 + 6 = 1
new = 6

Choose:

6

Next 7:

continue = 6 + 7 = 13
new = 7

Choose:

13

Answer:

13

Subarray:

[6, 7]
14. Kadane's Algorithm is a DP technique

This is an important interview concept.

Kadane's Algorithm can be viewed as Dynamic Programming.

Define:

currentSum =
maximum subarray sum ending at index i

Then:

currentSum[i]
=
max(
    nums[i],
    currentSum[i - 1] + nums[i]
)

We only need the previous value:

currentSum[i - 1]

Therefore we don't need an entire DP array.

We optimize:

O(n) space

to:

O(1) space

This is often called space-optimized DP.

15. Brute Force vs Kadane
Brute force
Generate every subarray
Calculate its sum
Keep maximum

Complexity:

O(n²) or O(n³)

depending on implementation.

Prefix sum optimization

Can reduce sum calculation but still examines:

O(n²)

subarrays.

Kadane

Only one pass:

O(n)

So:

Brute Force → O(n²) / O(n³)
Prefix Sum   → O(n²)
Kadane       → O(n)
16. Kadane's Algorithm can also find the actual subarray

The basic version returns only:

maximum sum

But you can also track:

start
end

For example:

[-2, 1, -3, 4, -1, 2, 1, -5, 4]

Answer:

sum = 6
subarray = [4, -1, 2, 1]

We introduce:

start
end
tempStart

The idea:

Whenever we decide:

start new subarray

we set:

tempStart = i;

Whenever we find a new maximum:

maxSum = currentSum;
start = tempStart;
end = i;

C#:

public static (int Sum, int Start, int End) MaxSubArray(int[] nums)
{
    int currentSum = nums[0];
    int maxSum = nums[0];

    int tempStart = 0;
    int start = 0;
    int end = 0;

    for (int i = 1; i < nums.Length; i++)
    {
        if (nums[i] > currentSum + nums[i])
        {
            currentSum = nums[i];
            tempStart = i;
        }
        else
        {
            currentSum += nums[i];
        }

        if (currentSum > maxSum)
        {
            maxSum = currentSum;
            start = tempStart;
            end = i;
        }
    }

    return (maxSum, start, end);
}
17. Recognition pattern

When you see:

Maximum sum of a contiguous subarray

think immediately:

Contiguous
+
Maximum Sum
        ↓
Kadane's Algorithm

Other common wording:

Find the contiguous subarray having the largest sum.

or:

Return the maximum possible sum of a non-empty subarray.

These should trigger:

Kadane
18. Where Kadane's Algorithm does NOT directly apply

Don't confuse subarray with subsequence.

Subarray

Must be contiguous:

[2, 3, 4]
Subsequence

Can skip:

[2, 4]

For example:

[5, -10, 6, 7]

Maximum subarray:

[6, 7]
sum = 13

But maximum subsequence could be:

[5, 6, 7]
sum = 18

Different problem.

19. Important variations

Once you understand basic Kadane, you'll encounter variations.

Variation 1 — Maximum subarray sum

Classic:

O(n)
O(1)
Variation 2 — Return the actual subarray

Track:

start
end
Variation 3 — Maximum circular subarray sum

Example:

[5, -3, 5]

Normal Kadane:

5 + (-3) + 5 = 7

But circular subarray:

[5] + [5]

gives:

10

This uses a variation of Kadane involving:

maximum subarray
+
minimum subarray
Variation 4 — Maximum product subarray

This is not standard Kadane, but it uses a related dynamic-programming idea.

Because negative numbers can turn:

negative × negative = positive

we track both:

currentMax
currentMin
20. Common mistakes
Mistake 1

Using:

int maxSum = 0;

when all-negative arrays are allowed.

Prefer:

int maxSum = nums[0];
Mistake 2

Thinking currentSum is the global maximum.

It isn't.

currentSum → best sum ending HERE
maxSum     → best sum found ANYWHERE

This distinction is critical.

Mistake 3

Sorting the array.

Sorting destroys the contiguous-subarray relationship.

Mistake 4

Using a dictionary.

There is no need to count frequencies.

21. The mental model you should remember

At every element, ask:

Should I continue what I have, or start fresh?

Mathematically:

currentSum =
    max(current element,
        previous currentSum + current element)

Then:

maxSum =
    max(maxSum, currentSum)

That's Kadane.

22. Interview-ready explanation

If an interviewer asks:

"Explain Kadane's Algorithm."

A good answer is:

"Kadane's Algorithm finds the maximum sum of a contiguous subarray in O(n) time and O(1) extra space.
At each element, I decide whether to extend the previous subarray or start a new subarray from the current element.
I maintain currentSum, representing the maximum sum of a subarray ending at the current position, and maxSum,
representing the maximum sum found so far. The transition is currentSum = max(nums[i], currentSum + nums[i]), followed by updating maxSum."

The one-line memory trick:
Kadane = CONTINUE or RESTART

And the formula:

currentSum = max(num, currentSum + num)
maxSum     = max(maxSum, currentSum)

Complexity: O(n) time, O(1) space.