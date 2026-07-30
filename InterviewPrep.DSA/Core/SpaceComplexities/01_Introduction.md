1. Definition

Space Complexity is the amount of memory (RAM) an algorithm requires during its execution.

It tells us how much extra memory an algorithm needs to solve a problem.

2. Simple Definition

Space Complexity answers one question:

"How much memory does this algorithm use?"

Just as Time Complexity measures execution time,

Space Complexity measures memory usage.

3. Why Do We Need Space Complexity?

Imagine two programs.

Program A
Completes in 2 seconds
Uses 50 MB RAM
Program B
Completes in 2 seconds
Uses 2 GB RAM

Both programs take the same time.

Which one is better?

Obviously,

Program A,

because it uses much less memory.

This is why memory is also important.


4. Time Complexity vs Space Complexity

| Time Complexity         | Space Complexity      |
| ----------------------- | --------------------- |
| Measures execution time | Measures memory usage |
| CPU performance         | RAM usage             |
| Number of operations    | Amount of memory      |
| Example: O(n)           | Example: O(n)         |


Notice:

Both use Big O notation,

but they measure different things.


5. What Uses Memory?

When a program runs,

memory is used by many things.

For example:

Variables
Arrays
Objects
Lists
Dictionaries
Function Calls (Call Stack)
Recursion
Temporary Data

We'll study each one.

---
6. Memory in a Simple Program
int age = 35;

Memory

+--------+
| age=35 |
+--------+

Only one integer variable exists.

Memory usage is constant.

Space Complexity:

O(1)

----
7. Another Example
int a = 10;
int b = 20;
int c = a + b;

Memory

+------+
| a=10 |
+------+

+------+
| b=20 |
+------+

+------+
| c=30 |
+------+

Three variables.

Regardless of input size,

memory remains fixed.

Therefore

O(1)

--------------------

8. Real-Life Example 1 – School Bag

Imagine your school bag.

If every day you carry:

One notebook
One pen
One pencil

The amount of luggage never changes.

Whether the school has:

100 students
1000 students

Your bag remains the same.

Memory is constant.

O(1)

9. Real-Life Example 2 – Water Bottle

Suppose you always carry

one water bottle.

Even if you visit

10 places,

or

100 places,

you still carry

one bottle.

Memory doesn't grow.

O(1)

10. Technical Example – Sum of an Array
int sum = 0;

for(int i = 0; i < arr.Length; i++)
{
    sum += arr[i];
}

Question:

How many extra variables are created?

sum

i

Only two variables.

Even if the array contains:

10 elements
1000 elements
1 million elements

The algorithm still creates only:

sum

i

Therefore,

Extra Space:

O(1)
11. Important Concept – Input Memory vs Extra Memory

This is one of the most confusing topics for beginners.

Suppose you have:

int[] arr = new int[n];

Question:

Should we count this array?

Usually,

No, because the array is the input.

When interviewers ask for Space Complexity,

they usually mean:

Extra (Auxiliary) Space Complexity

That means:

How much additional memory did your algorithm allocate besides the input?

Example

int FindMax(int[] arr)
{
    int max = arr[0];

    for(int i = 1; i < arr.Length; i++)
    {
        if(arr[i] > max)
            max = arr[i];
    }

    return max;
}

Input Array

arr

Already exists.

Algorithm creates only

max

i

Extra Space

O(1)
12. What is Auxiliary Space?
Definition

Auxiliary Space is the extra memory used by an algorithm excluding the input data.

Simple Definition

Think of the input as something the user already gave you.

We only count the new memory your algorithm creates.

13. Visual Understanding

Suppose

Input

Array

10

20

30

40

50

Already exists.

Algorithm creates

sum

i

Only

two variables.

Extra Space

O(1)
14. Interview Notes
Question 1

What is Space Complexity?

Answer:

Space Complexity measures the amount of memory an algorithm uses during execution.

Question 2

What is Auxiliary Space?

Answer:

Auxiliary Space is the extra memory used by an algorithm, excluding the input.

Question 3

Does Space Complexity include the input array?

Answer:

Usually, no.

In interviews, unless specified otherwise, we analyze auxiliary space, not the memory occupied by the input.

15. Common Mistakes

❌ Thinking Space Complexity means hard disk storage.

Correct:

It refers to RAM (main memory) used while the program is running.

❌ Counting the input array in every problem.

Correct:

Usually, the input is not counted when discussing auxiliary space.

❌ Thinking every loop increases Space Complexity.

Correct:

Loops increase Time Complexity because they repeat work.

A loop by itself does not allocate more memory each iteration (unless you create new data structures inside it).

16. Summary

Space Complexity:

✓ Measures memory usage.

✓ Uses Big O notation.

✓ Usually focuses on auxiliary (extra) memory.

✓ Variables often contribute O(1) space.

✓ Input memory is generally not counted.

17. Revision Notes
Space Complexity

↓

Memory Used

↓

Variables

Arrays

Objects

Lists

Recursion

↓

Extra Memory

↓

Big O

