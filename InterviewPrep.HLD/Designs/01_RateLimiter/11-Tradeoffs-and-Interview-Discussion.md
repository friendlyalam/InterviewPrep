# URL Shortener — Step 11: Trade-offs and Interview Discussion

# 1. Short Code Generation

## Option A — Random String

```text
aB92xK
```

Advantages:

* Simple
* Non-sequential

Disadvantages:

* Collision checking
* Possible retries

---

## Option B — Hash

```text
Hash(URL)
```

Advantages:

* Deterministic

Disadvantages:

* Collision handling
* Same URL may always produce same code
* Hash output needs shortening

---

## Option C — ID + Base62

```text
Unique ID
   ↓
Base62
   ↓
aB92xK
```

Advantages:

* Compact
* Efficient
* Easy to explain

Disadvantages:

* Requires scalable ID generation

### Interview Choice

Start with:

```text
Unique ID + Base62
```

Then discuss distributed ID generation at large scale.

---

# 2. SQL vs NoSQL

## SQL

Advantages:

* Strong consistency
* Unique constraints
* Transactions
* Mature indexing

## NoSQL

Advantages:

* Horizontal scalability
* Distributed architecture
* High throughput

### Initial Choice

```text
SQL + Redis
```

At extreme scale:

```text
Distributed NoSQL
```

can be considered.

---

# 3. 301 vs 302

## 301

Permanent redirect.

Advantages:

* Cache-friendly
* Indicates permanent destination

## 302

Temporary redirect.

Advantages:

* More control
* Better when destination may change

### Design Choice

Use:

```text
302
```

for a system where mappings may be managed/changed.

Discuss 301 if permanent URLs are a requirement.

---

# 4. Synchronous vs Asynchronous Analytics

## Synchronous

```text
Redirect
 ↓
Save Analytics
 ↓
Return
```

Problem:

```text
Higher latency
```

## Asynchronous

```text
Redirect
 ↓
Publish Event
 ↓
Return

Queue
 ↓
Worker
 ↓
Analytics DB
```

### Choice

```text
Asynchronous
```

because analytics is not part of the critical redirect path.

---

# 5. Cache vs No Cache

Without cache:

```text
Redirect
 ↓
Database
```

Problem:

```text
Database overload
```

With cache:

```text
Redirect
 ↓
Redis
 ↓
Database only on MISS
```

### Choice

```text
Redis
```

because the workload is read-heavy.

---

# 6. Monolith vs Microservices

## Monolith

Advantages:

* Simple
* Easy deployment
* Easy development

## Microservices

Advantages:

* Independent scaling
* Independent deployment
* Clear service boundaries

### Practical Evolution

Start:

```text
Modular Monolith
```

Then separate:

```text
URL Service
Redirect Service
Analytics Service
```

when scale or organizational requirements justify it.

---

# 7. Distributed ID Generator

At small scale:

```text
Database ID
```

At larger scale:

```text
Distributed ID Generator
```

Possible:

```text
Snowflake-style IDs
ID Service
Range Allocation
```

---

# 8. Read Replicas

Useful when database read traffic becomes significant.

But:

```text
Redis
```

should already absorb most redirect reads.

---

# 9. Sharding

Do not shard from day one.

Introduce sharding when:

```text
Storage too large
OR
Throughput too high
OR
Single DB becomes bottleneck
```

---

# 10. Multi-Region

Use when requirements demand:

* Regional disaster recovery
* Global availability
* Lower global latency

Do not add it unnecessarily.

---

# 11. Important Trade-off Principle

Every architectural decision should answer:

```text
Why?
```

Example:

```text
Why Redis?
→ Reduce DB reads and latency.

Why queue?
→ Keep analytics out of critical path.

Why stateless?
→ Easy horizontal scaling.

Why SQL?
→ Strong consistency and uniqueness.

Why Base62?
→ Compact short code.

Why asynchronous analytics?
→ Low redirect latency.
```

---

# 12. Most Important Failure Questions

## Q1. What if Redis fails?

Fallback to database.

---

## Q2. What if database fails?

Use replicas/failover. Existing cached URLs may still work.

---

## Q3. What if application server fails?

Load balancer routes traffic to healthy instances.

---

## Q4. What if queue fails?

Analytics should degrade independently from redirects.

---

## Q5. What if the same URL creation request is retried?

Use idempotency key.

---

## Q6. What if two users request the same custom alias?

Database unique constraint prevents duplicate ownership.

---

## Q7. What if a popular URL becomes extremely hot?

Aggressive caching, replication/CDN where appropriate.

---

## Q8. What if traffic becomes 10×?

Scale:

```text
Application
Redis
Database
Queue
```

independently according to bottlenecks.

---

# 13. 2-Minute Interview Answer

> I would design the URL shortener around two workloads: URL creation and redirection. The system is highly read-heavy, so the redirect path is the main scalability concern.
>
> For URL creation, the request goes through the API Gateway to the URL service. The service validates the URL, generates a unique ID, converts it to Base62, stores the mapping in the database, and populates Redis.
>
> For redirects, the request goes to the Redirect Service, which first checks Redis using the short code. On a cache hit, it immediately returns a redirect. On a miss, it reads from the database, updates Redis, and returns the redirect.
>
> Application services are stateless and horizontally scalable behind a load balancer. Analytics are asynchronous: the Redirect Service publishes a click event to a message queue, and workers process the event separately so analytics doesn't increase redirect latency.
>
> For reliability, I would use database replication, Redis high availability, health checks, timeouts, retries for transient failures, idempotency, and monitoring.
>
> At larger scale, I would introduce distributed ID generation, database sharding, distributed caching, multi-AZ deployment, and eventually multi-region architecture.

---

# 14. 30-Second Interview Summary

```text
Read-heavy system
        ↓
Stateless services
        ↓
Redis cache
        ↓
Database source of truth
        ↓
Queue for analytics
        ↓
Horizontal scaling
        ↓
Replication / Failover
        ↓
Monitoring + Security
```

---

# 15. Final Architecture

```text
                            CLIENT
                              │
                              ▼
                             DNS
                              │
                              ▼
                             CDN
                              │
                              ▼
                       LOAD BALANCER
                              │
                              ▼
                        API GATEWAY
                              │
                 ┌────────────┴────────────┐
                 │                         │
                 ▼                         ▼
           URL SERVICE              REDIRECT SERVICE
                 │                         │
                 ▼                         ▼
             DATABASE                   REDIS
                                           │
                                           │ MISS
                                           ▼
                                       DATABASE
                                           │
                                           ▼
                                      302 REDIRECT


                     CLICK EVENT
                          │
                          ▼
                    MESSAGE QUEUE
                          │
                          ▼
                  ANALYTICS WORKER
                          │
                          ▼
                  ANALYTICS DATABASE
```

---

# 16. Final Interview Checklist

Before saying "design complete", verify:

```text
[✓] Requirements
[✓] Capacity estimation
[✓] APIs
[✓] Architecture
[✓] Read flow
[✓] Write flow
[✓] Database
[✓] Indexing
[✓] Short-code generation
[✓] Base62
[✓] Redis
[✓] Cache-aside
[✓] Cache invalidation
[✓] Queue
[✓] Async processing
[✓] Idempotency
[✓] Rate limiting
[✓] Horizontal scaling
[✓] Replication
[✓] Sharding
[✓] Hot keys
[✓] Retry
[✓] Timeout
[✓] Circuit breaker
[✓] Failure scenarios
[✓] Security
[✓] Observability
[✓] RPO/RTO
[✓] Multi-AZ
[✓] Multi-region
[✓] Bottlenecks
[✓] Trade-offs
[✓] 2-minute explanation
```

# 17. Core Learning From URL Shortener

```text
URL Shortener
      │
      ├── Requirements
      ├── Capacity Estimation
      ├── REST API
      ├── Load Balancer
      ├── Stateless Services
      ├── Redis
      ├── Cache-Aside
      ├── SQL / NoSQL
      ├── Indexing
      ├── Base62
      ├── ID Generation
      ├── Message Queue
      ├── Async Processing
      ├── Idempotency
      ├── Rate Limiting
      ├── Replication
      ├── Sharding
      ├── Hot Keys
      ├── Reliability
      ├── Security
      ├── Observability
      └── Multi-Region
```

This problem becomes the foundation for many later HLD problems.
