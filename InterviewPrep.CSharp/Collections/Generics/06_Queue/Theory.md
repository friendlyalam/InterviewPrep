Collection Type: ✅ Generic Collection
Namespace: System.Collections.Generic
Category: Linear collection
Core principle: FIFO — First In, First Out
DSA importance: ⭐⭐⭐⭐⭐
Interview importance: ⭐⭐⭐⭐⭐

-----------------------------------------------------------------------------------------------------------------------------------
1. Definition

Queue<T> is a generic collection that follows the FIFO principle, meaning the element added first is the first element removed.

Simple example:

Enqueue:
10
20
30

Conceptually:

FRONT                         REAR
  ↓                             ↓
┌────┬────┬────┐
│ 10 │ 20 │ 30 │
└────┴────┴────┘
  ↑
Dequeue first

So:

Enqueue(10)
Enqueue(20)
Enqueue(30)

Dequeue() → 10
Dequeue() → 20
Dequeue() → 30

-----------------------------------------------------------------------------------------------------------------------------------
2. What Does FIFO Mean?

FIFO = First In, First Out

If elements enter in this order:

A → B → C → D

they leave in exactly the same order:

A → B → C → D

The first element that entered is the first element that leaves.

-----------------------------------------------------------------------------------------------------------------------------------

3. Real-Time Example — Bank Queue

Imagine a bank:

Customer A
Customer B
Customer C
Customer D

A arrives first.

Therefore:

A → served first
B → served second
C → served third
D → served fourth

This is FIFO.

A queue is therefore a natural data structure for:

customer waiting lines
print jobs
task processing
message processing
request processing

-----------------------------------------------------------------------------------------------------------------------------------
4. Queue vs Stack

This is extremely important for interviews.

Stack
LIFO

A
B
C

Pop → C
Queue
FIFO

A → B → C

Dequeue → A

Remember:

Stack = Last In, First Out

Queue = First In, First Out

-----------------------------------------------------------------------------------------------------------------------------------

5. Basic Syntax
Queue<int> numbers = new();

String queue:

Queue<string> customers = new();

Custom objects:

Queue<Order> orders = new();

-----------------------------------------------------------------------------------------------------------------------------------
6. Main Operations

The most important methods/properties are:

Enqueue()
Dequeue()
Peek()
TryDequeue()
TryPeek()
Contains()
Clear()
Count
ToArray()
CopyTo()
EnsureCapacity()
TrimExcess()

Let's understand each one.

-----------------------------------------------------------------------------------------------------------------------------------

7. Enqueue()

Enqueue() adds an element to the rear/end of the queue.

Queue<int> numbers = new();

numbers.Enqueue(10);
numbers.Enqueue(20);
numbers.Enqueue(30);

Conceptually:

FRONT                    REAR
  ↓                        ↓
┌────┬────┬────┐
│ 10 │ 20 │ 30 │
└────┴────┴────┘

The next element removed will be 10.

-----------------------------------------------------------------------------------------------------------------------------------

8. Dequeue()

Dequeue():

Removes and returns the element at the front.

int value = numbers.Dequeue();

Before:

10 → 20 → 30

After:

20 → 30

And:

value = 10

So:

Dequeue
   ↓
Read front
   +
Remove front

-----------------------------------------------------------------------------------------------------------------------------------
9. Peek()

Peek():

Returns the front element without removing it.

int value = numbers.Peek();

If:

10 → 20 → 30

then:

Peek() → 10

but the queue remains:

10 → 20 → 30

-----------------------------------------------------------------------------------------------------------------------------------
10. Dequeue() vs Peek()

Very important interview question.

| Method      | Returns front | Removes front |
| ----------- | ------------: | ------------: |
| `Peek()`    |             ✅ |             ❌ |
| `Dequeue()` |             ✅ |             ✅ |


Mental shortcut:

Peek
 ↓
Look

Dequeue
 ↓
Take it out

-----------------------------------------------------------------------------------------------------------------------------------
11. Count
numbers.Count

returns the number of elements currently in the queue.

Example:

10 → 20 → 30
numbers.Count

returns:

3

Count is an O(1) operation.

-----------------------------------------------------------------------------------------------------------------------------------

12. Contains()

Checks whether an element exists.

numbers.Contains(20);

Returns:

true

or:

false

Important:

Queue<T> isn't designed primarily for fast arbitrary searching.

Contains() is generally O(n).

If your main requirement is:

"Does this value exist?"

consider whether HashSet<T> is a better fit.

-----------------------------------------------------------------------------------------------------------------------------------

13. Clear()

Removes all elements.

numbers.Clear();

After:

numbers.Count

returns:

0

-----------------------------------------------------------------------------------------------------------------------------------
14. TryDequeue()

This is extremely useful.

If you use:

numbers.Dequeue();

on an empty queue, an exception is thrown.

Instead:

if (numbers.TryDequeue(out int value))
{
    Console.WriteLine(value);
}

If an element exists:

true
value = front element

If the queue is empty:

false

No exception is required for the normal empty case.

-----------------------------------------------------------------------------------------------------------------------------------

15. TryPeek()

Similarly:

if (numbers.TryPeek(out int value))
{
    Console.WriteLine(value);
}

If the queue isn't empty:

true
value = front element

If empty:

false

And the element is not removed.

-----------------------------------------------------------------------------------------------------------------------------------

16. Why TryDequeue() Is Useful

Instead of:

if (numbers.Count > 0)
{
    int value = numbers.Dequeue();
}

you can write:

if (numbers.TryDequeue(out int value))
{
    Console.WriteLine(value);
}

This directly expresses:

"Try to remove the next available item."

-----------------------------------------------------------------------------------------------------------------------------------

17. ToArray()

Converts the queue into an array.

int[] array = numbers.ToArray();

The resulting sequence follows the queue's logical dequeue order.

For:

10 → 20 → 30

the array is conceptually:

[10, 20, 30]

-----------------------------------------------------------------------------------------------------------------------------------
18. CopyTo()

Copies queue elements into an existing array.

int[] array = new int[numbers.Count];

numbers.CopyTo(array, 0);

The second parameter is the destination array index where copying begins.

-----------------------------------------------------------------------------------------------------------------------------------

19. EnsureCapacity()
numbers.EnsureCapacity(100);

Ensures internal storage can accommodate at least the requested number of elements.

This can be useful if you know approximately how many items will be queued.

-----------------------------------------------------------------------------------------------------------------------------------

20. TrimExcess()
numbers.TrimExcess();

Attempts to reduce unused internal storage.

Don't call this after every Dequeue().

It is an optimization tool, not a normal queue-processing operation.

-----------------------------------------------------------------------------------------------------------------------------------

21. Complete Built-In Methods

For your Generic Collections → Queue folder, keep:

Core operations
Enqueue()
Dequeue()
Peek()
TryDequeue()
TryPeek()
Searching
Contains()
Information
Count
Management
Clear()
EnsureCapacity()
TrimExcess()
Conversion/copy
ToArray()
CopyTo()

-----------------------------------------------------------------------------------------------------------------------------------
22. Time Complexity

For the main operations:

| Operation      |     Complexity |
| -------------- | -------------: |
| `Enqueue()`    | O(1) amortized |
| `Dequeue()`    |           O(1) |
| `Peek()`       |           O(1) |
| `TryDequeue()` |           O(1) |
| `TryPeek()`    |           O(1) |
| `Count`        |           O(1) |
| `Contains()`   |           O(n) |
| `ToArray()`    |           O(n) |
| `CopyTo()`     |           O(n) |
| `Clear()`      |           O(n) |


Again, Enqueue() is generally O(1) amortized because internal storage may occasionally need to grow.

-----------------------------------------------------------------------------------------------------------------------------------

23. Why Is Enqueue() O(1) Amortized?

Imagine internal storage:

[10][20][30][40]

If it becomes full and you add:

50

the queue may need to expand its internal storage.

That particular operation can be more expensive.

But over many enqueue operations, the average cost is:

O(1) amortized

-----------------------------------------------------------------------------------------------------------------------------------
24. Internal Structure — Important for Interviews

A common mistake is saying:

"Queue<T> is implemented using a linked list."

That's not the right general description of .NET's Queue<T>.

Queue<T> is an array-backed circular queue.

This is an important concept.

-----------------------------------------------------------------------------------------------------------------------------------

25. What Is a Circular Queue?

Suppose the internal array has:

Capacity = 5

Conceptually:

Index:
  0    1    2    3    4
┌────┬────┬────┬────┬────┐
│    │    │    │    │    │
└────┴────┴────┴────┴────┘

Suppose we enqueue:

10
20
30
┌────┬────┬────┬────┬────┐
│ 10 │ 20 │ 30 │    │    │
└────┴────┴────┴────┴────┘
  ↑
front

Now dequeue 10.

Logically:

20 → 30

The queue can continue using available positions rather than treating the underlying array as a simple one-direction-only structure.

The implementation uses circular indexing.

-----------------------------------------------------------------------------------------------------------------------------------

26. Why Circular Storage?

Without circular behavior, you could get a situation like:

[ ][ ][30][40][50]

where the beginning contains free space:

[ ][ ]

but the end is full.

A circular design allows the logical queue to reuse available positions efficiently.

Conceptually:

       ┌─────────────┐
       ↓             │
   [0] → [1] → [2] → [3] → [4]
     ↑                       │
     └───────────────────────┘

That's the fundamental idea behind a circular buffer.

-----------------------------------------------------------------------------------------------------------------------------------

27. Real-Time Example — Print Queue

Imagine three users send documents to a printer:

User A → 10 pages
User B → 20 pages
User C → 5 pages

Queue:

FRONT
  ↓
A → B → C

Printer processes:

A
↓
B
↓
C

Why?

Because the first job submitted should normally be processed first.

That's FIFO.

-----------------------------------------------------------------------------------------------------------------------------------

28. Real-Time Example — Customer Support

Requests arrive:

Ticket 101
Ticket 102
Ticket 103
Ticket 104

A simple FIFO processing model:

101 → 102 → 103 → 104

Process:

Dequeue()

gives:

101

then:

102

and so on.

-----------------------------------------------------------------------------------------------------------------------------------

29. Real-Time Example — Background Jobs

Suppose your application receives jobs:

Job A
Job B
Job C
Job D

A basic processing queue could conceptually be:

Incoming
   ↓
┌─────────────┐
│ Job A       │
│ Job B       │
│ Job C       │
│ Job D       │
└─────────────┘
   ↓
Worker

The worker takes:

A → B → C → D

This is a classic queue pattern.

In real distributed systems, you may use a dedicated messaging system rather than an in-memory Queue<T>, 
but the underlying FIFO concept is the same.

-----------------------------------------------------------------------------------------------------------------------------------

30. Queue in DSA — BFS

This is one of the most important reasons you need Queue<T> for DSA.

Breadth-First Search (BFS) uses a queue.

Suppose:

        A
       / \
      B   C
     / \
    D   E

BFS visits:

A
B
C
D
E

Process:

Queue

Start:

[A]

Remove A:

Queue = [B, C]

Remove B:

Queue = [C, D, E]

Remove C:

Queue = [D, E]

Then:

D
E

This is why BFS naturally uses FIFO.

31. BFS Mental Model
Start node
    ↓
Enqueue
    ↓
Dequeue
    ↓
Visit
    ↓
Enqueue neighbors
    ↓
Dequeue next
    ↓
Repeat

Remember:

BFS → Queue

DFS → Stack

This is an extremely useful DSA interview shortcut.

-----------------------------------------------------------------------------------------------------------------------------------

32. Queue vs List

You might ask:

"Can't I just use List<T> as a queue?"

You technically can implement queue-like behavior with a list, but it isn't the ideal abstraction.

For example:

List<int> numbers = new();

numbers.Add(10);
numbers.Add(20);
numbers.Add(30);

To remove the first element:

numbers.RemoveAt(0);

This can require shifting many elements.

That makes repeated front removal inefficient.

Queue<T> is specifically designed for queue semantics.

-----------------------------------------------------------------------------------------------------------------------------------

33. Queue vs Stack

| Feature      | `Stack<T>` | `Queue<T>`  |
| ------------ | ---------- | ----------- |
| Principle    | LIFO       | FIFO        |
| Add          | `Push()`   | `Enqueue()` |
| Remove       | `Pop()`    | `Dequeue()` |
| Inspect      | `Peek()`   | `Peek()`    |
| Main end     | Top        | Front/rear  |
| BFS          | ❌          | ✅           |
| DFS          | ✅          | ❌           |
| Undo         | ✅          | ❌           |
| Waiting line | ❌          | ✅           |

-----------------------------------------------------------------------------------------------------------------------------------


34. Queue vs LinkedList<T>

You can implement queue behavior with LinkedList<T>:

AddLast()
RemoveFirst()

But if your requirement is simply:

"I need a FIFO collection."

prefer:

Queue<T>

because its API directly communicates your intention.

-----------------------------------------------------------------------------------------------------------------------------------

35. Advantages
✅ Simple FIFO behavior

The collection naturally enforces:

First In → First Out
✅ Fast core operations
Enqueue → O(1) amortized
Dequeue → O(1)
Peek    → O(1)
✅ Excellent for DSA

Especially:

BFS
level-order traversal
sliding-window techniques
task processing
✅ Array-backed circular design

Provides efficient queue operations without requiring a linked-list node per element.

-----------------------------------------------------------------------------------------------------------------------------------

36. Disadvantages
❌ No random/index access

You cannot do:

queue[0]
❌ Searching is O(n)
queue.Contains(value)

is not a fast membership operation.

❌ Not appropriate for LIFO

Use:

Stack<T>

for LIFO.

❌ Not priority-based

If the highest-priority item should be processed first, a normal queue isn't enough.

For that requirement, consider:

PriorityQueue<TElement,TPriority>

which we'll study later.

-----------------------------------------------------------------------------------------------------------------------------------

37. When Should You Use Queue<T>?

Use it when:

First item in
      ↓
First item processed

Typical examples:

✓ BFS
✓ Level-order tree traversal
✓ Print queue
✓ Task processing
✓ Customer waiting line
✓ Basic request buffering
✓ Producer-consumer concepts.

-----------------------------------------------------------------------------------------------------------------------------------
38. When Should You NOT Use Queue<T>?

Don't use it when the requirement is:

✗ Last-in-first-out → Stack<T>

✗ Unique values → HashSet<T>

✗ Key/value lookup → Dictionary<TKey,TValue>

✗ Sorted unique values → SortedSet<T>

✗ Random index access → List<T>

✗ Priority processing → PriorityQueue<TElement,TPriority>

-----------------------------------------------------------------------------------------------------------------------------------
39. Interview Question
Q: What is Queue<T>?

Answer:

Queue<T> is a generic, array-backed FIFO collection where the first element added is the first element removed.
Enqueue() adds to the rear, while Dequeue() removes from the front.

40. Interview Question
Q: Difference between Enqueue() and Dequeue()?

Answer:

Enqueue() adds an element to the rear of the queue, while Dequeue() removes and returns the element at the front.

41. Interview Question
Q: Difference between Peek() and Dequeue()?

Answer:

Peek() returns the front element without removing it, whereas Dequeue() returns and removes it.

42. Interview Question
Q: What happens when Dequeue() is called on an empty queue?

Dequeue() throws an exception.

For normal empty-queue handling:

TryDequeue()

is preferable.

Example:

if (queue.TryDequeue(out int value))
{
    Console.WriteLine(value);
}

43. Interview Question
Q: Why does Queue<T> use a circular buffer?

Answer:

A circular buffer allows the queue to reuse positions freed at the beginning of the underlying array after dequeue operations, 
avoiding unnecessary shifting of all remaining elements.

That's a strong interview answer.

44. Interview Question
Q: What is the complexity of Queue operations?
Enqueue → O(1) amortized
Dequeue → O(1)
Peek    → O(1)
Contains → O(n)


45. Interview Question
Q: Which collection is commonly used for BFS?

Answer:

Queue<T> because BFS processes nodes level by level in FIFO order.

46. Interview Question
Q: Stack vs Queue in DSA?

Answer:

Stack uses LIFO and is commonly used for DFS, backtracking and undo operations. Queue uses FIFO and is commonly used for BFS and level-order processing.

47. Interview Question
Q: Is Queue<T> thread-safe?

The normal Queue<T> is not designed for concurrent access from multiple threads.

For concurrent producer/consumer scenarios, .NET provides:

ConcurrentQueue<T>

We'll cover concurrent collections separately.