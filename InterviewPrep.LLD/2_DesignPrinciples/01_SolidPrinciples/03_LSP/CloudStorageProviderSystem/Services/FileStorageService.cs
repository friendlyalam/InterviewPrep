
using InterviewPrep.LLD.SolidPrinciples.LSP.CloudStorageProviderSystem.Interfaces;
using InterviewPrep.LLD.SolidPrinciples.LSP.CloudStorageProviderSystem.Models;

namespace InterviewPrep.LLD.SolidPrinciples.LSP.CloudStorageProviderSystem.Services
{
//    This is the client class.

//Notice that it doesn't know whether it's using Azure, AWS, or Google.
        public class FileStorageService
        {
            private readonly IStorageProvider _storageProvider;

            public FileStorageService(IStorageProvider storageProvider)
            {
                _storageProvider = storageProvider;
            }

            public UploadResult UploadFile(UploadRequest request)
            {
                Console.WriteLine("===== File Upload Started =====");

                UploadResult result = _storageProvider.Upload(request);

                Console.WriteLine(result.Message);

                Console.WriteLine("===== File Upload Completed =====");

                return result;
            }

            public StorageFile DownloadFile(Guid fileId)
            {
                return _storageProvider.Download(fileId);
            }

            public void DeleteFile(Guid fileId)
            {
                _storageProvider.Delete(fileId);
            }
        }
    }
