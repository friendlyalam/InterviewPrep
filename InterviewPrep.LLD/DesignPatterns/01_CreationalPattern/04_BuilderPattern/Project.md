Builder Pattern
Lesson 2 – Architecture & Project Design
Enterprise Problem Statement

Imagine you're building a deployment tool used by a DevOps team.

Every deployment requires:

Deployment Name
Namespace
Docker Image
Replicas
CPU Limit
Memory Limit

Optional configurations:

Environment Variables
Labels
Secrets
Health Check

Creating this object using constructors is difficult.

Instead, we'll build it step by step.

Project Scope

We are NOT building Kubernetes.

We are only building the Deployment Builder.

The goal is to demonstrate the Builder Pattern, not Kubernetes.

Final Folder Structure
04_BuilderPattern
│
├── Models
│      KubernetesDeployment.cs
│
├── Interfaces
│      IKubernetesDeploymentBuilder.cs
│      IDeploymentService.cs
│
├── Builders
│      KubernetesDeploymentBuilder.cs
│
├── Services
│      DeploymentService.cs
│
├── DependencyInjection
│      ServiceCollectionExtensions.cs
│
└── Program.cs

Total Classes: 5 + Program.cs

Responsibilities
1. KubernetesDeployment

Represents the final deployment configuration.

It contains only data.

No business logic.

2. IKubernetesDeploymentBuilder

Defines how a deployment is built.

Provides fluent methods such as:

WithDeploymentName()
WithNamespace()
WithDockerImage()
WithReplicas()
Build()
3. KubernetesDeploymentBuilder

Builds the object step by step.

Responsible for:

Setting values
Validation
Returning the final object
4. DeploymentService

Consumes the completed deployment.

Represents the business layer.

Responsible for:

Deploying
Printing deployment details (Console project)

It does not know how the deployment was created.

5. Program.cs

Composition Root.

Responsible for:

DI registration
Creating the builder
Calling Build()
Calling DeploymentService
Architecture
Program.cs
      │
      ▼
IKubernetesDeploymentBuilder
      │
      ▼
KubernetesDeploymentBuilder
      │
      ▼
KubernetesDeployment
      │
      ▼
DeploymentService
SOLID Principles Used

| Principle | Usage                                                                                 |
| --------- | ------------------------------------------------------------------------------------- |
| SRP       | Builder builds, Service deploys, Model stores data.                                   |
| OCP       | New builder methods can be added without changing the service.                        |
| LSP       | Any implementation of `IKubernetesDeploymentBuilder` can replace the current builder. |
| ISP       | Small, focused interfaces.                                                            |
| DIP       | `Program.cs` and `DeploymentService` depend on abstractions.                          |



Required vs Optional Properties
Required
Deployment Name

Namespace

Docker Image

Replicas

Without these,

Build() should fail.

Optional
CPU

Memory

Labels

Environment Variables

Health Check

If not provided,

reasonable defaults will be used.

Validation Rules

Before Build() returns the object:

Deployment Name cannot be empty.
Namespace cannot be empty.
Docker Image cannot be empty.
Replicas must be greater than 0.

If validation fails:

throw new InvalidOperationException(...);

This mirrors enterprise code where invalid configuration is rejected early.

Why Use DI?

Many tutorials create the builder like this:

var builder = new KubernetesDeploymentBuilder();

For learning the pattern, that's acceptable.

For enterprise applications, we'll register the builder with DI and resolve it through the container.
This keeps object creation consistent with modern .NET applications and makes the builder easier to replace or test.

Final Build Flow
Create Builder

↓

Configure Deployment

↓

Validate

↓

Build()

↓

DeploymentService

↓

Deploy
Why This Project Is Better Than a Pizza Builder

Most tutorials use:

Pizza
Car
Burger
House

Those examples teach the pattern but don't reflect enterprise software.

A Kubernetes Deployment Builder demonstrates:

Complex configuration objects
Required vs optional fields
Fluent APIs
Validation
Enterprise coding practices
Product Company Discussion

This pattern is commonly seen in APIs such as:

Host.CreateDefaultBuilder()

WebApplication.CreateBuilder()

ConfigurationBuilder()

OptionsBuilder<T>()

HttpRequestMessage builders

Cloud SDK request builders

These APIs all construct complex objects through a sequence of configuration steps before producing the final object.
