using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._02_AdapterPattern.ThirdParty
{
    public sealed class GoogleCloudStorageClient
    {
        public GoogleUploadResponse UploadObject(
            string bucket,
            string fileName,
            byte[] data,
            string mimeType)
        {
            Console.WriteLine("Uploading to Google Cloud Storage...");

            return new GoogleUploadResponse
            {
                FileUrl = $"https://storage.googleapis.com/{bucket}/{fileName}",
                Success = true
            };
        }
    }

    public sealed class GoogleUploadResponse
    {
        public bool Success { get; init; }

        public string FileUrl { get; init; } = string.Empty;
    }
}
