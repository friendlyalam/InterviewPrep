## Approach: Reverse Linear Search — Early Exit

### Why
We need the last occurrence, so searching from the end allows
us to stop immediately when the target is found.

### Idea
Start from the last index and move toward index 0.
Return the index as soon as the target is found.

### Dry Run
[10, 20, 30, 20, 40], target = 20

index 4 → 40 → no
index 3 → 20 → found → return 3

### Time Complexity
O(n)

### Why O(n)?
In the worst case, the target is at index 0 or does not exist,
so every element may need to be checked.

### Space Complexity
O(1)

### Why O(1)?
Only the loop variable is used. No additional collection is created.

### Edge Cases
Null, empty array, one element, target not found,
target at index 0, target at last index, duplicate target,
negative values, zero, extreme integer values.

Important cases covered:


Null                  → Exception
Empty                 → Exception
One element           → 0 / -1
Target not found      → -1
Target at index 0     → 0
Target at last index  → Last index
Multiple occurrences  → Last occurrence
All elements same     → Last index
Negative values       → Tested
Zero                  → Tested
int.MinValue          → Tested
int.MaxValue          → Tested