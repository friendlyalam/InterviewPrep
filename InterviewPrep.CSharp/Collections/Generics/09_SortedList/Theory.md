Collection: ✅ Generic
Namespace: System.Collections.Generic
Category: Key-value collection
Internal structure: Sorted arrays
DSA relevance: ⭐⭐⭐⭐
Interview relevance: ⭐⭐⭐⭐⭐

------------------------------------------------------------------------------------------------------------------------------------

1. Definition

SortedList<TKey,TValue> is a generic key-value collection that maintains its elements sorted by key and stores the keys and values in arrays.

Example:

SortedList<int, string> students = new()
{
    [103] = "Rahul",
    [101] = "Aman",
    [102] = "Priya"
};

When enumerated:

101 → Aman
102 → Priya
103 → Rahul

The important thing is:

The keys are kept sorted.

------------------------------------------------------------------------------------------------------------------------------------

2. Generic or Non-Generic?

For your VS 2022 folders:

Collections
│
├── Generic
│   ├── List<T>
│   ├── Dictionary<TKey,TValue>
│   ├── HashSet<T>
│   ├── SortedSet<T>
│   ├── Stack<T>
│   ├── Queue<T>
│   ├── LinkedList<T>
│   ├── SortedDictionary<TKey,TValue>
│   └── SortedList<TKey,TValue>
│
└── NonGeneric

So:

✅ SortedList<TKey,TValue> is a generic collection.

Don't confuse it with the old non-generic:

System.Collections.SortedList

These are different types.

------------------------------------------------------------------------------------------------------------------------------------

3. Why Do We Need SortedList?

Imagine you have:

Product ID → Product Name
105 → Laptop
101 → Mouse
103 → Keyboard
102 → Monitor

You want:

key-value mapping
sorted keys
relatively compact storage
fast key lookup

SortedList<TKey,TValue> is designed for this type of scenario.

It maintains:

101 → Mouse
102 → Monitor
103 → Keyboard
105 → Laptop

------------------------------------------------------------------------------------------------------------------------------------
4. Internal Structure

This is the most important concept.

Unlike SortedDictionary, which is tree-based, SortedList<TKey,TValue> uses two arrays conceptually:

Keys:
┌─────┬─────┬─────┬─────┐
│ 101 │ 102 │ 103 │ 105 │
└─────┴─────┴─────┴─────┘

Values:
┌────────┬─────────┬──────────┬────────┐
│ Mouse  │ Monitor │ Keyboard │ Laptop │
└────────┴─────────┴──────────┴────────┘

The positions correspond:

Key       Value
101   →   Mouse
102   →   Monitor
103   →   Keyboard
105   →   Laptop

This is fundamentally different from:

Dictionary
     ↓
Hash table

SortedDictionary
     ↓
Balanced tree

SortedList
     ↓
Sorted arrays

------------------------------------------------------------------------------------------------------------------------------------
5. Why Is This Important?

Because the internal structure explains the performance.

Lookup

The keys are sorted, so binary search can be used:

O(log n)
Insertion

Suppose:

101
103
105

You insert:

102

The array has to shift:

Before:

101  103  105

After:

101  102  103  105
       ↑
    inserted

Therefore insertion can require shifting elements:

O(n)
Removal

Removing from the middle also requires shifting:

O(n)

This is the biggest performance difference from SortedDictionary.

------------------------------------------------------------------------------------------------------------------------------------

6. SortedList vs SortedDictionary

This is one of the most important C# interview comparisons.

| Feature                         | `SortedList<TKey,TValue>` | `SortedDictionary<TKey,TValue>` |
| ------------------------------- | ------------------------- | ------------------------------- |
| Sorted keys                     | ✅                         | ✅                               |
| Internal structure              | Arrays                    | Balanced tree                   |
| Lookup                          | O(log n)                  | O(log n)                        |
| Insert                          | O(n)                      | O(log n)                        |
| Remove                          | O(n)                      | O(log n)                        |
| Memory overhead                 | Lower                     | Higher                          |
| Random key lookup               | O(log n)                  | O(log n)                        |
| Good for frequent modifications | ❌                         | ✅                               |
| Compact storage                 | ✅                         | Less compact                    |

So:

SortedList is good when the collection doesn't change frequently and you want compact storage plus sorted lookup.

------------------------------------------------------------------------------------------------------------------------------------

7. SortedList vs Dictionary

| Feature        | `Dictionary`        | `SortedList`                   |
| -------------- | ------------------- | ------------------------------ |
| Key-value      | ✅                   | ✅                              |
| Unique keys    | ✅                   | ✅                              |
| Sorted keys    | ❌                   | ✅                              |
| Average lookup | O(1)                | O(log n)                       |
| Insert         | O(1) average        | O(n)                           |
| Remove         | O(1) average        | O(n)                           |
| Memory         | Hash-table overhead | More compact                   |
| Best for       | Fast lookup         | Sorted, relatively stable data |


If you don't need sorted keys:

Prefer Dictionary<TKey,TValue> in many situations.

------------------------------------------------------------------------------------------------------------------------------------

8. Basic Syntax
SortedList<int, string> students = new();

Add:

students.Add(103, "Rahul");
students.Add(101, "Aman");
students.Add(102, "Priya");

Enumeration:

foreach (var student in students)
{
    Console.WriteLine(
        $"{student.Key} -> {student.Value}");
}

Output:

101 -> Aman
102 -> Priya
103 -> Rahul

------------------------------------------------------------------------------------------------------------------------------------
9. Add()

Adds a key-value pair.

students.Add(101, "Aman");

Duplicate keys are not allowed:

students.Add(101, "Rahul");

This throws:

ArgumentException

------------------------------------------------------------------------------------------------------------------------------------
10. Indexer []

You can retrieve a value by key:

Console.WriteLine(students[101]);

You can also update:

students[101] = "Arjun";

And if the key doesn't exist:

students[104] = "Sara";

it adds a new key-value pair.

So:

Add(key,value)
     ↓
expects new key

list[key] = value
     ↓
add OR update

------------------------------------------------------------------------------------------------------------------------------------
11. ContainsKey()

Checks whether a key exists.

bool exists = students.ContainsKey(101);

Returns:

true

or:

false

Complexity:

O(log n)

because the sorted keys can be searched using binary search.

------------------------------------------------------------------------------------------------------------------------------------

12. ContainsValue()

Checks whether a value exists.

students.ContainsValue("Aman");

Returns:

true

or:

false

Complexity:

O(n)

because the values aren't independently sorted.

------------------------------------------------------------------------------------------------------------------------------------

13. TryGetValue()

Safely retrieves a value by key.

if (students.TryGetValue(
        101,
        out string? name))
{
    Console.WriteLine(name);
}

This avoids an exception if the key doesn't exist.

Complexity:

O(log n)
14. Remove(key)

Removes the element associated with a key.

students.Remove(101);

Returns:

true

if the key existed.

Otherwise:

false

Important:

SortedList<TKey,TValue>.Remove() takes the key, not a key-value pair.

------------------------------------------------------------------------------------------------------------------------------------

15. Clear()

Removes all elements.

students.Clear();

Then:

students.Count

returns:

0

------------------------------------------------------------------------------------------------------------------------------------
16. Count

Returns the number of key-value pairs.

Console.WriteLine(students.Count);

Complexity:

O(1)

------------------------------------------------------------------------------------------------------------------------------------
17. Keys

Gets the keys.

foreach (int key in students.Keys)
{
    Console.WriteLine(key);
}

They appear in sorted order.

------------------------------------------------------------------------------------------------------------------------------------

18. Values

Gets the values.

foreach (string value in students.Values)
{
    Console.WriteLine(value);
}

The values follow the ordering of their corresponding keys.

But remember:

The values themselves are not sorted.

------------------------------------------------------------------------------------------------------------------------------------

19. IndexOfKey()

This is an interesting SortedList-specific capability.

int index = students.IndexOfKey(102);

If the sorted list contains:

101
102
103

then:

IndexOfKey(102)

returns:

1

because the key is at array position 1.

If the key doesn't exist:

-1

This operation is:

O(log n)

because the keys are sorted.

------------------------------------------------------------------------------------------------------------------------------------

20. IndexOfValue()

Finds the index of a value.

int index =
    students.IndexOfValue("Priya");

If:

101 → Aman
102 → Priya
103 → Rahul

result:

1

Important:

Value lookup is linear.

Complexity:

O(n)

------------------------------------------------------------------------------------------------------------------------------------
21. GetKeyAtIndex()

This is a major difference from Dictionary and SortedDictionary.

You can retrieve the key at a particular sorted position:

int key =
    students.Keys[0];

Or use:

int key =
    students.GetKeyAtIndex(0);

For:

101 → Aman
102 → Priya
103 → Rahul

you get:

101

This operation is:

O(1)

because the underlying keys are stored in an array.

------------------------------------------------------------------------------------------------------------------------------------

22. GetValueAtIndex()

Similarly:

string value =
    students.GetValueAtIndex(0);

returns the value at sorted index 0.

For:

101 → Aman
102 → Priya
103 → Rahul

result:

Aman

Complexity:

O(1)

This is an important advantage of SortedList.

------------------------------------------------------------------------------------------------------------------------------------

23. RemoveAt()

You can remove by sorted index.

students.RemoveAt(1);

Suppose:

Index   Key
0       101
1       102
2       103

RemoveAt(1) removes:

102

However, because the arrays need to be shifted:

O(n)

------------------------------------------------------------------------------------------------------------------------------------
24. SetKeyAtIndex()

This is a more specialized API.

students.SetKeyAtIndex(index, key);

It changes the key at the specified index.

But there are important restrictions because the collection must remain correctly sorted and keys must remain unique.

This is not a method you should casually use in normal application code.

For most code, prefer:

Add()
Remove()
indexer

rather than manipulating keys by index.

------------------------------------------------------------------------------------------------------------------------------------

25. SetValueAtIndex()

You can update the value at a particular sorted index:

students.SetValueAtIndex(
    1,
    "Updated Name");

Unlike changing a key, changing a value doesn't affect key ordering.

Complexity:

O(1)

------------------------------------------------------------------------------------------------------------------------------------
27. TrimExcess()

Reduces unused capacity where possible.

students.TrimExcess();

Don't call this repeatedly after every removal.

It's an optimization operation.

------------------------------------------------------------------------------------------------------------------------------------

28. Capacity

SortedList<TKey,TValue> exposes its internal capacity:

Console.WriteLine(
    students.Capacity);

This represents the allocated capacity of its internal arrays, not the number of elements.

So:

Count
  ↓
actual elements

Capacity
  ↓
allocated storage

------------------------------------------------------------------------------------------------------------------------------------
29. Comparer

You can inspect the comparer used to order keys.

var comparer = students.Comparer;

By default, it uses the default comparer for TKey.

You can also supply a custom comparer when creating the collection.

------------------------------------------------------------------------------------------------------------------------------------

30. Descending Order

Suppose you want:

103
102
101

instead of:

101
102
103

Use a comparer:

var students =
    new SortedList<int, string>(
        Comparer<int>.Create(
            (x, y) => y.CompareTo(x)));

Then:

students.Add(101, "Aman");
students.Add(103, "Rahul");
students.Add(102, "Priya");

enumerates as:

103 → Rahul
102 → Priya
101 → Aman

------------------------------------------------------------------------------------------------------------------------------------
31. Complete Important API
Adding / updating
Add()
this[key]
SetValueAtIndex()
SetKeyAtIndex()

--
Searching
ContainsKey()
ContainsValue()
TryGetValue()
IndexOfKey()
IndexOfValue()

--
Removing
Remove(key)
RemoveAt(index)
Clear()

--
Accessing
Keys
Values
GetKeyAtIndex()
GetValueAtIndex()
Information / storage
Count
Capacity
Comparer
TrimExcess()

------------------------------------------------------------------------------------------------------------------------------------
32. Time Complexity

Here's the interview cheat sheet:

| Operation             | Complexity |
| --------------------- | ---------: |
| `Add()`               |       O(n) |
| `Remove(key)`         |       O(n) |
| `RemoveAt()`          |       O(n) |
| `ContainsKey()`       |   O(log n) |
| `TryGetValue()`       |   O(log n) |
| Indexer lookup by key |   O(log n) |
| `ContainsValue()`     |       O(n) |
| `IndexOfKey()`        |   O(log n) |
| `IndexOfValue()`      |       O(n) |
| `GetKeyAtIndex()`     |       O(1) |
| `GetValueAtIndex()`   |       O(1) |
| `SetValueAtIndex()`   |       O(1) |
| `Count`               |       O(1) |
| `Clear()`             |       O(n) |
| Enumeration           |       O(n) |


The big three to remember:

Key lookup       → O(log n)
Index access     → O(1)
Insert/remove    → O(n)

------------------------------------------------------------------------------------------------------------------------------------
33. Why Is Insertion O(n)?

Suppose:

101
103
105

Insert:

102

The arrays must become:

101
102
103
105

So 103 and 105 have to move.

Conceptually:

Before:

[101][103][105]

Insert 102:

[101][102][103][105]
       ↑
      new

Therefore:

O(n)

in the worst case.

------------------------------------------------------------------------------------------------------------------------------------

34. Why Is Key Lookup O(log n)?

Because keys are sorted:

101 102 103 104 105 106 107

Binary search can be used.

For example, search for 106:

Middle → 104
106 > 104
     ↓
Search right half

106
 ↓
Found

So:

O(log n)

------------------------------------------------------------------------------------------------------------------------------------
35. Why Is Index Access O(1)?

Because internally it's array-based.

If you request:

students.GetValueAtIndex(5);

the collection can directly access the underlying array position.

Conceptually:

array[5]

So:

O(1)

This is something SortedDictionary doesn't provide.

------------------------------------------------------------------------------------------------------------------------------------

36. SortedList vs SortedDictionary — Interview Answer

If asked:

"When would you choose SortedList over SortedDictionary?"

A strong answer:

I would choose SortedList<TKey,TValue> when the collection is relatively stable, I need sorted keys, 
and I benefit from compact array-based storage or indexed access. 
I would choose SortedDictionary<TKey,TValue> when insertions and removals are frequent because its tree
structure provides O(log n) insertion and deletion instead of O(n).

That's a very strong interview answer.

------------------------------------------------------------------------------------------------------------------------------------
37. Advantages
✅ Sorted keys

Always maintained according to the comparer.

✅ Binary-search key lookup
O(log n)
✅ O(1) access by sorted index

This is a major advantage.

✅ Lower memory overhead than tree-based structures

Arrays are generally more compact than individual tree nodes.

✅ Good cache locality

Arrays are contiguous, which can be beneficial for CPU cache behavior.

✅ Good for relatively static data

If you don't frequently insert/remove elements, it can be very effective.

------------------------------------------------------------------------------------------------------------------------------------

38. Disadvantages
❌ Insertions can be O(n)

Elements may need to shift.

❌ Removals can be O(n)

Again, elements may need to shift.

❌ Not ideal for frequent modifications

If data changes constantly, SortedDictionary may be a better choice.

❌ Key lookup slower than Dictionary
Dictionary       → O(1) average
SortedList       → O(log n)
❌ Values aren't sorted independently

Sorting is based on keys.

------------------------------------------------------------------------------------------------------------------------------------

39. When Should You Use SortedList?

Good scenario:

Data changes rarely
       +
Need sorted keys
       +
Need fast lookup
       +
Need compact storage

Examples:

Configuration data
Setting ID → Setting

where data is loaded once and read frequently.

Static lookup tables
Code → Description

where keys need to remain sorted.

Small/medium relatively stable datasets

Especially when indexed access to sorted positions is useful.

------------------------------------------------------------------------------------------------------------------------------------

40. When Should You NOT Use It?
Frequent insertions/deletions

Consider:

SortedDictionary
No sorting required

Consider:

Dictionary
Only unique values

Consider:

SortedSet
Need LIFO
Stack
Need FIFO
Queue

------------------------------------------------------------------------------------------------------------------------------------
41. DSA Connection

SortedList is particularly useful for understanding:

Sorted arrays
     ↓
Binary search
     ↓
O(log n) lookup
     ↓
Array shifting
     ↓
O(n) insertion/removal

This is directly relevant to DSA.

You should recognize the tradeoff:

Array-based
     ↓
Fast random/index access
     ↓
Good cache locality
     ↓
Expensive insertion/removal

versus:

Tree-based
     ↓
No direct index access
     ↓
O(log n) modifications

------------------------------------------------------------------------------------------------------------------------------------
42. Interview Questions
Q1. What is SortedList<TKey,TValue>?

A generic key-value collection that maintains keys in sorted order and internally uses arrays for keys and values.

Q2. What is the difference between SortedList and SortedDictionary?

SortedList is array-based, while SortedDictionary is tree-based. Both provide O(log n) key lookup, 
but SortedList generally has O(n) insertion/removal while SortedDictionary provides O(log n) insertion/removal.

Q3. Why is SortedList insertion O(n)?

Because maintaining sorted order can require shifting keys and values in the underlying arrays.

Q4. Why is lookup O(log n)?

Because the sorted key array can be searched using binary search.

Q5. Does SortedList support index access?

Yes, but be precise.

It provides methods such as:

GetKeyAtIndex()
GetValueAtIndex()

and its Keys/Values collections support indexed access.

This is sorted-position access, not dictionary-key access.

Q6. Does SortedDictionary support the same indexed access?

No.

That's one of the distinctions between them.

Q7. Does SortedList sort by value?

No.

It sorts by:

KEY
Q8. Can SortedList contain duplicate keys?

No.

Keys must be unique.

Q9. What happens when you use:
list[key] = value;

If the key exists:

Update

If it doesn't:

Add
Q10. When would Dictionary be better?

When sorted keys aren't required and fast average key lookup is more important.

------------------------------------------------------------------------------------------------------------------------------------

43. Common Interview Trap

Don't say:

"SortedList is faster than SortedDictionary because arrays are faster."

That's too broad.

The correct answer depends on the operation.

Lookup:
SortedList       O(log n)
SortedDictionary O(log n)

Insert:
SortedList       O(n)
SortedDictionary O(log n)

Remove:
SortedList       O(n)
SortedDictionary O(log n)

Index access:
SortedList       O(1)
SortedDictionary Not supported

That's the comparison interviewers actually want.

