6. Our Project

We'll build:

Employee Leave Approval System

An employee submits a leave request.

The request goes through a mediator to the appropriate handler.

Employee
   ↓
Leave Request
   ↓
Mediator
   ↓
Leave Request Handler
   ↓
Leave Result

That's all.

We do not need:

PaymentService ❌
NotificationService ❌
InventoryService ❌
ShippingService ❌
Database ❌

Those don't help us understand Mediator.

-----------------------------------------------------------------------------------------------------

7. Project Goal

Our application should allow:

Employee submits leave request
             ↓
         Mediator
             ↓
     LeaveRequestHandler
             ↓
       Validate request
             ↓
        Approve/Reject
             ↓
          Result

For example:

EmployeeId: 101
Days: 3
Reason: Family function

Result:

Leave approved

-----------------------------------------------------------------------------------------------------

8. Project Structure

We'll keep it small:

11_MediatorPattern
│
├── Requests
│   └── LeaveRequest.cs
│
├── Handlers
│   └── LeaveRequestHandler.cs
│
├── Interfaces
│   ├── IRequest.cs
│   ├── IRequestHandler.cs
│   └── IMediator.cs
│
├── Mediator
│   └── Mediator.cs
│
├── Models
│   └── LeaveResult.cs
│
├── DependencyInjection
│   └── ServiceCollectionExtensions.cs
│
└── Program.cs

Only 8 files.