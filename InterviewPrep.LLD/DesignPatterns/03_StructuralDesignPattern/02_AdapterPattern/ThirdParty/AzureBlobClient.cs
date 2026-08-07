using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._02_AdapterPattern.ThirdParty
{
    public sealed class AzureBlobClient
    {
        public string UploadBlob(
            byte[] content,
            string blobName,
            string containerName,
            string contentType)
        {
            Console.WriteLine("Uploading to Azure Blob Storage...");

            return $"https://azurestorage.blob.core.windows.net/{containerName}/{blobName}";
        }
    }
}
