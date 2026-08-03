1. Definition

Linear Scan is a technique where we visit each element exactly once, from the beginning to the end (or end to beginning), to find or process information.

2. Why Do We Need It?

Suppose I ask:

Find the maximum number.
Find the minimum number.
Count even numbers.
Find the first negative number.
Check whether a number exists.

How will you answer?

The simplest approach is:

Look at every element one by one.

That is Linear Scan.

-----------------------------------------

3. Simple Definition

Linear Scan means

Visit every element one by one until the work is completed.

-----------------------------------------
4. Real-Life Example
Teacher Checking Attendance

Imagine a teacher has a class of 40 students.

Roll No.

1

2

3

4

...

40

The teacher calls each roll number one by one.

Nobody is skipped.

This is Linear Scan.

---------------------------------------------------

Another Example

Finding your friend in a queue.

You start from the first person.

Check everyone.

Stop when you find your friend.

----------------------------------------------------

5. Technical Example

Array

[12, 25, 8, 41, 30]

Find the maximum element.

Linear Scan

12

↓

25

↓

8

↓

41

↓

30

Every element is visited once.

-------------------------------------------------------

6. Visualization
Index

0    1    2    3    4

+----+----+----+----+----+
|12  |25  |8   |41  |30  |
+----+----+----+----+----+

        ↑

Move one step at a time

0 → 1 → 2 → 3 → 4

---------------------------------------------

7. Generic C# Template
for (int i = 0; i < array.Length; i++)
{
    // Process array[i]
}

Reverse scan

for (int i = array.Length - 1; i >= 0; i--)
{
    // Process array[i]
}

----------------------------------------------------
8. Which Data Structures Can Use Linear Scan?

Almost every sequential data structure.

| Data Structure | Can Use?                       |
| -------------- | ------------------------------ |
| Array          | ✅                              |
| List           | ✅                              |
| String         | ✅                              |
| Linked List    | ✅                              |
| Queue          | ✅                              |
| Stack          | ✅ (while popping or iterating) |

Trees and Graphs use traversal methods like DFS/BFS instead of a simple linear scan.

-----------------------------------------------------

9. Recognition Clues

When a problem contains words like:

Find maximum
Find minimum
Search
Count
Check
Exists
Sum
Average
Product
Frequency (simple counting)
Traverse

Your first thought should be:

Linear Scan
-----------------------------------------------------------------

10. When Should We NOT Use Linear Scan?

Avoid it when:

The data is sorted and searching is required.

Instead of

O(n)

Use Binary Search

O(log n)

-----------------------
The problem asks repeated lookups.

Instead of scanning again and again,

Use

HashMap

or

HashSet
----------------------

The problem asks Top K.

Use

Heap

instead.

11. Time Complexity

One traversal

O(n)

Because every element is visited once.

-----------------------------------------------

12. Space Complexity
O(1)

No extra memory is usually required.

------------------------------------------------------------
13. Advantages

✅ Very simple

✅ Easy to implement

✅ Works on unsorted data

✅ Doesn't require preprocessing

✅ Often the optimal solution

----------------------------------

14. Disadvantages

❌ Slow for repeated searches

Example

Searching 1000 times

Each search

O(n)

Total

O(n × 1000)

Better

HashMap

❌ Not suitable for sorted search problems.

-------------------------------------------

15. Frequently Asked Interview Questions
Q1. Is Linear Scan an algorithm?

Answer

No.

It is a problem-solving technique.

Many algorithms use Linear Scan.

Q2. Is Linear Search and Linear Scan the same?

Answer

No.

Linear Search is a specific algorithm used to find an element.

Linear Scan is a broader technique used to process every element, such as finding the maximum, counting, summing, or searching.

Q3. Can Linear Scan stop before reaching the end?

Answer

Yes.

For example, when searching for an element, you can stop as soon as it is found.

For tasks like finding the maximum element, you generally must scan all elements.

Q4. Is Linear Scan always O(n)?

Answer

Usually yes, but if you stop early (for example, finding an element at index 0), the actual execution is shorter. 
However, in Big O we consider the worst case, which is still O(n).

---------------------------------------------------------------

16. Common Mistakes

❌ Confusing Linear Scan with Linear Search.

❌ Forgetting to process the last element.

❌ Assuming every scan must visit all elements.

❌ Using Linear Scan repeatedly when a HashMap would be more efficient.

----------------------------------------------------------

17. Summary
Need to process every element?

        │
       Yes
        │
        ▼
Use Linear Scan

↓

Visit each element

↓

Process

↓

Move to next

↓

Finish
----------------------------------------------------------
Technique Cheat Sheet (Updated)

| Technique   | Purpose                 | Common Data Structures           | Typical Complexity |
| ----------- | ----------------------- | -------------------------------- | -----------------: |
| Brute Force | Solve correctly first   | All                              |             Varies |
| Linear Scan | Visit each element once | Array, String, List, Linked List |               O(n) |
