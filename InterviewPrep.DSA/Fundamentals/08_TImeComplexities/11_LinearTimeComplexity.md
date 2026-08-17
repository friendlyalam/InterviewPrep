1. Definition

An algorithm has O(n) (Linear Time Complexity) if the number of operations increases linearly with the size of the input.

In simple words,

If the input doubles, the work approximately doubles.

2. Simple Definition

O(n) means the algorithm processes each input item once.

The more data we have, the more work the algorithm performs.

--------------------------------------------------------------------------------------------------------------

3. Why Do We Need O(n)?

Suppose you have a classroom.

Teacher asks,

"Count all the students."

Can you know the answer by looking at just one student?

No.

You must count every student.

If there are:

20 students → Count 20
100 students → Count 100
500 students → Count 500

As the number of students increases, your work also increases.

This is Linear Time.

------------------------------------------------------------

4. Why is it Called "Linear Time"?

Let's see.

| Number of Students | Work Required |
| ------------------ | ------------- |
| 10                 | 10 counts     |
| 20                 | 20 counts     |
| 30                 | 30 counts     |
| 100                | 100 counts    |


--------------------------------------------------------------------
Notice something.

If students become:

10 → 20

Work also becomes

10 → 20

The increase is proportional.

This relationship forms a straight line if plotted on a graph.

That's why it is called Linear Time.

-----------------------------------------------------------------------

5. Real-Life Example 1 – Finding a Friend

Imagine your class photo.

Rahul
Aman
John
David
Rohit

Teacher asks:

"Find David."

You start checking.

Rahul ❌

↓

Aman ❌

↓

John ❌

↓

David ✅

Suppose David is the last person.

You check everyone.

If there are 500 students,

you may check 500 students.

More students → More work.

O(n)

----------------------------------------------------------------------

6. Real-Life Example 2 – Attendance

Teacher takes attendance.

Rahul

↓

Present

↓

Aman

↓

Present

↓

John

↓

Absent

↓

...

Teacher must call every student's name.

Cannot skip anyone.

Again,

O(n)

-------------------------------------------------------------------------------------------
7. Real-Life Example 3 – Reading a Book

Suppose a book has:

100 pages.

To read the whole book,

you read:

Page 1

↓

Page 2

↓

Page 3

↓

...

↓

Page 100

Now the book has:

500 pages.

You must read:

500 pages.

Again,

Work grows with input size.

O(n)

--------------------------------------------------------------------------------------------

8. Technical Example 1 – Printing an Array
int[] numbers = {10,20,30,40,50};

for(int i = 0; i < numbers.Length; i++)
{
    Console.WriteLine(numbers[i]);
}
Dry Run

Array

10 20 30 40 50

Iteration 1

Print 10

Iteration 2

Print 20

Iteration 3

Print 30

Iteration 4

Print 40

Iteration 5

Print 50

Loop executes:

5 times

Suppose

n = 1000

Loop executes

1000 times

As n increases,

operations increase.

Therefore,

O(n)

------------------------------------------------------------------------------

9. Technical Example 2 – Finding Maximum Number
int max = numbers[0];

for(int i = 1; i < numbers.Length; i++)
{
    if(numbers[i] > max)
        max = numbers[i];
}

Question:

How many numbers must we check?

Every number.

If there are:

10 elements

↓

10 comparisons

1000 elements

↓

1000 comparisons (approximately)

Again,

O(n)

----------------------------------------------------------------------------------

10. Technical Example 3 – Searching (Linear Search)
for(int i = 0; i < numbers.Length; i++)
{
    if(numbers[i] == target)
        return i;
}

Suppose

Target = 50

Array

10 20 30 40 50

Dry Run

Compare 10 ❌

↓

Compare 20 ❌

↓

Compare 30 ❌

↓

Compare 40 ❌

↓

Compare 50 ✅

Total comparisons

5

If there are

10000 elements,

the algorithm may perform

10000 comparisons.

Hence,

O(n)

-----------------------------------------------------------------------------------

11. Visual Understanding

Suppose

n = 10
Operations
10


n = 100
Operations
100


n = 1000
Operations
1000

n = 1000000
Operations
1000000

Notice:

As input increases,

operations increase in the same proportion.

This is Linear Time.

-------------------------------------------------------------------------------------

12. O(1) vs O(n)

| Input Size | O(1)        | O(n)                 |
| ---------- | ----------- | -------------------- |
| 10         | 1 operation | 10 operations        |
| 100        | 1 operation | 100 operations       |
| 1,000      | 1 operation | 1,000 operations     |
| 1,000,000  | 1 operation | 1,000,000 operations |

Observation:

O(1) remains constant.

O(n) keeps growing.

--------------------------------------------------------------------------------------------

13. Does Every Loop Mean O(n)?

Many beginners think:

"If there is a loop, then it is always O(n)."

❌ Wrong.

Example:

for(int i = 0; i < 5; i++)
{
    Console.WriteLine(i);
}

This loop always executes exactly 5 times, no matter how large n is.

Even if n = 10 or n = 1,000,000, it still runs only 5 times.

So its complexity is:

O(1)

Now look at this:

for(int i = 0; i < n; i++)
{
    Console.WriteLine(i);
}

This loop depends on n.

If n changes, the number of iterations changes.

Therefore:

O(n)

Important Rule: A loop is O(n) only if its number of iterations depends on the input size (n).

------------------------------------------------------------------------------------------------------------

14. Interview Notes
Question 1

What is O(n)?

Answer:

O(n) means the algorithm performs work proportional to the input size. As n increases, the number of operations increases approximately at the same rate.

Question 2

Why is Linear Search O(n)?

Answer:

Because in the worst case it may need to check every element exactly once.

Question 3

Is every for loop O(n)?

Answer:

No.

A loop is O(n) only when the number of iterations depends on the input size. A loop with a fixed number of iterations (like 5 or 100) is O(1).

-----------------------------------------------------------------------------------------------------------------------------------------------------

15. Common Mistakes

❌ Thinking every loop is O(n).

Correct:

Only loops that depend on n are O(n).

❌ Thinking O(n) means slow.

Correct:

O(n) is often the best possible solution for many problems because every element must be examined.

For example:

Finding the maximum element.
Calculating the sum of an array.
Checking if an array contains duplicates (without extra data structures).

❌ Thinking O(n) means exactly n operations.

Correct:

It means the work grows proportionally with n. There may be more than one operation per iteration, but the growth is still linear.

------------------------------------------------------------------------------------------------------------------------------------------------

16. Summary

A Linear Time algorithm:

✓ Processes data one item at a time.

✓ Work increases as input increases.

✓ Usually involves traversing an array, list, or string.

✓ Is written as O(n).

Examples:

Printing an array.
Linear Search.
Finding the maximum element.
Calculating the sum of an array.

---------------------------------------------------------------------------------------------------------------------------------------------

17. Revision Notes
Input Size (n)

10
100
1000
1000000

↓

Operations

10
100
1000
1000000

↓

Work grows proportionally

↓

O(n)

