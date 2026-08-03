1. What is an Array?
Definition

An Array is a linear data structure that stores elements of the same data type in contiguous(continuous) memory locations.

Simple Definition

An array is a collection of similar items stored one after another in memory.

Example:

Marks = [85, 90, 78, 95, 88]

Instead of creating five variables, we store all marks in one array.


An array is like a row of boxes.

Each box stores one value.

Each box has a number called an index.

Example:

Index : 0   1   2   3   4
Value :10  20  30  40  50

--------------------------------------------------------------------------------
2. Real-Life Example

Imagine a train.

+------+ +------+ +------+ +------+
| C1   | | C2   | | C3   | | C4   |
+------+ +------+ +------+ +------+

Each coach is connected in order.

Similarly, an array stores elements one after another in memory.

----------------------------------------------------------------------
3. Why Do We Use Arrays?

Without an array:

int mark1 = 85;
int mark2 = 90;
int mark3 = 78;
int mark4 = 95;

With an array:

int[] marks = { 85, 90, 78, 95 };
string[] names = { "Alice", "Bob", "Charlie", "David" };

Benefits:

Store many values together
Easy to loop through
Easy to search
Easy to update using an index.

---------------------------------------
4. Memory Representation
Index

 0     1     2     3     4

+----+----+----+----+----+
| 85 | 90 | 78 | 95 | 88 |
+----+----+----+----+----+

Important:

First index = 0
Last index = Length - 1

---------------------------------------------------

5. Why Does an Array Start at Index 0?

Suppose the first element is stored at address 1000, and each integer occupies 4 bytes.

Index	Address
0	1000
1	1004
2	1008
3	1012

The computer calculates the address using:

Address = Base Address + (Index × Size of Element)

Because the first element has an offset of 0 bytes, indexing naturally starts at 0.

------------------------------------------------------------------------------------------

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

9. When Should You Use an Array?

Use an array when:

You know the number of elements.
You need fast access by index.
You process elements sequentially.
Most operations are reading rather than inserting or deleting.

Examples:

Student marks
Monthly sales
Daily temperatures
Employee IDs

---------------------------------------------------

10. When Should You NOT Use an Array?

Avoid arrays when:

The size changes frequently.
There are many insertions and deletions in the middle.
You don't know the number of elements in advance.

In such cases, structures like linked lists or dynamic collections are often better choices.

-----------------------------------------------------------------------------------

11. Recognition Clues (Interview Keywords)

If a problem contains words like:

array
sorted array
integer array
index
position
reverse
rotate
contiguous
subarray
maximum
minimum

👉 Your first thought should be:

"This problem will probably involve an Array, possibly combined with another technique."

--------------------------------------------------------------------------------------

12. What Techniques Are Commonly Used with Arrays?

Arrays are the foundation for many interview techniques.

Technique	Common Keywords
Linear Scan	Find maximum, minimum, search
Two Pointers	Reverse, pair, palindrome, remove duplicates
Sliding Window	Subarray, substring, window of size K
Prefix Sum	Range sum, cumulative sum
Binary Search	Sorted array, search
Hashing	Frequency, duplicates, unique elements
Kadane's Algorithm	Maximum sum subarray

This table is very important because it helps you recognize patterns quickly.

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

-----------------------------------------------------------

14. Summary
Array

↓

Linear Data Structure

↓

Same Data Type

↓

Contiguous Memory

↓

Index Starts at 0

↓

Fast Access O(1)

↓

Fixed Size

--------------------------------------------------------------------
12. Which Algorithms/Techniques Use Arrays?

Arrays are the most commonly used data structure in interviews.
| Technique           | Purpose                                        |
| ------------------- | ---------------------------------------------- |
| Linear Scan         | Search, max, min                               |
| Two Pointers        | Reverse, pairs, merge                          |
| Sliding Window      | Subarray/substring problems                    |
| Prefix Sum          | Range sum queries                              |
| Binary Search       | Sorted arrays                                  |
| Hashing             | Frequency counting                             |
| Sorting             | Arrange data                                   |
| Kadane's Algorithm  | Maximum subarray                               |
| Greedy              | Interval and scheduling problems               |
| Dynamic Programming | Many DP problems use arrays for storing states |


-----------------------------------------
13. Time Complexity Summary
	

	| Operation                       | Complexity |
| ------------------------------- | ---------: |
| Access                          |       O(1) |
| Update                          |       O(1) |
| Traverse                        |       O(n) |
| Search (Unsorted)               |       O(n) |
| Search (Sorted - Binary Search) |   O(log n) |
| Insert at End*                  |       O(1) |
| Insert at Middle                |       O(n) |
| Delete                          |       O(n) |
	

	------------------------------------------------

	Technique Cheat Sheet (Updated)
	

	| Data Structure | Common Problem Words                            | Common Techniques                                                                         |
| -------------- | ----------------------------------------------- | ----------------------------------------------------------------------------------------- |
| Array          | array, index, subarray, reverse, rotate, sorted | Linear Scan, Two Pointers, Sliding Window, Prefix Sum, Binary Search, Hashing, Kadane, DP |

