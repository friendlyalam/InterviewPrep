Optimal Approach
### Why
We need to include every element in the sum, so we scan the array once.


### Idea
Initialize sum = 0 and add each array element to sum.


### Dry Run
[10, 20, 30]
sum = 0 → 10 → 30 → 60


### Time Complexity
O(n)


### Why O(n)?
The loop visits all n elements once.


### Space Complexity
O(1)


### Why O(1)?
Only a fixed number of variables (sum and i) are used; extra memory does not grow with n.


### Edge Cases
Null, empty array, single element, zero, negative values, mixed positive/negative values.