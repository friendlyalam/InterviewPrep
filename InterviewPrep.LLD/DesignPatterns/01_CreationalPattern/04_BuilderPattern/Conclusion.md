Execution Flow
Program.cs

      │

      ▼

Resolve Builder

      │

      ▼

Configure Deployment

      │

      ▼

Validate

      │

      ▼

Build()

      │

      ▼

Immutable Deployment

      │

      ▼

DeploymentService

      │

      ▼

Deploy
Expected Output
====================================
 Kubernetes Deployment
====================================

Deployment Name : payment-api
Namespace       : production
Docker Image    : payment-api:v3
Replicas        : 3
CPU Limit       : 500m
Memory Limit    : 1Gi
Health Check    : True

Environment Variables

ASPNETCORE_ENVIRONMENT = Production
REDIS_HOST = redis.company.com

Labels

Team = Payments
Region = India

Deployment Created Successfully.
Why This Is Enterprise Ready

✅ Fluent API

builder
.WithDeploymentName(...)
.WithNamespace(...)
...
.Build();

✅ Immutable Model

init;

✅ Validation

Build()

↓

Validate()

↓

Return Object

✅ Dependency Injection

No

new KubernetesDeploymentBuilder()

inside business code.

✅ Separation of Concerns

Class	Responsibility
Model	Holds deployment configuration
Builder	Builds and validates the configuration
Service	Uses the configuration
Program	Composition Root
Interview Questions
Q1. Why Builder instead of constructor?

Because the object has many optional properties, and a long constructor becomes hard to read and maintain.

Q2. Why is Builder registered as Transient?

Because it stores temporary state while constructing an object. Reusing the same instance could leak state between builds.

Q3. Why immutable model?

To prevent accidental modification after the object has been built.

Q4. Why validate in Build()?

So the object is never returned in an invalid state.

Q5. Why not call new KubernetesDeployment() directly in Program.cs?

Because that would bypass the Builder's validation and fluent construction process, defeating the purpose of the pattern.

Project Rating
Feature	Status
Enterprise Example	✅
Product Company Relevant	✅
Fluent API	✅
DI	✅
SOLID	✅
Immutable Model	✅
Validation	✅
15–20 Minute Interview Demo	✅
Review of this project

This is the size I recommend we keep for the remaining patterns:

6–8 classes
One realistic enterprise scenario
Complete DI setup
One Program.cs
Interview-ready in 15–20 minutes
No unnecessary architecture or artificial complexity

I believe this is a much better balance than our earlier, oversized projects and is much closer to what interviewers actually expect to discuss.