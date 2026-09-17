# Order Processing

## 1. Definition

**Order processing** is the end-to-end workflow that takes an order from creation through validation, payment, inventory, fulfillment, and completion.

Typical flow:

```text
Customer
   |
   ↓
Create Order
   |
   ↓
Validate Order
   |
   ↓
Reserve Inventory
   |
   ↓
Process Payment
   |
   ↓
Confirm Order
   |
   ↓
Create Shipment
   |
   ↓
Deliver Order

In a distributed system, order processing must handle:

Concurrency
Duplicate requests
Retries
Failures
Timeouts
Payment consistency
Inventory consistency
Idempotency
Event processing
Transactions
Distributed workflows
2. Typical Order Lifecycle

A practical order state machine might be:

Pending
   ↓
Confirmed
   ↓
PaymentProcessing
   ↓
Paid
   ↓
Processing
   ↓
Shipped
   ↓
Delivered

Failure/cancellation paths:

Pending ─────────→ Cancelled
PaymentProcessing → PaymentFailed
Paid ────────────→ RefundPending
Processing ──────→ Cancelled

Do not allow arbitrary state transitions.

For example:

Delivered → Pending

should normally be invalid.

3. Order Processing Architecture

A typical microservices architecture:

                         ┌─────────────────┐
                         │    Client       │
                         └────────┬────────┘
                                  ↓
                         ┌─────────────────┐
                         │ API Gateway     │
                         └────────┬────────┘
                                  ↓
                         ┌─────────────────┐
                         │ Order Service   │
                         └────────┬────────┘
                                  │
              ┌───────────────────┼───────────────────┐
              ↓                   ↓                   ↓
       Inventory Service   Payment Service     Customer Service
              │                   │
              ↓                   ↓
       Inventory DB        Payment Provider

                         Order Service
                              |
                              ↓
                       Message Broker
                              |
              ┌───────────────┼───────────────┐
              ↓               ↓               ↓
        Shipping Service Notification     Analytics

The exact architecture depends on scale and business requirements.

4. Create Order

Client:

POST /api/orders

Request:

{
  "customerId": 1001,
  "items": [
    {
      "productId": 501,
      "quantity": 2
    }
  ]
}

For an operation that may be retried, use an idempotency key:

Idempotency-Key: 7c9d5a1e-...

The server should ensure that retrying the same logical request does not create another order.

5. Why Idempotency Is Important

Consider:

Client
   |
   | POST /orders
   ↓
Order Service
   |
   | Order created
   ↓
Network failure

The client does not know whether the order was created.

It retries:

POST /orders
Idempotency-Key: ABC123

Without idempotency:

Order #1001
Order #1002

With idempotency:

ABC123
   ↓
Existing Order #1001
   ↓
Return Order #1001

Important:

A timeout does not necessarily mean the operation failed.

6. Order Database

A simplified Orders table:

Orders
--------------------------------
Id
CustomerId
Status
TotalAmount
Currency
CreatedAt
UpdatedAt

OrderItems:

OrderItems
--------------------------------
Id
OrderId
ProductId
Quantity
UnitPrice
TotalPrice

Possible constraints:

Orders.Id → Primary Key

OrderItems.Id → Primary Key

OrderItems.OrderId → Foreign Key

Business-specific uniqueness constraints may also be required.

7. Never Trust Client Price

The client might send:

{
  "productId": 501,
  "quantity": 2,
  "unitPrice": 100
}

Do not blindly trust:

unitPrice = 100

The server should retrieve authoritative pricing:

Client
  ↓
Product/Pricing Service
  ↓
Current price
  ↓
Calculate order total

Otherwise a malicious client could send:

unitPrice = 1

for a product that costs 10,000.

8. Order Validation

Typical validation:

Customer exists
Customer is allowed to order
Products exist
Products are active
Quantity > 0
Quantity within allowed limits
Price is valid
Currency is valid
Address is valid
Order total is valid

Separate:

Validation failure

from:

Unexpected system failure

Validation failures should not normally be retried.

9. Inventory Reservation

Suppose inventory is:

Product 501
Available = 10

Customer orders:

Quantity = 3

The system needs to reserve:

3 units

Remaining available quantity:

10 - 3 = 7

But this becomes a concurrency problem when multiple customers order simultaneously.

10. Inventory Race Condition

Suppose:

Available = 10

Two requests:

Customer A → quantity 8
Customer B → quantity 8

Naive implementation:

A reads 10
B reads 10

A decides 10 >= 8
B decides 10 >= 8

A updates → 2
B updates → 2

Result:

16 units promised
10 units available

This is an overselling problem.

11. Atomic Inventory Update

A better database operation can make the check and update atomic.

Conceptually:

UPDATE Inventory
SET AvailableQuantity = AvailableQuantity - @quantity
WHERE ProductId = @productId
  AND AvailableQuantity >= @quantity;

Then:

Rows affected = 1
    → Reservation succeeded

Rows affected = 0
    → Insufficient inventory or conflicting state

This is often preferable to:

SELECT quantity
↓
Check in application
↓
UPDATE

because the latter can introduce a race condition.

12. Optimistic Concurrency

Another approach uses a version/concurrency token.

Example:

ProductId = 501
Available = 10
Version = 20

Application reads:

Available = 10
Version = 20

Update:

UPDATE Inventory
SET AvailableQuantity = 7,
    Version = 21
WHERE ProductId = 501
  AND Version = 20;

If:

Rows affected = 1

the update succeeded.

If:

Rows affected = 0

another transaction changed the row.

The application can then reload and handle the conflict.

13. Payment Processing

Typical flow:

Order
  ↓
Payment Service
  ↓
Payment Provider
  ↓
Payment Result

Possible states:

PaymentPending
PaymentProcessing
Paid
PaymentFailed
RefundPending
Refunded

Payment operations should be idempotent.

Example:

OrderId = 1001
PaymentIdempotencyKey = PAY-1001

If the payment request is retried:

PAY-1001
   ↓
Existing payment
   ↓
Return existing result

Do not create another payment.

14. Payment Timeout

Important scenario:

Order Service
      |
      | Payment request
      ↓
Payment Provider
      |
      | Payment succeeds
      ↓
Network timeout
      X

Order Service sees:

Timeout

It must not automatically assume:

Payment failed

The provider may already have charged the customer.

Therefore payment APIs need:

Idempotency
+
Payment status lookup/reconciliation

where supported.

15. Order State vs Payment State

Do not unnecessarily combine all states into one field.

For example:

Order.Status
Payment.Status
Shipment.Status
Inventory.Status

Possible state:

Order = Confirmed
Payment = Paid
Inventory = Reserved
Shipment = Pending

This is more expressive than:

Order.Status = "SomeHugeCombinedState"
16. Transaction Boundaries

A common mistake is trying to put the entire order workflow into one database transaction across multiple services.

For example:

BEGIN TRANSACTION

Create Order
Call Inventory Service
Call Payment Service
Call Shipping Service

COMMIT

This is generally problematic in microservices.

Why?

Long-running transaction
External network calls
Distributed transaction complexity
Lock duration
Failure handling
Reduced scalability

Instead, use appropriate local transactions and distributed workflow patterns.

17. Local Transaction

Within Order Service:

BEGIN TRANSACTION

Create Order
Create OrderItems
Create OutboxEvent

COMMIT

This keeps local database changes atomic.

Then an asynchronous publisher publishes the event.

18. Outbox Pattern

Suppose:

Create Order
Publish OrderCreated

If the application crashes between them:

Order created
Event not published

The Outbox Pattern solves this problem.

Transaction:

BEGIN TRANSACTION

Insert Order
Insert OutboxEvent

COMMIT

Example:

Orders
----------------
1001 | Pending

Outbox
----------------
EventId | OrderCreated | OrderId=1001

A background publisher reads the outbox and publishes the event.

19. Order Processing with Events

Example:

Order Service
      |
      | OrderCreated
      ↓
Message Broker
      |
      ├──→ Inventory Service
      |
      ├──→ Payment Service
      |
      └──→ Notification Service

Each service processes the event independently.

Benefits:

Loose coupling
Scalability
Asynchronous processing
Failure isolation
Independent consumers
20. Message Delivery

Message brokers commonly provide at-least-once delivery semantics.

Therefore:

OrderCreated
OrderCreated

may potentially be delivered more than once.

Consumers should be idempotent.

Example:

ProcessedMessages
-------------------------
MessageId UNIQUE
ProcessedAt

Before processing:

MessageId already processed?

If yes:

Skip duplicate
21. Idempotent Consumer

Typical pattern:

Message
   ↓
MessageId
   ↓
Already processed?
  /       \
Yes       No
 |         |
Skip       ↓
         Process
           ↓
       Mark processed

For strong consistency, processing the message and recording the processed state should generally be coordinated in the same database transaction when possible.

22. Saga Pattern

Order processing often spans multiple services:

Order
Inventory
Payment
Shipping

There is usually no single ACID transaction covering all of them.

A Saga breaks the workflow into local transactions with compensating actions.

Example:

Create Order
    ↓
Reserve Inventory
    ↓
Charge Payment
    ↓
Create Shipment

Failure:

Payment Failed
    ↓
Release Inventory
    ↓
Cancel Order
23. Saga Example

Successful path:

Order Created
      ↓
Inventory Reserved
      ↓
Payment Successful
      ↓
Shipment Created
      ↓
Order Confirmed

Failure path:

Order Created
      ↓
Inventory Reserved
      ↓
Payment Failed
      ↓
Release Inventory
      ↓
Cancel Order

The important idea:

A Saga uses a sequence of local transactions and compensating actions instead of one distributed ACID transaction.

24. Orchestration vs Choreography

Two common Saga styles.

Orchestration

A central orchestrator controls the workflow.

             Order Saga
                 |
       ┌─────────┼─────────┐
       ↓         ↓         ↓
  Inventory   Payment   Shipping

The orchestrator knows:

What happens next
What to do on failure
What compensation is required
Choreography

Services react to events.

OrderCreated
     ↓
Inventory Service
     ↓
InventoryReserved
     ↓
Payment Service
     ↓
PaymentCompleted
     ↓
Shipping Service

There is no central workflow controller.

25. Orchestration vs Choreography
| Aspect              | Orchestration               | Choreography              |
| ------------------- | --------------------------- | ------------------------- |
| Central controller  | Yes                         | No                        |
| Workflow visibility | High                        | Can become difficult      |
| Coupling            | Orchestrator knows services | Services depend on events |
| Complex workflows   | Often easier                | Can become complicated    |
| Debugging           | Usually easier              | Can be harder             |
| Event-driven        | Can still use events        | Core mechanism            |
| Risk                | Orchestrator complexity     | Event-chain complexity    |

For complex business workflows, orchestration is often easier to reason about.

26. Order Cancellation

Cancellation can happen at different stages.

Example:

Pending
   ↓
Cancelled

But after shipping:

Shipped

the system may need:

Return
Refund
Reverse shipment

Therefore cancellation rules must depend on the current state.

Example:

Pending       → Cancel allowed
Paid          → Cancel + Refund
Shipped       → Return process
Delivered     → Return policy
27. Refund Processing

Suppose payment succeeded but order cannot be fulfilled.

Example:

Order Created
     ↓
Inventory Reserved
     ↓
Payment Successful
     ↓
Inventory failure

The system may need:

Release inventory
+
Refund payment
+
Cancel order

Refund itself should be idempotent.

Example:

RefundIdempotencyKey = REFUND-1001

Retrying should not issue multiple refunds.

28. Inventory Reservation vs Inventory Deduction

These are different concepts.

Reservation

Temporarily holds inventory for an order.

Available = 100
Reserved = 10
Deduction

Permanently reduces sellable inventory.

For example:

Order shipped

might cause final inventory deduction.

The exact model depends on business requirements.

29. Reservation Expiration

Suppose a customer reserves inventory but never completes payment.

Reservation:

10:00 AM

Expiration:

10:15 AM

After expiration:

Release reservation

Possible architecture:

Reservation
   ↓
Expiration timestamp
   ↓
Background worker
   ↓
Release inventory

Use reliable background processing rather than relying on an in-memory timer for critical business operations.

30. Concurrency in Order Processing

Important concurrent scenarios:

Two customers buy last item
Two payment requests arrive
Same order is submitted twice
Same message is delivered twice
Cancellation arrives during payment
Payment callback arrives twice
Shipment event is duplicated

Solutions may include:

Idempotency
Optimistic concurrency
Atomic database updates
Unique constraints
Transactions
Queues
State machines
Distributed coordination where appropriate
31. Order State Machine

A state machine prevents invalid transitions.

Example:

Pending
  |
  ├──→ Cancelled
  |
  ↓
Confirmed
  |
  ↓
PaymentProcessing
  |
  ├──→ PaymentFailed
  |
  ↓
Paid
  |
  ↓
Processing
  |
  ↓
Shipped
  |
  ↓
Delivered

Define valid transitions explicitly.

Example:

Pending → Confirmed
Pending → Cancelled

Confirmed → PaymentProcessing

PaymentProcessing → Paid
PaymentProcessing → PaymentFailed

Paid → Processing

Processing → Shipped

Shipped → Delivered
32. Optimistic Concurrency for Order State

Suppose two requests try to modify the same order.

Current:

OrderId = 1001
Status = Paid
Version = 10

Request A:

Paid → Processing

Request B:

Paid → Cancelled

Both read:

Version = 10

Use:

UPDATE Orders
SET Status = @newStatus,
    Version = Version + 1
WHERE Id = @id
  AND Version = @expectedVersion;

Only one update should succeed.

The other detects a concurrency conflict.

33. Unique Constraints

Use database constraints for business invariants where appropriate.

Examples:

Unique Order Number
Unique Payment Reference
Unique Idempotency Key
Unique External Transaction ID
Unique SKU

Do not rely only on application code:

if (!Exists(orderNumber))
{
    Create(orderNumber);
}

Concurrent requests can bypass this check.

The database should enforce uniqueness.

34. Payment Callback / Webhook

Payment providers often send webhooks.

Example:

Payment Provider
      |
      | PaymentSucceeded
      ↓
Your Webhook

The provider may retry the webhook:

Webhook #1
Webhook #2
Webhook #3

Therefore webhook processing must be idempotent.

Use:

ProviderEventId
+
Unique constraint
+
Idempotent state transition

Example:

ProcessedProviderEvents
--------------------------
EventId UNIQUE
35. Webhook State Validation

Do not blindly process:

PaymentSucceeded

Check:

Order exists
Payment belongs to order
Payment is not already completed
Event is valid
Event is not stale
State transition is allowed

Then update the state atomically.

36. Out-of-Order Events

Distributed systems may deliver events out of order.

Example:

PaymentSucceeded
PaymentRefunded

could arrive incorrectly as:

PaymentRefunded
PaymentSucceeded

The system should not blindly apply every event.

Use:

State validation
Version/sequence number
Event timestamp where appropriate
Business rules
Idempotency

Do not rely only on event arrival order unless the infrastructure explicitly guarantees the required ordering.

37. Ordering

Some operations require ordering.

Example:

OrderCreated
    ↓
OrderPaid
    ↓
OrderShipped

You should not process:

OrderShipped

before the order is ready for shipping.

Possible approaches:

Partitioning by OrderId
State validation
Sequence numbers
Workflow orchestration

The correct choice depends on the message infrastructure and business requirements.

38. Retry Strategy

For transient failures:

Request
   ↓
Failure
   ↓
Retry
   ↓
Failure
   ↓
Backoff
   ↓
Retry

Use:

Limited retries
Exponential backoff
Jitter
Timeouts
Circuit breaker
Idempotency

Do not retry:

Invalid input
Unauthorized
Forbidden
Permanent business rule failure

unless there is a specific reason.

39. Circuit Breaker

Suppose Payment Service is unavailable.

Without protection:

Order Service
   ↓
Payment
   ↓
Failure
   ↓
Retry
   ↓
Failure
   ↓
Retry

This can overload the failing service.

Circuit breaker:

Closed
  ↓
Failures exceed threshold
  ↓
Open
  ↓
Fail fast
  ↓
After recovery period
  ↓
Half-Open
  ↓
Success → Closed

This protects the system from repeated calls to an unhealthy dependency.

40. Timeout

Every external dependency should have a reasonable timeout.

Example:

Order Service
     |
     | timeout = 3 sec
     ↓
Payment Service

If the dependency does not respond:

Timeout

The workflow should determine whether to:

Retry
Mark Pending
Compensate
Fail
Queue for later processing

Do not blindly mark payment as failed after a timeout.

41. Background Processing

Some operations do not need to block the HTTP request.

Example:

Order Confirmed
      ↓
Message Broker
      ↓
Notification Worker
      ↓
Send Email/SMS

The order request should not necessarily wait for the email provider.

This improves:

Latency
Resilience
Scalability
42. Channel vs Message Broker
Channel<T>

Useful for:

Process-local
In-memory
Background work
Producer-consumer

But:

Process crash → in-memory work lost
Message Broker

Examples:

Kafka
RabbitMQ
Azure Service Bus
Amazon SQS/SNS

Useful for:

Distributed processing
Durability
Multiple consumers
Cross-service communication
Retry/dead-letter patterns

Choose based on requirements.

43. Dead-Letter Queue

Some messages repeatedly fail.

Example:

OrderCreated
   ↓
Consumer
   ↓
Failure
   ↓
Retry
   ↓
Failure
   ↓
Retry
   ↓
Maximum retries reached
   ↓
Dead-Letter Queue

The message can then be investigated or reprocessed safely.

44. Observability

Order processing needs strong observability.

Track:

OrderId
CorrelationId
TraceId
CustomerId where appropriate
PaymentId
Inventory reservation ID
Message ID
Service name
Event type
Status transition
Duration
Retry count
Failure reason

Distributed tracing helps follow:

API
 ↓
Order Service
 ↓
Inventory
 ↓
Payment
 ↓
Shipping

across services.

45. Correlation ID

Example:

CorrelationId = CORR-12345

Use it across logs and distributed operations where appropriate.

Example:

Order Service
CorrelationId = CORR-12345

Inventory Service
CorrelationId = CORR-12345

Payment Service
CorrelationId = CORR-12345

Shipping Service
CorrelationId = CORR-12345

This makes troubleshooting significantly easier.

46. Security

Order processing must validate:

Authentication
Authorization
Customer ownership
Order access
Payment information
Input validation
Rate limiting
Sensitive data handling

Never expose sensitive payment information in logs.

Avoid logging:

Card number
CVV
Payment credentials
Authentication tokens
47. Order Processing Failure Matrix

| Failure                   | Possible Action                   |
| ------------------------- | --------------------------------- |
| Invalid order             | Reject                            |
| Product unavailable       | Reject/cancel                     |
| Inventory conflict        | Retry/reload/compensate           |
| Payment declined          | Cancel/retry according to rules   |
| Payment timeout           | Check/reconcile status            |
| Shipping unavailable      | Queue/retry                       |
| Duplicate request         | Return existing result            |
| Duplicate message         | Ignore/replay safely              |
| Service timeout           | Retry/fallback/mark pending       |
| Permanent failure         | Fail/compensate                   |
| Consumer repeatedly fails | Dead-letter                       |
| Application crash         | Recover from durable state/events |


48. Complete Example Flow

A realistic order workflow:

Client
  |
  | POST /orders
  | Idempotency-Key
  ↓
Order API
  |
  | Validate request
  ↓
Order Service
  |
  | Create Order + Outbox Event
  ↓
Database Transaction
  |
  ↓
OrderCreated
  |
  ↓
Message Broker
  |
  ├───────────────┐
  ↓               ↓
Inventory       Payment
Service         Service
  |               |
  ↓               ↓
Reserve          Charge
Inventory        Payment
  |               |
  └───────┬───────┘
          ↓
      Order Workflow
          |
          ↓
     Order Confirmed
          |
          ↓
   Shipping Service
          |
          ↓
      Notification
49. Failure Example

Suppose:

Order Created
      ↓
Inventory Reserved
      ↓
Payment Attempt
      ↓
Payment Timeout

Do not immediately assume:

Payment Failed

Instead:

Check payment status
      |
      ├── Paid
      │    ↓
      │ Confirm order
      │
      ├── Failed
      │    ↓
      │ Release inventory
      │
      └── Unknown/Pending
           ↓
        Reconcile later

This is much safer for financial workflows.

50. Complete Enterprise Mental Model

Think about order processing at four levels.

Level 1 — Request Reliability
Idempotency
Timeout
Retry
Cancellation
Level 2 — Data Consistency
Transactions
Unique constraints
Optimistic concurrency
Atomic updates
Level 3 — Distributed Workflow
Events
Queues
Outbox
Saga
Compensation
Idempotent consumers
Level 4 — Operational Reliability
Observability
Circuit breaker
Rate limiting
Dead-letter queue
Reconciliation
Monitoring
Alerting
51. Common Mistakes
Mistake 1: One huge distributed transaction

Avoid trying to keep:

Order
Inventory
Payment
Shipping

inside one long transaction across services.

Mistake 2: No idempotency

Retries can create:

Duplicate orders
Duplicate payments
Duplicate refunds
Duplicate inventory reservations
Mistake 3: Check-then-update inventory

Bad:

SELECT quantity
↓
if quantity >= requested
↓
UPDATE quantity

This can race.

Prefer an atomic database update or appropriate concurrency control.

Mistake 4: Trusting client price

The server should use authoritative pricing.

Mistake 5: Treating timeout as failure

A timeout means:

The caller did not receive a response within the expected time.

It does not prove that the operation did not succeed.

Mistake 6: Ignoring duplicate messages

At-least-once delivery can produce duplicate messages.

Consumers should be idempotent.

Mistake 7: No state machine

Without controlled transitions, invalid states can appear.

Mistake 8: No compensation

If:

Inventory reserved
Payment failed

the inventory reservation may need to be released.

Mistake 9: Unlimited retries

This can create retry storms.

Mistake 10: No observability

Distributed order workflows are difficult to debug without:

CorrelationId
TraceId
OrderId
MessageId
State transition logs
Metrics
Distributed tracing
52. Interview Questions
Q1. How would you design an order processing system?

I would separate order, inventory, payment, and shipping responsibilities. Use local transactions for each service, asynchronous messaging for cross-service workflows, idempotency for retries, optimistic/atomic concurrency control for inventory, and a Saga/outbox approach for distributed consistency.

Q2. How do you prevent duplicate orders?

Use an idempotency key with a shared durable store and a unique database constraint, and make the create operation atomic.

Q3. How do you prevent inventory overselling?

Use an atomic conditional update or appropriate optimistic/pessimistic concurrency control at the database level rather than relying on application-level check-then-act logic.

Q4. What happens if payment succeeds but the response times out?

Do not assume payment failed. Use the payment provider's idempotency mechanism and/or payment-status lookup/reconciliation before deciding the final order state.

Q5. Why use Saga?

Because a business workflow spanning multiple services generally cannot rely on one local ACID transaction. Saga coordinates local transactions and compensating actions.

Q6. What is the Outbox Pattern?

It stores the business state change and the event to be published in the same local database transaction. A publisher later sends the event to the message broker.

Q7. Why do consumers need to be idempotent?

Because messages can be delivered more than once. Idempotent consumers prevent duplicate business effects.

Q8. How do you handle duplicate payment webhooks?

Use the provider's unique event ID with a unique constraint and validate the payment/order state before applying the transition.

Q9. How do you handle payment failure after inventory reservation?

Release or compensate the inventory reservation and transition the order to an appropriate failure/cancelled state.

Q10. What if the inventory service is temporarily unavailable?

Use timeout, bounded retry/backoff where appropriate, and potentially asynchronous processing. Do not keep a long database transaction open while waiting for the service.

Q11. Orchestration vs choreography?

Orchestration uses a central workflow coordinator. Choreography uses events where services react to each other's state changes.

Q12. Why shouldn't you use C# lock for distributed order processing?

lock is process-local. Multiple application instances have independent memory and locks. Business consistency should be enforced through appropriate shared durable mechanisms such as database atomic operations, transactions, constraints, or carefully designed distributed coordination.

Q13. How do you handle cancellation?

Propagate CancellationToken through API, service, repository, and downstream calls where cancellation is meaningful.

Q14. What happens if the same event is delivered twice?

The consumer checks the message/event ID or another idempotency key and ensures the business operation is applied only once.

Q15. What is the role of a state machine?

It explicitly defines valid order states and transitions, preventing invalid business state changes.

53. Product-Company Design Checklist

When asked to design order processing in an interview, cover:

Requirements
    ↓
Order lifecycle
    ↓
Services
    ↓
Database model
    ↓
API design
    ↓
Inventory concurrency
    ↓
Payment idempotency
    ↓
Transactions
    ↓
Outbox
    ↓
Message broker
    ↓
Saga / workflow
    ↓
Retries + timeouts
    ↓
Compensation
    ↓
Duplicate messages
    ↓
State machine
    ↓
Observability
    ↓
Security
    ↓
Scalability
    ↓
Failure scenarios
54. Key Points to Remember
Order processing is a distributed business workflow.

Order, inventory, payment, and shipping should have clear responsibilities.

Never trust client-provided prices.

Validate all order input.

Use idempotency for retryable create/payment operations.

A timeout does not prove that an operation failed.

Use atomic database updates for inventory where appropriate.

Prevent overselling with proper concurrency control.

Use unique constraints for business invariants.

Use local database transactions for local consistency.

Avoid long distributed transactions.

Use the Outbox Pattern for reliable event publication.

Expect duplicate messages in at-least-once systems.

Make consumers idempotent.

Use Saga for multi-service workflows requiring compensation.

Understand orchestration vs choreography.

Use explicit order state transitions.

Payment callbacks/webhooks must be idempotent.

Do not blindly retry payment operations.

Use timeout + retry + backoff + jitter appropriately.

Use circuit breakers to protect unhealthy dependencies.

Use queues for asynchronous/background work.

Use dead-letter queues for repeatedly failing messages.

Propagate CancellationToken.

Use distributed tracing and correlation IDs.

Keep critical business invariants enforced by authoritative durable systems.

C# lock is process-local and is not a replacement for distributed consistency.

Design for failure, not only the happy path.
55. Final Mental Model
                    ORDER REQUEST
                         |
                         ↓
                   Idempotency
                         |
                         ↓
                     Validate
                         |
                         ↓
                   Create Order
                         |
                         ↓
                    Local Tx
                  /           \
                 ↓             ↓
             Order DB       Outbox
                               |
                               ↓
                         Message Broker
                               |
             ┌─────────────────┼─────────────────┐
             ↓                 ↓                 ↓
        Inventory          Payment           Other
             |                 |
             ↓                 ↓
        Concurrency       Idempotency
          Control           + Retry
             |                 |
             └────────┬────────┘
                      ↓
                Workflow/Saga
                      |
             ┌────────┴────────┐
             ↓                 ↓
          Success            Failure
             |                 |
             ↓                 ↓
        Confirm Order      Compensate
             |                 |
             ↓                 ↓
         Shipment         Release/Refund
             |
             ↓
          Delivered
One-Line Definition

Order processing is a distributed business workflow that coordinates order creation, inventory, payment, fulfillment, and delivery while using idempotency, concurrency control, transactions, messaging, retries,
compensation, and observability to remain correct and reliable under failures and duplicate operations.