1. Definition

Hashing is a technique that stores and retrieves data using a key so that 
searching, inserting, and deleting can usually be performed in O(1) average time.

Instead of searching element by element, we directly access data using its key.

---------------
2. Why Do We Need Hashing?

Suppose an interviewer asks:

Is this number already present?
Count the frequency of each character.
Find duplicate elements.
Find the first unique character.
Solve Two Sum.
Group anagrams.

Without Hashing:

Search every element

↓

O(n)

If this search happens inside another loop:

O(n²)

With Hashing:

Store once

↓

Lookup directly

↓

O(1) Average

----------------------------------
3. Simple Definition

Store information so you can find it instantly instead of searching repeatedly.

Think of it as creating a fast lookup table.

----------------------------------------

4. Real-Life Examples
Example 1 – Phone Contacts

Imagine your phone has 5,000 contacts.

When someone calls:

Do you check every contact one by one?

No.

You search directly by name or number.

This is the idea behind Hashing.

Example 2 – Dictionary

Suppose you want the meaning of the word:

Algorithm

You don't read every page.

You jump directly to the word.

That's Hashing.

Example 3 – Student Roll Number
Roll No.

101

↓

Student Details

Instead of searching all students, you directly find the required record.

---------------------------------------------------------------------------------------

5. Technical Example

Array

[4, 2, 7, 2, 5, 7, 7]

Frequency Map

4 → 1

2 → 2

7 → 3

5 → 1

Instead of counting repeatedly, we store the count once.

---------------------------------------------------------------

6. What is a HashMap?

A HashMap stores data as:

Key → Value

Example

101 → "Mohd"

102 → "Rahul"

103 → "Amit"

In C#

Dictionary<int, string> students = new();


-----------------------------------------------------------------------------------

7. What is a HashSet?

A HashSet stores only unique values.

Example

Input

2 4 2 7 4 8

HashSet

2

4

7

8

Duplicates are automatically ignored.

In C#

HashSet<int> numbers = new();

-----------------------------------------------------------------

8. HashMap vs HashSet

| HashMap (Dictionary)           | HashSet                     |
| ------------------------------ | --------------------------- |
| Stores Key + Value             | Stores only Value           |
| Used for frequency and mapping | Used for uniqueness         |
| Example: `5 → "Apple"`         | Example: `5`                |
| Allows fast lookup by key      | Allows fast existence check |


----------------------------------------------------------------------

9. Generic C# Templates
HashMap
Dictionary<int, int> map = new();

foreach (int num in array)
{
    if (map.ContainsKey(num))
        map[num]++;
    else
        map[num] = 1;
}

This is the standard frequency-counting template.

---------------------------------------------------------------------

HashSet
HashSet<int> set = new();

foreach (int num in array)
{
    set.Add(num);
}

----------------------------------------------------------------
10. Which Data Structures Support Hashing?

Hashing is usually combined with other data structures.

----------------------------------------------------------------------

| Data Structure | Can Use? |
| -------------- | -------- |
| Array          | ✅        |
| String         | ✅        |
| Linked List    | ✅        |
| Tree           | ✅        |
| Graph          | ✅        |

Notice:

Hashing is a technique, not a replacement for these structures.

--------------------------------------------------------------------------------

11. Recognition Clues

When you see words like:

Duplicate
Unique
Frequency
Count occurrences
Exists
Already seen
Common elements
Intersection
Lookup
Mapping
Pair sum (unsorted)

Think immediately:

HashMap or HashSet

-------------------------------------------------

12. When Should We Use HashMap?

Use HashMap (Dictionary) when you need:

Frequency counting
Key → Value mapping
Index lookup
Two Sum
Character counts
Word counts

Example

Apple → 5

Banana → 8

Orange → 2


------------------------------------------------------

13. When Should We Use HashSet?

Use HashSet when you only need:

Unique values
Duplicate detection
Fast existence checking

Example

Is 25 already present?

--------------------------------------------------------------
14. When Should We NOT Use Hashing?

Avoid Hashing when:

The data must remain sorted.

HashMap does not keep elements in sorted order.

Use:

Sorted Dictionary
Tree-based structures
The problem depends on element order.

Example:

"Find the next greater element to the right."

A HashMap alone cannot solve this.

Use:

Stack
The problem asks for a priority.

Use:

Heap (Priority Queue)

--------------------------------------------------------------
15. Time Complexity
| Operation | Average |
| --------- | ------- |
| Insert    | O(1)    |
| Search    | O(1)    |
| Delete    | O(1)    |

 Interview Note: In the worst case (due to many hash collisions), these operations can degrade, 
 but for most interview problems and real-world usage, we consider the average case.

--------------------------------------------------------------

16. Space Complexity

Extra HashMap

O(n)

Extra HashSet

O(n)

----------------------------------------------
17. Advantages

✅ Extremely fast lookups.

✅ Reduces many O(n²) problems to O(n).

✅ Excellent for counting and duplicate detection.

✅ Simple to use.

-------------------------------------

18. Disadvantages

❌ Uses extra memory.

❌ Doesn't maintain sorted order.

❌ Keys must be hashable.

-----------------------------------------------------------------

19. Frequently Asked Interview Questions
Q1. Is Hashing an algorithm?

Answer:

No.

It is a problem-solving technique that uses hash-based data structures.

Q2. What is the difference between HashMap and HashSet?

Answer:

HashMap (Dictionary) stores Key → Value pairs.
HashSet stores only unique values.
Q3. Why is Hashing faster than Linear Search?

Answer:

Because we access data using a key instead of checking each element one by one.

Q4. Does HashMap keep insertion order?

Answer:

In C#, you should not rely on Dictionary<TKey, TValue> preserving insertion order. If order matters, choose an appropriate ordered collection.

Q5. Can duplicate keys exist in a HashMap?

Answer:

No.

Keys must be unique.

However, different keys may have the same value.

Example

101 → "A"

102 → "A"

This is valid because the keys are different.

20. Common Mistakes

❌ Using a HashSet when frequency counts are needed.

❌ Using a HashMap when only uniqueness needs to be checked.

❌ Assuming the iteration order is sorted or fixed.

❌ Forgetting to handle missing keys safely.


-----------------------------------------------------------------------------

21. Summary
Need fast lookup?

        │
       Yes
        │
        ▼

Need only uniqueness?

│
├── Yes
│      HashSet
│
└── No
       Need Key → Value?

        │
       Yes
        ▼

     HashMap (Dictionary)


     ----------------------------------------------------------------
22. Technique Cheat Sheet (Updated)

| Technique      | Recognition Words                             | Common Data Structures                  |     Typical Complexity |
| -------------- | --------------------------------------------- | --------------------------------------- | ---------------------: |
| Brute Force    | Try all, straightforward                      | All                                     |                 Varies |
| Linear Scan    | Find, count, max, min                         | Array, String, List                     |                   O(n) |
| Two Pointers   | Reverse, palindrome, pair, sorted             | Array, String, Linked List              |                   O(n) |
| Sliding Window | Contiguous subarray, substring, window        | Array, String                           |                   O(n) |
| Prefix Sum     | Range sum, cumulative sum                     | Array, Matrix                           | Build O(n), Query O(1) |
| Hashing        | Duplicate, unique, frequency, lookup, mapping | Array, String, Linked List, Tree, Graph |    Average O(1) lookup |


