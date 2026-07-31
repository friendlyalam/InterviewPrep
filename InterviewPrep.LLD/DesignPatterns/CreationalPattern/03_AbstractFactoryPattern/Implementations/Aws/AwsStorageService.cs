using InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Enums;
using InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Interfaces;
using InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Models;

namespace InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Implementations.Aws
{
    public sealed class AwsStorageService : IStorageService
    {
        public CloudProvider Provider => CloudProvider.Aws;

        public void Upload(CloudFile file)
        {
            Console.WriteLine(
                $"[Amazon S3] Uploading '{file.FileName}'...");
        }

        public CloudFile Download(string fileName)
        {
            Console.WriteLine(
                $"[Amazon S3] Downloading '{fileName}'...");

            return new CloudFile
            {
                FileName = fileName,
                Content = Array.Empty<byte>()
            };
        }
    }
}

//Provider => CloudProvider.Aws;

//is the only identifier that tells the system these services belong to the AWS family.

//Later, our resolver will use this metadata instead of switch statements.
