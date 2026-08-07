using InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Enums;
using InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Models;

namespace InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Interfaces
{
    public interface ICloudPlatformService
    {
        void Backup(
            CloudProvider provider,
            CloudFile file);
    }
}


//This is the service that your application or API will call.

//It coordinates the workflow but doesn't know anything about Azure, AWS, or Google implementations.

//Interface Relationships:
//                    ICloudPlatformService
//                               │
//                               ▼
//                    ICloudServiceFactory
//                    ┌─────────┼─────────┐
//                    ▼         ▼         ▼
//             IStorage   IMessageQueue   ISecretManager