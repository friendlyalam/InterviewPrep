Before We Start

Many beginners think:

What is "log"?

Don't worry.

You do NOT need advanced mathematics.

We'll understand it using real-life examples.

Later, when we study Binary Search, this chapter will become crystal clear.

-----------------------------------------------------------------------------------------------------------------------------------------------------

1. Definition

An algorithm has O(log n) (Logarithmic Time Complexity) if it reduces the problem size by a constant factor (usually half) in every step.

Instead of checking every element, it repeatedly removes a large portion of the remaining data.

-----------------------------------------------------------------------------------------------------------------------------------------------------

2. Simple Definition

O(log n) means:

Don't solve the whole problem.

Keep throwing away half of the remaining work until almost nothing is left.

-----------------------------------------------------------------------------------------------------------------------------------------------------

3. Why Do We Need O(log n)?

Imagine you have a phone book with 1,000 pages.

You want to find:

Mohd Alam

Do you start from page 1?

No.

You open somewhere near the middle.

Then decide:

Go left?
Go right?

Again open the middle of the remaining pages.

Again remove half.

Again remove half.

Eventually you reach the correct page.

You never read all 1,000 pages.

-----------------------------------------------------------------------------------------------------------------------------------------------------

4. Why is it Called Logarithmic?

Let's imagine you always divide the remaining work by 2.

Suppose

1024 books

Step 1

1024

↓

512

Step 2

512

↓

256

Step 3

256

↓

128

Continue...

128

↓

64

↓

32

↓

16

↓

8

↓

4

↓

2

↓

1

Notice:

You reached 1 after only 10 divisions.

You did NOT perform 1024 operations.

You performed only about 10.

This is O(log n).

-----------------------------------------------------------------------------------------------------------------------------------------------------

5. Real-Life Example 1 – Guess the Number

Suppose I ask you:

Guess a number between 1 and 100.

You don't guess:

1

2

3

4

5

That would be O(n).

Instead, you ask:

50?

If I say:

Higher

You immediately remove half the numbers.

Remaining

51–100

Again

Guess

75?

If I say:

Lower

Remaining

51–74

Again remove half.

Every guess cuts the search space dramatically.

This is exactly how Binary Search works.

-----------------------------------------------------------------------------------------------------------------------------------------------------

6. Real-Life Example 2 – Dictionary

Suppose you want the word:

Algorithm

Do you start from page 1?

No.

You open near the middle.

Then choose:

Left half

or

Right half.

Again open the middle.

Again remove half.

Eventually you reach the word.

O(log n).

-----------------------------------------------------------------------------------------------------------------------------------------------------
7. Visual Understanding

Suppose

n = 16

Remaining work

16

↓

8

↓

4

↓

2

↓

1

How many steps?

5

Suppose

n = 32
32

↓

16

↓

8

↓

4

↓

2

↓

1

Steps

6

Suppose

n = 1024
1024

↓

512

↓

256

↓

...

↓

1

Steps

10

Notice something amazing.

The input increased from:

16

↓

1024

But the work increased only from:

5

↓

10

That's why O(log n) is extremely efficient.

-----------------------------------------------------------------------------------------------------------------------------------------------------

8. Technical Example (Concept)

We haven't learned Binary Search yet, so don't worry about every line.

Just focus on the idea.

while(left <= right)
{
    int mid = (left + right) / 2;

    if(arr[mid] == target)
        return mid;

    if(target < arr[mid])
        right = mid - 1;
    else
        left = mid + 1;
}

What happens here?

Every iteration removes half of the remaining search space.

Therefore

O(log n)

-----------------------------------------------------------------------------------------------------------------------------------------------------
9. Comparing O(n) vs O(log n)

Suppose there are

1,000,000 elements
O(n)

Worst case

1,000,000 comparisons
O(log n)

Approximately

20 comparisons

Look at that difference!

| Input Size |      O(n) | O(log n) |
| ---------: | --------: | -------: |
|         10 |        10 |       ~4 |
|        100 |       100 |       ~7 |
|      1,000 |     1,000 |      ~10 |
|  1,000,000 | 1,000,000 |      ~20 |


This is why interviewers love Binary Search.

-----------------------------------------------------------------------------------------------------------------------------------------------------

10. When Do We Usually Get O(log n)?

Usually when we repeatedly divide the problem.

Examples:

✓ Binary Search

✓ Searching in a Balanced Binary Search Tree

✓ Some Heap operations (Insert/Delete)

✓ Divide-and-Conquer algorithms (parts of them)

-----------------------------------------------------------------------------------------------------------------------------------------------------

11. Important Observation

Many beginners think:

"If I divide by 2, it's always O(log n)."

Not always.

The key question is:

Does the remaining problem become half as large each iteration?

If yes,

it is often O(log n).

-----------------------------------------------------------------------------------------------------------------------------------------------------

12. Interview Notes
Question 1

What is O(log n)?

Answer:

O(log n) means the algorithm reduces the problem size by a constant factor (commonly half) during each iteration,
resulting in very slow growth in the number of operations.

Question 2

Why is Binary Search O(log n)?

Answer:

Because after each comparison, it discards half of the remaining search space.

Question 3

Is O(log n) better than O(n)?

Answer:

Yes.

As the input size grows, O(log n) requires far fewer operations than O(n).

-----------------------------------------------------------------------------------------------------------------------------------------------------

13. Common Mistakes

❌ Thinking O(log n) means dividing the answer by 2.

Correct:

It means dividing the remaining problem size by 2 (or another constant factor).

❌ Thinking every algorithm with division is O(log n).

Correct:

Only when each step reduces the remaining problem size by a constant factor.

❌ Thinking Binary Search works on any array.

Correct:

Binary Search requires the array to be sorted.

We'll learn this in detail later.

-----------------------------------------------------------------------------------------------------------------------------------------------------

14. Summary

A logarithmic-time algorithm:

✓ Removes a large part of the remaining work each step.

✓ Commonly halves the search space.

✓ Grows very slowly.

✓ Is written as O(log n).

Examples:

Binary Search
Balanced BST search
Some Heap operations

-----------------------------------------------------------------------------------------------------------------------------------------------------
15. Revision Notes
Start

1024

↓

512

↓

256

↓

128

↓

64

↓

32

↓

16

↓

8

↓

4

↓

2

↓

1

↓

About 10 Steps

↓

O(log n)


