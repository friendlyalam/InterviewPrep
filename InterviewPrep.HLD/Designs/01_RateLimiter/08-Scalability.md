# URL Shortener — Step 8: Scalability

# 1. Main Scaling Challenge

The system is read-heavy:

```text
~600 writes/sec
~60K reads/sec peak
```

Therefore redirect scalability is the primary concern.

---

# 2. Horizontal Scaling

Application servers should be stateless.

```text
                    Load Balancer
                         │
             ┌───────────┼───────────┐
             ▼           ▼           ▼
          Server 1    Server 2    Server 3
```

Add more servers as traffic increases.

---

# 3. Independent Scaling

URL creation and redirects should scale independently.

```text
URL Service
    ↓
5 instances

Redirect Service
    ↓
50 instances
```

Numbers are illustrative.

---

# 4. Database Scaling

## Read Replicas

```text
             Database
                 │
        ┌────────┴────────┐
        ▼                 ▼
     Primary          Read Replica
      Writes              Reads
```

But Redis should absorb most redirect reads.

---

# 5. Database Sharding

At very large scale:

```text
                Application
                     │
          ┌──────────┼──────────┐
          ▼          ▼          ▼
       Shard 1    Shard 2    Shard 3
```

Partition using a suitable key such as:

```text
hash(ShortCode)
```

---

# 6. Distributed ID Generation

Initially:

```text
Database ID
    ↓
Base62
```

At very large scale:

```text
Distributed ID Generator
        ↓
Base62
```

Possible approaches:

* Snowflake-style IDs
* Range allocation
* Dedicated ID service

---

# 7. Base62

Characters:

```text
a-z
A-Z
0-9
```

Total:

```text
62 characters
```

For 7 characters:

```text
62^7 ≈ 3.5 trillion
```

This provides a large keyspace.

---

# 8. Hot Keys

One URL may become extremely popular.

Solutions:

* Redis
* Cache replication
* CDN where appropriate
* Avoid single-node concentration

---

# 9. CDN

CDN can potentially help with extremely popular redirect responses depending on:

* Redirect cache behavior
* TTL
* URL mutability
* Product requirements

But application-level Redis remains central to the basic design.

---

# 10. Multi-AZ

Deploy services across multiple availability zones.

```text
                Load Balancer
                     │
        ┌────────────┼────────────┐
        ▼            ▼            ▼
       AZ1          AZ2          AZ3
        │            │            │
     Servers      Servers      Servers
```

If one AZ fails, others can continue serving traffic.

---

# 11. Multi-Region

At very large scale:

```text
                Global Routing
                     │
             ┌───────┴───────┐
             ▼               ▼
          Region A        Region B
             │               │
          Services        Services
             │               │
           Cache           Cache
```

Benefits:

* Regional failure protection
* Lower geographic latency

Challenges:

* Data replication
* Consistency
* Conflict resolution
* Higher complexity

---

# 12. Scaling Evolution

## Version 1

```text
Load Balancer
      ↓
API
      ↓
Database
```

## Version 2

```text
Load Balancer
      ↓
Multiple APIs
      ↓
Redis
      ↓
Database
```

## Version 3

```text
URL Service
Redirect Service
Redis
Database
Message Queue
Analytics Workers
```

## Version 4

```text
Multi-AZ
+
Sharding
+
Distributed ID Generation
+
Multi-Region
```

---

# 13. Interview Principle

Do NOT immediately design:

```text
20 microservices
10 databases
5 regions
```

Instead:

```text
Start simple
     ↓
Estimate scale
     ↓
Find bottleneck
     ↓
Scale that component
```

This is an important product-company interview skill.

---

# 14. Interview Questions

### How do you handle 10× traffic?

* More stateless instances
* More Redis capacity
* Database scaling
* Queue scaling
* Load balancing

### What is the first bottleneck?

Likely database/cache/read path depending on workload.

### How do you handle 100× traffic?

Introduce:

* Distributed ID generation
* Sharding
* Distributed caching
* Multi-region architecture

### Can all services scale equally?

No.

Scale according to workload.
