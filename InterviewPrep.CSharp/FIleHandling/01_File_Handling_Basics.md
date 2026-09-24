# File Handling Basics in C#

## 1. What is File Handling?

File handling means reading, writing, creating, updating, copying, moving, and deleting files from the filesystem.

Common real-world uses:

- Application configuration
- Log files
- CSV/JSON/XML files
- Reports
- Import/export
- Temporary files
- File uploads/downloads
- Batch processing

---

# 2. Important .NET Classes

| Class | Purpose |
|---|---|
| `File` | Static helper methods for common file operations |
| `FileInfo` | Instance-based file operations and metadata |
| `Directory` | Static helper methods for directories |
| `DirectoryInfo` | Instance-based directory operations |
| `Path` | Safely constructs and analyzes paths |
| `FileStream` | Byte-level file I/O |
| `StreamReader` | Text reading |
| `StreamWriter` | Text writing |

---

# 3. File Class

`File` provides static methods for common operations.

### Check existence

```csharp
bool exists = File.Exists("data.txt");

Create
using FileStream stream = File.Create("data.txt");
Write text
File.WriteAllText("data.txt", "Hello World");
Read text
string content = File.ReadAllText("data.txt");
Delete
File.Delete("data.txt");
Copy
File.Copy("source.txt", "destination.txt");
Move
File.Move("source.txt", "destination.txt");
4. ReadAllText vs ReadAllLines
string text = File.ReadAllText("data.txt");

Loads the complete file into memory.

string[] lines = File.ReadAllLines("data.txt");

Loads all lines into memory.

For very large files, these can consume significant memory.

Prefer streaming for large files.

5. ReadLines
IEnumerable<string> lines =
    File.ReadLines("large-file.txt");

foreach (string line in lines)
{
    Console.WriteLine(line);
}

ReadLines allows lines to be processed incrementally instead of creating the complete string array first.

This is generally preferable for large text files.

6. WriteAllText vs AppendAllText
WriteAllText

Creates or overwrites the file.

File.WriteAllText(
    "log.txt",
    "Application started");
AppendAllText

Adds content to the end.

File.AppendAllText(
    "log.txt",
    "Application stopped");
7. Path Class

Never manually construct filesystem paths like:

string path = "C:\\Data\\" + fileName;

Prefer:

string path =
    Path.Combine("C:", "Data", fileName);

Important methods:

Path.Combine(...)
Path.GetFileName(...)
Path.GetDirectoryName(...)
Path.GetExtension(...)
Path.GetFileNameWithoutExtension(...)
Path.GetFullPath(...)
Path.GetTempPath(...)
Path.GetTempFileName(...)
8. File Extensions
string extension =
    Path.GetExtension("report.pdf");

Result:

.pdf

Filename:

string name =
    Path.GetFileName("C:\\Reports\\report.pdf");

Result:

report.pdf
9. FileInfo

FileInfo provides an object-oriented representation of a file.

FileInfo file =
    new("report.txt");

Console.WriteLine(file.Exists);
Console.WriteLine(file.Length);
Console.WriteLine(file.FullName);
Console.WriteLine(file.Extension);
Console.WriteLine(file.LastWriteTime);

Operations:

file.CopyTo("copy.txt");
file.MoveTo("new-name.txt");
file.Delete();
10. File vs FileInfo

| File                        | FileInfo                                   |
| --------------------------- | ------------------------------------------ |
| Static API                  | Instance API                               |
| Simple operations           | Object representing a file                 |
| Good for one-off operations | Useful when performing multiple operations |
| Easy syntax                 | Provides metadata through properties       |

Do not claim that one is universally faster.

Choose based on usage and readability.

11. Exceptions

File operations can fail because of:

File not found
Directory not found
Access denied
Invalid path
File already exists
File locked by another process
Insufficient storage
Permission issues

Example:

try
{
    string content =
        File.ReadAllText("data.txt");
}
catch (IOException ex)
{
    Console.WriteLine(ex.Message);
}

Do not blindly catch Exception.

Catch only when you can meaningfully handle or translate the error.

12. IDisposable

Streams usually implement IDisposable.

Prefer:

using FileStream stream =
    File.OpenRead("data.txt");

or:

using (FileStream stream =
       File.OpenRead("data.txt"))
{
    // use stream
}

using ensures resources are disposed even when an exception occurs.

13. Product-Company Considerations

When implementing file handling:

Validate paths.
Do not trust user-provided filenames.
Avoid loading huge files completely into memory.
Dispose streams.
Prefer async I/O in server applications.
Handle cancellation.
Handle permissions and I/O failures.
Consider concurrent access.
Avoid blocking ASP.NET Core request threads unnecessarily.
Use object/blob storage for production-scale file storage where appropriate.
14. Interview Questions
What is the difference between File and FileInfo?

File provides static helper methods, while FileInfo represents a specific file as an object and provides instance operations and metadata.

Why use Path.Combine?

It constructs paths using platform-appropriate separators and avoids fragile manual string concatenation.

ReadAllText vs ReadLines?

ReadAllText loads the complete file into memory. ReadLines allows incremental line-by-line processing.

Why use using with FileStream?

To ensure the stream and its underlying resources are released deterministically.

Key Points
File → common file operations.
FileInfo → file object + metadata.
Path → safe path manipulation.
ReadLines → useful for large text files.
using → deterministic resource disposal.
Never trust user-provided paths.
Large files should generally be streamed.