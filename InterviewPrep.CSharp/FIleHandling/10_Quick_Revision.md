
# 10_Quick_Revision.md

```markdown
# C# File Handling — Quick Revision

## Core Classes

```text
File
FileInfo
Directory
DirectoryInfo
Path
FileStream
StreamReader
StreamWriter
MemoryStream
Stream
File

Common operations:

File.Exists(path);

File.ReadAllText(path);

File.WriteAllText(path, text);

File.ReadLines(path);

File.Copy(source, destination);

File.Move(source, destination);

File.Delete(path);
Directory
Directory.Exists(path);

Directory.CreateDirectory(path);

Directory.Delete(path);

Directory.GetFiles(path);

Directory.EnumerateFiles(path);

Directory.EnumerateDirectories(path);
Path

Remember:

Path.Combine(...);

Path.GetFileName(...);

Path.GetExtension(...);

Path.GetDirectoryName(...);

Path.GetFullPath(...);

Path.GetTempPath();

Never manually concatenate filesystem paths.

FileInfo
FileInfo file =
    new(path);

file.Exists;
file.Length;
file.Extension;
file.FullName;
file.LastWriteTime;
DirectoryInfo
DirectoryInfo directory =
    new(path);

directory.Exists;
directory.FullName;
directory.GetFiles();
directory.GetDirectories();
Large Files

Avoid:

File.ReadAllBytes(path);
File.ReadAllText(path);

when the file may be very large.

Prefer:

FileStream
StreamReader
ReadLines
Streaming
Stream
Stream
 ├── FileStream
 ├── MemoryStream
 └── NetworkStream

Stream = abstraction for sequential byte-based data.

FileStream

Remember:

FileMode
FileAccess
FileShare
Buffer
Position
Seek
FileMode
Create
CreateNew
Open
OpenOrCreate
Truncate
Append
FileAccess
Read
Write
ReadWrite
FileShare

Controls how other processes can access an open file.

Text Handling

Small text:

string text =
    File.ReadAllText(path);

Large text:

foreach (string line in
         File.ReadLines(path))
{
    Process(line);
}
Writing

Overwrite:

File.WriteAllText(path, text);

Append:

File.AppendAllText(path, text);
Encoding

Common:

UTF-8
UTF-16
ASCII

UTF-8 is the common modern choice.

Async

Prefer:

await File.ReadAllTextAsync(path);

await File.WriteAllTextAsync(path, text);

await File.AppendAllTextAsync(path, text);

Remember:

async ≠ parallel
Task.Run

Usually don't do:

await Task.Run(
    () => File.ReadAllText(path));

Prefer the native async API.

Cancellation

Pass the token:

await File.ReadAllTextAsync(
    path,
    cancellationToken);
Disposal

Streams should be disposed:

using FileStream stream =
    File.OpenRead(path);

or:

await using FileStream stream =
    new(...);
Large File Mental Model
Open
 ↓
Read chunk
 ↓
Process
 ↓
Read next chunk
 ↓
...

Not:

Open
 ↓
Load entire file
 ↓
Process
ASP.NET Core Upload

Common abstraction:

IFormFile

Never blindly trust:

file.FileName

Generate a server-side storage key.

Secure Upload

Remember:

Authentication
 ↓
Authorization
 ↓
Size validation
 ↓
Type/content validation
 ↓
Generate storage key
 ↓
Secure storage
 ↓
Metadata
Path Traversal

Never trust:

../../secret.txt

User-controlled paths can escape the intended directory.

Extension Validation

Extension alone is not enough.

Consider:

Extension
MIME/content
File signature
File structure
Malware scanning

depending on risk.

Production Storage

Common architecture:

Database
    ↓
File metadata

Object Storage
    ↓
Actual file

Examples:

Azure Blob Storage
Amazon S3
File Processing

Large processing:

Upload
 ↓
Storage
 ↓
Queue
 ↓
Background Worker
 ↓
Stream
 ↓
Process
Concurrency

Don't process unlimited files simultaneously.

Use:

Parallel.ForEachAsync
SemaphoreSlim
Channel<T>
Background Worker

according to the workload.

Backpressure

If:

Producer > Consumer

the queue can grow.

Use bounded queues to control memory and work-in-flight.

File vs Stream
File
→ high-level convenience API

Stream
→ data flow abstraction

FileStream
→ stream backed by file
File vs FileInfo
File
→ static helper

FileInfo
→ object representing a file
Directory vs DirectoryInfo
Directory
→ static helper

DirectoryInfo
→ object representing directory
ReadAllText vs ReadLines
ReadAllText
→ complete content in memory

ReadLines
→ incremental enumeration
FileStream vs MemoryStream
FileStream
→ file/storage

MemoryStream
→ memory
Local Files vs Object Storage
Local filesystem
→ simple/single-server scenarios

Object storage
→ scalable distributed applications
Common Mistakes
❌ Trusting FileName
❌ Trusting file extension
❌ Loading huge files into memory
❌ Unlimited concurrency
❌ Blocking ASP.NET Core I/O unnecessarily
❌ Forgetting disposal
❌ Ignoring cancellation
❌ Blind retries
❌ Exposing physical storage paths
❌ Using check-then-act for correctness
Product-Company Mental Model
                    FILE HANDLING
                         |
       ┌─────────────────┼─────────────────┐
       ↓                 ↓                 ↓
   Basic I/O          Streaming          Security
       |                 |                 |
 File/Directory      FileStream          Validation
 Path                ReadLines           Path traversal
 FileInfo            Async I/O           Access control
       |                 |                 |
       └─────────────────┼─────────────────┘
                         ↓
                   ASP.NET Core
                         |
                ┌────────┴────────┐
                ↓                 ↓
             Upload            Download
                |
                ↓
          Storage Abstraction
                |
        ┌───────┴────────┐
        ↓                ↓
   Local Storage     Object Storage
                         |
                         ↓
                  Background Worker
                         |
                         ↓
               Queue + Processing
Most Important Rules
File → simple file operations.
FileInfo → file object and metadata.
Directory → simple directory operations.
Path → path manipulation.
Stream → generic data-flow abstraction.
FileStream → file-based stream.
Small files → high-level APIs are usually fine.
Large files → stream them.
Server applications → prefer async I/O where appropriate.
async does not mean parallel.
Always dispose streams.
Pass cancellation tokens.
Never trust user-provided filenames or paths.
Use server-generated storage keys.
Validate file size and content.
Use bounded concurrency.
Use backpressure for pipelines.
Don't blindly retry I/O failures.
Separate file metadata from large file content when appropriate.
For scalable production systems, consider object storage.
Expensive processing can move to background workers.
Business logic should depend on storage abstractions, not physical filesystem details.

### Recommended learning order

```text
01 Basics
   ↓
02 File + Directory
   ↓
03 FileStream + Streams
   ↓
04 Text Handling
   ↓
05 Async File Handling
   ↓
06 Performance
   ↓
07 Security
   ↓
08 ASP.NET Core
   ↓
09 Interview Questions
   ↓
10 Quick Revision