# URL Shortener — Step 5: Database and Data Model

# 1. Database Responsibilities

Database is the:

```text
SOURCE OF TRUTH
```

Redis is only a cache.

---

# 2. URL Table

```text
URL
--------------------------------
Id
ShortCode
OriginalUrl
UserId
CreatedAt
ExpiresAt
IsActive
```

---

# 3. Example

```text
Id          = 1000001
ShortCode   = aB92xK
OriginalUrl = https://example.com/products/123
UserId      = 501
CreatedAt   = 2026-09-23
ExpiresAt   = 2027-01-01
IsActive    = true
```

---

# 4. Most Important Query

Redirect lookup:

```sql
SELECT *
FROM Url
WHERE ShortCode = @shortCode;
```

Therefore:

```text
ShortCode
```

must have an efficient index.

---

# 5. Unique Constraint

Very important:

```text
UNIQUE(ShortCode)
```

Why?

Two URLs cannot have:

```text
aB92xK
```

at the same time.

The database must enforce uniqueness.

Application-side checking alone is not sufficient because concurrent requests can race.

---

# 6. Indexes

Recommended:

```text
UNIQUE INDEX ShortCode
INDEX UserId
INDEX ExpiresAt
```

The most important index is:

```text
ShortCode
```

because redirect traffic constantly performs this lookup.

---

# 7. SQL vs NoSQL

## SQL

Examples:

```text
SQL Server
PostgreSQL
MySQL
```

Advantages:

* Strong consistency
* Unique constraints
* Transactions
* Mature indexing
* Easy querying

---

## NoSQL

Examples:

```text
DynamoDB
Cassandra
Cosmos DB
```

Advantages:

* Horizontal scalability
* Distributed architecture
* High throughput

---

# 8. Initial Choice

For a first production design:

```text
SQL Database + Redis
```

is reasonable.

Why?

URL mapping is simple and we need strong uniqueness for short codes.

---

# 9. Database Replication

As scale increases:

```text
             Application
                  │
           ┌──────┴──────┐
           ▼             ▼
        Primary       Read Replica
           │             │
         Writes         Reads
           │
           └── Replication ──►
```

However, Redis should handle most redirect reads.

---

# 10. Sharding

At very large scale:

```text
             Application
                  │
       ┌──────────┼──────────┐
       ▼          ▼          ▼
    Shard 1    Shard 2    Shard 3
```

Possible partition key:

```text
ShortCode
```

or a hash derived from it.

---

# 11. Sharding Problems

Be prepared to discuss:

* Hot partitions
* Rebalancing
* Cross-shard queries
* Operational complexity

---

# 12. Database Source of Truth

Important rule:

```text
Database = Truth
Redis    = Performance
```

If Redis loses data:

```text
Database → Rebuild cache
```

---

# 13. Database Failure

Use:

* Replication
* Automatic failover
* Multi-AZ deployment
* Backups
* Restore testing

---

# 14. Interview Questions

### Why SQL?

Strong consistency and unique constraints are useful.

### Why not only Redis?

Redis is a cache and should not be the only durable source of truth.

### Why index ShortCode?

Every redirect performs a lookup by ShortCode.

### When would you shard?

When a single database cannot handle storage/throughput requirements.

### Do we need sharding immediately?

No.

Start simple and introduce it when scale requires it.
