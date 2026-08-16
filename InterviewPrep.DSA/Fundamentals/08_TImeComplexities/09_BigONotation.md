1. Definition

Big O Notation is a mathematical notation used to describe the upper bound (worst-case growth) of an algorithm as the input size increases.

2. Simple Definition

Big O tells us how quickly the amount of work grows when the input becomes larger.

It focuses on the growth of the algorithm, not the exact number of operations.

------------------------------------------------------------------------------------------------------
3. Why Do We Need Big O?

Suppose two developers solve the same problem.

Developer A

Needs about 10 operations for 10 items.

Developer B

Needs about 100 operations for 10 items.

Which algorithm is better?

Probably Developer A.

But what happens when there are 10 million items?

Big O helps us compare algorithms fairly, regardless of computer speed or programming language.

------------------------------------------------------------------------------------------------------
4. Real-Life Example

Imagine two people searching for a book.

Person A

Looks at every book one by one.

Book 1

↓

Book 2

↓

Book 3

↓

...

↓

Book n

---
Person B

Uses the library catalogue.

Search Catalogue

↓

Go Directly

↓

Find Book

Person B does much less work.

Big O helps us compare these methods.

------------------------------------------------------------------------------

5. What Does "O" Mean?

Many beginners think:

O = Operation

❌ Wrong.

The "O" stands for "Order".

It describes the order of growth of an algorithm.

Example:

O(n)

means

The work grows roughly in proportion to the input size.

----------------------------------------------------------------------------------
6. Understanding Growth

Let's look at this table.

Input (n)	        Algorithm A    	        Algorithm B
10	                10 operations	        100 operations
100	                100 operations	        10,000 operations
1,000	            1,000 operations	    1,000,000 operations

Notice:

Algorithm A grows slowly.

Algorithm B grows very quickly.

As n increases, the difference becomes huge.

Big O describes this growth.

------------------------------------------------------------------------------------
8. Big O Measures Growth, Not Exact Operations

Suppose we have this code.

Console.WriteLine("Hello");

Operations

1

Now suppose we have

Console.WriteLine("Hello");
Console.WriteLine("World");

Operations

2

Now suppose we have

Console.WriteLine("A");
Console.WriteLine("B");
Console.WriteLine("C");
Console.WriteLine("D");
Console.WriteLine("E");

Operations

5

The number of operations increased from 1 to 5.

But does it depend on n?

No.

Whether n = 10 or n = 1,000,000, these statements still execute only 5 times.

Therefore, this is called Constant Time.

Later we'll write this as:

O(1)

---------------------------------------------------------------------------------------------

9. Another Example
for(int i = 0; i < n; i++)
{
    Console.WriteLine(arr[i]);
}

Suppose

n = 5

Loop executes

5 times

Suppose

n = 100

Loop executes

100 times

Suppose

n = 10,000

Loop executes

10,000 times

Here, the work increases with n.

Later, we'll call this

O(n)

-----------------------------------------------------------------------------------------------
10. Important Observation

Look carefully.

Example 1

Console.WriteLine("Hello");

Input becomes

10

↓

100

↓

1000

↓

100000

Operations remain

1

Example 2

for(int i = 0; i < n; i++)

Input becomes

10

↓

100

↓

1000

↓

100000

Operations become

10

↓

100

↓

1000

↓

100000

This difference is exactly what Big O describes.

-----------------------------------------------------------------------------------
11. Common Big O Notations

We will learn all of these one by one.
| Big O      | Meaning           |
| ---------- | ----------------- |
| O(1)       | Constant Time     |
| O(log n)   | Logarithmic Time  |
| O(n)       | Linear Time       |
| O(n log n) | Linearithmic Time |
| O(n²)      | Quadratic Time    |
| O(n³)      | Cubic Time        |
| O(2ⁿ)      | Exponential Time  |
| O(n!)      | Factorial Time    |

---------------------------------------------------------------------------------------
12. Interview Notes
Interview Question

What is Big O Notation?

Answer:

Big O Notation is a mathematical notation used to describe how the running time (or number of operations)
of an algorithm grows as the input size increases. It focuses on the worst-case growth of the algorithm.

Interview Question

Does Big O measure seconds?

Answer:

No.

It measures the growth in the number of operations, not the actual execution time.

Interview Question

Why is Big O important?

Answer:

It allows us to compare algorithms independently of hardware, programming language, and execution speed.

----------------------------------------------------------------------------------------------------------------------------------------------------
13. Common Mistakes

❌ Thinking Big O measures seconds.

❌ Thinking a faster computer changes Big O.

❌ Thinking C++ has a better Big O than C#.

A language may run faster in practice, but the algorithm's Big O remains the same.

❌ Memorising Big O without understanding why the work grows.

Understanding the growth is much more important than memorising names.

----------------------------------------------------------------------------------------------------------------------------
14. Summary

Big O:

✓ Describes the growth of an algorithm.

✓ Does not measure seconds.

✓ Helps compare algorithms fairly.

✓ Focuses on how work increases as n increases.

✓ Is the standard way to analyse algorithms in interviews.

-------------------------------------------------------------------------------------------------------------------------------
