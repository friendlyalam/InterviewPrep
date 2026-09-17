# Immutability

## 1. Definition

**Immutability** means an object's state cannot be changed after it is created.

Instead of modifying an existing object, create a new object with the required values.

```text
Mutable:
Object → Change State

Immutable:
Object → Cannot Change
              ↓
        Create New Object
2. Why Immutability Matters

Immutability is important because it makes code:

Easier to reason about
Safer in multithreaded applications
Easier to test
Less prone to accidental modification
Easier to cache
Easier to share between components
3. Mutable vs Immutable
Mutable
User user = new();

user.Name = "John";
user.Name = "David";

The same object changes state.

Immutable
User user1 = new("John");

User user2 = user1 with
{
    Name = "David"
};

user1 remains unchanged.

4. Common Immutable Types in C#

Common immutable types include:

string
Primitive/value types such as int, double, bool
DateTime
DateTimeOffset
Guid
TimeSpan
Uri
record types when designed with immutable properties

Important:

readonly does not automatically make an object immutable.

5. readonly vs Immutability
private readonly List<string> _items;

The reference cannot point to another list, but the list itself can still change:

_items.Add("Order");

Therefore:

readonly protects the reference, not the object's internal state.

6. Creating an Immutable Class

Use:

Private setters or init
Constructor initialization
No public mutation methods
Avoid exposing mutable internal collections

Example:

public sealed class Customer
{
    public string Name { get; }
    public int Age { get; }

    public Customer(
        string name,
        int age)
    {
        Name = name;
        Age = age;
    }
}

After construction, the properties cannot be changed.

7. init Properties

Modern C# supports init:

public class Customer
{
    public string Name { get; init; } = string.Empty;
    public int Age { get; init; }
}

Usage:

Customer customer = new()
{
    Name = "John",
    Age = 30
};

The properties can be assigned during initialization but not normally changed afterward.

8. Records

Records are useful for immutable data models and value-based equality.

public record Customer(
    string Name,
    int Age);

Usage:

Customer customer1 =
    new("John", 30);

Customer customer2 =
    customer1 with
    {
        Age = 31
    };

customer1 is unchanged.

9. with Expression

with creates a new object based on an existing record.

var updatedCustomer =
    customer with
    {
        Age = 31
    };

Conceptually:

Customer 1
Name = John
Age  = 30

        ↓ with

Customer 2
Name = John
Age  = 31
10. Immutable Collections

For collections, consider:

ImmutableList<T>
ImmutableDictionary<TKey,TValue>
ImmutableHashSet<T>
IReadOnlyList<T>
IReadOnlyDictionary<TKey,TValue>

System.Collections.Immutable provides immutable collection types.

ImmutableList<int> numbers =
    ImmutableList.Create(1, 2, 3);

ImmutableList<int> updated =
    numbers.Add(4);

The original collection remains unchanged.

11. Immutability and Thread Safety

Immutable objects are naturally easier to share between threads.

Thread 1 ──┐
Thread 2 ──┼──> Same immutable object
Thread 3 ──┘

No thread can modify its state.

Therefore, many synchronization problems disappear.

But:

Immutability does not automatically make every surrounding operation thread-safe.

12. Shallow vs Deep Immutability

Consider:

public class Order
{
    public List<string> Items { get; init; }
        = new();
}

Although the property uses init, the list itself is mutable:

order.Items.Add("Laptop");

So the object is not deeply immutable.

True immutability requires immutable state throughout the object graph.

13. Exposing Collections Safely

Avoid:

public List<string> Items { get; }

because callers can modify the list.

Prefer:

public IReadOnlyList<string> Items { get; }

or an immutable collection.

However, IReadOnlyList<T> is only a read-only view; the underlying collection could still be changed internally.

14. Immutability in Multithreading

Instead of:

Multiple threads
      ↓
Shared mutable object
      ↓
Locks / race conditions

Prefer:

Multiple threads
      ↓
Immutable data
      ↓
Independent processing

This can reduce:

Lock contention
Race conditions
Synchronization complexity
15. Immutability and Functional Programming

Immutability is a major concept in functional programming.

Instead of:

order.Status = "Completed";

prefer:

Order completedOrder =
    order with
    {
        Status = "Completed"
    };

The original object remains unchanged.

16. Immutability and Caching

Immutable objects are good cache values because their state cannot unexpectedly change after being cached.

Create Object
     ↓
Cache Object
     ↓
Many Readers
     ↓
No Mutation

This reduces synchronization requirements.

17. Immutability in ASP.NET Core

Immutable DTOs and request/response models can make application code easier to reason about.

For example:

public record OrderResponse(
    int OrderId,
    string Status);

Once created, the response data is not accidentally modified by another component.

18. Immutability Does Not Mean No Objects Change

The important distinction is:

Mutable approach:
Modify existing object

Immutable approach:
Create new object with changed state

The application state can still change; individual objects do not.

19. Performance Consideration

Immutability can sometimes create additional allocations because new objects may be created instead of modifying existing ones.

However, this can be worth the cost when it provides:

Safer concurrency
Easier reasoning
Better caching
Reduced locking

Do not assume immutable code is always faster.

20. Common Mistakes
Mistake 1

Thinking readonly automatically means immutable.

Mistake 2

Using init but exposing a mutable collection.

Mistake 3

Returning internal mutable collections.

Mistake 4

Assuming IReadOnlyList<T> guarantees deep immutability.

Mistake 5

Creating excessive objects without considering performance.

21. Immutability vs Readonly

| `readonly`                                         | Immutability                   |
| -------------------------------------------------- | ------------------------------ |
| Restricts reassignment of a field                  | Prevents state mutation        |
| Reference may point to immutable or mutable object | Object state cannot change     |
| Mainly language feature                            | Design principle               |
| Does not guarantee thread safety                   | Often simplifies thread safety |


22. Immutability vs Thread Safety
| Immutability                          | Thread Safety                       |
| ------------------------------------- | ----------------------------------- |
| Object cannot change                  | Concurrent access is safely handled |
| Reduces synchronization needs         | May require locks/atomic operations |
| Naturally safe to share in many cases | Can support mutable shared state    |
| Design approach                       | Correctness property                |


Immutable objects are easier to make thread-safe, but thread safety is a broader concept.

23. Interview Questions
Q1. What is immutability?

An immutable object cannot change its state after creation.

Q2. Why is immutability useful in multithreading?

Multiple threads can safely read immutable state without coordinating mutations.

Q3. Does readonly make an object immutable?

No. It prevents reassignment of the field, but the referenced object can still be mutable.

Q4. Why are strings immutable in C#?

It makes strings safer to share, supports string interning, and prevents unexpected modification of existing string instances.

Q5. What is the benefit of records?

Records provide convenient value-based equality and support concise immutable-style data modeling.

Q6. Is IReadOnlyList<T> immutable?

No. It prevents mutation through that interface, but the underlying collection may still be modified elsewhere.

Q7. What is deep immutability?

All reachable mutable state is protected so the object's entire state graph cannot be changed.

24. Key Points
Immutable object state cannot change after creation.
string is immutable.
readonly does not guarantee immutability.
init helps create immutable-style properties.
Records are useful for immutable data models.
with creates a modified copy.
Immutable collections are useful for immutable collection state.
Immutable objects are easier to share between threads.
Avoid exposing mutable internal collections.
IReadOnlyCollection<T> and IReadOnlyList<T> are not necessarily deeply immutable.
Immutability can reduce locking and race-condition risks.
Immutability can increase allocations.
Choose immutability where safety and simplicity outweigh mutation-based performance needs.
Final Mental Model
Mutable Object
     ↓
Shared Mutation
     ↓
Race Condition Risk
     ↓
Synchronization Required


Immutable Object
     ↓
No Mutation
     ↓
Safe Sharing
     ↓
Less Synchronization

Product-company one-line summary:
Immutability means an object's state cannot change after creation, making objects easier to reason about,
cache, test, and safely share across concurrent code while reducing the need for synchronization.