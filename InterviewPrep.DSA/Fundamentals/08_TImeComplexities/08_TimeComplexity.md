
1. Definition

Time Complexity is the measure of the amount of work (or number of basic operations) an algorithm performs as the size of the input increases.

It tells us how the running time of an algorithm grows when the input size becomes larger.

---------------------------------------------------------------------------------------------------------------------------------------------------

2. Simple Definition

Time Complexity tells us how much work an algorithm has to do.

It does not tell us the actual time in seconds.

Instead, it tells us how the work increases when the amount of data increases.

---------------------------------------------------------------------------------------------------------------------------------------------------

3. Why Do We Need Time Complexity?

Imagine you have written two C# programs.

Both give the correct answer.

Now the question is...

Which program is better?

Without Time Complexity, we cannot compare them.

Suppose we have these two programs.

Program A
for(int i = 0; i < arr.Length; i++)
{
    if(arr[i] == target)
        return i;
}

Program A checks every element one by one.

Program B
Array.BinarySearch(arr, target);

Program B uses Binary Search.

Both return the correct answer.

But...

Which one will perform better for 10 million elements?

Time Complexity helps us answer this question.

---------------------------------------------------------------------------------------------------------------------------------------------------

4. Why Don't We Measure Time in Seconds?

This is the most important concept.

Many beginners think:

"My program took 2 seconds, so its Time Complexity is 2 seconds."

❌ Wrong.

Time Complexity is not measured in seconds.

Suppose you run the same program on two computers.

Computer A

Intel i3
8 GB RAM

Computer B

Intel i9
64 GB RAM

The same algorithm may take:

Computer A

5 seconds

Computer B

1 second

Did the algorithm change?

No.

Only the computer changed.

Therefore, measuring seconds is not a reliable way to compare algorithms.

------------------------------------------

Another Example

Imagine you ask two students to solve the same maths problem.

Student A

Finishes in 2 minutes.

Student B

Finishes in 5 minutes.

Does this mean Student A always has the better method?

Not necessarily.

Maybe Student B writes slowly.

Maybe Student A writes quickly.

The speed of the person is different.

We care about the method, not the person.

Similarly:

CPU speed changes.
RAM changes.
Programming language changes.
Compiler optimizations change.

But the algorithm remains the same.

---------------------------------------------------------------------------------------------------------------------------------------------------

5. What Does Time Complexity Actually Measure?

Time Complexity measures the number of basic operations performed by an algorithm.

Examples of basic operations:

Comparison (==, <, >)
Addition (+)
Subtraction (-)
Assignment (=)
Increment (i++)
Array access (arr[i])

Instead of counting seconds, we count how many operations are performed as the input grows.

---------------------------------------------------------------------------------------------------------------------------------------------------

6. What is Input Size (n)?

In DSA, you'll often see the letter:

n

n represents the size of the input.

Example 1
int[] numbers = new int[5];

Number of elements = 5

Therefore,

n = 5

------
Example 2
int[] numbers = new int[1000];
n = 1000

-----
Example 3

A company has

1,000,000 employees

Then,

n = 1,000,000
Important Point

n does not always mean an array.

It simply means the size of the input.

Examples:

| Problem     | Meaning of `n`                                                                 |
| ----------- | ------------------------------------------------------------------------------ |
| Array       | Number of elements                                                             |
| String      | Number of characters                                                           |
| Linked List | Number of nodes                                                                |
| Tree        | Number of nodes                                                                |
| Graph       | Number of vertices (or sometimes vertices + edges, depending on the algorithm) |
| Matrix      | Number of rows, columns, or both (depending on the algorithm)                  |

---------------------------------------------------------------------------------------------------------------------------------------------------

7. Real-Life Example 1 – Finding Your Friend

Suppose you're standing outside a school.

There are 10 students.

You want to find your friend.

You may need to check:

Student 1

↓

Student 2

↓

Student 3

Maybe you find them after checking 3 students.

Easy.

Now imagine there are:

100,000 students

You still check one person at a time.

Student 1

↓

Student 2

↓

Student 3

↓

...

↓

Student 100000

Has your method changed?

No.

Only the number of people increased.

Therefore, the amount of work increased.

This is exactly what Time Complexity studies.

---------------------------------------------------------------------------------------------------------------------------------------------------

8. Real-Life Example 2 – Dictionary

Suppose you want to find the word:

Algorithm

Method 1

Start from page 1.

Read every page.

Eventually you find it.

Method 2

Open the correct alphabetical section.

Reach the word quickly.

Both methods work.

One requires much less work.

Time Complexity helps us compare these methods.

---------------------------------------------------------------------------------------------------------------------------------------------------

9. Technical Example

Suppose we have:

int[] arr = { 5, 10, 15, 20, 25 };

Target

25

Algorithm

for(int i = 0; i < arr.Length; i++)
{
    if(arr[i] == 25)
        return i;
}
Dry Run

Iteration 1

Compare 5 with 25

Not Equal

Iteration 2

Compare 10 with 25

Not Equal

Iteration 3

Compare 15 with 25

Not Equal

Iteration 4

Compare 20 with 25

Not Equal

Iteration 5

Compare 25 with 25

Found

Total comparisons:

5

If the array had 1000 elements, the algorithm might perform 1000 comparisons in the worst case.

As n grows, the amount of work grows.

---------------------------------------------------------------------------------------------------------------------------------------------------

10. Important Observation

Suppose the input sizes are:

n = 5
Operations
5

n = 100
Operations
100


n = 1000
Operations
1000

Notice something:

As n increases,

the number of operations also increases.

This relationship between input size and work is what Time Complexity describes.

---------------------------------------------------------------------------------------------------------------------------------------------------

11. Common Misconceptions
❌ Misconception 1

Time Complexity means seconds.

Correct

Time Complexity measures growth of operations, not clock time.

❌ Misconception 2

A faster computer gives better Time Complexity.

Correct

It gives better execution time, not better Time Complexity.

❌ Misconception 3

Changing C# to C++ changes Time Complexity.

Correct

It may change execution speed.

The algorithm's Time Complexity remains the same.

12. Interview Notes

Interviewers may ask:

Q1. What is Time Complexity?

Answer:

Time Complexity is the measure of how the number of basic operations performed by an algorithm grows as the input size increases.

Q2. Why don't we measure algorithms in seconds?

Answer:

Because execution time depends on hardware, programming language, compiler optimizations, and system load. 
Time Complexity provides a machine-independent way to compare algorithms.

Q3. What does n represent?

Answer:

n represents the size of the input. Depending on the problem, it could be the number of array elements, string characters, tree nodes, graph vertices, etc.

---------------------------------------------------------------------------------------------------------------------------------------------------

13. Summary

Time Complexity:

Measures the growth of an algorithm as input size increases.
Does not measure seconds.
Counts the amount of work (basic operations).
Uses n to represent input size.
Helps us compare algorithms fairly.

---------------------------------------------------------------------------------------------------------------------------------------------------
14. Revision Notes
Input Size (n)

        ↓

Algorithm Executes

        ↓

Performs Basic Operations

        ↓

As n Increases

        ↓

Operations Increase

        ↓

This Growth = Time Complexity



