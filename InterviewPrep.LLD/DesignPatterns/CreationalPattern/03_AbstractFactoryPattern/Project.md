Cloud Resource Management Platform

Imagine we are building a SaaS platform like Azure Portal.

Customers can select

Azure
AWS
Google Cloud

Once selected,

every operation must use services belonging to that provider.

Functional Requirements

Our platform supports

Storage
Upload File
Download File
Queue
Publish Message
Receive Message
Secret Manager
Get Secret
Save Secret

Business Service

↓

Should never know

Azure
AWS
Google

It only knows

ICloudServiceFactory

Exactly how Microsoft designs SDK abstractions.

Final Enterprise Folder Structure
03_AbstractFactoryPattern
│
├── Models
│      CloudFile.cs
│      Secret.cs
│
├── Enums
│      CloudProvider.cs
│
├── Interfaces
│      IStorageService.cs
│      IMessageQueueService.cs
│      ISecretManagerService.cs
│
│      ICloudServiceFactory.cs
│      ICloudPlatformService.cs
│
├── Implementations
│
│      Azure
│          AzureStorageService.cs
│          AzureQueueService.cs
│          AzureSecretManagerService.cs
│          AzureCloudServiceFactory.cs
│
│      Aws
│          AwsStorageService.cs
│          AwsQueueService.cs
│          AwsSecretManagerService.cs
│          AwsCloudServiceFactory.cs
│
│      GoogleCloud
│          GoogleStorageService.cs
│          GoogleQueueService.cs
│          GoogleSecretManagerService.cs
│          GoogleCloudServiceFactory.cs
│
├── Factories
│      CloudFactoryResolver.cs
│
├── Services
│      CloudPlatformService.cs
│
├── DependencyInjection
│      ServiceCollectionExtensions.cs
│
└── Program.cs
Enterprise Flow
Program.cs

↓

CloudPlatformService

↓

CloudFactoryResolver

↓

ICloudServiceFactory

↓

Azure Factory
        │
        ├──── Storage
        ├──── Queue
        └──── Secret

or

AWS Factory

or

Google Factory

Notice

The business service doesn't know

Azure

AWS

Google

Only

ICloudServiceFactory