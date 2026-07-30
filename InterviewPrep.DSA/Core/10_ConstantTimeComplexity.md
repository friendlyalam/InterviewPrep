1. Definition

An algorithm has O(1) (Constant Time Complexity) if the number of operations does not change, regardless of the input size.

Whether the input contains 10 elements, 1,000 elements, or 1 billion elements, the algorithm performs approximately the same amount of work.


2. Simple Definition

O(1) means the algorithm always performs a fixed number of operations.

It does not matter whether the input is small or large.

The work remains almost the same.


3. Why Do We Need O(1)?

Imagine you have a library with:

100 books
10,000 books
10,00,000 books

If someone asks:

"What is the first book?"

You simply go to the first shelf and pick the first book.

You don't check every book.

The work remains the same.

This is the idea behind O(1).


4. Why is it Called "Constant Time"?

The word Constant means Fixed.

For example:

Suppose I ask you:

"What is today's day?"

You answer:

Wednesday

Whether I ask once or 100 times, the work required to answer one question remains the same.

Similarly, an O(1) algorithm performs a fixed amount of work.

----------------------------------------------------------------------------------------------------

5. Real-Life Example 1 – First Book on a Shelf

Imagine a bookshelf.

+---------+
| Book 1  |
+---------+
| Book 2  |
+---------+
| Book 3  |
+---------+
| Book 4  |
+---------+
| Book 5  |
+---------+

Question:

Give me the first book.

You immediately take:

Book 1

You don't inspect Books 2, 3, 4, or 5.

Even if the shelf contains 1,000 books, you still take only the first one.

Work remains constant.

-----------------------------------------------------------------------------------------------------
6. Real-Life Example 2 – First Person in a Queue

Suppose people are standing in a queue.

Rahul

↓

Aman

↓

John

↓

David

Question:

Who is the first person?

Answer:

Rahul

You don't need to count everyone.

Whether there are 4 people or 4,000 people, the first person is obtained immediately.

-----------------------------------------------------------------------------------------------------
7. Real-Life Example 3 – TV Remote

Suppose you press:

Volume +

The TV increases the volume by one level.

Whether you own:

10 movies
100 movies
10,000 movies

Pressing the volume button still performs one action.

This is another example of O(1).

-------------------------------------------------------------------------------------------------------------

8. Technical Example 1 – Accessing an Array Element
int[] numbers = { 10, 20, 30, 40, 50 };

Console.WriteLine(numbers[0]);

Question:

How many operations are performed?

Answer:

1 array access.

The computer directly goes to index 0.

Dry Run

Array

Index

0   1   2   3   4

↓

10 20 30 40 50

Statement:

numbers[0]

The computer immediately accesses:

10

No loop.

No searching.

Only one operation.

----------------------------------------------------------------
9. Technical Example 2 – Last Element
Console.WriteLine(numbers[4]);

Does the computer read:

10

↓

20

↓

30

↓

40

↓

50

No.

It directly accesses index 4.

Still O(1).

----------------------------------------------------------------------------
10. Technical Example 3 – Variable Assignment
int age = 35;

Operations:

Store 35 into age

Only one assignment.

Regardless of n.

Therefore:

O(1)

------------------------------------------------------------------------------

11. Technical Example 4 – Simple Addition
int result = 50 + 20;

Operations:

Addition

↓

Assignment

Fixed work.

Still:

O(1)

---------------------------------------------------------------------------------------------

12. Technical Example 5 – Dictionary Lookup (Average Case)
Dictionary<int, string> employees = new();

employees.Add(101, "Alice");

Console.WriteLine(employees[101]);

The dictionary uses hashing to find the key directly (on average).

Average Time Complexity:

O(1)

Note: We'll study Hash Tables later. For now, remember that dictionary lookup is average-case O(1), but not guaranteed in every situation.

-------------------------------------------------------------------------------------------------

13. Visual Understanding

Suppose the input size changes.

n = 10

Operations

1
n = 100

Operations

1
n = 1,000

Operations

1
n = 1,000,000

Operations

1

Notice:

The input increases dramatically.

The work stays the same.

This is exactly why we write:

O(1)

-----------------------------------------------------------------------------------
14. Why is Array Index Access O(1)?

This is a favourite interview question.

Suppose the array is:

Index

0   1   2   3   4

↓

10 20 30 40 50

If you ask for:

numbers[3]

The computer does not start from index 0 and count to 3.

Arrays are stored in contiguous memory.

The computer calculates the memory address directly using a formula.

Simplified idea:

Address of Element

=

Base Address

+

(Index × Size of One Element)

Example (illustrative numbers):

Base Address = 1000

Size of int = 4 bytes

Index = 3

Address

=

1000 + (3 × 4)

=

1012

The computer jumps directly to address 1012.

That's why array indexing is O(1).

We'll study memory layout in more detail when we learn Arrays.

---------------------------------------------------------------------------------------------

15. Interview Notes
Question 1

What is O(1)?

Answer:

O(1) means the algorithm performs a constant amount of work regardless of the input size.

Question 2

Is O(1) always one operation?

Answer:

No.

It means a constant number of operations.

For example:

int a = 10;
int b = 20;
int c = a + b;

This performs several operations, but the number is fixed.

So it is still O(1).

Question 3

Is accessing an array element O(1)?

Answer:

Yes.

The computer calculates the memory address directly using the index.

Question 4

Is dictionary lookup always O(1)?

Answer:

No.

It is O(1) on average. In rare cases, collisions can make it slower. We'll learn why when we study hashing.

-----------------------------------------------------------------------------------------------------------------------
16. Common Mistakes

❌ Thinking O(1) means exactly one operation.

Correct:

It means a constant number of operations.

❌ Thinking O(1) means one second.

Correct:

It has nothing to do with seconds.

❌ Thinking numbers[999999] is slower than numbers[0].

Correct:

Both are O(1) because array indexing uses direct address calculation.

--------------------------------------------------------------------------------------------------------------------

17. Summary

A constant-time algorithm:

✓ Performs a fixed amount of work.

✓ Does not depend on input size.

✓ Is written as O(1).

Examples:

Accessing an array element.
Reading the first item.
Variable assignment.
Simple arithmetic.
Average dictionary lookup.

-------------------------------------------------------------------------------------------------------------------
18. Revision Notes
Input Size (n)

10
100
1000
1000000

↓

Operations

1
1
1
1

↓

Constant Work

↓

O(1)