using InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Enums;
using InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Interfaces;
using InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Implementations.GoogleCloud
{
    public sealed class GoogleStorageService : IStorageService
    {
        public CloudProvider Provider => CloudProvider.GoogleCloud;

        public void Upload(CloudFile file)
        {
            Console.WriteLine(
                $"[Google Cloud Storage] Uploading '{file.FileName}'...");
        }

        public CloudFile Download(string fileName)
        {
            Console.WriteLine(
                $"[Google Cloud Storage] Downloading '{fileName}'...");

            return new CloudFile
            {
                FileName = fileName,
                Content = Array.Empty<byte>()
            };
        }
    }
}
