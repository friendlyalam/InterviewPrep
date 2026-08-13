46. Program Flow

The important part is:

Push(10)
Push(20)
Push(30)

Stack:

     30 ← TOP
     20
     10

Then:

Peek()

returns:

30

but stack remains:

30
20
10

Then:

Pop()

returns:

30

and stack becomes:

20
10

Then:

TryPop()

returns:

20

and stack becomes:

10


47. The Most Important Mental Model

Don't memorize Stack<T> as a list of methods.

Remember:

                 STACK
                   │
                   ▼
                  LIFO
                   │
          ┌────────┼────────┐
          ▼        ▼        ▼
        Push      Peek      Pop
          │        │         │
          ▼        ▼         ▼
         Add     Read      Read
                           +
                          Remove

And for your DSA preparation:

Stack
 ↓
LIFO
 ↓
Latest item first
 ↓
Push / Pop / Peek
 ↓
Undo
 ↓
Backtracking
 ↓
Parentheses
 ↓
DFS
 ↓
Expression evaluation
⭐ One-line interview definition

Stack<T> is a generic, array-backed LIFO collection where Push, Pop, and Peek provide O(1) top-element operations, with Push being O(1) amortized.