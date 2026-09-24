# ASP.NET Core File Handling

## 1. Common Requirements

ASP.NET Core applications commonly need:

- File upload
- File download
- Image upload
- Document storage
- CSV import
- Report generation
- Large-file streaming

---

# 2. IFormFile

ASP.NET Core uses `IFormFile` for uploaded multipart files.

Example:

```csharp
[HttpPost("upload")]
public async Task<IActionResult> Upload(
    IFormFile file,
    CancellationToken cancellationToken)
{
    if (file.Length == 0)
    {
        return BadRequest(
            "Empty file.");
    }

    // process file

    return Ok();
}
3. Never Trust File.FileName

Do not use:

file.FileName

directly as the physical storage path.

Use it only as metadata if needed.

Generate your own storage name.

string storageName =
    Guid.NewGuid().ToString("N");
4. Save Uploaded File

Basic example:

string path =
    Path.Combine(
        uploadDirectory,
        storageName);

await using FileStream stream =
    new(path, FileMode.CreateNew);

await file.CopyToAsync(
    stream,
    cancellationToken);

Production systems should additionally validate:

size
content
permissions
file type
storage location
authorization
5. File Upload Flow
HTTP Request
     ↓
Controller/API
     ↓
Validation
     ↓
Application Service
     ↓
Storage Abstraction
     ↓
Filesystem / Object Storage

Avoid putting all file-storage logic directly into controllers.

6. Storage Abstraction
public interface IFileStorage
{
    Task<string> UploadAsync(
        Stream content,
        string contentType,
        CancellationToken cancellationToken);

    Task<Stream?> DownloadAsync(
        string key,
        CancellationToken cancellationToken);
}

Possible implementations:

LocalFileStorage
AzureBlobStorage
S3Storage

This allows the application layer to remain independent of the physical storage mechanism.

7. File Download

For a small file:

return PhysicalFile(
    path,
    "application/pdf",
    "report.pdf");

For controlled streaming:

FileStream stream =
    System.IO.File.OpenRead(path);

return File(
    stream,
    "application/pdf",
    "report.pdf");

The stream lifetime must be handled according to the ASP.NET Core response/file-result behavior.

8. Authorization

Never assume:

If user knows file ID → user can download file

The server must verify authorization.

Example:

User
 ↓
Can this user access document 123?
 ↓
YES → download
NO  → deny
9. Database + File Storage

A common architecture:

              ┌───────────────┐
Request ─────→│ ASP.NET Core  │
              └───────┬───────┘
                      |
             ┌────────┴────────┐
             ↓                 ↓
        Database          File Storage
        Metadata           Actual File

Database:

DocumentId
OriginalName
StorageKey
ContentType
Size
OwnerId
CreatedAt

Storage:

Actual binary content
10. Local Storage vs Object Storage
Local Filesystem

Useful for:

Development
Simple internal applications
Single-server scenarios

Problems at scale:

Multiple application instances
Shared storage
Deployment lifecycle
Backup
Availability
Object Storage

Examples:

Azure Blob Storage
Amazon S3

Often better for scalable production file storage.

11. Large File Upload

Avoid:

IFormFile
 ↓
byte[]
 ↓
Memory

for very large files.

Prefer streaming:

HTTP
 ↓
Stream
 ↓
Storage
12. Large File Download

Avoid unnecessarily loading the entire file:

byte[] file =
    await File.ReadAllBytesAsync(path);

Prefer streaming/file results for large files.

13. Request Cancellation

Pass the request cancellation token:

await file.CopyToAsync(
    stream,
    cancellationToken);

If the client disconnects, the application can stop work where supported.

14. File Size Limits

Applications should enforce reasonable upload limits.

Possible levels:

Reverse proxy
ASP.NET Core
Application validation
Storage

Do not depend only on application-level validation.

15. Security

For uploads:

Authentication
     ↓
Authorization
     ↓
Size validation
     ↓
Extension/content validation
     ↓
Malware scanning if required
     ↓
Generate storage key
     ↓
Private storage
16. Background Processing

Large file processing should not always happen completely inside the HTTP request.

Example:

Upload
  ↓
Store file
  ↓
Create processing job
  ↓
Return response
  ↓
Background worker
  ↓
Process file

For distributed systems, a durable message broker may be appropriate.

For process-local workloads, Channel<T> can be useful.

17. Idempotency

Suppose the client retries:

Upload request
Upload request again

You may accidentally create duplicate files.

For business-critical uploads, consider:

Idempotency keys
Unique business identifiers
Content hashes
Database uniqueness
Processing state
18. File Processing Architecture

Example:

Client
  ↓
API
  ↓
Upload
  ↓
Object Storage
  ↓
Database Metadata
  ↓
Queue/Event
  ↓
Background Worker
  ↓
Processing
  ↓
Update Status

This architecture scales better than performing every expensive operation synchronously inside the HTTP request.

Interview Questions
What is IFormFile?

An ASP.NET Core abstraction representing an uploaded multipart form file.

Should FileName be used as the physical path?

No. Treat it as untrusted input and generate a server-side storage key.

Where should large files be stored?

Often object storage is preferred for scalable production systems.

Should large file processing happen inside the request?

Not necessarily. Expensive processing can be moved to background workers.

Why use an abstraction like IFileStorage?

It separates business/application logic from the physical storage implementation.

Key Points
IFormFile → uploaded file abstraction.
Never trust FileName.
Validate uploads.
Stream large files.
Use cancellation.
Authorize downloads.
Separate metadata from file content.
Use storage abstraction.
Object storage is commonly preferred for scalable production systems.
Move expensive processing to background workers.