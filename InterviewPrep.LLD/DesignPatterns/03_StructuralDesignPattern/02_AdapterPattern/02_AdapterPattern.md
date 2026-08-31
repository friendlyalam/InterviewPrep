Definition

Adapter Pattern converts the interface of one class into another interface that the client expects.

Simple definition:

Adapter acts like a translator between two incompatible interfaces.

--------------------------------------------------------------------------------------------------------

Intent

Suppose your application expects

ICloudStorageService

But Azure SDK provides

AzureBlobClient

Google SDK provides

GoogleCloudStorageClient

AWS SDK provides

AmazonS3Client

Different APIs.

Different method names.

Different request objects.

Different response objects.

Adapter converts all of them into one common interface.

--------------------------------------------------------------------------------------------------------


Real Life Example 1
Mobile Charger

Indian Socket

↓

Adapter

↓

US Charger

Without adapter

❌ Doesn't fit.

With adapter

✅ Works perfectly.



Real Life Example 2
Language Translator

English Speaker

↓

Translator

↓

Japanese Speaker

Both can communicate.

Translator = Adapter

--------------------------------------------------------------------------------------------------------


Product Company Example

We'll build

Multi Cloud Storage System

Exactly the kind of project used inside Microsoft, Amazon and Google.

Supported Providers

Application

      │

      ▼

ICloudStorageService

      ▲

 ┌────┼───────────┐
 │    │           │
 ▼    ▼           ▼

Azure Adapter
Google Adapter
AWS Adapter

Notice

Application only knows

ICloudStorageService

It never knows Azure SDK.

It never knows Google SDK.

It never knows AWS SDK.

--------------------------------------------------------------------------------------------------------

Why This Project?

Because almost every enterprise product supports multiple cloud providers.

Examples

Azure Blob Storage
Amazon S3
Google Cloud Storage
MinIO
DigitalOcean Spaces

They all expose different SDKs.

Without Adapter
if(provider=="Azure")
{
    AzureBlobClient.Upload();
}
else if(provider=="AWS")
{
    AmazonS3Client.PutObject();
}
else if(provider=="Google")
{
    GoogleStorageClient.UploadObject();
}

After two years...

25 Providers

↓

1200-line class
With Adapter
Application

↓

ICloudStorageService

↓

Azure Adapter

or

AWS Adapter

or

Google Adapter

No if-else.

No switch.

--------------------------------------------------------------------------------------------------------

Advantages

✅ Open/Closed Principle

✅ Easy Provider Replacement

✅ Third-party SDK Isolation

✅ Easier Testing

✅ Cleaner Architecture

✅ Vendor Independence

--------------------------------------------------------------------------------------------------------

Disadvantages

❌ More Classes

❌ Small Performance Overhead

❌ Slightly More Complex Structure

--------------------------------------------------------------------------------------------------------

When to Use

Use Adapter when:

Third-party APIs differ.
Legacy code must integrate with new systems.
Multiple providers expose different interfaces.
Vendor lock-in should be avoided.


When NOT to Use

Don't use Adapter when:

You control both interfaces.
The APIs are already compatible.
There's only one provider and no chance of replacement.
Adapter vs Strategy

--------------------------------------------------------------------------------------------------------

A favorite interview question.

| Adapter                            | Strategy                  |
| ---------------------------------- | ------------------------ |
| Converts interfaces                | Changes algorithms        |
| Hides API differences              | Hides algorithm differences|
| Structural                         | Behavioral                |
| Client expects one interface       | Client chooses one algorithm|

Example:

Strategy

Festival Pricing

↓

Premium Pricing

Different pricing algorithms.

Adapter

Azure SDK

↓

Google SDK

↓

AWS SDK

Different SDK interfaces.

Adapter vs Decorator
| Adapter                            | Decorator                |
| ---------------------------------- | ------------------------ |
| Changes interface                  | Adds behavior            |
| Translator                         | Wrapper                  |
| Makes incompatible code compatible | Enhances existing object |


--------------------------------------------------------------------------------------------------------


Enterprise Project
Multi Cloud Storage System

Project Structure
07_AdapterPattern
│
├── Models
│      FileUploadRequest.cs
│      UploadResult.cs
│
├── Interfaces
│      ICloudStorageService.cs
│
├── ThirdParty
│      AzureBlobClient.cs
│      GoogleCloudStorageClient.cs
│      AmazonS3Client.cs
│
├── Adapters
│      AzureStorageAdapter.cs
│      GoogleStorageAdapter.cs
│      AmazonS3Adapter.cs
│
└── Program.cs

Total Classes: 9

No DependencyInjection folder.

No ServiceCollectionExtensions.

Because Adapter is about interface conversion, not dependency registration.

--------------------------------------------------------------------------------------------------------

Architecture
Program

      │

      ▼

ICloudStorageService

      ▲

 ┌────┼───────────┐
 │    │           │
 ▼    ▼           ▼

Azure Adapter
Google Adapter
Amazon Adapter

      │
      ▼

Third-party SDK
SOLID Principles
| Principle | Usage                                                           |
| --------- | --------------------------------------------------------------- |
| SRP       | Each adapter handles one provider.                              |
| OCP       | Add a new cloud provider without modifying existing adapters.   |
| LSP       | Any adapter can replace another through `ICloudStorageService`. |
| ISP       | Small, focused interface for storage operations.                |
| DIP       | Client depends on `ICloudStorageService`, not vendor SDKs.      |

--------------------------------------------------------------------------------------------------------

Product Company Discussion

Where you'll see Adapter:

| Company   | Example                        |
| --------- | ------------------------------ |
| Microsoft | Azure SDK wrappers             |
| Amazon    | Payment gateway integrations   |
| Google    | Google Cloud SDK integrations  |
| Uber      | Maps provider integrations     |
| Walmart   | Shipping provider integrations |


--------------------------------------------------------------------------------------------------------
Interview Questions
What problem does Adapter solve?
Adapter vs Strategy?
Adapter vs Decorator?
Why wrap third-party SDKs instead of using them directly?
How does Adapter reduce vendor lock-in?
Can Adapter be combined with Factory? (Yes, a Factory can choose which Adapter to create.)