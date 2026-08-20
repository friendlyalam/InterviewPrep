Dry Run

Consider:

[10, 25, 7, 90, 15]

Initially:

smallest = 10
i = 1
25 < 10 → false

Still:

smallest = 10
i = 2
7 < 10 → true

Update:

smallest = 7
i = 3
90 < 7 → false
i = 4
15 < 7 → false

Final:

7

----------------------------------------

Time Complexity

You visit every element exactly once.

Therefore:

Time = O(n)

This is optimal.

7. Space Complexity

You only use:

int smallest
int i

These are constant extra variables.

Therefore:

Space = O(1)

