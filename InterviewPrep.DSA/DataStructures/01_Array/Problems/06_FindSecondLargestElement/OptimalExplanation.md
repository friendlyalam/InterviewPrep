## Approach: Optimal — Single Pass

### Why
Sorting is unnecessary because we only need the two largest distinct values.

### Idea
Maintain largest and secondLargest while scanning the array once.

### Initialization
Both are initialized as null because no array element has been processed yet.
null means "value not found yet", not zero.

### Dry Run
[10, 50, 20, 50, 30]

10 → largest = 10
50 → largest = 50, second = 10
20 → second = 20
50 → duplicate largest → ignore
30 → second = 30

Result = 30

### Time Complexity
O(n)

### Why O(n)?
Every array element is processed exactly once.

### Space Complexity
O(1)

### Why O(1)?
Only a fixed number of variables are used; extra memory does not grow with n.

### Edge Cases
Null, empty, one element, all duplicates, negative values,
duplicate largest, int.MinValue, int.MaxValue.


Final complexity
Time  → O(n)   ✅ Best possible
Space → O(1)   ✅ Best possible

And importantly, we are not using an additional List, HashSet, or sorting, so the optimal solution truly uses constant extra space.