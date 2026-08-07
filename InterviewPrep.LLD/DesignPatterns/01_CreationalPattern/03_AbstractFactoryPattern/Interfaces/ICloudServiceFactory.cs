

using InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Enums;
using InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Implementations.Azure;

namespace InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Interfaces
{

    //This is the heart of the Abstract Factory pattern.
    public interface ICloudServiceFactory
    {
        CloudProvider Provider { get; }

        IStorageService CreateStorageService();

        IMessageQueueService CreateQueueService();

        ISecretManagerService CreateSecretManagerService();
    }
}

//Notice that it creates a family of services.


//Why Doesn't This Interface Accept CloudProvider?

//Many developers write:

//CreateStorageService(CloudProvider provider)

//That weakens the Abstract Factory pattern.

//The provider should already be determined when the appropriate factory is selected.

//For example:

//AzureCloudServiceFactory

//↓

//CreateStorageService()

//↓

//AzureStorageService

//No provider parameter is needed.