# Distributed Lock

## 1. Definition

A **distributed lock** is a synchronization mechanism that allows multiple instances of an application to coordinate access to the same shared resource across different machines or processes.

Unlike C# `lock`, a distributed lock works across application instances.

```text
Server A ──┐
Server B ──┼──→ Distributed Lock Store
Server C ──┘
                 ↓
            Shared Resource

Only one instance should hold the lock for a particular resource at a time.

2. Why Distributed Lock Is Needed

Suppose an application has three servers:

Load Balancer
      │
 ┌────┼────┐
 ↓    ↓    ↓
A     B    C

A normal C# lock:

lock (_lockObject)
{
    ProcessOrder();
}

only protects code inside one process.

Server A, B, and C have different memory spaces and different lock objects.

Therefore, they can all enter the critical section simultaneously.

A distributed lock provides coordination across these instances.

3. Common Use Cases

Distributed locks can be useful for:

Preventing duplicate scheduled jobs.
Coordinating resource allocation.
Preventing duplicate processing.
Protecting a highly contended shared resource.
Cache stampede protection.
Distributed leader election in some designs.
Coordinating maintenance/migration tasks.

Example:

10 application instances
        ↓
Only 1 should run the scheduled job
        ↓
Distributed lock
4. How It Works

Typical flow:

Acquire Lock
     ↓
Lock Granted?
   /     \
 Yes      No
  ↓        ↓
Work     Wait/Skip
  ↓
Release Lock

The lock is stored in a shared system accessible by all application instances.

Possible lock stores include:

Redis.
Database.
Cloud-managed distributed coordination services.
5. Basic Example

Conceptually:

Server A
   ↓
Acquire "Order:101"
   ↓
Lock granted
   ↓
Process order
   ↓
Release "Order:101"

Server B
   ↓
Acquire "Order:101"
   ↓
Lock unavailable
   ↓
Wait / retry / skip

The important part is that both servers use the same distributed lock key.

6. Lock Key

A lock should normally represent the resource being protected.

Example:

product:101
order:5001
customer:123
job:daily-report

For inventory:

lock:inventory:product:101

Server A and Server B must use exactly the same key to coordinate.

7. Lock Ownership

A distributed lock should have an identifiable owner.

Conceptually:

Lock Key     = inventory:101
Owner        = Server-A/request-123
Expiration   = 30 seconds

The owner should be the only one allowed to release its lock.

This prevents one process from accidentally releasing another process's lock.

8. Lock Expiration / Lease

Distributed systems can fail.

Suppose:

Server A
   ↓
Acquires lock
   ↓
Server crashes

If the lock never expires:

Lock remains forever
       ↓
Resource permanently blocked

Therefore distributed locks commonly use a lease/expiration.

Example:

Lock acquired
TTL = 30 seconds

If the owner disappears, the lock can eventually become available.

9. Why TTL Alone Is Not Enough

Suppose:

Server A
   ↓
Lock TTL = 30 sec
   ↓
Long operation takes 60 sec

The lock may expire after 30 seconds.

Then:

Server B
   ↓
Acquires same lock
   ↓
Server A is still working

Now both servers may operate concurrently.

Therefore, long-running work may require:

Lock renewal/heartbeat.
A sufficiently designed lease.
Fencing tokens.
Idempotent operations.
Database-level correctness guarantees.
10. Fencing Tokens

A fencing token is a monotonically increasing value issued when acquiring a lock.

Example:

Server A → Lock token 10
Server A → starts work

Lock expires

Server B → Lock token 11
Server B → starts work

If Server A continues working with token 10, the protected resource can reject it because token 10 is older than token 11.

Conceptually:

Older token → Reject
Newer token → Accept

Fencing is important when a stale lock holder could continue operating after its lease expires.

11. Redis-Based Distributed Lock

Redis is commonly used for distributed coordination.

A simplified conceptual operation is:

SET lock-key unique-owner NX PX 30000

Meaning approximately:

NX → create only if key does not exist
PX → expiration in milliseconds

If the operation succeeds, the caller owns the lock for the lease duration.

The lock should be released only by its owner.

In production, prefer a well-tested Redis distributed-lock implementation rather than implementing the protocol yourself.

12. Database-Based Distributed Lock

A database can also coordinate application instances.

Example conceptual table:

DistributedLocks

Key
Owner
ExpiresAt

Instances compete to acquire the same lock record.

The acquisition must be atomic.

For example:

INSERT lock
WHERE key does not already have an active owner

The exact implementation depends on the database.

Database locking mechanisms, unique constraints, transactions, or provider-specific advisory/application locks can also be used.

13. Distributed Lock vs C# lock
| C# `lock`                       | Distributed Lock                         |
| ------------------------------- | ---------------------------------------- |
| Process-local                   | Works across processes/servers           |
| Uses local memory               | Uses shared coordination system          |
| Very fast                       | Network/storage overhead                 |
| Simple                          | More failure modes                       |
| No network dependency           | Depends on distributed infrastructure    |
| Suitable for local shared state | Suitable for cross-instance coordination |


14. Distributed Lock vs Database Transaction

They solve different problems.

Distributed Lock

Coordinates who is allowed to perform an operation.

Database Transaction

Provides atomicity and consistency for database changes.

For critical business operations, do not assume a distributed lock replaces a transaction.

Example:

Distributed Lock
       ↓
Read inventory
       ↓
Update inventory
       ↓
Database Transaction

The exact design depends on the business requirement.

15. Distributed Lock vs Optimistic Concurrency
Optimistic Concurrency
Read
 ↓
Work
 ↓
UPDATE WHERE Version = expectedVersion
 ↓
Conflict?
Distributed Lock
Acquire lock
 ↓
Work
 ↓
Release lock

Optimistic concurrency detects conflicts.

Distributed locking attempts to prevent concurrent execution of the protected operation.

16. Distributed Lock vs Pessimistic Database Lock

A database pessimistic lock protects data within database transaction semantics.

A distributed lock is an application-level coordination mechanism backed by a shared distributed system.

A distributed lock does not automatically guarantee database consistency.

For database correctness, database constraints and transactions should remain the authoritative mechanism where appropriate.

17. Scheduled Job Example

Suppose five application instances all run the same scheduled job:

Server A ──┐
Server B ──┤
Server C ──┼── Daily Report Job
Server D ──┤
Server E ──┘

Without coordination:

5 servers
   ↓
5 reports generated

With a distributed lock:

Server A → Acquire lock → Success → Run job
Server B → Acquire lock → Failed
Server C → Acquire lock → Failed
Server D → Acquire lock → Failed
Server E → Acquire lock → Failed

Only one instance runs the job.

18. Important Failure Scenarios

Distributed locks must consider:

Application crash
Owner crashes
   ↓
Lease eventually expires
Network failure
Server ↔ Lock Store
       ↓
Connection lost

The application must not blindly assume that inability to communicate means it still owns the lock.

Lock-store failure

If Redis/database/coordination service becomes unavailable, lock acquisition may fail.

Long-running operation

The lease may expire while work is still executing.

Clock problems

Do not rely on local wall-clock assumptions for correctness across distributed machines. Prefer server-side expiration/lease semantics and robust protocols.

19. Critical Rule: Do Not Hold a Distributed Lock Unnecessarily

Distributed locks are more expensive than local locks because they involve network/distributed infrastructure.

Avoid:

Acquire lock
   ↓
Call external API
   ↓
Wait 10 seconds
   ↓
Process large operation
   ↓
Release

Prefer to keep the protected section as small as the business requirement allows.

20. Distributed Lock and Idempotency

A distributed lock should not be your only protection against duplicate processing.

Distributed systems can experience:

Retries.
Timeouts.
Network failures.
Client retries.
Message redelivery.
Process crashes.

Therefore important operations should often also be idempotent.

Example:

Request ID = ABC123

If the same request arrives twice:

ABC123 → Process once
ABC123 → Return existing result

This protects against duplicate execution even when locking fails or a lease expires.

21. Distributed Lock and Message Processing

For message consumers:

Message
   ↓
Consumer A
Consumer B
Consumer C

A distributed lock may sometimes be used, but it is often better to use:

Consumer-group semantics.
Partition ownership.
Queue visibility/lease mechanisms.
Idempotent processing.
Database constraints.

Do not introduce distributed locking when the messaging platform already provides the required coordination.

22. When NOT to Use a Distributed Lock

Avoid it when a simpler mechanism provides the required correctness.

Consider:

Database transaction.
Atomic SQL update.
Unique constraint.
Optimistic concurrency.
Idempotency.
Queue partitioning.
Local synchronization.

For example, instead of locking inventory externally, an atomic database update may be better:

UPDATE Product
SET Stock = Stock - 1
WHERE Id = @Id
  AND Stock > 0;

Then check the affected-row count.

This can be simpler and more strongly tied to the authoritative data.

23. Common Mistakes
Mistake 1: Using C# lock across servers

It is process-local.

Mistake 2: Lock without expiration

A crashed owner can leave the resource unavailable indefinitely.

Mistake 3: Releasing another owner's lock

Always associate the lock with a unique owner/token.

Mistake 4: Long lock duration

Increases contention and failure risk.

Mistake 5: Assuming lock guarantees database consistency

Use database transactions/constraints where required.

Mistake 6: Ignoring lease expiration

A process may continue after its lock has expired.

Mistake 7: Using distributed locks everywhere

They add complexity and infrastructure dependency.

Mistake 8: Ignoring idempotency

Retries and failures can still cause duplicate business effects.

24. Interview Questions
Q1. What is a distributed lock?

A synchronization mechanism that coordinates access to a shared resource across multiple processes or application instances.

Q2. Why can't C# lock solve distributed concurrency?

Because each application instance has its own process memory and therefore its own lock object.

Q3. Where can a distributed lock be stored?

Common choices include Redis, a database, or a dedicated distributed coordination service.

Q4. Why does a distributed lock need expiration?

To prevent a crashed lock owner from holding the lock indefinitely.

Q5. What happens if the lock expires while the owner is still working?

Another instance may acquire the lock. The old owner can become a stale worker, so long operations may require lease renewal and/or fencing tokens.

Q6. Does a distributed lock replace a database transaction?

No. They solve different problems.

Q7. When should you avoid distributed locks?

When an atomic database operation, transaction, optimistic concurrency, idempotency, or messaging primitive can solve the problem more simply and reliably.

25. Key Points to Remember
1. Distributed lock coordinates multiple application instances.

2. C# lock is process-local; distributed locks are cross-instance.

3. A shared lock key identifies the protected resource.

4. Lock ownership should be identifiable.

5. Distributed locks commonly use leases/expiration.

6. A lock can expire while the original owner is still working.

7. Long-running operations may require lease renewal and fencing.

8. Distributed locks can introduce network and infrastructure failure modes.

9. Keep lock duration as short as practical.

10. A distributed lock does not replace database transactions.

11. Idempotency is important for retry and failure scenarios.

12. Prefer simpler mechanisms such as atomic database updates,
    constraints, optimistic concurrency, or queue semantics when they
    satisfy the requirement.

13. Distributed locks should be used only when cross-instance
    coordination is actually required.
One-Line Mental Model

Distributed lock = A shared, time-bounded ownership mechanism that coordinates multiple application 
instances so only the appropriate instance performs a protected operation at a time.