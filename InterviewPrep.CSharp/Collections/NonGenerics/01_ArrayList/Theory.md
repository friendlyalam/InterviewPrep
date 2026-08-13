Type: ❌ Non-generic
Namespace: System.Collections
Modern usage: ⚠️ Mostly legacy
Generic replacement: List<T>
Product-company relevance: ⭐⭐⭐ — mainly interview/legacy-code knowledge

------------------------------------------------------------------------------------------------------------

1. Definition

ArrayList is a non-generic, dynamically sized collection that stores elements as object.

Example:

ArrayList numbers = new();

numbers.Add(10);
numbers.Add("Hello");
numbers.Add(20.5);

It can store different types in the same collection:

10
"Hello"
20.5

That's the main difference from:

List<int>

where every element must be an int.

2. Why Was ArrayList Created?

Before generics were introduced in .NET 2.0, developers needed a dynamically growing collection.

So they used:

ArrayList

After generics were introduced, the preferred approach became:

List<T>

Therefore:

ArrayList
   ↓
old .NET code

List<T>
   ↓
modern C#
3. Basic Syntax
using System.Collections;

ArrayList numbers = new ArrayList();

Modern syntax:

ArrayList numbers = new();
4. Adding Elements
numbers.Add(10);
numbers.Add(20);
numbers.Add(30);

You can also add different types:

numbers.Add("Hello");
numbers.Add(10.5);

That's possible because internally the collection deals with object.

5. Accessing Elements
Console.WriteLine(numbers[0]);

Unlike generic collections, the result is effectively an object.

If you know the expected type:

int value = (int)numbers[0];

This introduces casting.

6. Important Methods

For product-company interviews, these are the ones worth remembering:

Add()
numbers.Add(10);
AddRange()
numbers.AddRange(new int[] { 40, 50, 60 });
Insert()
numbers.Insert(1, 99);
Remove()
numbers.Remove(99);
RemoveAt()
numbers.RemoveAt(0);
Contains()
numbers.Contains(20);
IndexOf()
numbers.IndexOf(20);
Clear()
numbers.Clear();
Count
Console.WriteLine(numbers.Count);
Sort()
numbers.Sort();

⚠️ Sort() requires the contained objects to be mutually comparable. Mixing unrelated types can cause runtime problems.

7. Capacity

Like List<T>, ArrayList has:

numbers.Count
numbers.Capacity

Example:

Console.WriteLine(numbers.Count);
Console.WriteLine(numbers.Capacity);

Remember:

Count
 ↓
actual elements

Capacity
 ↓
allocated storage
8. Boxing and Unboxing

This is the most important interview concept related to ArrayList.

Suppose:

ArrayList numbers = new();

numbers.Add(10);

10 is an int, a value type.

Because ArrayList stores objects, the integer may need to be boxed:

int
 ↓
boxing
 ↓
object

When you retrieve it:

int number = (int)numbers[0];

the object is converted back:

object
 ↓
unboxing
 ↓
int

So:

ArrayList
   ↓
object
   ↓
boxing/unboxing

This is one reason generic collections are preferred.

9. ArrayList vs List<T>

This is the most important comparison.

| Feature                | `ArrayList`                     | `List<T>`                   |
| ---------------------- | ------------------------------- | --------------------------- |
| Generic                | ❌                               | ✅                           |
| Type-safe              | ❌                               | ✅                           |
| Stores                 | `object`                        | Specific `T`                |
| Boxing for value types | Can occur                       | Avoided for `T` value types |
| Casting                | Often required                  | Usually not                 |
| Modern choice          | ❌                               | ✅                           |
| Performance            | Generally worse for value types | Generally better            |
| DSA relevance          | Low                             | High                        |

Example:

ArrayList
ArrayList numbers = new();

numbers.Add(10);
numbers.Add("Hello");

int x = (int)numbers[0];
List
List<int> numbers = new();

numbers.Add(10);
// numbers.Add("Hello");  // compile-time error

int x = numbers[0];

That's the big difference.

10. Why Is List<T> Better?

Consider:

ArrayList numbers = new();

numbers.Add(10);
numbers.Add("Hello");
numbers.Add(20);

The compiler allows all of these.

Then:

int value = (int)numbers[1];

💥 Runtime exception.

Because "Hello" isn't an int.

With:

List<int> numbers = new();

this is caught immediately:

numbers.Add("Hello");

❌ Compile-time error.

That's much safer.

11. Advantages
✅ Flexible

Can hold different object types.

✅ Dynamic size

Grows automatically.

✅ Useful for legacy code

You may encounter it in older .NET applications.

✅ Easy to understand

Its API resembles List<T>.

12. Disadvantages
❌ No compile-time type safety

Different types can be mixed.

❌ Boxing/unboxing

Can introduce overhead for value types.

❌ Casting

Often required when retrieving values.

❌ Runtime errors

Incorrect casts can fail at runtime.

❌ Not recommended for new code

Use:

List<T>

instead.

13. When Should You Use It?

In modern application development:

Almost never.

You might encounter it when:

Maintaining legacy .NET applications
Working with old libraries/APIs
Reading old interview questions
Migrating old code to generic collections


14. When Should You NOT Use It?

For new C# code, prefer:

List<T>

For example:

Instead of:

ArrayList employees = new();

use:

List<Employee> employees = new();

Instead of:

ArrayList numbers = new();

use:

List<int> numbers = new();
15. Interview Questions
Q1. What is ArrayList?

ArrayList is a non-generic dynamically sized collection that stores elements as object.

Q2. Why is ArrayList not type-safe?

Because it accepts objects of different types.

Q3. What is the modern replacement?
ArrayList → List<T>
Q4. Why can ArrayList cause boxing?

Because value types such as int must be represented as object when stored.

Q5. What is the disadvantage of boxing/unboxing?

It can introduce performance overhead and requires conversions.

Q6. Can ArrayList contain different types?

Yes.

ArrayList list = new();

list.Add(10);
list.Add("Hello");
list.Add(10.5);
Q7. Would you use ArrayList in a new application?

Generally no. I'd use the appropriate generic collection, usually List<T>.



