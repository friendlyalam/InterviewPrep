IProductImageService
                    ▲
                    │
          ┌─────────┴─────────┐
          │                   │
          ▼                   ▼
ProductImageService    ProductImageProxy

This is important because the client doesn't need to know which implementation it receives.

3. Why Interface?

Suppose Program.cs directly depends on:

ProductImageService

Then replacing it with:

ProductImageProxy

would require changing the client.

Instead:

IProductImageService

allows us to transparently substitute:

Real Service
     OR
Proxy

The client doesn't care.

That's exactly what makes the Proxy useful.

4. The Future Flow

The client will eventually call:

var image = await imageService.GetImageAsync(101);

But the actual object will be:

IProductImageService
        │
        ▼
ProductImageProxy
        │
        ├── Cache HIT
        │      ↓
        │   Return image
        │
        └── Cache MISS
               ↓
       ProductImageService
               ↓
          Return image

The client won't know that a proxy is sitting in between.

5. Why Task<ProductImage>?

We're simulating an external/expensive operation.

In a real application, the real image service might call:

HTTP API
Database
Object Storage
CDN
Remote service

Those operations are naturally asynchronous in .NET.

So:

Task<ProductImage>

makes the example more realistic than a synchronous fake.


------------------------------------------------------------------------


2. What Happens?
First request
GetImageAsync(101)
        ↓
ProductImageProxy
        ↓
Cache MISS
        ↓
ProductImageService
        ↓
Wait 1 second
        ↓
Return image
        ↓
Store in cache

Expected:

First request:
[PROXY] Cache miss for product 101.
[REAL SERVICE] Fetching image for product 101...
[PROXY] Image cached for product 101.
3. Second Request

Now:

await imageService.GetImageAsync(101);

The Proxy already has product 101.

GetImageAsync(101)
        ↓
Proxy
        ↓
Cache HIT
        ↓
Return cached image

Expected:

Second request for the same product:
[PROXY] Cache hit for product 101.

Notice:

[REAL SERVICE] Fetching...

does not appear.

That's our proof that the Proxy is working.

4. Third Request

We request:

await imageService.GetImageAsync(202);

Product 202 isn't cached.

Therefore:

Proxy
 ↓
MISS
 ↓
Real Service
 ↓
Cache
 ↓
Client

Expected:

Request for another product:
[PROXY] Cache miss for product 202.
[REAL SERVICE] Fetching image for product 202...
[PROXY] Image cached for product 202.
5. Final Project Structure ✅
13_ProxyPattern
│
├── Interfaces
│   └── IProductImageService.cs
│
├── Services
│   ├── ProductImageService.cs
│   └── ProductImageProxy.cs
│
├── Models
│   └── ProductImage.cs
│
├── DependencyInjection
│   └── ServiceCollectionExtensions.cs
│
└── Program.cs

6 files — complete.

We deliberately didn't add unnecessary classes just to increase the file count.

6. Complete Architecture
                         Client
                           │
                           ▼
                 IProductImageService
                           │
                           ▼
                ┌─────────────────────┐
                │ ProductImageProxy   │
                │                     │
                │ Check Cache         │
                └──────────┬──────────┘
                           │
                ┌──────────┴──────────┐
                │                     │
             Cache HIT            Cache MISS
                │                     │
                │                     ▼
                │          ProductImageService
                │                     │
                │                     ▼
                │               External API
                │                     │
                └──────────┬──────────┘
                           ▼
                    ProductImage


 --------------------------------------------------------------------------------------

 Proxy Pattern — Interview & Product-Company Section

Now let's finish the 13th and final pattern with the interview-level material.

1. Best Definition to Give

If the interviewer asks:

What is the Proxy Pattern?

Say:

"Proxy is a structural design pattern that provides a substitute object which controls access to a real object while maintaining the same interface."

Then give the example:

"For example, a caching proxy can check whether data is already cached before calling an expensive service."

That's a strong 20–30 second answer.

2. The Three Main Participants

Remember these:

        Client
          │
          ▼
      Subject
       ▲    ▲
       │    │
    Proxy   Real Subject

In our project:

Client
  │
  ▼
IProductImageService
  ▲
  │
  ├── ProductImageProxy
  │
  └── ProductImageService
3. Why Does Proxy Implement the Same Interface?

Because the client should not need to know whether it is talking to the Proxy or the real object.

IProductImageService imageService;

could point to:

ProductImageProxy

or:

ProductImageService

This gives us substitutability.

4. Proxy vs Decorator

🔥 Very important interview question.

They often look almost identical structurally.

Proxy
Client
  ↓
Proxy
  ↓
Real Object

Purpose:

Control access.

Examples:

Authorization
Lazy loading
Remote access
Caching
Protection
Decorator
Client
  ↓
Decorator
  ↓
Real Object

Purpose:

Add responsibilities/behavior.

Examples:

Logging
Metrics
Compression
Additional validation
Best interview answer

"Proxy and Decorator can have similar structures, but their intent differs. A Proxy controls access to an object, whereas a Decorator enhances an object's behavior by adding responsibilities."

5. Proxy vs Adapter

Another important comparison.

Adapter

Makes incompatible interfaces work together.

Client
 ↓
Adapter
 ↓
Incompatible API

Example:

Our application
      ↓
PaymentAdapter
      ↓
Third-party payment API
Proxy

The interface is already compatible.

Client
 ↓
Proxy
 ↓
Real Object
Easy rule

Adapter changes the interface. Proxy preserves the interface.

6. Proxy vs Facade

You already learned Facade.

Facade

Simplifies several components:

Client
   ↓
Facade
   ↓
Service A
Service B
Service C
Service D
Proxy

Controls access to one underlying object:

Client
   ↓
Proxy
   ↓
Real Object
Easy rule

Facade simplifies a subsystem; Proxy controls access to an object.

7. Proxy vs Decorator vs Adapter vs Facade

Memorize this table:
| Pattern       | Main Purpose       |
| ------------- | ------------------ |
| **Proxy**     | Control access     |
| **Decorator** | Add behavior       |
| **Adapter**   | Convert interface  |
| **Facade**    | Simplify subsystem |

This is one of the most useful four-pattern comparisons for your interviews.


8. Scenario Question
Interviewer:

"We need to prevent unauthorized users from accessing a service. Which pattern?"

Good answer:

Proxy.

Client
  ↓
Authorization Proxy
  ↓
Real Service

The Proxy checks:

Authorized?
   │
 ┌─┴─┐
YES  NO
 │    │
 ▼    ▼
Real  Reject
Service
9. Scenario — Caching
Interviewer:

"A service is expensive and repeatedly receives the same request. What pattern could you use?"

Answer:

A caching Proxy can sit in front of the service and return cached results before invoking the expensive operation.

Exactly what we implemented.

10. Scenario — Logging
Interviewer:

"You need to add logging around an existing service without modifying it."

Don't automatically answer Proxy.

A better answer:

"Decorator would generally be a better fit if the goal is simply to add logging behavior. Proxy would be more appropriate if the logging is part of controlling/intercepting access to the underlying object."

This demonstrates that you understand intent, rather than simply matching keywords.

11. Scenario — Remote Service

Suppose:

Application
    ↓
OrderServiceProxy
    ↓
HTTP/gRPC
    ↓
Remote Order Service

The Proxy hides the remote communication details.

This is commonly called a Remote Proxy.

12. Scenario — Lazy Loading

Suppose an object is expensive to create:

Client
 ↓
Proxy
 ↓
Object doesn't exist yet
 ↓
Create object only now

This is a Virtual Proxy / lazy-loading style.

13. Types of Proxy

You don't need to memorize dozens, but know these common forms:

1. Virtual Proxy

Delays creation of an expensive object.

2. Protection Proxy

Controls authorization/access.

3. Remote Proxy

Represents an object in another process/system.

4. Caching Proxy

Caches results.

5. Logging/Monitoring Proxy

Intercepts calls for observability.

14. Senior-Level Question
"Would you use an in-memory dictionary as a production cache?"

Answer:

"Not necessarily. For a single-instance application it may be sufficient for a simple local cache, but in a horizontally scaled application each instance would have its own dictionary. If the data needs to be shared across instances, I'd consider a distributed cache such as Redis. I'd also need to consider TTL, eviction, invalidation, memory limits and cache stampede behavior."

🔥 This is the kind of answer that takes you beyond textbook pattern knowledge.

15. Cache Stampede

Suppose:

Cache expires
     ↓
100 requests arrive
     ↓
All see CACHE MISS
     ↓
100 calls to real service

That's a cache stampede.

A production caching Proxy might need:

locking/single-flight
TTL jitter
background refresh
stale-while-revalidate
distributed locking where appropriate

You don't need to implement these in our small pattern project, but you should know the trade-off.

16. Thread Safety

Our demo contains:

Dictionary<int, ProductImage>

If multiple requests access the same Proxy concurrently, a normal Dictionary isn't appropriate for unsynchronized concurrent access.

A production implementation might use:

ConcurrentDictionary<TKey, TValue>

or, more commonly, an actual caching abstraction such as:

IMemoryCache
IDistributedCache
Redis

depending on the architecture.

17. SOLID Connection
Dependency Inversion

Client depends on:

IProductImageService

rather than:

ProductImageService
Open/Closed

We can introduce:

ProductImageProxy

without modifying the real service.

Single Responsibility

The real service retrieves images.

The Proxy manages access/caching.

18. 20-Minute Interview Explanation

If the interviewer says:

"Show me a design pattern you've used."

You can explain our project like this:

Minute 1–2 — Problem

"We have an expensive product-image service. Repeated requests for the same product cause unnecessary calls."

Minute 3–5 — Design

Draw:

Client
  ↓
IProductImageService
  ↓
Proxy
  ↓
Real Service
Minute 6–10 — Code

Show:

public sealed class ProductImageProxy
    : IProductImageService

Then:

if (_cache.TryGetValue(productId, out ...))
{
    return cachedImage;
}

var image = await _realService.GetImageAsync(productId);

_cache[productId] = image;

return image;
Minute 11–13 — DI

Explain:

IProductImageService
        ↓
Proxy
        ↓
Real Service
Minute 14–16 — Trade-offs

Discuss:

Cache consistency
TTL
memory
distributed cache
concurrency
Minute 17–20 — Comparisons

Be prepared for:

Proxy vs Decorator
Proxy vs Adapter
Proxy vs Facade
Proxy vs Strategy
19. Product-Company Questions

Make sure you can answer these:

What is Proxy Pattern?
Why is Proxy structural?
Why does Proxy implement the same interface?
Proxy vs Decorator?
Proxy vs Adapter?
Proxy vs Facade?
What is a Protection Proxy?
What is a Virtual Proxy?
What is a Remote Proxy?
How would you implement caching with Proxy?
How would you make the cache thread-safe?
Would an in-memory cache work with multiple servers?
How would Redis change the design?
How would you handle cache expiration?
What happens if the real service is unavailable?
How would you test the Proxy?
What are the disadvantages of Proxy?
When would you avoid Proxy?
How does Proxy relate to SOLID?
Where have you seen Proxy-like behavior in .NET?
🎯 DESIGN PATTERNS — COMPLETE

You have now completed your 13-pattern target:

Creational — 4
✅ Abstract Factory
✅ Singleton
✅ Builder
✅ Factory
Behavioral — 5
✅ Strategy
✅ Observer
✅ Command
✅ Mediator
✅ Chain of Responsibility
Structural — 4
✅ Decorator
✅ Facade
✅ Adapter
✅ Proxy
13 / 13 ✅

