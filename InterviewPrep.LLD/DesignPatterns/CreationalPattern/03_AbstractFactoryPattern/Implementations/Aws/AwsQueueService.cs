using InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Enums;
using InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Implementations.Aws
{
    public sealed class AwsQueueService : IMessageQueueService
    {
        public CloudProvider Provider => CloudProvider.Aws;

        public void Publish(string message)
        {
            Console.WriteLine(
                $"[Amazon SQS] Published : {message}");
        }

        public string Receive()
        {
            Console.WriteLine(
                "[Amazon SQS] Receiving message...");

            return "Backup completed.";
        }
    }
}
