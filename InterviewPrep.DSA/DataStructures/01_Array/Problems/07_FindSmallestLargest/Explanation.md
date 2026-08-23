## Approach: Single Pass — Optimal

### Why
We need both the smallest and largest values, so we can track
both while traversing the array once.

### Initialization
Initialize smallest and largest with input[0] because we need
an actual array value as the initial comparison value.

### Idea
Compare every remaining element with smallest and largest.
Update them when a smaller or larger value is found.

### Dry Run
[10, 5, 20, 3, 15]

smallest = 10
largest  = 10

5  → smallest = 5
20 → largest = 20
3  → smallest = 3
15 → no change

Result:
Smallest = 3
Largest  = 20

### Time Complexity
O(n)

### Why O(n)?
Every array element is visited exactly once.

### Space Complexity
O(1)

### Why O(1)?
Only a fixed number of variables are used. Extra memory does
not grow with the input size.

### Edge Cases
Null, empty array, one element, duplicate values, zero,
negative values, int.MinValue, int.MaxValue.


----------------------------------------------------------------------------
Important test cases covered
Null                  → Exception
Empty                 → Exception
One element           → Same value for both
All values same       → Same value for both
Positive values       → Correct
Negative values       → Correct
Zero                  → Correct
Mixed values          → Correct
int.MinValue          → Correct
int.MaxValue          → Correct

--------------------------------------------------------------------------------------------

One small point to remember

Your final algorithm:

if (input[i] > largest)
{
    largest = input[i];
}

if (input[i] < smallest)
{
    smallest = input[i];
}

uses two independent if statements, and that's important.

Don't change them to:

if (...)
{
}
else
{
}

because an element that isn't the largest isn't necessarily the smallest.