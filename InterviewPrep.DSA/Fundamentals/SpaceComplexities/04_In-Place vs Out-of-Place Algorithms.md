1. Definition

Algorithms can be classified based on how much extra memory they use.

There are two common types:

In-Place Algorithm
Out-of-Place Algorithm
2. Simple Definition
In-Place Algorithm

Uses very little extra memory (usually O(1)).

The algorithm modifies the original data instead of creating another copy.

Out-of-Place Algorithm

Creates a new data structure to solve the problem.

Therefore, it uses additional memory.

3. Why Do We Need This?

Imagine you have a file that is 100 GB.

Method A

Create another 100 GB copy.

Now you need:

100 GB (Original)

+

100 GB (Copy)

=

200 GB
Method B

Modify the original file directly.

Memory used:

100 GB

No extra copy.

Obviously,

Method B uses less memory.

That is the idea behind In-Place Algorithms.

4. In-Place Algorithm
Definition

An algorithm is In-Place if it solves the problem using only a constant amount of extra memory.

Usually,

Auxiliary Space:

O(1)
Simple Definition

Don't create another collection.

Modify the existing one.

5. Real-Life Example 1 – Rearranging Books

Imagine a bookshelf.

Books are already placed.

Instead of buying another bookshelf,

you simply move books within the same shelf.

No extra shelf is needed.

This is an In-Place approach.

6. Real-Life Example 2 – Organizing Clothes

You have clothes inside one cupboard.

You rearrange them inside the same cupboard.

You don't buy another cupboard.

Again,

In-Place.

7. Technical Example – Reverse an Array (In-Place)

Original Array

10 20 30 40 50

Code

int left = 0;
int right = arr.Length - 1;

while(left < right)
{
    int temp = arr[left];
    arr[left] = arr[right];
    arr[right] = temp;

    left++;
    right--;
}

Result

50 40 30 20 10

Question

Did we create another array?

No.

Only variables:

left

right

temp

Extra Space

O(1)

This is an In-Place Algorithm.

8. Visual Representation

Before

+----+----+----+----+----+
| 10 | 20 | 30 | 40 | 50 |
+----+----+----+----+----+

After

+----+----+----+----+----+
| 50 | 40 | 30 | 20 | 10 |
+----+----+----+----+----+

The same array was modified.

9. Out-of-Place Algorithm
Definition

An algorithm is Out-of-Place if it creates another data structure while solving the problem.

Auxiliary Space usually grows with the input.

Simple Definition

Instead of modifying the original data,

create another collection.

10. Real-Life Example 1 – Photocopy

Original document

↓

Create another copy

↓

Edit the copy

Original remains unchanged.

Extra memory is required.

11. Real-Life Example 2 – New Notebook

Instead of erasing mistakes,

you rewrite everything in a new notebook.

Now you have

Two notebooks.

More memory is used.

12. Technical Example – Copy an Array
int[] copy = new int[arr.Length];

for(int i = 0; i < arr.Length; i++)
{
    copy[i] = arr[i];
}

Memory

Original

10 20 30 40 50

Copy

10 20 30 40 50

Two arrays exist.

Extra Space

O(n)

This is an Out-of-Place algorithm.

13. Visual Representation

Original

+----+----+----+----+----+
| 10 | 20 | 30 | 40 | 50 |
+----+----+----+----+----+

Copy

+----+----+----+----+----+
| 10 | 20 | 30 | 40 | 50 |
+----+----+----+----+----+

Memory doubled.

14. Comparing Both

| Feature       | In-Place          | Out-of-Place               |
| ------------- | ----------------- | -------------------------- |
| Extra Memory  | O(1) (usually)    | O(n) or more               |
| Original Data | Modified          | Usually preserved          |
| Memory Usage  | Low               | Higher                     |
| Speed         | May avoid copying | May be easier to implement |


15. Common Algorithm Examples
In-Place

✓ Bubble Sort

✓ Selection Sort

✓ Insertion Sort

✓ Reverse Array (using swaps)

These modify the original array.

Out-of-Place

✓ Merge Sort (traditional implementation)

✓ Copy Array

✓ Clone List

✓ Creating a filtered copy

These allocate additional memory.

Note: Advanced versions of some algorithms (like Merge Sort) can reduce extra memory, but the standard implementation taught in DSA uses additional arrays.

16. Why Product Companies Care

Suppose a company processes

1 Billion Records

Creating another copy means:

More RAM
More memory allocation
More garbage collection
Higher cost

An In-Place algorithm can save significant resources.

That's why companies like Microsoft, Google, and Amazon often ask about memory optimization.

17. Interview Notes
Question 1

What is an In-Place Algorithm?

Answer:

An algorithm that solves a problem using a constant amount of extra memory, usually by modifying the original data.

Question 2

What is an Out-of-Place Algorithm?

Answer:

An algorithm that allocates additional data structures instead of modifying the original input directly.

Question 3

Is In-Place always better?

Answer:

Not always.

In-place algorithms save memory, but they modify the original data.

Sometimes preserving the original data is more important.

Question 4

Why is reversing an array using swaps considered In-Place?

Answer:

Because it only uses a few temporary variables and does not allocate another array.

18. Common Mistakes

❌ Thinking "In-Place" means no variables.

Correct:

A few temporary variables (like temp, left, right) are allowed.

❌ Thinking every sorting algorithm is In-Place.

Correct:

Some, such as traditional Merge Sort, require extra memory.

❌ Thinking Out-of-Place is always bad.

Correct:

Out-of-place algorithms are sometimes simpler, safer, or preserve the original input.

19. Summary
In-Place

✓ Uses constant extra memory.

✓ Usually O(1) auxiliary space.

✓ Modifies original data.

Examples:

Reverse Array
Bubble Sort
Selection Sort
Insertion Sort
Out-of-Place

✓ Creates new data structures.

✓ Uses additional memory.

✓ Often O(n) auxiliary space.

Examples:

Copy Array
Merge Sort (traditional)
Clone List
20. Revision Notes
Space Optimization

↓

In-Place

↓

Modify Original Data

↓

O(1) Extra Space

----------------------

Out-of-Place

↓

Create New Data

↓

O(n) Extra Space