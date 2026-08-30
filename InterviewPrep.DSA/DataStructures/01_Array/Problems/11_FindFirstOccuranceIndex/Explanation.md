## Approach: Linear Search — Early Exit

### Why
The array is unsorted, so we may need to check every element
to find the first occurrence.

### Idea
Traverse from left to right. When the target is found, immediately
return its index.

### Dry Run
[10, 20, 30, 20, 40], target = 20

10 → no
20 → found at index 1 → return 1

The remaining elements are not checked.

### Time Complexity
O(n)

### Why O(n)?
In the worst case, the target is at the last position or does not
exist, so every element must be checked.

### Space Complexity
O(1)

### Why O(1)?
Only the loop variable is used; no additional collection is created.

### Edge Cases
Null, empty array, one element, target not found,
target at first index, target at last index, duplicate target,
negative values, zero, extreme integer values.