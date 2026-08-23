## Approach: Brute Force — Distinct + Sorting

### Why
Sorting makes it easy to identify the second largest distinct value.

### Idea
Remove duplicates, sort the distinct values, and return the second-last element.

### Dry Run
[10, 50, 20, 50, 30]
→ [10, 50, 20, 30]
→ [10, 20, 30, 50]
→ 30

### Time Complexity
O(n log n)

### Why O(n log n)?
Removing duplicates takes up to O(n²) with List.Contains,
while sorting takes O(n log n); therefore this implementation
with List.Contains is actually O(n²).

### Space Complexity
O(n)

### Why O(n)?
The distinct list can contain up to n elements.

### Edge Cases
Null, empty, one distinct value, duplicates, negative values.


Important correction

Notice something important here.

Although sorting itself is O(n log n), your particular List.Contains() implementation makes the duplicate-removal phase potentially O(n²).

So your exact implementation is:

Time = O(n²)

not O(n log n).

That's an excellent DSA lesson.