Non-generic means the code works with a fixed type or uses object instead of a type parameter like <T>.

1. Simple non-generic class
public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
}

Here, Id is always int and Name is always string.

There is no <T>.

2. Non-generic method
public int Add(int a, int b)
{
    return a + b;
}

This method specifically accepts int.

If you want double, you need another method:

public double Add(double a, double b)
{
    return a + b;
}

That's one limitation of non-generic code: less reusable.

3. Non-generic collections

C# has older collections that are non-generic.

Common examples:

ArrayList
Hashtable
Queue
Stack
SortedList

They are mainly in:

System.Collections
Example: ArrayList
ArrayList list = new();

list.Add(10);
list.Add("Hello");
list.Add(20.5);

It can contain different types:

10       → int
"Hello"  → string
20.5     → double

That's because internally values are handled as object.

4. Generic vs Non-Generic
Generic
List<int> numbers = new();

numbers.Add(10);
numbers.Add(20);

Only int is expected.

numbers.Add("Hello"); // ❌ Compile-time error
Non-generic
ArrayList numbers = new();

numbers.Add(10);
numbers.Add("Hello");

Different types can be stored.

5. Main difference

| Generic                            | Non-Generic                 |
| ---------------------------------- | --------------------------- |
| Uses `<T>`                         | No `<T>`                    |
| Strong type safety                 | Less type safety            |
| Usually preferred                  | Mostly legacy               |
| Better performance for value types | May involve boxing/unboxing |
| `List<int>`                        | `ArrayList`                 |
| `Dictionary<int,string>`           | `Hashtable`                 |


Simple interview definition

Non-generic collections/classes don't use type parameters and generally work with fixed types or object. 
Generic code uses type parameters such as <T> to provide reusable and type-safe code.

For modern .NET development, prefer generic collections such as List<T>, Dictionary<TKey,TValue>, HashSet<T>,
Queue<T>, and Stack<T> over their non-generic/legacy counterparts.