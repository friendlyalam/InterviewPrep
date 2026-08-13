46. Program Flow

Initially, we insert in this order:

103
101
102

But enumeration gives:

101
102
103

because the keys are sorted.

Then:

students[101] = "Arjun";

updates the value associated with key 101.

Then:

TryGetValue()

safely retrieves a value without requiring a separate ContainsKey() check.

Then:

GetViewBetween(102, 104)

gives the range:

102
103
104

Finally, we create a second SortedDictionary with a descending comparer:

103
102
101
47. Final Mental Model

Keep this picture in your head:

             SortedDictionary<TKey,TValue>
                          │
                          ▼
                    Key → Value
                          │
                          ▼
                   Balanced Tree
                          │
              ┌───────────┴───────────┐
              ▼                       ▼
           Key < X                 Key > X
              │                       │
              └──────────┬────────────┘
                         ▼
                    O(log n)

And the decision rule:

Need key-value?
      │
      ├── No → another collection
      │
      └── Yes
           │
           ├── Need sorted keys?
           │       │
           │       ├── No → Dictionary
           │       │
           │       └── Yes → SortedDictionary
           │
           └── Need unique values only?
                   │
                   └── SortedSet
⭐ One-line interview definition

SortedDictionary<TKey,TValue> is a generic tree-based key-value collection that maintains unique keys in sorted order and provides O(log n) lookup, insertion, and removal.