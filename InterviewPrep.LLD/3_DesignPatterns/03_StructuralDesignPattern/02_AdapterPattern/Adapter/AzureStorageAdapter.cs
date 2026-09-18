using InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._02_AdapterPattern.Interfaces;
using InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._02_AdapterPattern.Models;
using InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._02_AdapterPattern.ThirdParty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._02_AdapterPattern.Adapter
{
    public sealed class AzureStorageAdapter : ICloudStorageService
    {
        private readonly AzureBlobClient _azureBlobClient;

        public AzureStorageAdapter(AzureBlobClient azureBlobClient)
        {
            _azureBlobClient = azureBlobClient;
        }

        public UploadResult Upload(FileUploadRequest request)
        {
            string fileUrl = _azureBlobClient.UploadBlob(
                request.Content,
                request.FileName,
                request.FolderName,
                request.ContentType);

            return new UploadResult
            {
                Success = true,
                FileUrl = fileUrl,
                Provider = "Microsoft Azure",
                Message = "File uploaded successfully."
            };
        }
    }
}

//What Happened?

//Application calls

//Upload(request)

//Adapter converts it into

//UploadBlob(
//    content,
//    blobName,
//    container,
//    contentType)

//The application never knows.