Collection Type: ✅ Generic Collection
Namespace: System.Collections.Generic
Internal concept: Hash table

------------------------------------------------------------------------------------
1. Definition

Dictionary<TKey,TValue> is a generic collection that stores data as key-value pairs and provides fast average-case lookup using a key.

Example:

Dictionary<int, string> employees = new();

We can store:

Key       Value
----------------
101   →   Ali
102   →   Ahmed
103   →   John

Then:

employees[102]

returns:

Ahmed

----------------------------------------------------------------------------------------------------------
2. Why Do We Need Dictionary?

Suppose you have:

100000 employees

and each employee has:

EmployeeId
Name

You want:

"Give me the employee whose ID is 85432."

With a List<Employee>, you might have to search through the list:

1 → 2 → 3 → 4 → ... → 85432

Potentially:

O(n)

With a properly configured Dictionary<int, Employee>:

EmployeeId → Employee

you can usually find it in:

O(1) average case

That's the main reason dictionaries are so important.

-----------------------------------------------------------------------------------------------------------------
3. Core Concept

A dictionary looks like:

+---------+----------------+
|   Key   |     Value      |
+---------+----------------+
|   101   | Ali            |
|   102   | Ahmed          |
|   103   | John           |
+---------+----------------+

The key identifies the value.

Think:

Key
 ↓
"Find this"
 ↓
Value

---------------------------------------------------------------------------------------------------------------------------

4. Real-Time Examples
Example 1 — Employee Lookup
Employee ID → Employee
101 → Ali
102 → Ahmed
103 → John


Example 2 — Product Price
ProductId → Price
1001 → ₹500
1002 → ₹750
1003 → ₹1200


Example 3 — Configuration
Setting Name → Setting Value
"Theme" → "Dark"
"Language" → "English"
"Timeout" → "30"


Example 4 — Frequency Counting

This is extremely important for DSA.

Suppose:

apple banana apple orange apple banana

We can calculate frequency:

apple  → 3
banana → 2
orange → 1

using:

Dictionary<string, int>

This pattern appears constantly in coding interviews.

----------------------------------------------------------------------------------------------------------------

5. Technical Example
Dictionary<int, string> students = new();

students.Add(1, "Ali");
students.Add(2, "Ahmed");
students.Add(3, "John");

Conceptually:

1 → Ali
2 → Ahmed
3 → John

Then:

string name = students[2];

Result:

Ahmed

-----------------------------------------------------------------------------------------------------------------

6. Syntax

Basic syntax:

Dictionary<TKey, TValue> dictionary = new();

Example:

Dictionary<int, string> employees = new();

Here:

TKey   = int
TValue = string

Therefore:

int → key
string → value

----------------------------------------------------------------------------------------------------------------------

7. Keys and Values

This is fundamental.

Dictionary<int, string>

means:

int    → Key
string → Value

For example:

101 → Ali

Here:

101 = Key
Ali = Value

------------------------------------------------------------------------------------------------------------------------------

8. Keys Must Be Unique

This is one of the most important rules.

You cannot have:

101 → Ali
101 → Ahmed

as two separate entries in the same dictionary.

The key:

101

must uniquely identify an entry.

---------------------------------------------------------------------------------------------------------------------------------

9. Values Can Be Duplicate

This is allowed:

101 → Ali
102 → Ali
103 → Ali

Keys:

101
102
103

are unique.

Values can be the same.

---------------------------------------------------------------------------------------------------------------------------------

10. Basic Creation
Dictionary<int, string> employees = new();

You can also initialize directly:

Dictionary<int, string> employees = new()
{
    { 101, "Ali" },
    { 102, "Ahmed" },
    { 103, "John" }
};

Modern C# also supports collection-expression syntax in appropriate language versions:

Dictionary<int, string> employees =
    new()
    {
        [101] = "Ali",
        [102] = "Ahmed",
        [103] = "John"
    };

------------------------------------------------------------------------------------------------------------------------------------------
11. Add()
employees.Add(104, "David");

Adds:

104 → David
Important

If key 104 already exists:

employees.Add(104, "Mike");

an exception is thrown.

Specifically:

ArgumentException

So Add() is appropriate when you expect the key to be new and want duplicate keys to be treated as an error.

-------------------------------------------------------------------------------------------------------------------------------------------------

12. Indexer [key]

You can retrieve a value using:

employees[101]

Result:

Ali

You can also update:

employees[101] = "Mohammad Ali";

Now:

101 → Mohammad Ali


Important distinction

If the key doesn't exist:

employees[999]

accessing it throws:

KeyNotFoundException

But assigning:

employees[999] = "New Employee";

will create a new entry.

This distinction is a common interview question.

-----------------------------------------------------------------------------------------------------------------

13. TryGetValue()

One of the most useful dictionary methods:

if (employees.TryGetValue(101, out string? name))
{
    Console.WriteLine(name);
}

It safely attempts to retrieve the value.

If found:

true

and name receives the value.

If not found:

false

No KeyNotFoundException is thrown.

--------------------------------------------------------------------------------------------------------------------

14. Why Prefer TryGetValue()?

Suppose:

if (employees.ContainsKey(101))
{
    Console.WriteLine(employees[101]);
}

This performs a lookup to check the key and then another lookup to retrieve the value.

A better pattern is generally:

if (employees.TryGetValue(101, out string? name))
{
    Console.WriteLine(name);
}

This expresses the operation directly and avoids the unnecessary check-then-fetch pattern.

⭐ Interview tip: Know TryGetValue() very well.

---------------------------------------------------------------------------------------------------
15. ContainsKey()
employees.ContainsKey(101)

asks:

Does this key exist?

Returns:

true
false

Example:

if (employees.ContainsKey(101))
{
    Console.WriteLine("Employee exists");
}

Average-case complexity:

O(1)

Worst case can degrade due to hash collisions.

--------------------------------------------------------------------------------------------------------

16. ContainsValue()
employees.ContainsValue("Ali")

asks:

Does this value exist?

Unlike key lookup, this requires searching through values.

Complexity:

O(n)

This is an important distinction:

ContainsKey   → average O(1)
ContainsValue → O(n)

--------------------------------------------------------------------------------------------------------

17. Remove()
employees.Remove(101);

Removes the entry with key 101.

Before:

101 → Ali
102 → Ahmed

After:

102 → Ahmed

Returns:

true

----
if an entry was removed.

Returns:

false

if the key wasn't present.

------------------------------------------------------------------------------------------------------------------
18. Clear()
employees.Clear();

Removes all key-value pairs.

After:

Count = 0

--------------------------------------------------------------------------------------------------------------

19. Count
employees.Count

returns the number of key-value pairs.

Example:

101 → Ali
102 → Ahmed
103 → John

Then:

Count = 3

---------------------------------------------------------------------------------------------------------------

20. Keys
employees.Keys

gives access to the collection of keys.

Conceptually:

101
102
103

You can iterate:

foreach (int id in employees.Keys)
{
    Console.WriteLine(id);
}

--------------------------------------------------------------------------------------------------------------

21. Values
employees.Values

gives access to the collection of values.

Example:

Ali
Ahmed
John

You can iterate:

foreach (string name in employees.Values)
{
    Console.WriteLine(name);
}

-----------------------------------------------------------------------------------------------------------

22. Iterating Through the Dictionary

The most common approach:

foreach (KeyValuePair<int, string> employee in employees)
{
    Console.WriteLine(
        $"{employee.Key} → {employee.Value}");
}

You can also use tuple deconstruction:

foreach (var (id, name) in employees)
{
    Console.WriteLine($"{id} → {name}");
}

---------------------------------------------------------------------------------------------
23. TryAdd()

This is another important method.

bool added =
    employees.TryAdd(105, "Sara");

If key 105 doesn't exist:

true

and the entry is added.

If key 105 already exists:

false

No exception is thrown.

Compare:
Add()
    ↓
Duplicate key → Exception

TryAdd()
    ↓
Duplicate key → false

This is very useful when duplicate keys are an expected possibility.

--------------------------------------------------------------------------------------------

24. Remove(key, out value)

Modern .NET also provides an overload that can return the removed value:

if (employees.Remove(101, out string? removedName))
{
    Console.WriteLine(removedName);
}

This allows you to remove the entry and retrieve its value in one operation.

-------------------------------------------------------------------------------------------

25. GetValueOrDefault()

For dictionaries implementing the appropriate APIs, you can use:

string? name =
    employees.GetValueOrDefault(101);

If the key exists:

Ali

If it doesn't:

null

For value types, the default value is 

returned, such as 0 for int.

Important

Don't use this when you need to distinguish:

key doesn't exist

from:

key exists but its value is default

For that situation, use:

TryGetValue()

---------------------------------------------------------------------------------------------------

26. EnsureCapacity()

You may encounter:

employees.EnsureCapacity(1000);

This asks the dictionary to ensure it has enough internal capacity for the requested number of entries.

This can be useful when you already know approximately how many entries you'll add.

It can reduce unnecessary resizing.

It's not something you'll use every day, but it's worth knowing for performance-oriented interviews.

---------------------------------------------------------------------------------------------------------

27. TrimExcess()
employees.TrimExcess();

Attempts to reduce unused internal capacity.

Think:

Before:
Count    = 100
Capacity = much larger

After:
Capacity ≈ appropriate size

Don't call it casually after every removal. Resizing/shrinking itself has a cost.

-------------------------------------------------------------------------------------------------------

28. Important Dictionary Members

Your project notes should include:

Properties
Count
Keys
Values
Adding
Add()
TryAdd()
[index] = value
Searching
ContainsKey()
ContainsValue()
TryGetValue()
GetValueOrDefault()
Removing
Remove()
Remove(key, out value)
Clear()
Capacity-related
EnsureCapacity()
TrimExcess()
Enumeration
foreach

---------------------------------------------------------------------------------------------------------

29. Internal Working — The Important Part

Now we reach the DSA heart of Dictionary.

Dictionary<TKey,TValue> is based on a hash table.

Conceptually:

Key
 ↓
Hash Function
 ↓
Hash Code
 ↓
Bucket
 ↓
Entry
 ↓
Value

For example:

Key = 101

101
 ↓
GetHashCode()
 ↓
some hash code
 ↓
bucket calculation
 ↓
bucket #7
 ↓
entry
 ↓
"Ali"

This is why dictionary lookup can be extremely fast.

---------------------------------------------------------------------------------------------------
30. What Is Hashing?

Hashing is the process of converting a key into a hash code.

Conceptually:

Key
 ↓
Hash Function
 ↓
Hash Code

For example, conceptually:

"Ali"
 ↓
hash function
 ↓
12345678

Don't memorize the actual number.

The important idea is:

The hash code helps the dictionary determine where to look.

---------------------------------------------------------------------------------------------------

31. GetHashCode()

C# objects provide:

GetHashCode()

For dictionary keys, hashing is part of determining where an entry belongs.

For example:

int hash = key.GetHashCode();

But there is a very important rule:

Equal objects must produce equal hash codes.

However:

Different objects can produce the same hash code.

That leads us to collisions.

-------------------------------------------------------------------------------------------

32. Hash Collision

A collision happens when two different keys map to the same bucket.

Conceptually:

Key A
 ↓
Hash
 ↓
Bucket 5

Key B
 ↓
Hash
 ↓
Bucket 5

Both end up associated with the same bucket.

This is called a:

Hash collision

A good hash table handles collisions internally.

You don't normally have to manually resolve them when using Dictionary<TKey,TValue>.

--------------------------------------------------------------------------------------

33. Why Doesn't Collision Break Dictionary?

Because the dictionary doesn't simply say:

bucket → value

Internally it maintains entry information that allows it to distinguish entries that share a bucket.

Conceptually:

Bucket 5
   ↓
Entry A → Entry B → ...

The exact internal representation is runtime implementation detail, so for interviews 
focus on the conceptual model rather than memorizing private fields.

----------------------------------------------------------------------------------------------------

34. Equality Is Extremely Important

Hashing alone isn't enough.

Suppose:

Key A
Key B

produce the same hash code.

The dictionary still needs to determine:

Are these actually the same key?

That's where equality comes in.

Conceptually:

Hash code
   ↓
Find candidate location
   ↓
Compare keys for equality
   ↓
Match?

For custom key types, correct equality and hash-code behavior is critical.

-------------------------------------------------------------------------------------------------------

35. Equals() + GetHashCode()

If you create a custom class used as a dictionary key, you need consistent equality semantics.

The fundamental rule is:

If A.Equals(B) == true

then:

A.GetHashCode() == B.GetHashCode()

The reverse is not required:

Same hash code
≠
Objects must be equal

because collisions are possible.

This is a very common interview topic.

-----------------------------------------------------------------------------------------------------

36. Why Dictionary Lookup Is O(1) Average

Suppose we have:

1,000,000 entries

A good hash function distributes keys reasonably across buckets.

Instead of searching:

1 → 2 → 3 → ... → 1,000,000

the dictionary uses the key's hash to go toward the appropriate bucket.

Therefore the expected/average lookup cost is approximately:

O(1).

But it is not mathematically guaranteed for every possible workload.

-------------------------------------------------------------------------------------------------------------

37. Worst Case

If many keys collide badly, lookup can degrade.

Conceptually:

Bucket
  ↓
A → B → C → D → E → F

The dictionary may have to examine multiple candidates.

So a useful interview statement is:

Dictionary lookup is O(1) average case, but can degrade toward O(n) in a pathological collision scenario.

Modern .NET has implementation details and protections that can mitigate certain collision patterns, 
but the standard algorithmic answer remains average O(1), worst-case O(n).

----------------------------------------------------------------------------------------------------------

38. Resizing

Like List<T>, a dictionary has internal capacity.

When it needs more space:

Current capacity
       ↓
Not enough
       ↓
Resize
       ↓
Reorganize entries

Resizing has a cost.

This is why:

new Dictionary<int, string>(10000);

can be useful when you already know approximately how many entries you will store.

---------------------------------------------------------------------------------------------------------

39. Dictionary Capacity

You can specify initial capacity:

Dictionary<int, string> employees =
    new(1000);

This doesn't mean:

Count = 1000

It means you're requesting enough initial internal capacity for roughly that scale of entries.

Initially:

Count = 0

-------------------------------------------------------------------------------------------------------

40. Dictionary Complexity

| Operation         | Average | Worst Case |
| ----------------- | ------: | ---------: |
| Add               |    O(1) |       O(n) |
| Lookup by key     |    O(1) |       O(n) |
| Update by key     |    O(1) |       O(n) |
| `ContainsKey()`   |    O(1) |       O(n) |
| `TryGetValue()`   |    O(1) |       O(n) |
| Remove            |    O(1) |       O(n) |
| `ContainsValue()` |    O(n) |       O(n) |
| Enumeration       |    O(n) |       O(n) |

The important interview phrase is:

Average-case O(1), not guaranteed O(1).


---------------------------------------------------------------------------------------------------------
41. Dictionary vs List

| Requirement        | `List<T>`     | `Dictionary<TKey,TValue>`     |
| ------------------ | ------------- | ----------------------------- |
| Ordered sequence   | ✅             | ❌ Not for relying on ordering |
| Index access       | ✅             | ❌                             |
| Key lookup         | ❌ O(n) search | ✅ O(1) average                |
| Unique keys        | ❌             | ✅                             |
| Duplicate values   | ✅             | ✅                             |
| Membership by key  | ❌             | ✅                             |
| Frequency counting | Possible      | ⭐ Excellent                   |
| Memory overhead    | Lower         | Higher                        |
| Internal structure | Array-backed  | Hash table                    |

----------------------------------------------------------------------------------------------------------------

42. Dictionary vs HashSet

Very important for DSA.

Dictionary
Key → Value

Example:

101 → Ali
HashSet
Value

Example:

101
102
103

Use:

Dictionary

when you need to associate information with a key.

Use:

HashSet

when you primarily need unique values and fast membership checking.

--------------------------------------------------------------------------------------------------------------------

43. Dictionary vs SortedDictionary

Dictionary<TKey,TValue>:

Fast average lookup
No sorted-key guarantee

SortedDictionary<TKey,TValue>:

Keys maintained in sorted order
Typically O(log n) lookup/insertion/removal

So:

Need fastest average lookup
→ Dictionary

Need sorted keys
→ SortedDictionary

----------------------------------------------------------------------------------------------------------------------
44. Dictionary vs SortedList

This is a more advanced C# interview comparison.

SortedList<TKey,TValue> is designed around sorted key/value storage and has different performance/memory characteristics from Dictionary<TKey,TValue>.

Don't assume:

SortedList = List + Dictionary

It's a distinct collection with different trade-offs.

--------------------------------------------------------------------------------------------------------------------

45. When Should You Use Dictionary?

Use it when:

✅ Lookup is based on a key
EmployeeId → Employee
✅ You need fast average-case lookup
✅ You need frequency counting
character → frequency
✅ You need caching
URL → Response
✅ You need configuration mapping
setting → value
✅ You need to associate two pieces of information
ProductId → Product

---------------------------------------------------------------------------------------------------------------

46. When Should You NOT Use Dictionary?

Don't use it just because lookup sounds important.

Avoid it when:

❌ You need index-based access

Use:

List<T>


❌ You need only unique values

Use:
HashSet<T>

❌ You need FIFO

Use:
Queue<T>

❌ You need LIFO

Use:
Stack<T>

❌ You need sorted keys
Consider:

SortedDictionary<TKey,TValue>
❌ You need to preserve a specific sequence as your primary abstraction

Use an ordered collection designed for that requirement.

----------------------------------------------------------------------------------------------------------------------

47. DSA — Frequency Counting

This is one of the most important patterns.

Input:

a b a c a b

We want:

a → 3
b → 2
c → 1

Conceptually:

Dictionary<char, int> frequency = new();

Then:

character
   ↓
Dictionary
   ↓
count

This technique appears in:

Anagram problems
Duplicate detection
Frequency counting
Two Sum variants
Sliding Window
Subarray problems
String problems
Hashing problems

You will use this heavily in your DSA preparation.

---------------------------------------------------------------------------------

48. Product-Company Interview Tip

If an interviewer asks:

"Why is Dictionary lookup O(1)?"

Don't answer:

"Because Dictionary is fast."

Instead:

"Dictionary uses hashing. The key is converted to a hash code, which helps locate the
appropriate bucket. With a good distribution of keys, lookup takes constant expected time. 
Collisions can require additional comparisons, so O(1) is the average-case complexity rather than an absolute guarantee."
That's a strong interview answer.

------------------------------------------------------------------------------------------

49. Another Interview Question
What happens if I do this?
dictionary.Add(1, "A");
dictionary.Add(1, "B");

Answer:

The second Add() throws ArgumentException because dictionary keys must be unique.

But:

dictionary[1] = "B";

updates the existing value.

And if key 1 didn't exist, the indexer assignment would create it.

---------------------------------------------------------------------------------------

50. Add() vs TryAdd() vs Indexer

| Operation     | Existing Key | Missing Key   |
| ------------- | ------------ | ------------- |
| `Add(k,v)`    | ❌ Exception  | ✅ Adds        |
| `TryAdd(k,v)` | `false`      | `true` + adds |
| `[k] = v`     | Updates      | Adds          |

-----------------------------------------------------------------------------------

