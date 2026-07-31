using InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Enums;
using InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Interfaces;

namespace InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Implementations.GoogleCloud
{
    public sealed class GoogleCloudServiceFactory : ICloudServiceFactory
    {
        private readonly IEnumerable<IStorageService> _storageServices;
        private readonly IEnumerable<IMessageQueueService> _queueServices;
        private readonly IEnumerable<ISecretManagerService> _secretManagerServices;

        public GoogleCloudServiceFactory(
            IEnumerable<IStorageService> storageServices,
            IEnumerable<IMessageQueueService> queueServices,
            IEnumerable<ISecretManagerService> secretManagerServices)
        {
            _storageServices = storageServices;
            _queueServices = queueServices;
            _secretManagerServices = secretManagerServices;
        }
        public CloudProvider Provider => CloudProvider.GoogleCloud;
        public IStorageService CreateStorageService()
            => _storageServices.Single(s => s.Provider == CloudProvider.GoogleCloud);

        public IMessageQueueService CreateQueueService()
            => _queueServices.Single(q => q.Provider == CloudProvider.GoogleCloud);

        public ISecretManagerService CreateSecretManagerService()
            => _secretManagerServices.Single(s => s.Provider == CloudProvider.GoogleCloud);
    }
}
