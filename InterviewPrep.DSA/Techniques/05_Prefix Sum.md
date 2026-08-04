Prefix Sum is a technique where we precompute the cumulative sum of elements from the beginning of an array so that range sum queries can be answered quickly.

Instead of calculating the same sums repeatedly, we store them once and reuse them.

------------------------------

1. Definition

Prefix Sum is a technique where we precompute the cumulative sum of elements from the beginning of an array so that range sum queries can be answered quickly.

Instead of calculating the same sums repeatedly, we store them once and reuse them.

--------------------------------------------------

2. Why Do We Need Prefix Sum?

Suppose an interviewer asks:

Find the sum of elements from index L to R.

Example:

Array

[5, 2, 7, 3, 6, 1, 4]

Queries:

Sum(0,3)

Sum(2,5)

Sum(1,6)

Sum(3,4)

Sum(0,6)
Beginner Approach

Every query:

Loop from L to R

↓

Add numbers

↓

Return sum

If there are 100,000 queries, you'll repeat the same additions thousands of times.

Very inefficient.

Prefix Sum solves this.

----------------------------------------------

3. Simple Definition

Store the running total once so you don't have to add the same numbers again.

-----------------------------------------------------------------------

4. Real-Life Example
Bank Account Balance

Suppose you deposit money every month.

Jan = 1000

Feb = 500

Mar = 800

Apr = 700

Running balance becomes

Jan = 1000

Feb = 1500

Mar = 2300

Apr = 3000

Instead of adding from January every time, you simply use the running balance.

That running balance is exactly the Prefix Sum idea.

------------------------------------------------------------------

Another Example

School Marks

Subject Marks

English = 80

Math = 90

Science = 70

Computer = 95

Running total

80

170

240

335

-----------------------------------------------------------------

5. Technical Example

Original Array

Index

0   1   2   3   4

+---+---+---+---+---+
|2  |4  |6  |3  |5  |
+---+---+---+---+---+

Prefix Sum Array

Index

0   1   2   3   4

+---+---+---+---+---+
|2  |6  |12 |15 |20 |
+---+---+---+---+---+

Explanation

Prefix[0] = 2

Prefix[1] = 2 + 4 = 6

Prefix[2] = 2 + 4 + 6 = 12

Prefix[3] = 15

Prefix[4] = 20

---------------------------------------------------------

6. Visualization
Original

2   4   6   3   5

↓

Running Sum

2

↓

6

↓

12

↓

15

↓

20

----------------------------------------------------------

7. Generic C# Template
Build Prefix Sum
int[] prefix = new int[array.Length];

prefix[0] = array[0];

for (int i = 1; i < array.Length; i++)
{
    prefix[i] = prefix[i - 1] + array[i];
}
Find Sum from L to R

If

L = 0
sum = prefix[R];

Otherwise

sum = prefix[R] - prefix[L - 1];

This formula is one of the most important things to remember.

-----------------------------------------------------------------------------

8. Formula (Most Important)

Suppose

Array

[2,4,6,3,5]

Prefix

[2,6,12,15,20]

Need

Sum(2,4)

↓

6 + 3 + 5

Formula

Sum(L,R)

=

Prefix[R]

-

Prefix[L-1]

Example

20 - 6

=

14

Correct

6 + 3 + 5 = 14

-----------------------------------------------------------------

9. Which Data Structures Support Prefix Sum?

| Data Structure         | Can Use? |
| ---------------------- | -------- |
| Array                  | ✅        |
| Matrix (2D Prefix Sum) | ✅        |
| List                   | ✅        |
| String (special cases) | ⚠️       |


-------------------------------------------------------

Not commonly used for:

Linked List ❌
Tree ❌
Graph ❌

-----------------------------------------------------

10. Recognition Clues

If a problem contains words like:

Range Sum
Sum from L to R
Multiple Queries
Running Total
Cumulative Sum
Prefix
Interval Sum

Think immediately:

Prefix Sum

-------------------------------------------------------

11. When Should We NOT Use Prefix Sum?

Avoid Prefix Sum when:

The array changes frequently.

Example

Update index 5

Update index 8

Update index 20

Every update requires rebuilding the prefix array.

For such problems, use:

Fenwick Tree (Binary Indexed Tree)
Segment Tree
The problem isn't about cumulative values.

For example:

Maximum element
Sorting
Searching

Prefix Sum won't help.

--------------------------------------------------

12. Time Complexity

Building Prefix Sum

O(n)

Answering each range query

O(1)

Without Prefix Sum

Each query

O(n)

This is why Prefix Sum is powerful when there are many queries.

---------------------------------------------------------------------

13. Space Complexity

Extra Prefix Array

O(n)

--------------------------------------------------------------------

14. Advantages

✅ Answers range sum queries in O(1).

✅ Easy to implement.

✅ Eliminates repeated calculations.

✅ Excellent for multiple queries.

---------------------------------------------------------------------

15. Disadvantages

❌ Requires extra memory.

❌ Expensive to maintain if the array changes frequently.

--------------------------------------------------------------------

16. Frequently Asked Interview Questions
Q1. Is Prefix Sum an algorithm?

Answer:

No.

It is a problem-solving technique.

Q2. Why is Prefix Sum faster?

Answer:

Because we compute cumulative sums once and reuse them instead of adding the same elements repeatedly.

Q3. What is the most important Prefix Sum formula?

Answer:

Sum(L,R)

=

Prefix[R]

-

Prefix[L-1]

If L = 0, then the answer is simply:

Prefix[R]
Q4. When should Prefix Sum not be used?

Answer:

When the array is updated frequently, because every update may require recomputing the prefix sums.

------------------------------------------------------------------------------------------------------

17. Common Mistakes

❌ Forgetting the special case when L = 0.

❌ Building the prefix array incorrectly.

❌ Using Prefix Sum for dynamic updates.

❌ Confusing Prefix Sum with Sliding Window.

-----------------------------------------------------------------

18. Prefix Sum vs Sliding Window

This is one of the most commonly asked interview discussions.

| Prefix Sum                        | Sliding Window                          |
| --------------------------------- | --------------------------------------- |
| Used for **range sum queries**    | Used for **contiguous window problems** |
| Supports many independent queries | Maintains one moving window             |
| Requires extra memory             | Usually O(1) extra memory               |
| Precomputes running sums          | Continuously expands/shrinks a window   |

Rule of thumb:

If you see many sum queries, think Prefix Sum.
If you see longest/shortest contiguous subarray or substring, think Sliding Window.

------------------------------------------------------
19. Summary
Need many range sums?

        │
       Yes
        │
        ▼

Build Prefix Sum

↓

O(n)

↓

Answer each query

↓

O(1)

Formula

Sum(L,R)

=

Prefix[R]

-

Prefix[L-1]

-------------------------------------
Technique Cheat Sheet (Updated)

| Technique      | Recognition Words                           | Common Data Structures     |       Typical Complexity |
| -------------- | ------------------------------------------- | -------------------------- | -----------------------: |
| Brute Force    | Try all, straightforward                    | All                        |                   Varies |
| Linear Scan    | Find, count, max, min                       | Array, String, List        |                     O(n) |
| Two Pointers   | Reverse, palindrome, pair, sorted           | Array, String, Linked List |                     O(n) |
| Sliding Window | Contiguous subarray, substring, window      | Array, String              |                     O(n) |
| Prefix Sum     | Range sum, cumulative sum, multiple queries | Array, Matrix              | Build: O(n), Query: O(1) |
