# 05 — `JIT.md`

```markdown
# JIT - Just-In-Time Compilation

# 1. What is JIT?

JIT stands for:

Just-In-Time Compiler.

Its job is to convert IL/CIL into native machine code during runtime.

```text
IL
 ↓
JIT
 ↓
Native Machine Code
 ↓
CPU
2. Why JIT?

IL is CPU-independent.

The CPU needs architecture-specific machine instructions.

Therefore:

IL
 ↓
JIT
 ↓
x64 / ARM / other target
3. When Does JIT Run?

JIT compilation occurs as methods are needed for execution.

Conceptually:

Application starts
      ↓
Runtime loads assemblies
      ↓
Method is called
      ↓
JIT compiles required code
      ↓
Native code executes

The runtime does not need to compile every method that might never execute.

4. Example

C#:

public int Add(int x, int y)
{
    return x + y;
}

Conceptually:

C#
 ↓
IL
 ↓
JIT
 ↓
Native instructions
 ↓
CPU
5. JIT and Performance

JIT compilation itself has a cost.

There can be startup/warm-up considerations because native code must be generated for methods that execute.

However, JIT also has opportunities to optimize code based on runtime information.

6. JIT Optimization

Modern .NET JIT can perform various optimizations.

Examples include:

Method inlining
Constant propagation
Dead-code elimination
Bounds-check optimizations
Runtime-specific optimizations

Do not memorize every internal optimization for normal interviews.

Understand:

JIT can optimize IL into efficient native code for the actual runtime environment.

7. Method Inlining

Suppose:

int Square(int x)
{
    return x * x;
}

A JIT optimization may inline a small method instead of performing a normal method call.

Conceptually:

Before:

Call Square(5)

After optimization:

5 * 5

Actual optimization decisions are runtime-dependent.

Never say:

JIT always inlines small methods.

Say:

JIT may inline methods when its optimization heuristics determine that it is beneficial.

8. JIT vs Compiler
C# Compiler
C#
 ↓
IL + Metadata
JIT
IL
 ↓
Native Machine Code

This distinction is extremely important.

9. JIT vs AOT
JIT:

C#
 ↓
IL
 ↓
Runtime
 ↓
JIT
 ↓
Native Code


AOT:

C#
 ↓
IL
 ↓
Build/Publish
 ↓
Native Code
10. JIT and ASP.NET Core

For an ASP.NET Core application:

HTTP Request
     ↓
Controller/Endpoint
     ↓
Service
     ↓
Repository
     ↓
CLR/JIT executes managed methods

JIT is part of the execution/runtime pipeline.

11. Product Company Interview Questions
Q1. What is JIT?

JIT converts IL into native machine code at runtime.

Q2. Does the C# compiler produce machine code?

In the normal managed execution model, it produces IL/CIL and metadata.

Q3. Why does JIT exist?

To convert CPU-independent IL into native instructions suitable for the target runtime environment.

Q4. Does JIT compile the entire application at startup?

No.

Methods are compiled as needed in the normal JIT execution model.

Q5. What is method inlining?

An optimization where the JIT may replace a method call with the method's body when beneficial.

Points to Remember
JIT = Just-In-Time compiler.
JIT runs during execution.
JIT converts IL → native code.
C# compiler normally produces IL + metadata.
IL is not machine code.
JIT can perform runtime optimizations.
JIT does not necessarily compile every method at startup.
JIT can inline methods.
JIT and AOT are different compilation strategies.
Never confuse C# compiler with JIT.