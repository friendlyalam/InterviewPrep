

using InterviewPrep.LLD.SolidPrinciples.LSP.CloudStorageProviderSystem.Exceptions;

namespace InterviewPrep.LLD.SolidPrinciples.LSP.CloudStorageProviderSystem.Exceptions
{
    public class StorageException: Exception
    {
        public StorageException(string message)
        : base(message)
        {
        }
    }
}

//Why custom exception?

//Instead of

//throw new Exception("Invalid File");

//enterprise applications throw

//throw new StorageException("...");

//It is

//meaningful
//searchable
//easier to handle