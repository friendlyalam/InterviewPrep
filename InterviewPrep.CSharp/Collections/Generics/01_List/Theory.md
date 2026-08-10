1. Definition

List<T> is a generic, dynamically sized collection that stores elements of the same type and provides index-based access.

Example:

List<int> numbers = new();

Here:

List → collection type
<T> → generic type parameter
int → actual type
numbers → variable

So:

List<int>
   ↓
Can store int values

-----------------------------------------------------------------------------------------------------------------

2. Why Do We Need List<T>?

Suppose we have 5 numbers.

An array works:

int[] numbers = { 10, 20, 30, 40, 50 };

But what if we don't know how many numbers we'll receive?

Maybe:

Today → 10 numbers
Tomorrow → 500 numbers
Next month → 10,000 numbers

A List<T> can grow as elements are added.

List
 ↓
Add
 ↓
Add
 ↓
Add
 ↓
Add
 ↓
...

You don't need to manually create a larger array whenever the number of elements changes.

--------------------------------------------------------------------------------------------------------------------------
3. Core Idea

Think of List<T> as a dynamic array.

Conceptually:

Index
  0      1      2      3
  ↓      ↓      ↓      ↓
+------+------+------+------+
|  10  |  20  |  30  |  40 |
+------+------+------+------+

You can access elements by index:

numbers[0]
numbers[1]
numbers[2]

So one of the most important characteristics of List<T> is:

Fast index-based access.

--------------------------------------------------------------------------------------------
4. Real-Time Examples
Example 1 — Employees

An application receives a list of employees:

Ali
Ahmed
John
David

We could represent it as:

List<Employee>
Example 2 — Shopping Cart

A shopping cart contains:

Laptop
Mouse
Keyboard
Monitor

A List<Product> can represent the products.

Example 3 — API Response

An API might return:

100 customers

The application can deserialize them into:

List<Customer>

This is extremely common in ASP.NET Core applications.

Example 4 — DSA

Suppose a problem asks you to store all numbers:

5, 10, 15, 20, 25

A List<int> can store them.

However, in DSA you'll need to understand whether List<T> is actually the best choice for the required operations.

------------------------------------------------------------------------------------------------------------------------------------
5. Technical Example

Suppose we have:

List<int> numbers = new();

We add:

numbers.Add(10);
numbers.Add(20);
numbers.Add(30);

Conceptually:

numbers

+-----+-----+-----+
| 10  | 20  | 30  |
+-----+-----+-----+
   0     1     2

Then:

numbers[1]

returns:

20

----------------------------------------------------------------------------------------

6. Generic Nature of List<T>

List<T> is generic.

The T represents the type of elements.

Examples:

List<int>
List<string>
List<double>
List<bool>
List<Employee>
List<Product>
Example
List<int> numbers;

means:

This list contains integers.

While:

List<string> names;

means:

This list contains strings.

And:

List<Employee> employees;

means:

This list contains Employee objects.

-------------------------------------------------------------------------------------------------------

7. Type Safety

This is one of the biggest advantages of generic collections.

List<int> numbers = new();

numbers.Add(10);
numbers.Add(20);

This is valid.

But:

numbers.Add("Hello");

will produce a compile-time error.

Why?

Because:

List<int>
    ↓
Only int

The compiler protects us from accidentally putting the wrong type into the collection.

--------------------------------------------------------------------------------------------------------------
8. Syntax
Empty List
List<int> numbers = new();
Collection initializer
List<int> numbers = new()
{
    10,
    20,
    30
};
Explicit constructor
List<int> numbers = new List<int>();

Both are valid.

Modern C# commonly uses:

List<int> numbers = new();

-------------------------------------------------------------------------------------------------------
9. Adding Elements

The main method is:

Add()

Example:

numbers.Add(10);
numbers.Add(20);
numbers.Add(30);

Result:

10
20
30

-----------------------------------------------------------------------------------------------------

10. Adding Multiple Elements

Use:

AddRange()

Example:

numbers.AddRange(new[] { 40, 50, 60 });

Now:

10
20
30
40
50
60

-----------------------------------------------------------------------------------------------------

11. Accessing Elements

Use an index:

numbers[0]

Example:

Index:   0   1   2
Value:  10  20  30

Therefore:

numbers[0] → 10
numbers[1] → 20
numbers[2] → 30

This is generally O(1).

Why?

Because the underlying storage is array-based.

-------------------------------------------------------------------------------------------------

12. Updating an Element

You can directly assign a new value:

numbers[1] = 200;

Before:

10  20  30

After:

10  200  30

Index-based replacement is generally:

O(1).

-------------------------------------------------------------------------------------------------

13. Count

Count tells us the number of elements currently stored.

numbers.Count

Example:

Elements:
10
20
30

Count = 3

Important:

Count = number of elements currently present.


------------------------------------------------------------------------------------------------------
14. Capacity

This is an important interview concept.

Capacity represents the amount of internal storage currently allocated by the List<T>.

For example, conceptually:

Count = 3
Capacity = 4

means:

Currently used:
[10][20][30]

Available internal space:
[   ]

Therefore:

Count tells you how many elements exist. Capacity tells you how many elements the current internal storage can hold before resizing is required.

Don't confuse:

Count ≠ Capacity

----------------------------------------------------------------------------------------------------------------------------
15. Count vs Capacity

| Property   | Meaning                             |
| ---------- | ----------------------------------- |
| `Count`    | Number of elements currently stored |
| `Capacity` | Current internal storage capacity   |

Example:

List<int> numbers = new();

numbers.Add(10);
numbers.Add(20);
numbers.Add(30);

Possible state:

Count    = 3
Capacity = 4

The exact capacity growth behavior is implementation-dependent, so don't build application logic around a specific growth factor.

----------------------------------------------------------------------------------------------------------------------------------------

16. Removing Elements
Remove by value
numbers.Remove(20);

It searches for the first matching element and removes it.

Important:

Remove() needs to find the element first.

Therefore, for a List<T>, this is generally O(n).

Remove by index
numbers.RemoveAt(1);

Example:

Before:

10  20  30  40
    ↑
   index 1

After:

10  30  40

Elements after the removed position generally need to shift left.

Therefore:

O(n) in the general case.

---------------------------------------------------------------------------------------------------------
17. Remove the Last Element
numbers.RemoveAt(numbers.Count - 1);

Removing the last element doesn't require shifting later elements.

Therefore, it is generally:

O(1).

This distinction is useful in DSA.

----------------------------------------------------------------------------------------------------------
18. Searching
Contains()
numbers.Contains(30);

Returns:

true

if the value exists.

For a normal List<T>, this is generally:

O(n).

Why?

It may need to inspect every element.

--------------------------------------------------------------------------------------------------------------
19. IndexOf()
int index = numbers.IndexOf(30);

It returns the index of the first occurrence.

Example:

10  20  30  40
         ↑
       index 2

Result:

2

Generally:

O(n).

----------------------------------------------------------------------------------------------------------

20. Sorting
numbers.Sort();

Example:

Before:

40  10  30  20

After:

10  20  30  40

The exact algorithm/implementation details should not be assumed from the method name alone;
what matters for interview purposes is that sorting is not O(1),
and you should understand the algorithmic cost when analyzing your own DSA solution.

-----------------------------------------------------------------------------------------------------------------
21. Reversing
numbers.Reverse();

Example:

10 20 30 40

becomes:

40 30 20 10

-------------------------------------------------------------------------------------------------------------------

22. Clearing the List
numbers.Clear();

After:

Count = 0

An important point:

Clear() removes the elements, but it does not necessarily reduce the internal capacity to zero.

So you can have:

Count = 0
Capacity = some existing value

------------------------------------------------------------------------------------------------------------------------

23. Contains() vs IndexOf()

Both can search a list.

numbers.Contains(20);

asks:

Does this value exist?

Whereas:

numbers.IndexOf(20);

asks:

Where is the first occurrence?

Both are generally:

O(n).

-------------------------------------------------------------------------------------------------------------------------

24. Iterating Through a List

The most common approach:

foreach (int number in numbers)
{
    // use number
}

You can also use a for loop when you need the index:

for (int i = 0; i < numbers.Count; i++)
{
    // use numbers[i]
}

------------------------------------------------------------------------------------------------------------------------------

25. Important List<T> Methods

You should know these first:

| Method/Property | Purpose                     |
| --------------- | --------------------------- |
| `Add()`         | Add one element             |
| `AddRange()`    | Add multiple elements       |
| `Insert()`      | Insert at an index          |
| `InsertRange()` | Insert multiple elements    |
| `Remove()`      | Remove first matching value |
| `RemoveAt()`    | Remove by index             |
| `RemoveRange()` | Remove a range              |
| `Contains()`    | Check existence             |
| `IndexOf()`     | Find first index            |
| `Sort()`        | Sort elements               |
| `Reverse()`     | Reverse elements            |
| `Clear()`       | Remove all elements         |
| `Count`         | Number of elements          |
| `Capacity`      | Internal storage capacity   |

----------------------------------------------------------------------------------------------------------------------------------------
26. Insert() — Important for DSA

Suppose:

10  20  30

We execute:

numbers.Insert(1, 99);

Result:

10  99  20  30

Elements from index 1 onward must generally shift.

Therefore:

O(n).

This is an important reason why List<T> is not ideal when you frequently insert into the middle.

------------------------------------------------------------------------------------------------------------------------------

27. Internal Working of List<T>

Now we reach an important DSA concept.

A List<T> is backed by an array internally.

Conceptually:

List<T>
   │
   ▼
Internal Array
   │
   ├── [0]
   ├── [1]
   ├── [2]
   └── [3]

Suppose:

Count = 3
Capacity = 4

and we add another item.

No resize is necessary.

But if capacity is already full:

Count = 4
Capacity = 4

and we add another item:

numbers.Add(50);

the list must obtain a larger backing array and move/copy the existing elements into it.

Conceptually:

Old Array
[10][20][30][40]

       ↓ resize

New Array
[10][20][30][40][50][ ][ ]

The exact capacity growth policy is an implementation detail and can vary by runtime/version.

--------------------------------------------------------------------------------------------------------------------
28. Why Is List[index] O(1)?

Because the underlying storage is array-based.

Conceptually:

Base Address
     +
index × element-size
     ↓
target element

So:

numbers[3]

doesn't normally need to search:

0 → 1 → 2 → 3

It can directly calculate where the element resides in the backing array.

That's why:

Index access is O(1).

This is the same array-access concept you'll use heavily in DSA.

------------------------------------------------------------------------------------------------------------------

29. Time Complexity

Important operations:

| Operation             | Typical Complexity |
| --------------------- | -----------------: |
| Index access          |           **O(1)** |
| Update by index       |           **O(1)** |
| Add at end            | **O(1) amortized** |
| Remove last           |           **O(1)** |
| Search                |           **O(n)** |
| `Contains()`          |           **O(n)** |
| `IndexOf()`           |           **O(n)** |
| Insert at beginning   |           **O(n)** |
| Insert in middle      |           **O(n)** |
| Remove from beginning |           **O(n)** |
| Remove from middle    |           **O(n)** |
| Iterate all elements  |           **O(n)** |


Why "amortized" for Add?

Most individual Add() operations are O(1), but occasionally a resize requires copying elements, which is O(n).

Across a long sequence of additions, the amortized cost per append is O(1).

This is a very important DSA concept.

----------------------------------------------------------------------------------------------------------------------------

30. Advantages
1. Dynamic size

You don't have to know the final number of elements beforehand.

2. Fast index access
numbers[index]

is generally O(1).

3. Type safety
List<int>

only accepts integers.

4. Rich API

It provides many useful methods.

5. Easy to use

It is one of the most commonly used collections in C#.

6. Excellent for DSA

Many array-based problems can conveniently be implemented using List<T>.

---------------------------------------------------------------------------------------------------------------------------------------

31. Disadvantages
1. Middle insertion is expensive
O(n)

because elements may need to shift.

2. Middle deletion is expensive

Again, elements may need to shift.

3. Searching is linear
Contains → O(n)
IndexOf  → O(n)

If you need fast membership lookup, HashSet<T> may be better.

4. Resizing has a cost

Occasional resizing requires allocating a larger backing array and copying elements.

5. Not ideal for every access pattern

If your requirement is:

FIFO

use:

Queue<T>

If:

LIFO

use:

Stack<T>

If:

Key → Value

use:

Dictionary<TKey,TValue>

-----------------------------------------------------------------------------------------
32. When Should You Use List<T>?

Use it when:

✅ You need ordered elements
A
B
C
D
✅ You need index-based access
items[5]
✅ You frequently append to the end
items.Add(item);
✅ The collection size changes
✅ You need a simple general-purpose collection
✅ You are processing API results
List<Customer>
✅ You need array-like behavior with a dynamic size

----------------------------------------------------------------------------------------------------

33. When Should You NOT Use List<T>?
 
 
❌ Frequent lookup by key

Instead consider:

Dictionary<TKey,TValue>


❌ Only unique values

Consider:

HashSet<T>
❌ FIFO processing

Use:

Queue<T>
❌ LIFO processing

Use:

Stack<T>
❌ Frequent insertion/removal in the middle

A different data structure may be more suitable depending on the exact access pattern.

❌ You know the exact fixed size and don't need resizing

An array may be simpler and more appropriate.

-------------------------------------------------------------------------------------------------------------
34. List<T> vs Array

| Feature      | Array          | `List<T>`        |
| ------------ | -------------- | ---------------- |
| Size         | Fixed          | Dynamic          |
| Index access | O(1)           | O(1)             |
| Add at end   | Manual/fixed   | `Add()`          |
| Remove       | Manual         | Built-in methods |
| API          | Smaller        | Richer           |
| Resizing     | Manual         | Automatic        |
| Type safety  | Yes            | Yes              |
| DSA          | Very important | Very important   |


Important:

List<T> doesn't replace arrays in DSA. You should understand both.

---------------------------------------------------------------------------------------------------------------------

35. List<T> vs LinkedList<T>

This is an important interview comparison.

| Feature              | `List<T>`         | `LinkedList<T>`               |
| -------------------- | ----------------- | ----------------------------- |
| Internal structure   | Array-backed      | Linked nodes                  |
| Index access         | O(1)              | O(n)                          |
| Append               | O(1) amortized    | O(1) when adding at known end |
| Insert at known node | Requires shifting | O(1)                          |
| Search               | O(n)              | O(n)                          |
| Memory locality      | Better            | Generally worse               |
| Random access        | Excellent         | Poor                          |


This is one reason "LinkedList insertion is O(1)" is an incomplete interview answer.

You must specify:

Insertion is O(1) when you already have the relevant node/reference; finding that location may take O(n).

--------------------------------------------------------------------------------------------------------------------

36. DSA Connection

List<T> is especially useful for:

Arrays
Traversal
Searching
Sorting
Two Pointer
left →      ← right
Sliding Window
[left ........ right]
Prefix Sum
prefix[i]
Binary Search

Because of efficient index access.

Dynamic arrays

Understanding List<T> helps you understand how dynamic arrays work.

--------------------------------------------------------------------------------------------------------------------------

37. Common Interview Trap
Question:

Is List<T>.Add() always O(1)?

❌ Don't say:

Yes.

Better answer:

"Appending to a List is O(1) amortized. Most appends are O(1), but when the backing array needs to resize,
that particular operation can take O(n) because existing elements must be copied."

That's a much stronger answer.

--------------------------------------------------------------------------------------------------------------------------

38. Another Interview Trap
Question:

Is List<T> implemented using a linked list?

❌ No.

List<T> is array-backed.

List<T>
   ↓
Backing array

Whereas:

LinkedList<T>
   ↓
Linked nodes

-------------------------------------------------------------------------------------------------------------------------
39. Interview Questions & Answers
 
Q1. What is List<T>?

List<T> is a generic, dynamically sized, array-backed collection that provides type-safe storage and index-based access.

Q2. Is List<T> thread-safe?

No. A normal List<T> isn't designed for concurrent mutation from multiple threads; appropriate synchronization or concurrent collections may be required.

Q3. What is the difference between Count and Capacity?

Count is the number of elements currently stored, while Capacity is the size of the internal storage currently allocated for the list.

Q4. Why is index access O(1)?

Because List<T> uses an array internally, allowing direct index-based access.

Q5. What happens when List capacity is exceeded?

It allocates a larger backing array and copies the existing elements into it.

Q6. What is the complexity of Contains()?

Generally O(n), because the list may need to inspect each element.

Q7. What is the complexity of inserting at index 0?

Generally O(n), because existing elements need to be shifted.

Q8. What is amortized O(1)?

It means that although some individual Add() operations can be expensive due to resizing, the average cost per append over many operations remains O(1).

Q9. When would you choose HashSet over List?

When the primary requirement is fast membership checking and uniqueness rather than ordered/index-based access.

Q10. When would you choose Dictionary over List?

When the primary operation is retrieving values using a key rather than searching sequentially by position or value.

----------------------------------------------------------------------------------------------------------------------------------------------

40. Common Mistakes
Mistake 1

Using List<T> for every problem.

Better: Choose based on required operations.

Mistake 2

Thinking:

List = LinkedList

They are completely different internal structures.

Mistake 3

Saying Add() is always O(1).

Correct:

O(1) amortized.

Mistake 4

Thinking Count and Capacity are the same.

They aren't.

Mistake 5

Using List.Contains() when you repeatedly need membership checks.

Consider whether:

HashSet<T>

would be more appropriate.

------------------------------------------------------------------------------------------------

