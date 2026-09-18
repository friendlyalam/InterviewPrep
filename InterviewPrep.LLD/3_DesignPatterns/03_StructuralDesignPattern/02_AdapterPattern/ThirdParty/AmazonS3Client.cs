using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._02_AdapterPattern.ThirdParty
{
    public sealed class AmazonS3Client
    {
        public bool PutObject(
            string bucketName,
            string key,
            byte[] fileBytes)
        {
            Console.WriteLine("Uploading to Amazon S3...");

            return true;
        }
    }
}
