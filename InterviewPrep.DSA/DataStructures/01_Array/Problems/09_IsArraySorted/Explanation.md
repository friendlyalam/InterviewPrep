## Approach: Previous Element Comparison

### Why
To determine whether the array is sorted, we only need to check
whether any element is smaller than the element immediately before it.

### Idea
Start from index 1 and compare input[i] with input[i - 1].
If input[i] < input[i - 1], return false immediately.
If no violation is found, return true.

### Dry Run
[1, 2, 2, 4, 5]

2 < 1 → false
2 < 2 → false
4 < 2 → false
5 < 4 → false

Result = true

[1, 3, 2, 4]

3 < 1 → false
2 < 3 → true → return false

### Time Complexity
O(n)

### Why O(n)?
In the worst case, every element is checked once.

### Space Complexity
O(1)

### Why O(1)?
Only the loop variable is used; no additional collection is created.

### Edge Cases
Null, empty array, one element, duplicates, all equal values,
negative values, already sorted, reverse sorted, unsorted middle element.

Dry run

For:

[1, 2, 2, 4, 5]
2 < 1 → false
2 < 2 → false
4 < 2 → false
5 < 4 → false

Nothing violates the ascending order:

true

For:

[1, 3, 2, 4]
3 < 1 → false
2 < 3 → true

Immediately:

false