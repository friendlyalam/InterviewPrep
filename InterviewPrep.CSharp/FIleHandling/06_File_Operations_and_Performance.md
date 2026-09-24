# File Operations and Performance

## 1. File Performance Is Not Only About Code

File performance depends on:

- File size
- Storage device
- Network vs local storage
- Sequential vs random access
- Buffer size
- Number of concurrent operations
- Memory pressure
- Serialization/deserialization
- File locking
- Operating system
- Application architecture

---

# 2. Small File vs Large File

Small file:

```csharp
string content =
    await File.ReadAllTextAsync(path);

Often perfectly reasonable.

Large file:

Use streaming
    ↓
Read chunks
    ↓
Process

Avoid loading the entire file unnecessarily.

3. Buffering

A buffer allows data to be processed in chunks.

Concept:

File
 ↓
Buffer
 ↓
Application

Instead of reading one byte at a time.

Example:

byte[] buffer =
    new byte[81920];

The ideal buffer size depends on workload. Do not blindly assume one size is always optimal.

4. Sequential Access

Sequential:

1 → 2 → 3 → 4 → 5

Random access:

1 → 1000 → 20 → 5000

Sequential access is generally more efficient for large streaming workloads.

5. FileStream

For controlled processing:

await using FileStream stream =
    new(
        path,
        FileMode.Open,
        FileAccess.Read,
        FileShare.Read,
        bufferSize: 81920,
        useAsync: true);

Then process chunks.

6. FileMode Selection

Choosing the correct FileMode prevents accidental overwrites.

Example:

FileMode.CreateNew

means:

Create only if the file doesn't already exist.

Whereas:

FileMode.Create

can overwrite an existing file.

7. Concurrency

Suppose you have:

100,000 files

Starting:

100,000 concurrent operations

is usually a bad idea.

It can cause:

Storage contention
Thread/resource pressure
Memory usage
Queue buildup
Increased latency

Use bounded concurrency.

8. Parallel File Processing

For CPU-heavy processing after reading files:

File I/O
   ↓
Read data
   ↓
CPU processing
   ↓
Result

Parallelism may help the CPU-processing stage.

But parallelizing disk I/O blindly may make performance worse.

9. Backpressure

Suppose:

Producer → File Queue → Consumers

If producers create work faster than consumers process it, the queue grows.

Use bounded buffering when appropriate:

Producer
   ↓
Bounded Queue
   ↓
Consumers

This prevents unlimited memory growth.

10. Large File Uploads

Bad:

Entire upload
     ↓
byte[]
     ↓
Memory

Better:

Upload
   ↓
Stream
   ↓
Storage

This is especially important for ASP.NET Core applications.

11. Temporary Files

Use framework APIs rather than manually guessing temporary paths.

Example:

string tempPath =
    Path.GetTempFileName();

Temporary files should be cleaned up appropriately.

12. File Locking

A file may be locked by:

Your process
Another process
Antivirus software
Indexing services
Network storage

Therefore:

File.ReadAllText(path);

can fail even if the file exists.

13. Retry

Do not blindly retry every IOException.

Some failures are:

transient
permanent
permission-related
path-related
application bugs

Retries should be used only when the failure is genuinely transient and retrying is safe.

For repeated transient failures, use:

bounded retries
+
exponential backoff
+
jitter
+
timeout
14. Benchmarking

Do not optimize based only on assumptions.

Measure:

Latency
Throughput
Memory
Allocations
CPU
I/O wait

For .NET performance work, BenchmarkDotNet is commonly used for microbenchmarks.

15. Product-Company Scenario

Requirement:

Process a 20 GB log file.

Bad architecture:

20 GB file
    ↓
ReadAllText
    ↓
20 GB+ memory pressure

Better:

20 GB file
    ↓
Stream
    ↓
Read chunk/line
    ↓
Process
    ↓
Write result
16. File Storage Architecture

For enterprise applications:

Application
    ↓
Storage abstraction
    ↓
Local filesystem / Object Storage

The business logic should ideally not be tightly coupled to a specific storage implementation.

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

Implementations can be:

LocalFileStorage
BlobStorage
S3Storage
Interview Questions
Why is loading a huge file into memory dangerous?

It increases memory usage and GC pressure and can cause poor scalability or out-of-memory failures.

Does parallel file processing always improve performance?

No. Storage can become the bottleneck.

What is backpressure?

A mechanism that prevents producers from overwhelming consumers by limiting queued work.

How should a 20 GB file be processed?

Stream it incrementally rather than loading it completely into memory.

Key Points
Stream large files.
Use bounded concurrency.
Avoid unlimited tasks.
Don't assume parallel I/O is faster.
Use backpressure for pipelines.
Retry only transient failures.
Measure before optimizing.
Abstract storage in product applications.