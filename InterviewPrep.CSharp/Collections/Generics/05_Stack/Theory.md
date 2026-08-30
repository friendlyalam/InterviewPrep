Collection Type: ✅ Generic Collection
Namespace: System.Collections.Generic
Category: Linear collection
Core principle: LIFO — Last In, First Out
DSA importance: ⭐⭐⭐⭐⭐
Interview importance: ⭐⭐⭐⭐⭐

------------------------------------------------------------------------------------------------------------------------
1. Definition

Stack<T> is a generic collection that follows the LIFO (Last In, First Out) principle,
meaning the element added most recently is the first element removed.

Simple example:

Add:
10
20
30

The stack looks like:

     ┌───────┐
TOP →│  30   │ ← Last added
     ├───────┤
     │  20   │
     ├───────┤
     │  10   │
     └───────┘

If we remove an element:

30

comes out first.

Then:

20

Then:

10

------------------------------------------------------------------------------------------------------------------------
2. What Does LIFO Mean?

LIFO = Last In, First Out

Suppose you add:

A
B
C
D

Order of insertion:

A → B → C → D

Order of removal:

D → C → B → A

Visualize it as:

                TOP
                 ↓
              ┌─────┐
              │  D  │ ← Last In → First Out
              ├─────┤
              │  C  │
              ├─────┤
              │  B  │
              ├─────┤
              │  A  │
              └─────┘

------------------------------------------------------------------------------------------------------------------------
3. Real-Life Example — Stack of Plates

Imagine plates in a restaurant.

You place:

Plate 1
Plate 2
Plate 3

The stack becomes:

     Plate 3
     Plate 2
     Plate 1

You can't conveniently take Plate 1 without first removing Plate 3 and Plate 2.

So:

Last plate placed
        ↓
First plate removed

That's LIFO.

------------------------------------------------------------------------------------------------------------------------

4. Another Real-Life Example — Browser Back History

Suppose you visit:

Google
 ↓
YouTube
 ↓
Amazon
 ↓
GitHub

When you press Back:

GitHub
 ↓
Amazon
 ↓
YouTube
 ↓
Google

The most recent navigation is undone first.

This is conceptually stack-like.

------------------------------------------------------------------------------------------------------------------------

5. Another Important Example — Undo

Consider a text editor.

You perform:

Type "Hello"
Bold text
Delete word
Add punctuation

When you press Undo, the most recent operation is reversed first:

Undo punctuation
Undo delete
Undo bold
Undo typing

This is a classic stack use case.

------------------------------------------------------------------------------------------------------------------------

6. Basic Syntax
Stack<int> numbers = new();

String stack:

Stack<string> pages = new();

Custom object:

Stack<Order> orders = new();

------------------------------------------------------------------------------------------------------------------------
7. Main Operations

The most important Stack<T> methods are:

Push()
Pop()
Peek()
TryPop()
TryPeek()
Contains()
Clear()
Count

We'll understand every important one.

------------------------------------------------------------------------------------------------------------------------

8. Push()

Push() adds an element to the top of the stack.

Stack<int> numbers = new();

numbers.Push(10);
numbers.Push(20);
numbers.Push(30);

Stack:

     ┌───────┐
     │  30   │ ← TOP
     ├───────┤
     │  20   │
     ├───────┤
     │  10   │
     └───────┘

So:

Push(10)
Push(20)
Push(30)

means:

10 → 20 → 30

but 30 is at the top.

------------------------------------------------------------------------------------------------------------------------

9. Pop()

Pop():

Removes and returns the top element.

int value = numbers.Pop();

If:

30
20
10

then:

Pop()

returns:

30

and the stack becomes:

20
10

------------------------------------------------------------------------------------------------------------------------
10. Peek()

Peek():

Returns the top element without removing it.

Suppose:

30
20
10

Execute:

int value = numbers.Peek();

Result:

30

Stack remains:

30
20
10

This distinction is extremely important:

Push → Add
Pop  → Read + Remove
Peek → Read only

------------------------------------------------------------------------------------------------------------------------
11. Pop() vs Peek()

Interviewers frequently ask this.

Pop()
Returns top
+
Removes top
Peek()
Returns top
+
Does NOT remove top

Example:

Before:

30
20
10

Peek():

Returns 30

After:

30
20
10

Pop():

Returns 30

After:

20
10

------------------------------------------------------------------------------------------------------------------------

12. Count
numbers.Count

returns the number of elements.

Example:

30
20
10

Then:

Count = 3

------------------------------------------------------------------------------------------------------------------------
13. Contains()

Checks whether an element exists.

numbers.Contains(20);

Returns:

true

or:

false

Important:

Unlike HashSet<T>, Stack<T> isn't optimized around hash-based membership lookup.

So don't choose a stack primarily because you need fast arbitrary membership testing.

------------------------------------------------------------------------------------------------------------------------

14. Clear()

Removes all elements:

numbers.Clear();

After:

Count = 0

------------------------------------------------------------------------------------------------------------------------
15. TryPop()

This is a very useful modern API.

Problem with:

numbers.Pop();

If the stack is empty, Pop() throws an exception.

Instead:

if (numbers.TryPop(out int value))
{
    Console.WriteLine(value);
}

If an element exists:

true
value = top element

If empty:

false

No exception is needed for the normal empty-stack case.

------------------------------------------------------------------------------------------------------------------------

16. TryPeek()

Similarly:

if (numbers.TryPeek(out int value))
{
    Console.WriteLine(value);
}

If stack isn't empty:

true
value = top

If empty:

false

And importantly:

TryPeek() does not remove the element.

------------------------------------------------------------------------------------------------------------------------

17. Why TryPop() Is Useful

Instead of:

if (numbers.Count > 0)
{
    int value = numbers.Pop();
}

you can write:

if (numbers.TryPop(out int value))
{
    Console.WriteLine(value);
}

This expresses the operation directly:

"Try to remove the top element."

------------------------------------------------------------------------------------------------------------------------

18. ToArray()

You can convert the stack's elements into an array:

int[] array = numbers.ToArray();

Be aware that stack enumeration is from the top toward the bottom.

So if the stack was built as:

10
20
30

with 30 on top, enumeration is conceptually:

30
20
10

------------------------------------------------------------------------------------------------------------------------
19. CopyTo()
int[] array = new int[numbers.Count];

numbers.CopyTo(array, 0);

Copies the stack elements into an array starting at the specified destination index.

------------------------------------------------------------------------------------------------------------------------

20. TrimExcess()
numbers.TrimExcess();

Attempts to reduce excess internal storage.

Use this only when there is a real reason to reduce unused capacity.

Don't call it after every Pop().

------------------------------------------------------------------------------------------------------------------------

21. EnsureCapacity()
numbers.EnsureCapacity(100);

Ensures that the internal storage can accommodate at least the requested number of elements.

This can be useful if you know approximately how many elements you're going to push.

------------------------------------------------------------------------------------------------------------------------

22. Complete Built-In API You Should Know

For your Generic Collections → Stack folder, keep:

Core operations
Push()
Pop()
Peek()
TryPop()
TryPeek()

Searching
Contains()

Information
Count

Management
Clear()
EnsureCapacity()
TrimExcess()

Conversion/copying
ToArray()
CopyTo()

------------------------------------------------------------------------------------------------------------------------
23. Stack Does NOT Have Index Access

You cannot do:

numbers[0]

The stack is intentionally designed around its top.

Its primary operations are:

Push
Pop
Peek

not arbitrary indexed access.

------------------------------------------------------------------------------------------------------------------------

24. Stack vs List

A List<T> allows:

numbers[0]
numbers[1]
numbers[2]

and you can insert/remove at various positions.

A stack is conceptually:

                TOP
                 ↓
              Push
              Peek
              Pop

This restriction is actually useful because it enforces the LIFO model.

------------------------------------------------------------------------------------------------------------------------

25. Stack vs Queue

This is one of the most important collection comparisons.

Stack
LIFO
A
B
C

Pop → C

--
Queue
FIFO
A
B
C

Dequeue → A

So:

Stack
Last In → First Out

Queue
First In → First Out

------------------------------------------------------------------------------------------------------------------------
26. Visual Comparison
Stack
Push A
Push B
Push C

     C ← Pop first
     B
     A
Queue
Enqueue A
Enqueue B
Enqueue C

A → B → C

A ← Dequeue first

Remember:

Stack = LIFO

Queue = FIFO

------------------------------------------------------------------------------------------------------------------------

27. Time Complexity

For the core operations:

| Operation    |     Complexity |
| ------------ | -------------: |
| `Push()`     | O(1) amortized |
| `Pop()`      |           O(1) |
| `Peek()`     |           O(1) |
| `TryPop()`   |           O(1) |
| `TryPeek()`  |           O(1) |
| `Count`      |           O(1) |
| `Contains()` |           O(n) |
| `Clear()`    |           O(n) |
| `ToArray()`  |           O(n) |
| `CopyTo()`   |           O(n) |


Important

Push() is generally O(1) amortized, because occasionally the internal storage needs to grow.

------------------------------------------------------------------------------------------------------------------------

28. What Does "Amortized O(1)" Mean?

Suppose the stack has capacity:

4

and contains:

10
20
30
40

Now:

numbers.Push(50);

There may not be enough space.

The internal storage can be expanded.

That particular operation can cost more than O(1).

But over many pushes, the average cost remains:

O(1) amortized

This is the same general idea you saw with List<T> capacity growth.

------------------------------------------------------------------------------------------------------------------------

29. Advantages
✅ Simple LIFO behavior

Perfect when the latest operation must be processed first.

✅ Fast top operations
Push → O(1) amortized
Pop  → O(1)
Peek → O(1)
✅ Excellent for DSA

Stacks appear in many important algorithms.

✅ Useful for nested processing

For example:

(
[
{

and matching closing brackets.

✅ Useful for backtracking

You can store previous states and return to them.

------------------------------------------------------------------------------------------------------------------------

30. Disadvantages
❌ No random/index access

You can't naturally ask for the "third" element using an index.

❌ Not suitable for FIFO processing

For FIFO use:

Queue<T>
❌ Arbitrary searching is O(n)

If your main requirement is:

"Does this element exist?"

then HashSet<T> may be more appropriate.

❌ Doesn't automatically keep elements sorted

If you need sorted unique elements:

SortedSet<T>

may be appropriate.

------------------------------------------------------------------------------------------------------------------------

31. When Should You Use Stack<T>?

Use it when the requirement is naturally:

Last action → processed first

Examples:

✓ Undo/Redo
✓ Backtracking
✓ Expression evaluation
✓ Parentheses matching
✓ DFS
✓ Browser history concepts
✓ Function/call-stack concepts
✓ Parsing

------------------------------------------------------------------------------------------------------------------------
32. When Should You NOT Use Stack<T>?

Don't use it when you need:

✗ FIFO processing → Queue<T>

✗ Unique values → HashSet<T>

✗ Key → Value mapping → Dictionary<TKey,TValue>

✗ Sorted unique values → SortedSet<T>

✗ Random/index access → List<T>

✗ Priority-based processing → PriorityQueue<TElement,TPriority>

We'll cover PriorityQueue later.

------------------------------------------------------------------------------------------------------------------------

33. DSA Example — Reverse a String

This is one of the easiest ways to understand the stack.

Input:

HELLO

Push:

H
E
L
L
O

Stack:

O ← TOP
L
L
E
H

Pop:

O
L
L
E
H

Result:

OLLEH

That's why stacks naturally reverse sequences.

------------------------------------------------------------------------------------------------------------------------

34. DSA Example — Parentheses Matching

Consider:

( [ { } ] )

When we encounter opening brackets:

(
[
{

we push them.

Then closing brackets arrive:

}
]
)

We compare with the most recent opening bracket.

Conceptually:

Opening:
(
[
{

Stack:

{
[
(

Closing:
}

Pop → {

Closing:
]

Pop → [

Closing:
)

Pop → (

Everything matches.

Therefore:

Valid

This is a very common coding interview problem.

------------------------------------------------------------------------------------------------------------------------

35. DSA Example — DFS

Depth-First Search can be implemented using a stack.

Conceptually:

Start
 ↓
Push node
 ↓
Pop node
 ↓
Visit
 ↓
Push neighbors
 ↓
Pop next
 ↓
Continue

This is another reason Stack<T> is important for DSA.

------------------------------------------------------------------------------------------------------------------------

36. Real-Time Example — Undo

Suppose a user performs:

Action 1: Type "Hello"
Action 2: Make bold
Action 3: Change font
Action 4: Delete word

Store actions:

Delete word   ← TOP
Change font
Make bold
Type Hello

Undo:

Pop → Delete word
Pop → Change font
Pop → Make bold
Pop → Type Hello

Perfect LIFO behavior.

------------------------------------------------------------------------------------------------------------------------

37. Interview Question
Q: What is Stack<T>?

Answer:

Stack<T> is a generic collection that follows the LIFO principle. Elements are added and removed from the top,
with Push() adding, Pop() removing and returning, and Peek() returning without removing.

------------------------------------------------------------------------------------------------------------------------

38. Interview Question
Q: Difference between Pop() and Peek()?

Answer:

Pop() returns and removes the top element, whereas Peek() returns the top element without removing it.

39. Interview Question
Q: What happens if you call Pop() on an empty stack?

Pop() throws an exception.

For normal empty-stack handling, prefer:

TryPop()

Example:

if (stack.TryPop(out int value))
{
    // successful
}

40. Interview Question
Q: Why use TryPop() instead of checking Count and then calling Pop()?

Because TryPop() directly expresses the operation and avoids using an exception for a normal "nothing available" case.

if (stack.TryPop(out int value))
{
    Console.WriteLine(value);
}


41. Interview Question
Q: Stack vs Queue?

Answer:

A stack follows LIFO, while a queue follows FIFO. A stack processes the most recently 
added element first, whereas a queue processes the earliest added element first.

42. Interview Question
Q: Is Stack<T> thread-safe?

The normal Stack<T> implementation is not synchronized for concurrent access.

If multiple threads need to concurrently operate on a stack, consider the concurrent collections such as:

ConcurrentStack<T>

We'll cover concurrent collections separately.

43. Interview Question
Q: Is Stack<T> based on linked list?

Don't say:

"Stack<T> is a linked list."

That's incorrect.

The .NET Stack<T> is implemented as an array-backed collection.

Conceptually:

Stack<T>
    ↓
array-backed storage
    ↓
top position

This is an important distinction.

------------------------------------------------------------------------------------------------------------------------

44. Internal Concept

Suppose:

Stack<int> stack = new();

and:

stack.Push(10);
stack.Push(20);
stack.Push(30);

Conceptually:

Internal array:

Index
  0     1     2
┌─────┬─────┬─────┐
│ 10  │ 20  │ 30  │
└─────┴─────┴─────┘
                  ↑
                 TOP

Then:

stack.Pop();

removes the top logical element:

Index
  0     1
┌─────┬─────┐
│ 10  │ 20  │
└─────┴─────┘
            ↑
           TOP

Again, this is a conceptual model; don't depend on private implementation details.
