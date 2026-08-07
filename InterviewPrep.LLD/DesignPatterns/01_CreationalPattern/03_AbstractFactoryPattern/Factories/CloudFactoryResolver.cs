using InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Enums;
using InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Factories
{
    public sealed class CloudFactoryResolver : ICloudFactoryResolver
    {
        private readonly IReadOnlyDictionary<CloudProvider, ICloudServiceFactory> _factories;

        public CloudFactoryResolver(IEnumerable<ICloudServiceFactory> factories)
        {
            _factories = factories.ToDictionary(factory => factory.Provider);
        }

        public ICloudServiceFactory Resolve(CloudProvider provider)
        {
            return _factories.TryGetValue(provider, out var factory)
                ? factory
                : throw new NotSupportedException(
                    $"Cloud provider '{provider}' is not supported.");
        }
    }
}
