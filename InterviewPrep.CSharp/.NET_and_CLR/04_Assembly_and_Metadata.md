# 04 — `Assembly_and_Metadata.md`

```markdown
# Assembly and Metadata

# 1. What is an Assembly?

An assembly is a compiled .NET unit.

Typical assembly files include:

```text
.dll
.exe

Examples:

MyApplication.dll
MyLibrary.dll

An assembly can contain:

IL
Metadata
Manifest information
Resources
2. Assembly Mental Model

Think:

Assembly
│
├── IL
├── Metadata
├── Manifest
└── Resources
3. DLL vs EXE
DLL

Usually a reusable library.

Example:

OrderService.dll

It can be referenced by another application.

EXE

An executable application.

Modern .NET applications may also use .dll application assemblies launched through the .NET host.

Do not oversimplify the distinction as:

DLL = library, EXE = always completely different runtime.

The exact deployment model matters.

4. What is Metadata?

Metadata describes the contents of an assembly.

It can describe:

Types
Methods
Properties
Fields
Interfaces
Inheritance
Method signatures
References

Example:

public interface IPaymentService
{
    Task PayAsync(decimal amount);
}

Metadata can describe:

IPaymentService
    ↓
PayAsync
    ↓
decimal amount
5. Why Metadata Matters

The runtime needs information about types and members.

Metadata enables runtime mechanisms such as:

Type loading
Reflection
Method resolution
Dependency/reference resolution
Object layout
Runtime invocation
6. Reflection

Reflection allows runtime inspection of types.

Example:

Type type = typeof(Customer);

Console.WriteLine(type.Name);

foreach (var property in type.GetProperties())
{
    Console.WriteLine(property.Name);
}

Output might be:

Customer
Id
Name

The runtime can inspect metadata to provide this information.

7. Assembly Loading

At runtime, .NET needs to load required assemblies.

Conceptually:

Application
   ↓
Required assembly
   ↓
Runtime loads assembly
   ↓
Types become available
8. Assembly References

Suppose:

OrderService
      ↓
PaymentLibrary

The application assembly contains reference information needed to resolve types from the dependency.

9. Strong Naming

Strong naming is mainly associated with assembly identity/signing scenarios, particularly .NET Framework and certain library/deployment requirements.

Do not confuse strong naming with application security.

A strong name is not a complete security mechanism.

10. Versioning

Assembly identity/versioning matters when applications depend on libraries.

Example:

PaymentLibrary v1
PaymentLibrary v2

Changing public APIs can affect consumers.

Product-company interviews may connect this to:

Backward compatibility
Package versioning
API compatibility
Deployment
11. NuGet vs Assembly

Important distinction.

NuGet package

Distribution/package mechanism.

Example:

Newtonsoft.Json
Assembly

Compiled .NET unit used by the runtime.

A NuGet package may contain one or more assemblies.

12. Assembly vs Namespace

Very common interview question.

Namespace

Logical organization of types.

namespace Company.Ordering;
Assembly

Compiled deployment/runtime unit.

Company.Ordering.dll

They are NOT the same thing.

One assembly can contain many namespaces.

One namespace can have types distributed across multiple assemblies.

13. Product Company Example

Suppose:

Company.OrderService
Company.PaymentService
Company.InventoryService

Each application/library can have its own assemblies.

At runtime:

Application
    ↓
Assemblies
    ↓
Metadata + IL
    ↓
CLR
    ↓
JIT
14. Interview Questions
Q1. What is an assembly?

A compiled .NET unit containing IL, metadata, manifest information and potentially resources.

Q2. What does an assembly contain?

Remember:

IL
Metadata
Manifest
Resources
Q3. Assembly vs namespace?

Namespace = logical grouping.

Assembly = compiled/runtime/deployment unit.

Q4. What is metadata?

Information describing types, members, signatures, references and other runtime information.

Q5. What is reflection?

Runtime inspection of types and members using metadata.

Points to Remember
Assembly is a compiled .NET unit.
.dll and .exe can represent assemblies/application artifacts depending on deployment.
Assembly contains IL + metadata.
Namespace is NOT an assembly.
One assembly can contain many namespaces.
One namespace can span multiple assemblies.
Reflection uses runtime type information/metadata.
NuGet package != assembly.
Strong name != complete security mechanism.