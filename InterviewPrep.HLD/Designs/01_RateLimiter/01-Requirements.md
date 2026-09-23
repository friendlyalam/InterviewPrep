# URL Shortener — Step 1: Requirements

## 1. Problem

Design a URL Shortener that converts a long URL into a short URL.

Example:

```text
Long URL:
https://www.example.com/products/category/mobile/iphone

Short URL:
https://short.ly/aB92xK
```

When the user opens:

```text
https://short.ly/aB92xK
```

the system redirects the user to the original URL.

---

# 2. Functional Requirements

## FR-1: Create Short URL

User provides a long URL.

```text
POST /api/v1/urls
```

System generates:

```text
https://short.ly/aB92xK
```

---

## FR-2: Redirect

User accesses:

```text
GET /aB92xK
```

System returns a redirect to the original URL.

```text
302 Found
Location: https://www.example.com/products/123
```

---

## FR-3: Custom Alias

User can optionally request:

```text
https://short.ly/my-product
```

instead of a generated code.

The alias must be unique.

---

## FR-4: Expiration

A URL can optionally have an expiration time.

Example:

```text
ExpiresAt = 2027-01-01
```

After expiration, the short URL should no longer redirect.

---

## FR-5: Disable/Delete

User can disable a short URL.

Disabled URL:

```text
GET /aB92xK
```

should not redirect.

---

## FR-6: Analytics

System should support basic click analytics:

* Click count
* Timestamp
* Country
* Device
* Browser
* Referrer

Analytics should not slow down the redirect.

---

# 3. Non-Functional Requirements

## NFR-1: Scalability

System should support:

* Millions of users
* Millions of URL creations
* Billions of redirects

---

## NFR-2: Availability

Target:

```text
99.9%+
```

Redirect functionality should remain available even if some application instances fail.

---

## NFR-3: Performance

Redirect should have low latency.

Target example:

```text
p95 < 100 ms
```

---

## NFR-4: Reliability

System should:

* Avoid duplicate short codes.
* Avoid losing URL mappings.
* Handle server failures.
* Handle database failures.
* Handle cache failures.

---

## NFR-5: Consistency

Strong consistency is important for:

```text
ShortCode → OriginalURL
```

Eventual consistency is acceptable for:

```text
Analytics
```

---

## NFR-6: Security

System should support:

* Authentication
* Authorization
* HTTPS
* Rate limiting
* URL validation
* Abuse protection

---

# 4. Scope

## In Scope

```text
✓ Create short URL
✓ Redirect
✓ Custom alias
✓ Expiration
✓ Disable URL
✓ Basic analytics
✓ Caching
✓ Rate limiting
✓ Scalability
```

## Out of Scope

```text
✗ Advanced billing
✗ Marketing platform
✗ Recommendation system
✗ Advanced BI
```

---

# 5. Assumptions

For interview discussion:

```text
Users              = 10 million
New URLs/day       = 10 million
Redirects/day      = 1 billion
Read : Write       = 100 : 1
Average URL size   = ~500 bytes
Short code         = 7 characters
```

These numbers are assumptions and can be changed during the interview.

---

# 6. Most Important Interview Insight

The system is:

```text
READ HEAVY
```

because:

```text
Redirects >> URL Creations
```

Therefore the architecture should optimize the redirect/read path.

This leads naturally to:

```text
Redis
Caching
Stateless services
Horizontal scaling
Read optimization
```

---

# 7. Critical Requirements

If the interviewer asks for only the important requirements:

### Functional

1. Create short URL.
2. Redirect short URL.
3. Support expiration.
4. Support custom aliases.
5. Track analytics asynchronously.

### Non-Functional

1. High availability.
2. Low latency.
3. Horizontal scalability.
4. Strong URL mapping consistency.
5. Reliable storage.
6. Security and abuse protection.

---

# 8. Interview Questions

### Q1. What is the most important workload?

Redirect.

### Q2. Why?

Because:

```text
Read traffic >> Write traffic
```

### Q3. Which requirement drives the architecture?

High-volume, low-latency redirects.

### Q4. What can be eventually consistent?

Analytics.

### Q5. What should be strongly consistent?

Short-code uniqueness and URL mapping.
