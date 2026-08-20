1. Definition

Sliding Window is a technique where we maintain a continuous (contiguous) portion of an array or string and move that 
window step by step instead of repeatedly processing the same elements.

Instead of creating a new subarray or substring every time, we expand or shrink one window.

--------------------------------------------------------------------

2. Why Do We Need Sliding Window?

Suppose an interviewer asks:

Maximum sum of 3 consecutive elements
Longest substring without repeating characters
Minimum size subarray with sum ≥ K
Maximum average of K elements

A beginner often checks every possible subarray, leading to O(n²) or worse.

Sliding Window reuses previous work, reducing many of these problems to O(n).

----------------------------------------------------------------------------------

3. Simple Definition

Keep a window over consecutive elements and move it instead of starting over each time.

Think of it as looking through a moving frame.

-----------------------------------------------------------

4. Real-Life Example
Camera on a Cricket Ground

Imagine a camera that shows 3 players at a time.

Initially:

[A] [B] [C] D E F G

Move one step:

A [B] [C] [D] E F G

Move again:

A B [C] [D] [E] F G

The camera (window) slides.

It doesn't restart from the beginning every time.

Bus Window

While sitting in a moving bus, your window always shows only a small part of the road.

The window moves continuously.

That's the Sliding Window idea.

------------------------------------------------------

5. Technical Example

Array

[2, 5, 1, 8, 2, 9, 1]

Window Size = 3

Windows are:

[2,5,1]

↓

[5,1,8]

↓

[1,8,2]

↓

[8,2,9]

↓

[2,9,1]

Notice:

We don't calculate every window from scratch.

We remove one element and add one element.

----------------------------------------------------------

6. Types of Sliding Window

There are two major types.

Type 1 – Fixed Size Window

Window size never changes.

Example:

Maximum sum of K elements
Average of K elements

Visualization:

Window Size = 3

[A B C] D E F

↓

A [B C D] E F

↓

A B [C D E] F


Type 2 – Variable Size Window

Window grows and shrinks based on a condition.

Examples:

Longest substring without repeating characters
Minimum window substring
Smallest subarray with sum ≥ K

Visualization:

[A B]

↓

[A B C D]

↓

Shrink

[C D]

The window size changes dynamically.

-----------------------------------------------------

7. Generic C# Templates
Fixed Size Window
int left = 0;

for (int right = 0; right < array.Length; right++)
{
    // Add current element

    if (right - left + 1 > k)
    {
        // Remove left element
        left++;
    }

    if (right - left + 1 == k)
    {
        // Process current window
    }
}
Variable Size Window
int left = 0;

for (int right = 0; right < array.Length; right++)
{
    // Expand window

    while (/* condition not satisfied */)
    {
        // Shrink window
        left++;
    }

    // Process valid window
}

Don't worry about the exact code yet. We'll use these templates repeatedly during problems.

8. Which Data Structures Support Sliding Window?

| Data Structure  | Can Use?              |
| --------------- | --------------------- |
| Array           | ✅                     |
| String          | ✅                     |
| Character Array | ✅                     |
| List            | ✅ (with index access) |
| Linked List     | ⚠️ Rarely             |


Not commonly used on:

Tree ❌
Graph ❌
Stack ❌
Queue ❌

-------------------------------

9. Recognition Clues

When you see words like:

Contiguous subarray
Substring
Consecutive elements
Window size K
Longest
Shortest
Maximum sum
Minimum length
Continuous segment

Think:

Sliding Window

--------------------------------------------------------

10. Sliding Window vs Two Pointers

| Two Pointers                               | Sliding Window                                  |
| ------------------------------------------ | ----------------------------------------------- |
| Two pointers solve many different problems | Sliding Window is a special use of two pointers |
| May move toward each other                 | Usually both pointers move left to right        |
| Doesn't always maintain a window           | Always maintains a contiguous window            |

So:

Sliding Window is built using the Two Pointer technique.

-------------------------------------------------------------

11. When Should We NOT Use Sliding Window?

Avoid it when:

Elements are not contiguous.

Example:

Pick any 3 elements.

(Not necessarily consecutive.)

The problem asks for frequency over the entire dataset.

Use:

HashMap

The problem involves trees or graphs.

Use:

DFS

or

BFS

The problem requires random intervals repeatedly.

Use:

Prefix Sum

or

Segment Tree

-----------------------------------------------

12. Time Complexity

Most Sliding Window solutions:

O(n)

Each element enters and leaves the window at most once.

----------------------------------------------------------------

13. Space Complexity

Usually:

O(1)

Sometimes:

O(26)

O(128)

O(256)

O(k)

if we maintain character frequencies or similar auxiliary structures.

-------------------------------------------------------------------------

14. Advantages

✅ Reduces many O(n²) solutions to O(n).

✅ Avoids repeated calculations.

✅ Very common in coding interviews.

✅ Efficient for contiguous data.

---------------------------------------------------------------

15. Disadvantages

❌ Only works well when the problem involves contiguous elements.

❌ Choosing when to expand or shrink the window can be tricky.

--------------------------------------------------------------------------

16. Frequently Asked Interview Questions
Q1. Is Sliding Window an algorithm?

Answer:

No.

It is a problem-solving technique.

Q2. Is Sliding Window always implemented using Two Pointers?

Answer:

Yes.

Sliding Window uses two pointers (left and right) to define the current window.

Q3. Does Sliding Window always have a fixed size?

Answer:

No.

It can be either Fixed Size or Variable Size.

Q4. Can Sliding Window be used on strings?

Answer:

Yes.

A string is simply a sequence of characters, so a window can slide over it just like an array.

-----------------------------------------------------------------------------------------------

17. Common Mistakes

❌ Using Sliding Window when the problem doesn't involve contiguous elements.

❌ Forgetting to remove elements when shrinking the window.

❌ Confusing Fixed Window with Variable Window.

❌ Recalculating the entire window instead of updating it incrementally.

---------------------------------------------------------------------------

18. Summary
Need contiguous elements?

        │
       Yes
        │
        ▼

Window Size Fixed?

│
├── Yes
│      Fixed Sliding Window
│
└── No
       Variable Sliding Window

↓

Move Left & Right

↓

Reuse Previous Work

↓

Often O(n)

--------------------------------------------------------------------------------
Technique Cheat Sheet (Updated)

| Technique      | Recognition Words                                                | Common Data Structures     | Typical Complexity |
| -------------- | ---------------------------------------------------------------- | -------------------------- | -----------------: |
| Brute Force    | Try all, straightforward                                         | All                        |             Varies |
| Linear Scan    | Find, count, maximum, minimum                                    | Array, String, List        |               O(n) |
| Two Pointers   | Reverse, palindrome, pair, sorted, remove duplicates             | Array, String, Linked List |               O(n) |
| Sliding Window | Contiguous subarray, substring, longest, shortest, window size K | Array, String              |               O(n) |


