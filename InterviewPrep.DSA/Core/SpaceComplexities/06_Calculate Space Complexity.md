1. Definition

Calculating Space Complexity means determining how much extra memory an algorithm allocates while solving a problem.

Remember:

We usually calculate Auxiliary Space Complexity, not the memory already occupied by the input.

2. Simple Definition

Whenever you see a program, ask yourself one question:

"What extra memory is this program creating?"

If the answer is:

Only a few variables → O(1)
One array of size n → O(n)
One matrix of size n × n → O(n²)
Recursive calls of depth n → O(n)
3. The 5-Step Formula

Whenever you solve any DSA problem, follow these five steps.

Step 1 – Ignore the Input

Example

int FindMax(int[] arr)

Do we count arr?

No.

It is the input.

We only count new memory created by the algorithm.

Step 2 – Count Variables

Example

int max = arr[0];
int i = 0;

Variables:

max

i

Only two variables.

Space

O(1)
Step 3 – Check for New Data Structures

Example

int[] copy = new int[n];

New array created.

Space

O(n)

Another example

List<int> list = new List<int>();

If the list stores n items,

Space

O(n)
Step 4 – Check for Recursion

Example

Print(n - 1);

Every recursive call creates a stack frame.

If recursion depth is n

Space

O(n)

If recursion depth is log n

Space

O(log n)

We'll see examples later in Binary Search and Trees.

Step 5 – Take the Largest Growth

Suppose a program creates

Three variables → O(1)
One array of size n → O(n)

Overall Space

O(n)

We always keep the largest growth, because Big O describes the dominant term.

4. Example 1 – Only Variables
int sum = 0;
int max = 0;

for(int i = 0; i < arr.Length; i++)
{
    sum += arr[i];

    if(arr[i] > max)
        max = arr[i];
}
Step 1

Ignore input array.

Step 2

Variables

sum

max

i

Three variables.

Step 3

Any array?

No.

Step 4

Recursion?

No.

Final Answer

Time

O(n)

Space

O(1)
5. Example 2 – Copy Array
int[] copy = new int[arr.Length];

for(int i = 0; i < arr.Length; i++)
{
    copy[i] = arr[i];
}

Step 1

Ignore input.

Step 2

Variable

i

Step 3

New array

copy

Size

n

Final

Time

O(n)

Space

O(n)
6. Example 3 – Matrix
int[,] board = new int[n, n];

Variables

None important.

Matrix

n × n

Space

O(n²)

Time (allocation only)

O(1)

If you later initialize every element using nested loops, the time complexity becomes O(n²).

7. Example 4 – Recursive Factorial
int Factorial(int n)
{
    if (n == 1)
        return 1;

    return n * Factorial(n - 1);
}

Variables

n

Recursion depth

n

Final

Time

O(n)

Space

O(n)
8. Example 5 – Nested Loops
for(int i = 0; i < n; i++)
{
    for(int j = 0; j < n; j++)
    {
        Console.WriteLine(i + j);
    }
}

Many beginners answer

Space

O(n²)

Wrong.

Why?

No extra memory is created.

Only variables

i

j

Final

Time

O(n²)

Space

O(1)
9. Example 6 – Dictionary
Dictionary<int, string> employees
    = new Dictionary<int, string>();

for(int i = 0; i < n; i++)
{
    employees.Add(i, "Employee");
}

Dictionary grows with n.

Space

O(n)
10. Visual Decision Tree
Start

↓

Ignore Input

↓

Count Variables

↓

New Array?

↓

Yes

↓

O(n)

↓

No

↓

Recursion?

↓

Yes

↓

O(n) or O(log n)

↓

No

↓

O(1)
11. Quick Reference Table

| Code Pattern                         | Time  | Auxiliary Space |
| ------------------------------------ | ----- | --------------: |
| One variable                         | O(1)  |            O(1) |
| `for` loop with variables only       | O(n)  |            O(1) |
| New array of size `n`                | O(n)  |            O(n) |
| New matrix `n × n`                   | O(1)* |           O(n²) |
| Recursive factorial                  | O(n)  |            O(n) |
| Copy array                           | O(n)  |            O(n) |
| Nested loops with no extra structure | O(n²) |            O(1) |
| Dictionary storing `n` items         | O(n)  |            O(n) |


12. Interview Notes
Question 1

What is Auxiliary Space?

Answer

The extra memory used by an algorithm excluding the input.

Question 2

Do loops increase Space Complexity?

Answer

No.

Loops increase Time Complexity unless they allocate additional memory.

Question 3

Does recursion affect Space Complexity?

Answer

Yes.

Every recursive call creates a stack frame.

Question 4

How do we calculate Space Complexity?

Answer

Ignore the input.
Count extra variables.
Count additional data structures.
Count recursion stack.
Keep the dominant term.
13. Common Mistakes

❌ Counting the input array.

Correct:

Usually, only auxiliary space is counted.

❌ Assuming nested loops always mean O(n²) space.

Correct:

Nested loops affect time, not necessarily space.

❌ Forgetting the recursion stack.

Correct:

Recursive solutions often use additional stack memory.

14. Summary

To calculate Auxiliary Space:

✓ Ignore the input.

✓ Count variables.

✓ Count arrays, lists, stacks, queues, dictionaries, matrices.

✓ Count recursion stack.

✓ Keep the largest growth.

15. Revision Notes
Auxiliary Space

↓

Ignore Input

↓

Count Variables

↓

Count New Data Structures

↓

Count Recursion

↓

Largest Growth

↓

Final Big O
