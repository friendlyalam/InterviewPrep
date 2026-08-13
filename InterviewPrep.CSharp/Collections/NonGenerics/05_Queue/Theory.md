Type: ❌ Non-generic
Namespace: System.Collections
Modern replacement: Queue<T>
Product-company relevance: ⭐⭐ — mainly legacy/interview comparison

--------------------------------------------------------------------------------------------

1. Definition

System.Collections.Queue is a non-generic FIFO collection that stores elements as object.

FIFO = First In, First Out

Enqueue
   ↓
10 → 20 → 30
↑
First

Dequeue()
   ↓
10
2. Basic Syntax
using System.Collections;

Queue queue = new();

Add:

queue.Enqueue(10);
queue.Enqueue(20);
queue.Enqueue(30);

Order:

10 → 20 → 30
3. Important Methods
Enqueue()

Adds an element to the back.

queue.Enqueue(40);
Dequeue()

Removes and returns the element at the front.

object value = queue.Dequeue();

For:

10 → 20 → 30

result:

10

Remaining:

20 → 30
Peek()

Returns the front element without removing it.

object value = queue.Peek();
Contains()
bool exists = queue.Contains(20);
Clear()
queue.Clear();
Count
Console.WriteLine(queue.Count);
4. Dequeue() vs Peek()

Very common interview question:

Peek()
 ↓
Look at front
 ↓
Element stays
Dequeue()
 ↓
Take front
 ↓
Element removed
5. Type Casting

Because the non-generic Queue stores objects:

queue.Enqueue(100);

int value = (int)queue.Dequeue();

With generic Queue<T>:

Queue<int> queue = new();

queue.Enqueue(100);

int value = queue.Dequeue();

No cast is required.

6. Boxing / Unboxing

With:

Queue queue = new();

queue.Enqueue(100);

the int can be boxed to object.

Retrieval:

int value = (int)queue.Dequeue();

requires unboxing.

So again:

Non-generic Queue
       ↓
object
       ↓
boxing/unboxing
       ↓
casting
7. Queue vs Queue<T>

| Feature       | `Queue`              | `Queue<T>`                   |
| ------------- | -------------------- | ---------------------------- |
| Generic       | ❌                    | ✅                            |
| Namespace     | `System.Collections` | `System.Collections.Generic` |
| Type-safe     | ❌                    | ✅                            |
| Stores        | `object`             | `T`                          |
| Casting       | Often required       | No                           |
| Boxing        | Can occur            | Avoided for value-type T     |
| FIFO          | ✅                    | ✅                            |
| Modern choice | ❌                    | ✅                            |


8. Complexity

| Operation    |     Complexity |
| ------------ | -------------: |
| `Enqueue()`  | O(1) amortized |
| `Dequeue()`  |           O(1) |
| `Peek()`     |           O(1) |
| `Count`      |           O(1) |
| `Contains()` |           O(n) |
| `Clear()`    |           O(n) |


9. Advantages
Simple FIFO structure.
Dynamic size.
Useful for legacy .NET code.
Same fundamental behavior as Queue<T>.
10. Disadvantages
Non-generic.
No compile-time type safety.
Casting required.
Boxing/unboxing can occur.
Not preferred for modern C#.

Use:

Queue<T>

for new code.

11. Common DSA Applications

You already learned these with generic Queue<T>:

BFS
Task scheduling
Print queues
Producer-consumer scenarios
Level-order tree traversal

The important DSA concept remains:

FIFO — First In, First Out.

12. Interview Questions
Q1. What principle does Queue follow?

FIFO — First In, First Out.

Q2. Dequeue() vs Peek()?

Dequeue() removes and returns the front element; Peek() only returns it.

Q3. Modern replacement?
Queue → Queue<T>
Q4. Why is Queue<T> preferred?

Type safety, no unnecessary casting, and better handling of value types.

Q5. Which operation adds an element?
Enqueue()
Q6. Which operation removes the front?
Dequeue()


Final Mental Model
System.Collections.Queue
        ↓
    Non-generic
        ↓
       FIFO
        ↓
      object
        ↓
Enqueue / Dequeue / Peek
        ↓
Boxing + casting possible
        ↓
      Legacy
        ↓
Use Queue<T>