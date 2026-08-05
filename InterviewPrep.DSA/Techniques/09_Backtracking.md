
1. Definition

Backtracking is a technique that systematically explores all possible solutions by making a choice,
exploring it recursively, and then undoing (backtracking) the choice before trying the next possibility.

It is often described as:

Choose → Explore → Undo

--------------------------------------------------------------------------------------------------------

2. Why Do We Need Backtracking?

Some problems cannot be solved by making one greedy decision.

Instead, we must try different possibilities.

Examples:

Generate all permutations
Generate all combinations
Solve Sudoku
N-Queens
Word Search
Rat in a Maze
Restore IP Addresses

A simple loop cannot explore every valid path efficiently.

Backtracking allows us to explore one path at a time.

--------------------------------------------------------------------------------------------------------

3. Simple Definition

Try one option. If it doesn't lead to a solution, go back and try another option.


--------------------------------------------------------------------------------------------------------

4. Real-Life Examples
Example 1 – Maze

Imagine you're inside a maze.

At a junction, you have four choices.

       Start
         │
    ┌────┼────┐
    │    │    │
   Left Right Down

Suppose you choose Left.

Dead end.

You return to the previous junction.

Now choose Right.

This returning is called Backtracking.


Example 2 – Password Lock

You forgot a 3-digit password.

You try:

123

↓

124

↓

125

↓

...

↓

999

Wrong password?

Go back.

Try another.


Example 3 – Seating Arrangement

Suppose four people must sit in four chairs.

Try one arrangement.

Not acceptable?

Undo it.

Try another arrangement.

Continue until every arrangement is checked.

--------------------------------------------------------------------------------------------------------

5. Technical Example

Suppose we need all permutations of

A B C

Backtracking explores like this:

Start

│

├── A

│     ├── B

│     │      └── C

│     │

│     └── C

│            └── B

│

├── B

│     ├── A

│     └── C

│

└── C

      ├── A

      └── B

Notice:

After exploring one branch,

we return and try another.


--------------------------------------------------------------------------------------------------------
. Core Idea

Every Backtracking algorithm follows the same pattern:

Choose

↓

Explore

↓

Undo

↓

Try Next Choice

This is the heart of Backtracking.

--------------------------------------------------------------------------------------------------------
7. Visualization

Imagine a tree.

Start

├── Choice 1

│      ├── Choice

│      └── Choice

│

├── Choice 2

│      ├── Choice

│      └── Choice

│

└── Choice 3

You visit one branch.

Return.

Visit another.

Return.

Until all branches are explored.

--------------------------------------------------------------------------------------------------------
8. Generic C# Template
void Backtrack(...)
{
    // Base Case
    if (/* solution found */)
    {
        // Save answer
        return;
    }

    foreach (var choice in choices)
    {
        // Choose

        Backtrack(...);

        // Undo (Backtrack)
    }
}

Notice the important step:

Choose

↓

Recursive Call

↓

Undo

Without the Undo step,

Backtracking does not work correctly.

--------------------------------------------------------------------------------------------------------
9. Which Data Structures Commonly Use Backtracking?

| Data Structure | Can Use? |
| -------------- | -------- |
| Array          | ✅        |
| String         | ✅        |
| Matrix         | ✅        |
| Graph          | ✅        |
| Tree           | ✅        |

Backtracking is a technique, so it can be applied to many different data structures.

--------------------------------------------------------------------------------------------------------
10. Recognition Clues

When you see words like:

All possible
Every combination
Every permutation
Generate all
Can we place?
Explore every path
Sudoku
N-Queens
Maze
Word Search
Restore

Think immediately:
Backtracking

--------------------------------------------------------------------------------------------------------
11. Difference Between Recursion and Backtracking

| Recursion                | Backtracking                                          |
| ------------------------ | ----------------------------------------------------- |
| Function calls itself    | Uses recursion plus undoing choices                   |
| May follow only one path | Explores many possible paths                          |
| Doesn't always undo work | Always performs an undo step after exploring a choice |
| Example: Factorial       | Example: Sudoku Solver                                |

Important Interview Point:

Every Backtracking solution uses Recursion.

But not every Recursive solution is Backtracking.

--------------------------------------------------------------------------------------------------------
12. When Should We NOT Use Backtracking?

Avoid Backtracking when:

A direct mathematical or greedy solution exists.

Example:

Find maximum element.

A simple loop is enough.

Dynamic Programming is more efficient.

Many optimization problems with overlapping subproblems are better solved with DP.

The search space is too large without pruning.

Exploring every possibility may become impractical.

--------------------------------------------------------------------------------------------------------
13. Time Complexity

There is no fixed complexity.

Typical Backtracking problems have exponential complexity.

Examples:
| Problem      |  Complexity |
| ------------ | ----------: |
| Permutations |       O(n!) |
| Subsets      |       O(2ⁿ) |
| N-Queens     | Exponential |


--------------------------------------------------------------------------------------------------------
4. Space Complexity

Mostly determined by the recursion depth.

Typical:

O(n)

where n is the maximum recursion depth.

--------------------------------------------------------------------------------------------------------
15. Advantages

✅ Finds all valid solutions.

✅ Natural for search problems.

✅ Elegant recursive structure.

✅ Useful for many classic interview problems.

--------------------------------------------------------------------------------------------------------
16. Disadvantages

❌ Can be very slow.

❌ Exponential time in many cases.

❌ Requires careful undo logic.

❌ Easy to introduce bugs if state is not restored correctly.

--------------------------------------------------------------------------------------------------------
17. Frequently Asked Interview Questions
Q1. What is Backtracking?

Answer:

A technique that explores possible solutions by choosing an option, exploring it recursively, and undoing the choice before trying the next option.

Q2. Is Backtracking an algorithm?

Answer:

No.

It is a problem-solving technique.

Q3. Does Backtracking always use Recursion?

Answer:

Almost always in interview settings.

While iterative implementations are possible, recursive solutions are the standard approach.

Q4. Why is the Undo step important?

Answer:

Because after exploring one choice, we must restore the previous state before trying another choice.

Without undoing, later branches may start with incorrect state.

Q5. What kinds of problems use Backtracking?

Answer:

Problems that require exploring all valid possibilities, such as:

Permutations
Combinations
Sudoku
N-Queens
Maze
Word Search

--------------------------------------------------------------------------------------------------------
18. Common Mistakes

❌ Forgetting the Undo step.

❌ Missing the Base Case.

❌ Modifying shared data without restoring it.

❌ Not pruning invalid branches when possible.

--------------------------------------------------------------------------------------------------------
19. Summary
Need all possible solutions?

        │
       Yes
        │
        ▼

Choose

↓

Explore

↓

Undo

↓

Try Next Choice

↓

Repeat Until Complete

--------------------------------------------------------------------------------------------------------
20. Technique Cheat Sheet (Updated)

| Technique        | Recognition Words                                     | Common Data Structures       |     Typical Complexity |
| ---------------- | ----------------------------------------------------- | ---------------------------- | ---------------------: |
| Brute Force      | Try all                                               | All                          |                 Varies |
| Linear Scan      | Find, count, max                                      | Array, String                |                   O(n) |
| Two Pointers     | Reverse, pair                                         | Array, String, Linked List   |                   O(n) |
| Sliding Window   | Contiguous, substring                                 | Array, String                |                   O(n) |
| Prefix Sum       | Range sum                                             | Array, Matrix                | Build O(n), Query O(1) |
| Hashing          | Duplicate, frequency                                  | Array, String                |           Average O(1) |
| Binary Search    | Sorted, answer space                                  | Array, Matrix                |               O(log n) |
| Recursion        | Smaller problem                                       | Tree, Graph                  |                Depends |
| **Backtracking** | All possibilities, permutations, combinations, Sudoku | Array, String, Matrix, Graph |    Usually Exponential |

--------------------------------------------------------------------------------------------------------