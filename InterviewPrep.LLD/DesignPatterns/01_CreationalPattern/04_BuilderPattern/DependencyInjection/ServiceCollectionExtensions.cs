using InterviewPrep.LLD.DesignPatterns.CreationalPattern._04_BuilderPattern.Builders;
using InterviewPrep.LLD.DesignPatterns.CreationalPattern._04_BuilderPattern.Interfaces;
using InterviewPrep.LLD.DesignPatterns.CreationalPattern._04_BuilderPattern.Services;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;

namespace InterviewPrep.LLD.DesignPatterns.CreationalPattern._04_BuilderPattern.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDeploymentServices(
            this IServiceCollection services)
        {
            services.AddTransient<IKubernetesDeploymentBuilder,
                                  KubernetesDeploymentBuilder>();

            services.AddTransient<IDeploymentService,
                                  DeploymentService>();

            return services;
        }
    }
}


//Why Transient?

//Builder stores temporary state.

//Deployment A

//↓

//Deployment B

//↓

//Deployment C

//Every deployment should receive a new Builder instance.

//If we make Builder a Singleton, previous values may remain inside private fields.

//That would be a bug.

//Therefore

//AddTransient()

//is the correct lifetime.