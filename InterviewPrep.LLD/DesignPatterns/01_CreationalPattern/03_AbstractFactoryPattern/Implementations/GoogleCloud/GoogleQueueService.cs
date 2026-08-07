using InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Enums;
using InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Interfaces;

namespace InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Implementations.GoogleCloud
{
    public sealed class GoogleQueueService : IMessageQueueService
    {
        public CloudProvider Provider => CloudProvider.GoogleCloud;

        public void Publish(string message)
        {
            Console.WriteLine(
                $"[Google Pub/Sub] Published : {message}");
        }

        public string Receive()
        {
            Console.WriteLine(
                "[Google Pub/Sub] Receiving message...");

            return "Backup completed.";
        }
    }
}
