
“Dutch National Flag is a three-pointer technique used specifically for partitioning an array into three regions.”

The Dutch National Flag (DNF) algorithm is used to sort an array containing three distinct values, most commonly:

0, 1, 2

It sorts the array in-place using one pass.

Problem

Given:

[2, 0, 2, 1, 1, 0]

Sort it without using another array:

[0, 0, 1, 1, 2, 2]
1. Core Idea

We maintain three regions using three pointers:

low     → position for 0
mid     → current element
high    → position for 2

At any time:

[ 0s ][ 1s ][ unknown ][ 2s ]
      low   mid       high

More precisely:

0 ... low-1       → all 0
low ... mid-1     → all 1
mid ... high      → unknown
high+1 ... end    → all 2

Initially:

low = 0
mid = 0
high = n - 1
2. Three Cases
Case 1 — nums[mid] == 0

Move 0 to the left.

Swap(nums, low, mid);
low++;
mid++;

Why both?

Because after placing 0 at low, the current mid position contains a value that has already been processed.

Case 2 — nums[mid] == 1

1 is already in its correct middle region.

mid++;
Case 3 — nums[mid] == 2

Move 2 to the right.

Swap(nums, mid, high);
high--;

Important: Do NOT increment mid.

Why?

Because the value coming from high is unknown. We must process it again.

3. Complete C# Solution
public static class DutchNationalFlag
{
    public static void Sort(int[] nums)
    {
        int low = 0;
        int mid = 0;
        int high = nums.Length - 1;

        while (mid <= high)
        {
            if (nums[mid] == 0)
            {
                Swap(nums, low, mid);
                low++;
                mid++;
            }
            else if (nums[mid] == 1)
            {
                mid++;
            }
            else // nums[mid] == 2
            {
                Swap(nums, mid, high);
                high--;
            }
        }
    }

    private static void Swap(int[] nums, int i, int j)
    {
        (nums[i], nums[j]) = (nums[j], nums[i]);
    }
}
4. Dry Run

Input:

[2, 0, 2, 1, 1, 0]

Initial:

low = 0
mid = 0
high = 5
Step 1

nums[mid] = 2

Swap mid and high:

[0, 0, 2, 1, 1, 2]
       ↑        ↑
      mid      high
high--

Now:

low = 0
mid = 0
high = 4

Notice: mid stays 0.

Step 2

nums[mid] = 0

Swap low and mid:

[0, 0, 2, 1, 1, 2]
 ↑  ↑
low mid

Then:

low++
mid++
Step 3

nums[mid] = 2

Swap with high:

[0, 0, 1, 1, 2, 2]
          ↑     ↑
         mid   high

Then:

high--
Step 4

nums[mid] = 1

Simply:

mid++
Step 5

nums[mid] = 1

Again:

mid++

Now:

mid > high

Stop.

Final:

[0, 0, 1, 1, 2, 2]

5. The Most Important Rule

Remember this table:

| `nums[mid]` | Action           | Pointers         |
| ----------- | ---------------- | ---------------- |
| `0`         | Swap with `low`  | `low++`, `mid++` |
| `1`         | Already correct  | `mid++`          |
| `2`         | Swap with `high` | `high--` only    |


Memory trick
0 → LEFT
1 → MIDDLE
2 → RIGHT

So:

0 → low
1 → mid++
2 → high
6. Why Don't We Do mid++ After Swapping 2?

This is the most important interview point.

Suppose:

[1, 2, 0]
    ↑     ↑
   mid   high

nums[mid] = 2.

Swap:

[1, 0, 2]
    ↑
   mid

The 0 came from the high side.

We haven't processed it yet.

Therefore:

Swap(nums, mid, high);
high--;

but not:

mid++;

We process that position again.

7. Complexity
Time
O(n)

Why?

mid moves from left to right, and high only moves left. Every element is processed a constant number of times.

Space
O(1)

Why?

We modify the original array and use only three variables:

low
mid
high

So this is an in-place and one-pass algorithm.

8. When to Recognize DNF

Think of Dutch National Flag when you see:

Sort an array containing 0, 1, 2
Sort three categories
Partition elements into three groups
Sort in-place
O(n) time required
O(1) extra space required

Classic interview problem:

Sort Colors — LeetCode 75

The key pattern to remember is:

[ 0 ][ 1 ][ UNKNOWN ][ 2 ]
       ↑
      mid

0 → swap left
1 → move forward
2 → swap right, process again

This is an important three-pointer / partitioning pattern for your DSA preparation.