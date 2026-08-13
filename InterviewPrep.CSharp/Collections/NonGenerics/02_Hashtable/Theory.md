Type: ❌ Non-generic
Namespace: System.Collections
Modern replacement: Dictionary<TKey,TValue>
Product-company relevance: ⭐⭐⭐⭐ — mainly for legacy code and interview comparison

-----------------------------------------------------------------------------------------------------

1. Definition

Hashtable is a non-generic key-value collection that stores keys and values as object and uses hashing to provide fast key-based lookup.

Example:

Hashtable employees = new();

employees.Add(101, "Aman");
employees.Add(102, "Priya");

Conceptually:

Key → Value

101 → Aman
102 → Priya
2. Why Was Hashtable Created?

Hashtable existed before generics.

Later, .NET introduced:

Dictionary<TKey,TValue>

which provides:

compile-time type safety
less casting
better handling of value types
clearer APIs

So today:

Hashtable
    ↓
Legacy

Dictionary<TKey,TValue>
    ↓
Modern preferred choice
3. Basic Syntax
using System.Collections;

Hashtable employees = new();

Adding:

employees.Add(101, "Aman");
employees.Add(102, "Priya");
4. Key Characteristics

Remember these:

Hashtable
   │
   ├── Non-generic
   ├── Key → Value
   ├── Keys must be unique
   ├── Uses hashing
   ├── Keys/values are object
   └── Not sorted

Unlike SortedDictionary:

Hashtable
    ↓
No sorted-key guarantee
5. Add()
employees.Add(101, "Aman");

Duplicate key:

employees.Add(101, "Rahul");

throws an exception.

Keys must be unique.

6. Indexer

You can retrieve a value using its key:

Console.WriteLine(
    employees[101]);

Output:

Aman

You can also add/update:

employees[101] = "Rahul";

If the key exists:

Update

If it doesn't:

Add
7. ContainsKey()

Checks whether a key exists.

if (employees.ContainsKey(101))
{
    Console.WriteLine("Employee exists");
}
8. ContainsValue()

Checks whether a value exists:

employees.ContainsValue("Aman");

This is generally slower than key lookup because values aren't hashed for direct lookup.

9. Remove()

Remove by key:

employees.Remove(101);
10. Clear()

Remove everything:

employees.Clear();
11. Count
Console.WriteLine(
    employees.Count);

Returns the number of key-value pairs.

12. Keys
foreach (object key in employees.Keys)
{
    Console.WriteLine(key);
}
13. Values
foreach (object value in employees.Values)
{
    Console.WriteLine(value);
}

Remember:

Hashtable does not guarantee sorted order.

14. Contains() / ContainsKey()

You may encounter:

employees.Contains(101);

and:

employees.ContainsKey(101);

For clarity, prefer:

ContainsKey()

when your intention is specifically checking a key.

15. Type Casting

This is an important disadvantage.

Suppose:

Hashtable employees = new();

employees.Add(101, "Aman");

When retrieving:

string name =
    (string)employees[101];

You need a cast.

With:

Dictionary<int, string>

you don't:

string name =
    employees[101];
16. Boxing / Unboxing

Suppose:

Hashtable salaries = new();

salaries.Add(101, 50000);

50000 is an int.

Because Hashtable stores objects, the value can undergo boxing:

int
 ↓
boxing
 ↓
object

Retrieving it:

int salary =
    (int)salaries[101];

requires unboxing:

object
 ↓
unboxing
 ↓
int

Again, this is one reason generic collections are preferred.

17. Hashtable vs Dictionary

This is the most important interview comparison.

| Feature        | `Hashtable`          | `Dictionary<TKey,TValue>`    |
| -------------- | -------------------- | ---------------------------- |
| Generic        | ❌                    | ✅                            |
| Namespace      | `System.Collections` | `System.Collections.Generic` |
| Type-safe      | ❌                    | ✅                            |
| Key/value type | `object`             | Strongly typed               |
| Casting        | Often required       | Usually not                  |
| Boxing         | Can occur            | Avoided for value-type `T`   |
| Sorted         | ❌                    | ❌                            |
| Hash-based     | ✅                    | ✅                            |
| Modern choice  | ❌                    | ✅                            |

Example:

Hashtable
Hashtable data = new();

data.Add(101, "Aman");

string name =
    (string)data[101];
Dictionary
Dictionary<int, string> data = new();

data.Add(101, "Aman");

string name =
    data[101];

The generic version is safer and cleaner.

18. Internal Working

Conceptually, both use hashing:

Key
 ↓
Hash function
 ↓
Hash code
 ↓
Bucket
 ↓
Stored entry
 ↓
Value

For example:

101
 ↓
Hash
 ↓
Some hash code
 ↓
Bucket
 ↓
"Aman"

This is the same general hashing concept you've already studied with Dictionary<TKey,TValue>.

The important interview point is:

Hashtable is hash-based, but it is not sorted.

19. Time Complexity

For normal key operations, think in terms of average-case hash-table behavior:

| Operation     | Average |
| ------------- | ------: |
| Add           |    O(1) |
| Lookup        |    O(1) |
| Remove        |    O(1) |
| ContainsKey   |    O(1) |
| ContainsValue |    O(n) |
| Count         |    O(1) |


Worst-case hash collisions can degrade performance.

For interviews, say:

Average O(1), worst case can degrade because of collisions.

20. Advantages
✅ Fast average key lookup
O(1)
✅ Hash-based

Good for key-value lookup.

✅ Useful for legacy .NET code

You may encounter it in older systems.

21. Disadvantages
❌ Non-generic

Everything is essentially handled as object.

❌ No compile-time type safety

Different types can be inserted.

❌ Casting required

Retrieval often requires explicit casts.

❌ Boxing/unboxing

Can occur with value types.

❌ Not sorted

If you need sorted keys, use an appropriate sorted collection.

❌ Not recommended for new code

Prefer:

Dictionary<TKey,TValue>
22. When Should You Use It?

For new development:

Generally, don't.

You mainly need it when:

maintaining legacy applications
working with old APIs
reading existing .NET code
answering interview questions
23. When Should You NOT Use It?

For new C# code, use:

Dictionary<TKey,TValue>

For example:

❌:

Hashtable employees = new();

✅:

Dictionary<int, Employee> employees = new();
24. Interview Questions
Q1. What is Hashtable?

A non-generic hash-based key-value collection that stores keys and values as objects.

Q2. What is the modern replacement?

Dictionary<TKey,TValue>.

Q3. Hashtable vs Dictionary?

Hashtable is non-generic and object-based, while Dictionary<TKey,TValue> is generic and strongly typed.

Q4. Is Hashtable sorted?

No.

Q5. What is the average lookup complexity?

O(1).

Q6. Why can Hashtable cause boxing?

Because value types are stored as object.

Q7. Can Hashtable have duplicate keys?

No.

Q8. Does Hashtable provide compile-time type safety?

No.

