# URL Shortener — Step 7: Messaging and Async Processing

# 1. Why Messaging?

Redirect should be extremely fast.

Analytics does not need to block the redirect.

Therefore:

```text
Redirect
   ↓
Publish Event
   ↓
Return Redirect
```

Analytics is processed separately.

---

# 2. Architecture

```text
                 Redirect Service
                       │
                       ▼
                 Message Queue
                       │
                       ▼
               Analytics Worker
                       │
                       ▼
               Analytics Database
```

---

# 3. Click Event

Example:

```json
{
  "eventId": "evt-123",
  "shortCode": "aB92xK",
  "timestamp": "2026-09-23T10:00:00Z",
  "country": "IN",
  "device": "mobile",
  "browser": "Chrome",
  "referrer": "google.com"
}
```

---

# 4. Producer

```text
Redirect Service
```

publishes:

```text
ClickEvent
```

---

# 5. Consumer

```text
Analytics Worker
```

consumes:

```text
ClickEvent
```

and stores analytics.

---

# 6. Why Asynchronous?

Without queue:

```text
Redirect
 ↓
Save Analytics
 ↓
Redirect User
```

This increases latency.

With queue:

```text
Redirect
 ↓
Publish Event
 ↓
Redirect User

Queue
 ↓
Worker
 ↓
Analytics DB
```

This decouples analytics from the critical path.

---

# 7. At-Least-Once Delivery

A common practical approach is:

```text
At-least-once
```

The same event may be delivered more than once.

Therefore consumers should be idempotent.

---

# 8. Deduplication

Each event has:

```text
eventId
```

Example:

```text
evt-123
```

Consumer can detect:

```text
Already processed?
```

If yes:

```text
Ignore duplicate.
```

---

# 9. Retry

If analytics processing fails:

```text
Retry
   ↓
Retry
   ↓
Retry
```

Use:

```text
Exponential Backoff
```

---

# 10. Dead Letter Queue

If processing repeatedly fails:

```text
Message Queue
      ↓
Consumer
      ↓
Failure
      ↓
Retry
      ↓
Retry
      ↓
DLQ
```

DLQ allows investigation and reprocessing.

---

# 11. Kafka vs RabbitMQ

For interview discussion:

### Kafka

Useful for:

* High-throughput event streaming
* Large event volumes
* Replay
* Multiple consumers

### RabbitMQ

Useful for:

* Traditional message queues
* Task distribution
* Routing
* Work queues

For extremely high-volume analytics/event streaming, Kafka is a natural option to discuss.

---

# 12. Azure Mapping

Possible choices:

```text
Azure Service Bus
Azure Event Hubs
```

Use the technology based on whether the requirement is primarily queueing or high-volume event streaming.

---

# 13. Interview Questions

### Why not save analytics synchronously?

It increases redirect latency.

### What if analytics fails?

Redirect should continue; analytics can be retried asynchronously.

### What if the same event arrives twice?

Use event ID and idempotent processing.

### What if a message cannot be processed?

Retry and eventually send it to DLQ.

### Why queue?

Decoupling, buffering, scalability, and asynchronous processing.
