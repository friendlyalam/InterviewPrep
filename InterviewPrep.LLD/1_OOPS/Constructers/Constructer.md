Constructor in C#

A constructor is a special member that runs when an object is created and is primarily used to put the object into a valid initial state.

Constructor — Must Remember
Constructor initializes an object when new is used.
Constructor name must be the same as the class name.
Constructor has no return type, not even void.
Constructors can be overloaded.
Constructors can have parameters.
this() → calls another constructor in the same class.
base() → calls a constructor of the parent class.
Constructors are not inherited.
Constructors cannot be virtual or override.
A constructor can be private — useful for Singleton/factory patterns.
A static constructor initializes static members and runs at most once per type.
If you don't define an instance constructor, C# may provide a parameterless constructor automatically.
Once you define a constructor, the compiler doesn't automatically provide the parameterless constructor.
In inheritance, base-class constructor executes before derived-class constructor.
Constructor should establish a valid initial state of the object.
Avoid putting heavy business logic, database calls, or API calls inside constructors.
Constructor Injection is the preferred way to provide dependencies in ASP.NET Core.
For required dependencies, prefer private readonly fields + constructor injection.
Constructors cannot be called like normal methods; they execute as part of object creation.
Constructor ≠ destructor/finalizer — constructor creates/initializes; finalizer is related to last-chance cleanup.
⭐ One-line interview memory

Constructor = Create and initialize an object into a valid state.

And the three things you should immediately remember:

this()  → same class
base()  → parent class
static constructor → once per type

public class Employee
{
    public int Id { get; }
    public string Name { get; }

    public Employee(int id, string name)
    {
        Id = id;
        Name = name;
    }
}

Employee employee = new Employee(101, "John");

Here:

new Employee(...)
       ↓
Constructor executes
       ↓
Object initialized

2. Types of constructors you should know

| Constructor                   | Importance |
| ----------------------------- | ---------- |
| Parameterless                 | ⭐⭐⭐        |
| Parameterized                 | ⭐⭐⭐⭐⭐      |
| Constructor overloading       | ⭐⭐⭐⭐       |
| `this()` constructor chaining | ⭐⭐⭐⭐       |
| `base()` constructor chaining | ⭐⭐⭐⭐⭐      |
| Private constructor           | ⭐⭐⭐⭐       |
| Static constructor            | ⭐⭐⭐⭐⭐      |
| Primary constructor           | ⭐⭐⭐        |
| Copy-constructor pattern      | ⭐⭐⭐        |


3. Parameterless constructor
public class Employee
{
    public Employee()
    {
        Console.WriteLine("Employee created");
    }
}

Usage:

Employee employee = new Employee();

If you don't define any instance constructor in a class, the compiler can provide a public parameterless constructor.
But once you define a constructor yourself, that automatic constructor is no longer provided.

4. Parameterized constructor

Very common in real applications.

public class Order
{
    public int Id { get; }
    public decimal Amount { get; }

    public Order(int id, decimal amount)
    {
        Id = id;
        Amount = amount;
    }
}

Usage:

var order = new Order(101, 2500);

The benefit is that the object cannot be created without required information.

This is especially important for maintaining valid domain objects.


5. Constructor overloading

A class can have multiple constructors with different parameter lists.

public class Employee
{
    public Employee()
    {
    }

    public Employee(int id)
    {
    }

    public Employee(int id, string name)
    {
    }
}

This is called constructor overloading.


6. this() constructor chaining

Instead of duplicating initialization logic:

public class Employee
{
    public int Id { get; }
    public string Name { get; }

    public Employee()
        : this(0, "Unknown")
    {
    }

    public Employee(int id, string name)
    {
        Id = id;
        Name = name;
    }
}

Now:

new Employee();

internally calls:

Employee()
   ↓
this(0, "Unknown")
   ↓
Employee(int, string)

Interview point: this() is used to call another constructor in the same class.


7. base() constructor

Used when inheritance is involved.

public class Employee
{
    protected string Name;

    public Employee(string name)
    {
        Name = name;
    }
}

public class Manager : Employee
{
    public Manager(string name)
        : base(name)
    {
    }
}

Execution:

new Manager("John")
       ↓
Employee constructor
       ↓
Manager constructor

base() calls a constructor of the base class.

Interview distinction
this(...)

→ same class

base(...)

→ parent/base class


8. Private constructor

A private constructor prevents outside code from directly creating an instance.

public class Configuration
{
    private Configuration()
    {
    }
}

This is commonly encountered with patterns such as Singleton and factory-based creation.

Example:

public class Logger
{
    private Logger()
    {
    }

    public static Logger Create()
    {
        return new Logger();
    }
}

Outside:

// new Logger(); ❌

But:

Logger logger = Logger.Create(); // ✅

Private constructors are explicitly supported for controlling object creation.


9. Static constructor ⭐⭐⭐⭐⭐

Very important for interviews.

public class DatabaseConfig
{
    static DatabaseConfig()
    {
        Console.WriteLine("Static constructor");
    }

    public DatabaseConfig()
    {
        Console.WriteLine("Instance constructor");
    }
}

A static constructor:

has no access modifier
has no parameters
runs automatically
runs at most once for the type
initializes static state

Microsoft documents that the static constructor runs before the instance-constructor initialization for the first instance and at most once.

Example:

public class AppConfig
{
    public static string Environment;

    static AppConfig()
    {
        Environment = "Production";
    }
}


10. Primary constructor — modern C#

Since C# 12, classes and structs can use primary constructors.

Traditional:

public class Employee
{
    public string Name { get; }

    public Employee(string name)
    {
        Name = name;
    }
}

Primary constructor:

public class Employee(string name)
{
    public string Name => name;
}

You should know this because modern .NET codebases may use it.



11. Constructor injection ⭐⭐⭐⭐⭐

This is extremely important for product-company .NET development and LLD.

public interface IPaymentService
{
    void Process();
}

public class OrderService
{
    private readonly IPaymentService _paymentService;

    public OrderService(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }
}

ASP.NET Core DI:

builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<OrderService>();

The framework creates:

OrderService
      ↓
IPaymentService
      ↓
PaymentService

This connects directly to Dependency Inversion Principle + Dependency Injection + LLD.


12. Constructor best practices
✅ Do
public Order(int customerId)
{
    if (customerId <= 0)
        throw new ArgumentException("Invalid customer ID.");

    CustomerId = customerId;
}

Use constructors to establish valid object state.

❌ Don't put heavy business operations inside constructors

Avoid:

public Order()
{
    CallExternalApi();
    SendEmail();
    SaveToDatabase();
}

Constructors should generally initialize the object, not perform large workflows.



17. Interview-level question: Is GC.Collect() a replacement for Dispose?

No.

GC.Collect();

doesn't mean:

"Immediately clean up all my resources."

GC manages managed memory. Dispose() is for deterministic resource cleanup.

So:

GC
↓
Managed memory

Dispose
↓
Deterministic resource cleanup

This distinction is very important in .NET interviews.

18. Another important interview question
Can constructor be inherited?

No.

Constructors belong to the class that defines them. A derived class has to define its own constructors.

Can constructor be virtual?

No.

You cannot declare:

public virtual Employee()
{
}

Constructors cannot be virtual or overridden.

Can constructor be static?

Yes.

static MyClass()
{
}

But it is a static constructor, not an instance constructor.

Can constructor be private?

Yes.

private MyClass()
{
}

Useful for controlled object creation, Singleton, factories, etc.

Can finalizer be overloaded?

No.

A class can have at most one finalizer because it has no parameters.

19. Most important execution order

For a derived class:

class Parent
{
    public Parent()
    {
        Console.WriteLine("Parent");
    }
}

class Child : Parent
{
    public Child()
    {
        Console.WriteLine("Child");
    }
}
var obj = new Child();

Output:

Parent
Child

Conceptually:

new Child()
    ↓
Object/base initialization
    ↓
Parent constructor
    ↓
Child constructor

The full initialization process is more detailed: field initialization, base initialization/constructors, the current constructor, and then object initializers.

20. What you actually need for product companies

For your C# + LLD preparation, I would prioritize it like this:

Must know deeply ⭐⭐⭐⭐⭐
Parameterized constructors
Constructor overloading
this()
base()
Constructor execution order
Private constructors
Static constructors
Constructor injection / DI
Constructor + inheritance
Constructor + object initialization
Know well ⭐⭐⭐⭐
Primary constructors
Constructor validation
Constructor vs factory method
Constructor vs property initialization
Immutability through constructors
Know conceptually ⭐⭐⭐
Finalizers
GC and finalization
IDisposable
Dispose() vs finalizer
GC.SuppressFinalize()
SafeHandle
Don't spend excessive time on

❌ Writing complex finalizers
❌ Trying to control exactly when a finalizer executes
❌ Using finalizers for ordinary managed objects

The interview mental model

Remember this:

CONSTRUCTOR
    ↓
Create + initialize valid object
    ↓
Used frequently
    ↓
Deterministic


IDisposable / Dispose()
    ↓
Release resources deterministically
    ↓
Preferred cleanup mechanism


FINALIZER (~ClassName)
    ↓
Last-chance unmanaged-resource cleanup
    ↓
GC controlled
    ↓
Non-deterministic
    ↓
Used rarely

This is the level of constructors + destructors/finalizers I would expect you to know for a senior .NET/product-company interview.

