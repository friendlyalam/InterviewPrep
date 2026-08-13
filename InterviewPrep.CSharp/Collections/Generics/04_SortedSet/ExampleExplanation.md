39. What You Should Remember

Don't memorize SortedSet<T> as just another collection.

Remember this decision:

                    Need collection
                          │
             ┌────────────┴────────────┐
             │                         │
        Duplicates?                Unique?
             │                         │
            YES                       YES
             │                         │
          List<T>          ┌───────────┴───────────┐
                           │                       │
                       Need sorted?           No sorting
                           │                       │
                          YES                     NO
                           │                       │
                     SortedSet<T>             HashSet<T>
⭐ Core difference
List<T>
→ sequence + index

HashSet<T>
→ unique + fast lookup

SortedSet<T>
→ unique + sorted + range operations