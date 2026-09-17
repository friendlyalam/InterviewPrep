Boyer–Moore Voting Algorithm — Complete Technique
1. What problem does this technique solve?

The classic Boyer–Moore Voting Algorithm is used to find a majority element in an array.

A majority element is an element that appears:

more than n / 2 times

where n is the array length.

Example
nums = [2, 2, 1, 1, 1, 2, 2]

Length:

n = 7

Majority requirement:

n / 2 = 3

2 appears 4 times.

Therefore:

2 is the majority element

2. Why do we need this technique?

A straightforward solution is to count every number.

For example:

[2, 2, 1, 1, 1, 2, 2]

We could use:

Dictionary<int, int>

and count:

2 → 4
1 → 3

This works, but requires:

Time  = O(n)
Space = O(n)

Boyer–Moore allows us to do:

Time  = O(n)
Space = O(1)

That constant-space property is the main reason this technique is important.

3. The fundamental idea

The entire technique is based on cancellation.

Imagine every majority-element occurrence is a vote for itself.

Every different element is a vote against the current candidate.

We repeatedly cancel:

one candidate
+
one different element

If an element occurs more than all other elements combined, it cannot be completely cancelled.

Therefore, it will survive as the final candidate.

4. The two variables

We need only two variables:

candidate
count


candidate:
The number we currently believe could be the majority element.

count:
The current "vote balance" for that candidate.

Important:

count is not necessarily the actual number of times the candidate has appeared in the entire array.

It represents the candidate's remaining advantage after cancellations.

This distinction is very important.

5. The three rules

The algorithm has three basic rules.

Rule 1 — Count becomes zero

If:

count == 0

the current candidate has been completely cancelled.

So the current number becomes the new candidate.

candidate = num

Rule 2 — Same as candidate

If:

num == candidate

the candidate gets another vote.

count++

Rule 3 — Different from candidate

If:

num != candidate

the current number cancels one vote of the candidate.

count--

That's the entire algorithm.

6. Basic pseudocode
candidate = nothing
count = 0

for every number:

    if count == 0:
        candidate = number

    if number == candidate:
        count++
    else:
        count--

return candidate

Notice something interesting:

We don't maintain a frequency dictionary.

We don't store the array.

We don't sort the array.

We only maintain:

candidate
count
7. Full dry run

Let's take:

[2, 2, 1, 1, 1, 2, 2]

Initially:

candidate = -
count = 0
Step 1

Current:

2

count == 0, so:

candidate = 2

Current number equals candidate:

count++

Result:

candidate = 2
count = 1
Step 2

Current:

2

Same candidate:

count++

Result:

candidate = 2
count = 2
Step 3

Current:

1

Different from candidate:

1 != 2

So:

count--

Result:

candidate = 2
count = 1

Think of this as:

2 vs 1

cancel one 2 with one 1
Step 4

Current:

1

Again:

1 != 2

So:

count--

Result:

candidate = 2
count = 0

Our two 2s have effectively been cancelled by two 1s.

Step 5

Current:

1

Now:

count == 0

So we choose a new candidate:

candidate = 1

Then:

count++

Result:

candidate = 1
count = 1
Step 6

Current:

2

Different:

2 != 1

Therefore:

count--

Result:

candidate = 1
count = 0
Step 7

Current:

2

Count is zero, so:

candidate = 2

Then:

count++

Final:

candidate = 2
count = 1

Therefore:

Answer = 2
8. The important mental model

Don't think:

"count tells me how many times candidate occurs."

Instead think:

"count tells me how many unmatched votes are currently supporting the candidate."

For example:

2 2 1 1

The two 2s and two 1s cancel:

2 2
1 1
↓
cancel
↓
nothing

So:

count = 0
9. Why does cancellation work?

This is the most important part of the technique.

Suppose:

A = majority element

and:

frequency(A) > n / 2

That means:

frequency(A) > frequency(all other elements combined)

For example:

A A A A A B B C C

Counts:

A = 5
Others = 4

We can cancel:

A B
A B
A C
A C

Four As disappear.

One A remains:

A

Therefore, A survives the cancellation process.

That's the mathematical reason the algorithm works.

10. Another example

Consider:

[3, 3, 4, 2, 3, 3, 3]

Counts:

3 → 5
4 → 1
2 → 1

Other elements combined:

4 + 2 = 2

3 has 5 votes.

We can cancel only two of its votes:

3 vs 4
3 vs 2

Three 3s remain.

So 3 must survive.

11. Why sorting is unnecessary

Another possible approach:

[2, 2, 1, 1, 1, 2, 2]

Sort:

[1, 1, 1, 2, 2, 2, 2]

Then the middle element is:

2

But sorting requires:

O(n log n)

Boyer–Moore requires:

O(n)

and doesn't modify/sort the array.

12. Complexity
Time

We scan the array once:

O(n)
Space

Only two variables:

candidate
count

Therefore:

O(1)
Final complexity
Time:  O(n)
Space: O(1)

This is optimal for the classic problem.

13. Very important: candidate vs guaranteed answer

This is a common interview trap.

The algorithm gives you a:

candidate

It does not automatically prove that the candidate is a majority element.

If the problem says:

"You may assume that a majority element always exists."

Then you can directly return the candidate.

But if the problem says:

"Return the majority element if one exists, otherwise return -1."

you need a second pass.

14. Example where candidate isn't actually majority

Consider:

[1, 2, 3, 4]

There is no majority element.

The algorithm still produces some candidate.

Therefore, after finding the candidate:

candidate = ...

count its actual occurrences:

actualCount

Then check:

actualCount > nums.Length / 2

If true:

return candidate

Otherwise:

return -1

So there are two versions.

Version A — Majority guaranteed
Find candidate
Return candidate
Version B — Majority not guaranteed
Find candidate
↓
Verify candidate
↓
Return candidate or no-majority result
15. C# implementation — guaranteed majority
public static int MajorityElement(int[] nums)
{
    int candidate = 0;
    int count = 0;

    foreach (int num in nums)
    {
        if (count == 0)
        {
            candidate = num;
        }

        if (num == candidate)
        {
            count++;
        }
        else
        {
            count--;
        }
    }

    return candidate;
}
16. C# implementation — verification included
public static int MajorityElement(int[] nums)
{
    int candidate = 0;
    int count = 0;

    foreach (int num in nums)
    {
        if (count == 0)
        {
            candidate = num;
        }

        if (num == candidate)
        {
            count++;
        }
        else
        {
            count--;
        }
    }

    int actualCount = 0;

    foreach (int num in nums)
    {
        if (num == candidate)
        {
            actualCount++;
        }
    }

    return actualCount > nums.Length / 2
        ? candidate
        : -1;
}

This is:

First pass  → O(n)
Second pass → O(n)

Total       → O(n)
Space       → O(1)

Because:

O(n) + O(n) = O(n)
17. Edge cases

You should always consider these.

One element
[5]

Answer:

5

Because:

5 > 1 / 2
Two equal elements
[5, 5]

5 appears 2 times.

2 > 2 / 2

True.

Answer:

5
Majority at beginning
[7, 7, 7, 1, 2]

Answer:

7
Majority at end
[1, 2, 7, 7, 7]

Answer:

7
Negative numbers
[-1, -1, 2, -1]

Works normally.

Answer:

-1
No majority
[1, 2, 3, 4]

Candidate exists, but there is no majority.

Verification is required if the problem doesn't guarantee one.

18. When should you recognize Boyer–Moore?

When you see:

Find an element appearing more than n/2 times

immediately think:

Majority Element
        ↓
Boyer–Moore Voting Algorithm
        ↓
candidate + count
        ↓
O(n) time / O(1) space

That's the recognition pattern you want to develop.

19. Extension: More than n/3

This is where the technique becomes more interesting.

Suppose the problem asks:

Find all elements appearing more than n/3 times.

Now there can be at most two such elements.

Why?

If there were three elements each appearing more than n/3:

A > n/3
B > n/3
C > n/3

Their total would be:

> n

which is impossible.

Therefore, we maintain:

candidate1
count1

candidate2
count2

This is called the Boyer–Moore majority vote generalization.

And again, candidate selection is followed by verification.

20. General pattern

For finding elements appearing more than:

n / k

there can be at most:

k - 1

such elements.

So the generalized approach maintains up to:

k - 1 candidates

However, in interviews, the most important versions are:

> n/2  → one candidate
> n/3  → two candidates
21. What you should remember

Don't memorize a long explanation. Remember this:

MAJORITY ELEMENT
       ↓
More than n/2
       ↓
Cancellation
       ↓
Candidate + Count

Rules:

count == 0
    → candidate = current

current == candidate
    → count++

current != candidate
    → count--

And the most important concept:

A true majority element has more occurrences than all other elements combined, so pairwise cancellation cannot eliminate it completely.

Interview-ready summary

If asked "Explain Boyer–Moore Voting Algorithm", you can say:

"Boyer–Moore Voting Algorithm finds a majority element in O(n) time and O(1) space.
It maintains a candidate and a vote count. When the count becomes zero, the current element becomes the new candidate. 
Matching elements increase the count, while different elements decrease it, effectively cancelling opposing votes.
Since a majority element appears more than all other elements combined, it cannot be completely cancelled and remains
the final candidate. If a majority isn't guaranteed, I verify the candidate with a second pass."