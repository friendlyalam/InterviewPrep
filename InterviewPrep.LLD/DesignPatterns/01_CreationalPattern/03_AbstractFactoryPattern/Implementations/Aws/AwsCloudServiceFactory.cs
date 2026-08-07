using InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Enums;
using InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Implementations.Aws
{
    public sealed class AwsCloudServiceFactory : ICloudServiceFactory
    {
        private readonly IEnumerable<IStorageService> _storageServices;
        private readonly IEnumerable<IMessageQueueService> _queueServices;
        private readonly IEnumerable<ISecretManagerService> _secretManagerServices;

        public AwsCloudServiceFactory(
            IEnumerable<IStorageService> storageServices,
            IEnumerable<IMessageQueueService> queueServices,
            IEnumerable<ISecretManagerService> secretManagerServices)
        {
            _storageServices = storageServices;
            _queueServices = queueServices;
            _secretManagerServices = secretManagerServices;
        }

        public CloudProvider Provider => CloudProvider.Aws;
        public IStorageService CreateStorageService()
            => _storageServices.Single(s => s.Provider == CloudProvider.Aws);

        public IMessageQueueService CreateQueueService()
            => _queueServices.Single(q => q.Provider == CloudProvider.Aws);

        public ISecretManagerService CreateSecretManagerService()
            => _secretManagerServices.Single(s => s.Provider == CloudProvider.Aws);
    }
}
