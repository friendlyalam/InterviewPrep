1. What is In-Place Marking?

In-place marking is a technique where we use the input array itself to store information about
elements we have already seen, processed, or need to identify.

Normally, you might use another data structure:

HashSet<int> seen = new();

But that requires extra memory.

With in-place marking, we modify the existing array:

Input array
     ↓
Use its values/indexes
     ↓
Mark information inside the same array

The main goal is usually:

Extra Space → O(1)

----------------------------------------------------------------------------------------------------------------------------------------------
2. Why is it called "In-Place"?

Suppose:

nums = [1, 3, 4, 2, 2]

If you create:

bool[] visited = new bool[nums.Length];

you're using another data structure.

That's not O(1) extra space.

With in-place marking:

nums = [1, 3, 4, 2, 2]
       ↑
       modify nums itself

The original array becomes both:

data + tracking structure

That's why it is called in-place.

----------------------------------------------------------------------------------------------------------------------------------------------

3. The Most Important Requirement

In-place marking does not work for every array.

The most common situation is:

Array values correspond to array indexes.

For example:

nums = [1, 3, 4, 2, 2]

The values are:

1, 2, 3, 4

These values can map naturally to indexes:

value 1 → index 0
value 2 → index 1
value 3 → index 2
value 4 → index 3

Usually we use:

index = value - 1

because array indexes start at 0.

----------------------------------------------------------------------------------------------------------------------------------------------

4. The Core Idea

Suppose:

nums = [1, 3, 4, 2, 2]

We see:

1

Its corresponding index is:

1 - 1 = 0

So we mark:

nums[0]

Then see:

3

Corresponding index:

3 - 1 = 2

Mark:

nums[2]

The array itself tells us:

"I have already seen this value."

----------------------------------------------------------------------------------------------------------------------------------------------

5. Negative Marking

One of the most common forms of in-place marking is using the sign.

Suppose all values are positive.

We can mark a position by making it negative:

nums[index] = -nums[index];

Example:

Before:

[1, 3, 4, 2, 2]

We see 1.

Corresponding index:

1 - 1 = 0

Mark index 0:

[-1, 3, 4, 2, 2]

Now we see 3.

3 - 1 = 2

Mark index 2:

[-1, 3, -4, 2, 2]

The negative value means:

This position has already been visited.

6. Detecting a Duplicate

This is where the technique becomes powerful.

Suppose we encounter another 2.

Mapping:

2 - 1 = 1

We check:

nums[1]

If it is already negative:

nums[1] < 0

then:

We have already seen 2.

Therefore:

2 is a duplicate.

----------------------------------------------------------------------------------------------------------------------------------------------
7. Basic Example

Problem:

Find a duplicate number in an array containing values from 1 to n.

Example:

Input:
[1, 3, 4, 2, 2]

Output:
2

Conceptually:

Value     Index to mark
-----------------------
1         0
3         2
4         3
2         1
2         1 ← already marked

Therefore:

Duplicate = 2

----------------------------------------------------------------------------------------------------------------------------------------------

8. C# Implementation

A simple version:

public static int FindDuplicate(int[] nums)
{
    if (nums is null)
        throw new ArgumentNullException(nameof(nums));

    if (nums.Length == 0)
        throw new ArgumentException("Array cannot be empty.", nameof(nums));

    for (int i = 0; i < nums.Length; i++)
    {
        int index = Math.Abs(nums[i]) - 1;

        if (nums[index] < 0)
        {
            return Math.Abs(nums[i]);
        }

        nums[index] = -nums[index];
    }

    return -1;
}

Notice this important part:

Math.Abs(nums[i])

Why?

Because we have already modified some values to negative.

For example:

-3

still represents the original value:

3

So:

Math.Abs(-3)

gives:3

----------------------------------------------------------------------------------------------------------------------------------------------

9. Why Does This Give O(1) Space?

We aren't creating:

HashSet
Dictionary
boolean array
another tracking array

We're modifying:

nums[]

itself.

Therefore:

Time  → O(n)
Space → O(1) extra space

This is one of the main reasons interviewers like this technique.

----------------------------------------------------------------------------------------------------------------------------------------------

10. But There Is a BIG Trade-Off

The input array gets modified.

Example:

Before:
[1, 3, 4, 2, 2]

After marking:
[-1, -3, -4, -2, -2]

So the original array is no longer preserved.

Therefore, you should always check the problem requirement:

Are we allowed to modify the input array?

If the answer is no, then this technique may not be appropriate.

----------------------------------------------------------------------------------------------------------------------------------------------

11. Another Marking Method: Add n

Negative marking isn't the only method.

Suppose:

nums = [3, 0, 1, 4, 1]

Sometimes we can mark using an offset.

For example:

nums[index] += n;

If the value becomes greater than a certain threshold, we know that position has been visited.

This technique requires more careful handling because we must preserve/recover the original values.

So for your first learning, remember:

Negative marking is the most important version.

----------------------------------------------------------------------------------------------------------------------------------------------

12. In-Place Marking vs HashSet

Consider finding duplicates.

HashSet approach
HashSet<int> seen = new();

foreach (int num in nums)
{
    if (!seen.Add(num))
        return num;
}

Complexity:

Time  → O(n)
Space → O(n)
In-place marking
Use nums itself

Complexity:

Time  → O(n)
Space → O(1)

But:

Input array gets modified

So there is a trade-off.

----------------------------------------------------------------------------------------------------------------------------------------------

13. In-Place Marking vs Sorting

You might also think:

Why not sort the array first?

For:

[1, 3, 4, 2, 2]

After sorting:

[1, 2, 2, 3, 4]

Now duplicates are easy to find.

But sorting normally costs:

O(n log n)

while in-place marking can achieve:

O(n)

So when the value/index relationship allows it, in-place marking can be more efficient.

----------------------------------------------------------------------------------------------------------------------------------------------

14. Important Pattern: Value → Index

This is probably the single most important thing to remember.

When you see:

values are from 1 to n

think:

value → index

Usually:

int index = value - 1;

Then ask:

Can I use that position to store information?

If yes:

In-place marking may be applicable.

----------------------------------------------------------------------------------------------------------------------------------------------

15. Common Problems

This technique appears frequently in array problems.

Find All Duplicates
[4,3,2,7,8,2,3,1]

Output:
[2,3]

Use the corresponding indexes and mark them.

Find Disappeared Numbers
[4,3,2,7,8,2,3,1]

Output:
[5,6]

Mark every value that appears.

Unmarked positions correspond to missing values.

First Missing Positive

Example:

[3,4,-1,1]

Output:

2

This is a more advanced application of index-based marking.

Find Duplicate
[1,3,4,2,2]

Output:
2

Classic in-place marking pattern.

----------------------------------------------------------------------------------------------------------------------------------------------

16. Important Interview Clues

When a problem says:

Array contains numbers from 1 to n

or:

Array contains numbers in a specific index-related range

immediately ask:

Can value X map to index X - 1?

Then think:

Can I mark that index?

This is the mental pattern you want to develop.

----------------------------------------------------------------------------------------------------------------------------------------------

17. When NOT to Use It

Don't blindly use in-place marking.

Avoid it when:

Input cannot be modified

If the problem says:

Do not modify the input array.

Then this technique isn't suitable.

Values aren't index-compatible

Example:

[100, 500, 9000, -20]

You can't naturally map those values to array positions.

Negative values have important meaning

If the original array legitimately contains negative values, simple sign marking becomes problematic.

You may need another technique.

Values have an unrestricted range

If values can be:

Integer.MinValue → Integer.MaxValue

index-based marking won't work directly.

----------------------------------------------------------------------------------------------------------------------------------------------

18. The Mental Model

Imagine the array has two jobs:

┌──────────────────────────────┐
│        INPUT ARRAY           │
│                              │
│  Original data               │
│          +                   │
│  Tracking / marking         │
└──────────────────────────────┘

Instead of:

Input array
     +
HashSet

we do:

Input array
     ↓
Use its own positions as markers

That's the essence of In-Place Marking.

----------------------------------------------------------------------------------------------------------------------------------------------

19. Complexity

The typical target is:

Time:
O(n)

Extra Space:
O(1)

But remember:

O(1) extra space does not mean the array isn't using memory.

It means:

We aren't allocating additional memory proportional to n.

he one sentence to memorize

In-place marking uses the input array's values or indexes to record information, usually allowing us to solve a problem in O(n) time with O(1) extra space.

And the most common clue is:

"Values are in the range 1..n" → think about using values as indexes and marking those positions.