Now Understand Every Method

This is the important part.

Count
numbers.Count

Returns the number of elements currently stored.

Example:

10 20 30 40

Count = 4

Complexity: O(1)

5. Capacity
numbers.Capacity

Shows the currently allocated internal storage capacity.

Remember:

Count    → actual elements
Capacity → allocated storage

For example:

Count = 5
Capacity = 8

The list contains 5 elements but currently has room for more before resizing.

6. Add()
numbers.Add(60);

Adds one element to the end.

Before:
10 20 30

After:
10 20 30 60

Complexity: O(1) amortized.

7. AddRange()
numbers.AddRange(new[] { 70, 80, 90 });

Adds multiple elements to the end.

10 20 30
        ↓
10 20 30 70 80 90

Useful when you already have multiple values.

8. Insert()
numbers.Insert(1, 15);

Adds an element at a particular index.

Before:

10 20 30
   ↑
 index 1

After:

10 15 20 30

Elements after the insertion point must generally shift.

Complexity: O(n)

9. InsertRange()
numbers.InsertRange(2, new[] { 17, 18 });

Inserts multiple elements at a specified index.

Example:

Before:
10 15 20 30

After:
10 15 17 18 20 30
10. Indexer [index]
numbers[0]

Gets an element.

numbers[0] = 5;

Updates an element.

Because List<T> is array-backed:

Access: O(1)

Update: O(1)

11. Contains()
numbers.Contains(30);

Checks whether the value exists.

Returns:

true
false

For List<T>:

Complexity: O(n)

because it may need to examine every element.

12. IndexOf()
numbers.IndexOf(30);

Returns the first index where the value appears.

Example:

10 20 30 40
      ↑
      2

Result:

2

If not found:

-1

Complexity: O(n)

13. LastIndexOf()
numbers.LastIndexOf(30);

Returns the last occurrence.

Example:

10 20 30 40 30
      ↑       ↑
    first    last

Result:

4

Complexity: O(n)

14. Find()
numbers.Find(number => number > 30);

Returns the first element matching the condition.

Example:

10 20 30 40 50
         ↑

Result:

40

Complexity: O(n)

15. FindLast()
numbers.FindLast(number => number > 30);

Returns the last matching element.

Example:

10 20 30 40 50
         ↑   ↑
       first last

Result:

50

Complexity: O(n)

16. FindAll()
numbers.FindAll(number => number > 30);

Returns all matching elements in a new List<T>.

Example:

10 20 30 40 50
         ↓  ↓
       40  50

Result:

40
50
17. Exists()
numbers.Exists(number => number > 50);

Asks:

Does at least one element satisfy this condition?

Returns:

true
false

Very useful when you only need a yes/no answer.

18. TrueForAll()
numbers.TrueForAll(number => number > 0);

Asks:

Do ALL elements satisfy this condition?

Example:

10 20 30

All > 0?
YES

Returns:

true

But:

10 20 -5

All > 0?
NO

Returns:

false
19. Remove()
numbers.Remove(30);

Removes the first matching value.

If:

10 30 20 30

then:

numbers.Remove(30);

produces:

10 20 30

Only the first 30 is removed.

Complexity: O(n)

20. RemoveAt()
numbers.RemoveAt(2);

Removes the element at index 2.

10 20 30 40
      ↑

After:

10 20 40

Complexity: O(n) generally.

Removing the final element is O(1).

21. RemoveRange()
numbers.RemoveRange(1, 2);

Meaning:

start index = 1
count       = 2

Example:

10 20 30 40 50
    ↑  ↑

After:

10 40 50
22. RemoveAll()
numbers.RemoveAll(number => number > 50);

Removes every element matching the condition.

Very different from:

Remove()

which removes only the first matching value.

23. Clear()
numbers.Clear();

Removes all elements.

After:

Count = 0

But remember:

Clear() doesn't necessarily shrink the internal capacity.

24. Sort()
numbers.Sort();

Sorts the list according to the default comparer.

Example:

50 10 40 20 30

becomes:

10 20 30 40 50

For interview/DSA purposes, treat sorting as O(n log n) for the normal comparison-based case; don't confuse it with O(n).

25. Reverse()
numbers.Reverse();

Reverses the elements in place.

10 20 30 40

becomes:

40 30 20 10

Generally:

O(n)

26. ForEach()
numbers.ForEach(number =>
{
    Console.WriteLine(number);
});

Executes an action for every element.

It is convenient for simple operations.

But don't assume it is always better than a normal foreach loop; readability and control flow matter.

27. ToArray()
int[] array = numbers.ToArray();

Creates an array containing the elements.

List<int>
    ↓
ToArray()
    ↓
int[]

Useful when an API specifically expects an array.

28. GetRange()
List<int> range =
    numbers.GetRange(0, 2);

Gets a portion of the list into a new list.

Original:
10 20 30 40 50

GetRange(0, 2):

10 20

It does not create a view into the original list.

29. CopyTo()
int[] array = new int[numbers.Count];

numbers.CopyTo(array);

Copies the list's elements into an existing array.

Important distinction:

ToArray()
→ creates a new array

CopyTo()
→ copies into an existing array
30. BinarySearch()
numbers.Sort();

int index = numbers.BinarySearch(30);

Searches for a value using binary search.

Important: the list must be sorted appropriately for the search to work correctly.

This is particularly useful for connecting C# collections with your DSA knowledge.

Complexity: O(log n) after the required sorted ordering exists.

But remember: if you first need to sort an unsorted list, sorting costs much more than the search itself.

31. Most Important Complexity Table
Operation	Complexity
numbers[index]	O(1)
numbers[index] = value	O(1)
Add()	O(1) amortized
AddRange()	O(k) amortized, excluding resize effects
Insert()	O(n)
InsertRange()	O(n + k) generally
Contains()	O(n)
IndexOf()	O(n)
LastIndexOf()	O(n)
Find()	O(n)
FindLast()	O(n)
FindAll()	O(n)
Exists()	O(n)
TrueForAll()	O(n)
Remove()	O(n)
RemoveAt()	O(n) generally
RemoveRange()	O(n) generally
RemoveAll()	O(n)
Sort()	O(n log n) typical
Reverse()	O(n)
Clear()	O(n)
ToArray()	O(n)
GetRange()	O(k)
CopyTo()	O(n)
BinarySearch()	O(log n)
32. Methods You Should Prioritize

You don't need to give equal importance to every method.

🔥 Must know for interviews + DSA
Add
AddRange
Insert
Remove
RemoveAt
Contains
IndexOf
Count
Sort
Reverse
BinarySearch
Important for normal C# development
Find
FindAll
FindLast
Exists
TrueForAll
Clear
ToArray
GetRange
CopyTo
Less commonly used directly
InsertRange
RemoveRange
RemoveAll
LastIndexOf
ForEach

But you should still know what they do.

33. Important Interview Questions
Q1. Is List<T> a generic or non-generic collection?

Generic.

System.Collections.Generic
Q2. What is the internal data structure of List<T>?

It is backed by an array that can be replaced with a larger array when additional capacity is required.

Q3. Why is List<T>[index] O(1)?

Because the list uses an array internally, allowing direct index-based access.

Q4. Difference between Count and Capacity?

Count is the number of elements; Capacity is the currently allocated storage capacity.

Q5. Difference between Remove() and RemoveAt()?

Remove() removes the first matching value, while RemoveAt() removes the element at a specified index.

Q6. Difference between Remove() and RemoveAll()?

Remove() removes the first matching element, while RemoveAll() removes every element satisfying a condition.

Q7. Difference between Find() and FindAll()?

Find() returns the first matching element, while FindAll() returns all matching elements as a new list.

Q8. Is Add() always O(1)?

No. It is O(1) amortized; a resize can make an individual append O(n).

Q9. Is Contains() O(1)?

No. For List<T>, it is generally O(n) because it performs a sequential search.

Q10. When would you replace List with HashSet?

When fast membership testing and uniqueness are more important than ordering and index-based access.

34. Advantages
✅ Dynamic size
✅ Type-safe
✅ Fast index access
✅ Rich built-in API
✅ Easy to use
✅ Excellent general-purpose collection
✅ Very useful for DSA
✅ Good memory locality compared with node-based structures
35. Disadvantages
❌ Search is O(n)
❌ Middle insertion is O(n)
❌ Middle deletion is O(n)
❌ Resizing can require copying
❌ Doesn't guarantee uniqueness
❌ Not inherently thread-safe for concurrent mutation
❌ Not ideal when access is primarily by key
36. When to Use

Use List<T> when you need:

✓ Ordered elements
✓ Index access
✓ Dynamic size
✓ Frequent append operations
✓ General-purpose collection
✓ Array-like behavior
37. When NOT to Use

Don't automatically choose List<T> when you need:

✗ Key → Value lookup       → Dictionary
✗ Unique values            → HashSet
✗ FIFO                     → Queue
✗ LIFO                     → Stack
✗ Frequent node insertion  → Consider LinkedList/another structure
✗ Fixed-size storage       → Array may be simpler
38. DSA Connection

List<T> is essentially your bridge between C# programming and array-based DSA.

You'll repeatedly use it for:

Arrays
Two Pointer
Sliding Window
Prefix Sum
Binary Search
Sorting
Frequency-related problems
Dynamic arrays

But there's an important DSA habit:

Don't choose a collection because it's convenient; choose it because its operations match the problem.

For example:

Need "Does this value exist?"

If you have millions of membership checks:

List → O(n)
HashSet → average O(1)

That difference can turn an inefficient solution into an efficient one.