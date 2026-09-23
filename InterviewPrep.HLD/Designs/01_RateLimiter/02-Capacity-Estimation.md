# URL Shortener — Step 2: Capacity Estimation

# 1. Why Capacity Estimation?

Before designing infrastructure, estimate:

* Requests/sec
* Storage
* Bandwidth
* Read/write ratio
* Peak traffic

This tells us how large the system needs to be.

---

# 2. Assumptions

```text
Users              = 10 million
New URLs/day       = 10 million
Redirects/day      = 1 billion
Read : Write       = 100 : 1
Average record     = ~1 KB
Peak factor        = 5×
```

---

# 3. URL Creation RPS

New URLs:

```text
10,000,000/day
```

Seconds/day:

```text
86,400
```

Average writes/sec:

```text
10,000,000 / 86,400
≈ 116 writes/sec
```

Peak:

```text
116 × 5
≈ 580 writes/sec
```

Therefore:

```text
~600 writes/sec
```

---

# 4. Redirect RPS

Redirects:

```text
1,000,000,000/day
```

Average:

```text
1,000,000,000 / 86,400
≈ 11,574 redirects/sec
```

Peak:

```text
11,574 × 5
≈ 57,870 redirects/sec
```

Therefore:

```text
~60K redirects/sec
```

---

# 5. Key Observation

```text
Write:
~600/sec

Read:
~60,000/sec peak
```

Therefore:

```text
READ >> WRITE
```

The read path is the main scalability challenge.

---

# 6. Storage Estimation

Assume:

```text
1 URL record ≈ 1 KB
```

Daily:

```text
10M × 1 KB
≈ 10 GB/day
```

Yearly:

```text
10 GB × 365
≈ 3.65 TB/year
```

Actual storage will be higher because of:

* Indexes
* Replication
* Metadata
* Backups

---

# 7. Storage Growth

Approximate:

```text
1 year  → 3.65 TB
3 years → 10.95 TB
5 years → 18.25 TB
```

Before replication and indexes.

---

# 8. Bandwidth

Assume:

```text
Average redirect request ≈ 1 KB
```

At 60K requests/sec:

```text
60,000 × 1 KB
≈ 60 MB/sec
```

Approximately:

```text
~480 Mbps
```

This is a rough estimate.

Actual bandwidth depends on:

* HTTP headers
* TLS
* Response size
* Client behavior

---

# 9. What Capacity Estimation Tells Us

We now know:

```text
~600 writes/sec
~60K reads/sec peak
~3.65 TB/year base storage
```

Therefore:

### Application

Needs horizontal scaling.

### Database

Needs scalable storage and read optimization.

### Cache

Required to reduce database reads.

### Queue

Useful for analytics.

---

# 10. Interview Shortcut

If time is limited, say:

> Assume 10M new URLs/day and 1B redirects/day. This gives roughly 600 peak writes/sec and
60K peak redirects/sec using a 5× peak factor. Since reads dominate writes by roughly 100×, 
the system should optimize the redirect path with caching and horizontally scalable stateless services.

This is enough to establish the architecture direction.
