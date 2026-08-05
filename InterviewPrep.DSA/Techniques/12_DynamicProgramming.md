1. Definition

Dynamic Programming (DP) is a technique used to solve problems by breaking them into smaller 
overlapping subproblems, solving each subproblem only once, storing its result, and reusing it whenever needed.

Instead of solving the same problem repeatedly, DP remembers previous answers.

--------------------------------------------------------------------------------------------------------
2. Why Do We Need Dynamic Programming?

Imagine you're solving a problem.

While solving it, you notice:

Problem(10)

↓

Problem(9)

↓

Problem(8)

Later,

another branch again needs

Problem(8)

Without DP,

you solve

Problem(8)

again.

And again.

And again.

A lot of repeated work.

DP eliminates this repetition.

--------------------------------------------------------------------------------------------------------
3. Simple Definition

Solve once, store the answer, reuse it whenever needed.

This is the entire idea behind Dynamic Programming.

--------------------------------------------------------------------------------------------------------
4. Real-Life Examples
Example 1 – Student Marks

Suppose your teacher asks:

"What was your Math score last semester?"

Without records,

you search old notebooks.

Every time.

With records,

you simply look up the stored mark.

DP works the same way.


Example 2 – Calculator

Suppose you calculate

785 × 642

Five times.

Would you calculate it every time?

No.

You'd write it down once.

Reuse it.

That's DP.

Example 3 – Google Maps

Google Maps calculates a route.

Instead of recalculating the same road repeatedly,

it reuses already computed information.

--------------------------------------------------------------------------------------------------------
5. Technical Example

Consider Fibonacci.

F(5)

↓

F(4) + F(3)

Now,

F(4)

↓

F(3) + F(2)

Notice

F(3)

is calculated multiple times.

Visualization:

                 F(5)

             /         \

          F(4)         F(3)

         /    \       /    \

      F(3)   F(2)  F(2)   F(1)

      /  \

   F(2) F(1)

Repeated work:

F(3)

F(2)

F(1)

DP stores them.

No repeated calculations.

--------------------------------------------------------------------------------------------------------
6. Two Conditions Required for DP

A problem should usually satisfy both of these.

1. Overlapping Subproblems

The same smaller problem appears again and again.

Example

Fibonacci

Need

F(5)

↓

F(4)

↓

F(3)

↓

Again F(3)

Repeated work.

2. Optimal Substructure

The optimal solution of the larger problem can be built from optimal solutions of smaller subproblems.

Example:

Shortest Path.

Knapsack.

Longest Common Subsequence.

--------------------------------------------------------------------------------------------------------
7. DP Approaches

There are two major ways.

A. Memoization (Top-Down)

Uses:

Recursion
Cache (Memory)

Flow

Problem

↓

Recursive Call

↓

Store Answer

↓

Reuse Answer
Generic C# Template
Dictionary<int, int> memo = new();

int Solve(int n)
{
    if (memo.ContainsKey(n))
        return memo[n];

    // Base Case

    int answer = ...;

    memo[n] = answer;

    return answer;
}
B. Tabulation (Bottom-Up)

No recursion.

Start from smallest problem.

Build toward the final answer.

Visualization

Small Problem

↓

Next

↓

Next

↓

Next

↓

Final Answer
Generic C# Template
dp[0] = ...;

for (int i = 1; i <= n; i++)
{
    dp[i] = ...;
}

--------------------------------------------------------------------------------------------------------
8. Memoization vs Tabulation

| Memoization                 | Tabulation                  |
| --------------------------- | --------------------------- |
| Top-Down                    | Bottom-Up                   |
| Uses Recursion              | Uses Loops                  |
| Uses Call Stack             | No Call Stack               |
| Computes only needed states | Usually computes all states |
| Easier to write             | Often faster in practice    |

--------------------------------------------------------------------------------------------------------
9. Which Data Structures Commonly Use DP?

| Data Structure | Can Use?                 |
| -------------- | ------------------------ |
| Array          | ✅                        |
| Matrix         | ✅                        |
| String         | ✅                        |
| Tree           | ✅ (Tree DP)              |
| Graph          | ✅ (Certain DAG problems) |


--------------------------------------------------------------------------------------------------------
10. Recognition Clues

When you see words like:

Maximum
Minimum
Number of ways
Count ways
Longest
Shortest
Best possible
Optimize
Cost
Profit
Path
Sequence

Ask yourself:

Does this problem have overlapping subproblems and optimal substructure?

If yes,

think:

Dynamic Programming

--------------------------------------------------------------------------------------------------------
11. Famous DP Problems

| Problem                        | Uses DP? |
| ------------------------------ | -------- |
| Fibonacci                      | ✅        |
| Climbing Stairs                | ✅        |
| House Robber                   | ✅        |
| Coin Change                    | ✅        |
| 0/1 Knapsack                   | ✅        |
| Longest Common Subsequence     | ✅        |
| Longest Increasing Subsequence | ✅        |
| Edit Distance                  | ✅        |
| Matrix Chain Multiplication    | ✅        |

--------------------------------------------------------------------------------------------------------
12. DP vs Recursion

| Recursion          | Dynamic Programming    |
| ------------------ | ---------------------- |
| Repeats work       | Stores and reuses work |
| May be exponential | Often polynomial       |
| Uses no cache      | Uses cache/table       |
| Simpler            | More optimized         |

Example:

Naive Fibonacci

O(2ⁿ)

DP Fibonacci

O(n)

--------------------------------------------------------------------------------------------------------
13. DP vs Greedy

| Greedy                 | DP                                                  |
| ---------------------- | --------------------------------------------------- |
| Makes one decision     | Considers many possibilities                        |
| Doesn't revisit        | Reuses previous results                             |
| Faster when applicable | More general                                        |
| May fail               | Finds the optimal solution for suitable DP problems |



--------------------------------------------------------------------------------------------------------
14. DP vs Backtracking

| Backtracking           | DP                             |
| ---------------------- | ------------------------------ |
| Explores possibilities | Stores computed results        |
| Often exponential      | Often polynomial               |
| Tries every path       | Eliminates repeated work       |
| Finds valid solutions  | Optimizes repeated computation |

--------------------------------------------------------------------------------------------------------
15. When Should We NOT Use DP?

Avoid DP when:

There are no overlapping subproblems.

Example:

Binary Search.

The problem can be solved greedily.

Example:

Activity Selection.

A simple linear scan is enough.

Example:

Find Maximum.

--------------------------------------------------------------------------------------------------------
16. Time Complexity

Depends on:

Number of States

×

Work Per State

Many DP problems become

O(n)

O(n²)

O(n × m)

instead of exponential time.

--------------------------------------------------------------------------------------------------------
17. Space Complexity

Depends on the DP table.

Usually

O(n)

or

O(n × m)

Many problems can later be optimized to

O(1)

or

O(n)

using Space Optimization.

--------------------------------------------------------------------------------------------------------
18. Advantages

✅ Eliminates repeated work.

✅ Produces optimal solutions for many optimization problems.

✅ Converts exponential algorithms into polynomial ones.

✅ Extremely common in product-company interviews.

--------------------------------------------------------------------------------------------------------
19. Disadvantages

❌ Difficult to recognize initially.

❌ Requires additional memory.

❌ Designing DP states can be challenging.

--------------------------------------------------------------------------------------------------------
20. Frequently Asked Interview Questions
Q1. What is Dynamic Programming?

Answer:

A technique that solves overlapping subproblems once, stores the results, and reuses them.

Q2. What are the two conditions required for DP?

Answer:

Overlapping Subproblems
Optimal Substructure
Q3. What are the two DP approaches?

Answer:

Memoization (Top-Down)
Tabulation (Bottom-Up)
Q4. Why is DP faster than Recursion?

Answer:

Because repeated subproblems are computed only once and then reused.

Q5. Does every recursive problem require DP?

Answer:

No.

DP is useful only when there are overlapping subproblems.

--------------------------------------------------------------------------------------------------------
21. Common Mistakes

❌ Using DP when there are no repeated subproblems.

❌ Confusing Memoization with Tabulation.

❌ Forgetting to define the DP state clearly.

❌ Memorizing solutions instead of understanding the recurrence.

--------------------------------------------------------------------------------------------------------
22. Summary
Need optimization?

        │
        ▼

Do subproblems repeat?

        │
      Yes
        │
        ▼

Store Their Answers

↓

Reuse Them

↓

Avoid Recalculation

↓

Dynamic Programming

--------------------------------------------------------------------------------------------------------
23. Technique Cheat Sheet (Updated)

| Technique               | Recognition Words                               |           Typical Complexity |
| ----------------------- | ----------------------------------------------- | ---------------------------: |
| Brute Force             | Try all                                         |                       Varies |
| Linear Scan             | Find, count, max                                |                         O(n) |
| Two Pointers            | Pair, reverse, palindrome                       |                         O(n) |
| Sliding Window          | Contiguous subarray/substring                   |                         O(n) |
| Prefix Sum              | Range sum                                       |       Build O(n), Query O(1) |
| Hashing                 | Duplicate, frequency                            |          Average O(1) lookup |
| Binary Search           | Sorted, answer space                            |                     O(log n) |
| Recursion               | Smaller problem                                 |                      Depends |
| Backtracking            | All possibilities                               |          Usually Exponential |
| Divide & Conquer        | Split, merge                                    |             Often O(n log n) |
| Greedy                  | Local optimum                                   |             Often O(n log n) |
| **Dynamic Programming** | Maximum, minimum, count ways, longest, shortest | Often O(n), O(n²), or O(n×m) |

