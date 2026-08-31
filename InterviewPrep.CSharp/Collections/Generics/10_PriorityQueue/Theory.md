Definition

A Priority Queue is a collection where each element has a priority, and the element with the highest priority is removed first.

Unlike a normal queue:

Queue:
First In → First Out

Priority Queue:
Highest Priority → First Out
Simple example

Suppose we have:

Task       Priority
-------------------
Task A        3
Task B        1
Task C        5
Task D        2

If larger number = higher priority, removal order is:

Task C (5)
Task A (3)
Task D (2)
Task B (1)

So it does not matter which task entered first.

Priority Queue in C#

C# provides:

PriorityQueue<TElement, TPriority>

Namespace:

System.Collections.Generic

It is generic.

Basic syntax
PriorityQueue<string, int> queue = new();

Here:

string → Element
int    → Priority
Adding elements — Enqueue()
queue.Enqueue("Task A", 3);
queue.Enqueue("Task B", 1);
queue.Enqueue("Task C", 5);

Important C# detail:

PriorityQueue<TElement,TPriority> is a min-priority queue by default.

So smaller priority value comes out first.

For the above:

Task B → 1
Task A → 3
Task C → 5
Removing — Dequeue()
string task = queue.Dequeue();

Result:

Task B

because 1 is the smallest priority.

Peek()

Looks at the highest-priority element without removing it.

string task = queue.Peek();

For:

Task B → 1
Task A → 3
Task C → 5

Peek() returns:

Task B

Priority Queue vs Queue

|               | `Queue<T>`      | `PriorityQueue<TElement,TPriority>` |
| ------------- | --------------- | ----------------------------------- |
| Rule          | FIFO            | Priority                            |
| First element | Oldest          | Highest priority                    |
| `Enqueue()`   | ✅               | ✅                                   |
| `Dequeue()`   | ✅               | ✅                                   |
| `Peek()`      | ✅               | ✅                                   |
| Typical DSA   | BFS             | Dijkstra, Top-K                     |
| Ordering      | Insertion order | Priority order                      |



Important Interview Point

Don't confuse:

Queue
→ First In, First Out

with:

Priority Queue
→ Highest priority first

And in .NET's built-in PriorityQueue, remember:

Smaller priority value
        ↓
Higher priority
        ↓
Removed first

For example:

queue.Enqueue("Critical", 1);
queue.Enqueue("Normal", 3);
queue.Enqueue("Low", 5);

Removal:

Critical
Normal
Low
DSA mental model
                Priority Queue
                     │
          ┌──────────┴──────────┐
          │                     │
       Element               Priority
          │                     │
       "Job A"                   2
                                ↓
                       smallest first
                                ↓
                            Dequeue()