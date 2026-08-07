using InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Enums;

namespace InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Interfaces
{
    public interface ICloudFactoryResolver
    {
        ICloudServiceFactory Resolve(CloudProvider provider);
    }
}
