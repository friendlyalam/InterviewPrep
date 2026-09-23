# HLD — URL SHORTENER

## 0. Problem Statement

Design a highly scalable URL Shortening service similar to Bitly.

The system should allow a user to provide a long URL and receive a short URL.

Example:

Long URL:

```text
https://www.example.com/products/category/mobile/iphone-17?campaign=summer-sale
```

Short URL:

```text
https://short.ly/aB92xK
```

When a user opens the short URL:

```text
https://short.ly/aB92xK
```

the system should redirect the user to the original long URL.

---

# 1. Goals

The system should:

1. Create short URLs.
2. Redirect short URLs to original URLs.
3. Support very high read traffic.
4. Provide low-latency redirects.
5. Remain highly available.
6. Prevent duplicate/conflicting short codes.
7. Support expiration.
8. Provide basic analytics.
9. Protect the system from abuse.
10. Scale horizontally.

---

# 2. Functional Requirements

## 2.1 Create Short URL

User provides:

```text
Original URL
```

System returns:

```text
Short URL
```

Example:

```text
POST /api/v1/urls

Input:
https://example.com/products/123

Output:
https://short.ly/aB92xK
```

---

## 2.2 Redirect

User opens:

```text
GET /aB92xK
```

System finds the original URL and redirects:

```text
HTTP 301 / 302
```

to:

```text
https://example.com/products/123
```

---

## 2.3 Custom Alias

User may optionally request:

```text
https://short.ly/my-product
```

instead of a generated code.

---

## 2.4 Expiration

User may optionally specify:

```text
ExpiresAt
```

After expiration:

```text
Short URL → Expired
```

---

## 2.5 Delete / Disable URL

User can disable an existing short URL.

---

## 2.6 Analytics

System may track:

* Number of clicks
* Timestamp
* Country
* Device type
* Browser
* Referrer

Analytics should not slow down the redirect request.

Therefore analytics processing should preferably be asynchronous.

---

# 3. Non-Functional Requirements

## 3.1 Scalability

System should support:

* Millions of URLs
* Billions of redirects
* Large traffic spikes

---

## 3.2 Availability

Target:

```text
99.9%+
```

Redirect functionality should remain available even when some application instances fail.

---

## 3.3 Performance

Redirect should have very low latency.

Target:

```text
p95 < 100 ms
```

The exact target depends on infrastructure and requirements.

---

## 3.4 Reliability

The system should:

* Avoid losing URL mappings.
* Avoid duplicate short codes.
* Handle server failures.
* Handle database failures.
* Handle cache failures.

---

## 3.5 Consistency

URL creation requires strong consistency for the mapping:

```text
shortCode → originalUrl
```

Redirects can tolerate some eventual-consistency behavior for secondary data such as analytics.

---

## 3.6 Security

The system should:

* Validate URLs.
* Prevent malicious URLs where required.
* Rate-limit creation APIs.
* Protect against abuse.
* Authenticate users for management operations.

---

# 4. Scope

## In Scope

* URL creation
* URL redirection
* Custom aliases
* Expiration
* URL disabling
* Basic analytics
* Rate limiting
* Caching
* Horizontal scaling

## Out of Scope

For the first version:

* Advanced recommendation systems
* Full marketing platform
* Complex user billing
* Advanced fraud detection
* Detailed BI dashboards

---

# 5. Assumptions

For interview discussion, assume:

```text
Registered users       = 10 million
New URLs/day           = 10 million
Redirects/day          = 1 billion
Read : Write           = 100 : 1
Average URL size       = 500 bytes
Short code length      = 7 characters
```

The numbers are assumptions, not fixed requirements.

In an interview, we can change them based on interviewer expectations.

---

# 6. Capacity Estimation

## 6.1 URL Creation

New URLs:

```text
10 million/day
```

Average writes per second:

```text
10,000,000 / 86,400

≈ 116 writes/sec
```

Assume peak traffic is 5×:

```text
116 × 5

≈ 580 writes/sec
```

So we should design for approximately:

```text
~600 URL creations/sec
```

---

# 7. Redirect Traffic

Redirects:

```text
1 billion/day
```

Average RPS:

```text
1,000,000,000 / 86,400

≈ 11,574 requests/sec
```

Peak at 5×:

```text
11,574 × 5

≈ 57,870 requests/sec
```

So the system should be capable of handling roughly:

```text
~60K redirects/sec
```

This immediately tells us something important:

```text
READ TRAFFIC >> WRITE TRAFFIC
```

Therefore our architecture must be optimized primarily for reads.

---

# 8. Storage Estimation

Assume:

```text
Average record ≈ 1 KB
```

New URLs:

```text
10 million/day
```

Daily storage:

```text
10M × 1 KB

≈ 10 GB/day
```

Yearly:

```text
10 × 365

≈ 3.65 TB/year
```

This is before considering:

* Indexes
* Replication
* Metadata
* Analytics

Therefore actual storage will be higher.

---

# 9. High-Level Architecture

```text
                         ┌─────────────────┐
                         │     Clients     │
                         │ Web / Mobile    │
                         └────────┬────────┘
                                  │
                                  ▼
                         ┌─────────────────┐
                         │      DNS        │
                         └────────┬────────┘
                                  │
                                  ▼
                         ┌─────────────────┐
                         │      CDN        │
                         └────────┬────────┘
                                  │
                                  ▼
                         ┌─────────────────┐
                         │ Load Balancer   │
                         └────────┬────────┘
                                  │
                                  ▼
                         ┌─────────────────┐
                         │  API Gateway    │
                         └────────┬────────┘
                                  │
                     ┌────────────┴────────────┐
                     │                         │
                     ▼                         ▼
             ┌────────────────┐       ┌────────────────┐
             │ URL Service    │       │ Redirect       │
             │                │       │ Service        │
             └───────┬────────┘       └───────┬────────┘
                     │                        │
                     ▼                        ▼
             ┌────────────────┐       ┌────────────────┐
             │   Database     │       │     Redis      │
             └────────────────┘       └───────┬────────┘
                                              │
                                              ▼
                                      ┌────────────────┐
                                      │   Database     │
                                      └────────────────┘

                     Analytics
                         │
                         ▼
                ┌────────────────┐
                │ Message Queue  │
                └───────┬────────┘
                        │
                        ▼
                ┌────────────────┐
                │ Analytics      │
                │ Workers        │
                └───────┬────────┘
                        │
                        ▼
                ┌────────────────┐
                │ Analytics DB   │
                └────────────────┘
```

---

# 10. Main Components

## 10.1 Client

Examples:

* Browser
* Mobile application
* Web application

Responsibilities:

* Send URL creation request.
* Open short URL.

---

# 10.2 DNS

Example:

```text
short.ly
```

DNS resolves the domain to our infrastructure.

---

# 10.3 CDN

CDN can help cache frequently accessed redirects/content depending on the chosen design.

However, we should be careful with caching redirects because URL mappings may change or expire.

For the core interview design, Redis at the application layer is the primary cache.

---

# 10.4 Load Balancer

Responsibilities:

* Distribute requests.
* Perform health checks.
* Remove unhealthy instances.
* Enable horizontal scaling.

Example:

```text
                    Load Balancer
                         │
          ┌──────────────┼──────────────┐
          ▼              ▼              ▼
       Server 1       Server 2       Server 3
```

---

# 10.5 API Gateway

Responsibilities:

* Authentication
* Authorization
* Rate limiting
* Routing
* Request validation
* Logging

---

# 10.6 URL Service

Responsible for:

* Creating short URLs.
* Validating URLs.
* Generating short codes.
* Storing mappings.
* Handling custom aliases.

---

# 10.7 Redirect Service

Responsible for:

```text
shortCode
     ↓
originalUrl
     ↓
HTTP Redirect
```

This service is highly read-intensive.

---

# 10.8 Redis

Redis is used as the hot cache.

Example:

```text
Key:
url:aB92xK

Value:
https://example.com/products/123
```

Most popular URLs can be served directly from Redis.

---

# 10.9 Database

Stores permanent URL mappings.

Example:

```text
aB92xK
        ↓
https://example.com/products/123
```

Database is the source of truth.

---

# 10.10 Message Queue

Used for asynchronous analytics.

Example:

```text
Redirect
   ↓
Publish Click Event
   ↓
Message Queue
   ↓
Analytics Worker
   ↓
Analytics Database
```

This prevents analytics from slowing down redirects.

---

# 11. API Design

## 11.1 Create Short URL

```http
POST /api/v1/urls
```

Request:

```json
{
  "originalUrl": "https://example.com/products/123",
  "expiresAt": "2027-01-01T00:00:00Z",
  "customAlias": null
}
```

Response:

```json
{
  "shortCode": "aB92xK",
  "shortUrl": "https://short.ly/aB92xK",
  "expiresAt": "2027-01-01T00:00:00Z"
}
```

---

# 12. Redirect API

```http
GET /aB92xK
```

Response:

```http
HTTP/1.1 302 Found
Location: https://example.com/products/123
```

---

# 13. Why 302 Instead of 301?

For an interview:

```text
302 = temporary redirect
301 = permanent redirect
```

If URL destinations may change or we want more control over redirect behavior, 302 can be safer.

If the mapping is guaranteed permanent and browser/CDN caching is desirable, 301 can be considered.

This is a trade-off.

---

# 14. Database Design

## URL Table

```text
URL
---------------------------------------
Id
ShortCode
OriginalUrl
UserId
CreatedAt
ExpiresAt
IsActive
```

Example:

```text
Id          = 10001
ShortCode   = aB92xK
OriginalUrl = https://example.com/products/123
UserId      = 501
CreatedAt   = ...
ExpiresAt   = ...
IsActive    = true
```

---

# 15. Important Database Constraint

The following must be unique:

```text
ShortCode
```

Database should enforce:

```text
UNIQUE(ShortCode)
```

Why?

Because two different URLs cannot use:

```text
aB92xK
```

simultaneously.

Application-level checking alone is not enough.

---

# 16. Indexing

Important indexes:

```text
UNIQUE INDEX ShortCode
INDEX UserId
INDEX ExpiresAt
```

Most important query:

```sql
SELECT *
FROM Url
WHERE ShortCode = @shortCode;
```

Therefore `ShortCode` needs an efficient unique index.

---

# 17. SQL vs NoSQL

## Option 1 — SQL

Examples:

* SQL Server
* PostgreSQL
* MySQL

Advantages:

* Strong consistency
* Unique constraints
* Transactions
* Mature indexing
* Easy management of relationships

---

## Option 2 — NoSQL

Examples:

* DynamoDB
* Cassandra
* Cosmos DB

Advantages:

* Horizontal scalability
* High write throughput
* Flexible distributed architecture

---

# 18. Selection

For our first design:

```text
SQL Database
+
Redis
```

is a reasonable choice.

Why?

The URL mapping is simple and requires a strong uniqueness guarantee for `ShortCode`.

At extremely large scale, we can evolve toward a distributed NoSQL design.

---

# 19. Short Code Generation

This is one of the MOST IMPORTANT parts of the URL Shortener interview.

We need:

```text
Long URL
       ↓
Unique Short Code
```

Example:

```text
123456789
     ↓
aB92xK
```

---

# 20. Option 1 — Hash the URL

Example:

```text
SHA-256(URL)
```

Then take a few characters.

Problem:

Different URLs can theoretically produce the same short code.

Therefore we need collision handling.

Another problem:

Same URL may always generate the same code, which may or may not be desirable.

---

# 21. Option 2 — Random String

Generate:

```text
aB92xK
```

Check database:

```text
Does aB92xK exist?
```

If yes:

```text
Generate again.
```

If no:

```text
Store it.
```

Problem:

At huge scale, collision probability and repeated database checks become less attractive.

---

# 22. Option 3 — ID + Base62

Generate a unique numeric ID:

```text
1000001
```

Convert it to Base62:

```text
Base62(1000001)
       ↓
aB92xK
```

Base62 characters:

```text
a-z
A-Z
0-9
```

Total:

```text
62 characters
```

This is a common and easy-to-explain approach.

---

# 23. Why Base62?

Because it allows a large number of unique values using a small number of characters.

For example:

```text
62^6 ≈ 56.8 billion
```

and:

```text
62^7 ≈ 3.5 trillion
```

So 7 characters provide a very large keyspace.

---

# 24. ID Generation Problem

If multiple servers generate IDs, we cannot simply do:

```text
MAX(Id) + 1
```

because concurrent requests can generate duplicates.

Possible solutions:

* Database auto-increment
* Distributed ID generator
* Snowflake-style IDs
* Range allocation
* Central ID service

---

# 25. Recommended Interview Approach

Start simple:

```text
Database-generated unique ID
        ↓
Base62 encoding
        ↓
Short Code
```

Then discuss scaling:

```text
Distributed ID Generator
        ↓
Base62
```

This demonstrates good interview reasoning:

**Start simple → identify limitation → evolve architecture.**

---

# 26. Base62 Encoding

Character set:

```text
abcdefghijklmnopqrstuvwxyz
ABCDEFGHIJKLMNOPQRSTUVWXYZ
0123456789
```

Conceptually:

```text
Numeric ID
    ↓
Repeated division by 62
    ↓
Base62 characters
    ↓
Short Code
```

---

# 27. Create URL Flow

```text
Client
  │
  │ POST /api/v1/urls
  ▼
API Gateway
  │
  ▼
URL Service
  │
  ├── Validate URL
  │
  ├── Generate Unique ID
  │
  ├── Convert ID → Base62
  │
  ├── Store mapping
  │
  └── Update Redis
  │
  ▼
Response
```

Example:

```text
Original URL
     ↓
ID = 1000001
     ↓
Base62
     ↓
aB92xK
     ↓
Database
     ↓
Redis
     ↓
Return short URL
```

---

# 28. Redirect Flow

This is the MOST important request path.

```text
User
 │
 │ GET /aB92xK
 ▼
Load Balancer
 │
 ▼
Redirect Service
 │
 ▼
Redis
 │
 ├── HIT ────────► Original URL
 │
 │
 └── MISS
        │
        ▼
     Database
        │
        ▼
     Redis
        │
        ▼
   Original URL
        │
        ▼
    HTTP 302
```

---

# 29. Cache-Aside Pattern

For redirect:

```text
1. Check Redis.
2. If found → return URL.
3. If not found:
      Query database.
4. Store result in Redis.
5. Return URL.
```

This is called:

```text
Cache-Aside
```

---

# 30. Why Cache?

Because:

```text
Redirects ≈ 60K peak RPS
```

Database should not handle every request.

Redis can handle very high read throughput with low latency.

Therefore:

```text
Client
 ↓
Redis
 ↓
Database only on cache miss
```

---

# 31. Cache Key

```text
url:{shortCode}
```

Example:

```text
url:aB92xK
```

Value:

```text
https://example.com/products/123
```

---

# 32. Cache TTL

Example:

```text
TTL = 1 hour
```

or longer depending on requirements.

If URLs rarely change, a longer TTL may be appropriate.

If expiration matters, cache TTL should not allow an expired URL to remain active beyond its permitted lifetime.

---

# 33. Cache Invalidation

If URL is disabled:

```text
Database
   ↓
IsActive = false
   ↓
Delete Redis key
```

Example:

```text
DEL url:aB92xK
```

If the URL destination changes:

```text
Update DB
   ↓
Update/Delete cache
```

---

# 34. Cache Failure

If Redis fails:

```text
Redis unavailable
       ↓
Fallback to Database
```

The system should remain functional, although latency and database load may increase.

This is an important reliability principle:

```text
Cache = performance layer
Database = source of truth
```

---

# 35. Analytics

Do NOT perform heavy analytics synchronously during redirect.

Bad:

```text
Redirect
 ↓
Save analytics
 ↓
Redirect user
```

Better:

```text
Redirect
 ↓
Publish Click Event
 ↓
Return Redirect

Message Queue
 ↓
Analytics Worker
 ↓
Analytics DB
```

---

# 36. Click Event

Example:

```json
{
  "eventId": "evt-123",
  "shortCode": "aB92xK",
  "timestamp": "2026-09-23T10:00:00Z",
  "country": "IN",
  "device": "mobile",
  "browser": "Chrome"
}
```

---

# 37. Why Asynchronous Analytics?

Because analytics is not required to complete the redirect.

We prioritize:

```text
Redirect latency
```

over:

```text
Immediate analytics persistence
```

This reduces latency and decouples the systems.

---

# 38. Message Delivery

Possible delivery models:

```text
At-most-once
At-least-once
Exactly-once
```

For analytics, at-least-once processing is often acceptable if consumers are designed to handle duplicates.

Therefore:

```text
Event ID
+
Deduplication
```

can be used.

---

# 39. Read/Write Separation

Because:

```text
Reads >> Writes
```

we can scale independently.

```text
                 Load Balancer
                       │
              ┌────────┴────────┐
              │                 │
              ▼                 ▼
       URL Service       Redirect Service
                              │
                              ▼
                            Redis
                              │
                              ▼
                           Database
```

The redirect service can have many more instances than the URL creation service.

---

# 40. Horizontal Scaling

Application servers should be stateless.

Example:

```text
                    Load Balancer
                         │
          ┌──────────────┼──────────────┐
          ▼              ▼              ▼
       Instance 1     Instance 2     Instance 3
```

Any request can go to any instance.

No local session/state should be required.

---

# 41. Database Scaling

Initially:

```text
Application
     ↓
Primary DB
```

As traffic grows:

```text
                Application
                     │
             ┌───────┴────────┐
             ▼                ▼
          Primary          Read Replica
             │                │
           Writes            Reads
```

However, redirect reads should preferably be served by Redis first.

---

# 42. Database Sharding

At very large scale:

```text
Shard 1
Shard 2
Shard 3
Shard 4
```

Partition based on:

```text
ShortCode
```

or an underlying hash/key.

Important:

Avoid a partitioning strategy that creates hot shards.

---

# 43. Consistent Hashing

For very large distributed deployments, consistent hashing can help distribute keys across cache/database nodes while reducing the amount of data that needs to move when nodes are added or removed.

This becomes more relevant when discussing distributed caches or sharded storage.

---

# 44. Hot URLs

Suppose one URL becomes extremely popular:

```text
aB92xK
```

and receives millions of requests.

This creates a hot key.

Solution:

* Cache aggressively.
* Replicate/cache hot data.
* Use CDN where appropriate.
* Avoid sending every request to one backend node.

---

# 45. Rate Limiting

We should rate-limit:

```text
POST /api/v1/urls
```

to prevent abuse.

Example:

```text
100 URL creations / minute / user
```

Exact limits depend on product requirements.

Redis can be used to maintain distributed rate-limit counters.

---

# 46. URL Validation

Before creating a short URL:

Validate:

* URL syntax
* Allowed protocols
* Maximum length
* Blocked domains if required
* Malicious destinations if the product requires safety scanning

Example:

Accept:

```text
https://example.com
```

Reject:

```text
invalid-url
```

---

# 47. Security

## Authentication

Management APIs require authentication.

Example:

```text
POST /api/v1/urls
```

---

## Authorization

Users should only be able to manage their own URLs unless they have administrative privileges.

---

## HTTPS

All communication should use:

```text
HTTPS / TLS
```

---

## Rate Limiting

Protect against:

* Spam
* Abuse
* Automated URL generation

---

# 48. Idempotency

Consider:

```text
POST /api/v1/urls
```

Client sends request.

Network timeout occurs.

Client retries.

Without idempotency:

```text
Request 1 → short URL A
Request 2 → short URL B
```

Potentially two URLs are created.

We can support:

```text
Idempotency-Key
```

Example:

```text
Idempotency-Key: 12345
```

Then repeated requests can return the same result.

---

# 49. Reliability Patterns

Important patterns:

```text
Timeout
Retry
Exponential Backoff
Circuit Breaker
Bulkhead
Idempotency
Dead Letter Queue
```

For this system:

### Timeout

Prevent waiting indefinitely for dependencies.

### Retry

Use only for transient failures.

### Circuit Breaker

Prevent repeatedly calling an unhealthy dependency.

### Dead Letter Queue

Store messages that repeatedly fail processing.

---

# 50. Failure Scenario — Redis Down

```text
Redirect
   ↓
Redis unavailable
   ↓
Database
   ↓
Return URL
```

Problem:

Database traffic increases.

Mitigation:

* Redis cluster
* Replication
* Monitoring
* Failover
* Database capacity planning

---

# 51. Failure Scenario — Database Down

Existing cached URLs may continue working:

```text
Redis
  ↓
URL
  ↓
Redirect
```

But cache misses cannot be resolved.

Therefore highly available database infrastructure is required.

Possible techniques:

* Replication
* Automatic failover
* Multi-AZ deployment
* Backups

---

# 52. Failure Scenario — Application Server Down

Load balancer detects unhealthy instance.

```text
Instance 1 ❌
Instance 2 ✅
Instance 3 ✅
```

Traffic goes to healthy instances.

Because services are stateless, this is straightforward.

---

# 53. Failure Scenario — Queue Down

Redirect should ideally continue.

Analytics may temporarily accumulate elsewhere or be retried depending on the messaging architecture.

Important principle:

```text
Analytics failure
      ≠
Redirect failure
```

Keep non-critical asynchronous work decoupled from the critical path.

---

# 54. Expiration Handling

Suppose:

```text
ExpiresAt = 2026-10-01
```

After expiration:

```text
GET /aB92xK
```

should return something like:

```text
404 Not Found
```

or:

```text
410 Gone
```

depending on product semantics.

---

# 55. Expiration + Cache

Important interview issue:

Suppose Redis contains:

```text
aB92xK → originalUrl
```

but the URL has expired.

We must ensure cache does not allow expired URLs to continue redirecting.

Solutions:

1. Include expiration metadata in cached value.
2. Set Redis TTL based on `ExpiresAt`.
3. Validate expiration before redirect when necessary.

Best design:

```text
Cache TTL <= URL expiration time
```

---

# 56. Custom Alias

Example:

```text
POST /api/v1/urls

{
    "originalUrl": "https://example.com/product",
    "customAlias": "iphone"
}
```

Result:

```text
https://short.ly/iphone
```

Database:

```text
ShortCode = iphone
```

must have a unique constraint.

If already used:

```text
409 Conflict
```

---

# 57. Duplicate Original URLs

Question:

Should:

```text
https://example.com
```

always generate the same short code?

There are two possible designs.

### Design A

Same original URL → same short URL.

### Design B

Every creation request → new short URL.

Both are valid depending on product requirements.

For our design:

```text
Every creation request can generate a new short URL.
```

This gives users independent lifecycle/expiration/analytics behavior.

---

# 58. Observability

Monitor:

### Application

* RPS
* CPU
* Memory
* Error rate
* Latency

### Redirect

* Redirect latency
* Cache hit ratio
* Cache miss ratio
* Redirect failures

### Database

* CPU
* Connections
* Query latency
* Storage
* Replication lag

### Redis

* Memory
* Hit ratio
* Evictions
* Latency

### Queue

* Queue depth
* Consumer lag
* Failed messages
* DLQ size

---

# 59. Distributed Tracing

Example:

```text
Trace ID: ABC123

Client
  ↓
API Gateway
  ↓
Redirect Service
  ↓
Redis
  ↓
Database
```

The same trace/correlation ID should allow us to follow a request across services.

---

# 60. Disaster Recovery

Important concepts:

## RPO

How much data can we afford to lose?

Example:

```text
RPO = 5 minutes
```

## RTO

How quickly must the system recover?

Example:

```text
RTO = 30 minutes
```

Use:

* Automated backups
* Database replication
* Disaster recovery environment
* Restore testing

---

# 61. Availability Zones

Instead of:

```text
One server
```

use:

```text
                Load Balancer
                     │
        ┌────────────┼────────────┐
        ▼            ▼            ▼
       AZ1          AZ2          AZ3
        │            │            │
     Servers      Servers      Servers
```

If one availability zone fails, other zones continue serving traffic.

---

# 62. Multi-Region

At very large scale:

```text
                 Global DNS
                     │
          ┌──────────┴──────────┐
          ▼                     ▼
      Region A               Region B
          │                     │
       Services              Services
          │                     │
       Database              Database
```

Benefits:

* Lower geographic latency
* Regional failure protection

Challenges:

* Data replication
* Consistency
* Conflict handling
* Higher operational complexity

Do not introduce multi-region unnecessarily in the first version.

---

# 63. Final Architecture

```text
                           USERS
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
              ┌──────────────┴──────────────┐
              │                             │
              ▼                             ▼
        URL SERVICE                    REDIRECT SERVICE
              │                             │
              │                             ▼
              │                           REDIS
              │                             │
              │                     ┌───────┴───────┐
              │                     │               │
              │                   HIT             MISS
              │                     │               │
              │                     │               ▼
              │                     │           DATABASE
              │                     │               │
              │                     └───────┬───────┘
              │                             │
              │                             ▼
              │                       HTTP 302
              │
              ▼
          DATABASE
              │
              │
              ▼
           REDIS


REDIRECT EVENT
      │
      ▼
 MESSAGE QUEUE
      │
      ▼
 ANALYTICS WORKERS
      │
      ▼
 ANALYTICS DATABASE
```

---

# 64. Critical Path

The most important request is:

```text
GET /{shortCode}
```

Critical path:

```text
Client
 ↓
Load Balancer
 ↓
Redirect Service
 ↓
Redis
 ↓
302 Redirect
```

Database should ideally NOT be on the normal critical path.

Database is used mainly on cache misses.

---

# 65. Write Path

```text
Client
 ↓
API Gateway
 ↓
URL Service
 ↓
Generate ID
 ↓
Base62
 ↓
Database
 ↓
Redis
 ↓
Response
```

---

# 66. Read Path

```text
Client
 ↓
Load Balancer
 ↓
Redirect Service
 ↓
Redis
 ↓
302
```

Cache miss:

```text
Redis MISS
 ↓
Database
 ↓
Redis
 ↓
302
```

---

# 67. Main Bottlenecks

## Bottleneck 1 — Database Reads

Solution:

```text
Redis
```

---

## Bottleneck 2 — Hot Keys

Solution:

```text
Aggressive caching
+
CDN where appropriate
+
Replication
```

---

## Bottleneck 3 — ID Generation

Solution:

```text
Distributed ID generator
```

when a single database-generated sequence becomes a scaling constraint.

---

## Bottleneck 4 — Analytics

Solution:

```text
Message Queue
+
Workers
```

---

# 68. Single Points of Failure

Potential SPOFs:

* Single application server
* Single database
* Single Redis node
* Single queue broker

Mitigation:

```text
Multiple application instances
Database replication
Redis cluster/replication
Highly available messaging infrastructure
Multi-AZ deployment
```

---

# 69. Important Trade-offs

## SQL vs NoSQL

SQL:

```text
Strong consistency
Unique constraints
Transactions
```

NoSQL:

```text
Horizontal scalability
Distributed architecture
```

---

## Random Code vs ID + Base62

Random:

```text
Simple
Collision checking required
```

ID + Base62:

```text
Predictable
Compact
Efficient
Requires scalable ID generation
```

---

## 301 vs 302

301:

```text
Permanent
More cache-friendly
```

302:

```text
Temporary
More control
```

---

## Synchronous vs Asynchronous Analytics

Synchronous:

```text
Simple
But increases redirect latency
```

Asynchronous:

```text
Lower redirect latency
More scalable
More components
```

---

# 70. Scaling Evolution

## Version 1

```text
Load Balancer
      ↓
ASP.NET Core API
      ↓
SQL Server
```

Good for:

```text
Small scale
```

---

## Version 2

```text
Load Balancer
      ↓
Multiple API Instances
      ↓
Redis
      ↓
SQL Server
```

Adds:

```text
Horizontal scaling
Caching
```

---

## Version 3

```text
                 Load Balancer
                       │
             ┌─────────┴─────────┐
             ▼                   ▼
       URL Service         Redirect Service
             │                   │
             ▼                   ▼
          Database             Redis
                                  │
                                  ▼
                              Database

Redirect Events
       ↓
 Message Queue
       ↓
 Analytics Workers
```

Adds:

```text
Service separation
Async processing
Independent scaling
```

---

## Version 4 — Large Scale

```text
Global DNS
     │
 ┌───┴────┐
 ▼        ▼
Region A  Region B
 │        │
Services  Services
 │        │
Cache    Cache
 │        │
Distributed Storage
```

Adds:

```text
Multi-region
Global availability
Geographic scaling
```

---

# 71. Interview Explanation — 2 Minute Version

If the interviewer says:

"Explain your architecture."

Say:

The system has two primary workloads: URL creation and URL redirection. Since redirects are much more frequent than URL creation, I optimize the read path.

For URL creation, the client calls an API Gateway, which routes the request to the URL service. The service validates the URL, generates a unique ID, converts it to Base62 to create a short code, and stores the mapping in the database. The mapping is also placed in Redis.

For redirects, the request goes to the Redirect Service. It first checks Redis using the short code. On a cache hit, we immediately return a 302 redirect. On a cache miss, we query the database, populate Redis, and then redirect the user.

The application layer is stateless and can scale horizontally behind a load balancer.

Analytics are handled asynchronously. The redirect service publishes a click event to a message queue, and background workers process those events without increasing redirect latency.

For reliability, we use database replication, Redis high availability, health checks, timeouts, retries where appropriate, and monitoring.

At larger scale, we can introduce distributed ID generation, database sharding, multi-region deployment, and more advanced caching strategies.

---

# 72. Product-Company Interview Checklist

Before finishing this design, I should be able to explain:

[ ] Functional requirements

[ ] Non-functional requirements

[ ] Capacity estimation

[ ] Read/write ratio

[ ] API design

[ ] Architecture

[ ] Create URL flow

[ ] Redirect flow

[ ] Database choice

[ ] Database schema

[ ] Indexing

[ ] Short-code generation

[ ] Base62

[ ] ID generation

[ ] Redis

[ ] Cache-aside

[ ] Cache invalidation

[ ] Cache failure

[ ] Analytics

[ ] Message queue

[ ] Async processing

[ ] Idempotency

[ ] Rate limiting

[ ] URL validation

[ ] Horizontal scaling

[ ] Database replication

[ ] Database sharding

[ ] Hot keys

[ ] Availability

[ ] Reliability

[ ] Retry

[ ] Timeout

[ ] Circuit breaker

[ ] Security

[ ] Observability

[ ] Disaster recovery

[ ] RPO/RTO

[ ] Multi-AZ

[ ] Multi-region

[ ] Bottlenecks

[ ] SPOFs

[ ] Trade-offs

[ ] Scaling from small → large

[ ] 2-minute interview explanation

---

# 73. Technologies Mapping — .NET + Azure

For your technology stack, one possible production mapping is:

| HLD Component      | Azure / .NET Option                    |
| ------------------ | -------------------------------------- |
| API                | ASP.NET Core                           |
| API Gateway        | Azure API Management                   |
| Load Balancer      | Azure Application Gateway / Front Door |
| CDN / Global Entry | Azure Front Door                       |
| Database           | Azure SQL                              |
| Cache              | Azure Cache for Redis                  |
| Queue              | Azure Service Bus                      |
| Event Streaming    | Azure Event Hubs                       |
| Object Storage     | Azure Blob Storage                     |
| Containers         | Azure Container Apps / AKS             |
| Monitoring         | Azure Monitor                          |
| Tracing            | Application Insights                   |
| Secrets            | Azure Key Vault                        |
| Identity           | Microsoft Entra ID                     |

These are implementation choices. The HLD concepts come first; cloud services are mapped afterward.

---

# 74. What You Should Learn From This Problem

URL Shortener teaches several important HLD concepts:

```text
URL Shortener
      │
      ├── Requirements
      ├── Capacity Estimation
      ├── REST APIs
      ├── Load Balancer
      ├── Stateless Services
      ├── Caching
      ├── Redis
      ├── Database
      ├── Indexing
      ├── Base62
      ├── Unique ID Generation
      ├── Rate Limiting
      ├── Async Processing
      ├── Message Queue
      ├── Idempotency
      ├── Replication
      ├── Sharding
      ├── Hot Keys
      ├── Reliability
      ├── Availability
      ├── Security
      ├── Observability
      ├── Disaster Recovery
      └── Multi-Region Scaling
```

This is why URL Shortener is a very useful **first HLD problem**: it looks simple, but it exposes many of the fundamental ideas used in much larger product systems.
