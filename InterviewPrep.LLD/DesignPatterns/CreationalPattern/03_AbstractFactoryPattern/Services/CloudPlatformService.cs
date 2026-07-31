using InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Enums;
using InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Interfaces;
using InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Models;

namespace InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Services
{
    public sealed class CloudPlatformService : ICloudPlatformService
    {
        private readonly ICloudFactoryResolver _factoryResolver;

        public CloudPlatformService(
            ICloudFactoryResolver factoryResolver)
        {
            _factoryResolver = factoryResolver;
        }

        public void Backup(
            CloudProvider provider,
            CloudFile file)
        {
            ICloudServiceFactory factory =
                _factoryResolver.Resolve(provider);

            IStorageService storageService =
                factory.CreateStorageService();

            IMessageQueueService queueService =
                factory.CreateQueueService();

            ISecretManagerService secretManagerService =
                factory.CreateSecretManagerService();

            Secret secret =
                secretManagerService.Get("StorageConnection");

            storageService.Upload(file);

            queueService.Publish(
                $"Backup completed for {file.FileName}");

            Console.WriteLine(
                $"Secret Value : {secret.Value}");
        }
    }
}
