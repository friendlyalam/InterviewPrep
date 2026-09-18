13. What is a destructor in C#?

This is where C# differs significantly from C++.

C# technically uses a finalizer, although the syntax looks like a destructor:

public class MyClass
{
    ~MyClass()
    {
        Console.WriteLine("Finalizer executed");
    }
}

The ~MyClass() syntax defines a finalizer. It is invoked automatically by the runtime/GC and 
you cannot explicitly call it. Its execution time is nondeterministic.


14. Very important: Constructor vs Finalizer

| Constructor                 | Finalizer                                          |
| --------------------------- | -------------------------------------------------- |
| Initializes object          | Performs last-chance cleanup                       |
| Runs when object is created | Runs when object becomes eligible for finalization |
| Called by object creation   | Called by GC/runtime                               |
| Deterministic               | **Non-deterministic**                              |
| Can have parameters         | Cannot have parameters                             |
| Can be overloaded           | Cannot be overloaded                               |
| Can be called through `new` | Cannot be explicitly called                        |
| Used frequently             | Used very rarely                                   |


Microsoft explicitly notes that finalizers are used very rarely in C#


15. Don't use finalizer for normal resource cleanup

This is a very important product-company interview point.

Bad approach:

public class FileManager
{
    ~FileManager()
    {
        // cleanup
    }
}

Why?

Because you don't know exactly when the finalizer will execute.

Instead use:

public class FileManager : IDisposable
{
    public void Dispose()
    {
        // deterministic cleanup
    }
}

Then:

using var manager = new FileManager();

Conceptually:

using
  ↓
resource acquired
  ↓
work
  ↓
Dispose()
  ↓
resource released

For deterministic cleanup, Microsoft recommends Dispose rather than relying on finalization.


