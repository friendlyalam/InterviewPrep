# URL Shortener — Step 10: Security and Observability

# PART A — SECURITY

# 1. HTTPS

All communication should use:

```text
HTTPS / TLS
```

Protects data in transit.

---

# 2. Authentication

Management APIs require authentication.

Example:

```text
POST /api/v1/urls
```

Possible technologies:

```text
OAuth 2.0
OpenID Connect
JWT
Microsoft Entra ID
```

---

# 3. Authorization

Users should only manage URLs they own.

Example:

```text
User A
 ↓
Can manage User A URLs

User B
 ↓
Cannot modify User A URLs
```

---

# 4. Rate Limiting

Protect:

```text
POST /api/v1/urls
```

Example:

```text
100 requests/minute/user
```

Actual limit depends on product requirements.

Possible algorithms:

```text
Token Bucket
Leaky Bucket
Sliding Window
```

---

# 5. URL Validation

Validate:

* URL syntax
* Protocol
* Maximum length
* Allowed schemes
* Blocked domains if required

Example:

```text
https://example.com
```

valid.

```text
invalid-url
```

invalid.

---

# 6. Abuse Protection

URL shorteners can be abused for:

* Spam
* Phishing
* Malware distribution
* Automated URL generation

Possible protections:

* Rate limiting
* Domain reputation checks
* Abuse reporting
* URL scanning
* Blocklists
* CAPTCHA where appropriate

---

# 7. Input Validation

Never trust user input.

Validate:

```text
OriginalUrl
CustomAlias
Expiration
User input
```

---

# 8. Encryption

### In Transit

```text
TLS
```

### At Rest

Use database/storage encryption.

---

# 9. Secrets

Never store secrets in source code.

Use:

```text
Azure Key Vault
Secret Manager
Environment-based secure configuration
```

---

# PART B — OBSERVABILITY

# 10. Logging

Log:

```text
Request ID
Trace ID
Endpoint
Status Code
Latency
Error
Service
Timestamp
```

Avoid logging sensitive data unnecessarily.

---

# 11. Metrics

Important metrics:

```text
Requests/sec
Latency
Error rate
Cache hit ratio
Cache miss ratio
Database latency
Database connections
Redis memory
Queue depth
Consumer lag
```

---

# 12. Redirect Metrics

Especially monitor:

```text
Redirect RPS
Redirect latency
Cache hit ratio
Cache miss ratio
404 rate
Expired URL rate
```

---

# 13. Database Metrics

Monitor:

```text
CPU
Memory
Connections
Query latency
Storage
Replication lag
```

---

# 14. Redis Metrics

Monitor:

```text
Memory
Hit ratio
Miss ratio
Evictions
Latency
Connection count
```

---

# 15. Queue Metrics

Monitor:

```text
Queue depth
Consumer lag
Processing latency
Failed messages
DLQ size
```

---

# 16. Distributed Tracing

Example:

```text
Trace ID: ABC123

Client
 ↓
API Gateway
 ↓
Redirect Service
 ↓
Redis
 ↓
Database
```

The same trace ID allows us to follow the complete request.

---

# 17. Alerts

Alert on:

```text
High error rate
High latency
Database unavailable
Redis unavailable
High queue depth
High consumer lag
High CPU
High memory
Low cache hit ratio
Replication failure
```

---

# 18. SLI / SLO / SLA

### SLI

Measured metric.

Example:

```text
Redirect latency
```

### SLO

Target.

Example:

```text
99.9% successful redirects
```

### SLA

Contractual commitment to customers.

---

# 19. Interview Questions

### How do you monitor this system?

Logs + Metrics + Tracing + Alerts.

### What metrics matter most?

For this system:

```text
Redirect latency
Error rate
RPS
Cache hit ratio
Database health
```

### How do you debug slow redirects?

Trace:

```text
Gateway
 ↓
Redirect Service
 ↓
Redis
 ↓
Database
```

Find where latency increases.
