# .NET Overview

## 1. What is .NET?

.NET is a developer platform/runtime ecosystem used to build applications such as:

- Web APIs
- ASP.NET Core applications
- Microservices
- Console applications
- Desktop applications
- Cloud applications
- Background services
- Libraries

Modern .NET is:

- Cross-platform
- Open source
- Managed-runtime based
- High performance
- Type safe
- Memory managed

C# is the most commonly used language on .NET.

---

# 2. Important .NET Components

Think of .NET as several layers.

```text
Application
    ↓
C# / F# / VB
    ↓
.NET Libraries / BCL
    ↓
CLR / Runtime
    ↓
Operating System
    ↓
Hardware

Example:

ASP.NET Core Web API
        ↓
      .NET
        ↓
      CLR
        ↓
   Operating System
        ↓
       CPU
3. What is CLR?

CLR = Common Language Runtime.

CLR is the runtime environment responsible for executing managed .NET code.

It provides services such as:

JIT compilation
Garbage collection
Memory management
Exception handling
Type safety
Thread management
Interoperability
Runtime type information
Assembly loading
4. What is Managed Code?

Managed code is code whose execution is controlled by the .NET runtime.

Example:

public class Order
{
    public int Id { get; set; }
}

The CLR manages important runtime responsibilities for this code.

Examples:

Memory allocation
Garbage collection
Exception handling
Type checking
JIT compilation
5. What is Unmanaged Code?

Unmanaged code executes outside the normal CLR-managed environment.

Examples:

Native C/C++ libraries
Operating-system APIs
Native memory
COM/native interop

C# can interact with unmanaged code using interoperability mechanisms.

6. .NET vs CLR

This is a very common interview question.

.NET

.NET is the overall development platform/ecosystem.

It includes:

Runtime
Libraries
SDK
Compiler/tooling
Application frameworks
CLR

CLR is the execution/runtime component.

.NET
 ├── Runtime / CLR
 ├── Libraries
 ├── SDK
 ├── Compiler
 └── Tools

Do NOT say:

.NET and CLR are the same thing.

They are related, but they are not the same.

7. BCL

BCL = Base Class Library.

It provides common functionality such as:

List<T>
Dictionary<TKey,TValue>
String
File
Stream
Task
DateTime
HttpClient
Exception

Examples:

List<int> numbers = new();

Dictionary<int, string> users = new();

File.ReadAllText("data.txt");

await Task.Delay(1000);

These capabilities are provided by .NET libraries.

8. SDK vs Runtime
.NET SDK

Used for development.

It contains tools required to:

Create projects
Build projects
Run projects
Publish applications
Compile code

Example:

dotnet new
dotnet build
dotnet run
dotnet publish
.NET Runtime

Used to run applications.

A production machine may only need the runtime when the application is deployed as framework-dependent.

9. Framework-Dependent vs Self-Contained
Framework-dependent

Application depends on an installed .NET runtime.

Application
     ↓
Installed .NET Runtime
Self-contained

Application includes the required runtime components.

Application
+
Runtime

The deployment model affects application size and deployment requirements.

10. .NET Framework vs Modern .NET
.NET Framework

Primarily Windows-focused and older.

Common technologies:

ASP.NET Framework
WCF
Windows Forms
WPF
System.Web
Modern .NET

Modern .NET is:

Cross-platform
Open source
High performance
Used for ASP.NET Core
Used for cloud-native applications
Used for microservices

For new product-company development, focus primarily on modern .NET.

11. Important Mental Model

Remember:

.NET
 │
 ├── Runtime / CLR
 │      ├── JIT
 │      ├── GC
 │      ├── Exception handling
 │      └── Runtime services
 │
 ├── BCL / Libraries
 │
 ├── SDK
 │
 └── Tools
12. Product Company Interview Questions
Q1. What is .NET?

.NET is a cross-platform development platform that provides a runtime, libraries, SDK, and tooling for building applications.

Q2. What is CLR?

CLR is the runtime that executes managed .NET code and provides services such as JIT compilation, garbage collection, exception handling, and memory management.

Q3. Is CLR the same as .NET?

No.

.NET is the broader platform.

CLR is the runtime/execution environment.

Q4. What is BCL?

BCL is the set of core .NET libraries providing reusable functionality such as collections, files, streams, tasks, networking, and basic types.

Q5. What is managed code?

Code whose execution is managed by the .NET runtime.

Points to Remember
.NET = broader platform.
CLR = runtime.
C# code does not directly become CPU instructions through the C# compiler.
C# normally compiles to IL/CIL + metadata.
CLR/JIT participates in converting IL to machine code.
CLR manages runtime services.
GC manages managed memory.
BCL provides common libraries.
SDK is primarily for development.
Runtime is required for executing framework-dependent applications.