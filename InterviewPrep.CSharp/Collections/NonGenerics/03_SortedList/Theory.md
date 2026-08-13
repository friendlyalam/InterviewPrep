Type: ❌ Non-generic
Namespace: System.Collections
Modern equivalent: SortedList<TKey,TValue>
Product-company relevance: ⭐⭐⭐ — mostly comparison/legacy knowledge

⚠️ Important: This is System.Collections.SortedList, not the generic SortedList<TKey,TValue> we just studied.

----------------------------------------------------------------------------------------------------------------

1. Definition

System.Collections.SortedList is a non-generic key-value collection that maintains its entries sorted by key and stores them as object.

Example:

SortedList data = new();

data.Add(103, "Rahul");
data.Add(101, "Aman");
data.Add(102, "Priya");

Enumeration is sorted by key:

101 → Aman
102 → Priya
103 → Rahul
2. Why Does It Exist?

It's the older, non-generic version of the same general idea.

System.Collections.SortedList
             ↓
        non-generic
             ↓
       object/object

Modern C#:

SortedList<TKey,TValue>
             ↓
          generic
             ↓
       strongly typed
3. Basic Syntax
using System.Collections;

SortedList students = new();

Add:

students.Add(101, "Aman");
students.Add(102, "Priya");
4. Important Methods/Properties

For product-company interviews, these are enough.

Add()
students.Add(101, "Aman");

Keys must be unique.

Indexer
Console.WriteLine(students[101]);

Update:

students[101] = "Arjun";
ContainsKey()
students.ContainsKey(101);
ContainsValue()
students.ContainsValue("Aman");
Remove()
students.Remove(101);
RemoveAt()
students.RemoveAt(0);

Removes the item at the specified sorted index.

IndexOfKey()
int index =
    students.IndexOfKey(102);
IndexOfValue()
int index =
    students.IndexOfValue("Priya");
GetKey()
object key =
    students.GetKey(0);

Gets the key at a particular index.

GetByIndex()
object value =
    students.GetByIndex(0);

Gets the value at a particular index.

SetByIndex()
students.SetByIndex(
    0,
    "Updated");
Count
Console.WriteLine(students.Count);
Keys
foreach (object key in students.Keys)
{
    Console.WriteLine(key);
}
Values
foreach (object value in students.Values)
{
    Console.WriteLine(value);
}
Clear()
students.Clear();
5. Internal Idea

Like the generic SortedList<TKey,TValue>, it is array-based and maintains sorted keys.

Conceptually:

Keys:
101  102  103

Values:
Aman Priya Rahul

Therefore:

Key lookup       → O(log n)
Index access      → O(1)
Insert/remove     → O(n)

The object storage introduces the same legacy concerns we saw with ArrayList and Hashtable.

6. SortedList vs Hashtable

Very important:

| Feature            | `Hashtable` | Non-generic `SortedList` |
| ------------------ | ----------- | ------------------------ |
| Hash-based         | ✅           | ❌                        |
| Sorted keys        | ❌           | ✅                        |
| Average key lookup | O(1)        | O(log n)                 |
| Index access       | ❌           | ✅                        |
| Generic            | ❌           | ❌                        |


So:

Hashtable
   ↓
Fast hash lookup
   ↓
No ordering

SortedList
   ↓
Sorted keys
   ↓
Binary-search lookup
7. Non-Generic vs Generic SortedList

| Feature       | `System.Collections.SortedList` | `SortedList<TKey,TValue>` |
| ------------- | ------------------------------- | ------------------------- |
| Generic       | ❌                               | ✅                         |
| Type-safe     | ❌                               | ✅                         |
| Stores        | `object`                        | Strongly typed            |
| Casting       | Often required                  | Usually unnecessary       |
| Boxing        | Can occur                       | Avoided for value-type T  |
| Sorted        | ✅                               | ✅                         |
| Modern choice | ❌                               | ✅                         |

For new C# code:

SortedList<int, string>

is preferred.

8. Advantages
Maintains sorted keys.
Binary-search key lookup.
Supports indexed access.
Useful when reading legacy .NET code.
9. Disadvantages
Non-generic.
No compile-time type safety.
Casting required.
Boxing/unboxing can occur.
Insert/remove can be O(n).
Modern code should generally use the generic version.
10. When to Use?

Mostly:

Legacy application
        ↓
Existing System.Collections.SortedList

For new applications:

Prefer SortedList<TKey,TValue> or another appropriate generic collection.

11. Interview Questions
Q1. Is System.Collections.SortedList generic?

No.

Q2. Is it sorted?

Yes, by key.

Q3. How is it different from Hashtable?

Hashtable uses hashing and provides average O(1) key lookup; SortedList maintains sorted keys and generally uses O(log n) key lookup.

Q4. Modern replacement?
System.Collections.SortedList
          ↓
SortedList<TKey,TValue>
Q5. Can it contain duplicate keys?

No.

Q6. Why is insertion O(n)?

Because maintaining sorted order can require shifting array elements.

