# URL Shortener — Step 9: Reliability and Failure Scenarios

# 1. Reliability Goals

System should continue functioning despite individual component failures.

Important techniques:

```text
Timeout
Retry
Circuit Breaker
Failover
Replication
Health Checks
Idempotency
Dead Letter Queue
```

---

# 2. Application Server Failure

```text
Server 1 ❌

Server 2 ✅
Server 3 ✅
```

Load balancer stops sending traffic to Server 1.

Because services are stateless, requests continue through healthy instances.

---

# 3. Redis Failure

```text
Redis ❌
   ↓
Database
   ↓
Return URL
```

System remains available but:

```text
Database load ↑
Latency ↑
```

Mitigation:

```text
Redis replication
Redis cluster
Automatic failover
Monitoring
```

---

# 4. Database Failure

Existing cached URLs may still work:

```text
Redis
 ↓
Original URL
 ↓
302
```

But cache misses cannot be resolved.

Mitigation:

* Database replication
* Automatic failover
* Multi-AZ
* Backups
* Disaster recovery

---

# 5. Queue Failure

Analytics should not break the redirect path.

Important principle:

```text
Analytics failure
       ≠
Redirect failure
```

Depending on architecture, events can be buffered/retried.

---

# 6. External Service Failure

If an external service is involved:

```text
Timeout
Retry
Circuit Breaker
Fallback
```

Do not retry indefinitely.

---

# 7. Timeout

Every network dependency should have a timeout.

Example:

```text
Redis timeout
Database timeout
External API timeout
```

Why?

To avoid requests waiting indefinitely.

---

# 8. Retry

Retry only transient failures.

Retry examples:

```text
Timeout
503
Temporary network failure
```

Do not normally retry:

```text
400
401
403
Validation failure
```

---

# 9. Exponential Backoff

Instead of:

```text
Retry immediately
Retry immediately
Retry immediately
```

use:

```text
100 ms
200 ms
400 ms
800 ms
```

with jitter where appropriate.

---

# 10. Circuit Breaker

If a dependency repeatedly fails:

```text
Service A
   ↓
Dependency ❌
```

Circuit breaker opens and prevents continuous calls.

States:

```text
Closed
  ↓
Open
  ↓
Half-Open
```

---

# 11. Idempotency

Important for URL creation.

```text
Idempotency-Key: ABC123
```

Repeated request should not create multiple resources unintentionally.

---

# 12. Duplicate Short Code

Database must enforce:

```text
UNIQUE(ShortCode)
```

If collision occurs:

```text
Generate another code
```

or retry ID generation depending on the chosen approach.

---

# 13. Cache Stampede

Popular key expires:

```text
Thousands of requests
        ↓
Cache MISS
        ↓
Database overload
```

Mitigation:

* Request coalescing
* Lock
* Early refresh
* Randomized TTL

---

# 14. Message Duplication

Message may be processed twice.

Solution:

```text
eventId
+
idempotent consumer
```

---

# 15. Dead Letter Queue

Repeatedly failed messages:

```text
Queue
 ↓
Consumer
 ↓
Retry
 ↓
Retry
 ↓
DLQ
```

Operations team can inspect/reprocess them.

---

# 16. Disaster Recovery

## RPO

Maximum acceptable data loss.

Example:

```text
RPO = 5 minutes
```

## RTO

Maximum acceptable recovery time.

Example:

```text
RTO = 30 minutes
```

---

# 17. Backup

Use:

* Automated database backups
* Point-in-time recovery where supported
* Cross-region backup where required
* Restore testing

---

# 18. Reliability Interview Questions

### What if Redis goes down?

Fallback to database.

### What if DB goes down?

Use replicas/failover; cached URLs may continue working.

### What if one server crashes?

Load balancer routes to healthy instances.

### What if queue processing fails?

Retry → DLQ.

### What if request is repeated?

Idempotency.

### What if a popular cache expires?

Prevent cache stampede.

---

# 19. Important Principle

A good HLD does not assume:

```text
Everything works.
```

Instead ask:

```text
What happens when X fails?
```

For every major component.
