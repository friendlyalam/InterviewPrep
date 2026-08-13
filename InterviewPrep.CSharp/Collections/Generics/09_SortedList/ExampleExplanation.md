45. Program Flow

We insert:

103 → Rahul
101 → Aman
102 → Priya

But enumeration gives:

101 → Aman
102 → Priya
103 → Rahul

because the keys are automatically maintained in sorted order.

Then:

students.GetKeyAtIndex(0);

returns:

101

and:

students.GetValueAtIndex(0);

returns:

Aman

This demonstrates something that distinguishes SortedList from SortedDictionary:

SortedList has sorted-position access.

Then:

students.RemoveAt(0);

removes the first sorted element.

Finally, the custom comparer demonstrates descending key order.

46. Final Mental Model

Keep this one:

                 SortedList<TKey,TValue>
                           │
                           ▼
                    Two sorted arrays
                    ┌───────────────┐
                    │ Keys          │
                    │ Values        │
                    └───────────────┘
                           │
              ┌────────────┼────────────┐
              ▼            ▼            ▼
          Key lookup    Index access   Insert
           O(log n)       O(1)          O(n)
                                          
                           │
                           ▼
                    Best when data is
                    relatively stable
⭐ Interview definition

SortedList<TKey,TValue> is a generic, array-based key-value collection that maintains keys in sorted order, provides O(log n) key lookup and O(1) sorted-index access,
but generally requires O(n) time for insertion and removal because elements may need to be shifted.