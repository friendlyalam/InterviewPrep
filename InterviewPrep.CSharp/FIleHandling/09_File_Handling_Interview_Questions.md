# File Handling Interview Questions

## 1. What is File Handling?

File handling means creating, reading, writing, updating, copying, moving, deleting, and streaming files.

---

# 2. File vs FileInfo?

`File` provides static helper methods.

`FileInfo` represents a specific file and provides instance operations and metadata.

---

# 3. Directory vs DirectoryInfo?

`Directory` provides static directory operations.

`DirectoryInfo` represents a specific directory and provides instance operations and metadata.

---

# 4. Why use Path.Combine?

It safely constructs paths using platform-appropriate separators.

```csharp
string path =
    Path.Combine(
        root,
        "Reports",
        "report.pdf");
5. ReadAllText vs ReadLines?

ReadAllText loads the complete file into memory.

ReadLines allows incremental line-by-line processing.

For large files, ReadLines is generally more appropriate.

6. FileStream vs File?

File provides convenient high-level operations.

FileStream provides stream-based byte access and more control over file I/O.

7. What is a Stream?

A stream is an abstraction representing a sequence of bytes that can be read or written.

8. FileStream vs MemoryStream?
| FileStream             | MemoryStream                       |
| ---------------------- | ---------------------------------- |
| Backed by file         | Backed by memory                   |
| Persistent storage     | Temporary/in-memory data           |
| Useful for large files | Useful for transformations/testing |
| Disk/storage I/O       | Memory operations                  |

9. What is FileMode?

It specifies how a file should be opened or created.

Examples:

Create
CreateNew
Open
OpenOrCreate
Truncate
Append
10. What is FileAccess?

It controls whether the file is opened for:

Read
Write
ReadWrite
11. What is FileShare?

It controls how other processes can access the file while your stream is open.

12. Why use using with streams?

Streams hold unmanaged/OS resources.

using ensures deterministic disposal.

using FileStream stream =
    File.OpenRead(path);
13. Why use async file operations?

To avoid unnecessary thread blocking during asynchronous I/O, especially in server applications.

14. Does async mean parallel?

No.

async → asynchronous programming
parallel → simultaneous execution

They solve different problems.

15. Should Task.Run wrap File.ReadAllText?

Usually no.

Prefer:

await File.ReadAllTextAsync(path);

rather than:

await Task.Run(
    () => File.ReadAllText(path));
16. How do you process a huge file?

Use streaming.

Open
 ↓
Read chunk/line
 ↓
Process
 ↓
Continue

Avoid loading the entire file into memory.

17. What is FileShare?

It determines what access other processes are allowed while the file is open.

Example:

FileShare.Read

allows other readers according to the OS/filesystem semantics.

18. How do you safely handle uploaded filenames?

Do not use the user-provided filename as a trusted physical path.

Generate a server-side storage key.

string storageKey =
    Guid.NewGuid().ToString("N");
19. What is path traversal?

An attack where crafted path input attempts to escape the intended directory.

Example concept:

../../secret.txt

Never trust arbitrary user paths.

20. Is file extension validation enough?

No.

A malicious file can have a misleading extension.

Depending on risk, validate:

Extension
Content type
File signature
File structure
Malware scanning
21. Why use an allowlist?

Example:

Allowed:
.pdf
.png
.jpg

An allowlist explicitly defines what the application accepts.

22. How should large uploads be handled?

Prefer streaming:

HTTP request
 ↓
Stream
 ↓
Storage

rather than:

HTTP request
 ↓
byte[]
 ↓
Memory
23. Where should production files be stored?

Depending on requirements:

Local filesystem
Object storage

For scalable distributed applications, object storage is commonly preferred.

24. Why not store large files directly in SQL?

It can be appropriate in some systems, but large binary storage in a relational database may increase database size, backup cost, I/O pressure, and operational complexity.

A common architecture is:

Database → metadata
Object storage → actual file

The correct choice depends on requirements.

25. How do you handle file-processing failures?

Separate:

Transient failures
Permanent failures
Validation failures
Cancellation
Unexpected failures

Use retry only when retrying is safe and the failure is transient.

26. What is backpressure in file processing?

When producers generate file-processing work faster than consumers can process it, bounded queues prevent unlimited accumulation.

27. Should thousands of files be processed concurrently?

Usually no.

Use controlled concurrency.

Possible tools:

Parallel.ForEachAsync
SemaphoreSlim
Channel<T>
Queue/background worker

depending on the workload.

28. What happens if two processes access the same file?

Behavior depends on:

FileMode
FileAccess
FileShare
OS/filesystem
timing

The operation may succeed, block, or fail.

Design explicitly for concurrent access where required.

29. What is a storage abstraction?

Example:

public interface IFileStorage
{
    Task UploadAsync(
        string key,
        Stream content,
        CancellationToken cancellationToken);

    Task<Stream> DownloadAsync(
        string key,
        CancellationToken cancellationToken);
}

The application can then use:

Local storage
Azure Blob
S3

without changing business logic.

30. Product Scenario
Requirement

Users upload PDF documents.

Good design
Client
 ↓
ASP.NET Core API
 ↓
Authentication/Authorization
 ↓
Validation
 ↓
Generate storage key
 ↓
Object Storage
 ↓
Database metadata
 ↓
Background processing if required
31. Important Interview Traps
Trap 1

async means another thread is always created.

False.

Trap 2

IReadOnlyList is immutable.

False.

Similarly, a read-only view does not necessarily make the underlying collection immutable.

Trap 3

File.Exists() followed by Write() is always safe.

False.

It can introduce a check-then-act race.

Trap 4

File extension tells you what the file really is.

False.

Trap 5

More parallel file operations always improve performance.

False.

Storage can become the bottleneck.

Senior-Level Questions
Design a document upload service.

Discuss:

API
Authentication
Authorization
Size limits
Validation
Storage
Metadata
Object storage
Background processing
Idempotency
Retry
Virus/malware scanning
Access control
Audit
Observability
Design a large CSV import system.

Possible architecture:

Upload
 ↓
Object Storage
 ↓
Create Import Job
 ↓
Queue
 ↓
Worker
 ↓
Stream CSV
 ↓
Validate
 ↓
Batch DB writes
 ↓
Update progress

Important topics:

Streaming
Batching
Backpressure
Cancellation
Retry
Partial failures
Idempotency
Progress tracking
Final Interview Mental Model
Small file
    ↓
File.ReadAllText / WriteAllText

Large file
    ↓
Stream / StreamReader / FileStream

Server application
    ↓
Async I/O + CancellationToken

User upload
    ↓
Validate + Generate storage key + Secure storage

Large production storage
    ↓
Object Storage + DB metadata

Large processing
    ↓
Streaming + Bounded concurrency + Background workers

---
