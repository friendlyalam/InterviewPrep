using InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Enums;
using InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Interfaces;

namespace InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Implementations.Azure
{
    public sealed class AzureCloudServiceFactory : ICloudServiceFactory
    {
        private readonly IEnumerable<IStorageService> _storageServices;
        private readonly IEnumerable<IMessageQueueService> _queueServices;
        private readonly IEnumerable<ISecretManagerService> _secretManagerServices;

        public AzureCloudServiceFactory(
            IEnumerable<IStorageService> storageServices,
            IEnumerable<IMessageQueueService> queueServices,
            IEnumerable<ISecretManagerService> secretManagerServices)
        {
            _storageServices = storageServices;
            _queueServices = queueServices;
            _secretManagerServices = secretManagerServices;
        }

        public CloudProvider Provider => CloudProvider.Azure;
        public IStorageService CreateStorageService()
        {
            return _storageServices.Single(s =>
                s.Provider == CloudProvider.Azure);
        }

        public IMessageQueueService CreateQueueService()
        {
            return _queueServices.Single(q =>
                q.Provider == CloudProvider.Azure);
        }

        public ISecretManagerService CreateSecretManagerService()
        {
            return _secretManagerServices.Single(s =>
                s.Provider == CloudProvider.Azure);
        }
    }
}

//Enterprise Solution

//Instead of injecting

//IStorageService

//we inject

//IEnumerable<IStorageService>

//Likewise,

//IEnumerable<IMessageQueueService>

//IEnumerable<ISecretManagerService>

//The factory then selects the matching implementation using the Provider property.

//This avoids:

//❌ switch
//❌ if
//❌ new
//❌ reflection
