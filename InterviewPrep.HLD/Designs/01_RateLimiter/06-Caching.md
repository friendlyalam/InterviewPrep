# URL Shortener — Step 6: Caching

# 1. Why Cache?

Redirect traffic is extremely high.

Example:

```text
~60K redirects/sec peak
```

We do not want every request to hit the database.

Therefore:

```text
Client
  ↓
Redirect Service
  ↓
Redis
```

---

# 2. Cache Technology

Use:

```text
Redis
```

for the application-level cache.

---

# 3. Cache Key

```text
url:{shortCode}
```

Example:

```text
url:aB92xK
```

Value:

```text
https://example.com/products/123
```

---

# 4. Cache-Aside

The main strategy is:

```text
1. Check Redis.
2. If HIT → return URL.
3. If MISS → query database.
4. Store result in Redis.
5. Return URL.
```

---

# 5. Read Flow

```text
                 Redirect Request
                        │
                        ▼
                      Redis
                   ┌────┴────┐
                   │         │
                  HIT       MISS
                   │         │
                   ▼         ▼
                 URL      Database
                             │
                             ▼
                           Redis
                             │
                             ▼
                            URL
```

---

# 6. Why Cache-Aside?

Advantages:

* Simple
* Application controls caching.
* Database remains source of truth.
* Easy to invalidate.

---

# 7. Cache TTL

Example:

```text
TTL = 1 hour
```

Actual TTL depends on:

* URL lifetime
* Update frequency
* Cache capacity
* Traffic pattern

---

# 8. Expiration

If URL expires at:

```text
2027-01-01
```

cache should not keep it active beyond its expiration.

Therefore:

```text
Cache TTL <= URL expiration time
```

when expiration is enforced through cache.

---

# 9. Cache Invalidation

If URL is disabled:

```text
Database
   ↓
IsActive = false
   ↓
Delete Redis key
```

Example:

```text
DEL url:aB92xK
```

---

# 10. Cache Failure

If Redis fails:

```text
Redis ❌
   ↓
Database
   ↓
Return URL
```

System remains functional, but:

```text
Database load ↑
Latency ↑
```

Therefore Redis should have high availability.

---

# 11. Hot Keys

Suppose:

```text
aB92xK
```

becomes extremely popular.

Millions of requests may target the same key.

This is a:

```text
HOT KEY
```

Solutions:

* Aggressive caching
* CDN where appropriate
* Cache replication
* Avoid concentrating traffic on one backend

---

# 12. Cache Stampede

Problem:

A very popular cache entry expires.

Thousands of requests simultaneously miss cache.

```text
1000 requests
      ↓
Redis MISS
      ↓
1000 DB queries
```

Solutions:

* Request coalescing
* Distributed locks
* Early refresh
* Randomized TTL
* Stale-while-revalidate

---

# 13. Cache Eviction

Possible policies:

```text
LRU
LFU
TTL
```

For URL Shortener, popular URLs are valuable to retain.

---

# 14. Interview Questions

### Why Redis?

Low-latency distributed caching.

### What if Redis fails?

Fallback to database.

### What if one URL becomes extremely popular?

Treat it as a hot key and cache aggressively.

### What is cache stampede?

Many requests simultaneously hit the database after a popular cache entry expires.

### Is Redis the source of truth?

No.

```text
Database = Source of Truth
Redis = Cache
```
