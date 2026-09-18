Why Is This Better Than Using Concrete Classes?

Imagine this implementation:

public class DocumentManager
{
    private readonly PdfDocument _document;

    public DocumentManager(PdfDocument document)
    {
        _document = document;
    }
}

Tomorrow the business says:

Support Excel.

Now you must change the class.

Later:

Support Word.

Change again.

Later:

Support PowerPoint.

Change again.

Every new document type forces code modifications.

With an interface:

DocumentManager

↓

IDocument

↓

PDF

Excel

Word

PowerPoint

Markdown

Text

Rich Text

You only add new implementations.
#endregion

#region Output analysis
Product Company Interview Questions
Q1. Why use an interface here instead of inheritance?

Because the goal is to define capabilities, not an "is-a" hierarchy.

A PDF document can be printed and can be exported.

Those are behaviours, not inheritance relationships.

Q2. Why multiple small interfaces instead of one large interface?

To follow the Interface Segregation Principle.

Classes shouldn't be forced to implement methods they don't need.

Q3. Why does DocumentManager depend on IDocument?

To reduce coupling and allow any implementation to be substituted without changing the manager.

Q4. Can PdfDocument implement five interfaces?

Yes.

A class can implement multiple interfaces.

Q5. Is this runtime polymorphism?

Yes.

IDocument document =
    new PdfDocument(...);

When:

document.Open();

The runtime executes:

PdfDocument.Open()

based on the actual object.

Q6. Why are interfaces heavily used with Dependency Injection?

Because the DI container injects an implementation that satisfies the required contract. 
The consuming class depends on the abstraction rather than knowing which concrete class it receives.

    Enterprise Insight

If you open an ASP.NET Core application, you'll frequently see interfaces such as:

ILogger
IConfiguration
IHostEnvironment
IMemoryCache
IDistributedCache
IHttpClientFactory
IServiceProvider

Microsoft uses interfaces extensively because they make the framework:

Extensible
Testable
Loosely coupled
Easy to replace implementations
Easy to integrate with Dependency Injection