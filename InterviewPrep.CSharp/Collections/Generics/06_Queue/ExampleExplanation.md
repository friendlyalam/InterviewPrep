49. Program Flow

Initially:

Enqueue A
Enqueue B
Enqueue C

Queue:

FRONT                    REAR
  ↓                        ↓
┌────────────┬────────────┬────────────┐
│ Customer A │ Customer B │ Customer C │
└────────────┴────────────┴────────────┘

Then:

Peek()

returns:

Customer A

but doesn't remove it.

Then:

Dequeue()

returns:

Customer A

Queue becomes:

Customer B → Customer C

Then:

TryDequeue()

returns:

Customer B

Remaining:

Customer C

This demonstrates the complete FIFO behavior.

50. Most Important Mental Model

Don't memorize Queue<T> as just a collection of methods.

Remember:

                    QUEUE
                      │
                      ▼
                     FIFO
                      │
             ┌────────┼────────┐
             ▼        ▼        ▼
         Enqueue    Peek    Dequeue
             │        │        │
             ▼        ▼        ▼
            Add      Read    Read +
             │                 Remove
             │
             ▼
           REAR

And for DSA:

Queue
  ↓
FIFO
  ↓
First item processed first
  ↓
BFS
  ↓
Level-order traversal
  ↓
Task/request processing
⭐ One-line interview definition

Queue<T> is a generic, array-backed FIFO collection where Enqueue() adds elements at the rear and Dequeue() 
removes elements from the front, with O(1) amortized enqueue and O(1) dequeue operations.