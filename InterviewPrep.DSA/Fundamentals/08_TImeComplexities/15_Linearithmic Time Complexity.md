1. Definition

An algorithm has O(n log n) Time Complexity when it performs log n levels (or stages) of work, and at each level it processes n elements.

In simple words:

Total Work

=

n × log n

-----------------------------------------------------------------------------------------------------------------------------------------------------
2. Simple Definition

O(n log n) means:

The problem is divided repeatedly (log n).
At every division level, almost all elements are processed (n).

Therefore,

O(n log n)

-----------------------------------------------------------------------------------------------------------------------------------------------------
3. Why Do We Need O(n log n)?

Suppose you have:

1024 files

Instead of sorting them one by one,

you repeatedly divide them into smaller groups.

After dividing,

you process every file while combining them back.

So,

you are:

Dividing (log n)
Processing all elements (n)

Total:

n × log n

-----------------------------------------------------------------------------------------------------------------------------------------------------
4. Understanding with a Simple Example

Suppose

8 books

First divide:

8

↓

4 + 4

Again divide:

4

↓

2 + 2

Again divide:

2

↓

1 + 1

Now every book is separate.

Number of division levels:

3

because

8

↓

4

↓

2

↓

1

That is:

log₂(8) = 3

Now imagine

at every level,

you touch all books.

Level 1

8 books

Level 2

8 books

Level 3

8 books

Total work

8 + 8 + 8

=

24

=

8 × 3

General formula

n × log n

-----------------------------------------------------------------------------------------------------------------------------------------------------
5. Visual Understanding

Suppose

n = 8

Level 1

8 operations

↓

Level 2

8 operations

↓

Level 3

8 operations

↓

Total

8 × 3

=

24

Suppose

n = 16

Division levels

16

↓

8

↓

4

↓

2

↓

1

Levels

4

Each level

16 operations

Total

16 × 4

=

64

-----------------------------------------------------------------------------------------------------------------------------------------------------
6. Real-Life Example 1 – Organizing Books

Imagine a librarian has

16 books

She repeatedly divides the books into smaller groups until each group has one book.

Then,

while arranging them,

she looks at every book at each stage.

Process

Divide

↓

Process all books

↓

Divide

↓

Process all books

↓

Divide

↓

Process all books

This resembles

O(n log n)

-----------------------------------------------------------------------------------------------------------------------------------------------------
7. Real-Life Example 2 – Tournament

Suppose

16 teams

Round 1

16 teams

↓

Round 2

8 teams

↓

Round 3

4 teams

↓

Round 4

2 teams

↓

Winner

There are approximately

log₂(16)

=

4 rounds

If organizing each round involves processing all participating teams,

the total work follows the pattern of n log n.

-----------------------------------------------------------------------------------------------------------------------------------------------------

8. Technical Understanding (Merge Sort Concept)

Don't worry about every line of code yet.

We'll study Merge Sort later.

Just understand the idea.

Split Array

↓

Split Again

↓

Split Again

↓

Single Elements

↓

Merge

↓

Merge

↓

Merge

Splitting creates

log n levels

At each merge,

every element is processed once.

Therefore

O(n log n)

-----------------------------------------------------------------------------------------------------------------------------------------------------
9. Comparing O(n), O(n log n), and O(n²)

Suppose

n = 1000

Approximate work

Complexity	Approximate Operations
O(n)	1,000
O(n log n)	~10,000
O(n²)	1,000,000

Notice

O(n log n)

is much better than

O(n²).

Suppose

n = 1,000,000

| Complexity | Approximate Operations |
| ---------- | ---------------------: |
| O(n)       |              1,000,000 |
| O(n log n) |            ~20,000,000 |
| O(n²)      |      1,000,000,000,000 |


Even though O(n log n) is slower than O(n),

it is far faster than O(n²).


10. Why is O(n log n) Better than O(n²)?

Suppose you sort

100000 elements

Using

O(n²)

the work becomes enormous.

Using

O(n log n)

the work grows much more slowly.

That is why efficient sorting algorithms aim for O(n log n).

-----------------------------------------------------------------------------------------------------------------------------------------------------

11. Where Do We See O(n log n)?

Common algorithms include:

✓ Merge Sort

✓ Heap Sort

✓ Average-case Quick Sort

✓ Some Divide-and-Conquer algorithms

We'll study each one later in the course.

-----------------------------------------------------------------------------------------------------------------------------------------------------

12. Interview Notes
Question 1

What does O(n log n) mean?

Answer:

The algorithm performs approximately log n stages, and at each stage it processes about n elements, giving a total complexity of O(n log n).

Question 2

Why is Merge Sort O(n log n)?

Answer:

Because it repeatedly divides the array into halves (log n levels) and processes all elements while merging at each level (n work per level).

Question 3

Is O(n log n) better than O(n²)?

Answer:

Yes.

For large inputs, O(n log n) performs dramatically fewer operations than O(n²).

-----------------------------------------------------------------------------------------------------------------------------------------------------

13. Common Mistakes

❌ Thinking

O(n log n)

=

O(n + log n)

Correct:

It is

n × log n

❌ Thinking

log n

means exactly

10

Correct:

The value depends on the input size.

❌ Thinking every sorting algorithm is O(n log n).

Correct:

Some are O(n²), such as Bubble Sort and Selection Sort.

-----------------------------------------------------------------------------------------------------------------------------------------------------

14. Summary

An O(n log n) algorithm:

✓ Divides the problem repeatedly.

✓ Has approximately log n levels.

✓ Processes all n elements at each level.

✓ Is common in efficient sorting algorithms.

Examples:

Merge Sort
Heap Sort
Average-case Quick Sort
15. Revision Notes
Divide Problem

↓

log n Levels

↓

Process n Elements

at Every Level

↓

n × log n

↓

O(n log n)


