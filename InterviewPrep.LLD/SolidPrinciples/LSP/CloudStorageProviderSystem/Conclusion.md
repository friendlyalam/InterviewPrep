Why these three methods?

Every cloud storage provider supports

✅ Upload

✅ Download

✅ Delete

That makes them perfect candidates for substitution.

Why not include methods like
GenerateSasToken()

SetLifecyclePolicy()

EnableVersioning()

Because

Azure supports some features

AWS supports similar but different APIs

Google has its own capabilities

If we force every provider to implement provider-specific features,

they may throw

throw new NotSupportedException();

That would violate LSP.

A good abstraction contains only behavior that every implementation can honor.

Object Diagram
                IStorageProvider
                      ▲
                      │
          -------------------------
          │          │            │
          │          │            │
 AzureBlobStorage  AwsS3Storage  GoogleCloudStorage

Notice

No provider has extra mandatory methods.

Every provider promises exactly the same contract.

Interview Question
Why is interface design important for LSP?

Because a bad interface forces some implementations to fake behavior or throw exceptions, which breaks substitutability.

A well-designed interface contains only the operations that every implementation can genuinely support.

Product Company Insight

Many developers think:

LSP is only about inheritance.

In reality,

LSP applies equally to:

Interfaces ✅
Abstract classes ✅
Base classes ✅

In modern ASP.NET Core applications, interfaces are used far more often than inheritance, so understanding LSP in the context of interfaces is essential.

---------------------------------------------------------------

Console Output (Azure)
===== File Upload Started =====

Uploading file to Azure Blob Storage...

File uploaded successfully to Azure.

===== File Upload Completed =====

Provider : Azure Blob Storage

URL : https://azure.blob.com/Resume.pdf
Why This Demonstrates LSP

The FileStorageService depends only on this contract:

IStorageProvider

It never checks:

if(storageProvider is AzureBlobStorageProvider)

or

switch(provider)

Every implementation can be substituted without changing the client code or breaking its behavior.

What Would Violate LSP?

Suppose someone writes:

public class LegacyStorageProvider : IStorageProvider
{
    public UploadResult Upload(UploadRequest request)
    {
        throw new NotSupportedException();
    }

    public StorageFile Download(Guid fileId)
    {
        // implementation
    }

    public void Delete(Guid fileId)
    {
        // implementation
    }
}

This class implements IStorageProvider but does not honor its contract. Any code that expects every storage provider to support uploads will fail at runtime.

This is an LSP violation.

Product Company Discussion

If I were reviewing this code in a product company, I'd approve the overall design because:

✅ The client depends on an abstraction (IStorageProvider).
✅ Constructor injection enables substitution.
✅ Every implementation fulfills the same behavioral contract.
✅ There are no provider-specific type checks.

One improvement I'd suggest is to remove the duplicated ValidateRequest method from all three providers. 
A better design would be to introduce a shared validation component (or a base class if appropriate) so the
validation logic is written once. We'll intentionally postpone that discussion until we study design patterns 
and code reuse, because the focus here is understanding LSP, not every possible refactoring.