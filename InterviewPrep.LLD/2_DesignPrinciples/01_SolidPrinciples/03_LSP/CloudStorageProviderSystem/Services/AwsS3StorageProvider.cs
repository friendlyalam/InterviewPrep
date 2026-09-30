
using InterviewPrep.LLD.SolidPrinciples.LSP.CloudStorageProviderSystem.Exceptions;
using InterviewPrep.LLD.SolidPrinciples.LSP.CloudStorageProviderSystem.Interfaces;
using InterviewPrep.LLD.SolidPrinciples.LSP.CloudStorageProviderSystem.Models;

namespace InterviewPrep.LLD.SolidPrinciples.LSP.CloudStorageProviderSystem.Services
{
    // Notice that nothing changes in the contract.
    public class AwsS3StorageProvider : IStorageProvider
    {
        public UploadResult Upload(UploadRequest request)
        {
            ValidateRequest(request);

            StorageFile file = new StorageFile
            {
                FileId = Guid.NewGuid(),
                FileName = request.FileName,
                StorageProvider = "AWS S3",
                FileUrl = $"https://aws.s3.com/{request.FileName}",
                UploadedOn = DateTime.UtcNow
            };

            Console.WriteLine("Uploading file to AWS S3...");

            return new UploadResult
            {
                IsSuccess = true,
                Message = "File uploaded successfully to AWS.",
                File = file
            };
        }

        public StorageFile Download(Guid fileId)
        {
            Console.WriteLine($"Downloading file from AWS S3. FileId: {fileId}");

            return new StorageFile
            {
                FileId = fileId,
                FileName = "AwsDocument.pdf",
                StorageProvider = "AWS S3",
                FileUrl = $"https://aws.s3.com/{fileId}",
                UploadedOn = DateTime.UtcNow
            };
        }

        public void Delete(Guid fileId)
        {
            Console.WriteLine($"Deleting file from AWS S3. FileId: {fileId}");
        }

        private void ValidateRequest(UploadRequest request)
        {
            if (request == null)
                throw new StorageException("Upload request cannot be null.");

            if (string.IsNullOrWhiteSpace(request.FileName))
                throw new StorageException("File name is required.");

            if (request.FileContent == null || request.FileContent.Length == 0)
                throw new StorageException("File content cannot be empty.");

            if (request.FileSizeInBytes <= 0)
                throw new StorageException("Invalid file size.");
        }
    }
}
