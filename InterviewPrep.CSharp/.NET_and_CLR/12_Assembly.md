Definition

An assembly is the compiled unit of deployment, versioning, and identity in .NET.

Common examples:

MyApplication.dll
MyLibrary.dll
MyApplication.exe

An assembly contains information such as:

Assembly
├── IL / CIL
├── Metadata
├── Manifest
└── Resources
11. Simple Assembly Example

Suppose you create:

public class Calculator
{
    public int Add(int a, int b)
    {
        return a + b;
    }
}

You build the project.

The result could be:

CalculatorLibrary.dll

That DLL is an assembly.

It contains the compiled representation of your code and the metadata required by the runtime.

12. Assembly Contains IL

Your C# code:

public int Add(int a, int b)
{
    return a + b;
}

is compiled into IL.

Conceptually:

C# source
    ↓
C# compiler
    ↓
IL + Metadata
    ↓
Assembly (.dll/.exe)

At runtime:

Assembly
   ↓
CLR loads it
   ↓
JIT/AOT execution mechanism
   ↓
Native machine code
   ↓
CPU
13. Assembly Manifest

The assembly contains a manifest describing important assembly identity/dependency information.

Think:

Manifest
├── Assembly name
├── Version
├── Culture
├── Referenced assemblies
└── Other assembly identity information

For interview purposes:

Manifest describes the assembly itself and its dependencies.

14. Assembly Metadata

Metadata describes the types and members contained in the assembly.

For example:

public class Employee
{
    public int Id { get; set; }
}

Metadata can describe:

Type:
    Employee

Property:
    Id

Type of Id:
    System.Int32

Accessibility:
    public

This metadata enables features such as:

typeof(Employee)

and reflection.

15. Reflection Example
Type type = typeof(Employee);

Console.WriteLine(type.Name);

foreach (var property in type.GetProperties())
{
    Console.WriteLine(property.Name);
}

Output:

Employee
Id

The runtime obtains this information from metadata.

16. DLL vs EXE

Both can be assemblies.

DLL
MyLibrary.dll

Usually used as a reusable library.

Example:

Web API
   ↓
Business.dll
   ↓
DataAccess.dll
EXE
MyApplication.exe

An executable application.

Important:

DLL and EXE are file formats/extensions; an assembly is the .NET compiled unit with identity and metadata.

Don't say:

"Every DLL is an assembly."

That's too broad because a DLL can exist that isn't a .NET assembly.

17. Assembly vs Namespace

This is a common interview question.

They are completely different concepts.

Namespace

Used for logical organization of types.

namespace MyCompany.Payments
{
    public class PaymentService
    {
    }
}
Assembly

Physical compiled/deployment unit.

MyCompany.Payments.dll

Relationship:

Namespace
    ↓
organizes types logically

Assembly
    ↓
packages compiled code for deployment/versioning

Multiple namespaces can exist in one assembly.

A namespace can also span multiple assemblies.

18. One Assembly Can Contain Multiple Namespaces

Example:

MyCompany.Core.dll

MyCompany.Core
MyCompany.Core.Models
MyCompany.Core.Services
MyCompany.Core.Validation

All can be inside:

MyCompany.Core.dll
19. One Namespace Can Span Multiple Assemblies

For example:

Assembly A
└── MyCompany.Services

Assembly B
└── MyCompany.Services

Same namespace name doesn't mean same assembly.

20. Assembly References

Suppose:

OrderService.dll
       │
       ├── references
       ▼
PaymentService.dll

OrderService can use public types exposed by PaymentService.

Example:

using PaymentService;

var service = new PaymentProcessor();

At build/runtime, the required assembly reference is resolved.

21. Assembly and NuGet Package

Another common interview trap.

They are not the same thing.

NuGet package

A distribution package:

SomeLibrary.nupkg

It may contain:

.dll
.xml
.pdb
build files
targets
dependencies
Assembly

The compiled .NET unit:

SomeLibrary.dll

Therefore:

NuGet Package
       │
       ├── Assembly DLL
       ├── symbols
       ├── build metadata
       └── other files
22. Putting CTS + CLS + Assembly Together

This is the complete mental model you should remember:

              .NET
                │
        ┌───────┴────────┐
        │                │
       CTS              CLS
        │                │
   Type system      Interoperability
        │                │
        └───────┬────────┘
                │
        C# / VB / F# etc.
                │
             Compiler
                │
         IL + Metadata
                │
             Assembly
                │
          CLR / Runtime
                │
         JIT / AOT execution
                │
          Native execution
23. Real Product-Company Example

Imagine a company has:

PaymentService.dll
OrderService.dll
CustomerService.dll

The PaymentService exposes:

public interface IPaymentService
{
    bool ProcessPayment(decimal amount);
}
CTS

The runtime understands:

IPaymentService → interface
bool             → System.Boolean
decimal          → System.Decimal
CLS

The public API uses common .NET types so that other CLS-compliant .NET languages can consume it.

Assembly

The compiled service/library is packaged as:

PaymentService.dll
Runtime

The CLR/runtime loads the assembly, reads its metadata, resolves dependencies, and executes the compiled code.

24. Very Important Interview Comparisons

| Concept   | Meaning                                         |
| --------- | ----------------------------------------------- |
| CTS       | Defines .NET's type system                      |
| CLS       | Common rules for language interoperability      |
| Assembly  | Compiled unit of deployment/versioning/identity |
| Namespace | Logical organization of types                   |
| Metadata  | Information describing types/members            |
| IL        | Intermediate code generated by the compiler     |
| Manifest  | Assembly identity/dependency information        |
| DLL       | Common library file format for assemblies       |
| NuGet     | Package/distribution mechanism                  |


25. Common Interview Questions
Q1. What is CTS?

Answer:

CTS, or Common Type System, defines how types are declared, represented, and behave in the .NET runtime. It provides a common type system that allows different .NET languages to work together.

Q2. What is CLS?

Answer:

CLS, or Common Language Specification, defines a common subset of rules that .NET languages can follow to ensure better interoperability between languages.

Q3. Difference between CTS and CLS?

Answer:

CTS is the broader .NET type system, while CLS is a subset of rules designed to ensure cross-language interoperability.

Remember:

CTS = Type system
CLS = Interoperability rules
Q4. What is an assembly?

Answer:

An assembly is a compiled .NET unit that contains IL, metadata, a manifest, and possibly resources. It is a fundamental unit for deployment, versioning, and identity.

Q5. Is a DLL an assembly?

Better answer:

A .NET DLL can be an assembly, but DLL is a file format/extension. An assembly is the .NET logical compiled unit with metadata and identity.

Q6. What is the difference between namespace and assembly?
Namespace → logical organization
Assembly  → compiled/deployment unit
Q7. What is metadata?

Metadata is information about the types and members contained in compiled .NET code.

Example:

Employee
 ├── Class
 ├── Id
 ├── String Name
 └── Methods
Q8. Why is metadata important?

It enables runtime services such as:

Reflection
Type inspection
Assembly loading
Dependency information
Runtime type operations
Q9. Can multiple namespaces exist in one assembly?

Yes.

MyLibrary.dll
├── Company.Models
├── Company.Services
└── Company.Utilities
Q10. Can one namespace exist across multiple assemblies?

Yes.

A namespace is not tied to a particular assembly.

Points to Remember ⭐
CTS
CTS
↓
Common Type System
↓
Defines .NET types
↓
Value types + Reference types
CLS
CLS
↓
Common Language Specification
↓
Cross-language interoperability
↓
Subset of CTS
Assembly
Assembly
↓
Compiled .NET unit
↓
IL + Metadata + Manifest + Resources
↓
Deployment / Versioning / Identity
The most important distinctions
CTS       → What types does .NET understand?

CLS       → What common rules allow languages to interoperate?

Assembly  → What compiled unit is deployed?

Namespace → How are types logically organized?

Metadata  → What information describes those types?

IL        → What intermediate code did the compiler generate?
⭐ One-line interview memory trick

CTS defines types, CLS defines interoperability rules, and an assembly packages compiled .NET code plus metadata for deployment and runtime use.