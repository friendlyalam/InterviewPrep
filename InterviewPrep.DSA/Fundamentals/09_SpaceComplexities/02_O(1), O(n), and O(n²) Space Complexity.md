1. Definition

The Space Complexity of an algorithm depends on how much extra memory it allocates while solving a problem.

The most common space complexities are:

O(1) – Constant Space
O(n) – Linear Space
O(n²) – Quadratic Space
2. Simple Definition

Think of memory like storage boxes.

If you always use the same number of boxes, it is O(1).
If the number of boxes grows with the input size, it is O(n).
If you need a table (rows × columns), it is usually O(n²).
3. O(1) – Constant Space
Definition

An algorithm uses O(1) Space if the amount of extra memory remains constant, regardless of the input size.

Simple Definition

No matter how large the input becomes, the algorithm always uses the same amount of extra memory.

Example 1
int age = 35;

Memory

+---------+
| age=35  |
+---------+

Only one variable.

Extra Space

O(1)
Example 2
int a = 10;
int b = 20;
int sum = a + b;

Memory

+------+
| a    |
+------+

+------+
| b    |
+------+

+------+
| sum  |
+------+

Three variables.

Still constant.

Space

O(1)
Real-Life Example 1

You always carry:

One wallet
One phone
One key

Even if you travel for:

1 day
10 days
100 days

The number of items remains the same.

O(1)

Technical Example
int max = arr[0];

for(int i = 1; i < arr.Length; i++)
{
    if(arr[i] > max)
        max = arr[i];
}

Extra variables

max

i

Only two variables.

Even if the array has one million elements,

the extra memory remains constant.

Space

O(1)
4. O(n) – Linear Space
Definition

An algorithm uses O(n) Space if the amount of extra memory grows proportionally with the input size.

Simple Definition

As the input becomes larger,

the algorithm creates more memory.

Example 1
int[] temp = new int[n];

Suppose

n = 5

Memory

+----+
|    |
+----+
|    |
+----+
|    |
+----+
|    |
+----+
|    |
+----+

Five integers.

Suppose

n = 1000

The array now contains:

1000 integers

Memory increases with n.

Space

O(n)
Example 2
List<int> numbers = new List<int>();

for(int i = 0; i < n; i++)
{
    numbers.Add(i);
}

List size grows with the input.

Space

O(n)
Real-Life Example 1

Imagine a classroom.

Every new student receives one chair.

More students

↓

More chairs

The number of chairs grows with the number of students.

O(n)

Real-Life Example 2

Suppose a company creates one employee ID card for every employee.

10 employees

↓

10 cards

100 employees

↓

100 cards

Memory grows linearly.

O(n)

Technical Example

Copy Array

int[] copy = new int[arr.Length];

for(int i = 0; i < arr.Length; i++)
{
    copy[i] = arr[i];
}

Input array already exists.

The algorithm creates another array of size n.

Extra Space

O(n)
5. O(n²) – Quadratic Space
Definition

An algorithm uses O(n²) Space when it allocates memory in the form of a square matrix or another structure whose size is proportional to n × n.

Simple Definition

Memory grows like:

Rows × Columns
Example 1
int[,] matrix = new int[n, n];

Suppose

n = 3

Memory

+---+---+---+
|   |   |   |
+---+---+---+
|   |   |   |
+---+---+---+
|   |   |   |
+---+---+---+

Total cells

3 × 3 = 9

Suppose

n = 100

Memory

100 × 100

=

10000 cells

Space

O(n²)
Real-Life Example

Suppose a school creates a chart showing:

Every student can interact with every other student.

If there are:

n students

The chart contains:

n × n

entries.

Memory

O(n²)
Technical Example

Adjacency Matrix (Graph)

bool[,] graph = new bool[n, n];

Stores relationships between every pair of vertices.

Space

O(n²)
6. Visual Comparison
O(1)
Input = 10

Extra Memory

□
Input = 1000

Extra Memory

□

No change.

O(n)
Input = 5

□□□□□
Input = 10

□□□□□□□□□□

Memory doubles.

O(n²)
Input = 3

□□□
□□□
□□□
Input = 5

□□□□□
□□□□□
□□□□□
□□□□□
□□□□□

Rows and columns both increase.

7. Time vs Space Examples
Example 1
int sum = 0;

for(int i = 0; i < arr.Length; i++)
{
    sum += arr[i];
}

Time

O(n)

Space

O(1)
Example 2
int[] copy = new int[n];

for(int i = 0; i < n; i++)
{
    copy[i] = arr[i];
}

Time

O(n)

Space

O(n)
Example 3
int[,] matrix = new int[n, n];

Time (allocation)

O(1)

Space

O(n²)

Important Note: Here we're only analyzing the memory allocation itself. If you later initialize every element using nested loops, the time complexity becomes O(n²) as well.

8. Interview Notes
Question 1

What is O(1) Space?

Answer:

The algorithm uses a fixed amount of extra memory regardless of input size.

Question 2

Why is an array of size n O(n) Space?

Answer:

Because the amount of allocated memory grows directly with n.

Question 3

Why is a 2D array O(n²) Space?

Answer:

Because it allocates n × n elements, which grows quadratically with n.

Question 4

Can two algorithms have the same Time Complexity but different Space Complexity?

Answer:

Yes.

Example:

Algorithm A: O(n) Time, O(1) Space (sum an array).
Algorithm B: O(n) Time, O(n) Space (copy an array).

Both take the same time, but one uses much more memory.

9. Common Mistakes

❌ Thinking every loop increases Space Complexity.

Correct:

A loop repeats operations, but it doesn't automatically allocate more memory.

❌ Counting the input array as extra memory.

Correct:

Usually, we count only auxiliary (extra) memory.

❌ Thinking a 2D array is O(2n).

Correct:

An n × n array uses O(n²) space.

10. Summary
O(1)

✓ Fixed number of variables.

✓ Memory never grows.

O(n)

✓ Array

✓ List

✓ Queue

✓ Stack (that grows with input)

O(n²)

✓ Matrix

✓ Adjacency Matrix

✓ Two-dimensional tables

11. Revision Notes
Space Complexity

↓

O(1)

↓

Variables

-----------------

O(n)

↓

Array

List

Queue

Stack

-----------------

O(n²)

↓

Matrix

2D Array

Adjacency Matrix