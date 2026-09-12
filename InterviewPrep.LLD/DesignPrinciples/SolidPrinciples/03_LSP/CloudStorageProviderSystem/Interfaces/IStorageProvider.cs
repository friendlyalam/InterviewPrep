using InterviewPrep.LLD.SolidPrinciples.LSP.CloudStorageProviderSystem.Models;

namespace InterviewPrep.LLD.SolidPrinciples.LSP.CloudStorageProviderSystem.Interfaces
{
    public interface IStorageProvider
    {
        UploadResult Upload(UploadRequest request);

        StorageFile Download(Guid fileId);

        void Delete(Guid fileId);
    }
}

//This interface is

//the contract.

//Every provider must follow it.

//Whether it is

//Azure

//or

//AWS

//or

//Google

//the consumer should never care.