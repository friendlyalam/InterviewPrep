Program Flow
Program.cs
      │
      ▼
DI Container
      │
      ▼
CloudPlatformService
      │
      ▼
CloudFactoryResolver
      │
      ▼
AzureCloudServiceFactory
      │
      ├────────► AzureStorageService
      ├────────► AzureQueueService
      └────────► AzureSecretManagerService
Console Output
[Azure Key Vault] Reading secret 'StorageConnection'

[Azure Blob Storage] Uploading 'EmployeeReport.pdf'...

[Azure Queue] Published : Backup completed for EmployeeReport.pdf

Secret Value : AzureSecretValue
Testing Different Providers
Azure
cloudPlatformService.Backup(
    CloudProvider.Azure,
    file);

Output

Azure Blob Storage

Azure Queue

Azure Key Vault
AWS
cloudPlatformService.Backup(
    CloudProvider.Aws,
    file);

Output

Amazon S3

Amazon SQS

AWS Secrets Manager
Google Cloud
cloudPlatformService.Backup(
    CloudProvider.GoogleCloud,
    file);

Output

Google Cloud Storage

Google Pub/Sub

Google Secret Manager

No business code changes.

Only the provider changes.

Final Object Flow
                    Program.cs
                         │
                         ▼
              ICloudPlatformService
                         │
                         ▼
              ICloudFactoryResolver
                         │
                         ▼
         Dictionary<CloudProvider, ICloudServiceFactory>
                         │
         ┌───────────────┼───────────────┐
         ▼               ▼               ▼
 Azure Factory      AWS Factory     Google Factory
         │               │               │
         ▼               ▼               ▼
 Storage          Storage         Storage
 Queue            Queue           Queue
 Secret           Secret          Secret
Where is the Abstract Factory?

Many developers ask this in interviews.

The pattern is not the resolver.

The pattern is here:

ICloudServiceFactory
        │
        ├─────────────► CreateStorageService()

        ├─────────────► CreateQueueService()

        └─────────────► CreateSecretManagerService()

Each concrete factory creates a family of related objects.

That's the Abstract Factory.

What is CloudFactoryResolver?

It is an enterprise helper.

It selects the correct factory.

It is not part of the original GoF pattern.

Many enterprise applications add a resolver, registry, or provider to bridge runtime selection with the Abstract Factory.

SOLID Principles Used
| Principle | Implementation                                                                                                                            |
| --------- | ----------------------------------------------------------------------------------------------------------------------------------------- |
| SRP       | Every service has one responsibility.                                                                                                     |
| OCP       | New cloud providers can be added without modifying existing business logic or the resolver (with the provider-based dictionary approach). |
| LSP       | Any cloud provider can replace another through `ICloudServiceFactory`.                                                                    |
| ISP       | Separate interfaces for storage, queue, and secret management.                                                                            |
| DIP       | Business layer depends only on abstractions.                                                                                              |

Why This Is Enterprise Ready
Feature	Implemented
Constructor Injection	✅
Dependency Injection	✅
Interfaces	✅
Loose Coupling	✅
Abstract Factory	✅
Factory Resolver	✅
OCP	✅
Clean Folder Structure	✅
Composition Root	✅
No Service Locator in business code	✅
No new in business services	✅
How to Add Oracle Cloud
Step 1

Create:

OracleStorageService
OracleQueueService
OracleSecretManagerService
Step 2

Create:

OracleCloudServiceFactory
Step 3

Register in DI

services.AddTransient<IStorageService, OracleStorageService>();

services.AddTransient<IMessageQueueService, OracleQueueService>();

services.AddTransient<ISecretManagerService, OracleSecretManagerService>();

services.AddTransient<ICloudServiceFactory, OracleCloudServiceFactory>();
Step 4

Add enum value

Oracle

Done.

No business service changes.

No resolver changes.

Common Interview Questions
Q1. Why not inject AzureStorageService directly?

Because the provider is selected at runtime. Depending on concrete implementations would tightly couple the business service to one provider and violate DIP.

Q2. Why inject IEnumerable<T>?

To allow the DI container to provide all implementations. The factory can then choose the correct implementation based on its Provider.

Q3. Why use a dictionary in CloudFactoryResolver?

To avoid repeated searches through the collection and provide O(1) lookup by CloudProvider.

Q4. Is CloudFactoryResolver part of the Abstract Factory pattern?

No. It is an enterprise addition used to select the appropriate factory at runtime.

Q5. Where is the actual Abstract Factory?

ICloudServiceFactory and its implementations (AzureCloudServiceFactory, AwsCloudServiceFactory, GoogleCloudServiceFactory) form the Abstract Factory because they create families of related objects.

Final Enterprise Rating
| Area                      | Rating |
| ------------------------- | ------ |
| Product Company Relevance | ⭐⭐⭐⭐⭐  |
| Microsoft Interview       | ⭐⭐⭐⭐⭐  |
| Amazon Interview          | ⭐⭐⭐⭐⭐  |
| Clean Architecture        | ⭐⭐⭐⭐⭐  |
| SOLID Usage               | ⭐⭐⭐⭐⭐  |
| Dependency Injection      | ⭐⭐⭐⭐⭐  |
| Maintainability           | ⭐⭐⭐⭐⭐  |
| Extensibility             | ⭐⭐⭐⭐⭐  |

Abstract Factory Pattern Status
✅ Theory Complete
✅ Enterprise Architecture
✅ Real Product Example
✅ Complete C# Project
✅ Dependency Injection
✅ Interview Questions
✅ Product Company Standards