1. Definition

Binary Search is a technique that repeatedly divides the search space into two halves until the required element or answer is found.

Instead of checking every element, Binary Search eliminates half of the remaining possibilities in each step.

------------------------------------------------------------------------------------------------------------------

2. Why Do We Need Binary Search?

Suppose you have:

1 Million Numbers

Need to find:

999875
Linear Scan

Check

1

↓

2

↓

3

↓

...

↓

999875

Worst Case

O(n)

Binary Search

Middle

↓

Discard Half

↓

Middle

↓

Discard Half

↓

Middle

↓

Answer

Worst Case

O(log n)

Huge improvement.

---------------------------------------------------------------------------------

3. Simple Definition

Keep dividing the search space into half until only the answer remains.

4. Real-Life Examples
Example 1 – Dictionary

Suppose you want the meaning of

Programming

Do you start from Page 1?

No.

You open somewhere near the middle.

Then again in the middle.

Eventually reach the correct page.

That is Binary Search.

Example 2 – Guess the Number

Friend says

Guess a number between 1 and 100.

You ask

50?

Friend

Higher.

Now search

51–100

Middle again

75?

Lower.

Search

51–74

You keep halving the range.

--------------------------------------------------------------

5. Technical Example

Sorted Array

Index

0 1 2 3 4 5 6

+-------------------------+
|2 5 8 12 16 23 38|
+-------------------------+

Find

16

Step 1

Middle = 12

16 > 12

Discard left half.

Step 2

16 23 38

Middle = 23

16 < 23

Discard right half.

Step 3

16

Found.

6. Visualization
2 5 8 12 16 23 38

↓

Discard Left

16 23 38

↓

Discard Right

16

↓

Found

-----------------------------------------------------------

7. Generic C# Template
int left = 0;
int right = array.Length - 1;

while (left <= right)
{
    int mid = left + (right - left) / 2;

    if (array[mid] == target)
        return mid;

    if (array[mid] < target)
        left = mid + 1;
    else
        right = mid - 1;
}

return -1;
Why use this formula?
int mid = left + (right - left) / 2;

Instead of

(left + right) / 2

Because in some languages and very large arrays, left + right may overflow. The safer formula avoids that issue.

---------------------------------------------------------------------------------------------------------------------------

8. Where Can Binary Search Be Used?

Many beginners think only:

✅ Sorted Array

Actually:

| Problem Type               | Can Use? |
| -------------------------- | -------- |
| Sorted Array               | ✅        |
| Sorted List                | ✅        |
| Search Space               | ✅        |
| Answer Space               | ✅        |
| Rotated Sorted Array       | ✅        |
| Matrix (sorted conditions) | ✅        |


-----------------------------------------------------------------------------------------------------------------------

9. Recognition Clues

If you see words like:

Sorted
Ascending
Descending
Search
Minimum possible
Maximum possible
First occurrence
Last occurrence
Lower Bound
Upper Bound
Smallest answer
Largest answer

Think immediately:

Binary Search

----------------------------------------------------------------------------------------------------------------------

10. Types of Binary Search
Type 1 – Classic Binary Search

Find an element.

Example

Find

25
Type 2 – Lower Bound

Find the first position where a value can be placed without breaking the sorted order.

Example:

Array:

[2, 4, 4, 6, 8]

Target:

4

Lower Bound points to the first 4 (index 1).

Type 3 – Upper Bound

Find the first element greater than the target.

Example:

Array:

[2, 4, 4, 6, 8]

Target:

4

Upper Bound points to 6 (index 3).

Type 4 – Binary Search on Answer

This is one of the most important interview patterns.

Example questions:

Minimum speed to finish work
Smallest capacity
Maximum minimum distance
Minimum eating speed

You are not searching an array.

You are searching the answer.

We'll study this in detail later because it is an advanced topic.

-----------------------------------------------------------------------------------

11. When Should We NOT Use Binary Search?

Avoid it when:

Data is unsorted

Example

8 3 15 2 10

Binary Search requires an ordered search space.

Sequential traversal is required

Example

Find maximum element.

Linear Scan is better.

Frequency counting

Use:

HashMap

Hierarchical data

Use:

Tree

Network problems

Use:

Graph

------------------------------------------------------

12. Time Complexity

Each iteration removes half the search space.

| Operation | Complexity |
| --------- | ---------: |
| Search    |   O(log n) |

-------------------------------------------------------

13. Space Complexity

Iterative Binary Search

O(1)

Recursive Binary Search

O(log n)

(due to the recursion stack)

--------------------------------------------------------

14. Advantages

✅ Very fast.

✅ Eliminates half the search space each step.

✅ Excellent for large datasets.

✅ Frequently asked in interviews.

----------------------------------------------------------

15. Disadvantages

❌ Requires an ordered search space.

❌ Harder to recognize in "Binary Search on Answer" problems.

------------------------------------------------------------------

16. Frequently Asked Interview Questions
Q1. Is Binary Search an algorithm or a technique?

Answer:

It is both.

The classic search procedure is a Binary Search algorithm.
The broader idea of halving the search space is also used as a problem-solving technique in many interview problems.
Q2. Can Binary Search work on an unsorted array?

Answer:

No.

The search space must satisfy the required ordering or monotonic property.

Q3. Why is Binary Search O(log n)?

Answer:

Because every step removes approximately half of the remaining search space.

Q4. Why use
left + (right - left) / 2

instead of

(left + right) / 2

Answer:

To avoid integer overflow.

Q5. Is Binary Search always applied to arrays?

Answer:

No.

It can also be applied to sorted lists, matrices with suitable ordering, and even answer spaces where a monotonic condition exists.

--------------------------------------------------------------------------------------------------------------------------------------------

17. Common Mistakes

❌ Applying Binary Search to unsorted data.

❌ Updating left or right incorrectly, causing an infinite loop.

❌ Forgetting the left <= right condition for the classic version.

❌ Confusing Lower Bound and Upper Bound.

❌ Calculating mid unsafely.

❌ Easy to introduce off-by-one errors if loop conditions are incorrect.

-------------------------------------------------------------------------------
18. Summary
Need to search?

        │
        ▼

Is the search space ordered or monotonic?

        │
      Yes
        │
        ▼

Choose Binary Search

↓

Find Middle

↓

Discard Half

↓

Repeat

↓

O(log n)

19. Technique Cheat Sheet (Updated)

| Technique      | Recognition Words                                        | Common Data Structures      |     Typical Complexity |
| -------------- | -------------------------------------------------------- | --------------------------- | ---------------------: |
| Brute Force    | Try all                                                  | All                         |                 Varies |
| Linear Scan    | Find, count, maximum                                     | Array, String, List         |                   O(n) |
| Two Pointers   | Reverse, pair, palindrome                                | Array, String, Linked List  |                   O(n) |
| Sliding Window | Contiguous, substring, K                                 | Array, String               |                   O(n) |
| Prefix Sum     | Range sum, cumulative                                    | Array, Matrix               | Build O(n), Query O(1) |
| Hashing        | Duplicate, unique, frequency                             | Array, String, Graph        |           Average O(1) |
| Binary Search  | Sorted, lower bound, upper bound, minimum/maximum answer | Array, Matrix, Search Space |               O(log n) |
