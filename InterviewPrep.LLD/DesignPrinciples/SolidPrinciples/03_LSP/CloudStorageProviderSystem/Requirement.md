Business Requirement

A company develops an application that stores customer documents.

Initially it stores files in

Azure Blob Storage

After one year,

the customer wants

Amazon S3

After another year,

another customer wants

Google Cloud Storage

The developer should be able to replace

AzureBlobStorageProvider

with

AwsS3StorageProvider

without changing

FileStorageService

That is LSP.


------------------------------------------------------------------------------------

Enterprise Folder Structure
CloudStorageProviderSystem
│
├── Models
│     ├── UploadRequest.cs
│     ├── StorageFile.cs
│     └── UploadResult.cs
│
├── Interfaces
│     └── IStorageProvider.cs
│
├── Exceptions
│     └── StorageException.cs
│
├── Services
│     ├── AzureBlobStorageProvider.cs
│     ├── AwsS3StorageProvider.cs
│     ├── GoogleCloudStorageProvider.cs
│     └── FileStorageService.cs
│
└── Program.cs





