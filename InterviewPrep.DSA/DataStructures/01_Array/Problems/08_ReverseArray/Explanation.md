## Approach: Two Pointers — In Place

### Why
We need to reverse the array without creating another array.

### Idea
Use two pointers:
left starts at the beginning and right starts at the end.
Swap their values, then move both pointers toward the center.

### Swap
A temporary variable is used to preserve the left value
before it is overwritten.

### Dry Run
[1, 2, 3, 4, 5]

Swap 1 and 5 → [5, 2, 3, 4, 1]
Swap 2 and 4 → [5, 4, 3, 2, 1]
Pointers meet → stop.

### Time Complexity
O(n)

### Why O(n)?
We perform approximately n/2 swaps.
Ignoring constants, O(n/2) = O(n).

### Space Complexity
O(1)

### Why O(1)?
Only left, right and temp variables are used.
No additional array is created.

### Edge Cases
Null, empty array, one element, two elements,
even length, odd length, duplicate values, negative values.