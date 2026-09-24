# Async File Handling in C#

## 1. Why Async File I/O?

In server applications, synchronous file operations can block the calling thread while waiting for I/O.

Async APIs allow the operation to be awaited without unnecessarily blocking the thread.

Typical uses:

- ASP.NET Core
- Large file processing
- File uploads/downloads
- Background processing
- Cloud/local storage operations

---

# 2. Async Read

```csharp
string content =
    await File.ReadAllTextAsync(
        "data.txt");
3. Async Write
await File.WriteAllTextAsync(
    "data.txt",
    "Hello");
4. Async Append
await File.AppendAllTextAsync(
    "log.txt",
    "New log entry");
5. Async Stream Reading
using StreamReader reader =
    new("large-file.txt");

while (!reader.EndOfStream)
{
    string? line =
        await reader.ReadLineAsync();

    if (line is not null)
    {
        Process(line);
    }
}
6. Cancellation

Async file operations may support CancellationToken.

Example:

CancellationToken token =
    cancellationToken;

string content =
    await File.ReadAllTextAsync(
        "data.txt",
        token);

Cancellation is cooperative.

It does not mean the OS can always instantly stop every underlying operation.

7. Async FileStream
await using FileStream stream =
    new(
        "data.bin",
        FileMode.Open,
        FileAccess.Read,
        FileShare.Read,
        bufferSize: 81920,
        useAsync: true);

The exact constructor/options should be chosen according to the application and .NET version.

8. Async Is Not Parallelism

Important:

async ≠ parallel

Async primarily helps with asynchronous I/O.

If you have:

File A
File B
File C

and they are independent, you may coordinate operations concurrently.

But unlimited concurrent file operations can overload:

Disk
Storage subsystem
Network filesystem
CPU
Memory
9. Avoid Unnecessary Task.Run

Do not normally write:

await Task.Run(() =>
    File.ReadAllText("data.txt"));

just to make synchronous file I/O "async".

Prefer the actual async API:

await File.ReadAllTextAsync(
    "data.txt");
10. ASP.NET Core

Bad:

string content =
    File.ReadAllText(path);

for a large/latency-sensitive server operation when an async API is appropriate.

Prefer:

string content =
    await File.ReadAllTextAsync(
        path,
        cancellationToken);

This supports better scalability under concurrent requests.

11. Async Streaming

For large files:

Request
   ↓
Open file
   ↓
Read chunk
   ↓
Process/send
   ↓
Read next chunk
   ↓
...

Instead of:

Request
   ↓
Load entire file
   ↓
Process
12. IAsyncEnumerable

For asynchronous incremental data:

await foreach (
    string line in
    ReadLinesAsync(path, token))
{
    Process(line);
}

This can be useful when data is produced asynchronously over time.

13. Concurrency Control

Do not start thousands of file operations blindly:

Task[] tasks =
    files.Select(
        file => ProcessAsync(file))
    .ToArray();

For large collections, use controlled concurrency.

Example:

ParallelOptions options = new()
{
    MaxDegreeOfParallelism = 4,
    CancellationToken = token
};

await Parallel.ForEachAsync(
    files,
    options,
    async (file, cancellationToken) =>
    {
        await ProcessAsync(
            file,
            cancellationToken);
    });

The optimal degree depends on workload and storage.

14. Cancellation in ASP.NET Core

A request can be cancelled by the client.

Pass the request cancellation token down:

Controller
   ↓
Service
   ↓
File operation
   ↓
CancellationToken

Do not silently create unrelated cancellation tokens at every layer.

15. Error Handling

Async file operations can throw:

FileNotFoundException
DirectoryNotFoundException
UnauthorizedAccessException
IOException
OperationCanceledException

Handle only where meaningful.

Cancellation should normally be treated separately from unexpected failures.

Interview Questions
Why use async file APIs?

To avoid unnecessary thread blocking during asynchronous I/O and improve scalability in applications handling concurrent work.

Does async make file processing parallel?

No.

Should Task.Run wrap File.ReadAllText?

Usually no. Use the native async file API.

Why pass CancellationToken?

To allow callers to cooperatively stop work that is no longer needed.

Key Points
Prefer async I/O in server applications where appropriate.
async != parallel.
Avoid unnecessary Task.Run.
Pass cancellation tokens.
Stream large files.
Control concurrency.
Do not assume more concurrent file operations means better performance.