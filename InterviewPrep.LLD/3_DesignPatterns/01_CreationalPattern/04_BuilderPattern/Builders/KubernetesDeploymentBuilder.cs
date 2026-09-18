using InterviewPrep.LLD.DesignPatterns.CreationalPattern._04_BuilderPattern.Interfaces;
using InterviewPrep.LLD.DesignPatterns.CreationalPattern._04_BuilderPattern.Models;

namespace InterviewPrep.LLD.DesignPatterns.CreationalPattern._04_BuilderPattern.Builders
{
    public sealed class KubernetesDeploymentBuilder : IKubernetesDeploymentBuilder
    {
        private string _deploymentName = string.Empty;
        private string _namespace = string.Empty;
        private string _dockerImage = string.Empty;
        private int _replicas;
        private string _cpuLimit = "250m";
        private string _memoryLimit = "512Mi";
        private bool _healthCheckEnabled;

        private readonly Dictionary<string, string> _environmentVariables = new();
        private readonly Dictionary<string, string> _labels = new();

        public IKubernetesDeploymentBuilder WithDeploymentName(string deploymentName)
        {
            _deploymentName = deploymentName;
            return this;
        }

        public IKubernetesDeploymentBuilder WithNamespace(string namespaceName)
        {
            _namespace = namespaceName;
            return this;
        }

        public IKubernetesDeploymentBuilder WithDockerImage(string dockerImage)
        {
            _dockerImage = dockerImage;
            return this;
        }

        public IKubernetesDeploymentBuilder WithReplicas(int replicas)
        {
            _replicas = replicas;
            return this;
        }

        public IKubernetesDeploymentBuilder WithCpuLimit(string cpuLimit)
        {
            _cpuLimit = cpuLimit;
            return this;
        }

        public IKubernetesDeploymentBuilder WithMemoryLimit(string memoryLimit)
        {
            _memoryLimit = memoryLimit;
            return this;
        }

        public IKubernetesDeploymentBuilder EnableHealthCheck()
        {
            _healthCheckEnabled = true;
            return this;
        }

        public IKubernetesDeploymentBuilder AddEnvironmentVariable(string key, string value)
        {
            _environmentVariables[key] = value;
            return this;
        }

        public IKubernetesDeploymentBuilder AddLabel(string key, string value)
        {
            _labels[key] = value;
            return this;
        }

        public KubernetesDeployment Build()
        {
            Validate();

            return new KubernetesDeployment
            {
                DeploymentName = _deploymentName,
                Namespace = _namespace,
                DockerImage = _dockerImage,
                Replicas = _replicas,
                CpuLimit = _cpuLimit,
                MemoryLimit = _memoryLimit,
                HealthCheckEnabled = _healthCheckEnabled,
                EnvironmentVariables = new Dictionary<string, string>(_environmentVariables),
                Labels = new Dictionary<string, string>(_labels)
            };
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(_deploymentName))
                throw new InvalidOperationException("Deployment Name is required.");

            if (string.IsNullOrWhiteSpace(_namespace))
                throw new InvalidOperationException("Namespace is required.");

            if (string.IsNullOrWhiteSpace(_dockerImage))
                throw new InvalidOperationException("Docker Image is required.");

            if (_replicas <= 0)
                throw new InvalidOperationException("Replicas must be greater than zero.");
        }
    }
}
