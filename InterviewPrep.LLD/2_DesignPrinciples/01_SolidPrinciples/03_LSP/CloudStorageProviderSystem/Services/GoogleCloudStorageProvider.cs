using InterviewPrep.LLD.SolidPrinciples.LSP.CloudStorageProviderSystem.Exceptions;
using InterviewPrep.LLD.SolidPrinciples.LSP.CloudStorageProviderSystem.Interfaces;
using InterviewPrep.LLD.SolidPrinciples.LSP.CloudStorageProviderSystem.Models;

namespace InterviewPrep.LLD.SolidPrinciples.LSP.CloudStorageProviderSystem.Services
{
    public class GoogleCloudStorageProvider : IStorageProvider
    {
        public UploadResult Upload(UploadRequest request)
        {
            ValidateRequest(request);

            StorageFile file = new StorageFile
            {
                FileId = Guid.NewGuid(),
                FileName = request.FileName,
                StorageProvider = "Google Cloud Storage",
                FileUrl = $"https://gcs.google.com/{request.FileName}",
                UploadedOn = DateTime.UtcNow
            };

            Console.WriteLine("Uploading file to Google Cloud Storage...");

            return new UploadResult
            {
                IsSuccess = true,
                Message = "File uploaded successfully to Google Cloud.",
                File = file
            };
        }

        public StorageFile Download(Guid fileId)
        {
            Console.WriteLine($"Downloading file from Google Cloud Storage. FileId: {fileId}");

            return new StorageFile
            {
                FileId = fileId,
                FileName = "GoogleDocument.pdf",
                StorageProvider = "Google Cloud Storage",
                FileUrl = $"https://gcs.google.com/{fileId}",
                UploadedOn = DateTime.UtcNow
            };
        }

        public void Delete(Guid fileId)
        {
            Console.WriteLine($"Deleting file from Google Cloud Storage. FileId: {fileId}");
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
