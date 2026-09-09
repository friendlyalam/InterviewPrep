The key point is: constructors do not execute based on parameterized/parameterless/public/private order.
They execute based on how you create/access the object and whether a static constructor is involved.

Example
public class Employee
{
    static Employee()
    {
        Console.WriteLine("Static");
    }

    public Employee()
    {
        Console.WriteLine("Parameterless Public");
    }

    public Employee(int id)
    {
        Console.WriteLine("Parameterized Public");
    }

    private Employee(string name)
    {
        Console.WriteLine("Parameterized Private");
    }

    public static Employee Create(string name)
    {
        return new Employee(name);
    }
}

Suppose:

Employee e1 = new Employee();

Execution:

1. Static constructor
2. Parameterless public constructor

If:

Employee e2 = new Employee(101);

Execution:

1. Static constructor   ← only the first time the type needs initialization
2. Parameterized public constructor

If:

Employee e3 = Employee.Create("John");

Execution:

1. Static constructor   ← if not already executed
2. Private constructor

The private constructor can execute, but only from code that has access to it, such as another member of the same class.

Most important rule
Static constructor
       ↓
Runs automatically ONCE for the type
       ↓
Selected instance constructor
       ↓
Parameterless / parameterized / private / public
       ↓
Depends on the `new` call

So there is no fixed order like:

Static
↓
Private
↓
Public
↓
Parameterless
↓
Parameterized

That would be incorrect.

⭐ Remember

Static constructor: once per type, before the type is first used/initialized.

Instance constructor: only the constructor selected by your new expression executes.

Private/public: access modifier doesn't determine execution order; it determines who can call the constructor.

Parameterless/parameterized: the arguments in new determine which constructor is selected.