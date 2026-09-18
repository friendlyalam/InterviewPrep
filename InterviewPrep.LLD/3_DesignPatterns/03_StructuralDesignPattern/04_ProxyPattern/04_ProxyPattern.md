1. Category

Proxy is a Structural Design Pattern.

-------------------------------------------------------------------------------------------------

2. Definition

Proxy provides a substitute or representative object that controls access to another object.

In simple terms:

Client
  ↓
Proxy
  ↓
Real Object

The client thinks it is communicating with the real object, but the Proxy gets control first.

-------------------------------------------------------------------------------------------------

3. Why Do We Need a Proxy?

Suppose we have an expensive operation:

GenerateLargeReport()

Every time the client asks for the report, we might have to perform expensive processing.

Without Proxy:

Client
  ↓
RealReportService
  ↓
Expensive operation

With Proxy:

Client
  ↓
ReportProxy
  │
  ├── Is it already cached?
  │       │
  │      YES ──► Return cached result
  │
  └── NO
       ↓
   RealReportService
       ↓
   Cache result

The Proxy controls access to the real object.

-------------------------------------------------------------------------------------------------

4. Real-Life Examples
Security
User
 ↓
Security Proxy
 ↓
Actual Resource

The Proxy checks whether the user is authorized.

Caching
Client
 ↓
Cache Proxy
 ↓
Real Service
Lazy Loading
Client
 ↓
Proxy
 ↓
Load expensive object only when required
Remote Objects
Client
 ↓
Proxy
 ↓
Remote Service

-------------------------------------------------------------------------------------------------
5. Our Project

We'll use a different domain again.

Product Catalog Image Service

Imagine an e-commerce application where product images are expensive to retrieve from an external image server.

We'll build:

Product Image Request
        ↓
ImageServiceProxy
        ↓
Check cache
   ┌────┴────┐
   │         │
 cached    not cached
   │         │
   ▼         ▼
 Return   RealImageService
              │
              ▼
          Image data

This demonstrates the caching Proxy very clearly.

6. Project Structure

-------------------------------------------------------------------------------------------------

We'll keep it compact:

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

That's 6 files, which is actually better here.

We don't need to artificially create 8 files just to reach a number.

-------------------------------------------------------------------------------------------------

7. Architecture
                   Client
                     │
                     ▼
             IProductImageService
                     │
                     ▼
             ProductImageProxy
                     │
             ┌───────┴────────┐
             │                │
          Cache?             No
             │                │
            YES               ▼
             │        ProductImageService
             │                │
             └───────◄────────┘
                     │
                     ▼
               ProductImage

  -------------------------------------------------------------------------------------------------
8. Core Idea

The client will use:

IProductImageService

It won't know whether it is communicating with:

ProductImageService

or:

ProductImageProxy

Both implement:

IProductImageService

That's a very important characteristic of Proxy.

-------------------------------------------------------------------------------------------------

9. Proxy vs Decorator

This is extremely important because you've already completed Decorator.

They look similar:

Client
 ↓
Wrapper
 ↓
Real Object

But their intent is different.

Decorator

Adds behavior.

Service
 ↓
LoggingDecorator
 ↓
CachingDecorator
 ↓
Service

Intent:

Enhance the object's behavior.

Proxy

Controls access.

Client
 ↓
Proxy
 ↓
Real Object

Intent:

Control access to the object.

A proxy may implement:

authorization
caching
lazy loading
remote access
Interview shortcut

Decorator adds responsibilities; Proxy controls access.

-------------------------------------------------------------------------------------------------

10. Proxy vs Facade

You just completed Facade too.

Facade

Simplifies a complex subsystem.

Client
 ↓
Facade
 ↓
A + B + C + D
Proxy

Represents one underlying object.

Client
 ↓
Proxy
 ↓
Real Object
Remember

Facade simplifies. Proxy controls.

-------------------------------------------------------------------------------------------------

11. Advantages
Security

Access can be checked before reaching the real object.

Caching

Avoid expensive repeated operations.

Lazy Loading

Create/load expensive resources only when needed.

Remote Access

Hide communication with remote resources.

Logging/Monitoring

The Proxy can observe calls before forwarding them.

-------------------------------------------------------------------------------------------------

12. Disadvantages
Additional Complexity

You introduce another layer.

Extra Indirection

The call becomes:

Client
 ↓
Proxy
 ↓
Real Object
Potential Performance Cost

If the Proxy performs expensive processing itself, it can add overhead.

Overuse

A Proxy isn't justified when direct access is already simple and safe.

-------------------------------------------------------------------------------------------------

13. When to Use

Use Proxy when you need:

✅ Access control
✅ Caching
✅ Lazy initialization
✅ Remote access
✅ Logging/monitoring
✅ Expensive object protection

-------------------------------------------------------------------------------------------------
14. When NOT to Use

Don't use it when:

❌ Direct access is already sufficient
❌ No access-control/caching/lazy-loading requirement exists
❌ The proxy adds complexity without value

-------------------------------------------------------------------------------------------------
15. Important .NET Examples

You may encounter Proxy concepts in:

Entity Framework Core

Lazy-loading proxies can represent entities and load related data when required.

ASP.NET Core

Various interception/delegation mechanisms can provide proxy-like behavior.

DispatchProxy

.NET provides:

System.Reflection.DispatchProxy

which can dynamically create proxy implementations for interfaces.

Distributed systems

A client-side proxy can hide remote service communication:

Client
 ↓
Service Proxy
 ↓
HTTP/gRPC
 ↓
Remote Service

-------------------------------------------------------------------------------------------------

16. Product-Company Interview Scenario
Interviewer:

"We have an expensive external product-image API. Every request for the same product downloads the image again. How would you improve it?"

A good answer:

"I could introduce a caching proxy around the image service. The client would continue depending on the service abstraction.
The proxy would check the cache first and call the real image service only on a cache miss."

Architecture:

Client
  ↓
IProductImageService
  ↓
Caching Proxy
  ↓
Cache
  │
  ├── Hit → return
  │
  └── Miss
       ↓
   Real Service

That's a strong practical use case.

-------------------------------------------------------------------------------------------------


17. Implementation Sequence

We'll now implement the project in small steps:

Step 1
IProductImageService
ProductImage
Step 2
ProductImageService
Step 3
ProductImageProxy
Step 4
Dependency Injection
Step 5
Program.cs
Step 6

Run:

First request  → Cache MISS → Real service
Second request → Cache HIT  → Proxy returns cached data
Step 7

Interview section

Including:

Proxy vs Decorator
Proxy vs Facade
Proxy vs Adapter
Real .NET examples
caching proxy
security proxy
lazy proxy
remote proxy
senior-level design questions