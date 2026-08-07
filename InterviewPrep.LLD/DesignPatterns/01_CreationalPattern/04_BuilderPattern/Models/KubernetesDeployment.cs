
using System.Diagnostics.Metrics;

namespace InterviewPrep.LLD.DesignPatterns.CreationalPattern._04_BuilderPattern.Models
{
    public sealed class KubernetesDeployment
    {
        public string DeploymentName { get; init; } = string.Empty;

        public string Namespace { get; init; } = string.Empty;

        public string DockerImage { get; init; } = string.Empty;

        public int Replicas { get; init; }

        public string CpuLimit { get; init; } = "250m";

        public string MemoryLimit { get; init; } = "512Mi";

        public bool HealthCheckEnabled { get; init; }

        public Dictionary<string, string> EnvironmentVariables { get; init; } = new();

        public Dictionary<string, string> Labels { get; init; } = new();
    }
}

//Why init instead of set?
//public string DeploymentName { get; init; }
//Why?

//Because once the deployment is built

//Builder
//      │
//      ▼
//KubernetesDeployment

//it should become immutable.

//Nobody should change

//deployment.Replicas = 100;

//after deployment creation.

//This is a common enterprise practice for configuration objects.

//Why Dictionary?

//Instead of

//public string Environment1
//public string Environment2
//public string Environment3

//we use

//Dictionary<string,string>

//because real deployments can contain

//DATABASE_URL

//JWT_SECRET

//REDIS_HOST

//API_KEY

//...

//There is no fixed number.

//Why Default Values?
//CpuLimit = "250m"

//MemoryLimit = "512Mi"

//If DevOps doesn't specify these,

//reasonable defaults are applied.

//Builder should only force required properties.
