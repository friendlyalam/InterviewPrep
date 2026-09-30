# 09 — `Quick_Revision.md`

```markdown
# .NET and CLR - Quick Revision

## 1. One-Line Definitions

### .NET

Cross-platform development platform/ecosystem.

### CLR

Runtime environment for executing managed .NET code.

### C# Compiler

Converts C# source into IL/CIL + metadata.

### IL

CPU-independent intermediate code.

### Assembly

Compiled .NET unit containing IL, metadata, manifest and potentially resources.

### JIT

Converts IL into native machine code during runtime.

### GC

Automatically reclaims memory from unreachable managed objects.

### BCL

Core .NET libraries.

### Metadata

Information describing types, members and references.

### Reflection

Runtime inspection of types and members.

### IDisposable

Pattern/interface for deterministic resource cleanup.

---

# 2. Most Important Diagram

```text
C# Code
   ↓
C# Compiler
   ↓
IL + Metadata
   ↓
Assembly
   ↓
CLR
   ↓
JIT
   ↓
Native Machine Code
   ↓
CPU
3. .NET Structure
.NET
│
├── Runtime / CLR
│
├── BCL / Libraries
│
├── SDK
│
└── Tools
4. CLR Responsibilities

Remember:

JIT
GC
Memory Management
Exception Handling
Type/Runtime Services
Assembly Loading
Interop
5. GC
Object Created
     ↓
Gen 0
     ↓
Survives
     ↓
Gen 1
     ↓
Survives
     ↓
Gen 2

Important:

GC ≠ Dispose
6. Memory Leak

Even with GC:

Reachable object
      ↓
Still referenced
      ↓
GC cannot collect

Common causes:

Static collections
Caches
Events
Long-lived references
Unbounded dictionaries
7. Most Important Comparisons
| Concept      | Meaning                        |
| ------------ | ------------------------------ |
| .NET         | Overall platform               |
| CLR          | Runtime                        |
| Compiler     | C# → IL                        |
| JIT          | IL → Native code               |
| Assembly     | Compiled unit                  |
| Namespace    | Logical grouping               |
| NuGet        | Package/distribution           |
| GC           | Managed memory reclamation     |
| Dispose      | Deterministic resource cleanup |
| BCL          | Core libraries                 |
| ASP.NET Core | Web framework                  |


8. Top Interview Traps
Trap 1

"CLR = .NET"

Wrong.

.NET is broader.

Trap 2

"C# compiler converts C# directly to machine code."

Normally wrong.

C#
 ↓
IL + Metadata
 ↓
JIT
 ↓
Native Code
Trap 3

"IL is machine code."

Wrong.

IL is intermediate code.

Trap 4

"Dispose removes object from memory."

Wrong.

Dispose releases resources.

GC later handles unreachable managed objects.

Trap 5

"GC means there cannot be memory leaks."

Wrong.

Reachable objects can remain unnecessarily alive.

Trap 6

"async means another thread."

Wrong.

Async does not automatically mean another thread.

Trap 7

"Namespace is assembly."

Wrong.

Namespace = logical organization.

Assembly = compiled unit.

9. Final Mental Model
                  .NET
                    │
        ┌───────────┴───────────┐
        │                       │
     Libraries               Runtime
        │                       │
       BCL                     CLR
                                │
              ┌─────────────────┼─────────────────┐
              │                 │                 │
             JIT                GC          Runtime Services
              │                 │
              ↓                 ↓
         Native Code       Managed Memory
10. What You MUST Know for Product Companies

Priority 1:

.NET vs CLR
C# Compiler
IL/CIL
Metadata
Assembly
JIT
GC
Managed vs unmanaged
Dispose vs GC

Priority 2:

Generations
LOH
Reflection
BCL
SDK vs Runtime
JIT vs AOT
Memory leaks
Assembly vs Namespace

Priority 3:

Server vs Workstation GC
Background GC
Interop
Runtime profiling
Advanced JIT optimization

Do not spend excessive time memorizing CLR internals before understanding the execution pipeline.

One-Minute Revision

.NET = platform.

CLR = runtime.

C# compiler = C# → IL + metadata.

Assembly = compiled .NET unit.

IL = intermediate code.

JIT = IL → native code.

GC = manages managed memory.

Dispose = deterministic resource cleanup.

Namespace = logical grouping.

BCL = core libraries.

The complete flow:

C#
→ Compiler
→ IL + Metadata
→ Assembly
→ CLR
→ JIT
→ Native Code
→ CPU




### Key product-company takeaway

The **single most important concept** in this entire topic is the execution pipeline:

**C# → Compiler → IL + Metadata → Assembly → CLR → JIT → Native Code → CPU**

Once that is clear, **CLR, JIT, assemblies, metadata, GC, managed code, and runtime 
services all fit into one mental model**. Microsoft’s documentation describes this
managed-execution process and CLR responsibilities in essentially this layered way. :contentReference[oaicite:3]{index=3}