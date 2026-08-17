1. Definition

Time Complexity Rules are guidelines that help us determine how the running time of an algorithm grows by analysing its code.

2. Simple Definition

Instead of memorising time complexities, we use a few simple rules to calculate them.

Think of these rules as a checklist.

Whenever you see a piece of code, you apply the rules one by one.

---------------------------------------------------------------------------

3. Why Do We Need These Rules?

Suppose I give you this code.

int sum = 0;

for(int i = 0; i < n; i++)
{
    sum += arr[i];
}

How will you know whether it is:

O(1)
O(n)
O(n²)
O(log n)

Without rules, you would just guess.

With rules, you can calculate it correctly.

---------------------------------------------------------------------------------

4. Rule 1 – Ignore Constants

This is the most important rule in Big O.

Definition

When calculating Time Complexity, we ignore constant numbers because they do not affect the growth of the algorithm.

Simple Definition

Whether an algorithm performs:

5 operations
10 operations
100 operations

for each element, the growth is still linear.

Example 1
for(int i = 0; i < n; i++)
{
    Console.WriteLine(arr[i]);
}

Operations

Print

↓

n times

Complexity

O(n)


Example 2
for(int i = 0; i < n; i++)
{
    Console.WriteLine(arr[i]);
    Console.WriteLine(arr[i]);
}

Question:

Is it O(2n)?

Technically,

yes, there are about 2 × n print operations.

But in Big O,

we ignore constants.

Therefore,

O(2n)

↓

O(n)


Example 3
for(int i = 0; i < n; i++)
{
    Console.WriteLine(arr[i]);
    Console.WriteLine(i);
    Console.WriteLine("Hello");
}

Operations per iteration

3

Total operations

3n

Big O

O(3n)

↓

O(n)

----------------------------------------------------------------------------------------------------

Why Do We Ignore Constants?

Let's compare.

|     n |     n |    2n |    5n |
| ----: | ----: | ----: | ----: |
|    10 |    10 |    20 |    50 |
|   100 |   100 |   200 |   500 |
| 1,000 | 1,000 | 2,000 | 5,000 |

The values are different.

But notice something important.

The growth pattern is the same.

If n doubles,

all three approximately double.

So they all grow linearly.

That is why:

O(n)

O(2n)

O(5n)

O(100n)

are all written as

O(n)



----------------------------------------------------------------------------------------

Rule 2 – Ignore Smaller Terms

This is another very important interview rule.

Definition

When an expression has multiple terms, we keep only the term that grows the fastest.

Example

Suppose an algorithm performs

n + 5

As n becomes very large,

which part matters more?

The 5?

Or n?

Obviously,

n.

Therefore,

O(n + 5)

↓

O(n)


Another Example
n + 100

Big O

O(n)


Another Example
2n + 10

Step 1

Ignore constant multiplier

O(n + 10)

Step 2

Ignore smaller term

O(n)
Example
n² + n

Which grows faster?

For n = 10

n² = 100

n = 10

For n = 100

n² = 10000

n = 100

For n = 1000

n² = 1000000

n = 1000

Clearly,

n² dominates.

Therefore,

O(n² + n)

↓

O(n²)
Example
n³ + n² + n + 1

Largest term

n³

Big O

O(n³)

------------------------------------------------------------------------------------------------------------

Rule 3 – Consecutive Statements Add

Suppose the code is

Console.WriteLine("Start");

Console.WriteLine("Processing");

Console.WriteLine("Done");

Each statement is O(1).

Total work

1 + 1 + 1

=

3

Big O

O(3)

↓

O(1)
Another Example
for(int i = 0; i < n; i++)
{
    Console.WriteLine(arr[i]);
}

Console.WriteLine("Completed");

First part

O(n)

Second part

O(1)

Total

O(n + 1)

↓

O(n)

----------------------------------------------------------------------------------------

Rule 4 – Consecutive Loops Add

Look carefully.

for(int i = 0; i < n; i++)
{
    Console.WriteLine(arr[i]);
}

for(int i = 0; i < n; i++)
{
    Console.WriteLine(arr[i]);
}

First loop

O(n)

Second loop

O(n)

Total

O(n + n)

↓

O(2n)

↓

O(n)

Notice:

The loops are one after another, not inside each other.

So we add their work.

----------------------------------------------------------------------------------------------------

Visual Understanding
Loop 1

↓

n operations

+

Loop 2

↓

n operations

=

2n

↓

O(n)

-------------------------------------------------------------------------------------------------------
Interview Notes
Question 1

Why is O(2n) written as O(n)?

Answer:

Because Big O ignores constant multipliers and focuses only on the growth rate.

Question 2

Why is O(n + 100) simplified to O(n)?

Answer:

Because the constant term becomes insignificant as n grows.

Question 3

Why is O(n² + n) simplified to O(n²)?

Answer:

Because n² grows much faster than n, so it dominates the overall growth.

-----------------------------------------------------------------------------------------------------------

Common Mistakes

❌ Writing

O(2n)

instead of

O(n)

❌ Writing

O(n + 100)

instead of

O(n)

❌ Adding consecutive loops and thinking the result is O(n²).

Remember:

Loop

↓

Loop

means addition.

Only nested loops lead to multiplication.

We'll study that in the next lesson.

------------------------------------------------------------------------------------------

Summary

Today we learned four rules:

✅ Ignore constant multipliers.

O(2n)

↓

O(n)

✅ Ignore smaller terms.

O(n² + n)

↓

O(n²)

✅ Consecutive statements add.

O(n)

+

O(1)

↓

O(n)

✅ Consecutive loops add.

O(n)

+

O(n)

↓

O(2n)

↓

O(n)

--------------------------------------------------------------------------------------
Revision Notes
Rule 1

Ignore Constants

O(2n)

↓

O(n)

===================

Rule 2

Ignore Smaller Terms

O(n²+n)

↓

O(n²)

===================

Rule 3

Statements Add

O(1)+O(1)

↓

O(1)

===================

Rule 4

Consecutive Loops Add

O(n)+O(n)

↓

O(2n)

↓

O(n)

