# Lambda Expressions and Delegates in C#

Lambda expressions are one of the most common ways delegates are created and used in modern C#.

---

# 1. What is a Lambda?

A lambda is a concise way of writing an anonymous function.

Basic syntax:

```csharp
x => x * 2

Meaning:

input x
   ↓
multiply by 2
   ↓
return result
2. Basic Lambda
Func<int, int> square =
    x => x * x;

Usage:

int result = square(5);

Result:

25
3. Lambda with Multiple Parameters
Func<int, int, int> add =
    (a, b) => a + b;

Usage:

int result = add(10, 20);
4. Lambda with Statement Body

Expression body:

Func<int, int> square =
    x => x * x;

Statement body:

Func<int, int> square =
    x =>
    {
        int result = x * x;

        return result;
    };
5. Lambda and Delegate Relationship

A lambda expression can be converted to a compatible delegate.

Func<int, int> square =
    x => x * x;

Mental model:

Lambda
   ↓
Compatible delegate type
   ↓
Delegate variable

The lambda itself is not a named delegate type.

6. Lambda Without Parameters
Action action =
    () => Console.WriteLine("Hello");

action();
7. Lambda with One Parameter
Action<string> print =
    message => Console.WriteLine(message);
8. Lambda with Multiple Parameters
Action<int, int> printSum =
    (a, b) =>
    {
        Console.WriteLine(a + b);
    };
9. Lambda in LINQ

Example:

var result = numbers
    .Where(x => x > 10)
    .Select(x => x * 2);

The lambda:

x => x > 10

represents filtering behavior.

The lambda:

x => x * 2

represents transformation behavior.

10. Lambda as a Callback
public void Process(
    Action<string> callback)
{
    Console.WriteLine("Processing...");

    callback("Completed");
}

Usage:

Process(
    message =>
    {
        Console.WriteLine(message);
    });
11. Closures

A lambda can capture variables from its surrounding scope.

Example:

int discount = 10;

Func<int, int> calculate =
    price => price - discount;

The lambda uses:

price
discount

discount is captured by the lambda.

This is called a closure.

12. Why Closures Matter

Captured variables can outlive the method in which they were originally declared.

This matters for:

memory lifetime
loops
callbacks
asynchronous code
allocations
concurrency

Example:

Func<int> CreateCounter()
{
    int count = 0;

    return () =>
    {
        count++;
        return count;
    };
}

Usage:

var counter = CreateCounter();

Console.WriteLine(counter());
Console.WriteLine(counter());
Console.WriteLine(counter());

Output:

1
2
3

The lambda captured count.

13. Lambda in Sorting
List<int> numbers =
    new() { 5, 2, 9, 1 };

numbers.Sort(
    (a, b) => a.CompareTo(b));

The lambda provides comparison behavior.

14. Lambda in Filtering
var evenNumbers =
    numbers.Where(x => x % 2 == 0);
15. Lambda in Projection
var doubled =
    numbers.Select(x => x * 2);
16. Lambda in Dependency/Behavior Injection
public class PriceCalculator
{
    private readonly Func<decimal, decimal> _discount;

    public PriceCalculator(
        Func<decimal, decimal> discount)
    {
        _discount = discount;
    }

    public decimal Calculate(decimal price)
    {
        return _discount(price);
    }
}

Usage:

var calculator =
    new PriceCalculator(
        price => price * 0.90m);

This is useful for simple, stateless behavior.

17. Lambda vs Named Method

Named method:

static int Square(int x)
{
    return x * x;
}

Func<int, int> operation = Square;

Lambda:

Func<int, int> operation =
    x => x * x;

Use a lambda when the behavior is:

small
local
easy to understand

Use a named method when the behavior is:

reused
complex
business-critical
easier to test independently
18. Lambda vs Local Function

Lambda:

Func<int, int> square =
    x => x * x;

Local function:

int Square(int x)
{
    return x * x;
}

Local functions can be useful when:

logic is larger
recursion is needed
you want clearer debugging
you don't need delegate conversion
19. Expression Trees

This is an important advanced distinction.

A lambda can target a delegate:

Expression<Func<Order, bool>> expression =
    order => order.Amount > 1000;

Now the lambda is represented as an expression tree instead of simply executable delegate code.

This is important in technologies such as:

Entity Framework Core
LINQ providers
query translation

For example:

dbContext.Orders
    .Where(order => order.Amount > 1000);

The provider can inspect the expression and translate it to SQL.

20. Delegate vs Expression Tree
Delegate

Represents executable behavior.

Func<Order, bool>
Expression Tree

Represents the structure of the expression.

Expression<Func<Order, bool>>

Mental model:

Func
→ "Execute this"

Expression<Func<...>>
→ "Describe this expression"
21. Product-Company Interview Questions
What is a lambda?

A concise anonymous function that can be converted to a compatible delegate or expression tree.

Is a lambda itself a delegate?

Not exactly.

It is an expression that can be converted to a compatible delegate.

What is a closure?

A lambda/local function capturing variables from its surrounding scope.

Why do LINQ methods use lambdas?

Because LINQ methods accept delegate or expression-based behavior for filtering, projection, ordering, etc.

Delegate vs Expression Tree?

Delegate executes behavior.

Expression tree represents the structure of behavior and can be inspected or translated.

22. Key Points
Lambda
→ concise anonymous function

Lambda + delegate target
→ executable delegate

Closure
→ captured outer variable

LINQ
→ heavily uses lambdas/delegates

Expression<Func<T,...>>
→ expression tree

Func<T,...>
→ executable delegate

Small local behavior
→ lambda

Complex/reusable behavior
→ named method/class
Interview Definition

A lambda expression is a concise way to define an anonymous function that can be converted to a compatible delegate or expression tree.