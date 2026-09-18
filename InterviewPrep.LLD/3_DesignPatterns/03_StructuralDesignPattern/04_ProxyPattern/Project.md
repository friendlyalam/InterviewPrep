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