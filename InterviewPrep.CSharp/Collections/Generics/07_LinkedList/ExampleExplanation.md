51. Program Structure Explained

Initially:

numbers.AddFirst(20);
numbers.AddLast(40);

creates:

20 ↔ 40

Then:

numbers.AddBefore(node40, 30);

creates:

20 ↔ 30 ↔ 40

Then:

numbers.AddAfter(node40, 50);

creates:

20 ↔ 30 ↔ 40 ↔ 50

Then:

numbers.AddFirst(10);

creates:

10 ↔ 20 ↔ 30 ↔ 40 ↔ 50


52. Understanding the Node

This line:

LinkedListNode<int> node40 = numbers.Find(40)!;

means:

Find the node containing 40 and give me a reference to that actual node.

Then:

numbers.AddBefore(node40, 30);

means:

             node40
                ↓
10 ↔ 20 ↔ 40 ↔ 50

          AddBefore(40, 30)

10 ↔ 20 ↔ 30 ↔ 40 ↔ 50

And:

numbers.AddAfter(node40, 50);

means:

10 ↔ 20 ↔ 30 ↔ 40 ↔ 50
                         ↑
                      inserted


53. Understanding Previous and Next

For:

10 ↔ 20 ↔ 30

if:

var node = numbers.Find(20);

then:

node.Value

is:

20

and:

node.Previous?.Value

is:

10

while:

node.Next?.Value

is:

30

So:

Previous ← [20] → Next
    ↓                 ↓
   10                30

This is the core of a doubly linked list.



54. Final Mental Model

Don't memorize LinkedList<T> as just methods.

Remember:

                 LinkedList<T>
                       │
                       ▼
                 Doubly Linked
                       │
             ┌─────────┴─────────┐
             ▼                   ▼
         Previous              Next
             ↑                   ↓
        ┌─────────┐        ┌─────────┐
        │   10    │ ←────→ │   20    │
        └─────────┘        └─────────┘

And the most important rule:

Known node → insertion/removal can be O(1).

Finding the node first → usually O(n).

⭐ One-line interview definition

LinkedList<T> is a generic doubly linked collection whose nodes maintain previous and next references, 
providing O(1) insertion/removal when the relevant node is known, but O(n) searching and no random index access.