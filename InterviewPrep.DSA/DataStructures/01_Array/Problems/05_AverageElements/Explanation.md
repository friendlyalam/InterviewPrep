## Approach: Optimal — Linear Scan

### Why
We need the sum of every element, so we scan the array once.

### Idea
Add every element to sum, then calculate the average using the array length.

### Dry Run
[10, 20, 30, 40]
sum = 100
100 / 4 = 25

### Time Complexity
O(n)

### Why O(n)?
Every element is visited exactly once.

### Space Complexity
O(1)

### Why O(1)?
Only fixed variables (`sum` and `i`) are used.

### Edge Cases
Null, empty array, single element, zero, negative values, decimal result, large values.


------------------------------------------------------------------------------------------------
Why Assert.Equal(expected, result, 10)?

For double values, we allow a small precision difference.

For example:

13.333333333333334

and a mathematically equivalent floating-point result may have a tiny representation difference.

The 10 means xUnit checks the values to 10 decimal places.

Important test cases covered
Null              → Exception
Empty             → Exception
Single element    → Same element
Normal            → 25
Negative values   → Correct negative average
Zero              → 0
Mixed values      → Correct average
Decimal result    → 13.333...