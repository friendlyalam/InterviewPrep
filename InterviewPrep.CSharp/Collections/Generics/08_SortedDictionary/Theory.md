Collection: Generic ✅
Namespace: System.Collections.Generic
Category: Key-value collection
Underlying concept: Sorted tree-based dictionary
DSA relevance: ⭐⭐⭐⭐
Interview relevance: ⭐⭐⭐⭐

------------------------------------------------------------------------------------------------------------------------------------

1. Definition

SortedDictionary<TKey, TValue> is a generic key-value collection that stores elements according to the sorted order of their keys.

For example:

SortedDictionary<int, string> students = new()
{
    [103] = "Rahul",
    [101] = "Aman",
    [102] = "Priya"
};

When enumerated, the keys appear in sorted order:

101 → Aman
102 → Priya
103 → Rahul

The important difference from Dictionary<TKey,TValue> is:

Dictionary
    ↓
Fast key lookup
    ↓
No sorted-key requirement

SortedDictionary
    ↓
Key lookup + sorted keys
    ↓
Keys maintained in sorted order

------------------------------------------------------------------------------------------------------------------------------------
2. Generic or Non-Generic?

For your VS 2022 folders:

Collections
│
├── Generic
│   ├── List
│   ├── Dictionary
│   ├── HashSet
│   ├── SortedSet
│   ├── Stack
│   ├── Queue
│   ├── LinkedList
│   └── SortedDictionary
│
└── NonGeneric

So:

✅ SortedDictionary<TKey,TValue> is a generic collection.

------------------------------------------------------------------------------------------------------------------------------------
3. Why Do We Need It?

Suppose you have employee salaries:

Employee ID → Salary

103 → 75000
101 → 50000
105 → 90000
102 → 60000

If you use:

Dictionary<int, int>

you primarily care about key-based lookup.

But suppose the requirement is:

"Whenever I iterate through the employees, I want them ordered by employee ID."

That's where:

SortedDictionary<int, int>

is useful.

It maintains keys in sorted order.

------------------------------------------------------------------------------------------------------------------------------------

4. Real-Time Example

Imagine a leaderboard:

Player ID → Score
105 → 800
101 → 500
103 → 700
102 → 600

A SortedDictionary<int,int> enumerates by key:

101 → 500
102 → 600
103 → 700
105 → 800

Important: it sorts by the key, not by the value.

If you want sorting by score, SortedDictionary with player ID as the key is not the right direct abstraction.

------------------------------------------------------------------------------------------------------------------------------------

5. Basic Syntax
SortedDictionary<TKey, TValue> dictionary = new();

Example:

SortedDictionary<int, string> students = new();

Then:

students.Add(103, "Rahul");
students.Add(101, "Aman");
students.Add(102, "Priya");

Enumeration:

foreach (var student in students)
{
    Console.WriteLine(
        $"{student.Key} → {student.Value}");
}

Output:

101 → Aman
102 → Priya
103 → Rahul

------------------------------------------------------------------------------------------------------------------------------------
6. Key Characteristics

Remember these five points:

SortedDictionary<TKey,TValue>
        │
        ├── Key → Value
        ├── Unique keys
        ├── Keys sorted
        ├── No index access
        └── O(log n) basic operations

        ------------------------------------------------------------------------------------------------------------------------------------
7. Duplicate Keys

Like Dictionary<TKey,TValue>, keys must be unique.

This works:

students.Add(101, "Aman");

But:

students.Add(101, "Rahul");

throws:

ArgumentException

because key 101 already exists.

------------------------------------------------------------------------------------------------------------------------------------

8. Add()

Adds a key-value pair.

students.Add(101, "Aman");
students.Add(102, "Priya");

You cannot add a duplicate key.

students.Add(101, "Rahul");

throws an exception.

------------------------------------------------------------------------------------------------------------------------------------

9. Indexer — []

You can access a value by key:

Console.WriteLine(students[101]);

Output:

Aman

You can also update an existing value:

students[101] = "Rahul";

Now:

101 → Rahul

------------------------------------------------------------------------------------------------------------------------------------
10. Important Difference: Indexer and Add()

This is an interview favorite.

Add()
students.Add(101, "Aman");

If 101 already exists:

Exception
Indexer
students[101] = "Rahul";

If 101 exists:

Value updated

If 101 doesn't exist:

New key-value pair added

So:

Add()
   ↓
must be a new key

dictionary[key] = value
   ↓
add OR update

------------------------------------------------------------------------------------------------------------------------------------
11. ContainsKey()

Checks whether a key exists.

if (students.ContainsKey(101))
{
    Console.WriteLine("Student exists.");
}

Complexity:

O(log n)

------------------------------------------------------------------------------------------------------------------------------------
12. ContainsValue()

Checks whether a value exists.

students.ContainsValue("Aman");

Returns:

true

or:

false

Unlike key lookup, finding a value generally requires scanning the collection.

Complexity:

O(n)

------------------------------------------------------------------------------------------------------------------------------------
13. TryGetValue()

This is one of the most important methods.

Instead of:

if (students.ContainsKey(101))
{
    Console.WriteLine(students[101]);
}

prefer:

if (students.TryGetValue(101, out string? name))
{
    Console.WriteLine(name);
}

Why?

Because you're asking for the value in one operation rather than performing a separate existence check followed by indexing.

Complexity:

O(log n)

------------------------------------------------------------------------------------------------------------------------------------
14. Remove()

Removes a key-value pair by key.

students.Remove(101);

Returns:

true

if removed.

Otherwise:

false

------------------------------------------------------------------------------------------------------------------------------------
15. Remove() with Value Verification

There is also an overload that can remove only if both the key and value match.

Conceptually:

students.Remove(101, "Aman");

If:

101 → Aman

exists, it removes it.

If:

101 → Rahul

exists, it won't remove it.

This is useful when you want conditional removal.

------------------------------------------------------------------------------------------------------------------------------------

16. Clear()

Removes everything.

students.Clear();

Then:

students.Count

is:

0

------------------------------------------------------------------------------------------------------------------------------------
17. Count

Returns the number of key-value pairs.

Console.WriteLine(students.Count);

If there are five students:

5

------------------------------------------------------------------------------------------------------------------------------------
18. Keys

Gets the collection of keys.

foreach (int id in students.Keys)
{
    Console.WriteLine(id);
}

Because this is a SortedDictionary, the keys are enumerated in sorted order.

------------------------------------------------------------------------------------------------------------------------------------

19. Values

Gets the values.

foreach (string name in students.Values)
{
    Console.WriteLine(name);
}

The values are encountered in the order corresponding to the sorted keys.

Important: this does not mean the values themselves are sorted.

For example:

101 → "Zara"
102 → "Aman"
103 → "Rahul"

Values appear:

Zara
Aman
Rahul

not alphabetically.

------------------------------------------------------------------------------------------------------------------------------------

20. Comparer

You can inspect the comparer used to order the keys.

var comparer = students.Comparer;

By default, it uses the default comparer for the key type.

------------------------------------------------------------------------------------------------------------------------------------

21. Custom Sorting with IComparer<TKey>

This is where SortedDictionary becomes more interesting.

Suppose you want keys in descending order.

You can provide:

Comparer<int>.Create((x, y) => y.CompareTo(x))

Example:

var students =
    new SortedDictionary<int, string>(
        Comparer<int>.Create(
            (x, y) => y.CompareTo(x)));

Now:

103
102
101

instead of:

101
102
103

So the important principle is:

SortedDictionary doesn't necessarily mean ascending order; it means ordered according to its comparer.

------------------------------------------------------------------------------------------------------------------------------------

23. Reverse()

A SortedDictionary supports reverse enumeration through LINQ:

foreach (var item in students.Reverse())
{
    Console.WriteLine(item.Key);
}

This doesn't change the dictionary's ordering.

It creates a reversed enumeration.

------------------------------------------------------------------------------------------------------------------------------------

24. Important API Summary
Core
Add()
Remove()
Clear()
Count

--
Lookup
ContainsKey()
ContainsValue()
TryGetValue()

--
Access
dictionary[key]
Keys
Values
Ordering
Comparer

----
LINQ
Reverse()

------------------------------------------------------------------------------------------------------------------------------------
25. Time Complexity

This is extremely important for interviews.

| Operation           | Complexity |
| ------------------- | ---------: |
| `Add()`             |   O(log n) |
| `Remove()`          |   O(log n) |
| `ContainsKey()`     |   O(log n) |
| `TryGetValue()`     |   O(log n) |
| Indexer lookup      |   O(log n) |
| Update existing key |   O(log n) |
| `ContainsValue()`   |       O(n) |
| `Count`             |       O(1) |
| `Clear()`           |       O(n) |
| Enumeration         |       O(n) |


The reason for the O(log n) operations is that SortedDictionary<TKey,TValue> is implemented using a balanced tree structure.

More specifically, .NET implements it using a red-black tree.

------------------------------------------------------------------------------------------------------------------------------------

26. Internal Structure

This is the important DSA connection.

Conceptually:

                  50
                /    \
              30      70
             /  \    /  \
           20   40  60   80

The structure is kept balanced.

So searching doesn't normally require scanning every element.

Instead:

Search 60

50
 ↓
60 > 50
 ↓
go right

70
 ↓
60 < 70
 ↓
go left

60
 ↓
FOUND

Approximately:

O(log n)

------------------------------------------------------------------------------------------------------------------------------------
27. Dictionary vs SortedDictionary

This is one of the most important comparisons.

| Feature          | `Dictionary<TKey,TValue>` | `SortedDictionary<TKey,TValue>` |
| ---------------- | ------------------------- | ------------------------------- |
| Key-value        | ✅                         | ✅                               |
| Unique keys      | ✅                         | ✅                               |
| Sorted keys      | ❌                         | ✅                               |
| Average lookup   | O(1)                      | O(log n)                        |
| Add              | O(1) avg                  | O(log n)                        |
| Remove           | O(1) avg                  | O(log n)                        |
| Index access     | ❌                         | ❌                               |
| Range operations | Not naturally ordered     | ✅                               |
| Main advantage   | Fast lookup               | Sorted keys                     |


So:

If you don't need sorted keys, Dictionary is usually the better choice.

------------------------------------------------------------------------------------------------------------------------------------

28. SortedDictionary vs SortedList

This is a very important C# interview comparison.

Both maintain sorted keys.

But internally they're different.

SortedDictionary<TKey,TValue>

Uses:

Balanced tree
SortedList<TKey,TValue>

Uses:

Sorted arrays

So:

| Feature                         | SortedDictionary | SortedList    |
| ------------------------------- | ---------------- | ------------- |
| Internal structure              | Balanced tree    | Arrays        |
| Lookup                          | O(log n)         | O(log n)      |
| Insert                          | O(log n)         | O(n)          |
| Remove                          | O(log n)         | O(n)          |
| Memory overhead                 | Higher           | Lower         |
| Good for frequent modifications | ✅                | Less suitable |
| Good for compact storage        | Less             | ✅             |


This distinction is worth remembering.

------------------------------------------------------------------------------------------------------------------------------------

29. SortedDictionary vs SortedSet

You've already completed SortedSet<T>.

The difference is simple:

SortedSet<T>

Stores:

10
20
30

Only values.

SortedDictionary<TKey,TValue>

Stores:

10 → "A"
20 → "B"
30 → "C"

Key-value pairs.

Think:

SortedSet
    ↓
Sorted unique values

SortedDictionary
    ↓
Sorted unique keys + associated values

------------------------------------------------------------------------------------------------------------------------------------
30. Advantages
✅ Keys remain sorted

Useful when ordered traversal matters.

✅ O(log n) search

Predictable logarithmic performance.

✅ Range-based operations

GetViewBetween() can be useful.

✅ Custom key ordering

You can provide an IComparer<TKey>.

✅ No duplicate keys

Like Dictionary.

✅ Good for ordered key-value data

Especially when you need both lookup and sorted traversal.

------------------------------------------------------------------------------------------------------------------------------------

31. Disadvantages
❌ Slower lookup than Dictionary
Dictionary       → O(1) average
SortedDictionary → O(log n)
❌ More overhead

Tree nodes require additional structural information.

❌ No index access

You cannot do:

dictionary[0]

as an index.

That means dictionary[0] means:

"Give me the value whose key is 0."

It does not mean:

"Give me the first element."

------------------------------------------------------------------------------------------------------------------------------------

32. When Should You Use It?

Use SortedDictionary<TKey,TValue> when you need:

Key-value mapping
+
Sorted keys
+
Frequent lookup/insertion/removal

Examples:

Event scheduling
Timestamp → Event
Price ranges
Price → Product information
Score thresholds
Score → Description
Ordered configuration
Priority → Configuration
Range queries
ID → Record

where you frequently care about a key range.

------------------------------------------------------------------------------------------------------------------------------------

33. When Should You NOT Use It?
Only need fast lookup?

Use:

Dictionary<TKey,TValue>
Need unique sorted values but no associated value?

Use:

SortedSet<T>
Need index-oriented sorted data?

Consider:

SortedList<TKey,TValue>
Need FIFO processing?

Use:

Queue<T>

------------------------------------------------------------------------------------------------------------------------------------
34. Real-Time Example — Event Scheduler

Imagine:

Timestamp → Event
10:00 → Meeting
09:00 → Login
11:00 → Lunch
08:30 → System startup

With a SortedDictionary, enumeration gives:

08:30 → System startup
09:00 → Login
10:00 → Meeting
11:00 → Lunch

This is useful because the key itself represents ordering.

------------------------------------------------------------------------------------------------------------------------------------

35. DSA Connection

SortedDictionary is useful for understanding:

Balanced trees
     ↓
Binary search tree concepts
     ↓
Red-black trees
     ↓
O(log n) operations

So although you won't necessarily use SortedDictionary in every DSA problem, understanding 
its behavior strengthens your understanding of ordered tree-based data structures.

------------------------------------------------------------------------------------------------------------------------------------

36. Interview Question
Q: What is the difference between Dictionary and SortedDictionary?

Strong answer:

Dictionary<TKey,TValue> is hash-based and provides average O(1) lookup, while SortedDictionary<TKey,TValue> 
maintains keys in sorted order using a balanced tree and provides O(log n) lookup, insertion, and removal.

------------------------------------------------------------------------------------------------------------------------------------

37. Interview Question
Q: Why is SortedDictionary O(log n)?

Because its keys are stored in a balanced tree structure.

The tree height remains approximately logarithmic relative to the number of elements.

Therefore:

Search → O(log n)
Insert → O(log n)
Delete → O(log n)

------------------------------------------------------------------------------------------------------------------------------------
38. Interview Question
Q: Does SortedDictionary sort by values?

No.

It sorts by:

keys

If you have:

101 → "Zara"
102 → "Aman"
103 → "Rahul"

the keys determine ordering:

101
102
103

The values aren't independently sorted.

------------------------------------------------------------------------------------------------------------------------------------

39. Interview Question
Q: Can SortedDictionary contain duplicate keys?

No.

dictionary.Add(101, "Aman");
dictionary.Add(101, "Rahul");

throws an exception.

------------------------------------------------------------------------------------------------------------------------------------

40. Interview Question
Q: How can you change the sorting order?

Provide a custom:

IComparer<TKey>

For example, descending integers:

var dictionary =
    new SortedDictionary<int, string>(
        Comparer<int>.Create(
            (x, y) => y.CompareTo(x)));

            ------------------------------------------------------------------------------------------------------------------------------------
41. Interview Question
Q: Is SortedDictionary always better than Dictionary because it's sorted?

Absolutely not.

That's a trap.

If you don't need sorted keys:

Dictionary

is generally preferable because its average lookup is:

O(1)

instead of:

O(log n)

Choose based on requirements, not because one collection sounds more powerful.

------------------------------------------------------------------------------------------------------------------------------------

42. Interview Question
Q: What is the difference between SortedDictionary and SortedList?

A strong answer:

Both maintain sorted keys and provide O(log n) lookup. SortedDictionary uses a tree-based structure and supports O(log n) 
insertion and removal, while SortedList uses arrays, making insertion and removal generally O(n), but it can have lower memory overhead and better compactness.

------------------------------------------------------------------------------------------------------------------------------------

43. Common Mistake

Don't say:

"SortedDictionary gives O(1) lookup because it is a dictionary."

That's wrong.

Correct:

Dictionary
→ O(1) average

SortedDictionary
→ O(log n)

------------------------------------------------------------------------------------------------------------------------------------
44. Another Common Mistake

Don't assume:

dictionary[0]

means the first item.

It means:

Find the element whose key is 0.

SortedDictionary has no positional indexer.



