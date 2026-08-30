## Approach: Single Pass — Optimal

### Why
An unsorted array requires checking every element to count
all occurrences.

### Idea
Maintain count and scan the array once. Increment count when
input[i] equals target.

### Dry Run
[1, 2, 3, 2, 2], target = 2

count = 0
2 found → count = 1
2 found → count = 2
2 found → count = 3

Result = 3

### Time Complexity
O(n)

### Why O(n)?
Every element must potentially be checked once.

### Space Complexity
O(1)

### Why O(1)?
Only a fixed count variable is required.

### Edge Cases
Null, empty array, one element, target absent, all elements
matching, duplicates, negative values, zero, extreme integers.