1. Definition

When a function calls itself, the computer does not replace the previous function call.

Instead, it stores every function call in memory until it finishes.

This memory area is called the Call Stack.

2. Simple Definition

Think of the Call Stack like a stack of plates.

When a function is called:

A new plate is placed on the top.

When the function finishes:

The top plate is removed.

Functions always finish in the reverse order in which they were called.

This behavior is called:

LIFO

Last In

First Out

This is exactly how the Stack data structure works (we'll study it later).

3. Why Do We Need the Call Stack?

Suppose you call a function.

Print()

Inside Print(), another function is called.

Show()

Inside Show(), another function is called.

Display()

Question:

How does the computer remember where to return after Display() finishes?

Answer:

It stores the return information in the Call Stack.

4. What Does Each Function Store?

Every function call creates a Stack Frame.

A stack frame contains:

Function parameters
Local variables
Return address
Temporary information needed by the CPU
Visual Representation

Suppose:

Main()
{
    Print();
}

Memory

+-----------+
| Main()    |
+-----------+

Now Main() calls Print().

+-----------+
| Print()   |   ← Top
+-----------+
| Main()    |
+-----------+

Print() calls Show().

+-----------+
| Show()    |   ← Top
+-----------+
| Print()   |
+-----------+
| Main()    |
+-----------+

Notice

Every function stays in memory until the called function finishes.

5. Real-Life Example 1 – Stack of Plates

Imagine a restaurant.

You place plates one by one.

Plate 4

↓

Plate 3

↓

Plate 2

↓

Plate 1

To remove Plate 1 first?

Impossible.

You must remove:

Plate 4

↓

Plate 3

↓

Plate 2

↓

Plate 1

The last plate added is removed first.

Exactly like function calls.

6. Real-Life Example 2 – Books on a Table

Place books:

Math

Science

English

The English book is on top.

You must remove:

English

↓

Science

↓

Math

Again,

Last In

First Out.

7. First Recursive Example
void Print(int n)
{
    if (n == 0)
        return;

    Print(n - 1);
}

Suppose

n = 3

Execution

Print(3)

↓

Print(2)

↓

Print(1)

↓

Print(0)

The function hasn't finished yet.

Every call waits.

Memory

+-----------+
| Print(0)  | ← Top
+-----------+
| Print(1)  |
+-----------+
| Print(2)  |
+-----------+
| Print(3)  |
+-----------+

Now Print(0) returns.

+-----------+
| Print(1)  |
+-----------+
| Print(2)  |
+-----------+
| Print(3)  |
+-----------+

Then

Print(1) returns.

Then

Print(2).

Then

Print(3).

8. How Much Memory is Used?

Suppose

n = 5

Calls

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

Print(0)

There are approximately

6 stack frames

Memory grows with n.

Therefore,

Space Complexity

O(n)
9. Technical Example – Factorial
int Factorial(int n)
{
    if (n == 1)
        return 1;

    return n * Factorial(n - 1);
}

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

Memory

+----------------+
| Factorial(1)   |
+----------------+
| Factorial(2)   |
+----------------+
| Factorial(3)   |
+----------------+
| Factorial(4)   |
+----------------+

Four stack frames.

Space

O(n)
10. Time vs Space in Recursion

Example

Factorial(n)

Time

O(n)

because

n function calls are made.

Space

O(n)

because

n stack frames are stored.

Notice

Time and Space happen to be the same here,

but this is not always true.

11. Why Can Recursion Cause Stack Overflow?

Suppose

Print(1000000)

The computer keeps creating stack frames.

Eventually,

the Call Stack becomes full.

Then the program crashes.

This is called:

StackOverflowException
12. Recursive vs Iterative

Recursive

Print(n - 1);

Uses

Call Stack.

Extra Space

O(n)

Iterative

for(int i = 0; i < n; i++)
{
}

Only one variable.

Extra Space

O(1)

This is why interviewers sometimes ask:

Can you solve this recursively?

Then they ask:

Can you solve it iteratively?

13. Visual Comparison
Iterative
Variables

i

↓

Memory

O(1)
Recursive
Function 5

↓

Function 4

↓

Function 3

↓

Function 2

↓

Function 1

↓

Memory

O(n)
14. Interview Notes
Question 1

What is the Call Stack?

Answer:

The Call Stack is the memory area where the computer stores active function calls during program execution.

Question 2

Why is recursive factorial O(n) Space?

Answer:

Because every recursive call creates a new stack frame, and all frames remain in memory until the recursion starts returning.

Question 3

Why does recursion sometimes cause Stack Overflow?

Answer:

Because too many recursive calls fill the Call Stack, leaving no more space for additional function calls.

Question 4

Which usually uses less memory: recursion or iteration?

Answer:

Iteration, because it generally avoids creating additional stack frames.

15. Common Mistakes

❌ Thinking recursion uses no extra memory.

Correct:

Every recursive call creates a new stack frame.

❌ Thinking function calls disappear immediately.

Correct:

A function remains on the Call Stack until it finishes and returns.

❌ Thinking all recursive algorithms have O(n) Space.

Correct:

Many do, but the actual space depends on the maximum recursion depth.

For example, a recursion depth of log n leads to O(log n) space.

16. Summary

The Call Stack:

✓ Stores active function calls.

✓ Uses the LIFO principle.

✓ Creates one stack frame per function call.

✓ Can lead to O(n) Space in many recursive algorithms.

✓ Can overflow if recursion is too deep.

17. Revision Notes
Function Calls

↓

Call Stack

↓

Stack Frames

↓

LIFO

↓

Recursive Memory

↓

O(n) (Typical)