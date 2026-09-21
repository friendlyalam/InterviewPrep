# HLD — DAILY QUICK STRUCTURE

## 1. Requirements

* Functional Requirements
* Non-Functional Requirements
* In Scope
* Out of Scope

## 2. Capacity Estimation

* Users
* DAU
* Requests/sec (RPS)
* Storage
* Bandwidth
* Peak traffic

## 3. APIs

* Main APIs
* Request
* Response
* HTTP methods
* Status codes

## 4. High-Level Architecture

* Client
* DNS / CDN
* Load Balancer
* API Gateway
* Services
* Cache
* Database
* Message Queue
* External Services

## 5. Data Flow

### Write Flow

Client → Gateway → Service → Database → Event/Queue

### Read Flow

Client → Gateway → Service → Cache → Database

## 6. Database

* SQL or NoSQL?
* Data model
* Indexes
* Replication
* Partitioning / Sharding

## 7. Cache

* What to cache?
* Cache strategy
* TTL
* Invalidation

## 8. Messaging

* Kafka / RabbitMQ / Service Bus
* Producer
* Consumer
* Sync vs Async
* Retry
* Duplicate messages

## 9. Scalability

* Horizontal scaling
* Load balancing
* Read replicas
* Sharding
* Caching
* Queue-based scaling

## 10. Reliability

* Retry
* Timeout
* Circuit Breaker
* Idempotency
* Failover
* Health checks

## 11. Availability

* Multiple instances
* Multiple AZ/regions
* Database replication
* Automatic failover

## 12. Security

* Authentication
* Authorization
* HTTPS/TLS
* Encryption
* Rate limiting
* Input validation
* Secrets

## 13. Observability

* Logs
* Metrics
* Tracing
* Alerts
* Correlation/Trace ID

## 14. Failure Scenarios

Ask:

* What if DB fails?
* What if cache fails?
* What if service fails?
* What if queue fails?
* What if external API fails?
* What if traffic becomes 10×?

## 15. Trade-offs

For every major decision:

**Option A vs Option B**
→ Why did we choose this?

## 16. Final Architecture

```text
Client
  ↓
DNS / CDN
  ↓
Load Balancer
  ↓
API Gateway
  ↓
Services
  ↓
Cache / Database
  ↓
Message Queue
  ↓
Workers
  ↓
External Services
```

## 17. Interview Questions

Always be ready to explain:

* Why this architecture?
* Why this database?
* Why cache?
* Why queue?
* How does it scale?
* How does it handle failures?
* How do you maintain consistency?
* What are the bottlenecks?
* What are the trade-offs?
* How would you handle 10× traffic?

---

# HLD MEMORY FORMULA

**R → E → API → A → D → C → M → S → R → A → S → O → F → T**

### Remember:

**R** = Requirements
**E** = Estimation
**API** = APIs
**A** = Architecture
**D** = Data Flow / Database
**C** = Cache
**M** = Messaging
**S** = Scalability
**R** = Reliability
**A** = Availability
**S** = Security
**O** = Observability
**F** = Failure Scenarios
**T** = Trade-offs

### Simple Mental Model

**What are we building?**
→ Requirements

**How big is it?**
→ Estimation

**How do clients communicate?**
→ APIs

**How is everything connected?**
→ Architecture

**Where is data stored?**
→ Database

**How do we make it fast?**
→ Cache

**How do services communicate asynchronously?**
→ Queue

**How do we handle more traffic?**
→ Scalability

**What happens when things fail?**
→ Reliability

**How do we keep it running?**
→ Availability

**How do we protect it?**
→ Security

**How do we know something is wrong?**
→ Observability

**What can go wrong?**
→ Failure scenarios

**Why did we choose this design?**
→ Trade-offs
