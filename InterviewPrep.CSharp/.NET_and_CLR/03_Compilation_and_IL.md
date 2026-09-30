# 03 — `Compilation_and_IL.md`

```markdown
# Compilation and IL

# 1. What Happens When C# Code Is Built?

Suppose we write:

```csharp
public class Calculator
{
    public int Add(int a, int b)
    {
        return a + b;
    }
}

The compiler does NOT normally convert this directly into CPU-specific machine code.

The simplified flow is:

C# Source
   ↓
C# Compiler
   ↓
IL / CIL + Metadata
   ↓
Assembly
2. What is IL?

IL means:

Intermediate Language

It is also called:

CIL = Common Intermediate Language
MSIL = Microsoft Intermediate Language

For modern .NET interview discussions, IL/CIL is the important terminology.

IL is CPU-independent.

3. Why IL?

The same compiled IL can be used on different supported architectures.

Conceptually:

C# Code
   ↓
IL
   ↓
 ┌───────────────┐
 ↓               ↓
x64             ARM
 ↓               ↓
Native Code     Native Code

The runtime/JIT handles the target architecture.

4. Metadata

The compiler also produces metadata.

Metadata describes information such as:

Class
Methods
Properties
Fields
Interfaces
Method signatures
References

Example:

public class Customer
{
    public int Id { get; set; }

    public string Name { get; set; }
}

Runtime metadata can describe:

Customer
 ├── Id : int
 └── Name : string
5. IL + Metadata

Think:

Assembly
│
├── IL / CIL
│
└── Metadata

IL represents executable intermediate instructions.

Metadata describes the types and members.

6. Build Process

Typical command:

dotnet build

Conceptually:

.cs files
   ↓
Compiler
   ↓
IL + Metadata
   ↓
Assembly

The result is placed under the build output directory.

For example:

bin/Debug/net8.0/

or another target framework depending on the project.

7. What Happens at Runtime?

When the application runs:

Assembly
   ↓
CLR loads it
   ↓
Method is required
   ↓
JIT compiles IL
   ↓
Native code
   ↓
CPU executes
8. First Call vs Later Calls

Conceptually:

First execution
    ↓
IL
    ↓
JIT
    ↓
Native code
    ↓
Execute

Later execution can reuse the generated native code within the relevant process/runtime context rather than repeating the same initial compilation for every invocation.

9. Why JIT Is Useful

JIT knows information about the actual execution environment.

It can generate code appropriate for the target architecture and optimize runtime execution.

10. AOT

AOT = Ahead-of-Time compilation.

Instead of relying entirely on runtime JIT compilation, code can be compiled ahead of execution.

Conceptually:

JIT:

IL
 ↓
Runtime
 ↓
Native code


AOT:

IL
 ↓
Build/Publish
 ↓
Native code
 ↓
Runtime

Modern .NET also supports Native AOT scenarios.

Do not confuse:

JIT

with:

AOT
11. JIT vs AOT

| JIT                             | AOT                                                  |
| ------------------------------- | ---------------------------------------------------- |
| Compilation happens at runtime  | Compilation happens ahead of execution               |
| Can use runtime information     | Can perform build-time/native compilation            |
| Runtime compilation cost exists | Less runtime JIT work                                |
| Common managed execution model  | Useful for specific deployment/performance scenarios |

12. Product Company Question
Why doesn't C# compile directly to machine code?

Because the normal .NET managed execution model uses an intermediate representation, IL/CIL, which is then compiled for the target machine by the runtime/JIT.

This enables portability across supported platforms and architectures.

13. Interview Questions
Q1. What does the C# compiler produce?

Primarily:

IL/CIL + Metadata

inside a .NET assembly.

Q2. Is IL machine code?

No.

IL is an intermediate instruction representation.

Q3. Who converts IL to machine code?

The runtime's JIT compiler in the normal JIT-based execution path.

Q4. Why is IL useful?

It provides a CPU-independent intermediate representation and supports runtime compilation for the target environment.

Points to Remember
C# → IL/CIL + metadata.
IL is not native machine code.
IL is CPU-independent.
Assembly contains IL and metadata.
CLR/runtime loads the assembly.
JIT converts IL to native code.
JIT is runtime compilation.
AOT is ahead-of-time compilation.
Metadata describes types and members.
Learn this flow perfectly:

C#
→ Compiler
→ IL + Metadata
→ Assembly
→ CLR
→ JIT
→ Native Code
→ CPU

