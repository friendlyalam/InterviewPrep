# File Security and Safe File Handling

## 1. Never Trust File Input

File paths and filenames coming from users are untrusted input.

Dangerous:

```csharp
string path =
    Path.Combine(
        uploadDirectory,
        userProvidedFileName);

Simply combining paths does not automatically make the operation secure.

2. Path Traversal

A malicious user may attempt:

../../secret.txt

or platform-specific equivalent paths.

The goal is to escape the intended directory.

Never allow arbitrary user input to directly determine filesystem locations.

3. Safe Filename Strategy

A strong approach is to generate your own storage name.

Example:

string storageName =
    Guid.NewGuid().ToString("N");

Keep the original filename only as metadata if required.

Concept:

User filename:
invoice.pdf

Storage key:
8d7f...abc
4. Extension Validation

Do not rely only on the extension.

Example:

.exe
.jpg
.pdf

A file can be renamed.

For sensitive applications, consider:

Extension allowlist
MIME/content validation
File signature/magic bytes where appropriate
Maximum file size
Malware scanning where required
5. Allowlist

Prefer:

Allowed:
.pdf
.jpg
.png

over:

Block:
.exe
.bat
.cmd
...

An allowlist is generally easier to reason about.

6. File Size Limits

Always consider upload size.

Example policy:

Maximum file size = 10 MB

Do not allow unlimited uploads.

Limits protect against:

Memory exhaustion
Storage exhaustion
Denial-of-service scenarios
7. Storage Location

Avoid storing user-uploaded files directly in locations where they can execute as application code.

Prefer a dedicated storage location or object storage.

Concept:

Application
    |
    +---- Database
    |
    +---- File/Object Storage

Store metadata in the database.

8. Database Metadata

Example:

File
------------------------
Id
OriginalName
StorageKey
ContentType
Size
UploadedBy
CreatedAt
Hash

The database stores metadata.

The actual large content can be stored separately.

9. Permissions

The application process should have only the permissions it actually needs.

Principle:

Least privilege.

Do not run an application with unnecessarily broad filesystem permissions.

10. Sensitive Files

Examples:

Passwords
Private keys
Secrets
Identity documents
Medical documents
Financial documents

These require stronger controls.

Consider:

Access control
Encryption
Audit logging
Secure storage
Retention policies
Secure deletion requirements
11. File Names and Logging

Be careful when logging user-controlled filenames.

Potentially malicious strings can:

pollute logs
confuse monitoring
create log injection problems

Sanitize/structure logs appropriately.

12. Race Conditions

This is dangerous:

if (!File.Exists(path))
{
    File.WriteAllText(path, content);
}

Another process can create the file between the check and write.

This is a classic:

Check → Act

race.

Prefer atomic operations or appropriate file modes when correctness matters.

13. Temporary Files

Temporary files may contain sensitive information.

Consider:

Unique names
Restricted permissions
Cleanup
Appropriate storage location
Encryption when required
14. Symlinks and Reparse Points

Advanced filesystem scenarios may involve symbolic links/reparse points.

A path that appears to be inside an allowed directory may resolve somewhere else.

Security-sensitive applications should consider filesystem resolution behavior rather than relying only on string checks.

15. Upload Security Flow

A production upload can look like:

Request
   ↓
Authentication
   ↓
Authorization
   ↓
Size validation
   ↓
Filename handling
   ↓
Content-type validation
   ↓
Content validation/scanning
   ↓
Generate storage key
   ↓
Store file
   ↓
Store metadata
   ↓
Audit
16. Do Not Trust Content-Type

Client-provided MIME type can be incorrect.

Example:

Content-Type: image/jpeg

does not guarantee the file is actually a JPEG.

Use appropriate server-side validation.

17. Product-Company Scenario

Requirement:

Users upload identity documents.

Production considerations:

Authentication
Authorization
      ↓
Upload size limit
      ↓
Allowed file types
      ↓
Content validation
      ↓
Malware scanning
      ↓
Private object storage
      ↓
Metadata database
      ↓
Audit trail

Do not expose the raw storage path directly.

Interview Questions
What is path traversal?

An attack where crafted path input attempts to access files outside the intended directory.

How do you safely store uploaded files?

Generate server-side storage keys, validate content, enforce size limits, use controlled storage, and store metadata separately.

Is checking the file extension enough?

No.

Why use an allowlist?

It restricts accepted input to explicitly supported file types.

What is least privilege?

Granting the application only the permissions required to perform its work.

Key Points
Never trust user-provided paths.
Avoid direct use of user filenames as storage paths.
Generate server-side storage keys.
Validate size and content.
Prefer allowlists.
Apply least privilege.
Protect sensitive files.
Consider path traversal and symlink/reparse-point issues.
Do not expose physical storage paths.