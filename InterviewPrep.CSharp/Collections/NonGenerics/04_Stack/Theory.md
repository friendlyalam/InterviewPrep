Type: ❌ Non-generic
Namespace: System.Collections
Modern replacement: Stack<T>
Product-company relevance: ⭐⭐ — mainly legacy/interview comparison

--------------------------------------------------------------------------------------

1. Definition

System.Collections.Stack is a non-generic LIFO collection that stores elements as object.

LIFO = Last In, First Out

Push
 ↓

10
20
30  ← Top

Pop()
 ↓
30
2. Basic Syntax
using System.Collections;

Stack stack = new();

Add:

stack.Push(10);
stack.Push(20);
stack.Push(30);
3. Important Methods
Push()

Adds an element to the top.

stack.Push(40);
Pop()

Removes and returns the top element.

object value = stack.Pop();

If the stack is:

30
20
10

Pop() returns:

30
Peek()

Returns the top element without removing it.

object value = stack.Peek();
Contains()
bool exists = stack.Contains(20);
Clear()
stack.Clear();
Count
Console.WriteLine(stack.Count);
4. Pop() vs Peek()

Very common interview question.

Peek()
 ↓
Look at top
 ↓
Element remains
Pop()
 ↓
Take top
 ↓
Element removed
5. Type Casting

Because it's non-generic:

stack.Push(100);

int value = (int)stack.Pop();

With generic:

Stack<int> stack = new();

stack.Push(100);

int value = stack.Pop();

No cast required.

6. Boxing / Unboxing

With:

Stack stack = new();

stack.Push(100);

the int can be boxed into an object.

Then:

int value = (int)stack.Pop();

unboxes it.

This is another reason to prefer Stack<T>.

7. Non-Generic Stack vs Stack<T>

| Feature       | `Stack`              | `Stack<T>`                   |
| ------------- | -------------------- | ---------------------------- |
| Generic       | ❌                    | ✅                            |
| Namespace     | `System.Collections` | `System.Collections.Generic` |
| Type-safe     | ❌                    | ✅                            |
| Stores        | `object`             | `T`                          |
| Casting       | Often required       | No                           |
| Boxing        | Can occur            | Avoided for value-type T     |
| LIFO          | ✅                    | ✅                            |
| Modern choice | ❌                    | ✅                            |


8. Complexity

Same basic stack characteristics:

| Operation    |     Complexity |
| ------------ | -------------: |
| `Push()`     | O(1) amortized |
| `Pop()`      |           O(1) |
| `Peek()`     |           O(1) |
| `Count`      |           O(1) |
| `Contains()` |           O(n) |
| `Clear()`    |           O(n) |


9. Advantages
Simple LIFO structure.
Dynamic size.
Useful for legacy code.
Same basic stack behavior as Stack<T>.
10. Disadvantages
Non-generic.
No compile-time type safety.
Casting required.
Boxing/unboxing can occur.
Legacy API.

For new C# code:

Stack<int>

is preferred.

11. Interview Questions
Q1. What principle does Stack follow?

LIFO — Last In, First Out.

Q2. Pop() vs Peek()?

Pop() removes and returns the top element; Peek() only returns it.

Q3. Modern replacement?
Stack → Stack<T>
Q4. Why is generic Stack preferred?

Because it provides type safety and avoids unnecessary boxing/unboxing and casting.

Q5. Common DSA uses?
Undo operations
Browser history
Expression evaluation
DFS
Parentheses matching
Backtracking


---------------------------------------
What you need to remember
System.Collections.Stack
        ↓
Non-generic
        ↓
LIFO
        ↓
object
        ↓
Push / Pop / Peek
        ↓
Boxing + casting possible
        ↓
Legacy
        ↓
Use Stack<T> in modern C#