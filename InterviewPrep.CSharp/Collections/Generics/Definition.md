Generics allow us to write a class, method, interface, 
or collection once and make it work with different data types while keeping compile-time type safety.

Instead of writing:

int Add(int a, int b)
{
    return a + b;
}

double Add(double a, double b)
{
    return a + b;
}

we can write one generic method:

T Add<T>(T a, T b)

However, for operations such as +, C# needs additional constraints/operator support, so a simpler generic example is:

static void Print<T>(T value)
{
    Console.WriteLine(value);
}

Now:

Print<int>(10);
Print<string>("Hello");
Print<double>(10.5);

The same method works with different types.

----------------------------------------------------------------------------------------------------------------------------------------------
1. Why do we need Generics?

Without generics, older C# code often used object:

public void Print(object value)
{
    Console.WriteLine(value);
}

You lose strong type information.

With generics:

public void Print<T>(T value)
{
    Console.WriteLine(value);
}

The compiler knows what T is.

Print<int>(10);
Print<string>("Hello");

Main benefits:

| Advantage           | Meaning                                             |
| ------------------- | --------------------------------------------------- |
| **Type safety**     | Errors are caught at compile time                   |
| **Code reuse**      | Write one implementation for many types             |
| **Performance**     | Avoids many unnecessary boxing/unboxing operations  |
| **Maintainability** | Less duplicate code                                 |
| **Flexibility**     | Same class/method can work with different types     |
| **Strong typing**   | Unlike using `object`, the actual type is preserved |

---------------------------------------------------------------------------------------------------------------------------
2. Generic Class
Syntax
class ClassName<T>
{
    private T _value;

    public ClassName(T value)
    {
        _value = value;
    }

    public T GetValue()
    {
        return _value;
    }
}

T is a type parameter.

Example
public class Box<T>
{
    private T _value;

    public Box(T value)
    {
        _value = value;
    }

    public T GetValue()
    {
        return _value;
    }
}

Usage:

Box<int> intBox = new Box<int>(100);

Box<string> stringBox = new Box<string>("Hello");

Console.WriteLine(intBox.GetValue());
Console.WriteLine(stringBox.GetValue());

Here:

T → int

for the first object.

And:

T → string

for the second object.

----------------------------------------------------------------------------------------------------------------------------------------------
3. Generic Method

A class doesn't have to be generic for a method to be generic.

Syntax
returnType MethodName<T>(T parameter)
{
    // logic
}
Example
public static void Print<T>(T value)
{
    Console.WriteLine(value);
}

Usage:

Print<int>(100);
Print<string>("Hello");
Print<double>(25.5);

C# can usually infer the type, so you can simply write:

Print(100);
Print("Hello");
Print(25.5);

This is called type inference.

----------------------------------------------------------------------------------------------------------------------------------------------

4. Generic Method with Multiple Types

You can have multiple type parameters.

Syntax
void Method<T1, T2>(T1 value1, T2 value2)
{
}
Example
public static void PrintPair<T1, T2>(T1 first, T2 second)
{
    Console.WriteLine($"First: {first}");
    Console.WriteLine($"Second: {second}");
}

Usage:

PrintPair(10, "John");

C# infers:

T1 = int
T2 = string

----------------------------------------------------------------------------------------------------------------------------------------------
5. Generic Class with Multiple Types
public class Pair<TKey, TValue>
{
    public TKey Key { get; set; }

    public TValue Value { get; set; }
}

Usage:

Pair<int, string> employee = new Pair<int, string>
{
    Key = 101,
    Value = "John"
};

Here:

TKey   → int
TValue → string

This is similar to:

Dictionary<int, string>

which is itself a generic collection.

----------------------------------------------------------------------------------------------------------------------------------------------

6. Generic Constraints

This is very important for interviews.

A generic type parameter can be restricted using constraints.

Why?

Suppose:

public void Process<T>(T value)
{
}

What can T be?

Almost anything.

int
string
Employee
Customer
List<int>
etc.

Sometimes we want to say:

"T must satisfy certain requirements."

That's where constraints come in.

Syntax:

where T : constraint

----------------------------------------------------------------------------------------------------------------------------------------------

7. where T : class

T must be a reference type.

public class Repository<T> where T : class
{
}

Valid:

Repository<Employee> repo = new();

assuming Employee is a class.

Not valid:

Repository<int> repo = new();

because int is a value type.

----------------------------------------------------------------------------------------------------------------------------------------------

8. where T : struct

T must be a value type.

public class Calculator<T> where T : struct
{
}

Valid:

Calculator<int> c = new();
Calculator<double> d = new();

Not valid:

Calculator<string> c = new();

----------------------------------------------------------------------------------------------------------------------------------------------
9. where T : new()

T must have a public parameterless constructor.

public class Factory<T> where T : new()
{
    public T Create()
    {
        return new T();
    }
}

Example:

public class Employee
{
    public Employee()
    {
    }
}

Then:

Factory<Employee> factory = new();

Employee employee = factory.Create();

Without:

where T : new()

this would not be allowed:

new T();

----------------------------------------------------------------------------------------------------------------------------------------------
10. where T : BaseClass

T must inherit from a specific base class.

public class Repository<T>
    where T : Entity
{
}

Example:

public class Entity
{
    public int Id { get; set; }
}

public class Employee : Entity
{
}

Valid:

Repository<Employee> repo = new();

because:

Employee
   ↓
Entity

----------------------------------------------------------------------------------------------------------------------------------------------
11. where T : Interface

T must implement an interface.

public class Service<T>
    where T : IProcessable
{
}

Example:

public interface IProcessable
{
    void Process();
}

public class Order : IProcessable
{
    public void Process()
    {
        Console.WriteLine("Processing order");
    }
}

Now:

Service<Order> service = new();

works.

----------------------------------------------------------------------------------------------------------------------------------------------

12. Multiple Constraints

You can combine constraints.

public class Repository<T>
    where T : Entity, IProcessable, new()
{
}

This means T must:

1. Inherit from Entity
2. Implement IProcessable
3. Have a public parameterless constructor

Example:

public class Employee : Entity, IProcessable
{
    public Employee()
    {
    }

    public void Process()
    {
    }
}

Valid:

Repository<Employee> repo = new();

----------------------------------------------------------------------------------------------------------------------------------------------
13. Generic Interface

Generics can also be used with interfaces.

Syntax
interface IRepository<T>
{
    T GetById(int id);
}

Example:

public interface IRepository<T>
{
    T GetById(int id);

    void Add(T entity);
}

Implementation:

public class EmployeeRepository : IRepository<Employee>
{
    public Employee GetById(int id)
    {
        return new Employee();
    }

    public void Add(Employee entity)
    {
        // Save employee
    }
}

This is extremely common in .NET applications.

----------------------------------------------------------------------------------------------------------------------------------------------

14. Generic Delegates

Generics are also used heavily with delegates.

For example:

Func<int, int> square = x => x * x;

Func<T,TResult> is a generic delegate.

Another:

Func<int, string> convert = x => x.ToString();

Here:

T        = int
TResult  = string

----------------------------------------------------------------------------------------------------------------------------------------------
15. Generics in Collections

This is where you have already seen generics extensively.

For example:

List<int>
List<string>
Dictionary<int, string>
HashSet<int>
Queue<string>
Stack<Employee>

List<T> is a generic class.

Conceptually:

List<T>

When you write:

List<int>

you are saying:

T = int

When you write:

List<Employee>

you are saying:

T = Employee

----------------------------------------------------------------------------------------------------------------------------------------------
16. Generic vs object
Without generic
public void Process(object value)
{
}

You could pass anything:

Process(10);
Process("Hello");
Process(new Employee());

The method has no compile-time restriction on the intended type.

Generic
public void Process<T>(T value)
{
}

The compiler tracks T as the actual type.

This provides stronger type safety and often avoids boxing for value types.

----------------------------------------------------------------------------------------------------------------------------------------------

17. Generic Class vs Generic Method

This is a common interview question.

Generic class:
public class Repository<T>
{
    public void Add(T entity)
    {
    }
}

The class itself knows about T.

Repository<Employee> repo = new();


Generic method:
public class Utility
{
    public void Print<T>(T value)
    {
    }
}

The method itself knows about T.

Utility utility = new();

utility.Print(10);
utility.Print("Hello");
Both can be generic
public class Repository<T>
{
    public void Process<U>(T entity, U value)
    {
    }
}

Here:

T → class-level type parameter
U → method-level type parameter

----------------------------------------------------------------------------------------------------
18. Common Generic Constraints — Interview Cheat Sheet

| Constraint             | Meaning                                      |
| ---------------------- | -------------------------------------------- |
| `where T : class`      | T must be reference type                     |
| `where T : struct`     | T must be value type                         |
| `where T : BaseClass`  | T must derive from BaseClass                 |
| `where T : IInterface` | T must implement interface                   |
| `where T : new()`      | T must have public parameterless constructor |
| `where T : notnull`    | T cannot be nullable                         |
| `where T : unmanaged`  | T must be unmanaged type                     |

You can combine compatible constraints:

where T : BaseEntity, IEntity, new()

----------------------------------------------------------------------------------------------------------------------------------------------

19. Enterprise Example

A very common .NET pattern is a generic repository:

public interface IRepository<T>
    where T : class
{
    Task<T?> GetByIdAsync(int id);

    Task AddAsync(T entity);

    Task DeleteAsync(T entity);
}

Then:

public class EmployeeRepository : IRepository<Employee>
{
    public async Task<Employee?> GetByIdAsync(int id)
    {
        // Database logic
        return null;
    }

    public async Task AddAsync(Employee entity)
    {
        // Database logic
    }

    public async Task DeleteAsync(Employee entity)
    {
        // Database logic
    }
}

The same generic abstraction can potentially be used for:

Employee
Customer
Order
Product
Invoice

instead of creating an entirely separate interface definition for each entity.

----------------------------------------------------------------------------------------------------------------------------------------------

20. Most Important Mental Model

Remember generics like this:

                 Generic
                    |
        +-----------+-----------+
        |           |           |
      Class       Method      Interface
        |           |           |
      Class<T>    Method<T>   Interface<T>
        |
    Constraints
        |
    where T : ...

    And the simplest definition to remember for an interview:

Generics allow us to write reusable, type-safe code that works with different data types without duplicating the implementation.



