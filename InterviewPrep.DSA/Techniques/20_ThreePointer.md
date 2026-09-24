Three-Pointer Technique — Complete DSA Guide

The Three-Pointer Technique is not one specific algorithm. It is a problem-solving pattern where we
maintain three indexes/pointers and move them according to the problem's rules.

It is especially useful when a problem involves three regions, three sequences, partitioning, or a fixed pointer + two moving pointers.

1. What is a Pointer in an Array?

In DSA, a pointer usually means an integer index representing a position in an array.

nums = [10, 20, 30, 40, 50]

index
        0   1   2   3   4
        ↓   ↓   ↓   ↓   ↓
       10  20  30  40  50

For example:

int left = 0;
int middle = 2;
int right = 4;

means:

left   → 10
middle → 30
right  → 50
2. What Does Three-Pointer Mean?

Instead of using three nested loops:

O(n³)

we try to intelligently maintain three indexes:

pointer 1
pointer 2
pointer 3

and move them based on the current situation.

The goal is usually to reduce unnecessary work.

A three-pointer solution may have:

O(n)

or

O(n²)

complexity depending on the problem.

3. Important Point: There Is No Single Three-Pointer Algorithm

This is very important for interviews.

Three-pointer is a technique/pattern, not one fixed algorithm.

The pointers can move:

Pattern A — Left → Right
→ → → → →
Pattern B — Right → Left
← ← ← ← ←
Pattern C — One Left → Right + One Right → Left
left  → → → 
        ← ← ← right
Pattern D — Three pointers moving in the same direction
p1 → 
p2 → 
p3 →
Pattern E — One fixed + two moving
       left → → 
i ●
       ← ← right

This is the pattern used in 3Sum.

Pattern F — Three pointers representing regions
0s | 1s | unknown | 2s
 ↑    ↑             ↑
low  mid           high

This is the Dutch National Flag pattern.

4. Direction of Pointer Movement

Understanding direction is more important than memorizing pointer names.

A. Left-to-Right Pointer

Starts at:

int left = 0;

and normally moves:

left++;

Example:

[10, 20, 30, 40, 50]
 ↑
left

    [10, 20, 30, 40, 50]
     ↑
    left

        [10, 20, 30, 40, 50]
         ↑
        left

This is a forward-moving pointer.

5. Right-to-Left Pointer

Starts at:

int right = nums.Length - 1;

and moves:

right--;

Example:

[10, 20, 30, 40, 50]
                 ↑
                right

[10, 20, 30, 40, 50]
             ↑
            right

[10, 20, 30, 40, 50]
         ↑
        right

This is a backward-moving pointer.

6. Left-to-Right + Right-to-Left

This is one of the most important pointer combinations.

left → → →       ← ← ← right

Example:

[1, 2, 3, 4, 5, 6, 7]
 ↑                 ↑
left              right

We process elements from both ends.

Common problems:

Two Sum in sorted array
Palindrome
Container With Most Water
Partitioning
Dutch National Flag
Some three-pointer problems
7. Three Pointers Moving Left-to-Right

Sometimes all three pointers move forward.

Example:

p1 →
p2 →
p3 →

This is common when working with:

Array A
Array B
Array C

For example:

A: [1, 4, 7]
     ↑

B: [2, 5, 8]
     ↑

C: [3, 6, 9]
     ↑

Each pointer tracks its own array.

8. Three Pointers With Three Regions

This is one of the most important uses.

The classic example is:

Dutch National Flag Algorithm

Problem:

Given an array containing only 0, 1 and 2,
sort it in-place.

Example:

[2, 0, 2, 1, 1, 0]

Expected:

[0, 0, 1, 1, 2, 2]

We use:

low
mid
high
9. The Three Regions

Initially:

low = 0
mid = 0
high = n - 1

Conceptually:

[ 0 ... low-1 ] [ low ... mid-1 ] [ mid ... high ] [ high+1 ... n-1 ]
       0s               1s              UNKNOWN              2s

This is the key idea.

Region 1
0 ... low-1

contains only 0.

Region 2
low ... mid-1

contains only 1.

Region 3
mid ... high

contains unknown elements.

Region 4
high+1 ... n-1

contains only 2.

10. Why mid Is the Important Pointer

We always inspect:

nums[mid]

There are three possibilities.

Case 1 — nums[mid] == 0

We need 0 on the left.

So:

swap(low, mid)

Then:

low++;
mid++;

Why both?

Because:

low has received a 0
mid now moves to the next unknown element
11. Case 2 — nums[mid] == 1

1 already belongs in the middle.

So simply:

mid++;

No swap is required.

12. Case 3 — nums[mid] == 2

2 belongs on the right.

So:

swap(mid, high)
high--;

But:

DO NOT mid++

This is one of the most important interview points.

Why?

Because the element coming from high is unknown.

Example:

mid
 ↓
[1, 0, 2, 2, 1]
       ↑
      high

Suppose:

nums[mid] == 2

We swap:

[1, 0, 1, 2, 2]
       ↑
      mid

The 1 came from the right side.

We haven't processed it yet.

Therefore:

mid stays
high--
13. Complete Algorithm
public static void Sort012(int[] nums)
{
    if (nums is null)
        throw new ArgumentNullException(nameof(nums));

    if (nums.Length == 0)
        throw new ArgumentException(
            "Array cannot be empty.",
            nameof(nums));

    int low = 0;
    int mid = 0;
    int high = nums.Length - 1;

    while (mid <= high)
    {
        if (nums[mid] == 0)
        {
            (nums[low], nums[mid]) =
                (nums[mid], nums[low]);

            low++;
            mid++;
        }
        else if (nums[mid] == 1)
        {
            mid++;
        }
        else
        {
            (nums[mid], nums[high]) =
                (nums[high], nums[mid]);

            high--;
        }
    }
}
14. Direction Visualization

The three pointers don't necessarily all move in the same direction.

For Dutch National Flag:

low  → → →
mid  → → → →
high ← ← ←

So:

low  : Left → Right
mid  : Left → Right
high : Right → Left

This is an important pattern to recognize.

15. Complete Dry Run

Input:

[2, 0, 2, 1, 1, 0]

Initial:

low = 0
mid = 0
high = 5
Step 1
nums[mid] = 2

Swap mid and high.

[0, 0, 2, 1, 1, 2]
 ↑           ↑
mid         high

Then:

high--

Now:

low = 0
mid = 0
high = 4

Notice:

mid did NOT move.
Step 2

nums[mid] = 0

Swap low and mid.

They are the same position.

Then:

low++
mid++
low = 1
mid = 1
high = 4

Array:

[0, 0, 2, 1, 1, 2]
    ↑
   mid
Step 3

nums[mid] = 0

Again:

low++
mid++

Now:

low = 2
mid = 2
high = 4
Step 4

nums[mid] = 2

Swap mid and high.

[0, 0, 1, 1, 2, 2]
       ↑     ↑
      mid   high

Then:

high--

Now:

low = 2
mid = 2
high = 3

Again:

mid does NOT move.
Step 5

nums[mid] = 1

Therefore:

mid++
mid = 3
Step 6

nums[mid] = 1

Again:

mid++
mid = 4
high = 3

Condition:

mid <= high

is false.

Done.

Result:

[0, 0, 1, 1, 2, 2]
16. Three-Pointer Pattern: Fixed + Two Moving

Another extremely important pattern is 3Sum.

Problem:

Find all unique triplets whose sum is 0.

Example:

[-1, 0, 1, 2, -1, -4]

Sort first:

[-4, -1, -1, 0, 1, 2]

We use:

i
left
right

Conceptually:

        left → → → 
i
        ← ← ← right

Actually, i is fixed during one inner search.

For each i:

sum = nums[i] + nums[left] + nums[right]

If:

sum < 0

move:

left++

If:

sum > 0

move:

right--

If:

sum == 0

we found a triplet.

This gives:

O(n²)

instead of:

O(n³)
17. Important Difference Between These Patterns
Dutch National Flag
low → 
mid →
high ←

Purpose:

Partition the array into regions.

3Sum
i = fixed

left →
right ←

Purpose:

Search for a combination of three values.

Three Sorted Arrays
i →
j →
k →

Purpose:

Process three independent sequences.

18. How Do I Know Which Pointer Should Move?

This is the real skill.

Don't memorize:

if condition → left++

Instead ask:

What does each pointer represent?

Then determine what has already been proven.

For example, in Dutch National Flag:

low

means:

Everything before low is already confirmed to be 0.

mid

means:

Everything before mid has been classified.

high

means:

Everything after high is already confirmed to be 2.

Therefore, after seeing a 2:

high--

is safe.

But moving mid is not safe because the swapped-in value hasn't been classified.

19. Pointer Invariants

This is an advanced interview concept and very useful.

An invariant is something that remains true throughout the algorithm.

For Dutch National Flag:

[0 ... low-1]     → all 0
[low ... mid-1]    → all 1
[mid ... high]     → unknown
[high+1 ... n-1]   → all 2

The algorithm keeps these statements true after every iteration.

This is why the algorithm is correct.

20. Why Three-Pointer Algorithms Are Efficient

Suppose you tried every combination:

for i
    for j
        for k

That's:

O(n³)

Three-pointer techniques often reduce unnecessary searches.

For example:

3Sum

Naive:

O(n³)

Using sorting + two pointers:

O(n²)

Dutch National Flag:

O(n)

So the technique is about using information already discovered to avoid repeating work.

21. Common Pointer Directions

You should recognize these patterns immediately.

Pattern 1
→
→
→

All pointers move left-to-right.

Pattern 2
←
←
←

All move right-to-left.

Pattern 3
→
→
←

Two forward, one backward.

Dutch National Flag is the classic example.

Pattern 4
→
←

Two opposite directions.

Very common two-pointer pattern.

Pattern 5
fixed
→
←

Classic 3Sum pattern.

22. Common Mistakes
Mistake 1 — Moving all three pointers every iteration

Wrong.

Each pointer should move only when its role/condition requires it.

Mistake 2 — Moving mid after swapping with high

In Dutch National Flag:

swap(mid, high);
high--;

Do not:

mid++;

because the incoming value is unprocessed.

Mistake 3 — Not understanding the invariant

Don't memorize the code without understanding:

What is already sorted?
What is unknown?
What does each pointer guarantee?
Mistake 4 — Assuming three-pointer always means O(n)

False.

Three-pointer solutions can be:

O(n)
O(n log n)
O(n²)

depending on the algorithm.

Mistake 5 — Forgetting sorting

For problems like 3Sum, sorting is what makes the left/right pointer decisions possible.

23. Three-Pointer Interview Checklist

When you see an array problem, ask:

1. Can I represent the problem using 3 indexes?

2. What does each pointer represent?

3. Which direction does each pointer move?

4. Can a pointer move only forward?

5. Can a pointer move only backward?

6. Can one pointer remain fixed?

7. What information becomes guaranteed after each move?

8. What is the unknown region?

9. What is the stopping condition?

10. Can I reduce nested loops using pointer movement?

11. Do I need sorting first?

12. What is the invariant?

13. What is the time complexity?

14. What is the space complexity?
24. Three-Pointer Mental Model

The most important thing to remember is:

              THREE POINTERS
                    │
       ┌────────────┼────────────┐
       ↓            ↓            ↓
   3 regions    3 sequences   3-value search
       │            │            │
   low/mid/high   i/j/k      i/left/right
       │            │            │
   partition      merge       combination

And regarding direction:

LEFT → RIGHT
RIGHT → LEFT
FIXED + LEFT → RIGHT + RIGHT → LEFT
ALL THREE → RIGHT
The key interview principle

Don't ask "Where should I move the pointer?" first. Ask "What does this pointer represent, and what have I already proven?"