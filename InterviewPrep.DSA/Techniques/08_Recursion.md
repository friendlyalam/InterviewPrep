Recursion is a technique in which a function calls itself to solve a problem by breaking it into smaller versions of the same problem.

The recursion continues until it reaches a Base Case, where it stops.

----------------------------------------------------------------------------------

2. Why Do We Need Recursion?

Some problems are naturally repetitive.

Examples:

Factorial
Fibonacci
Tree Traversal
Folder Navigation
Maze Solving
Generate all subsets
Generate all permutations

Instead of writing many loops, recursion lets the function repeat the same logic on smaller inputs.

---------------------------------------------------------------------------------------------------
3. Simple Definition

A function solves a problem by asking itself to solve a smaller version of the same problem.

--------------------------------------------------------------------------------------------------

4. Real-Life Examples
Example 1 – Russian Dolls

Imagine Russian dolls.

Big Doll

↓

Medium Doll

↓

Small Doll

↓

Tiny Doll

To reach the smallest doll, you keep opening the next smaller doll.

When you reach the smallest one, you stop.

Then you close them one by one.

This is exactly how recursion works.

Example 2 – Climbing Stairs

Suppose you are on the 5th step.

To reach step 5, you first reached step 4.

To reach step 4, you first reached step 3.

Eventually:

Step 5

↓

Step 4

↓

Step 3

↓

Step 2

↓

Step 1

Then the process returns back.

Example 3 – Folder Structure
Documents

│

├── Office

│      ├── Projects

│      └── Reports

│

└── Personal

       ├── Photos

       └── Videos

A program visits every folder.

If another folder exists,

it calls itself again.

This is recursion.

---------------------------------------------------------------------------------------------------

5. Technical Example

Suppose

Print Numbers

5

Recursion works like this:

Print(5)

↓

Print(4)

↓

Print(3)

↓

Print(2)

↓

Print(1)

↓

Stop

↓

Return

↓

Return

↓

Return

Notice

The function goes down until the Base Case,



then comes back.

--------------------------------------------------------------------------------------------------

6. Two Most Important Parts

Every recursive function has two parts.

Part 1 – Base Case

Stopping condition.

Without it,

the function never stops.

Example

if (n == 0)

Stop
Part 2 – Recursive Call

Calling itself.

Example

Factorial(n-1)

---------------------------------------------------------------------------------------------------

7. Visualization

Suppose

Factorial(4)

Calls

Factorial(4)

↓

Factorial(3)

↓

Factorial(2)

↓

Factorial(1)

↓

Factorial(0)

↓

Stop

Then

Return

↓

Return

↓

Return

↓

Return

Think of it as:

Going Down → Coming Back Up

--------------------------------------------------------------------------------------------------

8. Generic C# Template
void RecursiveFunction(int n)
{
    // Base Case
    if (n == 0)
        return;

    // Work

    RecursiveFunction(n - 1);

    // Optional Work After Return
}

This is the standard structure for most recursive functions.


---------------------------------------------------------------------------------------------------

9. Which Data Structures Use Recursion?

| Data Structure | Uses Recursion? |
| -------------- | --------------- |
| Tree           | ✅ Very Common   |
| Graph (DFS)    | ✅               |
| Linked List    | ✅ Sometimes     |
| Array          | ✅ Sometimes     |
| String         | ✅ Sometimes     |


--------------------------------------------------------------------------------------------------

10. Recognition Clues

When you see words like:

Tree
DFS
Permutation
Combination
Subset
Backtracking
Divide
Explore all paths
Recursive definition
Nested structure

Think:

Recursion

---------------------------------------------------------------------------------------------------

12. Time Complexity

There is no single complexity for recursion.

Examples:
| Problem                   | Time Complexity |
| ------------------------- | --------------: |
| Print 1 to N              |            O(n) |
| Factorial                 |            O(n) |
| Binary Search (Recursive) |        O(log n) |
| Fibonacci (Naive)         |           O(2ⁿ) |

The complexity depends on:

Number of recursive calls.
Amount of work in each call.

--------------------------------------------------------------------------------------------------

13. Space Complexity

Recursive calls are stored in the Call Stack.

Example

Factorial(5)

↓

5 Calls

↓

Stack Size = 5

Space Complexity

O(n)


---------------------------------------------------------------------------------------------------

14. Advantages

✅ Code is often shorter and cleaner.

✅ Natural fit for trees and graphs.

✅ Simplifies Divide & Conquer.

✅ Foundation for Backtracking and DFS.

--------------------------------------------------------------------------------------------------

15. Disadvantages

❌ Uses extra stack memory.

❌ Can cause Stack Overflow.

❌ Harder to debug for beginners.

❌ Sometimes slower than iteration.

---------------------------------------------------------------------------------------------------

16. Call Stack (Very Important)

Whenever a recursive function is called,

the computer stores its information in a Call Stack.

Example

Factorial(3)

↓

Push

↓

Factorial(2)

↓

Push

↓

Factorial(1)

↓

Push

↓

Base Case

↓

Pop

↓

Pop

↓

Pop

This is why recursion uses additional memory.

--------------------------------------------------------------------------------------------------

17. Frequently Asked Interview Questions
Q1. What is recursion?

Answer:

A technique where a function calls itself to solve smaller instances of the same problem.

Q2. What is the Base Case?

Answer:

The condition that stops the recursive calls.

Without it, recursion continues indefinitely.

Q3. What happens if there is no Base Case?

Answer:

The recursive calls never stop, eventually causing a Stack Overflow.

Q4. Why does recursion use more memory than a loop?

Answer:

Because each function call is stored on the Call Stack until it returns.

Q5. Is recursion always slower than iteration?

Answer:

Not always.

However, recursion has function call overhead and additional stack usage. 
In some problems (especially trees and divide-and-conquer), recursion leads to much simpler and more maintainable solutions.

---------------------------------------------------------------------------------------------------

18. Common Mistakes

❌ Forgetting the Base Case.

❌ Incorrect Base Case.

❌ Infinite recursion.

❌ Not reducing the problem size.

❌ Ignoring stack overflow risks.

--------------------------------------------------------------------------------------------------
19. Summary
Problem

↓

Can it be broken into a smaller version?

        │
       Yes
        │
        ▼

Write Base Case

↓

Call Function Again

↓

Reach Base Case

↓

Return Step by Step

↓

Finish


---------------------------------------------------------------------------------------------------

20. Technique Cheat Sheet (Updated)

| Technique      | Recognition Words                    | Common Data Structures          |     Typical Complexity |
| -------------- | ------------------------------------ | ------------------------------- | ---------------------: |
| Brute Force    | Try all                              | All                             |                 Varies |
| Linear Scan    | Find, count, maximum                 | Array, String, List             |                   O(n) |
| Two Pointers   | Reverse, pair, palindrome            | Array, String, Linked List      |                   O(n) |
| Sliding Window | Contiguous, substring                | Array, String                   |                   O(n) |
| Prefix Sum     | Range sum, cumulative                | Array, Matrix                   | Build O(n), Query O(1) |
| Hashing        | Duplicate, frequency                 | Array, String                   |           Average O(1) |
| Binary Search  | Sorted, answer space                 | Array, Matrix                   |               O(log n) |
| **Recursion**  | Tree, DFS, subsets, nested structure | Tree, Graph, Linked List, Array | Depends on the problem |


--------------------------------------------------------------------------------------------------