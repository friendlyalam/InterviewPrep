| Adapter              | Calls            | Returns        |
| -------------------- | ---------------- | -------------- |
| AzureStorageAdapter  | `UploadBlob()`   | `UploadResult` |
| AmazonS3Adapter      | `PutObject()`    | `UploadResult` |
| GoogleStorageAdapter | `UploadObject()` | `UploadResult` |

Why This Is Powerful

Without Adapter

Application

↓

Azure SDK

Tomorrow

Need AWS.

Entire application changes.

With Adapter

Application

↓

ICloudStorageService

↓

Adapter

↓

SDK

Only adapter changes.

Enterprise Improvement ⭐⭐⭐⭐⭐

The constructors currently contain

new AzureBlobClient();

Would I write this in a real Microsoft or Amazon codebase?

No.

In enterprise applications, the SDK client itself is usually injected.

Example:

public sealed class AzureStorageAdapter : ICloudStorageService
{
    private readonly AzureBlobClient _client;

    public AzureStorageAdapter(AzureBlobClient client)
    {
        _client = client;
    }

    ...
}

Why?

Better testability
Centralized SDK configuration
Reuse expensive SDK clients
Easier mocking in unit tests

For learning the Adapter Pattern, creating the SDK client inside the adapter keeps the example focused. In production, prefer constructor injection.

SOLID Principles
Principle	Usage
SRP	One adapter per provider.
OCP	Add a new provider by creating a new adapter.
LSP	Any adapter can replace another through ICloudStorageService.
ISP	Small storage interface.
DIP	Business code depends only on ICloudStorageService. (In production, the adapter would also receive the SDK client through DI.)
Product Company Discussion

This pattern appears everywhere:

Microsoft
Application

↓

AzureStorageAdapter

↓

Azure Blob SDK
Amazon
Application

↓

S3Adapter

↓

AWS SDK
Google
Application

↓

GoogleStorageAdapter

↓

Google Cloud SDK
Interview Questions
Q1 Why does every adapter implement the same interface?

So the client can work with any provider without changing its own code.

Q2 Why doesn't the application call the SDK directly?

To avoid vendor lock-in and isolate SDK-specific changes.

Q3 Why convert responses into UploadResult?

To expose a consistent business model instead of leaking provider-specific response types throughout the application.


---------------------------------------------------------------------------------------------------------------------------

Output

Azure

Uploading to Azure Blob Storage...

Provider : Azure
Success  : True
URL      : https://azurestorage.blob.core.windows.net/documents/resume.pdf
Message  : Upload Successful

AWS

Uploading to Amazon S3...

Provider : AWS
Success  : True
URL      : https://s3.amazonaws.com/documents/resume.pdf
Message  : Upload Successful

Google

Uploading to Google Cloud Storage...

Provider : Google
Success  : True
URL      : https://storage.googleapis.com/documents/resume.pdf
Message  : Upload Successful
Switching Providers

The only change needed is:

new AzureStorageAdapter(...)

↓

new AmazonS3Adapter(...)

↓

new GoogleStorageAdapter(...)

The business code doesn't change.

Product Company Improvement ⭐⭐⭐⭐⭐

In a real enterprise application, you typically wouldn't change Program.cs to switch providers.

Instead, you'd combine Factory Method + Adapter.

Configuration

↓

StorageFactory

↓

AzureStorageAdapter

or

AmazonS3Adapter

or

GoogleStorageAdapter

Then your business code simply receives:

ICloudStorageService

This is a very common architecture in Microsoft and Amazon codebases.

Real Examples
Microsoft
Application

↓

Blob Storage Adapter

↓

Azure SDK
Amazon
Application

↓

S3 Adapter

↓

AWS SDK
Google
Application

↓

Cloud Storage Adapter

↓

Google SDK
Adapter + Factory

This combination is extremely common.

Factory

↓

Creates

↓

Correct Adapter

↓

Calls SDK

This keeps both object creation and interface translation separate.

Interview Questions
Q1. Why not call Azure SDK directly?

Because it tightly couples your application to a specific vendor and makes migration difficult.

Q2. Adapter vs Facade?
Adapter	Facade
Converts one interface into another	Simplifies a complex subsystem
Solves incompatibility	Hides complexity
Q3. Why inject SDK clients?

Because SDK clients often manage connections, authentication, and configuration, making them better candidates for dependency injection and reuse.

Q4. Can Adapter be combined with Factory?

Yes. Factory selects the appropriate adapter, and the adapter translates calls to the provider-specific SDK.

Common Mistakes

❌ Exposing SDK response objects throughout the application.

❌ Calling vendor SDKs directly from business logic.

❌ Putting business rules inside adapters.

❌ Using adapters to choose providers (that is the Factory's responsibility).