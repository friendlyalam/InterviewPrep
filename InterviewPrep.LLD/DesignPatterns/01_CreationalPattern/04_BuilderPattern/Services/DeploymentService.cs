using InterviewPrep.LLD.DesignPatterns.CreationalPattern._04_BuilderPattern.Interfaces;
using InterviewPrep.LLD.DesignPatterns.CreationalPattern._04_BuilderPattern.Models;

namespace InterviewPrep.LLD.DesignPatterns.CreationalPattern._04_BuilderPattern.Services
{
    public sealed class DeploymentService : IDeploymentService
    {
        public void Deploy(KubernetesDeployment deployment)
        {
            Console.WriteLine("====================================");
            Console.WriteLine(" Kubernetes Deployment");
            Console.WriteLine("====================================");

            Console.WriteLine($"Deployment Name : {deployment.DeploymentName}");
            Console.WriteLine($"Namespace       : {deployment.Namespace}");
            Console.WriteLine($"Docker Image    : {deployment.DockerImage}");
            Console.WriteLine($"Replicas        : {deployment.Replicas}");
            Console.WriteLine($"CPU Limit       : {deployment.CpuLimit}");
            Console.WriteLine($"Memory Limit    : {deployment.MemoryLimit}");
            Console.WriteLine($"Health Check    : {deployment.HealthCheckEnabled}");

            Console.WriteLine("\nEnvironment Variables");

            foreach (var environmentVariable in deployment.EnvironmentVariables)
            {
                Console.WriteLine($"{environmentVariable.Key} = {environmentVariable.Value}");
            }

            Console.WriteLine("\nLabels");

            foreach (var label in deployment.Labels)
            {
                Console.WriteLine($"{label.Key} = {label.Value}");
            }

            Console.WriteLine("\nDeployment Created Successfully.");
        }
    }
}


//Why DeploymentService?

//Many Builder tutorials end after Build().

//Product companies don't.

//The built object is always consumed by another service.

//Builder

//↓

//Build()

//↓

//Deployment Service

//↓

//Deploy

//This demonstrates Separation of Concerns (SRP).