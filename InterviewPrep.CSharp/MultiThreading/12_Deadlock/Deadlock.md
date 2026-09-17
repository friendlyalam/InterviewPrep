# Deadlock

## 1. What is a Deadlock?

A **deadlock** occurs when two or more threads/tasks are waiting for resources held by each other, so none of them can continue.

### Simple example

```text
Thread A
  ↓
Locks Resource 1
  ↓
Waits for Resource 2

Thread B
  ↓
Locks Resource 2
  ↓
Waits for Resource 1

Both wait forever.

Thread A → Resource 1 → waiting for Resource 2
Thread B → Resource 2 → waiting for Resource 1
2. Real-World Example

Imagine two people:

Person A has Pen
Person B has Notebook

Person A says:

"I will give the pen after I get the notebook."

Person B says:

"I will give the notebook after I get the pen."

Neither can proceed.

That is the basic idea of a deadlock.

3. Simple C# Deadlock Example
object lockA = new();
object lockB = new();

Task task1 = Task.Run(() =>
{
    lock (lockA)
    {
        Thread.Sleep(100);

        lock (lockB)
        {
            Console.WriteLine("Task 1 completed.");
        }
    }
});

Task task2 = Task.Run(() =>
{
    lock (lockB)
    {
        Thread.Sleep(100);

        lock (lockA)
        {
            Console.WriteLine("Task 2 completed.");
        }
    }
});

Possible situation:

Task 1 → holds lockA → waits for lockB

Task 2 → holds lockB → waits for lockA

Neither task can continue.

4. Four Conditions Required for Deadlock

A classic deadlock requires four conditions.

1. Mutual Exclusion

A resource can be held by only one thread at a time.

Example:

lock (lockObject)
{
    // Critical section
}
2. Hold and Wait

A thread holds one resource while waiting for another.

Thread A
  ↓
Holds Lock A
  ↓
Waits for Lock B
3. No Preemption

The resource cannot simply be forcibly taken away from the thread holding it.

The owner must release it.

4. Circular Wait

There is a circular dependency.

Thread A → Lock B
     ↑       ↓
     |       |
Lock A ← Thread B
Interview Point

Deadlock requires mutual exclusion, hold-and-wait, no preemption, and circular wait.

Breaking any one of these conditions can prevent deadlock.

5. Lock Ordering Deadlock

One of the most common causes in real applications is inconsistent lock ordering.

Bad:

// Thread 1
lock (lockA)
{
    lock (lockB)
    {
    }
}
// Thread 2
lock (lockB)
{
    lock (lockA)
    {
    }
}

Different order creates the possibility of circular waiting.

6. Preventing Lock Ordering Deadlock

Always acquire multiple locks in a consistent order.

For example:

lock (lockA)
{
    lock (lockB)
    {
        // Work
    }
}

Every thread that needs both locks should use:

Lock A → Lock B

Never:

Thread 1: A → B
Thread 2: B → A
Product-company rule

Define and consistently follow a global lock acquisition order.

7. Keep Critical Sections Small

Bad:

lock (lockObject)
{
    UpdateDatabase();

    CallExternalApi();

    Thread.Sleep(5000);
}

The lock is held while performing slow operations.

Better:

var data = PrepareData();

CallExternalApi(data);

lock (lockObject)
{
    UpdateSharedState(data);
}

Keep the protected section as small as possible.

8. Do Not Perform External Calls While Holding Locks

Avoid:

lock (lockObject)
{
    httpClient.GetAsync(...);
}

or:

lock (lockObject)
{
    CallPaymentService();
}

External operations can:

take a long time
timeout
retry
call back into your application
create additional dependencies

This increases deadlock risk and reduces scalability.

9. Lock and await

A major C# interview point:

You cannot use await directly inside a lock statement.

This is invalid:

lock (lockObject)
{
    await SomeOperationAsync();
}

Instead, for async coordination, consider:

SemaphoreSlim

Example:

await semaphore.WaitAsync();

try
{
    await SomeOperationAsync();
}
finally
{
    semaphore.Release();
}
10. SemaphoreSlim Can Also Deadlock

Using SemaphoreSlim does not automatically eliminate deadlocks.

Example:

Task A
  ↓
Semaphore 1
  ↓
Waits for Semaphore 2

Task B
  ↓
Semaphore 2
  ↓
Waits for Semaphore 1

This is still a circular wait.

The same lock-ordering principle applies.

11. Avoid Nested Locks When Possible

Nested synchronization increases complexity.

Example:

lock (lockA)
{
    lock (lockB)
    {
        lock (lockC)
        {
            // Complex operation
        }
    }
}

The more resources a thread holds simultaneously, the greater the opportunity for circular dependencies.

Prefer simpler synchronization designs.

12. Deadlock with Multiple Resources

Consider:

Thread A
  ├── Resource A
  └── waiting for Resource B

Thread B
  ├── Resource B
  └── waiting for Resource C

Thread C
  ├── Resource C
  └── waiting for Resource A

This creates:

A → B → C → A

Therefore, all three can become blocked.

13. Deadlock vs Race Condition

These are different problems.

| Deadlock                               | Race Condition                        |
| -------------------------------------- | ------------------------------------- |
| Threads wait forever                   | Result depends on timing              |
| Program can stop making progress       | Program may continue incorrectly      |
| Usually involves resource dependencies | Usually involves shared mutable state |
| Circular waiting is common             | Unsynchronized access is common       |
| No progress                            | Incorrect/unpredictable result        |


Example:

Deadlock:
A waits for B
B waits for A

Race condition:
A and B modify the same variable
without proper synchronization
14. Deadlock vs Starvation
Deadlock

Threads are permanently blocked because they are waiting for each other.

A → waits for B
B → waits for A
Starvation

A thread keeps waiting because other threads continuously get access to the resource.

Thread A → keeps waiting

Thread B → gets resource
Thread C → gets resource
Thread D → gets resource
...

The key difference:

Deadlock = circular dependency.

Starvation = unfair or continuously denied access.

15. Deadlock vs Livelock
Deadlock

Threads do nothing because they are blocked.

A → waiting
B → waiting
Livelock

Threads are active but keep changing state without making useful progress.

A → moves
B → moves
A → moves
B → moves
...

Example:

Two people repeatedly move left and right trying to let each other pass, but neither actually passes.

16. Common Deadlock Causes in .NET

Common causes include:

inconsistent lock ordering
nested locks
waiting for multiple resources
blocking on asynchronous code
.Result
.Wait()
poorly designed synchronization
holding locks during slow operations
synchronous waits inside async workflows
circular dependencies between components
17. Async Deadlock Concept

Consider:

Task task = SomeAsyncMethod();

task.Wait();

or:

var result = SomeAsyncMethod().Result;

Blocking on asynchronous operations can create deadlock scenarios, particularly in environments with synchronization contexts that require continuations to return to a blocked thread.

Important

In modern ASP.NET Core, the classic UI-style SynchronizationContext deadlock scenario generally does not apply in the same way.

However, blocking on async code is still bad practice because it can:

waste threads
reduce scalability
contribute to ThreadPool starvation
create complicated blocking behavior
Preferred approach

Use:

await SomeAsyncMethod();

instead of:

SomeAsyncMethod().Wait();

or:

SomeAsyncMethod().Result;
18. Deadlock Prevention Strategies
1. Consistent lock ordering
Always:
A → B → C
2. Keep critical sections small

Do not hold synchronization longer than necessary.

3. Avoid nested locks

Use simpler synchronization where possible.

4. Avoid blocking async code

Prefer:

await

over:

.Wait()
.Result
5. Use timeouts when appropriate

For example:

if (Monitor.TryEnter(
    lockObject,
    TimeSpan.FromSeconds(2)))
{
    try
    {
        // Work
    }
    finally
    {
        Monitor.Exit(lockObject);
    }
}
else
{
    // Could not acquire lock.
}

A timeout does not necessarily fix the underlying design problem, but it can prevent indefinite waiting.

6. Use cancellation

For async synchronization:

await semaphore.WaitAsync(
    cancellationToken);

This allows waiting to stop when cancellation is requested.

7. Reduce shared mutable state

Prefer:

immutable objects
message passing
thread confinement
concurrent collections
stateless services

when appropriate.

19. Deadlock Detection

Deadlocks can be difficult to diagnose.

Useful information includes:

thread dumps
stack traces
blocked threads
lock ownership
wait chains
CPU usage
ThreadPool metrics
application traces
diagnostic tools

A common pattern is:

Thread 1 → Waiting for Lock B
Thread 2 → Waiting for Lock A

The wait chain reveals the circular dependency.

20. Production Troubleshooting Approach

If an application appears stuck:

Step 1

Check whether requests are blocked.

Step 2

Check ThreadPool/thread counts.

Step 3

Look for synchronous blocking:

.Wait()
.Result
Step 4

Look for lock contention.

Step 5

Identify lock ownership.

Step 6

Build the wait-for graph.

Example:

Thread A
   ↓
Lock B

Thread B
   ↓
Lock A
Step 7

Look for the circular dependency.

21. ASP.NET Core Example

Imagine:

Request A
   ↓
Lock A
   ↓
External Service
   ↓
Request/Callback B
   ↓
Lock B

Meanwhile:

Request B
   ↓
Lock B
   ↓
Needs Lock A

Holding locks while performing external calls can create complex dependency chains.

Better design

Avoid keeping application locks across network boundaries.

Use:

database concurrency mechanisms
idempotency
queues
transactions
optimistic concurrency
proper state transitions

depending on the business problem.

22. Database Deadlocks

Deadlocks are not limited to C# locks.

Databases can also experience deadlocks.

Example:

Transaction A
    ↓
Locks Row 1
    ↓
Waits for Row 2

Transaction B
    ↓
Locks Row 2
    ↓
Waits for Row 1

The database detects the cycle and usually chooses one transaction as the deadlock victim.

Important

Application-level deadlock and database deadlock are related concepts but are handled differently.

23. Database Deadlock Prevention

Common techniques include:

consistent access order
short transactions
appropriate indexes
updating rows in consistent order
avoiding unnecessary locks
keeping transactions small
retrying transient deadlock failures where appropriate

For database deadlocks, a retry strategy may be appropriate because the database can resolve the deadlock by aborting one transaction.

24. Distributed Systems

A local C# lock:

lock (lockObject)
{
}

only coordinates threads inside the relevant process.

It does not coordinate:

Server A
Server B
Server C

Therefore, a local lock cannot solve a distributed deadlock/concurrency problem across servers.

Distributed systems may require:

database locking
distributed lock mechanisms
queues
optimistic concurrency
idempotency
transactional state management
25. Important Design Principle

Instead of asking:

"Where can I put a lock?"

Ask:

"Can I design the workflow so that shared locking is minimized?"

Good designs often reduce deadlock risk through:

immutable state
single ownership
message passing
queues
atomic database operations
optimistic concurrency
short critical sections
26. Common Mistakes
Mistake 1

Using different lock orders.

Thread A: A → B
Thread B: B → A
Mistake 2

Holding a lock during HTTP/database calls.

Mistake 3

Using .Result or .Wait() unnecessarily.

Mistake 4

Assuming SemaphoreSlim automatically prevents deadlocks.

Mistake 5

Using too many nested synchronization primitives.

Mistake 6

Creating complicated lock dependencies between services.

Mistake 7

Assuming a local lock solves distributed concurrency.

27. Product-Company Interview Questions
Q1. What is a deadlock?

A deadlock occurs when threads/tasks are permanently waiting for resources held by each other, so none can proceed.

Q2. What are the four necessary conditions for deadlock?
Mutual exclusion
Hold and wait
No preemption
Circular wait
Q3. How do you prevent deadlock?

Common approaches:

consistent lock ordering
avoid unnecessary nested locks
keep critical sections small
avoid blocking async code
avoid external calls while holding locks
use timeouts/cancellation where appropriate
reduce shared mutable state
Q4. Can lock cause deadlock?

Yes.

For example, inconsistent lock ordering can create circular waiting.

Q5. Can SemaphoreSlim cause deadlock?

Yes.

If multiple semaphores are acquired in inconsistent order, the same circular-wait problem can occur.

Q6. Can async code have deadlocks?

Yes.

Async code can participate in deadlocks, especially when synchronization primitives or synchronous blocking are used incorrectly.

Q7. Why should .Result and .Wait() generally be avoided with async code?

They synchronously block threads and can cause deadlocks in some environments while also reducing scalability and contributing to ThreadPool starvation.

Q8. Difference between deadlock and race condition?

Deadlock prevents progress because threads wait on each other.

Race condition causes incorrect or unpredictable behavior because concurrent operations access shared state without proper coordination.

Q9. Difference between deadlock and starvation?

Deadlock involves circular waiting.

Starvation occurs when a thread is continuously denied access to a resource.

Q10. Can a database have deadlocks?

Yes.

Database transactions can hold locks on different resources and wait for each other.

28. Quick Revision
Deadlock
↓
Threads cannot proceed
↓
Because they wait for each other's resources
Four conditions
Mutual Exclusion
        +
Hold and Wait
        +
No Preemption
        +
Circular Wait
        =
Deadlock
Main prevention technique
Consistent resource ordering

Example:

Correct:
A → B
A → B
A → B

Dangerous:
A → B
B → A

29. Key Points to Remember
Deadlock means no progress because threads/tasks wait on each other.
Four conditions are required: mutual exclusion, hold-and-wait, no preemption, circular wait.
Inconsistent lock ordering is a common cause.
Always acquire multiple locks in a consistent order.
Keep critical sections small.
Avoid external calls while holding locks.
Avoid unnecessary nested locks.
lock cannot contain await.
Use SemaphoreSlim for appropriate async synchronization.
SemaphoreSlim can still participate in deadlocks.
Avoid .Result and .Wait() in async workflows.
Deadlock is different from race condition.
Deadlock is different from starvation.
Deadlock is different from livelock.
Database transactions can also deadlock.
Local C# locks do not coordinate different application servers.
Timeouts and cancellation can prevent indefinite waiting but do not replace good lock design.
Reducing shared mutable state is often better than adding more locks.

30. Final Mental Model
          Thread A
             |
          Holds A
             |
             v
        Wants Resource B
             |
             |
             v
          Thread B
             |
          Holds B
             |
             v
        Wants Resource A
             |
             |
             └───────────────┐
                             |
                             v
                       DEADLOCK

Product-company one-line summary:
A deadlock is a state where concurrent operations cannot make progress because each is waiting for a resource held by another; the most practical prevention technique is consistent resource ordering combined with small critical sections, minimal blocking, and reduced shared mutable state.