1. Definition

Some algorithms do not grow linearly or quadratically.

Instead, the number of operations doubles or grows even faster as the input increases.

These complexities are:

O(2ⁿ) – Exponential Time
O(n!) – Factorial Time

These are among the slowest time complexities.

2. Simple Definition
O(2ⁿ)

Every increase in input creates twice as much work.

O(n!)

The algorithm tries every possible arrangement (permutation).

The amount of work becomes enormous very quickly.

3. Why Do We Need to Learn These?

Because many beginners write brute-force solutions like:

Generate every possible subset.
Generate every possible permutation.
Try every possible path.

These often work for small inputs but fail for large ones.

Understanding these complexities helps you know when a solution is not scalable.

4. Understanding O(2ⁿ)

Imagine you have a room with switches.

For each switch, you have 2 choices:

ON

or

OFF

Suppose there is:

1 switch

Possible combinations:

ON

OFF

Total:

2

Now:

2 switches

Possible combinations:

ON ON

ON OFF

OFF ON

OFF OFF

Total:

4

Now:

3 switches

Total combinations:

8

General Rule

n switches

↓

2ⁿ combinations



5. Visual Understanding of O(2ⁿ)

| Input (n) | Operations (2ⁿ) |
| --------: | --------------: |
|         1 |               2 |
|         2 |               4 |
|         3 |               8 |
|         4 |              16 |
|         5 |              32 |
|        10 |           1,024 |
|        20 |       1,048,576 |
|        30 |   1,073,741,824 |


Notice what happens.

Going from 20 to 30 inputs increases the work from about 1 million to over 1 billion.

That is explosive growth.

6. Real-Life Example – Choosing Clothes

Suppose you have

5 shirts

For every shirt,

you can either:

Wear it
Not wear it

Each shirt has 2 choices.

Total possible combinations:

2⁵

=

32

This resembles generating all subsets.

7. Technical Example (Concept)

Later we'll learn Recursion.

For now, just understand the idea.

void Solve(int n)
{
    if(n == 0)
        return;

    Solve(n - 1);

    Solve(n - 1);
}

Every function call creates two more calls.

The number of calls grows approximately like:

2ⁿ
8. Where Do We See O(2ⁿ)?

Common examples:

Generating all subsets
Brute-force recursion
Some Dynamic Programming problems before optimization
Recursive Fibonacci (naive implementation)
9. Understanding O(n!)

Now let's learn something even slower.

Suppose we have

3 students

A

B

C

How many different seating arrangements exist?

ABC

ACB

BAC

BCA

CAB

CBA

Total:

6

=

3!

Now suppose:

4 students

Arrangements:

4!

=

24

Suppose:

5 students
5!

=

120

Suppose:

10 students
10!

=

3,628,800

Already millions of possibilities.

10. What is Factorial?

Factorial means:

n!

=

n × (n−1) × (n−2) × ...

× 2 × 1

Examples:

3!

=

3 × 2 × 1

=

6
5!

=

5 × 4 × 3 × 2 × 1

=

120
8!

=

40320


11. Visual Understanding of O(n!)

| Input (n) |        n! |
| --------: | --------: |
|         1 |         1 |
|         2 |         2 |
|         3 |         6 |
|         4 |        24 |
|         5 |       120 |
|         6 |       720 |
|         7 |     5,040 |
|         8 |    40,320 |
|         9 |   362,880 |
|        10 | 3,628,800 |


The growth is even faster than O(2ⁿ).

12. Real-Life Example – Seating Arrangement

Imagine:

5 people

Every possible seating arrangement must be checked.

Total possibilities:

120

Now imagine

10 people

More than 3.6 million arrangements.

This is why brute-force permutation algorithms become impractical very quickly.

13. Where Do We See O(n!)?

Examples include:

Generating all permutations
Traveling Salesman Problem (brute force)
Some Backtracking problems
Exhaustive search algorithms
14. Comparing All Time Complexities

Suppose

n = 20

Approximate operations:

| Complexity |                Operations |
| ---------- | ------------------------: |
| O(1)       |                         1 |
| O(log n)   |                        ~5 |
| O(n)       |                        20 |
| O(n log n) |                       ~86 |
| O(n²)      |                       400 |
| O(n³)      |                     8,000 |
| O(2ⁿ)      |                 1,048,576 |
| O(n!)      | 2,432,902,008,176,640,000 |


Look at the last two rows.

That is why interviewers expect you to improve brute-force solutions.


5. Complete Ranking (Fastest to Slowest)
O(1)

↓

O(log n)

↓

O(n)

↓

O(n log n)

↓

O(n²)

↓

O(n³)

↓

O(2ⁿ)

↓

O(n!)

This order is extremely important for interviews.

16. Interview Notes
Question 1

Which is worse: O(2ⁿ) or O(n!)?

Answer:

O(n!) grows much faster than O(2ⁿ) and becomes impractical for even relatively small values of n.

Question 2

Where do we usually see O(2ⁿ)?

Answer:

In brute-force recursion, generating all subsets, and some unoptimized recursive algorithms.

Question 3

Where do we usually see O(n!)?

Answer:

In algorithms that generate every possible permutation or arrangement.

17. Common Mistakes

❌ Thinking O(2ⁿ) means 2 × n.

Correct:

It means 2 raised to the power n.

❌ Thinking factorial means multiplication by 2.

Correct:

Factorial multiplies all positive integers from n down to 1.

❌ Thinking brute-force is always acceptable.

Correct:

Brute-force is often useful as a starting point, but product companies usually expect you to optimize it.

18. Summary
O(2ⁿ)

✓ Two choices at every step.

✓ Very fast growth.

✓ Common in recursion and subset generation.

O(n!)

✓ Every possible arrangement.

✓ Even faster growth than O(2ⁿ).

✓ Common in permutation and backtracking problems.

19. Revision Notes
Fastest

↓

O(1)

↓

O(log n)

↓

O(n)

↓

O(n log n)

↓

O(n²)

↓

O(n³)

↓

O(2ⁿ)

↓

O(n!)

↓

Slowest