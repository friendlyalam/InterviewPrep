1. Definition

Two Pointers is a technique in which two variables (pointers or indices) move through a data structure (usually an array or string) to solve a problem more efficiently.

Instead of using nested loops, we use two pointers to reduce the time complexity.

--------------------------------

2. Why Do We Need Two Pointers?

Suppose you're given an array and asked:

Reverse an array
Find a pair with a given sum
Remove duplicates from a sorted array
Check if a string is a palindrome
Merge two sorted arrays

A beginner might use two nested loops (O(n²)).

With Two Pointers, many of these problems become O(n).

------------------------------------------------------------------

3. Simple Definition

Use two positions in the data and move them according to the problem until the answer is found.

The two pointers may:

Move towards each other.
Move in the same direction.
Move at different speeds.

-------------------------------
4. Real-Life Examples
Example 1 – Finding Two Friends

Imagine 10 students are standing in a line.

You ask:

"Are Ram and Shyam standing at opposite ends?"

One person starts checking from the left, another from the right.

Both move toward the center.

This is the Opposite Direction Two Pointer technique.

Example 2 – Checking a Palindrome Book Title

Book title:

MADAM

Check:

M == M ✓

A == A ✓

D

Left pointer moves right.

Right pointer moves left.

Meet in the middle.

Palindrome confirmed.

-------------------------------------------

5. Technical Example

Array:

[10, 20, 30, 40, 50]

Pointers:

Index

0    1    2    3    4

+----+----+----+----+----+
|10  |20  |30  |40  |50  |
+----+----+----+----+----+

 L                   R

After one move:

      L           R

Then:

          L   R

Eventually both pointers meet.

-----------------------------------------------------

6. Types of Two Pointer Techniques

There are three major types.

Type 1 – Opposite Direction

Pointers start from both ends.

L ------------->

<------------- R

Used for:

Reverse Array
Palindrome
Two Sum II (Sorted Array)
Container With Most Water
Trapping Rain Water

-------------
Type 2 – Same Direction

Both pointers move left to right.

Usually:

Slow

↓

Fast
S

F

Fast pointer explores.

Slow pointer maintains the required position.

Used for:

Remove Duplicates
Move Zeroes
Partition Array
Remove Element

----------------------------
Type 3 – Fast & Slow Pointer

Different speeds.

Slow

↓

Fast

↓↓

Mostly used in Linked Lists.

Examples:

Detect Cycle
Find Middle Node
Happy Number

We'll study Fast & Slow Pointers separately because it's an important specialization of the two-pointer idea.


-------------------------------------------
7. Generic C# Templates
Opposite Direction
int left = 0;
int right = array.Length - 1;

while (left < right)
{
    // Process

    left++;
    right--;
}
Same Direction
int slow = 0;

for (int fast = 0; fast < array.Length; fast++)
{
    // Process
}

-----------------------------------------------------------
8. Which Data Structures Support Two Pointers?

| Data Structure | Can Use? |
| -------------- | -------- |
| Array          | ✅        |
| String         | ✅        |
| Linked List    | ✅        |
| List           | ✅        |

Usually not used directly on:

Stack ❌
Queue ❌
Tree ❌
Graph ❌

---------------------------
9. Recognition Clues

When you read a problem and see words like:

Opposite Direction
Reverse
Palindrome
Pair
Sorted Array
Two Sum
Container
Closest Pair

Think:

Opposite Direction Two Pointers

---------------------------------

Same Direction
Remove duplicates
Move zeroes
Remove element
Compact array
Rearrange in-place

Think:

Same Direction Two Pointers

--------------------------------------------

10. When Should We NOT Use Two Pointers?

Avoid Two Pointers when:

The problem requires frequency counting.

Use:

HashMap
The problem is about hierarchy.

Use:

Tree
The problem is about networks.

Use:

Graph
The problem asks for repeated range sums.

Use:

Prefix Sum

----------------------------------------
11. Time Complexity

Most Two Pointer solutions:

O(n)

Each pointer moves through the data at most once.

-------------------------------------------------------

12. Space Complexity

Usually:

O(1)

Only a few variables are used.

---------------------------------------------------------

13. Advantages

✅ Faster than nested loops in many cases.

✅ Often reduces O(n²) to O(n).

✅ Easy to implement after recognizing the pattern.

✅ Uses constant extra memory.

14. Disadvantages

❌ Doesn't work for every problem.

❌ Recognition is important.

❌ Some problems require sorted data before applying Two Pointers.

----------------------------------------------------------------------------
15. Frequently Asked Interview Questions
Q1. Is Two Pointers an algorithm?

Answer:

No.

It is a problem-solving technique.

Many algorithms are built using this technique.

Q2. Why is Two Pointers faster than nested loops?

Answer:

Because each pointer usually moves only forward (or toward the center), so each element is processed a limited number of times instead of repeatedly.

Q3. Does Two Pointers always require a sorted array?

Answer:

No.

Some problems (like Two Sum II) require sorting or a sorted input, while others (like Reverse String or Palindrome) do not.

Q4. Can Two Pointers be used on strings?

Answer:

Yes.

Strings are sequences of characters, so we can move pointers over their indices just like arrays.

------------------------------------------

16. Common Mistakes

❌ Moving both pointers when only one should move.

❌ Forgetting to update a pointer, causing an infinite loop.

❌ Using Two Pointers on an unsorted array when the algorithm depends on sorted order.

❌ Confusing Same Direction with Fast & Slow Pointers.

------------------------------------------------------------------------------

17. Summary
Need two positions?

        │
        ▼

Can moving two pointers reduce work?

        │
       Yes
        │
        ▼

Choose the pattern

│
├── Opposite Direction
│      Reverse
│      Palindrome
│      Pair Sum (Sorted)
│
├── Same Direction
│      Remove Duplicates
│      Move Zeroes
│
└── Fast & Slow
       Cycle Detection
       Middle Node

 -------------------------------------------------------
Technique Cheat Sheet (Updated)

| Technique    | Recognition Words                                                 | Common Data Structures     | Typical Complexity |
| ------------ | ----------------------------------------------------------------- | -------------------------- | -----------------: |
| Brute Force  | Try all, straightforward                                          | All                        |             Varies |
| Linear Scan  | Find, count, maximum, minimum                                     | Array, String, List        |               O(n) |
| Two Pointers | Reverse, palindrome, pair, sorted, remove duplicates, move zeroes | Array, String, Linked List |               O(n) |


