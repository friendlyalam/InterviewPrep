using InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Enums;
using InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Interfaces;

namespace InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Implementations.Azure
{
    public sealed class AzureQueueService : IMessageQueueService
    {
        public CloudProvider Provider => CloudProvider.Azure;

        public void Publish(string message)
        {
            Console.WriteLine(
                $"[Azure Queue] Published : {message}");
        }

        public string Receive()
        {
            Console.WriteLine(
                "[Azure Queue] Receiving message...");

            return "Backup completed.";
        }
    }
}
