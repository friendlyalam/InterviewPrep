1. Definition

When a program runs, the Operating System gives it some memory (RAM).

This memory is divided into different areas.

The two most important areas are:

Stack Memory
Heap Memory
2. Simple Definition

Think of memory as a large office.

The office has two rooms.

Room 1

Stack

Stores

Local Variables
Function Calls
Parameters

Room 2

Heap

Stores

Objects
Arrays
Lists
Dictionaries
Large Data
3. Why Do We Need Two Memories?

Imagine a company.

Small documents

↓

Keep them on your desk.

Large files

↓

Store them in a storage room.

Why?

Because

Small things should be fast.
Large things need more space.

Exactly the same idea.

4. What is Stack Memory?
Definition

Stack Memory is a fast memory area used for:

Local variables
Function parameters
Function calls
Return addresses
Simple Definition

Stack Memory stores temporary information needed while a function is running.

When the function finishes,

its Stack Memory is automatically removed.

5. Real-Life Example 1

Imagine a waiter carrying plates.

Each new order goes on the top.

When an order finishes,

the top plate is removed first.

This is

LIFO

Last In

First Out

Exactly how Stack Memory works.

6. Real-Life Example 2

Suppose you are solving math problems.

You write temporary calculations on rough paper.

After finishing,

you throw the paper away.

Temporary memory.

Exactly like Stack Memory.

7. Stack Memory Example
void Main()
{
    int age = 35;

    int salary = 50000;
}

Memory

+-----------------+
| salary = 50000  |
+-----------------+
| age = 35        |
+-----------------+

Both variables live on the Stack.

When Main() finishes,

they disappear automatically.

8. What is Heap Memory?
Definition

Heap Memory stores dynamically allocated objects.

Examples

Arrays
Objects
List
Dictionary
StringBuilder
Simple Definition

Heap is a large storage area for objects that may live longer than a single function call.

9. Real-Life Example 1

Imagine a warehouse.

Large furniture

↓

Stored inside the warehouse.

The warehouse is the Heap.

10. Real-Life Example 2

A library stores thousands of books.

The books are too large to keep on your study table.

They remain in the library.

Library = Heap.

11. Heap Example
int[] numbers = new int[5];

Memory

Stack

+-----------+
| numbers ---+
+-----------+ |
              |
              v

Heap

+----+----+----+----+----+
|  0 |  0 |  0 |  0 |  0 |
+----+----+----+----+----+

Notice something important.

The array itself is on the Heap.

The variable numbers only stores a reference (address) on the Stack.

12. Value Types vs Reference Types

This is extremely important in C#.

Value Types

Examples

int

double

char

bool

struct

Stored directly.

Usually on the Stack (for local variables).

Reference Types

Examples

class

array

List

Dictionary

object

Object lives on the Heap.

Reference lives on the Stack.

13. Example
int age = 30;

Memory

Stack

+------+
| 30   |
+------+
Person p = new Person();

Memory

Stack

+-------+
| p ----+------+
+-------+      |
               |
               v

Heap

+------------------+
| Name = null      |
| Age = 0          |
+------------------+
14. Multiple References
Person p1 = new Person();

Person p2 = p1;

Memory

Stack

+--------+
| p1 ----+
+--------+ \
            \
+--------+   \
| p2 ----+----+
+--------+    |
              |
              v

Heap

+----------------+
| Person Object  |
+----------------+

Question

How many objects?

Answer

1

Question

How many references?

Answer

2

This is a very common interview question.

15. Function Calls
void Main()
{
    Print();
}

Memory

+-----------+
| Print()   |
+-----------+
| Main()    |
+-----------+

Every function call creates another Stack Frame.

This connects directly to our previous lesson on the Call Stack.

16. Who Removes Memory?
Stack Memory

Removed automatically when the function finishes.

No programmer intervention is required.

Heap Memory

In C#, unused objects are cleaned up by the Garbage Collector (GC).

When there are no reachable references to an object, the GC can reclaim its memory.

Important: The exact time when the GC runs is not predictable. It decides when to collect memory based on its own heuristics.

17. Stack vs Heap Comparison

| Stack                                     | Heap                             |
| ----------------------------------------- | -------------------------------- |
| Very fast                                 | Slower than Stack                |
| Stores local variables                    | Stores objects                   |
| Stores function calls                     | Stores arrays                    |
| Automatic cleanup when a function returns | Cleaned by the Garbage Collector |
| Usually smaller                           | Usually much larger              |


18. Interview Notes
Question 1

What is Stack Memory?

Answer

Stack Memory stores local variables, function parameters, and function call information. It is automatically managed and released when the function returns.

Question 2

What is Heap Memory?

Answer

Heap Memory stores dynamically allocated objects such as arrays, lists, and class instances. In C#, unused heap objects are reclaimed by the Garbage Collector.

Question 3

Where is an array stored?

Answer

The array object is stored on the Heap.

The local variable that references the array is stored on the Stack.

Question 4

Where is an integer stored?

Answer

A local int variable is a value type and is typically stored directly in the Stack frame.

19. Common Mistakes

❌ Thinking everything is stored on the Stack.

Correct:

Objects and arrays are stored on the Heap.

❌ Thinking the variable and object are the same thing.

Correct:

The variable (reference) and the object are different.

❌ Thinking the Garbage Collector immediately removes an object when it becomes unused.

Correct:

The object becomes eligible for collection when it is no longer reachable, but the GC decides when to reclaim it.

20. Summary
Stack Memory

✓ Local variables

✓ Function calls

✓ Parameters

✓ Automatic cleanup

Heap Memory

✓ Objects

✓ Arrays

✓ Lists

✓ Dictionaries

✓ Managed by the Garbage Collector

C#

✓ Value types are typically stored directly in the Stack frame for local variables.

✓ Reference-type objects are stored on the Heap.

✓ References (local variables) are stored on the Stack.

21. Revision Notes
Program Starts

        │
        ▼
+----------------------+
|      Stack           |
|----------------------|
| Local Variables      |
| Parameters           |
| Function Calls       |
| References           |
+----------------------+
          │
          │ points to
          ▼
+----------------------+
|       Heap           |
|----------------------|
| Objects              |
| Arrays               |
| Lists                |
| Dictionaries         |
+----------------------+