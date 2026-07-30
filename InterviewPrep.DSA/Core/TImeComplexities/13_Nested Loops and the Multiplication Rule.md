1. Definition

When one loop is inside another loop, the total number of operations is the product (multiplication) of the number of iterations of each loop.

2. Simple Definition

If one loop runs inside another loop, the inner loop executes every time the outer loop executes.

Therefore,

Total Work = Outer Loop × Inner Loop

3. Why Do We Need This Rule?

Suppose a teacher asks every student to shake hands with every other student.

There are 5 students.

Rahul
Aman
John
David
Rohit

Rahul shakes hands with everyone.

Then Aman shakes hands with everyone.

Then John.

Then David.

Then Rohit.

Notice:

Every student repeats the same work.

This is exactly how nested loops work.

4. Understanding Nested Loops

Example

for(int i = 0; i < n; i++)
{
    for(int j = 0; j < n; j++)
    {
        Console.WriteLine(i + "," + j);
    }
}

Many beginners only see two loops.

Instead, think like this:

Outer loop

↓

Every time outer loop runs,

↓

Entire inner loop runs again.

5. Visual Explanation

Suppose

n = 3

Outer Loop

i = 0

Inner Loop

j = 0

j = 1

j = 2

Total inner executions

3

Outer Loop

i = 1

Inner Loop

j = 0

j = 1

j = 2

Again

3

Outer Loop

i = 2

Inner Loop

j = 0

j = 1

j = 2

Again

3

Total executions

3 + 3 + 3 = 9

or

3 × 3 = 9
6. Dry Run

Code

for(int i = 0; i < 3; i++)
{
    for(int j = 0; j < 3; j++)
    {
        Console.WriteLine(i + "," + j);
    }
}

Execution

i = 0

    j = 0

    j = 1

    j = 2

--------------------

i = 1

    j = 0

    j = 1

    j = 2

--------------------

i = 2

    j = 0

    j = 1

    j = 2

Total

9 executions
7. Formula

Outer Loop

n times

Inner Loop

n times

Total

n × n

Therefore

O(n²)
8. Real-Life Example 1 – Classroom Greetings

There are

5 students

Every student says

"Good Morning"

to every other student.

Student 1

↓

5 greetings

Student 2

↓

5 greetings

Student 3

↓

5 greetings

Student 4

↓

5 greetings

Student 5

↓

5 greetings

Total greetings

5 × 5

=

25

This is O(n²).

9. Real-Life Example 2 – Chess Board

A chess board has

8 rows

8 columns

To visit every square,

you do

Row 1

↓

Column 1 to 8

Row 2

↓

Column 1 to 8

...

Total

8 × 8

=

64 squares

Nested traversal.

10. Technical Example 1 – Print All Pairs
int[] arr = {1,2,3};

for(int i = 0; i < arr.Length; i++)
{
    for(int j = 0; j < arr.Length; j++)
    {
        Console.WriteLine($"{arr[i]}, {arr[j]}");
    }
}

Output

1,1

1,2

1,3

2,1

2,2

2,3

3,1

3,2

3,3

Notice

There are

3 × 3

=

9 pairs

Complexity

O(n²)
11. Technical Example 2 – Compare Every Element
for(int i = 0; i < n; i++)
{
    for(int j = 0; j < n; j++)
    {
        if(arr[i] == arr[j])
        {
            // Compare
        }
    }
}

Outer loop

n

Inner loop

n

Total

n × n

=

n²

Complexity

O(n²)
12. Consecutive Loops vs Nested Loops

This is one of the most important interview questions.

Consecutive Loops
for(int i = 0; i < n; i++)
{
}

for(int j = 0; j < n; j++)
{
}

Work

n

+

n

=

2n

↓

O(n)
Nested Loops
for(int i = 0; i < n; i++)
{
    for(int j = 0; j < n; j++)
    {

    }
}

Work

n

×

n

=

n²

↓

O(n²)
13. Triple Nested Loops
for(int i = 0; i < n; i++)
{
    for(int j = 0; j < n; j++)
    {
        for(int k = 0; k < n; k++)
        {

        }
    }
}

Work

n

×

n

×

n

=

n³

Complexity

O(n³)
14. Important Observation

Suppose

n = 10

Operations

100

Suppose

n = 100

Operations

10000

Suppose

n = 1000

Operations

1000000

Notice

Growth is much faster than O(n).

This is why nested loops become expensive for large inputs.

15. Interview Notes
Question

Why are nested loops usually O(n²)?

Answer:

Because the inner loop executes completely for every iteration of the outer loop.

Total work is

n × n
Question

Why are consecutive loops not O(n²)?

Answer:

Because one loop finishes before the next starts.

The work is added, not multiplied.

Question

Are all nested loops O(n²)?

Answer:

No.

If both loops depend on n, they are often O(n²).

If the inner loop runs a fixed number of times (for example, 5 times), the complexity is still O(n).

Example:

for(int i = 0; i < n; i++)
{
    for(int j = 0; j < 5; j++)
    {
        Console.WriteLine(i + "," + j);
    }
}

Outer loop:

n

Inner loop:

5 (constant)

Total:

n × 5

=

5n

↓

O(n)

This is a common interview trick.

16. Common Mistakes

❌ Thinking every two loops mean O(n²).

Correct:

Only nested loops multiply.

❌ Thinking consecutive loops multiply.

Correct:

They add.

❌ Forgetting that a fixed-size inner loop is a constant.

Correct:

n × 5 = O(n).

17. Summary

Nested loops:

✓ Multiply their iterations.

✓ Usually produce O(n²).

✓ Triple nested loops usually produce O(n³).

Remember:

Consecutive loops → Add
Nested loops → Multiply
18. Revision Notes
Consecutive Loops

n + n

↓

2n

↓

O(n)

=====================

Nested Loops

n × n

↓

n²

↓

O(n²)

=====================

Triple Nested Loops

n × n × n

↓

n³
19. Practice Questions

Find the Time Complexity.

Q1
for(int i = 0; i < n; i++)
{
    Console.WriteLine(i);
}
Q2
for(int i = 0; i < n; i++)
{
    for(int j = 0; j < n; j++)
    {
        Console.WriteLine(i + j);
    }
}
Q3
for(int i = 0; i < n; i++)
{
}

for(int j = 0; j < n; j++)
{
}
Q4
for(int i = 0; i < n; i++)
{
    for(int j = 0; j < 10; j++)
    {
        Console.WriteLine(i);
    }
}
Q5
for(int i = 0; i < n; i++)
{
    for(int j = 0; j < n; j++)
    {
        for(int k = 0; k < n; k++)
        {

        }
    }
}