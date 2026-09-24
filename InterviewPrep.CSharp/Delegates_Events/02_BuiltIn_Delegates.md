# Action, Func and Predicate in C#

.NET provides built-in delegate types so that we usually don't need to define custom delegates for simple scenarios.

The three most important ones are:

```text
Action
Func
Predicate
1. Action

Action represents a method that returns void.

No Parameters
Action action = () =>
{
    Console.WriteLine("Hello");
};

action();

Signature:

() → void
2. Action with Parameters
Action<string> print = message =>
{
    Console.WriteLine(message);
};

Signature:

string → void

Multiple parameters:

Action<int, int> addAndPrint = (a, b) =>
{
    Console.WriteLine(a + b);
};
3. Func

Func represents a method that returns a value.

Important rule:

The last generic parameter is always the return type.

Example:

Func<int, int, int> add =
    (a, b) => a + b;

Meaning:

int + int → int
4. More Func Examples
Func<int, bool> isEven =
    number => number % 2 == 0;

Meaning:

int → bool

Another:

Func<string, int> getLength =
    text => text.Length;

Meaning:

string → int
5. Action vs Func
Feature	Action	Func
Return value	No	Yes
Return type	void	Last generic parameter
Example	Action<int>	Func<int, bool>
Common use	Perform operation	Calculate/transform

Example:

Action<string> log =
    message => Console.WriteLine(message);
Func<int, bool> isEven =
    number => number % 2 == 0;
6. Predicate

Predicate<T> represents:

T → bool

Example:

Predicate<int> isEven =
    number => number % 2 == 0;

Usage:

bool result = isEven(10);

Result:

true
7. Predicate and Collections

You will commonly see predicates in collection APIs.

Example:

List<int> numbers =
    new() { 1, 2, 3, 4, 5 };

int result =
    numbers.Find(x => x > 3);

The condition:

x => x > 3

represents behavior that returns bool.

8. Action, Func and Predicate Mental Model
Action<T>
    ↓
T → void

Func<T, TResult>
    ↓
T → TResult

Predicate<T>
    ↓
T → bool
9. Action with Multiple Parameters
Action<int, string> print =
    (id, name) =>
    {
        Console.WriteLine(
            $"{id}: {name}");
    };

print(101, "John");
10. Func with Multiple Parameters
Func<int, int, int> multiply =
    (a, b) => a * b;

int result = multiply(10, 5);

Result:

50
11. Func for Business Logic
Func<decimal, decimal> calculateDiscount =
    amount => amount * 0.90m;

decimal finalAmount =
    calculateDiscount(1000);

Result:

900

This is a simple form of behavior injection.

12. Delegate Type Inference

Instead of:

Func<int, int> square =
    (int x) => x * x;

C# can infer the parameter type:

Func<int, int> square =
    x => x * x;

The target delegate tells the compiler that x is an int.

13. Common LINQ Connection

Consider:

var result = numbers
    .Where(x => x > 10)
    .Select(x => x * 2);

Conceptually:

Where
→ receives a condition

Select
→ receives a transformation

The lambdas are converted to compatible delegate types.

This is why understanding delegates makes LINQ easier to understand.

14. Predicate vs Func<T, bool>

Both can represent a condition:

Predicate<int> p =
    x => x > 10;

and:

Func<int, bool> f =
    x => x > 10;

Both return bool.

However, they are different delegate types.

They are not interchangeable merely because their signatures look similar.

15. Async Delegates

Avoid this pattern:

Action action = async () =>
{
    await Task.Delay(1000);
};

The problem is that Action represents:

() → void

An async lambda assigned to Action becomes effectively async void.

For async callbacks, prefer:

Func<Task> action = async () =>
{
    await Task.Delay(1000);
};

await action();

For a parameter:

Func<int, Task> processor =
    async orderId =>
    {
        await ProcessOrderAsync(orderId);
    };
16. Product-Company Example

Suppose a generic processor needs configurable behavior:

public class Processor
{
    public TResult Execute<TInput, TResult>(
        TInput input,
        Func<TInput, TResult> operation)
    {
        return operation(input);
    }
}

Usage:

var processor = new Processor();

int result =
    processor.Execute(
        10,
        x => x * 2);

Result:

20

This demonstrates:

Input
  ↓
Delegate
  ↓
Behavior
  ↓
Result
17. When to Use Which?
Use Action when:

You need:

input → no result

Example:

Action<Order> audit;
Use Func when:

You need:

input → result

Example:

Func<Order, bool> validator;
Use Predicate when:

You specifically need a condition:

T → bool

Example:

Predicate<Order> isValid;
18. Interview Questions
What is Action?

A built-in delegate representing a method that returns void.

What is Func?

A built-in delegate where the last generic parameter is the return type.

What is Predicate?

A delegate representing a method that takes T and returns bool.

Can Action return a value?

No.

Can Func return void?

Func is intended for a return value. Use Action for void.

Why use Func<Task> instead of Action for async callbacks?

Because Func<Task> allows the asynchronous operation to be awaited and its exceptions/cancellation to be observed properly.

19. Key Points
Action
→ void

Func
→ returns value

Predicate
→ returns bool

Action<T>
→ T → void

Func<T, TResult>
→ T → TResult

Predicate<T>
→ T → bool

Async callback
→ Prefer Func<Task> / Func<T, Task>
Interview Definition

Action, Func, and Predicate<T> are built-in generic delegate types provided by .NET for representing methods
that respectively return no value, return a value, or evaluate a condition.