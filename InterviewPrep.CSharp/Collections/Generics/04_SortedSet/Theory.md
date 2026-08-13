C# Collections — SortedSet<T>

Collection Type: ✅ Generic Collection
Namespace: System.Collections.Generic
Category: Set collection
DSA importance: ⭐⭐⭐⭐
Interview importance: ⭐⭐⭐⭐

--------------------------------------------------------------------------------

1. Definition

SortedSet<T> is a generic collection that stores unique elements and automatically keeps them sorted.

So it combines two important properties:

Unique elements
       +
Sorted order

Example:

SortedSet<int> numbers = new();

numbers.Add(50);
numbers.Add(10);
numbers.Add(30);
numbers.Add(20);
numbers.Add(10);

The result is conceptually:

10
20
30
50

Notice:

10 appears only once → unique
Elements are sorted → ordered

---------------------------------------------------------------------------------------------------

2. Why Do We Need SortedSet<T>?

Compare:

HashSet<T>
HashSet<int>

is mainly for:

Fast membership + uniqueness

SortedSet<T>
SortedSet<int>

is for:

Uniqueness + sorted order

Example requirement:

"Store all unique employee IDs and always be able to iterate through them in ascending order."

SortedSet<int> is a natural fit.

--------------------------------------------------------------------------------------------------------

3. Real-Time Example

Suppose an application receives product prices:

500
100
300
500
200
100

We want:

No duplicates
Sorted prices

Result:

100
200
300
500

SortedSet<int> handles both requirements.

------------------------------------------------------------------------------------------------
4. Basic Syntax
SortedSet<int> numbers = new();

For strings:

SortedSet<string> names = new();

For custom objects, you need an appropriate comparison strategy.

------------------------------------------------------------------------------------------------
5. Adding Elements
numbers.Add(50);
numbers.Add(10);
numbers.Add(30);
numbers.Add(20);

Even though insertion happened in this order:

50 → 10 → 30 → 20

iteration gives sorted order:

10 → 20 → 30 → 50

-------------------------------------------------------
6. Duplicate Elements

Duplicates aren't allowed.

numbers.Add(10);
numbers.Add(10);
numbers.Add(10);

Only one 10 exists.

And, like HashSet<T>, Add() returns bool.

bool result = numbers.Add(10);

If already present:

false

If newly added:

true

---------------------------------------------------------------------------------
7. How Is It Different From HashSet<T>?

This is an important interview question.

| Feature      | `HashSet<T>`    | `SortedSet<T>`         |
| ------------ | --------------- | ---------------------- |
| Generic      | ✅               | ✅                      |
| Duplicates   | ❌               | ❌                      |
| Sorted       | ❌               | ✅                      |
| Index access | ❌               | ❌                      |
| Lookup       | O(1) average    | O(log n)               |
| Add          | O(1) average    | O(log n)               |
| Remove       | O(1) average    | O(log n)               |
| Main purpose | Fast membership | Sorted unique elements |

So:

HashSet
    ↓
Hashing
    ↓
Fast average O(1)

while:

SortedSet
    ↓
Sorted tree-based structure
    ↓
O(log n)

---------------------------------------------------------------------------------

8. Why Is SortedSet O(log n)?

SortedSet<T> maintains elements using a balanced tree structure.

Conceptually:

             40
            /  \
          20    60
         / \    / \
       10  30  50  70

Searching for 50 doesn't require checking every element.

It can navigate according to comparisons:

50
 ↓
40
 ↓
right
 ↓
60
 ↓
left
 ↓
50

This gives approximately:

O(log n)

for search, insertion, and removal.

The exact internal implementation details should not be treated as a public API contract,
but the important behavior is logarithmic sorted-set operations.

--------------------------------------------------------------------------------------------------------------
9. Contains()
numbers.Contains(30);

Returns:

true

or:

false

Unlike HashSet<T>:

HashSet<T>     → O(1) average
SortedSet<T>   → O(log n)

--------------------------------------------------------------------------------------------------------------
10. Remove()
numbers.Remove(30);

Removes the element.

Returns:

true

if removed.

Otherwise:

false

--------------------------------------------------------------------------------------------------------------
11. Count
Console.WriteLine(numbers.Count);

Returns the number of unique elements.

--------------------------------------------------------------------------------------------------------------

12. Clear()
numbers.Clear();

Removes everything.

--------------------------------------------------------------------------------------------------------------

13. Min

One of the most useful properties:

numbers.Min

Example:

10
20
30
50

Then:

Min = 10

--------------------------------------------------------------------------------------------------------------
14. Max
numbers.Max

For:

10
20
30
50

we get:

Max = 50

This is one major advantage over HashSet<T>.

--------------------------------------------------------------------------------------------------------------

15. GetViewBetween()

This is an important SortedSet<T> feature.

Suppose:

10
20
30
40
50
60
70

You want elements from:

20 → 50

You can use:

var view = numbers.GetViewBetween(20, 50);

Conceptually:

20
30
40
50

This is useful when working with sorted ranges.

--------------------------------------------------------------------------------------------------------------

16. Important: It Is a View

GetViewBetween() isn't simply an unrelated copy.

It provides a view over a range of the sorted set.

Therefore, you need to understand that operations on the view are constrained to that range.

For interviews, remember:

GetViewBetween(min, max) gives a range view over the sorted set.

--------------------------------------------------------------------------------------------------------------

17. Reverse()

You can enumerate the set in reverse sorted order:

foreach (int number in numbers.Reverse())
{
    Console.WriteLine(number);
}

If the set is:

10
20
30
40

output:

40
30
20
10

--------------------------------------------------------------------------------------------------------------
18. Set Operations

Just like HashSet<T>, SortedSet<T> supports set operations.

Union
setA.UnionWith(setB);
Intersection
setA.IntersectWith(setB);
Difference
setA.ExceptWith(setB);
Symmetric difference
setA.SymmetricExceptWith(setB);

So the mathematical set concepts you learned with HashSet<T> apply here too.

--------------------------------------------------------------------------------------------------------------

19. IsSubsetOf()
setA.IsSubsetOf(setB);

Checks whether every element in setA exists in setB.

--------------------------------------------------------------------------------------------------------------

20. IsSupersetOf()
setA.IsSupersetOf(setB);

Checks whether setA contains every element in setB.

--------------------------------------------------------------------------------------------------------------

21. Overlaps()
setA.Overlaps(setB);

Returns true if at least one common element exists.

--------------------------------------------------------------------------------------------------------------

22. SetEquals()
setA.SetEquals(setB);

Checks whether both sets contain exactly the same elements.

Order doesn't matter.

For example:

A = 10 20 30

B = 30 10 20

They are equal as sets.

--------------------------------------------------------------------------------------------------------------

23. RemoveWhere()

You can remove elements based on a condition:

numbers.RemoveWhere(x => x > 50);

Example:

10
20
30
60
70

becomes:

10
20
30

--------------------------------------------------------------------------------------------------------------
24. CopyTo()
int[] array = new int[numbers.Count];

numbers.CopyTo(array);

Copies elements into an array.

Because the set is sorted, enumeration/copying follows the set's sorted ordering.

--------------------------------------------------------------------------------------------------------------

25. EnsureCapacity()
numbers.EnsureCapacity(100);

Requests capacity for at least the specified number of elements.

Useful when you have an approximate idea of the required size.

--------------------------------------------------------------------------------------------------------------

26. TrimExcess()
numbers.TrimExcess();

Attempts to reduce excess internal capacity.

Don't call this routinely after every removal.

--------------------------------------------------------------------------------------------------------------

27. Complete Built-in Methods

For your VS 2022 Generic Collections folder, keep these under SortedSet<T>.

Basic operations
Add()
Contains()
Remove()
Clear()
Count

--
Extremes
Min
Max

---
Range
GetViewBetween()

--
Enumeration
Reverse()

--
Set operations
UnionWith()
IntersectWith()
ExceptWith()
SymmetricExceptWith()

--
Set relationships
IsSubsetOf()
IsSupersetOf()
IsProperSubsetOf()
IsProperSupersetOf()
Overlaps()
SetEquals()

--
Conditional removal
RemoveWhere()

--
Copy/capacity
CopyTo()
EnsureCapacity()
TrimExcess()


--------------------------------------------------------------------------------------------------------------
28. When Should You Use SortedSet<T>?

Use it when you need:

✅ Unique elements
No duplicates

AND:

✅ Sorted elements
Always maintain sorted order

AND potentially:

✅ Range operations
GetViewBetween()

--------------------------------------------------------------------------------------------------------------
29. When Should You NOT Use It?

Don't use SortedSet<T> when you don't need sorting.

For example:

"I only need to check whether an ID has already appeared."

Use:

HashSet<T>

not:

SortedSet<T>

because HashSet<T> generally gives faster average membership operations.

--------------------------------------------------------------------------------------------------------------

30. Choosing Between the Three

This is important for interviews.

List<T>

Use when:

Need sequence
+
Index access
+
Duplicates allowed

--
HashSet<T>

Use when:

Need uniqueness
+
Fast membership
+
Sorting NOT required

----
SortedSet<T>

Use when:

Need uniqueness
+
Sorted order
+
Range operations

Mental shortcut:

---

LIST
 ↓
"Give me a sequence."

HASHSET
 ↓
"Have I seen this?"

SORTEDSET
 ↓
"Have I seen this AND keep it sorted?"

--------------------------------------------------------------------------------------------------------------
31. Real-Time Example — Ranking Scores

Imagine an application receives scores:

85
95
70
95
80
70
90

You want all unique scores in sorted order.

SortedSet<int> scores = new()
{
    85,
    95,
    70,
    95,
    80,
    70,
    90
};

Result:

70
80
85
90
95

Now:

scores.Min

gives:

70

and:

scores.Max

gives:

95

This is a very good practical use case.

--------------------------------------------------------------------------------------------------------------

32. DSA Example

Suppose an array contains:

5 2 8 2 1 8 10

Requirement:

Return unique numbers in sorted order.

With SortedSet<int>:

SortedSet<int> unique = new(numbers);

Result:

1 2 5 8 10

Without SortedSet, you'd potentially need:

Remove duplicates
Sort the result

SortedSet<T> handles both properties in one collection.

--------------------------------------------------------------------------------------------------------------

33. But Be Careful in DSA

Don't automatically use SortedSet whenever you see:

"unique + sorted"

You should understand the requirements.

If you only need the final answer sorted once, sometimes:

HashSet
+
Sort

or:

List
+
Distinct()
+
OrderBy()

may be more appropriate.

SortedSet<T> is particularly useful when you need the data to remain sorted while you are continuously adding/removing elements.

--------------------------------------------------------------------------------------------------------------

34. Interview Question
Q: HashSet vs SortedSet?

Answer:

HashSet<T> provides unique elements with average O(1) insertion, lookup, and removal using hashing. 
SortedSet<T> also guarantees uniqueness but maintains elements in sorted order, with O(log n) search, insertion, and removal.

That is a very good product-company interview answer.

--------------------------------------------------------------------------------------------------------------

35. Another Interview Question
Q: Why is SortedSet<T> slower than HashSet<T> for lookup?

Because:

HashSet
→ hash-based
→ average O(1)

while:

SortedSet
→ ordered tree-based structure
→ O(log n)

The additional ordering requirement has a cost.

--------------------------------------------------------------------------------------------------------------

36. Another Important Question
Q: Does SortedSet<T> allow duplicate values?

No.

SortedSet<int> set = new();

set.Add(10);
set.Add(10);

Only one 10 remains.

--------------------------------------------------------------------------------------------------------------

37. Another Important Question
Q: Does SortedSet<T> support indexing?

No.

This won't work:

set[0]

If you need:

collection[index]

use something like:

List<T>

depending on your requirements.

--------------------------------------------------------------------------------------------------------------

