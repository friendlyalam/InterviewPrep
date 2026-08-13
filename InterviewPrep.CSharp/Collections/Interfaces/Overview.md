C# Collection Interfaces — Quick Reference

Classification: Generic collection interfaces
Purpose: DSA + product-company interviews + API design

1. IEnumerable<T>

Definition: Represents a sequence that can be iterated using foreach.

IEnumerable<int> numbers = new List<int> { 1, 2, 3 };

Key member:

GetEnumerator()

Use when: You only need to read/iterate through a sequence.

Don't use when: You need Add, Remove, or indexing.

2. ICollection<T>

Definition: Represents a collection of elements with basic modification and counting operations.

ICollection<int> numbers = new List<int>();

Important members:

Count
Add()
Remove()
Clear()
Contains()
CopyTo()
IsReadOnly

Use when: You need basic collection manipulation but don't care about indexing.

3. IList<T>

Definition: Represents an ordered collection accessible by index.

IList<int> numbers = new List<int>();

Important members:

this[index]
IndexOf()
Insert()
RemoveAt()
Add()
Remove()
Count

Use when: You need index-based access + modification.

4. IReadOnlyCollection<T>

Definition: Represents a collection that can only be read and exposes its count.

IReadOnlyCollection<int> numbers =
    new List<int> { 1, 2, 3 };

Important members:

Count
foreach

Use when: A method should expose a collection without allowing callers to modify it through the interface.

5. IReadOnlyList<T>

Definition: Read-only collection with index-based access.

IReadOnlyList<int> numbers =
    new List<int> { 10, 20, 30 };

Access:

int x = numbers[1];

Important members:

Count
this[index]
IndexOf()

Use when: Caller needs read + index, but shouldn't modify the collection.

6. ISet<T>

Definition: Represents a collection containing unique elements.

ISet<int> numbers = new HashSet<int>();

Important operations:

Add()
Remove()
Contains()
UnionWith()
IntersectWith()
ExceptWith()
IsSubsetOf()
IsSupersetOf()

Use when: Uniqueness and set operations matter.

7. IDictionary<TKey,TValue>

Definition: Represents a collection of unique keys mapped to values.

IDictionary<int, string> employees =
    new Dictionary<int, string>();

Important members:

Keys
Values
Count
this[key]
Add()
Remove()
ContainsKey()
TryGetValue()

Use when: You need key → value lookup.

8. IReadOnlyDictionary<TKey,TValue>

Definition: Read-only key-value collection.

IReadOnlyDictionary<int, string> employees =
    new Dictionary<int, string>();

Important members:

Keys
Values
Count
this[key]
ContainsKey()
TryGetValue()

Use when: Exposing dictionary data without allowing modification through the API.

9. IComparer<T>

Definition: Defines how two objects are compared.

class AgeComparer : IComparer<int>
{
    public int Compare(int x, int y)
        => x.CompareTo(y);
}

Result:

< 0 → x before y
  0 → equal
> 0 → x after y

Use when: You need custom sorting/comparison logic.

10. IEqualityComparer<T>

Definition: Defines equality and hash-code logic.

Important methods:

bool Equals(T x, T y);
int GetHashCode(T obj);

Used heavily by:

HashSet<T>
Dictionary<TKey,TValue>

Use when: You need custom equality or hashing behavior.

11. IReadOnlySet<T>

Definition: Read-only representation of a set.

IReadOnlySet<int> numbers =
    new HashSet<int> { 1, 2, 3 };

Provides set-oriented queries without exposing modification operations.

Use when: You want to expose unique values without allowing modification through the API.

12. IAsyncEnumerable<T>

Definition: Represents a sequence whose elements can be produced asynchronously.

async IAsyncEnumerable<int> GetNumbers()
{
    yield return 1;
    yield return 2;
}

Consume with:

await foreach (var number in GetNumbers())
{
    Console.WriteLine(number);
}

Use when: Data arrives asynchronously, such as streaming or paginated data.

⭐ Most Important Hierarchy

This is the part worth memorizing:

IEnumerable<T>
      │
      ▼
ICollection<T>
      │
      ▼
IList<T>

And separately:

IEnumerable<T>
      │
      ▼
IReadOnlyCollection<T>
      │
      ▼
IReadOnlyList<T>

Set:

IEnumerable<T>
      │
      ▼
ISet<T>

Dictionary:

IEnumerable<KeyValuePair<TKey,TValue>>
              │
              ▼
     IDictionary<TKey,TValue>
⭐ Interface → Typical Implementation

| Interface                          | Common implementation                                      |
| ---------------------------------- | ---------------------------------------------------------- |
| `IEnumerable<T>`                   | `List<T>`                                                  |
| `ICollection<T>`                   | `List<T>`, `HashSet<T>`                                    |
| `IList<T>`                         | `List<T>`                                                  |
| `IReadOnlyCollection<T>`           | `List<T>`, `HashSet<T>`                                    |
| `IReadOnlyList<T>`                 | `List<T>`                                                  |
| `ISet<T>`                          | `HashSet<T>`, `SortedSet<T>`                               |
| `IReadOnlySet<T>`                  | `HashSet<T>`, `SortedSet<T>`                               |
| `IDictionary<TKey,TValue>`         | `Dictionary<TKey,TValue>`, `SortedDictionary<TKey,TValue>` |
| `IReadOnlyDictionary<TKey,TValue>` | `Dictionary<TKey,TValue>`                                  |
| `IComparer<T>`                     | Custom comparer                                            |
| `IEqualityComparer<T>`             | Custom equality comparer                                   |
| `IAsyncEnumerable<T>`              | Async iterator                                             |


Product-Company Priority

If you're short on time, remember these first:

⭐⭐⭐⭐⭐ IEnumerable<T>
⭐⭐⭐⭐⭐ ICollection<T>
⭐⭐⭐⭐⭐ IList<T>
⭐⭐⭐⭐⭐ IDictionary<TKey,TValue>
⭐⭐⭐⭐⭐ ISet<T>

⭐⭐⭐⭐  IReadOnlyCollection<T>
⭐⭐⭐⭐  IReadOnlyList<T>
⭐⭐⭐⭐  IReadOnlyDictionary<TKey,TValue>

⭐⭐⭐⭐  IComparer<T>
⭐⭐⭐⭐  IEqualityComparer<T>

⭐⭐⭐    IAsyncEnumerable<T>
The core idea
"I only need to iterate"
        ↓
IEnumerable<T>

"I need collection operations"
        ↓
ICollection<T>

"I need index-based operations"
        ↓
IList<T>

"I need unique elements"
        ↓
ISet<T>

"I need key → value"
        ↓
IDictionary<TKey,TValue>

"I need to expose data without modification"
        ↓
IReadOnly*