Product Company Definition

Lazy Singleton is a Singleton implementation in which the object is created only when it is requested for the first time, instead of when the class is loaded.

The important phrase is:

Created only when needed.

2. Why was Lazy Singleton introduced?

Suppose creating a configuration object takes 5 seconds because it:

Reads appsettings.json
Loads secrets from Azure Key Vault
Connects to Redis
Reads Feature Flags
Loads Cache Settings

But your application starts and never uses the configuration.

With Eager Singleton:

Application Starts

↓

ConfigurationManager Created

↓

Configuration Never Used

The application still spends time creating the object.

This is unnecessary work.

3. Eager vs Lazy
Eager Singleton	Lazy Singleton
Object created immediately	Object created only when first used
Faster first access	Slightly slower first access
Slower application startup	Faster application startup
May waste memory	Better memory usage
Good when object is always required	Good when object may not be required
4. Real-Life Example 1
Office Printer

Imagine an office has one printer.

Eager

As soon as the office opens:

Office Opens

↓

Printer Powered On

↓

Nobody Prints Anything

Electricity is wasted.

Lazy
Office Opens

↓

Printer OFF

↓

First Employee Prints

↓

Printer Turns ON

This is Lazy Initialization.

5. Real-Life Example 2
Hotel Swimming Pool

A hotel has a heated swimming pool.

Eager
Morning

↓

Pool Heated

↓

Nobody Uses Pool

Energy wasted.

Lazy
Morning

↓

Pool Not Heated

↓

First Guest Arrives

↓

Heating Starts

Much better.

6. Enterprise Example

Imagine Microsoft Azure Configuration Service.

Configuration loading involves:

Azure App Configuration
Azure Key Vault
Feature Flags
Managed Identity
Secret Validation

Loading all this during application startup is expensive.

Instead:

Application Starts

↓

ConfigurationManager NOT Created

↓

Order Service Requests Configuration

↓

ConfigurationManager Created

↓

Same Object Reused


------------------------------------------------------------
eager and lazy

What changed?
Eager
private static readonly ConfigurationManager _instance =
    new ConfigurationManager();

Object created immediately.

Lazy
private static ConfigurationManager? _instance;

Initially

_instance

↓

null

No object exists.

When this executes:

ConfigurationManager.Instance

it checks:

if (_instance == null)

If true:

_instance = new ConfigurationManager();

Object is created only once.