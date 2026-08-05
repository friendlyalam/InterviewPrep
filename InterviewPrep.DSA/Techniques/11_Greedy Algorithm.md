
1. Definition

A Greedy Algorithm is a technique that makes the best possible choice at the current step (local optimum),
hoping that these choices will lead to the best overall solution (global optimum).

Unlike Backtracking or Dynamic Programming, Greedy never goes back to reconsider previous decisions.

--------------------------------------------------------------------------------------------------------
2. Why Do We Need Greedy?

Some optimization problems ask us to:

Maximize profit
Minimize cost
Finish tasks quickly
Schedule activities
Select intervals
Reach the destination with minimum jumps

Trying every possibility would take exponential time.

Instead, Greedy makes one smart decision at each step.

--------------------------------------------------------------------------------------------------------
3. Simple Definition

Choose the best option available right now and continue without changing previous decisions.

--------------------------------------------------------------------------------------------------------
4. Real-Life Examples
Example 1 – Cashier Giving Change

Suppose the bill is:

₹270

Available notes:

₹200

₹100

₹50

₹20

₹10

A cashier usually gives:

₹200

↓

₹50

↓

₹20

Instead of trying every possible combination.

This is a Greedy approach.

Note: This works correctly for many common currency systems (such as modern Indian currency), but not for every possible set of coin or note denominations.

Example 2 – Choosing the Shortest Queue

You enter a supermarket.

There are four billing counters.

You choose the shortest queue.

You're making the best decision right now.

Example 3 – Job Scheduling

You have several meetings.

You always choose the meeting that finishes earliest so you can attend more meetings later.

This is the classic Activity Selection problem.

--------------------------------------------------------------------------------------------------------
5. Technical Example

Activities

A : 1 - 3

B : 2 - 5

C : 4 - 6

D : 6 - 8

Greedy Strategy

Choose the activity that finishes earliest.

Possible selection:

A

↓

C

↓

D

Maximum number of non-overlapping activities.

--------------------------------------------------------------------------------------------------------
6. Core Idea

Greedy follows one simple pattern.

Choose Best Local Option

↓

Accept It

↓

Never Change It

↓

Move Forward

Unlike Backtracking,

there is no Undo.

--------------------------------------------------------------------------------------------------------
7. Visualization

Imagine climbing a hill.

At every step,

choose the steepest upward path.

Start

↓

Best Choice

↓

Best Choice

↓

Best Choice

↓

Goal

You never return to previous steps.

--------------------------------------------------------------------------------------------------------
8. Generic C# Template
// Sort if required

foreach (var item in collection)
{
    if (CurrentChoiceIsBest(item))
    {
        Take(item);
    }
}

Most Greedy algorithms begin with:

Sorting
Choosing the best candidate
Moving forward

--------------------------------------------------------------------------------------------------------
9. Which Data Structures Commonly Use Greedy?

| Data Structure        | Can Use? |
| --------------------- | -------- |
| Array                 | ✅        |
| Interval List         | ✅        |
| Priority Queue (Heap) | ✅        |
| Graph                 | ✅        |
| String                | ✅        |

--------------------------------------------------------------------------------------------------------
10. Recognition Clues

When you see words like:

Maximum activities
Minimum cost
Maximum profit
Earliest finish
Shortest interval
Minimum jumps
Assign
Schedule
Largest
Smallest

Think:

Could a Greedy choice work here?

Then verify whether earlier choices never need to be changed.

--------------------------------------------------------------------------------------------------------
11. Characteristics of Greedy Problems

A problem is a good candidate for Greedy when it has:

1. Greedy Choice Property

A locally optimal choice can lead to a globally optimal solution.

2. Optimal Substructure

After making a correct choice,

the remaining problem can be solved independently.

--------------------------------------------------------------------------------------------------------
12. Famous Greedy Problems

| Problem                                    | Uses Greedy? |
| ------------------------------------------ | ------------ |
| Activity Selection                         | ✅            |
| Assign Cookies                             | ✅            |
| Jump Game                                  | ✅            |
| Minimum Platforms (after sorting)          | ✅            |
| Huffman Coding                             | ✅            |
| Fractional Knapsack                        | ✅            |
| Dijkstra's Algorithm (with Priority Queue) | ✅            |


--------------------------------------------------------------------------------------------------------
13. Greedy vs Backtracking

 | Greedy                                               | Backtracking               |
| ---------------------------------------------------- | -------------------------- |
| Chooses one best option                              | Explores all options       |
| Never goes back                                      | Goes back (undoes choices) |
| Usually much faster                                  | Often exponential          |
| May fail if the local optimum isn't globally optimal | Finds all valid solutions  |

--------------------------------------------------------------------------------------------------------
14. Greedy vs Dynamic Programming

This is a very common interview question.

| Greedy                                   | Dynamic Programming                                               |
| ---------------------------------------- | ----------------------------------------------------------------- |
| Makes one decision and never revisits it | Stores results of subproblems and may consider many possibilities |
| Usually faster                           | Often slower but more powerful                                    |
| Doesn't always give the optimal answer   | Gives the optimal answer when DP conditions are satisfied         |
| Simpler implementation                   | More complex implementation                                       |

--------------------------------------------------------------------------------------------------------
15. When Should We NOT Use Greedy?

Avoid Greedy when:

A local optimum doesn't guarantee a global optimum.

Example:

0/1 Knapsack

Greedy fails.

Dynamic Programming works.

Decisions may need to be changed later.

If you need to reconsider earlier choices,

Backtracking or DP is usually more appropriate.

The problem requires exploring all possibilities.

Use:

Backtracking

--------------------------------------------------------------------------------------------------------
16. Time Complexity

Depends on the problem.

Most Greedy algorithms:

Sorting

↓

O(n log n)

+

Linear Scan

↓

O(n)

Overall:

O(n log n)

Some Greedy algorithms don't require sorting and run in:

O(n)

--------------------------------------------------------------------------------------------------------
17. Space Complexity

Usually:

O(1)

or

O(n)

depending on additional data structures used.

--------------------------------------------------------------------------------------------------------
18. Advantages

✅ Fast.

✅ Easy to implement.

✅ Often optimal for the right class of problems.

✅ Frequently asked in interviews.

--------------------------------------------------------------------------------------------------------
19. Disadvantages

❌ Doesn't always produce the optimal answer.

❌ Recognition can be difficult.

❌ Requires proof or reasoning that the Greedy choice is correct.

--------------------------------------------------------------------------------------------------------
20. Frequently Asked Interview Questions
Q1. What is a Greedy Algorithm?

Answer:

A technique that repeatedly chooses the best available local option without reconsidering previous choices.

Q2. Does Greedy always produce the optimal answer?

Answer:

No.

It works only for problems that satisfy the Greedy Choice Property and Optimal Substructure.

Q3. What is the Greedy Choice Property?

Answer:

It means making the best local decision at each step leads to the best overall solution.

Q4. Which famous problems use Greedy?

Answer:

Activity Selection
Fractional Knapsack
Huffman Coding
Assign Cookies
Jump Game
Dijkstra's Algorithm
Q5. What is the biggest difference between Greedy and Backtracking?

Answer:

Greedy never revisits earlier decisions.

Backtracking explores a choice, and if it fails, it returns and tries another.

--------------------------------------------------------------------------------------------------------
21. Common Mistakes

❌ Assuming Greedy always works.

❌ Forgetting to sort when required.

❌ Confusing Greedy with Dynamic Programming.

❌ Choosing a local optimum without verifying correctness.

--------------------------------------------------------------------------------------------------------
22. Summary
Need optimization?

        │
        ▼

Can a local best choice always lead
to the global best solution?

        │
      Yes
        │
        ▼

Use Greedy

↓

Choose Best Option

↓

Never Reconsider

↓

Continue

↓

Fast Solution

--------------------------------------------------------------------------------------------------------
23. Technique Cheat Sheet (Updated)

| Technique        | Recognition Words                                      | Common Data Structures     |     Typical Complexity |
| ---------------- | ------------------------------------------------------ | -------------------------- | ---------------------: |
| Brute Force      | Try all                                                | All                        |                 Varies |
| Linear Scan      | Find, count, maximum                                   | Array, String              |                   O(n) |
| Two Pointers     | Reverse, pair, palindrome                              | Array, String, Linked List |                   O(n) |
| Sliding Window   | Contiguous, substring                                  | Array, String              |                   O(n) |
| Prefix Sum       | Range sum, cumulative                                  | Array, Matrix              | Build O(n), Query O(1) |
| Hashing          | Duplicate, frequency, lookup                           | Array, String              |           Average O(1) |
| Binary Search    | Sorted, lower bound, answer space                      | Array, Matrix              |               O(log n) |
| Recursion        | Smaller problem                                        | Tree, Graph                |                Depends |
| Backtracking     | All possibilities                                      | Array, String, Matrix      |    Usually Exponential |
| Divide & Conquer | Split, merge, partition                                | Array, Matrix              |       Often O(n log n) |
| **Greedy**       | Maximum, minimum, earliest, schedule, interval, profit | Array, Graph, Heap         |       Often O(n log n) |
