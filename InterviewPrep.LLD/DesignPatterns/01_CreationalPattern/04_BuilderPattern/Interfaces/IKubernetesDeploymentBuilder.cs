using InterviewPrep.LLD.DesignPatterns.CreationalPattern._04_BuilderPattern.Models;

namespace InterviewPrep.LLD.DesignPatterns.CreationalPattern._04_BuilderPattern.Interfaces
{
    public interface IKubernetesDeploymentBuilder
    {
        IKubernetesDeploymentBuilder WithDeploymentName(string deploymentName);

        IKubernetesDeploymentBuilder WithNamespace(string namespaceName);

        IKubernetesDeploymentBuilder WithDockerImage(string dockerImage);

        IKubernetesDeploymentBuilder WithReplicas(int replicas);

        IKubernetesDeploymentBuilder WithCpuLimit(string cpuLimit);

        IKubernetesDeploymentBuilder WithMemoryLimit(string memoryLimit);

        IKubernetesDeploymentBuilder EnableHealthCheck();

        IKubernetesDeploymentBuilder AddEnvironmentVariable(string key, string value);

        IKubernetesDeploymentBuilder AddLabel(string key, string value);

        KubernetesDeployment Build();
    }
}


//Why return the interface?

//Notice:

//IKubernetesDeploymentBuilder

//instead of

//void

//Why?

//Because Builder uses Method Chaining.

//builder
//.WithDeploymentName(...)
//.WithNamespace(...)
//.WithDockerImage(...)
//.WithReplicas(...)
//.Build();

//Every method returns the same builder.

//Why no constructor?

//Because construction is happening

//step

//by

//step.

//Builder owns the creation process.