1. Definition

An Array is a data structure that stores multiple values of the same type in a collection of elements, 
where each element can be accessed using an index.

An Array is a linear data structure that stores elements of the same data type in contiguous(continuous) memory locations.


In C#, arrays have a fixed length after creation.

Example:

int[] numbers = { 10, 20, 30, 40, 50 };

Conceptually:

Index:     0    1    2    3    4
           ↓    ↓    ↓    ↓    ↓
Value:    10   20   30   40   50

------------------------------------------------------------------------------------------------------
2. Simple Definition

An array is a fixed-size collection of elements stored in an ordered sequence and accessed using indexes.

The most important thing to remember:

Array = Elements + Index

-------------------------------------------------------------------------------------------------------
3. Real-Life Example

1.Imagine a row of lockers.

Locker 0
Locker 1
Locker 2
Locker 3
Locker 4

Each locker has a position.

If you know the locker number, you can directly access it.

Arrays work similarly.

Index 0 → First element
Index 1 → Second element


2. 

Imagine a train.

+------+ +------+ +------+ +------+
| C1   | | C2   | | C3   | | C4   |
+------+ +------+ +------+ +------+

Each coach is connected in order.

Similarly, an array stores elements one after another in memory.


-----------------------------------------------------------------------------------
4. Technical Example
int[] marks = { 80, 75, 90, 85, 95 };

Memory can conceptually be represented as:

Index       Value

  0           80
  1           75
  2           90
  3           85
  4           95

To access 90:

int value = marks[2];

Result:

90

----------------------------------------------------------------------------------------
5. Why Does Array Start From Index 0?

This is very important.

In most programming languages, including C#, the first element has index:

0

So for:

int[] numbers = { 10, 20, 30 };

we have:

Index 0 → 10
Index 1 → 20
Index 2 → 30

The last index is:

Length - 1

Therefore:

numbers.Length

is 3, but the last index is 2.

-----------------------------------------------------------------------------------------
6. Array Syntax in C#
Declaration
int[] numbers;
Creation
numbers = new int[5];

This creates an array capable of storing 5 integers.

Declaration + Creation
int[] numbers = new int[5];
Initialization
int[] numbers = { 10, 20, 30, 40, 50 };

---------------------------------------------------------------------------------------
7. Accessing Elements
int[] numbers = { 10, 20, 30, 40, 50 };

Console.WriteLine(numbers[0]);

Output:

10

Another example:

Console.WriteLine(numbers[3]);

Output:

40

--------------------------------------------------------------------------------------------

8. Updating an Element

Arrays are mutable.

Example:

int[] numbers = { 10, 20, 30 };

numbers[1] = 100;

Now:

10 100 30

----------------------------------------------------------------------------------------------------
9. Traversing an Array

Traversal means visiting every element.

Using for
for (int i = 0; i < numbers.Length; i++)
{
    Console.WriteLine(numbers[i]);
}

This is extremely important for DSA.

You will use this pattern hundreds of times.

Using foreach
foreach (int number in numbers)
{
    Console.WriteLine(number);
}

For DSA problems, however, for is often more useful because we frequently need the index.

-------------------------------------------------------------------------------------------------------
10. Array Operations

The most important operations are:

Access
Search
Traversal
Update
Insertion
Deletion
Sorting

Let's understand them.

-----------------------------------------------------------------------------------------------------
11. Access

Accessing an element by index:

numbers[3]

Time Complexity:

O(1)

Why?

Because the computer can directly calculate where the element is located.

---------------------------------------------------------------------------------------------------------
12. Search

Suppose:

10 20 30 40 50

Find:

40

If the array is unsorted, we may need to check:

10 → 20 → 30 → 40

This is Linear Search.

Time:

O(n)

If the array is sorted, Binary Search may reduce this to:

O(log n)

------------------------------------------------------------------------------------------------------------

13. Insertion

This is where arrays become interesting.

Suppose:

10 20 30 40

We want to insert:

25

between 20 and 30.

We need to shift elements:

10 20 30 40

        ↓

10 20 25 30 40

Elements after the insertion position may need to move.

Therefore insertion can take:

O(n)

--------------------------------------------------------------------------------------------------------------

14. Deletion

Suppose:

10 20 30 40 50

Delete:

30

We need to shift later elements:

10 20 30 40 50

        ↓

10 20 40 50

Time:

O(n)


-----------------------------------------------------------------------------------------------------------------
6. Characteristics of an Array
Linear Data Structure
Stores the same data type
Contiguous memory
Fixed size after creation
Fast random access using an index

----------------------------------------------------------------------------------------

7. Advantages

✅ Direct access using an index → O(1)

✅ Easy traversal

✅ Memory efficient

----------------------------------------------

8. Disadvantages

❌ Fixed size

❌ Insertion in the middle is expensive

❌ Deletion in the middle is expensive

-----------------------------------------------------


15. Array Complexity

| Operation                     |           Complexity |
| ----------------------------- | -------------------: |
| Access by index               |             **O(1)** |
| Update by index               |             **O(1)** |
| Traversal                     |             **O(n)** |
| Linear Search                 |             **O(n)** |
| Binary Search on sorted array |         **O(log n)** |
| Insertion                     |             **O(n)** |
| Deletion                      |             **O(n)** |
| Sorting                       | Depends on algorithm |

Remember this table.

It will become very important during interviews.

--------------------------------------------------------------------------------------------
16. Why Is Array Access O(1)?

This is one of the most important concepts.

Suppose an array starts at some memory address.

Conceptually:

Base Address(address of index 0)
     ↓

1000   1004   1008   1012   1016
  ↓      ↓      ↓      ↓      ↓
 10     20     30     40     50

Suppose each integer occupies 4 bytes.

To access index 3, the computer can calculate:

Address = Base Address + (Index × Element Size)

So:

1000 + (3 × 4)
= 1012

Therefore it can directly access the element.

That's why:

numbers[3]

is:

O(1)

-----------------------------------------------------------------------------------------
17. Array Memory

One important interview concept:

Arrays store elements in a contiguous block of memory in the typical conceptual model used to explain arrays.

For example:

+----+----+----+----+----+
| 10 | 20 | 30 | 40 | 50 |
+----+----+----+----+----+

This contiguous layout is one reason indexed access is efficient.

------------------------------------------------------------------------------------------------

18. Fixed Size

A C# array has a fixed length.

int[] numbers = new int[5];

Its length is:

5

You cannot simply make that same array become length 10.

You would need another array or use a resizable collection such as:

List<int>

----------------------------------------------------------------------------

19. Array vs List

Very important for C# developers.

| Array                     | List<T>                  |
| ------------------------- | ------------------------ |
| Fixed length              | Dynamically resizable    |
| `int[]`                   | `List<int>`              |
| Lower-level structure     | Collection abstraction   |
| Excellent indexed access  | Excellent indexed access |
| Size fixed after creation | Can grow/shrink          |

Example:

int[] numbers = new int[5];

versus:

List<int> numbers = new();

For DSA, we need to understand both, but Array comes first.

--------------------------------------------------------------------------------------

20. One-Dimensional Array

Example:

int[] numbers =
{
    10,
    20,
    30,
    40
};

Visual:

10 → 20 → 30 → 40

-------------------------------------------------------------------------------------------

21. Two-Dimensional Array

A 2D array is useful for grids and matrices.

int[,] matrix =
{
    { 1, 2, 3 },
    { 4, 5, 6 },
    { 7, 8, 9 }
};

Visual:

1 2 3
4 5 6
7 8 9

Access:

matrix[1, 2]

Result:

6

------------------------------------------------------------------------------------------
22. Multidimensional Arrays in DSA

2D arrays are extremely important for:

Matrix problems
Grid problems
Dynamic Programming
BFS
DFS
Path problems
Island problems

Examples:

Number of Islands

Flood Fill

Shortest Path in Grid

Rotten Oranges

----------------------------------------------------------------------------------------------

23. Jagged Array

C# also supports jagged arrays.

int[][] numbers =
{
    new[] { 1, 2 },
    new[] { 3, 4, 5 },
    new[] { 6 }
};

Visual:

Row 0 → 1 2

Row 1 → 3 4 5

Row 2 → 6

Unlike a rectangular 2D array, each row can have a different length.

-----------------------------------------------------------------------------------------------------

24. Which Problems Are Solved Using Arrays?

This is the theory point you specifically asked for earlier.

Array problems commonly involve:

Searching
Words:

Find
Search
Locate
Target
Position
Index


Counting
Words:

Count
Frequency
Occurrences
Number of times


Maximum / Minimum
Words:

Maximum
Minimum
Largest
Smallest
Highest
Lowest


Sorting
Words:

Sort
Ascending
Descending
Ordered


Pair Problems
Words:

Pair
Two numbers
Sum
Target sum
Difference

Think:

Two Pointers / Hashing


Subarray Problems
Words:

Subarray
Contiguous
Continuous
Consecutive

Think:

Sliding Window / Prefix Sum


Range Problems
Words:

Range
Between indices
Range sum
Query

Think:

Prefix Sum


Sorted Array Problems
Words:

Sorted
Rotated sorted
Search in sorted array
First occurrence
Last occurrence

Think:

Binary Search

All Possibilities
Words:

All combinations
All subsets
All permutations
Generate all

Think:

Backtracking

---------------------------------------------------------------------------------
25. Array + Techniques

This is extremely important.

An array by itself doesn't tell us how to solve the problem.

We combine the data structure + technique.

| Problem Pattern              | Technique              |
| ---------------------------- | ---------------------- |
| Find element                 | Linear Search          |
| Sorted search                | Binary Search          |
| Pair sum                     | Hashing / Two Pointers |
| Contiguous subarray          | Sliding Window         |
| Range sum                    | Prefix Sum             |
| Duplicate detection          | Hashing                |
| All combinations             | Backtracking           |
| Maximum/minimum optimization | Greedy / DP            |
| Divide array                 | Divide & Conquer       |
| Matrix traversal             | BFS / DFS              |


------------------------------------------------------------------------------------------------------
26. Common Array Interview Keywords

When you read a problem, watch for these words:

Array

Index

Element

Position

Subarray

Subsequence

Sorted

Ascending

Descending

Contiguous

Range

Target

Pair

Duplicate

Frequency

Maximum

Minimum

Largest

Smallest

Rotate

Reverse

Merge

Partition

These words often give you clues about the required technique.

------------------------------------------------------------------------------------------------------------

27. Array vs Subarray vs Subsequence

Very important.

Array:
1 2 3 4 5


Subarray:

Must be contiguous.

Examples:

2 3 4

or

3 4 5

Subsequence:

Does not need to be contiguous.

Example:

1 3 5

from:

1 2 3 4 5

This distinction appears frequently in product-company interviews.

-----------------------------------------------------------------------------------------

28. Array vs Subarray vs Subsequence

| Concept     | Contiguous? |
| ----------- | ----------- |
| Array       | Depends     |
| Subarray    | ✅ Yes       |
| Subsequence | ❌ No        |


Remember:

Subarray = continuous portion

Subsequence = order maintained, gaps allowed

------------------------------------------------------------------------------------------

29. Common Array Mistakes

❌ Accessing an invalid index.

numbers[10]

when the array has only 5 elements.

❌ Forgetting that indexing starts at 0.

❌ Using <= instead of < in traversal.

Correct:

for (int i = 0; i < numbers.Length; i++)

❌ Confusing subarray with subsequence.

❌ Using nested loops when a better technique exists.

❌ Forgetting edge cases.

-------------------------------------------------------------------------------------------

30. Important Edge Cases

For every array problem, automatically think about:

Empty array
[]
One element
[5]
Two elements
[5, 10]
All elements same
[7, 7, 7, 7]
Negative values
[-5, -2, -10]
Already sorted
[1, 2, 3, 4, 5]
Reverse sorted
[5, 4, 3, 2, 1]

Very large input

Check whether your algorithm is efficient enough.

------------------------------------------------------------------------------------------------------------------------------------------------------
31. Array Problem-Solving Procedure

From now on, every DSA problem we solve will follow this process:

1. Understand the problem

        ↓

2. Identify input/output

        ↓

3. Check constraints

        ↓

4. Identify keywords

        ↓

5. Think of brute force

        ↓

6. Identify the technique

        ↓

7. Improve the solution

        ↓

8. Write C# code

        ↓

9. Dry run

        ↓

10. Test edge cases

        ↓

11. Time Complexity

        ↓

12. Space Complexity

        ↓

13. Interview discussion

This is how I want you to approach problems in real interviews.

------------------------------------------------------------------------------------------------------

32. Most Important Array Techniques

For our Array problems, these techniques will be especially important:

Level 1 — Foundation
Linear Search
Traversal
Basic manipulation
Level 2 — Pattern Recognition
Two Pointers
Sliding Window
Hashing
Prefix Sum
Level 3 — Advanced
Binary Search
Greedy
Recursion
Backtracking
Divide & Conquer
Dynamic Programming

We won't jump randomly between them.

We'll learn why a technique is applicable to a particular problem.

---------------------------------------------------------------------------------------------------------------

33. Array Interview Questions You Should Eventually Master

Our Array problem set will include patterns such as:

Find Maximum

Find Minimum

Reverse Array

Remove Duplicates

Move Zeroes

Rotate Array

Two Sum

Three Sum

Majority Element

Best Time to Buy and Sell Stock

Maximum Subarray

Merge Intervals

Product Except Self

Binary Search

Search in Rotated Sorted Array

Find Missing Number

Find Duplicate Number

Longest Consecutive Sequence

Subarray Sum

Sliding Window Problems

Prefix Sum Problems

Matrix Problems

We will not solve them all today.

First, you need this foundation.

-------------------------------------------------------------------------------------------
34. The Most Important Things to Remember

If you remember only these points today, that's enough:

1.
Array = Indexed collection
2.
First index = 0
3.
Last index = Length - 1
4.
Access = O(1)
5.
Search = O(n)

for an unsorted array using linear search.

6.
Binary Search = O(log n)

when the required ordering/monotonic condition exists.

7.
Insertion/Deletion = O(n)

in the general case when elements must be shifted.

8.
Subarray = contiguous
9.
Subsequence = gaps allowed
10.

Don't immediately code.

First ask:

Which technique can solve this problem efficiently?

That question is one of the biggest differences between a beginner and a strong interview candidate.

----------------------------------------------------------------------

13. Interview Notes

Q1

Why is array access O(1)?

Answer

Because the address is calculated directly using the index.

Q2

Why is insertion in the middle O(n)?

Answer

Because all elements after the insertion point must be shifted one position to the right.

Q3

Can an array store different data types?

Answer

No.

A standard array stores elements of one data type.

Q4

Why does indexing start from 0?

Answer

Because the first element is at an offset of 0 bytes from the base address, making address calculation straightforward.

Q5

Can an array grow automatically?

Answer

A standard array cannot. Dynamic collections such as List<T> can grow by allocating a larger underlying array when needed.

-----------------------------------------------------------------------------------------------------------------------------
35. Final Array Mental Model
   
                         ARRAY
                           │
            ┌──────────────┼──────────────┐
            │              │              │
         Search         Manipulate      Analyze
            │              │              │
       ┌────┴────┐      ┌──┴──┐       ┌───┴────┐
       │         │      │     │       │        │
    Linear    Binary   Reverse Rotate  Sum    Maximum
    Search    Search
       │         │
       └────┬────┘
            │
      Pattern Recognition
            │
    ┌───────┼────────┬──────────┐
    │       │        │          │
Two      Sliding   Prefix    Hashing
Pointers Window     Sum
    │       │        │          │
    └───────┴────────┴──────────┘
                    │
              Advanced Problems
                    │
          ┌─────────┼─────────┐
          │         │         │
        Greedy      DP    Backtracking

