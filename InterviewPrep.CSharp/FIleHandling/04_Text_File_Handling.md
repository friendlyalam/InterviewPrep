# Text File Handling in C#

## 1. Text vs Binary

Text files contain characters:

```text
.txt
.csv
.json
.xml

Binary files contain raw bytes:

.jpg
.png
.pdf
.zip
.mp4

Text handling requires character encoding.

2. Encoding

Common encodings:

UTF-8
UTF-16
ASCII

UTF-8 is the common default choice for modern applications.

Example:

using System.Text;

byte[] bytes =
    Encoding.UTF8.GetBytes("Hello");

Convert back:

string text =
    Encoding.UTF8.GetString(bytes);
3. ReadAllText
string content =
    File.ReadAllText("data.txt");

Simple but loads the entire file into memory.

Good for reasonably small files.

4. WriteAllText
File.WriteAllText(
    "data.txt",
    "Hello World");

Existing content is replaced.

5. AppendAllText
File.AppendAllText(
    "data.txt",
    Environment.NewLine +
    "Another line");

Adds content to the end.

6. ReadAllLines
string[] lines =
    File.ReadAllLines("data.txt");

All lines are loaded into memory.

7. ReadLines
foreach (string line in
         File.ReadLines("data.txt"))
{
    Console.WriteLine(line);
}

Useful for large text files.

8. StreamReader
using StreamReader reader =
    new("data.txt");

while (!reader.EndOfStream)
{
    string? line =
        reader.ReadLine();

    Console.WriteLine(line);
}
9. StreamWriter
using StreamWriter writer =
    new("log.txt");

writer.WriteLine(
    "Application started");

writer.WriteLine(
    "Processing request");
10. Encoding with StreamReader
using StreamReader reader =
    new(
        "data.txt",
        Encoding.UTF8);

The encoding must match the file's actual encoding.

11. CSV Files

CSV commonly contains:

Id,Name,Age
1,John,30
2,Alice,28

Do not assume that simple:

line.Split(',')

is a complete CSV parser.

Real CSV can contain:

"Smith, John"

quoted values, escaped quotes, delimiters inside fields, etc.

For production applications, use an appropriate CSV library when CSV complexity requires it.

12. JSON Files

For JSON, prefer a serializer rather than manually parsing strings.

Modern .NET:

using System.Text.Json;

string json =
    JsonSerializer.Serialize(customer);

Deserialize:

Customer? customer =
    JsonSerializer.Deserialize<Customer>(
        json);

For ASP.NET Core applications, JSON is normally handled by the framework's serialization pipeline.

13. Large Text Files

Avoid:

string content =
    File.ReadAllText("huge.log");

Prefer:

foreach (string line in
         File.ReadLines("huge.log"))
{
    Process(line);
}

This reduces memory usage.

14. Line-by-Line Processing

Typical processing:

Read line
   ↓
Validate
   ↓
Parse
   ↓
Process
   ↓
Continue

This is useful for:

Logs
CSV imports
Batch files
ETL
Large reports
15. Common Mistake

Do not use:

File.ReadAllText(...)

automatically for every file.

Consider:

File size
Memory usage
Processing requirement
Streaming requirement
Interview Questions
ReadAllText vs ReadLines?

ReadAllText loads the complete content into memory. ReadLines allows incremental processing.

StreamReader vs ReadAllText?

StreamReader provides streaming/control over reading, while ReadAllText is a convenient high-level operation.

Why does encoding matter?

Characters must be converted to and from bytes. Incorrect encoding can produce corrupted text.

Should I manually parse CSV with Split(',')?

Not for production-grade CSV when quoted fields or escaped delimiters are possible.

Key Points
Text is characters; binary is bytes.
Encoding matters.
UTF-8 is the common modern choice.
Small files → ReadAllText.
Large files → ReadLines/StreamReader.
Use serializers for JSON.
Use proper CSV libraries for complex CSV.