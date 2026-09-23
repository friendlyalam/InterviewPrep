# URL Shortener — Step 3: API Design

# 1. Create Short URL

```http
POST /api/v1/urls
```

## Request

```json
{
  "originalUrl": "https://example.com/products/123",
  "expiresAt": "2027-01-01T00:00:00Z",
  "customAlias": null
}
```

## Response

```json
{
  "shortCode": "aB92xK",
  "shortUrl": "https://short.ly/aB92xK",
  "expiresAt": "2027-01-01T00:00:00Z"
}
```

---

# 2. Redirect

```http
GET /{shortCode}
```

Example:

```http
GET /aB92xK
```

Response:

```http
302 Found
Location: https://example.com/products/123
```

---

# 3. Disable URL

```http
DELETE /api/v1/urls/{shortCode}
```

Response:

```json
{
  "message": "URL disabled successfully"
}
```

---

# 4. Get URL Details

```http
GET /api/v1/urls/{shortCode}
```

Response:

```json
{
  "shortCode": "aB92xK",
  "originalUrl": "https://example.com/products/123",
  "createdAt": "2026-09-23T10:00:00Z",
  "expiresAt": "2027-01-01T00:00:00Z",
  "isActive": true
}
```

---

# 5. Analytics

```http
GET /api/v1/urls/{shortCode}/analytics
```

Response:

```json
{
  "shortCode": "aB92xK",
  "totalClicks": 125000,
  "countries": {
    "IN": 80000,
    "US": 30000
  }
}
```

---

# 6. HTTP Status Codes

```text
200 OK
201 Created
302 Found
400 Bad Request
401 Unauthorized
403 Forbidden
404 Not Found
409 Conflict
410 Gone
429 Too Many Requests
500 Internal Server Error
```

---

# 7. Idempotency

Create URL:

```http
POST /api/v1/urls
Idempotency-Key: abc123
```

Why?

Network timeout may cause the client to retry.

Without idempotency:

```text
Request 1 → URL A
Request 2 → URL B
```

With idempotency:

```text
Request 1 → URL A
Retry      → URL A
```

---

# 8. API Design Interview Questions

### Why REST?

Simple and widely understood.

### Why version APIs?

```text
/api/v1/urls
```

allows future API evolution.

### Should redirect require authentication?

Normally no.

The short URL should be publicly accessible unless the product requires private URLs.

### Why should analytics not be part of redirect response?

Because analytics should not increase redirect latency.
