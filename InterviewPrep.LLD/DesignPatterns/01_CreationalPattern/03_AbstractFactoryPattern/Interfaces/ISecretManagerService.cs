using InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Enums;
using InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Models;

namespace InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Interfaces
{
    public interface ISecretManagerService
    {
        CloudProvider Provider { get; }

        void Save(Secret secret);

        Secret Get(string key);
    }
}

//Why Add Provider to Every Interface?

//Suppose we inject

//IEnumerable<IStorageService>

//The DI container returns

//AzureStorageService

//AwsStorageService

//GoogleStorageService

//How do we know which one to use?

//The Provider property allows us to identify the correct implementation without hard-coded type checks.