# HLD SYSTEM DESIGN — MASTER TEMPLATE

## 1. Problem Statement

### System Name

[System name]

### Problem

[Clearly describe what the system needs to solve.]

### Goal

[What should the system achieve?]

---

# 2. Requirements

## 2.1 Functional Requirements

1. [Requirement 1]
2. [Requirement 2]
3. [Requirement 3]
4. [Requirement 4]

### Example

* User can create an account.
* User can log in.
* User can create an order.
* User can track an order.

---

## 2.2 Non-Functional Requirements

### Scalability

* [Expected number of users]
* [Expected traffic]
* [Expected growth]

### Availability

* Target availability: [e.g. 99.9%]

### Reliability

* System should tolerate [failure scenarios].

### Performance

* Target API latency: [e.g. < 200 ms]

### Consistency

* [Strong / Eventual / Mixed]
* Reason: [Why?]

### Durability

* Data should not be lost even if [failure scenario].

### Security

* Authentication
* Authorization
* Encryption
* Rate limiting
* Secure communication

### Maintainability

* Modular architecture
* Clear service boundaries
* Monitoring and logging

---

# 3. Scope

## 3.1 In Scope

1. [Feature]
2. [Feature]
3. [Feature]

## 3.2 Out of Scope

1. [Feature]
2. [Feature]
3. [Feature]

---

# 4. Assumptions

1. [Assumption]
2. [Assumption]
3. [Assumption]

Example:

* 10 million registered users.
* 2 million daily active users.
* Average user performs 10 requests/day.
* Traffic is higher during peak hours.

---

# 5. Capacity Estimation

## 5.1 User Estimation

Total Users = [X]

Daily Active Users (DAU) = [X]

Monthly Active Users (MAU) = [X]

---

## 5.2 Request Estimation

Requests per user per day = [X]

Daily Requests:

Total DAU × Requests/User/Day

= [Calculation]

Average Requests Per Second (RPS):

Daily Requests / 86,400

= [Calculation]

Peak RPS:

Average RPS × Peak Factor

= [Calculation]

---

## 5.3 Storage Estimation

Data generated per request = [X]

Daily Storage:

Daily Requests × Data/Request

= [Calculation]

Yearly Storage:

Daily Storage × 365

= [Calculation]

Expected storage after [X] years:

[Calculation]

---

## 5.4 Bandwidth Estimation

Average request size = [X]

Average response size = [X]

Ingress bandwidth:

RPS × Request Size

= [Calculation]

Egress bandwidth:

RPS × Response Size

= [Calculation]

---

# 6. API Design

## 6.1 API: [API Name]

### Request

```http
POST /api/[resource]
```

### Request Body

```json
{
  "field1": "value",
  "field2": "value"
}
```

### Response

```json
{
  "id": "123",
  "status": "success"
}
```

### Status Codes

* 200 OK
* 201 Created
* 400 Bad Request
* 401 Unauthorized
* 403 Forbidden
* 404 Not Found
* 409 Conflict
* 429 Too Many Requests
* 500 Internal Server Error

---

# 7. High-Level Architecture

## 7.1 Components

### Client

* Web Application
* Mobile Application
* Third-party clients

### Entry Layer

* DNS
* CDN
* Load Balancer
* API Gateway

### Application Layer

* [Service 1]
* [Service 2]
* [Service 3]

### Data Layer

* SQL Database
* NoSQL Database
* Cache
* Object Storage

### Async Layer

* Message Queue
* Event Bus
* Background Workers

### External Systems

* Payment Provider
* Email Provider
* SMS Provider
* Third-party APIs

---

# 8. Architecture Diagram

```text
                         ┌──────────────────┐
                         │      Clients     │
                         │ Web / Mobile     │
                         └────────┬─────────┘
                                  │
                                  ▼
                         ┌──────────────────┐
                         │       DNS        │
                         └────────┬─────────┘
                                  │
                                  ▼
                         ┌──────────────────┐
                         │       CDN        │
                         └────────┬─────────┘
                                  │
                                  ▼
                         ┌──────────────────┐
                         │  Load Balancer   │
                         └────────┬─────────┘
                                  │
                                  ▼
                         ┌──────────────────┐
                         │   API Gateway    │
                         └────────┬─────────┘
                                  │
              ┌───────────────────┼───────────────────┐
              │                   │                   │
              ▼                   ▼                   ▼
       ┌────────────┐      ┌────────────┐      ┌────────────┐
       │ Service A  │      │ Service B  │      │ Service C  │
       └─────┬──────┘      └─────┬──────┘      └─────┬──────┘
             │                   │                   │
             └───────────────────┼───────────────────┘
                                 │
                  ┌──────────────┼──────────────┐
                  │              │              │
                  ▼              ▼              ▼
             ┌─────────┐    ┌─────────┐    ┌────────────┐
             │ Cache   │    │Database │    │ Message    │
             │         │    │         │    │ Queue      │
             └─────────┘    └─────────┘    └─────┬──────┘
                                                 │
                                                 ▼
                                          ┌─────────────┐
                                          │   Workers   │
                                          └─────────────┘
```

---

# 9. Component Responsibilities

## 9.1 Client

Responsibilities:

* [Responsibility]
* [Responsibility]

---

## 9.2 CDN

Responsibilities:

* Cache static content
* Reduce latency
* Reduce load on origin servers

---

## 9.3 Load Balancer

Responsibilities:

* Distribute traffic
* Health checks
* Remove unhealthy instances
* Support horizontal scaling

---

## 9.4 API Gateway

Responsibilities:

* Routing
* Authentication
* Authorization
* Rate limiting
* Request validation
* Logging

---

## 9.5 Service A

Responsibilities:

* [Responsibility]

---

## 9.6 Service B

Responsibilities:

* [Responsibility]

---

# 10. Request Flow

## 10.1 Write Request

```text
Client
  ↓
API Gateway
  ↓
Service
  ↓
Validation
  ↓
Database
  ↓
Publish Event
  ↓
Message Queue
  ↓
Background Worker
  ↓
External Service
```

### Explanation

1. Client sends request.
2. API Gateway authenticates request.
3. Request reaches appropriate service.
4. Service validates input.
5. Data is written to database.
6. Service publishes an event.
7. Consumer processes event asynchronously.
8. External operation is performed.

---

# 11. Read Flow

```text
Client
  ↓
API Gateway
  ↓
Service
  ↓
Cache
  │
  ├── Cache HIT
  │       ↓
  │     Return
  │
  └── Cache MISS
          ↓
       Database
          ↓
       Update Cache
          ↓
        Return
```

---

# 12. Database Design

## 12.1 Database Selection

### SQL

Use when:

* Strong consistency is required.
* Relationships are important.
* Transactions are required.
* Complex queries are required.

### NoSQL

Use when:

* Very high scale is required.
* Flexible schema is useful.
* Access patterns are predictable.
* Horizontal scaling is important.

### Selected Database

[Database]

### Reason

[Explain why.]

---

# 13. Data Model

## Entity: [Entity Name]

| Field     | Type     | Description   |
| --------- | -------- | ------------- |
| Id        | UUID     | Primary key   |
| Name      | String   | Entity name   |
| CreatedAt | DateTime | Creation time |
| UpdatedAt | DateTime | Last update   |

---

# 14. Indexing Strategy

## Indexes

1. `[Column]`
2. `[Column1, Column2]`

### Reason

[Explain query pattern and why the index is required.]

### Considerations

* Write overhead
* Storage overhead
* Query performance
* Cardinality

---

# 15. Caching

## Cache Technology

[Redis / Other]

## What Should Be Cached?

1. [Data]
2. [Data]
3. [Data]

## Cache Strategy

* Cache-aside
* Read-through
* Write-through
* Write-behind

### Selected Strategy

[Strategy]

### Reason

[Reason]

## TTL

[Duration]

## Cache Invalidation

[Strategy]

---

# 16. Message Queue / Event-Driven Architecture

## Queue / Broker

[Kafka / RabbitMQ / Azure Service Bus / etc.]

## Events

### Event: [Event Name]

```json
{
  "eventId": "123",
  "eventType": "[EventType]",
  "timestamp": "2026-01-01T10:00:00Z",
  "data": {}
}
```

## Producer

[Service]

## Consumer

[Service]

## Why Asynchronous?

* Reduce API latency
* Decouple services
* Handle traffic spikes
* Retry failed operations
* Improve scalability

---

# 17. Consistency

## Required Consistency

[Strong / Eventual / Mixed]

### Strong Consistency Required For

* [Example]

### Eventual Consistency Acceptable For

* [Example]

### Reason

[Explain trade-off.]

---

# 18. Scalability

## 18.1 Horizontal Scaling

```text
              Load Balancer
                    │
        ┌───────────┼───────────┐
        ▼           ▼           ▼
    Instance 1  Instance 2  Instance 3
```

Add more instances when traffic increases.

---

## 18.2 Vertical Scaling

Increase:

* CPU
* RAM
* Storage

### Limitation

[Explain limitation.]

---

## 18.3 Database Scaling

### Read Scaling

* Read replicas

### Write Scaling

* Sharding
* Partitioning

### Other Techniques

* Indexing
* Connection pooling
* Query optimization

---

# 19. Database Replication

## Primary Database

Handles:

* Writes

## Replica Database

Handles:

* Reads

```text
                Application
                    │
             ┌──────┴──────┐
             ▼             ▼
          Primary       Replica
          Database      Database
             │
             └──── Replication ────►
```

### Failure Scenario

[What happens if primary fails?]

---

# 20. Sharding / Partitioning

## When Needed?

[Explain.]

## Partition Key

[Key]

### Example

```text
Shard 1 → Users 1–10M
Shard 2 → Users 10M–20M
Shard 3 → Users 20M–30M
```

### Problems

* Hot partitions
* Rebalancing
* Cross-shard queries
* Increased complexity

---

# 21. Availability

## Target

[99.9% / 99.99% / etc.]

## Techniques

* Multiple application instances
* Load balancing
* Database replication
* Multi-AZ deployment
* Health checks
* Automatic failover

---

# 22. Reliability

## Failure Scenarios

### Scenario 1: Service Failure

What happens?

[Answer]

### Scenario 2: Database Failure

What happens?

[Answer]

### Scenario 3: Queue Failure

What happens?

[Answer]

### Scenario 4: External API Failure

What happens?

[Answer]

---

# 23. Retry Strategy

## Retryable Errors

* Timeout
* Temporary network failure
* HTTP 503
* Temporary dependency failure

## Non-Retryable Errors

* Invalid request
* Authentication failure
* Authorization failure
* Validation failure

## Retry Policy

* Maximum retries: [X]
* Backoff: [Exponential]
* Jitter: [Yes/No]

---

# 24. Idempotency

## Why?

Prevent duplicate operations when a request is retried.

### Example

```text
Client
  ↓
POST /payment
Idempotency-Key: ABC123
```

If the same request arrives again:

```text
ABC123 → Already Processed
```

Return the original result instead of processing again.

---

# 25. Rate Limiting

## Why?

* Protect services
* Prevent abuse
* Prevent overload
* Ensure fair usage

## Strategy

* Token Bucket
* Leaky Bucket
* Fixed Window
* Sliding Window

### Selected Strategy

[Strategy]

### Example

```text
User → 100 requests/minute
```

After the limit:

```text
HTTP 429 Too Many Requests
```

---

# 26. Security

## Authentication

[JWT / OAuth 2.0 / OpenID Connect / etc.]

## Authorization

* RBAC
* ABAC

## Data Security

* TLS
* Encryption at rest
* Encryption in transit

## API Security

* Authentication
* Authorization
* Rate limiting
* Input validation
* API gateway

## Secrets

* Secret Manager
* Key Vault

---

# 27. Observability

## Logging

Capture:

* Request ID
* User ID where appropriate
* Service name
* Error
* Timestamp
* Correlation ID

## Metrics

Monitor:

* RPS
* Latency
* Error rate
* CPU
* Memory
* Database connections
* Cache hit ratio
* Queue depth

## Distributed Tracing

```text
Client
  ↓
Gateway
  ↓
Service A
  ↓
Service B
  ↓
Database
```

Use a correlation/trace ID across the complete request.

---

# 28. Monitoring & Alerts

## Important Alerts

* High error rate
* High latency
* High CPU
* High memory
* Database unavailable
* Queue backlog
* Cache failure
* Disk usage
* Service unhealthy

---

# 29. Disaster Recovery

## Backup

[Backup strategy]

## Recovery Point Objective (RPO)

Maximum acceptable data loss:

[X minutes]

## Recovery Time Objective (RTO)

Maximum acceptable recovery time:

[X minutes]

## Disaster Scenario

[Describe failure.]

## Recovery Strategy

[Describe recovery.]

---

# 30. Deployment Architecture

```text
                 Internet
                    │
                    ▼
                Load Balancer
                    │
          ┌─────────┴─────────┐
          ▼                   ▼
       Region/AZ 1          Region/AZ 2
          │                   │
      Application         Application
       Instances            Instances
          │                   │
          └─────────┬─────────┘
                    │
                 Database
```

---

# 31. Cloud Mapping

## Compute

[Azure App Service / AKS / VM / Container Apps]

## Database

[Azure SQL / Cosmos DB / etc.]

## Cache

[Azure Cache for Redis]

## Messaging

[Azure Service Bus / Event Hubs]

## Storage

[Azure Blob Storage]

## Gateway

[Azure API Management]

## Monitoring

[Application Insights / Azure Monitor]

---

# 32. Bottleneck Analysis

Identify potential bottlenecks.

### Bottleneck 1

[Component]

Problem:

[Problem]

Solution:

[Solution]

### Bottleneck 2

[Component]

Problem:

[Problem]

Solution:

[Solution]

---

# 33. Single Points of Failure

Check:

* Application server
* Load balancer
* Database
* Cache
* Queue
* External dependency
* Storage

### Identified SPOFs

1. [SPOF]
2. [SPOF]

### Mitigation

[Solution]

---

# 34. Trade-offs

## Decision 1

### Option A

[Description]

### Option B

[Description]

### Selected

[Option]

### Reason

[Reason]

---

## Decision 2

### Option A

[Description]

### Option B

[Description]

### Selected

[Option]

### Reason

[Reason]

---

# 35. Alternative Architecture

## Alternative

[Describe alternative design.]

### Advantages

* [Advantage]
* [Advantage]

### Disadvantages

* [Disadvantage]
* [Disadvantage]

### Why We Did Not Select It

[Reason]

---

# 36. Scaling From 1M → 10M → 100M Users

## 1M Users

Architecture:

[Description]

## 10M Users

Changes:

* [Change]
* [Change]

## 100M Users

Changes:

* [Change]
* [Change]

---

# 37. Security & Failure Scenario Questions

### Question 1

What happens if the database goes down?

Answer:

[Answer]

### Question 2

What happens if the cache goes down?

Answer:

[Answer]

### Question 3

What happens if a message is processed twice?

Answer:

[Answer]

### Question 4

What happens if the same API request is sent twice?

Answer:

[Answer]

### Question 5

What happens if traffic suddenly increases 10×?

Answer:

[Answer]

### Question 6

What happens if one application server crashes?

Answer:

[Answer]

### Question 7

What happens if an external API is unavailable?

Answer:

[Answer]

---

# 38. Final Architecture

```text
                              CLIENTS
                         Web / Mobile / API
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
             ┌────────────────────┼────────────────────┐
             │                    │                    │
             ▼                    ▼                    ▼
        SERVICE A            SERVICE B            SERVICE C
             │                    │                    │
             └────────────────────┼────────────────────┘
                                  │
                    ┌─────────────┼─────────────┐
                    │             │             │
                    ▼             ▼             ▼
                  CACHE       DATABASE       MESSAGE
                                             BROKER
                                                │
                                                ▼
                                             WORKERS
                                                │
                                                ▼
                                      EXTERNAL SERVICES
```

---

# 39. Interview Summary

## Requirements

[Short summary]

## Traffic

[Short summary]

## Storage

[Short summary]

## Main Components

1. [Component]
2. [Component]
3. [Component]

## Database

[Database + reason]

## Cache

[Cache + reason]

## Messaging

[Technology + reason]

## Scaling

[Main scaling strategy]

## Availability

[Main availability strategy]

## Reliability

[Main reliability strategy]

## Security

[Main security strategy]

## Main Trade-offs

1. [Trade-off]
2. [Trade-off]
3. [Trade-off]

---

# 40. Interview Follow-Up Questions

1. How would you scale this system?
2. What is the biggest bottleneck?
3. What happens if the database fails?
4. What happens if the cache fails?
5. How would you handle 10× traffic?
6. How would you make the system highly available?
7. How would you handle duplicate requests?
8. How would you handle duplicate messages?
9. Why did you choose SQL/NoSQL?
10. Why did you choose synchronous/asynchronous communication?
11. Where would you use caching?
12. Where would you use a message queue?
13. How would you partition the database?
14. How would you handle hot partitions?
15. How would you monitor the system?
16. How would you secure the APIs?
17. What happens during a regional outage?
18. What are the major trade-offs in your design?

---

# 41. Final Checklist

Before considering the HLD complete, verify:

[ ] Requirements defined

[ ] Functional requirements defined

[ ] Non-functional requirements defined

[ ] Scope defined

[ ] Assumptions defined

[ ] Capacity estimation completed

[ ] API design completed

[ ] Architecture diagram completed

[ ] Component responsibilities defined

[ ] Read flow defined

[ ] Write flow defined

[ ] Database selected

[ ] Data model defined

[ ] Indexing considered

[ ] Caching considered

[ ] Message queue considered

[ ] Consistency considered

[ ] Scalability considered

[ ] Replication considered

[ ] Sharding considered

[ ] Availability considered

[ ] Reliability considered

[ ] Retry strategy defined

[ ] Idempotency considered

[ ] Rate limiting considered

[ ] Security considered

[ ] Logging considered

[ ] Monitoring considered

[ ] Disaster recovery considered

[ ] SPOFs identified

[ ] Bottlenecks identified

[ ] Trade-offs discussed

[ ] Alternative design discussed

[ ] 10× / 100× scaling discussed

[ ] Interview follow-up questions answered

[ ] Final architecture documented
