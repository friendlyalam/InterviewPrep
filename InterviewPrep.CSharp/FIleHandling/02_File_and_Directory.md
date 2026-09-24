# File and Directory Handling in C#

## 1. Directory Class

`Directory` provides static methods for directory operations.

Create:

```csharp
Directory.CreateDirectory("Reports");

Check existence:

bool exists =
    Directory.Exists("Reports");

Delete:

Directory.Delete("Reports");

Delete recursively:

Directory.Delete(
    "Reports",
    recursive: true);
2. Enumerating Files
string[] files =
    Directory.GetFiles("Reports");

foreach (string file in files)
{
    Console.WriteLine(file);
}

With pattern:

string[] pdfFiles =
    Directory.GetFiles(
        "Reports",
        "*.pdf");
3. Recursive Search
string[] files =
    Directory.GetFiles(
        "Reports",
        "*.pdf",
        SearchOption.AllDirectories);

SearchOption:

TopDirectoryOnly
AllDirectories

Be careful with recursive searches on very large directory trees.

4. EnumerateFiles

Instead of:

Directory.GetFiles(...)

you can use:

IEnumerable<string> files =
    Directory.EnumerateFiles(
        "Reports",
        "*.pdf",
        SearchOption.AllDirectories);

EnumerateFiles is useful when processing many files because results can be consumed incrementally.

5. EnumerateDirectories
IEnumerable<string> directories =
    Directory.EnumerateDirectories(
        "Reports");

foreach (string directory in directories)
{
    Console.WriteLine(directory);
}
6. DirectoryInfo
DirectoryInfo directory =
    new("Reports");

Console.WriteLine(directory.Exists);
Console.WriteLine(directory.FullName);

Create:

directory.Create();

Delete:

directory.Delete(
    recursive: true);

Files:

FileInfo[] files =
    directory.GetFiles();

Directories:

DirectoryInfo[] subDirectories =
    directory.GetDirectories();
7. FileInfo + DirectoryInfo

Useful when working with file metadata:

DirectoryInfo directory =
    new("Reports");

foreach (FileInfo file in directory.GetFiles())
{
    Console.WriteLine(file.Name);
    Console.WriteLine(file.Length);
    Console.WriteLine(file.Extension);
    Console.WriteLine(file.LastWriteTime);
}
8. File and Directory Relationships
Directory
   |
   +--- File
   |
   +--- File
   |
   +--- Directory
          |
          +--- File

Typical application:

Reports/
    2026/
        January.pdf
        February.pdf
    2025/
        December.pdf
9. Path + Directory

Prefer:

string reportsPath =
    Path.Combine(
        basePath,
        "Reports");

Directory.CreateDirectory(reportsPath);

Do not manually construct paths.

10. Safe Directory Creation

CreateDirectory is convenient because the directory is created if it does not already exist.

Directory.CreateDirectory(path);

You generally do not need:

if (!Directory.Exists(path))
{
    Directory.CreateDirectory(path);
}

for basic creation.

11. Moving Directories
Directory.Move(
    "OldReports",
    "NewReports");

Moving across different volumes/filesystems can have different behavior and may not be equivalent to a simple rename.

12. Important Production Considerations

Directory operations can fail due to:

Permission problems
Missing parent paths
Invalid paths
Files being locked
Large directory trees
Network filesystem issues
Storage failures

Do not assume filesystem operations are always successful.

13. Large Directory Trees

Prefer:

Directory.EnumerateFiles(...)

over:

Directory.GetFiles(...)

when you want to process a potentially large number of files incrementally.

14. Interview Questions
GetFiles vs EnumerateFiles?

GetFiles returns an array containing all matching files. EnumerateFiles returns an enumerable that can be consumed incrementally.

Directory vs DirectoryInfo?

Directory is static. DirectoryInfo represents a specific directory and provides instance operations and metadata.

How do you recursively search files?
Directory.EnumerateFiles(
    path,
    "*.pdf",
    SearchOption.AllDirectories);
Key Points
Directory → static directory operations.
DirectoryInfo → directory object + metadata.
EnumerateFiles → better for large enumeration.
SearchOption.AllDirectories → recursive search.
Use Path.Combine.
Validate paths and handle permissions.