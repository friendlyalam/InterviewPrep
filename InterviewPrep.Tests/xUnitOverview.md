xUnit — Basic Overview
1. What is xUnit?

xUnit is a testing framework for .NET applications.

It helps us automatically test our C# code.

Instead of manually doing:

Console.WriteLine(result);

and checking the output ourselves, xUnit can check it automatically.

Simple idea
Your C# Code
     ↓
   xUnit
     ↓
Does the code produce the expected result?
     ↓
   PASS / FAIL

--------------------------------------------------------------------------------------------------------------------------------

2. Why Do We Need xUnit?

Suppose we have this method:

public int Add(int a, int b)
{
    return a + b;
}

We expect:

10 + 20 = 30

We could manually test it:

Console.WriteLine(Add(10, 20));

Output:

30

But imagine an enterprise application has 500 methods.

Manually testing everything would be extremely difficult.

Instead, we write automated tests.

500 methods
   ↓
Thousands of tests
   ↓
Run all tests
   ↓
PASS / FAIL

That's where xUnit helps.

--------------------------------------------------------------------------------------------------------------------------------

3. xUnit Is Not Your Application

This distinction is important.

You have:

Production/Application Project
        ↓
    Your C# code

and:

Test Project
        ↓
    xUnit tests

For example:

DSA Solution
│
├── DSA
│   └── FindLargestElement.cs
│
└── DSA.Tests
    └── FindLargestElementTests.cs

Your actual algorithm is in:

DSA

Your tests are in:

DSA.Tests

--------------------------------------------------------------------------------------------------------------------------------
4. Basic xUnit Test

Let's start with something extremely simple.

Production code
public class Calculator
{
    public int Add(int a, int b)
    {
        return a + b;
    }
}

We want to test:

10 + 20 = 30
Test code
using Xunit;


public class CalculatorTests
{
    [Fact]
    public void Add_ShouldReturn30_WhenInputsAre10And20()
    {
        // Arrange
        Calculator calculator = new Calculator();


        // Act
        int result = calculator.Add(10, 20);


        // Assert
        Assert.Equal(30, result);
    }
}

This is the basic structure of an xUnit test.

--------------------------------------------------------------------------------------------------------------------------------

5. The Three Important Parts

Most tests follow:

Arrange
   ↓
Act
   ↓
Assert

This is extremely important.

Arrange

Prepare everything needed for the test.

Calculator calculator = new Calculator();

We're creating the object we want to test.

Act

Execute the method.

int result = calculator.Add(10, 20);

We call:

Add(10, 20)

and store the result.

--------------------------------------------------------------------------------------------------------------------------------

Assert

Check whether the result is what we expected.

Assert.Equal(30, result);

Meaning:

Expected = 30
Actual   = result

If:

result = 30

then:

30 == 30

✅ PASS

If:

result = 25

then:

30 != 25

❌ FAIL

--------------------------------------------------------------------------------------------------------------------------------

6. What Is [Fact]?

You will frequently see:

[Fact]

It tells xUnit:

"This method is a test."

Example:

[Fact]
public void Add_ShouldReturn30()
{
    // test
}

When you run the test project, xUnit discovers this method and executes it.

--------------------------------------------------------------------------------------------------------------------------------

7. What Is [Theory]?

Now suppose we want to test multiple inputs:

10 + 20 = 30
5 + 5 = 10
100 + 200 = 300

We could create three [Fact] methods.

But xUnit provides [Theory].

[Theory]
[InlineData(10, 20, 30)]
[InlineData(5, 5, 10)]
[InlineData(100, 200, 300)]
public void Add_ShouldReturnCorrectResult(
    int a,
    int b,
    int expected)
{
    Calculator calculator = new Calculator();


    int result = calculator.Add(a, b);


    Assert.Equal(expected, result);
}

Now xUnit runs the same test three times.

Conceptually:

Test 1:
10 + 20 → 30 ✅


Test 2:
5 + 5 → 10 ✅


Test 3:
100 + 200 → 300 ✅

--------------------------------------------------------------------------------------------------------------------------------
8. [Fact] vs [Theory]

Remember this simple rule:

[Fact]

Use when you have one specific scenario.

[Fact]
public void Add_ShouldReturn30()
[Theory]

Use when the same logic needs to be tested with different inputs.

[Theory]
[InlineData(...)]
[InlineData(...)]
[InlineData(...)]

For DSA, we'll use [Theory] a lot.

--------------------------------------------------------------------------------------------------------------------------------

9. Other Important Assertions

Assert.Equal() isn't the only assertion.

Equal
Assert.Equal(30, result);

Expected and actual should be equal.

Not Equal
Assert.NotEqual(50, result);

Expected and actual should NOT be equal.

True
Assert.True(result);

The value should be true.

False
Assert.False(result);

The value should be false.

Null
Assert.Null(result);

The result should be null.

Not Null
Assert.NotNull(result);

The result shouldn't be null.

Contains

For collections:

Assert.Contains(10, numbers);

Checks whether 10 exists in the collection.

--------------------------------------------------------------------------------------------------------------------------------

10. Testing Exceptions

Suppose our method does this:

public int Divide(int a, int b)
{
    if (b == 0)
    {
        throw new ArgumentException("Cannot divide by zero.");
    }


    return a / b;
}

We can test the exception:

[Fact]
public void Divide_ShouldThrowException_WhenDivisorIsZero()
{
    Calculator calculator = new Calculator();


    Assert.Throws<ArgumentException>(() =>
        calculator.Divide(10, 0));
}

We're saying:

"I expect this operation to throw ArgumentException."

11. How Tests Appear in Visual Studio

After creating your xUnit test project, Visual Studio provides Test Explorer.

Conceptually you'll see:

Test Explorer
│
├── CalculatorTests
│   ├── Add_ShouldReturn30          ✓
│   ├── Add_ShouldReturnCorrect...  ✓
│   └── Divide_ShouldThrow...       ✓
│
└── Summary
    Passed: 3
    Failed: 0

If something is wrong:

Passed: 2
Failed: 1

You can click the failed test and investigate it.

--------------------------------------------------------------------------------------------------------------------------------

12. xUnit in Our DSA Project

For our DSA learning:

DSA
│
└── FindLargestElement.cs

contains:

public class FindLargestElement
{
    public static int Find(int[] input)
    {
        // algorithm
    }
}

And:

DSA.Tests
│
└── FindLargestElementTests.cs

contains:

public class FindLargestElementTests
{
    [Theory]
    [InlineData(new[] { 10, 20, 5 }, 20)]
    [InlineData(new[] { -10, -5, -20 }, -5)]
    [InlineData(new[] { 100 }, 100)]
    public void Find_ShouldReturnLargest(
        int[] input,
        int expected)
    {
        int result = FindLargestElement.Find(input);


        Assert.Equal(expected, result);
    }
}

--------------------------------------------------------------------------------------------------------------------------------
13. The Most Important Concept

Don't think:

"xUnit is something I need to memorize."

Instead think:

[Fact]
    ↓
One specific test


[Theory]
    ↓
Same test logic + different inputs


[InlineData]
    ↓
Provides test inputs


Assert
    ↓
Checks expected vs actual

And the standard test flow:

Arrange
   ↓
Act
   ↓
Assert

That's enough for us to start.