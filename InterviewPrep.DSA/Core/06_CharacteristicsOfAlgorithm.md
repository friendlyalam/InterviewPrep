# Characteristics of an Algorithm

## Definition

The characteristics of an algorithm are the properties or features that every good algorithm should have.

These characteristics help us determine whether an algorithm is correct, complete, and capable of solving a problem efficiently.

---

# Simple Definition

Characteristics are the qualities of a good algorithm.

Just as a good student has qualities like honesty and discipline, a good algorithm also has certain qualities.

---

# Why Do We Need Characteristics?

Imagine two people solving the same problem.

Person A writes an algorithm that never ends.

Person B writes an algorithm that gives the correct answer in a few steps.

Which algorithm is better?

Obviously, Person B's algorithm.

Therefore, every algorithm should satisfy some basic characteristics.

---

# Characteristics of a Good Algorithm

A good algorithm should have the following characteristics.

1. Input

2. Output

3. Definiteness

4. Finiteness

5. Effectiveness

6. Correctness

7. Generality

We will study each one in detail.

============================================================

# 1. Input

## Definition

A good algorithm should accept zero or more inputs.

Input is the information provided to the algorithm before execution.

---

## Simple Definition

An algorithm should know what information it needs.

---

## Real-Life Example 1

Calculator

Input

10

20

---

## Real-Life Example 2

ATM

Input

ATM Card

PIN

Withdrawal Amount

---

## Technical Example 1

Login System

Username

Password

---

## Technical Example 2

Search Employee

Employee ID

---

## Important Point

Some algorithms do not require any input.

Example

Display "Welcome".

============================================================

# 2. Output

## Definition

Every algorithm should produce at least one output.

---

## Simple Definition

After processing, an algorithm should give a result.

---

## Real-Life Example 1

Washing Machine

Output

Clean Clothes

---

## Real-Life Example 2

Juicer

Output

Fruit Juice

---

## Technical Example 1

Calculator

Output

30

---

## Technical Example 2

Search Employee

Output

Employee Details

============================================================

# 3. Definiteness

## Definition

Every step of an algorithm must be clear, precise, and unambiguous.

---

## Simple Definition

Each instruction should have only one meaning.

The computer should never become confused.

---

## Bad Example

Cook food.

Question

How?

There are no clear steps.

---

## Good Example

Step 1

Wash vegetables.

Step 2

Cut vegetables.

Step 3

Heat oil.

Step 4

Cook for 10 minutes.

Every step is clear.

---

## Technical Example 1

Bad

Search the employee quickly.

Good

Search employee using EmployeeId.

---

## Technical Example 2

Bad

Sort the array somehow.

Good

Sort the array in ascending order.

============================================================

# 4. Finiteness

## Definition

An algorithm must finish after a finite number of steps.

---

## Simple Definition

An algorithm should never run forever.

It must stop.

---

## Real-Life Example 1

ATM

Transaction finishes after completing the request.

---

## Real-Life Example 2

Traffic Signal

Green

Yellow

Red

The cycle repeats, but one complete cycle has a defined sequence.

---

## Technical Example 1

Find Maximum in Array

After checking all elements,

algorithm stops.

---

## Technical Example 2

Linear Search

After checking every element,

algorithm finishes.

============================================================

# 5. Effectiveness

## Definition

Every step of an algorithm should be simple and executable.

---

## Simple Definition

Every instruction should actually be possible to perform.

---

## Real-Life Example 1

"Open the door."

Possible.

---

"Fly without wings."

Impossible.

Not an effective instruction.

---

## Real-Life Example 2

Count students in a classroom.

Possible.

Count every star in the universe manually.

Not practical.

---

## Technical Example 1

Add two numbers.

Possible.

---

## Technical Example 2

Compare two strings.

Possible.

============================================================

# 6. Correctness

## Definition

A good algorithm should always produce the correct output for valid input.

---

## Simple Definition

Correct input should produce the correct answer.

---

## Real-Life Example 1

Calculator

10 + 20 = 30

Correct.

---

## Real-Life Example 2

Railway Fare

Correct fare should be calculated.

---

## Technical Example 1

Sorting

Output must always be sorted.

---

## Technical Example 2

Binary Search

Should return the correct index.

============================================================

# 7. Generality

## Definition

An algorithm should solve all valid instances of a problem, not just one example.

---

## Simple Definition

The algorithm should work for many different inputs.

---

## Real-Life Example 1

Calculator

Works for

5 + 10

50 + 100

500 + 1000

---

## Real-Life Example 2

ATM

Works for every customer.

---

## Technical Example 1

Reverse Array

Should work for arrays of different sizes.

---

## Technical Example 2

Palindrome Program

Should work for every valid string.

============================================================

# Summary

A good algorithm should:

✓ Accept Input

✓ Produce Output

✓ Have Clear Steps

✓ Finish in Finite Time

✓ Be Practical

✓ Produce Correct Results

✓ Work for Different Inputs

============================================================

# Interview Notes

Interviewers often ask:

"What makes a good algorithm?"

Answer:

A good algorithm should satisfy all seven characteristics discussed above.

Simply knowing the definition is not enough.

You should also explain each characteristic with an example.

============================================================

# Common Mistakes

❌ Writing ambiguous steps.

❌ Creating an infinite loop.

❌ Producing incorrect output.

❌ Writing an algorithm that works for only one input.

============================================================

# Revision Notes

Input

↓

Processing

↓

Output

+

Clear Steps

+

Finite Steps

+

Correct Result

+

Works for all valid inputs

=

Good Algorithm