## Approach: Two Pointers — Read/Write Pointer

### Why
Move all zeros to the end while keeping the non-zero elements
in their original order and modifying the array in place.

### Idea
`i` scans every element.
`nonZeroIndex` points to the position where the next non-zero
element should be placed.

When a non-zero element is found, swap it with
`nums[nonZeroIndex]`, then move `nonZeroIndex` forward.

### Initialization
`nonZeroIndex = 0` because the first non-zero element should
be placed at index 0.

### Dry Run
Input: [0, 1, 0, 3, 12]

i=0 → 0 → skip
i=1 → 1 → swap index 1 and 0
       [1, 0, 0, 3, 12]

i=2 → 0 → skip
i=3 → 3 → swap index 3 and 1
       [1, 3, 0, 0, 12]

i=4 → 12 → swap index 4 and 2
       [1, 3, 12, 0, 0]

Result: [1, 3, 12, 0, 0]

### Why Swap?
Swapping places each non-zero element at the next available
non-zero position while pushing zeros toward the right.

### Time Complexity
O(n)

### Why O(n)?
The array is traversed once using the `i` pointer.

### Space Complexity
O(1)

### Why O(1)?
Only `nonZeroIndex` and `temp` are used.
No additional array or collection is created.

### Edge Cases
Null, empty array, one element, all zeros, no zeros,
zeros at beginning, zeros at end, consecutive zeros,
negative numbers and duplicate non-zero values.