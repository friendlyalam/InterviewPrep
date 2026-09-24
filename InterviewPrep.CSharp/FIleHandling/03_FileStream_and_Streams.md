# FileStream and Streams in C#

## 1. What is a Stream?

A `Stream` represents a sequence of bytes.

It provides a common abstraction for reading and writing data.

Examples:

```text
File
Network
Memory
Compression
HTTP

Common stream classes:

Stream
 ├── FileStream
 ├── MemoryStream
 ├── NetworkStream
 └── other specialized streams
2. FileStream

FileStream provides byte-level access to files.

using FileStream stream =
    File.OpenRead("data.bin");

Read bytes:

byte[] buffer =
    new byte[1024];

int bytesRead =
    stream.Read(buffer, 0, buffer.Length);
3. Why FileStream?

Useful when:

Working with binary files
Processing large files
Controlling read/write behavior
Streaming data
Avoiding loading the entire file into memory
4. FileMode

FileMode determines how the file is opened.

Common values:
| FileMode       | Meaning                          |
| -------------- | -------------------------------- |
| `CreateNew`    | Create new file; fail if exists  |
| `Create`       | Create or overwrite              |
| `Open`         | Open existing file               |
| `OpenOrCreate` | Open if exists, otherwise create |
| `Truncate`     | Open and remove existing content |
| `Append`       | Open at end for appending        |


Example:

using FileStream stream =
    new(
        "data.txt",
        FileMode.Open,
        FileAccess.Read);
5. FileAccess

Controls whether the file is opened for:

Read
Write
ReadWrite

Example:

new FileStream(
    "data.txt",
    FileMode.Open,
    FileAccess.Read);
6. FileShare

Controls how other processes can access the file while your stream is open.

Common values:

None
Read
Write
ReadWrite
Delete

Example:

using FileStream stream =
    new(
        "log.txt",
        FileMode.Open,
        FileAccess.Read,
        FileShare.Read);
7. FileStream Position

A stream maintains a current position.

long position =
    stream.Position;

Move position:

stream.Seek(
    100,
    SeekOrigin.Begin);

SeekOrigin:

Begin
Current
End
8. CanSeek

Not every stream supports seeking.

if (stream.CanSeek)
{
    stream.Seek(
        0,
        SeekOrigin.Begin);
}
9. CanRead / CanWrite
if (stream.CanRead)
{
    // read
}

if (stream.CanWrite)
{
    // write
}
10. FileStream and Memory

Bad for huge files:

byte[] data =
    File.ReadAllBytes("huge-file.bin");

The complete file is loaded into memory.

Streaming approach:

using FileStream stream =
    File.OpenRead("huge-file.bin");

byte[] buffer =
    new byte[81920];

int bytesRead;

while ((bytesRead =
       stream.Read(buffer, 0, buffer.Length)) > 0)
{
    // process buffer
}
11. MemoryStream

MemoryStream represents stream data in memory.

using MemoryStream stream =
    new();

byte[] data =
    Encoding.UTF8.GetBytes("Hello");

stream.Write(
    data,
    0,
    data.Length);

Useful for:

Temporary in-memory data
Transformations
Testing
Serialization
Working with APIs requiring streams

It is not a replacement for persistent storage.

12. Stream Composition

Streams can be combined.

Conceptually:

FileStream
    ↓
Buffered/Compression Stream
    ↓
Reader/Writer

Example:

File
 ↓
FileStream
 ↓
GZipStream
 ↓
StreamReader

This allows different responsibilities to be layered.

13. StreamReader

Use for text.

using StreamReader reader =
    new("data.txt");

string? line;

while ((line = reader.ReadLine()) != null)
{
    Console.WriteLine(line);
}
14. StreamWriter
using StreamWriter writer =
    new("log.txt");

writer.WriteLine(
    "Application started");
15. Dispose

Streams hold OS resources.

Always dispose them.

Preferred:

using FileStream stream =
    File.OpenRead("data.txt");

or:

await using FileStream stream =
    new(...);

Use the appropriate disposal pattern for the API being used.

16. Product-Company Example

Suppose a user uploads a 500 MB video.

Bad approach:

Upload
 ↓
Load entire file into byte[]
 ↓
Process

Potential problem:

High memory usage
GC pressure
Poor scalability

Better:

Upload
 ↓
Stream
 ↓
Process chunks
 ↓
Storage
17. Stream vs File

| File                        | Stream                                    |
| --------------------------- | ----------------------------------------- |
| High-level file helper      | Data flow abstraction                     |
| Easy operations             | Fine-grained control                      |
| Convenient                  | Useful for large/continuous data          |
| Often reads/writes directly | Can represent file, memory, network, etc. |


Interview Questions
What is a Stream?

A stream is an abstraction representing sequential data that can be read from or written to.

Why use FileStream?

For controlled byte-level file access and streaming large files without loading the complete content into memory.

What is FileMode?

It defines how a file should be opened or created.

What is FileAccess?

It controls whether the stream can read, write, or both.

What is FileShare?

It controls how other processes may access the file while it is open.

Key Points
Stream → abstraction for sequential data.
FileStream → file-based byte stream.
MemoryStream → in-memory stream.
FileMode → how file is opened.
FileAccess → read/write permissions.
FileShare → sharing behavior.
Use streams for large files.
Always dispose streams.

