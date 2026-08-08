6. Project We'll Build

We'll create:

Expense Approval Pipeline

An employee submits an expense.

Depending on the amount, different approval levels handle it.

Expense Request
       ↓
TeamLeadHandler
       ↓
ManagerHandler
       ↓
DirectorHandler

For example:

₹2,000
   ↓
Team Lead
   ↓
Approved

But:

₹75,000
   ↓
Team Lead
   ↓
Manager
   ↓
Director
   ↓
Approved

This demonstrates the pattern very clearly without creating unnecessary services.

7. Project Structure

We'll keep it compact:

12_ChainOfResponsibility
│
├── Handlers
│   ├── ExpenseHandler.cs
│   ├── TeamLeadHandler.cs
│   ├── ManagerHandler.cs
│   └── DirectorHandler.cs
│
├── Models
│   └── ExpenseRequest.cs
│
├── DependencyInjection
│   └── ServiceCollectionExtensions.cs
│
└── Program.cs

8 files.

No unnecessary:

PaymentService
NotificationService
DatabaseService
Repository
Factory

The pattern itself is the focus.

8. Architecture
                   ExpenseRequest
                         │
                         ▼
                TeamLeadHandler
                    /        \
              handles?       next
                 │             │
                 ▼             ▼
              ManagerHandler
                    /        \
              handles?       next
                 │             │
                 ▼             ▼
             DirectorHandler
                    │
                    ▼
                  Result
9. Core Concept

Every handler has two responsibilities:

1. Process the request

If it is responsible for that request.

2. Pass it forward

If it cannot process it.

Conceptually:

if (CanHandle(request))
{
    Handle(request);
}
else
{
    _next.Handle(request);
}

That's essentially the heart of the pattern.


---------------------------------------------------------------------------------

3. Why Do We Need ExpenseHandler?

Every handler needs the same basic capability:

Handler
   │
   ├── Process request
   │
   └── Pass request to next handler

Instead of repeating this logic in every class, we put the common behavior in the base class.

So:

ExpenseHandler
       │
       ├── TeamLeadHandler
       │
       ├── ManagerHandler
       │
       └── DirectorHandler
4. Understanding Next

This property:

protected ExpenseHandler? Next { get; private set; }

stores the next handler in the chain.

For example:

TeamLead
   ↓
Manager
   ↓
Director

Internally:

TeamLead.Next = Manager
Manager.Next  = Director
Director.Next = null
5. Understanding SetNext()
public ExpenseHandler SetNext(ExpenseHandler next)
{
    Next = next;
    return next;
}

This allows us to build the chain fluently:

teamLead
    .SetNext(manager)
    .SetNext(director);

After this:

TeamLead
    │
    ▼
Manager
    │
    ▼
Director

The important part is:

return next;

Because it allows the next .SetNext() call to operate on the newly added handler.

6. Understanding Handle()
public abstract string Handle(ExpenseRequest request);

Every concrete handler must decide:

Can I handle this request?

For example:

TeamLeadHandler
    ↓
Amount <= ₹10,000?
    ↓
YES → Approve
NO  → Next

Manager:

ManagerHandler
    ↓
Amount <= ₹50,000?
    ↓
YES → Approve
NO  → Next

Director:

DirectorHandler
    ↓
Amount <= ₹100,000?
    ↓
YES → Approve
NO  → Reject
7. Why PassToNext()?
protected string PassToNext(ExpenseRequest request)
{
    if (Next is null)
    {
        return "Expense request could not be approved.";
    }

    return Next.Handle(request);
}

This protects us from:

Handler
   ↓
Next
   ↓
Next
   ↓
null

If nobody handles the request, we return a controlled result.

8. Why Abstract Class Instead of Interface?

This is an important design decision.

We could have used:

interface IExpenseHandler
{
    ...
}

But our handlers share actual implementation:

Next
SetNext()
PassToNext()

An abstract base class lets us reuse that common behavior.

Therefore:

Use an abstract class here because the handlers share both a contract and common state/behavior.

If we only needed a contract with no shared implementation, an interface would be a better fit.

9. Current Flow

We haven't created the concrete handlers yet.

Currently we have:

ExpenseRequest
      │
      ▼
ExpenseHandler
      │
      ├── Next
      │
      └── Handle()

Next we'll create the three actual handlers:

TeamLeadHandler
       ↓
ManagerHandler
       ↓
DirectorHandler

and each will have a different approval responsibility, which is where the Chain of Responsibility pattern becomes visible.


--------------------------------------------------------------------------------------------------------------------------------------------

Notice something important.

The Director is the last handler, so it doesn't need to call:

PassToNext()

There is no next handler.

5. Our Chain

We now have:

┌──────────────────┐
│ TeamLeadHandler  │
│ Limit: ₹10,000   │
└────────┬─────────┘
         │
         ▼
┌──────────────────┐
│ ManagerHandler   │
│ Limit: ₹50,000   │
└────────┬─────────┘
         │
         ▼
┌──────────────────┐
│ DirectorHandler  │
│ Limit: ₹100,000  │
└──────────────────┘
6. Example — ₹5,000
Expense
₹5,000
   │
   ▼
Team Lead
   │
   ├── Can handle? YES
   │
   ▼
APPROVED

The request stops at Team Lead.

It doesn't reach Manager or Director.

7. Example — ₹30,000
Expense
₹30,000
   │
   ▼
Team Lead
   │
   ├── Can handle? NO
   │
   ▼
Manager
   │
   ├── Can handle? YES
   │
   ▼
APPROVED
8. Example — ₹80,000
Expense
₹80,000
   │
   ▼
Team Lead
   │
   ├── NO
   ▼
Manager
   │
   ├── NO
   ▼
Director
   │
   ├── YES
   ▼
APPROVED
9. Example — ₹150,000
Expense
₹150,000
   │
   ▼
Team Lead
   │
   ├── NO
   ▼
Manager
   │
   ├── NO
   ▼
Director
   │
   ├── NO
   ▼
REJECTED
10. Important Pattern Characteristics

Notice that Program.cs will not need to know which handler can approve which amount.

It will simply send:

handler.Handle(request);

The chain decides who handles it.

That's the key benefit.

Caller
  │
  │ doesn't know
  │ who will handle
  ▼
First Handler
  │
  ▼
Next Handler
  │
  ▼
Next Handler


-----------------------------------------------------------------------------

4. Why Register ExpenseHandler?

This is important.

We don't want Program.cs to depend on:

TeamLeadHandler
ManagerHandler
DirectorHandler

Instead, it can depend on the abstraction:

ExpenseHandler

So:

Program.cs
    │
    ▼
ExpenseHandler
    │
    ▼
TeamLeadHandler
    ↓
ManagerHandler
    ↓
DirectorHandler

This is a nice application of Dependency Inversion alongside the pattern.

5. Why Does the Factory Registration Return Team Lead?

Look carefully:

return teamLead;

We're returning the first handler.

Why?

Because the chain starts here:

ExpenseHandler
      ↓
TeamLead
      ↓
Manager
      ↓
Director

The caller only needs the entry point.

It doesn't need to know the internal chain.

6. Complete DI Flow

When .NET resolves:

ExpenseHandler

the factory executes:

Resolve TeamLead
       ↓
Resolve Manager
       ↓
Resolve Director
       ↓
TeamLead.SetNext(Manager)
       ↓
Manager.SetNext(Director)
       ↓
Return TeamLead

The resulting object graph is:

                    ExpenseHandler
                         │
                         ▼
                 TeamLeadHandler
                         │
                         ▼
                  ManagerHandler
                         │
                         ▼
                 DirectorHandler
7. Why Scoped?

We're using:

services.AddScoped<...>();

because this is an application/request processing pipeline.

In an ASP.NET Core application, Scoped is commonly appropriate when the handler may eventually depend on request-scoped resources.

For this console demo, Transient would also work.

The important interview point is:

The lifetime should be chosen based on the dependencies and state of the handlers, not because the pattern requires a particular lifetime.