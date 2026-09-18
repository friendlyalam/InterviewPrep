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
    public sealed class AmazonS3Adapter : ICloudStorageService
    {
        private readonly AmazonS3Client _amazonS3Client;

        public AmazonS3Adapter(AmazonS3Client amazonS3Client)
        {
            _amazonS3Client = amazonS3Client;
        }

        public UploadResult Upload(FileUploadRequest request)
        {
            bool uploaded = _amazonS3Client.PutObject(
                request.FolderName,
                request.FileName,
                request.Content);

            return new UploadResult
            {
                Success = uploaded,
                FileUrl = uploaded
                    ? $"https://s3.amazonaws.com/{request.FolderName}/{request.FileName}"
                    : string.Empty,
                Provider = "Amazon S3",
                Message = uploaded
                    ? "File uploaded successfully."
                    : "Upload failed."
            };
        }
    }
}

//Notice

//AWS returns

//bool

//We convert it into

//UploadResult