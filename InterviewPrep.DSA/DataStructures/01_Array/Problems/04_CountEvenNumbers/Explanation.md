## Approach: Optimal — Linear Scan

### Why
We need to check every element to count all even numbers.

### Idea
Traverse the array and increment count when element % 2 == 0.

### Dry Run
[1, 2, 4, 7, 10]
1 → odd
2 → even → count = 1
4 → even → count = 2
7 → odd
10 → even → count = 3

### Time Complexity
O(n)

### Why O(n)?
Every element is checked exactly once.

### Space Complexity
O(1)

### Why O(1)?
Only a fixed number of variables are used.

### Edge Cases
Null, empty, single element, zero, all odd, all even, negative values.