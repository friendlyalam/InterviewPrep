CTS — Common Type System
Definition

CTS (Common Type System) is the set of rules defined by .NET that specifies:

How types are declared, used, represented, and behave in the .NET runtime.

Its main purpose is to allow different .NET languages to understand and work with the same types.

Why do we need CTS?

Imagine:

C#
    int
     │
     ▼
   .NET CTS
     ▲
     │
VB.NET
   Integer

C# calls it int.

VB.NET may use Integer.

But both represent the same underlying .NET type:

System.Int32

Therefore, code written in different .NET languages can communicate.

2. CTS Type Categories

The two major categories are:

CTS
├── Value Types
└── Reference Types
Value Types

Examples:

int
double
bool
char
struct
enum

Example:

int age = 35;
double salary = 50000.50;
bool active = true;

Their values are stored directly as the variable's value, although the actual storage location depends on context.

Reference Types

Examples:

class
interface
delegate
array
string
object

Example:

Customer customer = new Customer();

customer contains a reference to a Customer object.

3. CTS and System.Object

A very important interview concept:

Most .NET types ultimately participate in the CTS type hierarchy rooted at:

System.Object

For example:

int
 ↓
System.Int32
 ↓
System.ValueType
 ↓
System.Object

And:

Customer
 ↓
System.Object

This is why:

object value = 10;

is valid.

The int value can be represented as object through boxing.

4. CTS Example

Suppose you have:

public class Employee
{
    public int Id { get; set; }

    public string Name { get; set; }
}

CTS defines concepts such as:

Employee → class/reference type
Id       → System.Int32
Name     → System.String

The runtime understands these types consistently regardless of which .NET language created them.