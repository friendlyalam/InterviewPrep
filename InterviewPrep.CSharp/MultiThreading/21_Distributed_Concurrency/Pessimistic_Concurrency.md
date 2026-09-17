# Pessimistic Concurrency

## 1. Definition

**Pessimistic concurrency** is a concurrency-control technique that assumes conflicts are likely and therefore prevents conflicting operations by locking the data/resource before modifying it.

The basic idea is:

```text
Acquire Lock
    ↓
Read / Modify Data
    ↓
Commit
    ↓
Release Lock

While the lock is held, other transactions may have to wait.

2. Why It Is Needed

Consider inventory:

Stock = 1

Two customers try to purchase the last item simultaneously.

Without proper coordination:

Customer A → Read Stock = 1
Customer B → Read Stock = 1

A → Purchase
B → Purchase

Possible result → Stock = -1 / overselling

With pessimistic concurrency:

Customer A → Acquire lock
              ↓
             Read stock = 1
              ↓
             Update stock = 0
              ↓
             Commit
              ↓
             Release lock

Customer B → Waits
              ↓
             Reads stock = 0
              ↓
             Purchase rejected
3. Database Locks

Pessimistic concurrency is commonly implemented using database locks.

Typical lock types include:

Shared lock
Exclusive lock
Update lock
Row-level lock
Page-level lock
Table-level lock

The exact behavior depends on the database and transaction/isolation configuration.

4. Shared vs Exclusive Locks
Shared Lock

Allows multiple transactions to read the same data while preventing conflicting modifications.

Transaction A → Read
Transaction B → Read
Transaction C → Read

Multiple readers may coexist depending on the database/isolation level.

Exclusive Lock

Used when modifying data.

Transaction A → Exclusive Lock
                    ↓
                 Update
                    ↓
                 Commit
                    ↓
              Release Lock

Other conflicting operations may have to wait.

5. Typical Database Flow
BEGIN TRANSACTION
       ↓
Acquire required lock
       ↓
Read data
       ↓
Validate business condition
       ↓
Update data
       ↓
COMMIT
       ↓
Release locks

The important principle is:

The lock must protect the entire critical database operation.

6. SQL Server Example

A common SQL Server pattern is to use an update lock while reading a row that will subsequently be modified:

BEGIN TRANSACTION;

SELECT Stock
FROM Product WITH (UPDLOCK, ROWLOCK)
WHERE Id = @ProductId;

-- Validate stock

UPDATE Product
SET Stock = Stock - 1
WHERE Id = @ProductId;

COMMIT;

UPDLOCK requests update locks for the selected rows, helping coordinate competing transactions that intend to update them.

ROWLOCK is a locking hint requesting row-level locking, but SQL Server can still make locking decisions based on its own requirements.

7. EF Core Example

Pessimistic concurrency usually requires database-specific transaction/locking behavior rather than simply using lock in C#.

Conceptually:

await using var transaction =
    await dbContext.Database.BeginTransactionAsync();

var product =
    await GetProductWithDatabaseLockAsync(
        productId);

if (product.Stock <= 0)
{
    return;
}

product.Stock--;

await dbContext.SaveChangesAsync();

await transaction.CommitAsync();

The actual locking mechanism depends on the database provider and SQL being executed.

8. Important: C# lock Is Not Database Locking

This is a major interview point.

lock (_lockObject)
{
    UpdateDatabase();
}

protects code only within the current application process.

If you have:

Server A
Server B
Server C

each server has its own _lockObject.

Therefore:

Server A → lock A
Server B → lock B
Server C → lock C

These locks do not coordinate with each other.

For distributed database concurrency, use database-level concurrency mechanisms or another distributed coordination mechanism.

9. Pessimistic Concurrency vs Optimistic Concurrency
| Pessimistic                           | Optimistic                                                   |
| ------------------------------------- | ------------------------------------------------------------ |
| Assumes conflicts are likely          | Assumes conflicts are uncommon                               |
| Prevents conflicts using locks        | Detects conflicts during update                              |
| Other transactions may wait           | Transactions usually do not wait for application-level locks |
| Can cause blocking                    | Can cause retries/reloads                                    |
| Can cause deadlocks                   | Generally avoids locking-based deadlocks                     |
| Useful for highly contended resources | Useful for read-heavy/low-conflict workloads                 |
| Database locks commonly used          | Version/concurrency token commonly used                      |

10. When to Use Pessimistic Concurrency

Consider it when:

Conflicts are frequent.
The resource is highly contended.
Waiting is preferable to conflict/retry.
The critical operation must be serialized.
The transaction can remain short.
The database provides appropriate locking support.

Examples:

Highly contested inventory.
Seat/room reservation.
Limited-capacity resource allocation.
Certain financial operations.
Critical state transitions.
11. Keep Transactions Short

A major production rule:

Acquire the lock as late as possible and release it as early as possible.

Bad:

BEGIN TRANSACTION
    ↓
Acquire lock
    ↓
Call external API
    ↓
Wait 5 seconds
    ↓
Update database
    ↓
COMMIT

This keeps the lock for too long.

Better:

Perform external work if possible
        ↓
BEGIN TRANSACTION
        ↓
Acquire lock
        ↓
Validate
        ↓
Update
        ↓
COMMIT

Do not hold database locks while waiting for slow external services unless the business operation genuinely requires that transactional behavior.

12. Lock Contention

When many transactions need the same resource:

Transaction A → LOCK
Transaction B → WAIT
Transaction C → WAIT
Transaction D → WAIT

This is lock contention.

High contention can cause:

Increased latency.
Lower throughput.
Blocking.
Connection/resource consumption.
Timeouts.
13. Deadlocks

Pessimistic locking can introduce deadlocks.

Example:

Transaction A:
Lock Account 1
      ↓
Wait for Account 2

Transaction B:
Lock Account 2
      ↓
Wait for Account 1

Neither can continue.

Avoid this by:

Keeping transactions short.
Accessing resources in a consistent order.
Avoiding unnecessary locks.
Avoiding long-running transactions.
Using appropriate isolation levels.
Handling database deadlock errors with carefully designed retry logic when appropriate.
14. Isolation Levels

Database isolation level affects how transactions interact.

Common SQL Server isolation levels include:

Read Uncommitted
Read Committed
Repeatable Read
Serializable
Snapshot

Higher isolation generally provides stronger consistency but may increase blocking or resource usage, depending on the database and configuration.

Serializable is particularly restrictive and can significantly increase contention.

Do not choose the highest isolation level automatically.

Choose based on the actual business consistency requirement.

15. Pessimistic Concurrency and Transactions

Locks are normally associated with a transaction boundary.

Example:

BEGIN
  ↓
Lock resource
  ↓
Validate
  ↓
Modify
  ↓
COMMIT
  ↓
Release

If the transaction rolls back:

ROLLBACK
  ↓
Changes discarded
  ↓
Locks released
16. Pessimistic Concurrency for Inventory

Example:

Stock = 10

Request A:

Acquire lock
    ↓
Stock = 10
    ↓
Stock >= 1
    ↓
Stock = 9
    ↓
Commit
    ↓
Release lock

Request B:

Wait for lock
    ↓
Acquire lock
    ↓
Read Stock = 9
    ↓
Stock = 8
    ↓
Commit

The operations are serialized for the protected resource.

17. Pessimistic Concurrency Is Not Always Better

It prevents conflicts, but it can introduce:

Blocking.
Lock contention.
Deadlocks.
Reduced throughput.
Longer response times.
Database resource pressure.

Therefore:

More locking
≠
More correctness

The locking strategy must match the business requirement.

18. Pessimistic vs Application-Level Synchronization

There are different concurrency scopes:

C# lock
    ↓
Current process

Database lock
    ↓
Database transactions

Distributed lock
    ↓
Multiple application instances

Choose the mechanism according to where the shared resource actually exists.

19. Common Mistakes
Mistake 1: Using C# lock for distributed database concurrency

A local lock does not coordinate multiple application servers.

Mistake 2: Holding database locks during HTTP calls

Slow external calls can create long lock durations.

Mistake 3: Keeping transactions unnecessarily large

Large transactions increase blocking and contention.

Mistake 4: Ignoring deadlocks

Pessimistic locking can create deadlock scenarios.

Mistake 5: Always choosing Serializable

Stronger isolation can increase contention and reduce concurrency.

Mistake 6: Assuming row locks are always guaranteed

Database engines can escalate or choose different locking strategies.

Mistake 7: Assuming locking alone solves business correctness

Business rules may also require:

Constraints.
Transactions.
Idempotency.
Atomic updates.
Proper validation.
20. Interview Questions
Q1. What is pessimistic concurrency?

A concurrency-control approach that assumes conflicts are likely and uses locks to prevent conflicting operations.

Q2. What is the main disadvantage?

Blocking and lock contention can reduce throughput and increase latency.

Q3. Can pessimistic concurrency cause deadlocks?

Yes. Multiple transactions acquiring locks in conflicting orders can create deadlocks.

Q4. Is C# lock suitable for coordinating multiple ASP.NET Core servers?

No. lock is process-local.

Q5. When would you prefer pessimistic concurrency?

When a resource is highly contended and preventing conflicting operations is preferable to detecting and resolving conflicts afterward.

Q6. What should you do to reduce locking problems?

Keep transactions short, minimize locked resources, acquire locks consistently, and avoid slow external operations while holding locks.

Q7. How is it different from optimistic concurrency?

Pessimistic concurrency prevents conflicts using locks; optimistic concurrency allows concurrent access and detects conflicts when updating.

21. Key Points to Remember
1. Pessimistic concurrency assumes conflicts are likely.

2. It uses locks to prevent conflicting operations.

3. Database transactions are commonly used with pessimistic concurrency.

4. C# lock and database locks are different.

5. C# lock is process-local.

6. Database locks can coordinate competing transactions through the database.

7. Lock contention increases latency and can reduce throughput.

8. Pessimistic locking can cause deadlocks.

9. Keep transactions and critical sections as short as possible.

10. Avoid slow external calls while holding database locks.

11. Isolation level affects concurrency and blocking behavior.

12. Do not automatically choose the strongest isolation level.

13. Use pessimistic concurrency when contention is high and waiting is
    preferable to conflict/retry.

14. For distributed applications, concurrency control must operate at
    the shared-resource level.

15. Locking is a tool for correctness, not automatically a performance
    optimization.
One-Line Mental Model

Pessimistic concurrency = Lock the shared resource first → perform the critical operation safely → commit → release the lock, accepting some blocking to prevent conflicts.