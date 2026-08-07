using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._02_AdapterPattern.Models
{
    public sealed class UploadResult
    {
        public bool Success { get; init; }

        public string FileUrl { get; init; } = string.Empty;

        public string Provider { get; init; } = string.Empty;

        public string Message { get; init; } = string.Empty;
    }
}

//Why UploadResult?

//Different SDKs return different response objects.

//Azure may return

//BlobClient

//AWS

//PutObjectResponse

//Google

//StorageObject

//We don't expose vendor-specific objects to our application.

//Instead

//every adapter converts its response into

//UploadResult

//This is one of the biggest benefits of Adapter.