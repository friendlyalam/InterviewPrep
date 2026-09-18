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
    public sealed class GoogleStorageAdapter : ICloudStorageService
    {
        private readonly GoogleCloudStorageClient _googleCloudStorageClient;

        public GoogleStorageAdapter(GoogleCloudStorageClient googleCloudStorageClient)
        {
            _googleCloudStorageClient = googleCloudStorageClient;
        }

        public UploadResult Upload(FileUploadRequest request)
        {
            GoogleUploadResponse response =
                _googleCloudStorageClient.UploadObject(
                    request.FolderName,
                    request.FileName,
                    request.Content,
                    request.ContentType);

            return new UploadResult
            {
                Success = response.Success,
                FileUrl = response.FileUrl,
                Provider = "Google Cloud Storage",
                Message = response.Success
                    ? "File uploaded successfully."
                    : "Upload failed."
            };
        }
    }
}
