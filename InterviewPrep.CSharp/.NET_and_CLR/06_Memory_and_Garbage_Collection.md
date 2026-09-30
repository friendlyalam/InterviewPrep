# 06 — `Memory_and_Garbage_Collection.md`

```markdown
# Memory and Garbage Collection

# 1. Why Learn This?

For product-company interviews, you should understand:

- Managed heap
- Object allocation
- References
- GC
- Generations
- LOH
- Roots
- IDisposable
- Managed vs unmanaged resources
- Memory leaks
- GC performance

---

# 2. Managed Heap

When you create a reference-type object:

```csharp
Customer customer = new Customer();

The object is normally allocated on the managed heap.

Conceptually:

Stack
  │
  │ customer reference
  ↓
Managed Heap
  │
  └── Customer object

Do not oversimplify every value as "stack" and every reference type as "heap".

Actual allocation and storage depend on context and runtime implementation.

3. What is GC?

GC = Garbage Collector.

It automatically manages the lifetime of managed objects.

Its main job is to identify objects that are no longer reachable and reclaim their managed memory.

4. Why GC?

Without automatic memory management, developers would have to manually manage object memory.

GC reduces many classes of memory-management errors.

5. Reachability

The important idea is:

An object can be collected when it is no longer reachable from GC roots.

Conceptually:

GC Root
  ↓
Object A
  ↓
Object B

Objects reachable from roots remain alive.

If an object is no longer reachable:

GC Root

Object A

Object B  ← unreachable

Object B can eventually be reclaimed.

6. GC Generations

.NET GC uses generations.

Generation 0
Generation 1
Generation 2

General idea:

Generation 0

New/short-lived objects.

Generation 1

Intermediate objects.

Generation 2

Long-lived objects.

7. Why Generations?

Most applications create many temporary objects.

Example:

for (...)
{
    var request = new Request();
}

Many temporary objects become unreachable quickly.

Generational GC allows the runtime to focus frequently on younger objects.

8. Typical Flow
Object created
     ↓
Generation 0
     ↓
Survives GC
     ↓
Generation 1
     ↓
Survives more collections
     ↓
Generation 2

The exact promotion behavior is runtime-managed.

9. Large Object Heap

Large objects are handled specially through the Large Object Heap (LOH).

Examples may include large:

Arrays
Strings/data buffers
Object graphs

The exact threshold and behavior are runtime-dependent; don't memorize an oversimplified fixed number unless the question specifically asks about a particular runtime version.

10. Why Large Allocations Matter

Large temporary allocations can cause:

Higher memory usage
More GC pressure
Increased allocation cost
Potential fragmentation

For large data:

Bad approach:

byte[] data = File.ReadAllBytes(path);

Better for large files:

Stream data progressively
11. GC and Performance

Too many allocations can create GC pressure.

Example:

for (int i = 0; i < 10_000_000; i++)
{
    var item = new SomeObject();
}

If objects become short-lived, the application may generate substantial GC activity.

Performance-sensitive code often considers:

Allocation rate
Object lifetime
Collection frequency
Large allocations
Memory retention
12. Memory Leak in Managed Code

Important interview question:

Can .NET applications have memory leaks even with GC?

Yes.

GC only collects objects that are no longer reachable.

Example:

static List<object> cache = new();

If objects are continuously added and never removed:

Static reference
      ↓
List
      ↓
Objects

Objects remain reachable.

Therefore GC cannot collect them.

13. Common Managed Memory Leak Causes

Examples:

Static collections
Incorrect caching
Event subscriptions
Long-lived objects holding references
Unbounded dictionaries
Incorrect object lifetime
Timers/background references
14. IDisposable

GC manages managed memory.

It does NOT mean every resource should wait for GC.

Examples of unmanaged/external resources:

File handles
Database connections
Sockets
Native handles
OS resources

Use deterministic cleanup.

Example:

using FileStream stream = File.OpenRead(path);

When leaving scope, Dispose() is called.

15. GC vs Dispose

Very important.

GC

Manages managed object memory.

Dispose

Releases resources deterministically.

GC
 ↓
Managed memory

Dispose
 ↓
External/unmanaged resource cleanup

Dispose does not mean:

Delete object from memory immediately.

16. Finalizer

A finalizer provides runtime cleanup support for certain unmanaged-resource scenarios.

Example syntax:

~MyClass()
{
}

Finalizers are:

Non-deterministic
More expensive
Not a replacement for IDisposable

Prefer safe resource-management patterns such as IDisposable and SafeHandle where appropriate.

17. Server GC vs Workstation GC

.NET supports different GC modes.

Two major modes:

Workstation GC
Server GC

Server GC is designed for server workloads and has different threading/heap behavior.

Do not blindly assume:

Server GC is always faster.

Configuration should match the workload and be measured.

18. Background GC

Modern .NET supports background GC.

The purpose is to reduce long pauses associated with collection work by allowing certain GC work to happen concurrently with application execution.

19. GC Pressure

GC pressure means the application creates enough allocation activity that GC work becomes significant.

Symptoms may include:

High CPU
Increased latency
Increased allocation rate
Frequent collections
Memory spikes
20. Product Company Example

Suppose an API processes 10,000 requests/sec.

If every request creates many unnecessary temporary objects:

Request
 ↓
Many allocations
 ↓
High allocation rate
 ↓
More GC activity
 ↓
CPU/latency impact

Optimization might involve:

Reducing allocations
Streaming
Reusing buffers where appropriate
Avoiding unnecessary conversions
Batching
Better object lifetimes

Always measure before optimizing.

21. Interview Questions
Q1. What is GC?

GC automatically reclaims memory occupied by managed objects that are no longer reachable.

Q2. What are generations?
Gen 0
Gen 1
Gen 2

They organize managed objects broadly by lifetime.

Q3. Can .NET have memory leaks?

Yes.

GC cannot collect reachable objects that your application accidentally keeps referenced.

Q4. GC vs IDisposable?

GC manages managed memory.

IDisposable provides deterministic resource cleanup.

Q5. Does Dispose destroy an object?

No.

Dispose releases resources owned by the object. The object itself becomes collectible when no longer reachable.

Q6. What is LOH?

Large Object Heap is a specialized area of the managed heap for large allocations.

Points to Remember
GC manages managed memory.
GC is reachability-based.
GC roots keep objects reachable.
Generations: Gen 0, Gen 1, Gen 2.
Short-lived objects are common.
Long-lived objects may reach Gen 2.
Large allocations need special consideration.
GC does NOT automatically fix all memory leaks.
Static references can keep objects alive.
Dispose != GC.
Dispose is deterministic.
Finalizer is non-deterministic.
Avoid unnecessary allocations in hot paths.
Measure GC before optimizing.