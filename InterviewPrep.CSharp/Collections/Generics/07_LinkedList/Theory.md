Collection Type: ✅ Generic Collection
Namespace: System.Collections.Generic
Category: Linear / node-based collection
DSA importance: ⭐⭐⭐⭐⭐
Interview importance: ⭐⭐⭐⭐

-------------------------------------------------------------------------------------------------------------------------------------------------------


1. Definition

LinkedList<T> is a generic collection made up of nodes, where each node stores a value and references the previous and next nodes.

Conceptually:

┌───────┐      ┌───────┐      ┌───────┐
│  10   │ ───→ │  20   │ ───→ │  30   │
└───────┘      └───────┘      └───────┘
     ←───────────────←───────────────

Each node is connected to another node.

Unlike List<T>, elements aren't conceptually stored as one contiguous sequence of values that you access by index.

-------------------------------------------------------------------------------------------------------------------------------------------------------


2. Generic or Non-Generic?

For your VS 2022 folder structure:

Collections
│
├── Generic
│   ├── List
│   ├── Dictionary
│   ├── HashSet
│   ├── SortedSet
│   ├── Stack
│   ├── Queue
│   └── LinkedList
│
└── NonGeneric

LinkedList<T> is:

✅ Generic

-------------------------------------------------------------------------------------------------------------------------------------------------------


3. Why Do We Need LinkedList<T>?

Suppose you frequently need to insert or remove elements in the middle of a sequence, and you already have a reference to the location/node.

A linked list can do this efficiently because it changes links rather than shifting all subsequent elements.

For example:

Before:

10 ↔ 20 ↔ 40

Insert 30 between 20 and 40:

After:

10 ↔ 20 ↔ 30 ↔ 40

The links are adjusted.

With an array-backed List<T>, inserting into the middle generally requires shifting subsequent elements.

-------------------------------------------------------------------------------------------------------------------------------------------------------


4. Real-Life Example — Train Coaches

Imagine:

Engine ↔ Coach A ↔ Coach B ↔ Coach C

Each coach is connected to neighboring coaches.

If you have the correct coach reference and need to insert a new coach:

Coach A ↔ Coach B ↔ Coach C

becomes:

Coach A ↔ Coach X ↔ Coach B ↔ Coach C

This is a good mental model for a doubly linked list.

-------------------------------------------------------------------------------------------------------------------------------------------------------


5. Important: .NET LinkedList<T> Is Doubly Linked

This is extremely important.

LinkedList<T> in .NET is a doubly linked list.

Each node conceptually contains:

Previous
   ↓
┌─────────────────┐
│ Value           │
│ Next            │
└─────────────────┘

So:

Node A
   ↕
Node B
   ↕
Node C

You can move:

Forward →
Backward ←

-------------------------------------------------------------------------------------------------------------------------------------------------------


6. LinkedListNode<T>

This is the object representing an individual node.

Example:

LinkedListNode<int> node;

A node provides:

Value
Next
Previous
List

So if:

10 ↔ 20 ↔ 30

the node containing 20 knows:

Previous → 10
Value    → 20
Next     → 30

-------------------------------------------------------------------------------------------------------------------------------------------------------
7. Creating a Linked List
LinkedList<int> numbers = new();

You can also initialize:

LinkedList<int> numbers = new()
{
    10,
    20,
    30
};

-------------------------------------------------------------------------------------------------------------------------------------------------------
8. AddFirst()

Adds an element at the beginning.

numbers.AddFirst(10);

Suppose:

20 ↔ 30

After:

10 ↔ 20 ↔ 30

-------------------------------------------------------------------------------------------------------------------------------------------------------
9. AddLast()

Adds an element at the end.

numbers.AddLast(40);

Before:

10 ↔ 20 ↔ 30

After:

10 ↔ 20 ↔ 30 ↔ 40

-------------------------------------------------------------------------------------------------------------------------------------------------------
10. AddBefore()

This is one of the most important methods.

Suppose:

10 ↔ 20 ↔ 30

You have a node representing 30.

LinkedListNode<int> node30 = numbers.Find(30)!;

numbers.AddBefore(node30, 25);

Result:

10 ↔ 20 ↔ 25 ↔ 30

The important point:

You provide the node before which the new value should be inserted.

-------------------------------------------------------------------------------------------------------------------------------------------------------

11. AddAfter()

Similarly:

numbers.AddAfter(node30, 35);

Result:

10 ↔ 20 ↔ 30 ↔ 35

So:

AddBefore(node, value)
       ↓
insert before node

AddAfter(node, value)
       ↓
insert after node

-------------------------------------------------------------------------------------------------------------------------------------------------------
12. Find()

Searches for the first node containing a value.

LinkedListNode<int>? node = numbers.Find(30);

If found:

node → node containing 30

If not found:

null

This is important because Find() gives you a node, not merely the value.

-------------------------------------------------------------------------------------------------------------------------------------------------------

13. FindLast()

Searches from the end and returns the last node containing the specified value.

LinkedListNode<int>? node = numbers.FindLast(30);

Example:

10 ↔ 20 ↔ 30 ↔ 20 ↔ 30

Find(30) returns the first 30.

FindLast(30) returns the second 30.

-------------------------------------------------------------------------------------------------------------------------------------------------------

14. Remove(T value)

Removes the first occurrence of a value.

numbers.Remove(20);

Example:

10 ↔ 20 ↔ 30

becomes:

10 ↔ 30

Returns:

true

if removed.

Otherwise:

false

-------------------------------------------------------------------------------------------------------------------------------------------------------
15. Remove(LinkedListNode<T>)

You can also remove a specific node:

numbers.Remove(node30);

This is one of the important advantages of having direct node references.

-------------------------------------------------------------------------------------------------------------------------------------------------------

16. RemoveFirst()

Removes the first node.

numbers.RemoveFirst();

Example:

10 ↔ 20 ↔ 30

becomes:

20 ↔ 30

-------------------------------------------------------------------------------------------------------------------------------------------------------
17. RemoveLast()

Removes the last node.

numbers.RemoveLast();

Example:

10 ↔ 20 ↔ 30

becomes:

10 ↔ 20

-------------------------------------------------------------------------------------------------------------------------------------------------------
18. First

Gets the first node.

LinkedListNode<int>? first = numbers.First;

If:

10 ↔ 20 ↔ 30

then:

first.Value = 10

-------------------------------------------------------------------------------------------------------------------------------------------------------
19. Last

Gets the last node.

LinkedListNode<int>? last = numbers.Last;

Then:

last.Value

would be:

30

-------------------------------------------------------------------------------------------------------------------------------------------------------
20. Count
numbers.Count

returns the number of nodes.

For:

10 ↔ 20 ↔ 30

result:

3

-------------------------------------------------------------------------------------------------------------------------------------------------------
21. Clear()

Removes all nodes.

numbers.Clear();

After:

Count = 0

-------------------------------------------------------------------------------------------------------------------------------------------------------
22. Contains()

Checks whether a value exists.

numbers.Contains(20);

Returns:

true

or:

false

Searching is generally:

O(n)

because you may need to traverse the nodes.

-------------------------------------------------------------------------------------------------------------------------------------------------------

23. CopyTo()

Copies elements into an array.

int[] array = new int[numbers.Count];

numbers.CopyTo(array, 0);

-------------------------------------------------------------------------------------------------------------------------------------------------------
24. Reverse()

LinkedList<T> itself doesn't provide a Reverse() mutating method like some people expect.

But LINQ can enumerate it in reverse:

foreach (int number in numbers.Reverse())
{
    Console.WriteLine(number);
}

You would need:

using System.Linq;

This creates a reverse enumeration rather than changing the linked list itself.

-------------------------------------------------------------------------------------------------------------------------------------------------------

25. Complete Important API

For your VS 2022 folder, remember:

Adding
AddFirst()
AddLast()
AddBefore()
AddAfter()
Searching
Find()
FindLast()
Contains()
Removing
Remove()
Remove(node)
RemoveFirst()
RemoveLast()
Nodes
First
Last
Information
Count
Management
Clear()
Copy
CopyTo()

-------------------------------------------------------------------------------------------------------------------------------------------------------
26. The Most Important Concept — Node

Suppose:

10 ↔ 20 ↔ 30

The 20 node conceptually looks like:

┌───────────────┐
│ Previous ─────┼────→ 10
│ Value         │
│     20        │
│ Next ─────────┼────→ 30
└───────────────┘

This is why a linked list is called a linked data structure.

The nodes are connected through references.

-------------------------------------------------------------------------------------------------------------------------------------------------------

27. How Insertion Works

Suppose:

10 ↔ 20 ↔ 40

We want:

10 ↔ 20 ↔ 30 ↔ 40

The links conceptually change from:

20.Next → 40
40.Previous → 20

to:

20.Next → 30
30.Previous → 20

30.Next → 40
40.Previous → 30

That's the fundamental linked-list operation.

-------------------------------------------------------------------------------------------------------------------------------------------------------

28. Why Is Insertion O(1)?

Here's an important interview nuance.

If you already have the node where you want to insert:

numbers.AddAfter(node20, 30);

the insertion itself is:

O(1)

because only a few links need to be changed.

But if you first need to search for 20:

var node20 = numbers.Find(20);
numbers.AddAfter(node20!, 30);

then:

Find()      → O(n)
AddAfter()  → O(1)
--------------------
Overall     → O(n)

This distinction is very important in interviews.

-------------------------------------------------------------------------------------------------------------------------------------------------------

29. Time Complexity

| Operation         | Complexity |
| ----------------- | ---------: |
| `AddFirst()`      |       O(1) |
| `AddLast()`       |       O(1) |
| `RemoveFirst()`   |       O(1) |
| `RemoveLast()`    |       O(1) |
| `AddBefore(node)` |       O(1) |
| `AddAfter(node)`  |       O(1) |
| `Remove(node)`    |       O(1) |
| `Find()`          |       O(n) |
| `FindLast()`      |       O(n) |
| `Contains()`      |       O(n) |
| `First`           |       O(1) |
| `Last`            |       O(1) |
| `Count`           |       O(1) |
| `CopyTo()`        |       O(n) |
| `Clear()`         |       O(n) |


Again:

The O(1) insertion/removal advantage assumes you already have the relevant node.

-------------------------------------------------------------------------------------------------------------------------------------------------------

30. Does LinkedList<T> Have Index Access?

No.

You cannot do:

numbers[3]

This is one of the biggest differences from:

List<T>

If you need:

list[index]

use List<T>.

-------------------------------------------------------------------------------------------------------------------------------------------------------

31. Why Can't LinkedList Have O(1) Index Access?

Suppose:

10 ↔ 20 ↔ 30 ↔ 40 ↔ 50

You want index 4.

A linked list doesn't directly calculate:

address = base + index × size

like an array-backed structure.

Instead, it must traverse:

10
 ↓
20
 ↓
30
 ↓
40
 ↓
50

Therefore random access is not its strength.

-------------------------------------------------------------------------------------------------------------------------------------------------------

32. List<T> vs LinkedList<T>

This is one of the most important collection comparisons.

| Feature                    | `List<T>`        | `LinkedList<T>` |
| -------------------------- | ---------------- | --------------- |
| Storage model              | Array-backed     | Node-based      |
| Index access               | ✅ O(1)           | ❌               |
| Search                     | O(n)             | O(n)            |
| Add at end                 | O(1) amortized   | O(1)            |
| Add at beginning           | O(n)             | O(1)            |
| Remove first               | O(n)             | O(1)            |
| Insert with node/reference | Usually shifting | O(1)            |
| Memory overhead            | Lower            | Higher          |
| Cache locality             | Better           | Worse           |
| Random access              | Excellent        | Poor            |

-------------------------------------------------------------------------------------------------------------------------------------------------------


33. Very Important: Is LinkedList<T> Usually Better Than List<T> for Insertions?

Not automatically.

This is a common interview trap.

People often say:

"LinkedList is faster for insertion."

That's incomplete.

Suppose you need to insert 30 after 20.

If you already have the node:

O(1)

But if you only know:

value = 20

you need:

Find(20)

which is:

O(n)

So the complete operation can become:

O(n) + O(1) = O(n)

-------------------------------------------------------------------------------------------------------------------------------------------------------
34. Why List<T> Can Be Faster in Practice

Even though linked lists have O(1) insertion after a known node, List<T> often performs better in many real-world workloads.

Why?

Because List<T> is backed by contiguous memory.

This gives better:

CPU cache locality

and lower per-element memory overhead.

Linked lists have separate node objects and references.

So:

Big-O complexity isn't the only factor in real-world performance.

That's an excellent product-company interview point.

-------------------------------------------------------------------------------------------------------------------------------------------------------

35. Memory Structure
List<T>

Conceptually:

┌────┬────┬────┬────┬────┐
│ 10 │ 20 │ 30 │ 40 │ 50 │
└────┴────┴────┴────┴────┘
LinkedList<T>

Conceptually:

┌──────┐      ┌──────┐      ┌──────┐
│ 10   │ ───→ │ 20   │ ───→ │ 30   │
└──────┘      └──────┘      └──────┘
   ↑              ↑              ↑
 separate       separate       separate
 node           node           node

The linked-list nodes aren't required to be adjacent in memory.

-------------------------------------------------------------------------------------------------------------------------------------------------------

36. Advantages of LinkedList<T>
✅ O(1) insertion/removal with known node
AddAfter(node)
AddBefore(node)
Remove(node)
✅ O(1) first/last operations
AddFirst()
AddLast()
RemoveFirst()
RemoveLast()
✅ Bidirectional traversal

Because it's doubly linked:

Previous
Next
✅ Useful for certain DSA problems

Especially when nodes and links are central to the algorithm.

-------------------------------------------------------------------------------------------------------------------------------------------------------

37. Disadvantages
❌ No index access
list[5]

isn't available.

❌ Searching is O(n)

You may need to traverse nodes.

❌ Higher memory usage

Every node has additional references.

Conceptually:

Previous
Value
Next
❌ Worse cache locality

Nodes may be scattered in memory.

❌ Often not the default choice

For many everyday application scenarios, List<T> is usually more convenient and often faster.

-------------------------------------------------------------------------------------------------------------------------------------------------------

38. When Should You Use LinkedList<T>?

Use it when your workload naturally involves:

Frequent insertion/removal
+
Known node/reference
+
Sequential traversal

Good examples:

✓ Certain queue/deque implementations
✓ LRU cache internals
✓ Playlist manipulation
✓ Browser-like linked navigation models
✓ DSA linked-list algorithms

-------------------------------------------------------------------------------------------------------------------------------------------------------
39. When Should You NOT Use It?

Don't choose it simply because:

"I need to insert data."

Instead ask:

Need index access?
→ List<T>
Need fast key lookup?
→ Dictionary<TKey,TValue>
Need uniqueness?
→ HashSet<T>
Need FIFO?
→ Queue<T>
Need LIFO?
→ Stack<T>
Need sorted unique values?
→ SortedSet<T>

-------------------------------------------------------------------------------------------------------------------------------------------------------
40. Real-Time Example — Music Playlist

Suppose:

Song A ↔ Song B ↔ Song C

You want to insert a song after Song B:

Song A ↔ Song B ↔ Song X ↔ Song C

If you already have a reference to Song B's node:

playlist.AddAfter(songBNode, "Song X");

The link manipulation is O(1).

This is a useful conceptual example.

-------------------------------------------------------------------------------------------------------------------------------------------------------

41. Real-Time Example — LRU Cache

A classic system-design use case is an LRU (Least Recently Used) cache.

A common design uses:

Dictionary<TKey, LinkedListNode<T>>
+
LinkedList<T>

Why?

The dictionary provides:

O(1) lookup

and the linked list provides:

O(1) movement/removal

when you already have the node.

Conceptually:

Dictionary
     │
     ▼
find node quickly
     │
     ▼
LinkedList
     │
     ▼
move/remove node quickly

This is an excellent Microsoft/Amazon/Google system-design interview concept.

-------------------------------------------------------------------------------------------------------------------------------------------------------

42. DSA Importance

LinkedList<T> is particularly important because linked lists are a major DSA topic.

You should eventually be able to implement:

Singly Linked List
Doubly Linked List
Circular Linked List

without relying on .NET's built-in collection.

The built-in LinkedList<T> helps you understand the API and behavior, but your DSA preparation should also include implementing linked lists yourself.

-------------------------------------------------------------------------------------------------------------------------------------------------------

43. Interview Question
Q: What is LinkedList<T>?

Answer:

LinkedList<T> is a generic doubly linked collection consisting of nodes, where each node maintains references to the previous and next nodes.
It provides O(1) insertion and removal when the relevant node is already known.

-------------------------------------------------------------------------------------------------------------------------------------------------------

44. Interview Question
Q: Why is AddAfter(node, value) O(1)?

Because the target node is already known.

The implementation only needs to adjust a small number of references:

Previous
Next

It doesn't need to shift all subsequent elements.

-------------------------------------------------------------------------------------------------------------------------------------------------------

45. Interview Question
Q: Is Find() O(1)?

No.

Find() → O(n)

because the list may need to traverse nodes sequentially.

-------------------------------------------------------------------------------------------------------------------------------------------------------

46. Interview Question
Q: Does LinkedList<T> support index access?

No.

It doesn't provide:

list[index]

because linked lists don't provide direct random access.

-------------------------------------------------------------------------------------------------------------------------------------------------------

47. Interview Question
Q: Why can List<T> be faster than LinkedList<T> even for some insertion-heavy workloads?

Because:

List<T> has contiguous storage.
It has better CPU cache locality.
It has lower per-element memory overhead.
Linked-list nodes require additional references and allocations.

And if the linked list requires a search before insertion:

Find → O(n)
Insert → O(1)

the overall operation is still:

O(n)

-------------------------------------------------------------------------------------------------------------------------------------------------------
48. Interview Question
Q: Why is LinkedList<T> called doubly linked?

Because each node maintains two directions:

Previous
   ↑
Node
   ↓
Next

Therefore you can traverse:

forward →
backward ←

-------------------------------------------------------------------------------------------------------------------------------------------------------
49. Interview Question
Q: LinkedList<T> or List<T>?

A strong answer:

I would choose List<T> by default when I need indexed access, compact storage, and good sequential performance.
I would consider LinkedList<T> when I have frequent insertion/removal around known nodes and don't need random access.

That is much better than simply saying:

"LinkedList is faster for insertion."