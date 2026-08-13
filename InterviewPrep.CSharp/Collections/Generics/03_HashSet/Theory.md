Collection Type: ✅ Generic Collection
Namespace: System.Collections.Generic
Underlying concept: Hash table
DSA importance: ⭐⭐⭐⭐⭐
Product-company interview importance: ⭐⭐⭐⭐⭐


-------------------------------------------------------------------------------------------------------------------------------------------

1. Definition

HashSet<T> is a generic collection that stores unique elements and provides fast average-case operations for checking, adding, and removing elements.

Example:

HashSet<int> numbers = new();

You can have:

10
20
30
40

But duplicates are automatically rejected:

10
20
20  ← duplicate
30

The final set is:

10
20
30

------------------------------------------------------------------------------------------------------------------------------------------------

2. Why Do We Need HashSet<T>?

Suppose you have:

10, 20, 30, 40, 50

and repeatedly ask:

"Does 30 exist?"

With a List<int>:

10 → 20 → 30

The search is generally:

O(n).

With a HashSet<int>:

30
 ↓
hash
 ↓
bucket
 ↓
found

Average:

O(1).

That's the major reason to use HashSet<T>.

-----------------------------------------------------------------------------------------------------------------------------------

3. The Biggest Difference from Dictionary

Remember:

Dictionary
Key → Value

Example:

101 → Ali
102 → Ahmed
HashSet
Value

Example:

101
102
103

There is no separate value associated with the key.

The element itself is what gets hashed and stored.

--------------------------------------------------------------------------------------------------------------------------------------
4. Real-Life Example

Imagine a system that receives customer IDs:

101
105
101
102
105
103

You want only unique IDs.

With List<int>:

101
105
101
102
105
103

You would need additional logic to remove duplicates.

With:

HashSet<int> customerIds = new();

you can simply add them.

Final result:

101
105
102
103

---------------------------------------------------------------------------------------------

5. Technical Example
HashSet<string> usernames = new();

usernames.Add("ali");
usernames.Add("ahmed");
usernames.Add("john");
usernames.Add("ali");

The second "ali" isn't added.

So the set contains:

ali
ahmed
john

-------------------------------------------------------------------------------------------------

6. Important Rule

A HashSet<T> does not allow duplicate elements.

HashSet<int> numbers = new();

numbers.Add(10);
numbers.Add(10);
numbers.Add(10);

Result:

10

not:

10
10
10

-------------------------------------------------------------------------------------------------

7. Add()
bool added = numbers.Add(10);

This is different from List<T>.Add().

HashSet<T>.Add() returns a bool.

New element
numbers.Add(10);

returns:

true
Duplicate element
numbers.Add(10);

returns:

false

This is extremely useful.

You can write:

if (numbers.Add(10))
{
    Console.WriteLine("New element added");
}
else
{
    Console.WriteLine("Element already exists");
}

-------------------------------------------------------------------------------------------------------------

8. Contains()

One of the most important methods:

numbers.Contains(30);

It asks:

Does this element exist?

Returns:

true
false

Average complexity:

O(1).

This is one of the main reasons we choose HashSet<T>.

----------------------------------------------------------------------------------------------------------------

9. Remove()
numbers.Remove(30);

Removes the specified element.

Returns:

true

if it existed and was removed.

Otherwise:

false

Average complexity:

O(1).

----------------------------------------------------------------------------------------------------------------------

10. Count
numbers.Count

returns the number of unique elements.

Example:

10
20
30

Then:

Count = 3

------------------------------------------------------------------------------------------------------------------------

11. Clear()
numbers.Clear();

Removes all elements.

After:

Count = 0

-------------------------------------------------------------------------------------------------------------------

12. RemoveWhere()

Very useful method:

numbers.RemoveWhere(x => x > 50);

It removes every element satisfying the condition.

Example:

10 20 30 60 70 80

After:

10 20 30

This is similar in concept to List<T>.RemoveAll().

But remember:

List<T>     → RemoveAll()
HashSet<T>  → RemoveWhere()

------------------------------------------------------------------------------------------------------------------

13. CopyTo()

You can copy set elements into an array:

int[] array = new int[numbers.Count];

numbers.CopyTo(array);

The set itself isn't converted into an array in-place; the elements are copied.

----------------------------------------------------------------------------------------------------------------------

14. ToArray()
int[] array = numbers.ToArray();

Creates an array containing the elements.

Remember:

Don't rely on HashSet<T> enumeration order as a sorted or index-based order.

-------------------------------------------------------------------------------------------------------------------

15. Set Operations

This is where HashSet<T> becomes particularly interesting.

Suppose:

Set A:

1 2 3 4 5

Set B:

4 5 6 7 8

We can perform mathematical set operations.

The major ones are:

Union
Intersection
Difference
Symmetric Difference

These are very useful in DSA.

-----------------------------------------------------------------------------------------------------------------------------

16. UnionWith()

Union means:

Elements that exist in either set.

A = 1 2 3 4 5

B = 4 5 6 7 8

Union:

1 2 3 4 5 6 7 8

Code:

setA.UnionWith(setB);

After the operation, setA contains the union.

----------------------------------------------------------------------------------------------------------------------------------
17. IntersectWith()

Intersection means:

Elements that exist in BOTH sets.

A = 1 2 3 4 5

B = 4 5 6 7 8

Intersection:

4 5

Code:

setA.IntersectWith(setB);

-----------------------------------------------------------------------------------------------------------------------------------------
18. ExceptWith()

Difference means:

Elements in the first set that aren't in the second.

A = 1 2 3 4 5

B = 4 5 6 7 8

A.ExceptWith(B) gives:

1 2 3

Code:

setA.ExceptWith(setB);

-------------------------------------------------------------------------------------------------------------------------------------------

19. SymmetricExceptWith()

This means:

Elements that belong to either set, but not both.

Given:

A = 1 2 3 4 5

B = 4 5 6 7 8

Common:

4 5

Symmetric difference:

1 2 3 6 7 8

Code:

setA.SymmetricExceptWith(setB);

----------------------------------------------------------------------------------------------------------------------------------------

20. IsSubsetOf()

Question:

Is every element of A also present in B?

Example:

A = 1 2 3

B = 1 2 3 4 5

Then:

A.IsSubsetOf(B)

returns:

true

----------------------------------------------------------------------------------------------------------------------------------------------

21. IsSupersetOf()

Question:

Does A contain every element of B?

Example:

A = 1 2 3 4 5

B = 2 3

Then:

A.IsSupersetOf(B)

returns:

true

-------------------------------------------------------------------------------------------------------------------------------------------------------

22. IsProperSubsetOf()

A is a proper subset of B when:

Every element of A exists in B
A and B are not equal

Example:

A = 1 2 3

B = 1 2 3 4
A.IsProperSubsetOf(B)

returns:

true

But:

A = 1 2 3

B = 1 2 3

returns:

false

because they are equal.

--------------------------------------------------------------------------------------------------------------------------------------------------

23. IsProperSubsetOf()

A is a proper subset of B when:

Every element of A exists in B
A and B are not equal

Example:

A = 1 2 3

B = 1 2 3 4
A.IsProperSubsetOf(B)

returns:

true

But:

A = 1 2 3

B = 1 2 3

returns:

false

because they are equal.

---------------------------------------------------------------------------------------------------------------------------------

24. Overlaps()

Checks whether two sets have at least one element in common.

A = 1 2 3

B = 3 4 5

They overlap because:

3

exists in both.

A.Overlaps(B)

returns:

true

--------------------------------------------------------------------------------------------------------------------------------

25. SetEquals()

Checks whether two sets contain exactly the same elements.

Order doesn't matter.

These are equal:

A = 1 2 3
B = 3 1 2

because both contain:

1 2 3

So:

A.SetEquals(B)

returns:

true

---------------------------------------------------------------------------------------------------------------------------

26. EnsureCapacity()

Like other hash-based collections, HashSet<T> can ensure capacity:

numbers.EnsureCapacity(1000);

This is useful if you know you're going to add many elements.

It can reduce repeated resizing.

---------------------------------------------------------------------------------------------------------------

27. TrimExcess()
numbers.TrimExcess();

Attempts to reduce unused capacity.

Again, don't call it unnecessarily.

------------------------------------------------------------------------------------------------------------

28. Important Methods Summary
Basic
Add()
Contains()
Remove()
Clear()
Count
Conditional removal
RemoveWhere()
Set operations
UnionWith()
IntersectWith()
ExceptWith()
SymmetricExceptWith()
Relationship checks
IsSubsetOf()
IsSupersetOf()
IsProperSubsetOf()
IsProperSupersetOf()
Overlaps()
SetEquals()
Copy/conversion
CopyTo()
ToArray()
Capacity
EnsureCapacity()
TrimExcess()

---------------------------------------------------------------------------------------------------------------
29. Internal Working

Now connect this to what we learned about Dictionary.

HashSet<T> also uses hashing.

But there's an important difference.

Dictionary
Key
 ↓
Hash
 ↓
Bucket
 ↓
Entry
 ↓
Value
HashSet
Element
 ↓
Hash
 ↓
Bucket
 ↓
Entry

There isn't a separate value.

The element itself is what we're storing and looking for.

----------------------------------------------------------------------------------

30. Example

Suppose:

HashSet<int> numbers = new();

numbers.Add(101);

Conceptually:

101
 ↓
GetHashCode()
 ↓
Hash code
 ↓
Bucket calculation
 ↓
Bucket
 ↓
Entry
 ↓
101

Now:

numbers.Contains(101);

conceptually:

101
 ↓
Hash
 ↓
Bucket
 ↓
Candidate entry
 ↓
Equality comparison
 ↓
101 found
 ↓
true

-------------------------------------------------------------------------------

31. Why HashSet<T> Is Fast

Suppose we have:

1 million numbers

and ask:

numbers.Contains(987654);

A List<int> may need to scan:

1
2
3
...
987654

Potentially O(n).

HashSet<int> uses hashing to locate the relevant bucket.

Average:

O(1).

------------------------------------------------------------------------------------

32. Duplicate Detection — DSA Pattern

This is one of the most important patterns you need to know.

Suppose:

Input:

1 2 3 4 2

We want to know whether a duplicate exists.

Use:

HashSet<int> seen = new();

foreach (int number in numbers)
{
    if (!seen.Add(number))
    {
        Console.WriteLine("Duplicate found");
        break;
    }
}

Why does this work?

Because:

seen.Add(number)

returns:

true  → new element
false → already exists

So:

1 → true
2 → true
3 → true
4 → true
2 → false ← duplicate


-----------------------------------------------------------------------------------------

33. Frequency vs Duplicate Detection

This distinction is critical.

Need only:

"Have I seen this before?"

Use:

HashSet
Need:

"How many times did I see it?"

Use:

Dictionary<T, int>

Example:

Input:
A B A C A B

HashSet gives:

A B C

Dictionary gives:

A → 3
B → 2
C → 1

Remember:

HashSet    → uniqueness / membership
Dictionary → mapping / frequency

----------------------------------------------------------------------------------------------
34. Complexity

| Operation         |                Average | Worst Case |
| ----------------- | ---------------------: | ---------: |
| `Add()`           |                   O(1) |       O(n) |
| `Contains()`      |                   O(1) |       O(n) |
| `Remove()`        |                   O(1) |       O(n) |
| `RemoveWhere()`   |                   O(n) |       O(n) |
| `Clear()`         |                   O(n) |       O(n) |
| `UnionWith()`     | Depends on input sizes |          — |
| `IntersectWith()` | Depends on input sizes |          — |
| `ExceptWith()`    | Depends on input sizes |          — |
| `Overlaps()`      | Depends on input sizes |          — |
| `SetEquals()`     | Depends on input sizes |          — |


---------------------------------------------------------------------------------------------

35. Advantages
✅ Unique elements

Duplicates are automatically rejected.

✅ Fast lookup

Average:

O(1)
✅ Excellent for DSA

Especially:

Duplicate detection
Membership testing
Distinct elements
Set operations
✅ Rich set-operation API

You can directly perform:

Union
Intersection
Difference
Symmetric difference

--------------------------------------------------------------------------------------

36. Disadvantages
❌ No index-based access

You cannot do:

numbers[0]

like a list.

❌ Don't use it when ordering is your primary requirement

A HashSet<T> isn't an indexed sequence.

❌ More memory overhead than a simple array

Hash-based structures require additional internal storage.

❌ Doesn't support key → value mapping

Use:

Dictionary<TKey,TValue>

for that.

------------------------------------------------------------------------------------------------

37. When to Use

Use HashSet<T> when you need:

✓ Unique elements
✓ Fast membership checking
✓ Duplicate detection
✓ Remove duplicates
✓ Set operations
✓ Efficient "have I seen this?" checks

-----------------------------------------------------------------------------------

38. When NOT to Use

Don't use it when:

✗ You need index access       → List
✗ You need key → value       → Dictionary
✗ You need FIFO              → Queue
✗ You need LIFO              → Stack
✗ You need sorted elements   → SortedSet
✗ You need duplicate values  → List

---------------------------------------------------------------------------------------

HashSet<T> vs List<T>

| Feature                 | List           | HashSet                     |
| ----------------------- | -------------- | --------------------------- |
| Duplicates              | ✅              | ❌                           |
| Index access            | ✅              | ❌                           |
| Average `Contains`      | O(n)           | O(1)                        |
| Average `Add`           | O(1) amortized | O(1)                        |
| Remove by value         | O(n)           | O(1) average                |
| Ordered sequence        | Better choice  | Not for relying on ordering |
| Unique values           | ❌              | ✅                           |
| DSA duplicate detection | Possible       | ⭐ Excellent                 |


----------------------------------------------------------------------------------------
40. HashSet<T> vs Dictionary<TKey,TValue>

| Feature             | HashSet  | Dictionary  |
| ------------------- | -------- | ----------- |
| Stores              | Elements | Key + Value |
| Unique              | Elements | Keys        |
| Lookup              | Element  | Key         |
| Average lookup      | O(1)     | O(1)        |
| Frequency counting  | ❌        | ✅           |
| Duplicate detection | ⭐⭐⭐⭐⭐    | Possible    |
| Key → data mapping  | ❌        | ⭐⭐⭐⭐⭐       |


-------------------------------------------------------------------------------------

41. HashSet<T> vs SortedSet<T>

This is important.

HashSet
Fast average membership
No sorted-order guarantee

--------
SortedSet
Elements maintained in sorted order
Typically O(log n) search/add/remove

So:

Need fastest average membership
→ HashSet

Need sorted unique elements
→ SortedSet

--------------------------------------------------------------------------------------------------


42. Interview Questions
Q1. What is HashSet<T>?

A generic collection that stores unique elements and provides average O(1) insertion, removal, and membership testing through hashing.

Q2. Can HashSet contain duplicates?

No.

Q3. What does Add() return?
true  → element was added
false → element already existed
Q4. Why is Contains() generally O(1)?

Because the set uses hashing to locate the bucket containing the candidate element.

Q5. What happens if two elements have the same hash code?

A collision occurs. The hash set uses equality comparison to determine whether the candidate is actually the same element.

Q6. HashSet vs Dictionary?

HashSet stores unique elements, while Dictionary stores unique keys associated with values.

Q7. HashSet vs List for duplicate detection?

For large collections and repeated membership checks:

HashSet → average O(1)
List    → O(n)

So HashSet is generally the better choice.

Q8. How do you detect duplicates efficiently?
HashSet<T> seen = new();

if (!seen.Add(value))
{
    // duplicate
}

This is a pattern you should remember for coding interviews.

-----------------------------------------------------------------------------------------------------