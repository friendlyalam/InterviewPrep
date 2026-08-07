using InterviewPrep.LLD.DesignPatterns.CreationalPattern._04_BuilderPattern.Models;

namespace InterviewPrep.LLD.DesignPatterns.CreationalPattern._04_BuilderPattern.Interfaces
{
    public interface IDeploymentService
    {
        void Deploy(KubernetesDeployment deployment);
    }
}

//Very small.

//Very clean.

//Exactly one responsibility.

//Architecture So Far
//Program.cs

//        │

//        ▼

//IKubernetesDeploymentBuilder

//        │

//        ▼

//KubernetesDeploymentBuilder

//        │

//        ▼

//KubernetesDeployment

//        │

//        ▼

//IDeploymentService

//        │

//        ▼

//DeploymentService