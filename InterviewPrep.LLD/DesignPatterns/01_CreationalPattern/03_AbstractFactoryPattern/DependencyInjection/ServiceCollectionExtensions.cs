using InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Factories;
using InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Implementations.Aws;
using InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Implementations.Azure;
using InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Implementations.GoogleCloud;
using InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Interfaces;
using InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCloudPlatform(
            this IServiceCollection services)
        {
            //-----------------------------
            // Storage Services
            //-----------------------------

            services.AddTransient<IStorageService, AzureStorageService>();
            services.AddTransient<IStorageService, AwsStorageService>();
            services.AddTransient<IStorageService, GoogleStorageService>();

            //-----------------------------
            // Queue Services
            //-----------------------------

            services.AddTransient<IMessageQueueService, AzureQueueService>();
            services.AddTransient<IMessageQueueService, AwsQueueService>();
            services.AddTransient<IMessageQueueService, GoogleQueueService>();

            //-----------------------------
            // Secret Manager Services
            //-----------------------------

            services.AddTransient<ISecretManagerService, AzureSecretManagerService>();
            services.AddTransient<ISecretManagerService, AwsSecretManagerService>();
            services.AddTransient<ISecretManagerService, GoogleSecretManagerService>();

            //-----------------------------
            // Abstract Factories
            //-----------------------------

            services.AddTransient<ICloudServiceFactory, AzureCloudServiceFactory>();
            services.AddTransient<ICloudServiceFactory, AwsCloudServiceFactory>();
            services.AddTransient<ICloudServiceFactory, GoogleCloudServiceFactory>();

            //-----------------------------
            // Resolver
            //-----------------------------

            services.AddSingleton<ICloudFactoryResolver, CloudFactoryResolver>();

            //-----------------------------
            // Business Service
            //-----------------------------

            services.AddTransient<ICloudPlatformService, CloudPlatformService>();

            return services;
        }
    }
}
