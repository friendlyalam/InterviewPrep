

using InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Enums;
using InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Models;

namespace InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Interfaces
{
    public interface IStorageService
    {
        CloudProvider Provider { get; }

        void Upload(CloudFile file);

        CloudFile Download(string fileName);
    }
}

//Why Provider Property?

//Instead of

//switch(provider)
//{
//    case Azure:
//        ...
//}

//We'll write

//_storageServices.Single(s => s.Provider == CloudProvider.Azure);

//or later

//_storageServices.Single(s => s.Provider == provider);

//No switch.

//No reflection.

//Fully extensible.