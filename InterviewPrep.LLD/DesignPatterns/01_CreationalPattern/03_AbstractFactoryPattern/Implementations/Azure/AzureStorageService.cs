using InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Enums;
using InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Interfaces;
using InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Models;

namespace InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Implementations.Azure
{
    public sealed class AzureStorageService : IStorageService
    {
        public CloudProvider Provider => CloudProvider.Azure;

        public void Upload(CloudFile file)
        {
            Console.WriteLine(
                $"[Azure Blob Storage] Uploading '{file.FileName}'...");
        }

        public CloudFile Download(string fileName)
        {
            Console.WriteLine(
                $"[Azure Blob Storage] Downloading '{fileName}'...");

            return new CloudFile
            {
                FileName = fileName,
                Content = Array.Empty<byte>()
            };
        }
    }
}

//Why is new CloudFile() okay?

//According to our enterprise rules:

//Object DI  new
//DTO ❌	✅

//CloudFile is a DTO.

//Creating it with new is correct.