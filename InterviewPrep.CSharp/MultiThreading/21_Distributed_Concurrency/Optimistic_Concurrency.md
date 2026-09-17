# Optimistic Concurrency

## 1. Definition

**Optimistic concurrency** is a concurrency-control technique that assumes conflicts are uncommon.

Multiple users/processes can read the same data without locking it. When updating, the system verifies that the data has not changed since it was read.

If another operation changed the data, the update fails and the application handles the conflict.

---

## 2. Why It Is Needed

Consider an inventory system:

```text
Initial Stock = 10
Version = 5

Two requests read the same product:

Request A → Stock 10, Version 5
Request B → Stock 10, Version 5

Request A updates first:

Stock = 9
Version = 6

Request B still has:

Version = 5

Request B must not overwrite the newer data.

Optimistic concurrency detects this conflict.

3. Basic Mechanism

Commonly use a:

Version number
RowVersion / timestamp
Concurrency token
Last-modified value

Example:

Id = 101
Stock = 10
Version = 5

When updating:

UPDATE Product
SET Stock = 9,
    Version = 6
WHERE Id = 101
  AND Version = 5;

The important part is:

WHERE Version = 5

The update succeeds only if the row still has the expected version.

4. Detecting a Conflict

Suppose the update affects:

1 row → Success
0 rows → Concurrency conflict

If zero rows are affected, another operation has already changed the record.

The application can then:

Reload the latest data.
Inform the user.
Retry when appropriate.
Recalculate the operation.
Reject the operation.
5. Typical Flow
Read data
   ↓
Read version
   ↓
User/business logic
   ↓
UPDATE ... WHERE Id AND Version
   ↓
 ┌───────────────┐
 │ Rows affected │
 └───────┬───────┘
         │
    ┌────┴─────┐
    ↓          ↓
   1 row      0 rows
    ↓          ↓
 Success    Conflict
6. EF Core Example

A property can be configured as a concurrency token:

public class Product
{
    public int Id { get; set; }

    public int Stock { get; set; }

    public byte[] RowVersion { get; set; } = [];
}

Configuration:

modelBuilder.Entity<Product>()
    .Property(p => p.RowVersion)
    .IsRowVersion();

EF Core then includes the concurrency value when generating the update.

If another transaction changed the row first, EF Core can throw:

DbUpdateConcurrencyException

Handle it explicitly:

try
{
    await dbContext.SaveChangesAsync();
}
catch (DbUpdateConcurrencyException)
{
    // Handle concurrency conflict
}
7. Version Number Example

A simple conceptual implementation:

if (product.Version != expectedVersion)
{
    return false;
}

product.Stock = newStock;
product.Version++;

return true;

In a real database, however, the version check and update must happen atomically in the database.

The application must not perform:

SELECT version
        ↓
if version matches
        ↓
UPDATE

as two independent operations without proper database concurrency control.

8. Optimistic vs Pessimistic Concurrency
| Optimistic                             | Pessimistic                        |
| -------------------------------------- | ---------------------------------- |
| Assumes conflicts are uncommon         | Assumes conflicts may occur        |
| Usually no lock while reading          | Uses locking                       |
| Detects conflict during update         | Prevents conflicting access        |
| Better for many read-heavy workloads   | Useful when conflicts are frequent |
| Failed update may require retry/reload | Waiting/blocking can occur         |
| Uses version/concurrency token         | Uses database locks                |

9. When to Use

Optimistic concurrency is a good choice when:

Conflicts are relatively rare.
Reads are much more frequent than conflicting writes.
Long user interactions occur between read and update.
Locking would unnecessarily reduce scalability.
The system can handle update conflicts.

Common examples:

Product inventory.
Order updates.
Customer profiles.
Document editing.
Account settings.
Financial/business records.
10. Important Inventory Example

Suppose:

Stock = 10
Version = 5

Two customers attempt to buy one item.

Both initially read:

Stock = 10
Version = 5

Customer A executes:

UPDATE Product
SET Stock = 9,
    Version = 6
WHERE Id = 101
  AND Version = 5
  AND Stock >= 1;

Result:

1 row updated

Customer B executes using the old version:

UPDATE Product
SET Stock = 9,
    Version = 6
WHERE Id = 101
  AND Version = 5
  AND Stock >= 1;

Result:

0 rows updated

Customer B detects the conflict instead of overwriting Customer A's update.

11. Important Production Considerations

Optimistic concurrency alone does not solve every business-concurrency problem.

For critical operations, combine it with:

Database transactions.
Atomic SQL updates.
Unique constraints.
Idempotency.
Appropriate isolation levels.
Retry/reload logic.
Business validation.

For distributed systems, an in-memory version or local lock is not sufficient. The concurrency guarantee must be enforced by the shared authoritative data store.

12. Common Mistakes
Mistake 1: Checking version only in application memory

Not sufficient for multiple application servers.

Mistake 2: Separate read and update without concurrency condition

Can result in lost updates.

Mistake 3: Blindly retrying conflicts

A retry may need to reload the latest state and reapply business logic.

Mistake 4: Assuming thread safety solves database concurrency

Application-level thread safety does not protect data across multiple servers or processes.

Mistake 5: Ignoring affected-row count

For a version-based update, 0 rows affected can indicate a concurrency conflict.

13. Interview Questions
Q1. What is optimistic concurrency?

A concurrency-control strategy that allows concurrent reads and detects conflicts when updating by checking a version/concurrency token.

Q2. How is a conflict detected?

The update includes the expected version/concurrency token. If zero rows are updated, the data was changed by another operation.

Q3. What is a concurrency token?

A value used to determine whether a record changed since it was read, such as a version number or SQL Server rowversion.

Q4. Optimistic vs pessimistic concurrency?

Optimistic concurrency detects conflicts during updates; pessimistic concurrency prevents conflicts by locking resources.

Q5. What exception does EF Core commonly throw for an optimistic concurrency conflict?

DbUpdateConcurrencyException.

14. Key Points to Remember
1. Optimistic concurrency assumes conflicts are relatively uncommon.

2. It allows concurrent reads without holding locks.

3. A version/concurrency token is used to detect changes.

4. UPDATE should verify the expected version atomically.

5. 1 affected row = update succeeded.

6. 0 affected rows = possible concurrency conflict.

7. EF Core commonly uses DbUpdateConcurrencyException.

8. Optimistic concurrency prevents lost updates.

9. Conflict handling may require reload, retry, or user intervention.

10. Real database concurrency checks must be atomic at the database level.

11. Optimistic concurrency is different from application-level thread safety.

12. For critical business operations, combine it with transactions,
    atomic updates, constraints, and idempotency where appropriate.
One-Line Mental Model

Optimistic concurrency = Read freely → remember the version → update only if the version is still unchanged → otherwise detect and handle the conflict.