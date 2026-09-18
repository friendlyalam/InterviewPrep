1. What is Prefix + Suffix?

Prefix means processing information from the left side of an array.

Suffix means processing information from the right side of an array.

Prefix + Suffix means using information from both directions to solve a problem efficiently.

Think of it as:

                 Array
        ┌─────────────────────┐
        │  2   4   3   5   6  │
        └─────────────────────┘
          ────────────────►
              PREFIX

          ◄────────────────
              SUFFIX

The important idea is:

Instead of repeatedly calculating information on the left and right of every element, calculate it once and reuse it.

----------------------------------------------------------------------------------------------------------------------------------------------------------

2. Prefix

Prefix information describes everything relevant before or up to the current position.

Example:

nums = [2, 4, 3, 5]
Prefix Sum
Index:       0   1   2   3
Array:       2   4   3   5
Prefix Sum:  2   6   9   14

At index 2:

Prefix = 2 + 4 + 3
       = 9

----------------------------------------------------------------------------------------------------------------------------------------------------------
3. Suffix

Suffix information describes everything relevant after or from the current position toward the end.

For:

nums = [2, 4, 3, 5]

Suffix Sum:

Index:       0   1   2   3
Array:       2   4   3   5
Suffix Sum: 14   12  8   5

At index 1:

Suffix = 4 + 3 + 5
       = 12

So:

Prefix → left to right
Suffix → right to left

---------------------------------------------------------------------------------------------------------------------------------------------------------

4. Why do we need Prefix + Suffix?

Consider a problem:

For every element, find the sum of all elements except itself.

For:

[2, 4, 3, 5]

For 4, we need:

Left side:  2
Right side: 3 + 5

So:

2 + 3 + 5 = 10

Instead of calculating these repeatedly, we can precompute:

Prefix information
+
Suffix information

Then each position can be solved quickly.

---------------------------------------------------------------------------------------------------------------------------------------------------------
5. Basic Pattern

The general pattern looks like this:

// Prefix
for (int i = 0; i < nums.Length; i++)
{
    // calculate/store information from left
}

// Suffix
for (int i = nums.Length - 1; i >= 0; i--)
{
    // calculate/store information from right
}

Sometimes we don't even need arrays for prefix/suffix.

We can maintain variables:

int prefix = ...;
int suffix = ...;

This can reduce space from:

O(n) → O(1)

---------------------------------------------------------------------------------------------------------------------------------------------------------
6. Prefix + Suffix Product

This is the technique we were discussing for Maximum Product Subarray.

We maintain:

prefixProduct
suffixProduct
maxProduct

For:

[2, 3, -2, 4]

We scan from both directions.

Prefix
2
2 × 3 = 6
6 × -2 = -12
-12 × 4 = -48
Suffix
4
4 × -2 = -8
-8 × 3 = -24
-24 × 2 = -48

At each step we consider the maximum product encountered.

Important: Zero

Zero is especially important for product problems.

If:

prefixProduct = 0

we reset it:

prefixProduct = 1;

Likewise:

suffixProduct = 1;

Why?

Because zero effectively breaks the current product sequence.

---------------------------------------------------------------------------------------------------------------------------------------------------------
7. Prefix + Suffix Arrays

Sometimes we explicitly create two arrays.

Example:

nums = [2, 4, 3, 5]

Prefix:

prefix = [2, 6, 9, 14]

Suffix:

suffix = [14, 12, 8, 5]

Then we can answer questions about the left and right sides in O(1) per position.

The trade-off is:

Time  → O(n)
Space → O(n)

---------------------------------------------------------------------------------------------------------------------------------------------------------
8. Prefix + Suffix Without Extra Arrays

Sometimes we only need the running values.

For example:

int prefix = 0;
int suffix = 0;

for (int i = 0; i < nums.Length; i++)
{
    prefix += nums[i];
    suffix += nums[nums.Length - 1 - i];
}

Here:

prefix → left → right
suffix → right → left

Space:

O(1)

---------------------------------------------------------------------------------------------------------------------------------------------------------
9. Common Problems Using Prefix + Suffix

This is an important interview pattern.

Problem types

1. Product of Array Except Self

[1, 2, 3, 4]

Output:
[24, 12, 8, 6]

Uses prefix and suffix products.

2. Trapping Rain Water

Uses:

leftMax
rightMax

Conceptually:

Prefix maximum + Suffix maximum

3. Maximum Product Subarray

Can be solved using:

Prefix Product + Suffix Product

4. Product/Sum Except Current Element

Often uses:

left information + right information

5. Equilibrium / Pivot Index

Can use:

left sum
+
right sum

6. Array partition problems

When the problem asks about:

everything before i
+
everything after i

Prefix/Suffix should come to mind.

---------------------------------------------------------------------------------------------------------------------------------------------------------
10. When Should You Think of Prefix + Suffix?

Look for phrases like:

"all elements before..."

"all elements after..."

"left side and right side..."

"except the current element..."

"maximum/minimum on the left and right..."

"product of elements except itself..."

"sum of elements on both sides..."

When you see these patterns, ask yourself:

Can I precompute information from the left
and
precompute information from the right?

If yes, Prefix + Suffix may be the right technique.

---------------------------------------------------------------------------------------------------------------------------------------------------------
11. Brute Force vs Prefix + Suffix

Suppose you need information about everything to the left and right of every element.

Brute Force

For every index:

calculate left
calculate right

This can become:

O(n²)
Prefix + Suffix

First calculate:

left information → O(n)
right information → O(n)

Then process the result:

O(n)

Total:

O(n)

So the technique often converts:

O(n²) → O(n)

---------------------------------------------------------------------------------------------------------------------------------------------------------

12. Most Important Concept

Don't memorize:

"Prefix + Suffix = two loops."

That's not the real technique.

Instead remember:

Prefix stores reusable information from the left. Suffix stores reusable information from the right.
Combining them lets us answer left-and-right dependent questions efficiently.

---------------------------------------------------------------------------------------------------------------------------------------------------------
13. Prefix Sum vs Prefix + Suffix

You asked this earlier, so let's make the distinction very clear.
| Technique           | Direction    | Typical information      |
| ------------------- | ------------ | ------------------------ |
| **Prefix Sum**      | Left → Right | Cumulative sum           |
| **Suffix Sum**      | Right → Left | Cumulative sum           |
| **Prefix Product**  | Left → Right | Cumulative product       |
| **Suffix Product**  | Right → Left | Cumulative product       |
| **Prefix + Suffix** | Both         | Left + right information |

So Prefix Sum is a specific technique, while Prefix + Suffix is a broader pattern.

---------------------------------------------------------------------------------------------------------------------------------------------------------
14. Your DSA Technique Notes

You can remember the technique like this:

PREFIX + SUFFIX
────────────────────────────────

Prefix:
    Process from LEFT → RIGHT

Suffix:
    Process from RIGHT → LEFT

Purpose:
    Reuse information from both sides.

Common operations:
    Sum
    Product
    Maximum
    Minimum
    Count

Common complexity:
    Time  → O(n)
    Space → O(n)
    
Can sometimes be optimized to:
    Space → O(1)

Look for:
    Left + Right
    Before + After
    Except current element
    Left maximum + Right maximum
⭐ Key interview thought

When you see:

"For every index i, consider elements
before i and after i..."

your brain should immediately consider:

PREFIX + SUFFIX

This is now a separate DSA technique from Prefix Sum in your preparation.

---------------------------------------------------------------------------------------------------------------------------------------------------------

