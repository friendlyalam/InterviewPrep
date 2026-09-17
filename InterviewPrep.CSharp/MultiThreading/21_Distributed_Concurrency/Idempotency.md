# Idempotency

## 1. Definition

**Idempotency** means an operation can be executed **multiple times with the same request/input without causing additional unintended effects after the first successful execution**.

In simple terms:

> **Same request repeated → same intended business result.**

Idempotency is especially important in distributed systems because requests can be retried due to **timeouts, network failures, duplicate messages, client retries, or service failures**.

---

## 2. Why Idempotency Is Important

In distributed systems, a client often cannot know whether a request actually succeeded.

Example:

```text
Client
   |
   | POST /payments
   |
Payment Service
   |
   | Payment processed successfully
   |
   X Network failure

The client does not receive the response.

The client may retry:

POST /payments

Without idempotency:

First request  → Payment created
Retry request  → Another payment created

Result:

Customer charged twice

With idempotency:

First request  → Payment created
Retry request  → Existing result returned

Result:

Customer charged once


3. Idempotent vs Non-Idempotent Operations

| Operation                 | Usually Idempotent? | Reason                                                                              |
| ------------------------- | ------------------: | ----------------------------------------------------------------------------------- |
| `GET /orders/100`         |                 Yes | Reading does not change state                                                       |
| `PUT /users/10`           |                 Yes | Setting the same representation repeatedly gives same state                         |
| `DELETE /users/10`        |       Generally yes | After first deletion, repeated deletion does not create additional deletion effects |
| `POST /orders`            |          Usually No | Each request can create a new order                                                 |
| `POST /payments`          |          Usually No | Each request could create another payment                                           |
| `POST /emails`            |          Usually No | Repeated request may send multiple emails                                           |
| `POST /inventory/reserve` |          Usually No | Repeated request may reserve multiple units                                         |

Important:

HTTP method semantics provide useful defaults, but business-level idempotency still needs explicit design.

4. Simple Example

Suppose:

POST /payments

Request:

{
  "orderId": 1001,
  "amount": 500
}

If the client sends it twice:

Request 1 → Payment #501 created
Request 2 → Payment #502 created

This is not idempotent.

Instead, the client sends:

Idempotency-Key: 8d4c7f2a-1234-4567

Now:

Request 1
Idempotency-Key = 8d4c7f2a-1234-4567
        ↓
Process payment
        ↓
Store result

Retry:

Request 2
Idempotency-Key = 8d4c7f2a-1234-4567
        ↓
Find existing request
        ↓
Return stored result

Only one payment is created.

5. Idempotency Key

An idempotency key is a unique identifier supplied by the client for a logical operation.

Example:

POST /payments
Idempotency-Key: payment-order-1001-request-abc123

The server uses this key to identify duplicate requests.

Typical flow:

Client
   |
   | Request + Idempotency-Key
   ↓
API
   |
   | Check key
   ↓
Already processed?
   |            |
  Yes           No
   |             |
Return result   Process
                 |
                 ↓
            Store result
6. Basic Database Design

A typical idempotency table might contain:

IdempotencyKey
Status
RequestHash
ResponseStatusCode
ResponseBody
CreatedAt
ExpiresAt

Example:

| IdempotencyKey | Status    | Response     |
| -------------- | --------- | ------------ |
| `abc123`       | Completed | Payment #501 |
| `xyz789`       | Completed | Payment #502 |


The database should have a unique constraint on the idempotency key.

Example:

CREATE UNIQUE INDEX UX_Idempotency_Key
ON IdempotencyRecords(IdempotencyKey);

This is important because two requests can arrive concurrently.

7. Why Check-Then-Act Is Dangerous

This is unsafe:

Request A → Check key → Not found
Request B → Check key → Not found

Request A → Process payment
Request B → Process payment

Both requests passed the check.

This is a classic:

Check-then-act race condition

Therefore:

Check
+
Insert

must be protected by an appropriate database constraint/transaction/atomic operation.

Do not rely only on:

if (!repository.Exists(key))
{
    repository.Create(key);
}

Two concurrent requests can both observe false.

8. Unique Constraint

A unique database constraint is one of the most important protections.

Example:

CREATE UNIQUE INDEX UX_Idempotency_Key
ON IdempotencyRecords(IdempotencyKey);

Suppose:

Request A → INSERT abc123
Request B → INSERT abc123

The database guarantees that only one insert succeeds.

This is much safer than relying on an application-level lock.

9. Why C# lock Is Not Enough

This is not a distributed solution:

lock (_lock)
{
    // process request
}

Imagine:

Server 1                    Server 2
   |                           |
   | lock succeeds             | lock succeeds
   |                           |
   ↓                           ↓
Process payment           Process payment

Each server has its own memory and its own lock.

Therefore:

C# lock protects only within the process.

For distributed applications, use shared coordination or, preferably, make the authoritative database/business operation atomic.

10. Idempotency and Database Transactions

A strong design often combines:

Idempotency Key
        +
Database Transaction
        +
Unique Constraint
        +
Business Operation

Conceptually:

BEGIN TRANSACTION

1. Check/create idempotency record
2. Perform business operation
3. Store resulting response
4. Commit

COMMIT

The exact implementation depends on the business operation and database.

11. Payment Example

Suppose:

POST /payments
Idempotency-Key: ABC123

Possible database records:

Idempotency table
Key       Status       PaymentId
ABC123    Completed    5001
Payment table
PaymentId   OrderId   Amount
5001        1001      500

First request:

ABC123 not processed
        ↓
Create payment 5001
        ↓
Store ABC123 → Payment 5001
        ↓
Return 200

Retry:

ABC123 already exists
        ↓
Find Payment 5001
        ↓
Return previous result

No second payment is created.

12. What Should Be Stored?

Depending on the API, the server may store:

Idempotency Key
Request Hash
Processing Status
Resource ID
HTTP Status Code
Response Body
Created Time
Expiration Time

Example:

Key: abc123
RequestHash: 7F81...
Status: Completed
ResourceId: Payment-5001
StatusCode: 200
Response: {...}
CreatedAt: ...
ExpiresAt: ...
13. Why Store Request Hash?

Consider:

Idempotency-Key: ABC123

First request:

{
  "orderId": 1001,
  "amount": 500
}

Later request:

{
  "orderId": 1002,
  "amount": 900
}

Same key, different operation.

This should normally be rejected.

Therefore:

Idempotency-Key
+
Request Hash

can be used to verify that the retry represents the same logical request.

Example:

ABC123 + Hash(Request A)

Retry:

ABC123 + Hash(Request A)

Valid retry.

But:

ABC123 + Hash(Request B)

Different request → reject.

14. Idempotency Status

A request may be:

Started
Processing
Completed
Failed

Example:

ABC123 → Processing

Another request arrives with the same key.

The server must decide what to do.

Possible behavior:

Completed
   → Return stored response

Processing
   → Wait / return conflict / retry later

Failed
   → Retry according to defined rules

The behavior should be explicitly designed.

15. Concurrent Duplicate Requests

This is an important production scenario.

Two identical requests arrive almost simultaneously:

Request A ─────┐
               ├──→ Payment Service
Request B ─────┘

Both contain:

Idempotency-Key = ABC123

Possible sequence:

A → attempts to claim ABC123
B → attempts to claim ABC123

Only one should successfully establish ownership.

Then:

A → Process payment
B → Wait / return in-progress response

After A completes:

ABC123 → Completed → Payment 5001

B can return:

Payment 5001

This requires proper database/concurrency design.

16. Idempotency vs Duplicate Detection

These are related but not identical.

Duplicate detection

Answers:

"Have I seen this request before?"

Idempotency

Answers:

"If this request is repeated, how do I ensure the business effect happens only once?"

Duplicate detection is therefore only one part of an idempotency solution.

17. Idempotency vs Exactly-Once Processing

Do not automatically say:

"Idempotency provides exactly-once processing."

Distributed systems make true exactly-once guarantees difficult.

A better approach is:

At-least-once delivery
        +
Idempotent processing
        =
Effectively-once business outcome

For example:

Message delivered twice
        ↓
Consumer processes same message twice
        ↓
Idempotency prevents duplicate business effect

The message itself may still be delivered more than once.

18. Idempotency in Message Processing

Suppose an Order Service publishes:

OrderCreated

A Payment Service consumes it.

The message broker may deliver the same message twice:

OrderCreated #1001
OrderCreated #1001

Without idempotency:

Payment created
Payment created again

With an idempotent consumer:

MessageId = MSG-1001
        ↓
Check processed messages
        ↓
Already processed?
        ↓
Skip duplicate

A common table is:

ProcessedMessage
----------------
MessageId UNIQUE
ProcessedAt

The unique constraint helps prevent concurrent duplicate processing.

19. Idempotent Consumer Pattern

Typical flow:

Message
   ↓
Extract MessageId
   ↓
Attempt to record MessageId
   ↓
Already exists?
   ├── Yes → Ignore duplicate
   │
   └── No
        ↓
   Process business operation
        ↓
   Commit

For strong consistency, recording the processed message and the business state change should generally be coordinated in the same database transaction when they share the same database.

20. Idempotency and Retries

Retries are one of the biggest reasons idempotency matters.

Example:

Client
  ↓
API
  ↓
Database
  ↓
Operation succeeds
  ↓
Response lost

Client:

Retry

Therefore:

Retries + non-idempotent operation

can create duplicate business effects.

Production design:

Retry
+
Idempotency
+
Timeout
+
Proper error classification

Do not blindly retry every failure.

21. Idempotency and Timeouts

Important distributed-system scenario:

Client → Server

Server processes request for 10 seconds.

Client timeout:

Client: "Request failed"

But server:

Request actually succeeded

Client retries.

Without idempotency:

Payment #1
Payment #2

With idempotency:

Retry → Existing result

Therefore:

A timeout does not necessarily mean the operation failed.

It may mean:

The client does not know the result.

This is one of the most important reasons for idempotency.

22. Idempotency and HTTP

HTTP semantics provide useful guidance:

GET

Generally safe and idempotent.

GET /orders/100

Repeated requests normally produce the same state.

PUT

Generally idempotent.

PUT /users/10
{
    "name": "John"
}

Sending the same request repeatedly should result in the same final state.

DELETE

Generally idempotent.

DELETE /users/10

First request deletes the resource.

Later requests do not delete another resource.

POST

Usually not idempotent.

POST /orders

Repeated requests can create multiple orders.

Therefore POST APIs often use:

Idempotency-Key

for operations such as payments, orders, reservations, and resource creation.

23. Idempotency Does Not Mean "Same Response Every Time"

Idempotency primarily concerns the intended effect on state.

For example:

DELETE /user/10

First request:

204 No Content

Second request might:

404 Not Found

The HTTP responses are different, but the operation may still be considered idempotent because repeating it does not create additional state changes.

Therefore:

Idempotent does not necessarily mean identical response bytes.

24. Idempotency and Side Effects

Be careful with external side effects.

Suppose:

Create Order
   ↓
Send Email
   ↓
Call Payment Provider

Even if order creation is idempotent, external operations may not automatically be.

Example:

Order creation → idempotent
Email sending   → potentially non-idempotent
Payment         → potentially non-idempotent

Each important side effect may require its own strategy.

Possible approaches:

Idempotency key
Unique business identifier
Provider-supported idempotency
Outbox pattern
Message deduplication
Transactional state transition
25. Payment Provider Idempotency

If an external payment provider supports idempotency keys, use them.

Example:

Your API
   |
   | Idempotency-Key = ABC123
   ↓
Payment Provider

Retry:

Same Idempotency-Key = ABC123

The provider can return the existing payment result instead of creating another payment.

This is preferable to assuming your own application's idempotency protection automatically protects the external provider.

26. Idempotency and Outbox Pattern

Consider:

Database Transaction
       ↓
Create Order
       ↓
Publish Event

If the application crashes between these operations:

Order saved
Event not published

The Outbox Pattern helps solve reliable event publication.

Typical design:

BEGIN TRANSACTION

Create Order
Create Outbox Event

COMMIT

A background publisher later sends the event.

But consumers should still be idempotent because messages can be delivered more than once.

Therefore:

Outbox
+
Idempotent Consumer

is a very common production combination.

27. Idempotency vs Optimistic Concurrency

These solve different problems.

| Concept                 | Main Problem                                                     |
| ----------------------- | ---------------------------------------------------------------- |
| Idempotency             | Prevent repeated logical requests from causing duplicate effects |
| Optimistic concurrency  | Detect conflicting updates to the same state                     |
| Pessimistic concurrency | Prevent conflicting access using locks                           |
| Distributed lock        | Coordinate multiple processes/servers                            |
| Unique constraint       | Prevent duplicate values/records                                 |
| Transaction             | Maintain atomicity/consistency across operations                 |

They can be used together.

Example:

Payment API
   ↓
Idempotency key
   ↓
Unique constraint
   ↓
Transaction
   ↓
Optimistic/business-state checks
   ↓
Payment
28. Idempotency vs Distributed Lock

Do not confuse them.

Distributed lock

Answers:

"Who is allowed to perform this work right now?"

Idempotency

Answers:

"Has this logical operation already been applied?"

Example:

Distributed Lock
→ Only one worker processes a scheduled job at a time.

Idempotency
→ If the job/message/request is processed again, the business effect is not duplicated.

In distributed systems, idempotency is often still needed even when a distributed lock exists because locks can expire, fail, or be bypassed during failure scenarios.

29. Idempotency Key Lifetime

Idempotency records should generally have a defined retention period.

Example:

Key = ABC123
CreatedAt = 10:00 AM
ExpiresAt = 24 hours later

After expiration, the key may be removed according to business requirements.

But the retention period should be long enough to cover expected:

Retries
Network delays
Client retry policies
Message redelivery
Operational recovery

Do not choose the TTL arbitrarily.

For high-value operations such as payments, retention and replay behavior should be explicitly defined.

30. Key Generation

The idempotency key should be sufficiently unique.

Common approach:

UUID/GUID

Example:

550e8400-e29b-41d4-a716-446655440000

The client should normally generate a new key for each new logical operation.

Important:

Same logical operation → Same key
New logical operation  → New key

Example:

Payment attempt A → KEY-A
Retry A           → KEY-A

New payment B     → KEY-B
31. Common Mistakes
Mistake 1: Using only in-memory storage
Dictionary<string, string>

This fails in a multi-server environment.

Server 1 → key exists
Server 2 → key does not exist

Use a shared durable/appropriate store when the operation crosses instances.

Mistake 2: Check-then-act
if (!Exists(key))
{
    Create(key);
}

Concurrent requests can both pass the check.

Use:

Unique constraint
+
Atomic operation/transaction
Mistake 3: Assuming retries are safe
Retry POST payment

is dangerous if the operation is not idempotent.

Mistake 4: Using timestamp as the only key

Example:

DateTime.UtcNow

is not a reliable idempotency key.

Use a proper unique identifier.

Mistake 5: Ignoring request differences

Same key with different request data can cause incorrect behavior.

Consider storing a request hash.

Mistake 6: Idempotency only at API layer

If the API calls:

Payment Provider

the external operation must also be protected appropriately.

Mistake 7: Assuming distributed lock solves everything

A lock does not automatically provide:

Idempotency
+
Durability
+
Business correctness
+
Exactly-once execution
Mistake 8: Treating every failure as retryable

Some errors are:

Validation failure
Authorization failure
Business rule failure
Permanent failure

Retrying them can be harmful.

32. Product-Company Scenario
Problem

An e-commerce application receives:

POST /orders

The client may retry because of network timeouts.

Requirements:

1. Customer must not receive duplicate orders.
2. Multiple API servers are running.
3. Requests may arrive concurrently.
4. Database must remain consistent.
5. Retry should return the original order.
Design
Client
   |
   | POST /orders
   | Idempotency-Key: ABC123
   ↓
Load Balancer
   |
   ↓
API Server
   |
   ↓
Shared Database
   |
   ├── IdempotencyRecords
   |
   ├── Orders
   |
   └── Unique Constraints

Flow:

Request
   ↓
Validate idempotency key
   ↓
Atomically establish/lookup key
   ↓
If already completed
   → Return previous order
   ↓
Otherwise
   → Create order
   → Store result
   ↓
Commit transaction
   ↓
Return response
33. Enterprise-Level Idempotency Checklist

When designing an idempotent API, ask:

1. What is the logical operation?
2. What uniquely identifies that operation?
3. Who generates the idempotency key?
4. How long is the key retained?
5. Is the key unique in a shared store?
6. Can two requests arrive concurrently?
7. How is the race prevented?
8. Is the request payload validated against the original?
9. What happens while the first request is processing?
10. What happens after success?
11. What happens after failure?
12. What happens after timeout?
13. Are retries allowed?
14. Which failures are retryable?
15. Are external side effects idempotent?
16. Does the database enforce uniqueness?
17. Are messages delivered at least once?
18. Is the consumer idempotent?
19. Is an outbox required?
20. What happens when the idempotency record expires?
34. Interview Questions
Q1. What is idempotency?

Answer:

Idempotency means repeating the same logical operation does not produce additional unintended business effects after the first successful execution.

Q2. Why is idempotency important in distributed systems?

Because requests can be retried after timeouts or network failures even when the original operation actually succeeded.

Q3. How do you implement idempotency for a POST API?

A common approach is:

Idempotency-Key
+
Shared idempotency store
+
Unique constraint
+
Request validation/hash
+
Atomic transaction/business operation
Q4. Why is a unique database constraint important?

Because concurrent requests can both check for the same key before either one inserts it. A database uniqueness constraint provides authoritative duplicate protection.

Q5. Why isn't a C# lock enough?

Because lock is process-local. In a multi-server deployment, different servers have different locks.

Q6. What happens if the client times out but the server completed the operation?

The client may retry. Idempotency allows the retry to return the previously completed result instead of repeating the business operation.

Q7. Is POST idempotent?

POST is generally not inherently idempotent, but a POST endpoint can be designed to be idempotent using an idempotency key or another business-level mechanism.

Q8. Is DELETE idempotent?

Generally yes with respect to the resource state, because repeating deletion does not create additional deletion effects. The response may differ between attempts.

Q9. Does idempotency mean exactly-once execution?

No.

A distributed system may execute or deliver something more than once. Idempotent processing ensures repeated execution does not create duplicate business effects.

Q10. What is an idempotent consumer?

A message consumer designed so that processing the same message multiple times does not create duplicate business effects.

Q11. What is the relationship between idempotency and retries?

Retries can cause duplicate side effects. Idempotency makes retries safe for operations that would otherwise be non-idempotent.

Q12. What is the difference between idempotency and concurrency control?

Idempotency handles repeated logical operations.

Concurrency control handles multiple operations occurring at the same time and potentially conflicting.

They often work together.

35. Key Points to Remember
Idempotency = repeated logical operation → no duplicate business effect.

Idempotency is critical for distributed systems.

Timeout ≠ operation failed.

POST is generally non-idempotent by default.

Idempotency-Key is commonly used for POST operations.

Same logical operation → same key.

New logical operation → new key.

Use a shared store in multi-server systems.

Use database uniqueness/atomicity to handle concurrent requests.

Check-then-act alone is unsafe.

Request hash can prevent reuse of the same key with different data.

C# lock is process-local.

Distributed lock ≠ idempotency.

Idempotency ≠ exactly-once execution.

At-least-once delivery + idempotent processing can provide an effectively-once business outcome.

External side effects need their own idempotency strategy.

Message consumers should often be idempotent.

Outbox + idempotent consumer is a common distributed-system pattern.

Do not blindly retry every error.

Idempotency records need an intentional retention/expiration strategy.
36. Mental Model

Think of idempotency as:

                 SAME LOGICAL REQUEST
                         |
                         ↓
                 Idempotency Key
                         |
                         ↓
                 Already processed?
                    /           \
                  YES            NO
                   |              |
                   ↓              ↓
             Return stored      Process
                result             |
                                   ↓
                              Store result
                                   |
                                   ↓
                              Return result
One-Line Definition

Idempotency is the ability to safely repeat the same logical operation without causing additional unintended business effects, 
which is essential for reliable retries and duplicate handling in distributed systems.


