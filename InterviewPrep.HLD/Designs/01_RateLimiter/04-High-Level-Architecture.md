# URL Shortener — Step 4: High-Level Architecture

# 1. Main Architecture

```text
                         ┌─────────────────┐
                         │     Clients     │
                         │ Web / Mobile    │
                         └────────┬────────┘
                                  │
                                  ▼
                              DNS / CDN
                                  │
                                  ▼
                           Load Balancer
                                  │
                                  ▼
                            API Gateway
                                  │
                    ┌─────────────┴─────────────┐
                    │                           │
                    ▼                           ▼
              URL Service                Redirect Service
                    │                           │
                    ▼                           ▼
                Database                     Redis
                                                │
                                                ▼
                                            Database

Redirect Event
      │
      ▼
 Message Queue
      │
      ▼
 Analytics Workers
      │
      ▼
 Analytics Database
```

---

# 2. Component Responsibilities

## Client

* Create short URLs.
* Open short URLs.
* View analytics.

---

## DNS

Resolves:

```text
short.ly
```

to our infrastructure.

---

## Load Balancer

* Distributes requests.
* Performs health checks.
* Removes unhealthy instances.

---

## API Gateway

* Authentication
* Authorization
* Routing
* Rate limiting
* Request validation

---

## URL Service

Handles:

* URL validation
* ID generation
* Base62 encoding
* Database writes
* Custom aliases

---

## Redirect Service

Handles:

```text
shortCode → originalUrl
```

It is optimized for high read traffic.

---

## Redis

Stores frequently accessed URL mappings.

```text
url:aB92xK
        ↓
https://example.com/products/123
```

---

## Database

Source of truth.

Stores:

```text
ShortCode
OriginalUrl
UserId
CreatedAt
ExpiresAt
IsActive
```

---

## Message Queue

Used for asynchronous analytics.

---

## Analytics Workers

Consume click events and store analytics.

---

# 3. Why Separate URL and Redirect Services?

Traffic characteristics are different.

Creation:

```text
~600/sec peak
```

Redirect:

```text
~60K/sec peak
```

Therefore they should be independently scalable.

---

# 4. Stateless Services

Application servers should be stateless.

```text
                    Load Balancer
                         │
             ┌───────────┼───────────┐
             ▼           ▼           ▼
          Server 1    Server 2    Server 3
```

Any request can go to any server.

Benefits:

* Easy horizontal scaling.
* Easy failover.
* No session synchronization.

---

# 5. Critical Read Path

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

Database is only needed on cache miss.

---

# 6. Cache Miss Path

```text
Redirect Service
      ↓
    Redis
      ↓
    MISS
      ↓
  Database
      ↓
  Redis SET
      ↓
  302 Redirect
```

---

# 7. Write Path

```text
Client
  ↓
API Gateway
  ↓
URL Service
  ↓
Validate URL
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

# 8. Why This Architecture?

Because the main characteristics are:

```text
READ HEAVY
LOW LATENCY
HIGH AVAILABILITY
HORIZONTAL SCALABILITY
```

Therefore:

```text
Stateless Services
+
Redis
+
Database
+
Message Queue
```

are the major architectural building blocks.

---

# 9. Interview Explanation

> The system has two primary workloads: URL creation and URL redirection.
Since redirects are much more frequent, I separate the URL creation and redirect paths.
URL creation stores the mapping in the database and populates Redis. 
Redirect requests first check Redis and fall back to the database on a miss.
The application layer is stateless and horizontally scalable. Analytics are
processed asynchronously through a message queue so they don't increase redirect latency.
