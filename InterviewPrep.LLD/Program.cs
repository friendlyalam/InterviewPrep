using InterviewPrep.LLD.Design.CreationalPattern._01_SingletonPattern.Consumer;
using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._01_StrategyPattern.DependencyInjection;
using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._01_StrategyPattern.Interfaces;
using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._01_StrategyPattern.Models;
using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._02_ObserverPattern.Models;
using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._02_ObserverPattern.Publishers;
using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._02_ObserverPattern.Subscibers;
using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._03_CommandPattern.Commands;
using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._03_CommandPattern.DependencyInjection;
using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._03_CommandPattern.Interfaces;
using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._03_CommandPattern.Models;
using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._04_MediatorPattern.DependencyInjection;
using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._04_MediatorPattern.Interfaces;
using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._04_MediatorPattern.Models;
using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._04_MediatorPattern.Requests;
using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._05_ChainOfResponsibility.DependencyInjection;
using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._05_ChainOfResponsibility.Handlers;
using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._05_ChainOfResponsibility.Models;
using InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._01_DecoratorPattern.Decorators;
using InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._02_AdapterPattern.Adapter;
using InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._02_AdapterPattern.Interfaces;
using InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._02_AdapterPattern.Models;
using InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._02_AdapterPattern.ThirdParty;
using InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._03_FacadePattern.Facades;
using InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._03_FacadePattern.Interfaces;
using InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._03_FacadePattern.Models;
using InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._03_FacadePattern.Services;
using InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._04_ProxyPattern.DependencyInjection;
using InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._04_ProxyPattern.Interfaces;
using InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._04_ProxyPattern.Models;
using InterviewPrep.LLD.DesignPatterns.CreationalPattern._02_FactoryPattern.Enums;
using InterviewPrep.LLD.DesignPatterns.CreationalPattern._02_FactoryPattern.Models;
using InterviewPrep.LLD.DesignPatterns.CreationalPattern._02_FactoryPattern.Services;
using InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.DependencyInjection;
using InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Enums;
using InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Interfaces;
using InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Models;
using InterviewPrep.LLD.OOPS;
using InterviewPrep.LLD.OOPS.Aggregation;
using InterviewPrep.LLD.OOPS.Association;
using InterviewPrep.LLD.OOPS.Dependency;
using InterviewPrep.LLD.OOPS.Interfaces.DocumentExample;
using InterviewPrep.LLD.OOPS.Interfaces.FlightBookingSystem;
using InterviewPrep.LLD.OOPS.PartialClass;
using InterviewPrep.LLD.OOPS.Polymorphism;
using InterviewPrep.LLD.SolidPrinciples.DIP.EmployeeAttendanceSystem.Interfaces;
using InterviewPrep.LLD.SolidPrinciples.DIP.EmployeeAttendanceSystem.Models;
using InterviewPrep.LLD.SolidPrinciples.DIP.EmployeeAttendanceSystem.Services;
using InterviewPrep.LLD.SolidPrinciples.ISP.HospitalManagementSystem.Interfaces;
using InterviewPrep.LLD.SolidPrinciples.ISP.HospitalManagementSystem.Models;
using InterviewPrep.LLD.SolidPrinciples.ISP.HospitalManagementSystem.Servces;
using InterviewPrep.LLD.SolidPrinciples.LSP.CloudStorageProviderSystem.Interfaces;
using InterviewPrep.LLD.SolidPrinciples.LSP.CloudStorageProviderSystem.Models;
using InterviewPrep.LLD.SolidPrinciples.LSP.CloudStorageProviderSystem.Services;
using InterviewPrep.LLD.SolidPrinciples.OCP.PaymentGatewaySystem.Interfaces;
using InterviewPrep.LLD.SolidPrinciples.OCP.PaymentGatewaySystem.Models;
using InterviewPrep.LLD.SolidPrinciples.OCP.PaymentGatewaySystem.Services;
using InterviewPrep.LLD.SolidPrinciples.SRP.OrderProcessingSystem.Interfaces;
using InterviewPrep.LLD.SolidPrinciples.SRP.OrderProcessingSystem.Repositories;
using InterviewPrep.LLD.SolidPrinciples.SRP.OrderProcessingSystem.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using CloudFile = InterviewPrep.LLD.OOPS.Polymorphism.CloudFile;
using EmailService = InterviewPrep.LLD.SolidPrinciples.SRP.OrderProcessingSystem.Services.EmailService;
using Employee = InterviewPrep.LLD.OOPS.PartialClass.Employee;
using OrderService = InterviewPrep.LLD.SolidPrinciples.SRP.OrderProcessingSystem.Services.OrderService;
using PaymentService = InterviewPrep.LLD.Design.CreationalPattern._01_SingletonPattern.Consumer.PaymentService;
using UploadResult = InterviewPrep.LLD.SolidPrinciples.LSP.CloudStorageProviderSystem.Models.UploadResult;
#region OOPS

#region Class Demo
Class emp = new Class(101, "Mohd Alam", 75000);

emp.Display();

emp.Work();

//--------------------------------------------------
// Event
//--------------------------------------------------

emp.SalaryChanged += () =>
{
    Console.WriteLine("Salary Updated Successfully.");
};

emp.IncreaseSalary(5000);

//--------------------------------------------------
// Indexer
//--------------------------------------------------

emp[0] = "C#";
emp[1] = ".NET";
emp[2] = "SQL Server";

Console.WriteLine(emp[0]);
Console.WriteLine(emp[1]);
Console.WriteLine(emp[2]);

//--------------------------------------------------
// Nested Class
//--------------------------------------------------

Class.Address address = new Class.Address();

address.City = "Moradabad";
address.Country = "India";

address.ShowAddress();
#endregion
#region Static class demo
StaticClass.Info("Application Started");

StaticClass.Warning("Low Disk Space");

StaticClass.Error("Database Connection Failed");

StaticClass.ShowSummary();
#endregion
#region Sealed Class demo
SealedClass sealedClass = new SealedClass(101, "Mohd Alam", 150000);

sealedClass.EmployeeSaved += () =>
{
    Console.WriteLine("Event Raised Successfully");
};

sealedClass[0] = "C#";
sealedClass[1] = ".NET";
sealedClass[2] = "Azure";

sealedClass.PrintDetails();

Console.WriteLine();

Console.WriteLine("Skills");

Console.WriteLine(emp[0]);
Console.WriteLine(emp[1]);
Console.WriteLine(emp[2]);

Console.WriteLine();

sealedClass.Save();

Console.WriteLine();

SealedClass.CompanyPolicy();

Console.WriteLine();

SealedClass.Address addresses = new SealedClass.Address();

addresses.City = "Moradabad";

addresses.Country = "India";

addresses.Display();
#endregion
#region Partial Class demo
Employee emps = new Employee();

emps.Id = 101;
emps.Name = "Mohd Alam";
emps.Salary = 150000;

//------------------------------------------------
// Event
//------------------------------------------------

emps.EmployeeSaved += () =>
{
    Console.WriteLine("Event Fired Successfully");
};

//------------------------------------------------
// Indexer
//------------------------------------------------

emps[0] = "C#";
emps[1] = ".NET";
emps[2] = "Azure";

//------------------------------------------------
// Methods
//------------------------------------------------

emps.Display();

Console.WriteLine();

emps.Work();

Console.WriteLine();

Console.WriteLine("Salary Valid : " +
                  emps.ValidateSalary());

Console.WriteLine("Name Valid   : " +
                  emps.ValidateName());

Console.WriteLine();

Console.WriteLine("Skills");

Console.WriteLine(emp[0]);
Console.WriteLine(emp[1]);
Console.WriteLine(emp[2]);

Console.WriteLine();

Employee.Address add =
    new Employee.Address();

add.City = "Moradabad";
add.Country = "India";

add.Display();

Console.WriteLine();

emps.Save();

#endregion
#region Abstract Class demo
Payment payment =
                new CreditCardPayment(2500, "INR", "1234-XXXX");

payment.PaymentCompleted += (msg) =>
{
    Console.WriteLine($"EVENT : {msg}");
};

payment.Validate();

payment.ProcessPayment();

payment.GenerateReceipt();

payment.SendNotification();

payment.Refund();

Payment.ShowCompanyPolicy();

Console.WriteLine();

Payment.AuditLog audit =
    new Payment.AuditLog();

audit.Save();

Console.WriteLine();

Console.WriteLine($"Total Payments : {Payment.TotalPayments}");
#endregion
#region Abstraction demo
Document document =
                new PdfDocument("EmployeeReport.pdf", 2048);

document.DocumentProcessed +=
    message => Console.WriteLine(message);

document.Process();
#endregion
#region Encapsulation demo
BankAccount account =
               new BankAccount(
                   "SB1001",
                   "Mohd Alam",
                   10000);

account.Deposit(5000);

account.Withdraw(3000);

account.ChangeAccountHolder("Mohammad Alam");

account.DisplayAccount();

// Not Allowed

// account.Balance = 1000000;
// account._balance = 1000000;
#endregion
#region Inheritance demo
ImageFile image = new ImageFile(
                "Holiday.jpg",
                2048,
                "1920x1080");

image.Upload();
image.Preview();
image.GenerateThumbnail();

Console.WriteLine();

VideoFile video = new VideoFile(
    "Demo.mp4",
    50000,
    120);

video.Upload();
video.Preview();
video.CompressVideo();
#endregion
#region Polymorphism demo
CloudFile[] files =
            {
                new ImageFiles("Photo.jpg",2500,"1920x1080"),

                new VideoFiles("Demo.mp4",50000,120),

                new DocumentFile("Architecture.pdf",1800,45)
            };

foreach (var cloudFile in files)
{
    cloudFile.FileUploaded +=
        name => Console.WriteLine($"Event : {name} uploaded.");

    cloudFile.Upload();

    cloudFile.Preview();

    cloudFile.Download();

    Console.WriteLine();

    // Pattern Matching

    if (cloudFile is ImageFiles images)
    {
        images.GenerateThumbnail();
    }
    else if (cloudFile is VideoFiles videos)
    {
        videos.Compress();
    }
    else if (cloudFile is DocumentFile documents)
    {
        documents.ExtractText();
    }

    Console.WriteLine("--------------------------------");
}
#endregion
#region Method Overloading demo
StorageService storage = new StorageService();

storage.Upload("Resume.pdf");

storage.Upload("Resume.pdf", "Documents");

storage.Upload("Resume.pdf", "Documents", true);
#endregion
#region Method Overriding demo
InterviewPrep.LLD.OOPS.Polymorphism.NotificationService notification =
            new SmsNotification();

notification.Send("Order Delivered");
#endregion
#region Method Hiding demo
ReportGenerator report =
            new PdfReportGenerator();

report.Generate();

Console.WriteLine();

PdfReportGenerator pdf =
    new PdfReportGenerator();

pdf.Generate();
#endregion
#region Operator Overloading demo
Cart cart = new Cart(2);

cart = cart + 3;

Console.WriteLine(cart.TotalItems);
#endregion
#region Interface demo for document processing
IDocument docs =
                new PdfDocuments("Architecture.pdf");

DocumentManager manager =
    new DocumentManager(docs);

manager.Process();

Console.WriteLine();

if (document is IPrintables printable)
{
    printable.Print();
}

if (document is IExportable exportable)
{
    exportable.Export("HTML");
}

if (document is IAuditable audits)
{
    audits.Audit("Document Processed");
}
#endregion
#region Interface demo for FLight booking system
IAirline airline = new Emirates();

BookingService bookingService =
    new BookingService(airline);

bookingService.CreateBooking("Mohd Alam");
#endregion
#region Association demo
Customer customer =
            new Customer("Mohd Alam");

Order order =
    new Order(101, "Wireless Mouse");

customer.PlaceOrder(order);
#endregion
#region Aggregation demo
Pilot pilot1 =
            new Pilot(101, "James", 15);

Pilot pilot2 =
    new Pilot(102, "Rahul", 11);

Pilot pilot3 =
    new Pilot(103, "David", 18);

List<Pilot> pilots =
    new List<Pilot>
    {
                pilot1,
                pilot2,
                pilot3
    };

Airline air =
    new Airline(
        "Sky Wings",
        pilots);

air.DisplayPilots();

Console.WriteLine();

Console.WriteLine("Pilot still exists independently:");

pilot1.FlyAircraft();
#endregion
#region Composition demo
InterviewPrep.LLD.OOPS.Composition.Order orders =
            new InterviewPrep.LLD.OOPS.Composition.Order(
                1001,
                "Sector 18",
                "Noida",
                "India");

order.DisplayOrder();
#endregion
#region Dependency demo
InterviewPrep.LLD.OOPS.Dependency.EmailService emailService =
            new InterviewPrep.LLD.OOPS.Dependency.EmailService();

InterviewPrep.LLD.OOPS.Dependency.OrderService orderService =
    new InterviewPrep.LLD.OOPS.Dependency.OrderService();

orderService.PlaceOrder(
    "Mohd Alam",
    "alam@example.com",
    emailService);
#endregion

#endregion

#region Solid Principles

#region SRP demo
InterviewPrep.LLD.SolidPrinciples.SRP.OrderProcessingSystem.Models.Order ord = new InterviewPrep.LLD.SolidPrinciples.SRP.OrderProcessingSystem.Models.Order
{
    OrderId = 1001,
    CustomerName = "Mohd Alam",
    CustomerEmail = "alam@email.com",
    ProductName = "Dell Laptop",
    Price = 65000,
    Quantity = 1
};

IOrderRepository repository =
    new OrderRepository();

InterviewPrep.LLD.SolidPrinciples.SRP.OrderProcessingSystem.Interfaces.IInventoryService inventory =
    new InterviewPrep.LLD.SolidPrinciples.SRP.OrderProcessingSystem.Services.InventoryService();

InterviewPrep.LLD.SolidPrinciples.SRP.OrderProcessingSystem.Interfaces.IInvoiceService invoice =
    new InterviewPrep.LLD.SolidPrinciples.SRP.OrderProcessingSystem.Services.InvoiceService();

IEmailService email =
    new EmailService();

IAuditService aud =
    new AuditService();

OrderService orderServices =
    new OrderService(
        repository,
        inventory,
        invoice,
        email,
        aud);

orderServices.PlaceOrder(ord);
#endregion

#region OCP demo
//Payment using Stripe
PaymentRequest paymentRequest = new PaymentRequest
{
    OrderId = 1001,
    CustomerName = "Mohd Alam",
    Amount = 65000,
    Currency = "INR"
};

IPaymentGateway paymentGateway =
    new StripePaymentGateway();

CheckoutService checkoutService =
    new CheckoutService(paymentGateway);

checkoutService.Checkout(paymentRequest);
#endregion

#region LSP demo
UploadRequest request = new UploadRequest
{
    FileName = "Resume.pdf",
    FileContent = new byte[] { 10, 20, 30, 40 },
    ContentType = "application/pdf",
    FileSizeInBytes = 4096
};

IStorageProvider storageProvider = new AzureBlobStorageProvider();

FileStorageService storageService =
    new FileStorageService(storageProvider);

UploadResult result = storageService.UploadFile(request);

Console.WriteLine();

Console.WriteLine("Provider : " + result.File.StorageProvider);
Console.WriteLine("URL      : " + result.File.FileUrl);

//Note:
//Switching to AWS

//Only one line changes.

//IStorageProvider storageProvider =
//    new AwsS3StorageProvider();

//Nothing else changes.

//Switching to Google

//Again,

//only one line changes.

//IStorageProvider storageProvider =
//    new GoogleCloudStorageProvider();

//Everything else remains exactly the same.
#endregion

#region ISP demo
Patient patient = new Patient
{
    PatientId = 1,
    Name = "Mohd Alam",
    Age = 35,
    MobileNumber = "9876543210"
};

Appointment appointment = new Appointment
{
    AppointmentId = 101,
    DoctorName = "Dr. Sharma",
    AppointmentDate = DateTime.Now
};

IReceptionService reception =
    new Receptionist();

IDoctorService doctor =
    new Doctor();

IPharmacyService pharmacy =
    new Pharmacist();

IBillingService billing =
    new Cashier();

HospitalManagementService hospital =
    new HospitalManagementService(
        reception,
        doctor,
        pharmacy,
        billing);

hospital.ProcessPatient(
    patient,
    appointment);

Console.ReadKey();
#endregion

#region DIP demo
InterviewPrep.LLD.SolidPrinciples.DIP.EmployeeAttendanceSystem.Models.Employee employee = new InterviewPrep.LLD.SolidPrinciples.DIP.EmployeeAttendanceSystem.Models.Employee
{
    EmployeeId = 101,
    FullName = "Mohd Alam",
    Email = "mohdalam@gmail.com",
    MobileNumber = "9876543210"
};

AttendanceRecord attendance = new AttendanceRecord
{
    EmployeeId = 101,
    AttendanceDate = DateTime.Today,
    CheckInTime = new TimeSpan(9, 15, 0),
    IsPresent = true
};

// Dependency Selection
InterviewPrep.LLD.SolidPrinciples.DIP.EmployeeAttendanceSystem.Interfaces.INotificationService notificationService =
    new EmailNotificationService();

// Constructor Injection
IAttendanceService attendanceService =
    new AttendanceService(notificationService);

AttendanceResult res =
    attendanceService.MarkAttendance(
        employee,
        attendance);

Console.WriteLine("------------------------------------------");
Console.WriteLine("ATTENDANCE RESULT");
Console.WriteLine("------------------------------------------");
Console.WriteLine(res.Message);
Console.WriteLine();
#endregion

#endregion


#region Design Patterns

#region Creational pattern

#region Eager Single pattern demo

UserService userService = new();

InterviewPrep.LLD.Design.CreationalPattern._01_SingletonPattern.Consumer.OrderService orderServic = new();

PaymentService paymentService = new();

userService.DisplayConfiguration();

orderServic.DisplayConfiguration();

paymentService.DisplayConfiguration();

Console.WriteLine("--------------------------------");

Console.WriteLine(
    Object.ReferenceEquals(
        InterviewPrep.LLD.Design.CreationalPattern._01_SingletonPattern.SingletonImplementations._01_EagerSingleton.ConfigurationManager.Instance,

        InterviewPrep.LLD.Design.CreationalPattern._01_SingletonPattern.SingletonImplementations._01_EagerSingleton.ConfigurationManager.Instance));

#endregion

#region Lazy singleton demo
UserService userServiceLazy = new();

InterviewPrep.LLD.Design.CreationalPattern._01_SingletonPattern.Consumer.OrderService orderServiceLazy = new();

PaymentService paymentServiceLazy = new();

userServiceLazy.DisplayConfiguration();

orderServiceLazy.DisplayConfiguration();

paymentServiceLazy.DisplayConfiguration();

Console.WriteLine("--------------------------------");

Console.WriteLine(
    Object.ReferenceEquals(
        InterviewPrep.LLD.Design.CreationalPattern._01_SingletonPattern.SingletonImplementations._02_LazySingleton.ConfigurationManager.Instance,

        InterviewPrep.LLD.Design.CreationalPattern._01_SingletonPattern.SingletonImplementations._02_LazySingleton.ConfigurationManager.Instance));

#endregion

#region Factory pattern demo
PaymentRequests requests = new()
{
    OrderId = Guid.NewGuid(),
    Amount = 2500,
    Currency = "INR",
    CustomerEmail = "customer@gmail.com",
    PaymentMethod = PaymentMethod.Upi
};

CheckoutServices checkoutServices = new();

PaymentResponse response = checkoutServices.Checkout(requests);

Console.WriteLine($"Status          : {response.IsSuccess}");
Console.WriteLine($"Transaction Id  : {response.TransactionId}");
Console.WriteLine($"Message         : {response.Message}");
#endregion

#region Abstract factory pattern demo
ServiceCollection services = new();

services.AddCloudPlatform();

ServiceProvider serviceProvider = services.BuildServiceProvider();

ICloudPlatformService cloudPlatformService =
    serviceProvider.GetRequiredService<ICloudPlatformService>();

InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Models.CloudFile file = new()
{
    FileName = "EmployeeReport.pdf",
    Content = Array.Empty<byte>()
};

cloudPlatformService.Backup(
    CloudProvider.Azure,
    file);
#endregion
#endregion

#region Behavioural pattern
#region Strategy pattern demo
ServiceCollection serviceCollection = new();

serviceCollection.AddPricingServices();

ServiceProvider provider = services.BuildServiceProvider();

IPricingService pricingService =
    provider.GetRequiredService<IPricingService>();

Product product = new()
{
    Id = 1,
    Name = "MacBook Pro",
    BasePrice = 200000
};

PricingContext context = new()
{
    Product = product,
    CustomerType = "Festival",
    DiscountPercentage = 20
};

decimal finalPrice = pricingService.CalculatePrice(context);

Console.WriteLine($"Original Price : {product.BasePrice:C}");
Console.WriteLine($"Final Price    : {finalPrice:C}");
#endregion

#region Observer pattern demo
OrderPublisher publisher = new();

publisher.Subscribe(new EmailSubscriber());

publisher.Subscribe(new SmsSubscriber());

publisher.Subscribe(new InventorySubscriber());

publisher.Subscribe(new AnalyticsSubscriber());

publisher.Subscribe(new AuditSubscriber());

ObserverOrder observerOrder = new()
{
    OrderId = "ORD-1001",
    CustomerId = 101,
    CustomerEmail = "customer@company.com",
    Amount = 4999
};

OrderPlacedEvent orderEvent = new()
{
    Order = observerOrder
};

publisher.Publish(orderEvent);
#endregion

#region Command pattern demo
ServiceCollection commandServices = new();

commandServices.AddCommandPatternServices();

using ServiceProvider commandServiceProvider = commandServices.BuildServiceProvider();

using IServiceScope serviceScope = commandServiceProvider.CreateScope();

IServiceProvider scopedProvider = serviceScope.ServiceProvider;

Console.WriteLine("======================================");
Console.WriteLine("      COMMAND PATTERN DEMO");
Console.WriteLine("======================================");

Console.WriteLine();
Console.WriteLine("Creating Order...");
Console.WriteLine("--------------------------------------");

ICommandHandler<CreateOrderCommand, CommandResult>
    createOrderHandler =
        scopedProvider.GetRequiredService<
            ICommandHandler<CreateOrderCommand, CommandResult>>();

CreateOrderCommand createCommand = new(
    CustomerId: 101,
    ProductId: 5001,
    Quantity: 2,
    Price: 1499.00m);

CommandResult createResult =
    await createOrderHandler.HandleAsync(createCommand);

Console.WriteLine($"Success : {createResult.Success}");
Console.WriteLine($"Message : {createResult.Message}");

CommandOrder? commandOrder = createResult.Data as CommandOrder;

if (commandOrder is null)
{
    Console.WriteLine("Order creation failed.");
    return;
}

Console.WriteLine($"Order ID : {commandOrder.Id}");
Console.WriteLine($"Amount   : {commandOrder.TotalAmount:C}");
Console.WriteLine($"Status   : {commandOrder.Status}");

Console.WriteLine();
Console.WriteLine("Cancelling Order...");
Console.WriteLine("--------------------------------------");

ICommandHandler<CancelOrderCommand, CommandResult>
    cancelOrderHandler =
        scopedProvider.GetRequiredService<
            ICommandHandler<CancelOrderCommand, CommandResult>>();

CancelOrderCommand cancelCommand = new(commandOrder.Id);

CommandResult cancelResult =
    await cancelOrderHandler.HandleAsync(cancelCommand);

Console.WriteLine($"Success : {cancelResult.Success}");
Console.WriteLine($"Message : {cancelResult.Message}");

CommandOrder? cancelledOrder = cancelResult.Data as CommandOrder;

if (cancelledOrder is not null)
{
    Console.WriteLine($"Order ID : {cancelledOrder.Id}");
    Console.WriteLine($"Status   : {cancelledOrder.Status}");
}

Console.WriteLine();
Console.WriteLine("Processing Refund...");
Console.WriteLine("--------------------------------------");

ICommandHandler<RefundOrderCommand, CommandResult>
    refundOrderHandler =
        scopedProvider.GetRequiredService<
            ICommandHandler<RefundOrderCommand, CommandResult>>();

RefundOrderCommand refundCommand = new(
    OrderId: commandOrder.Id,
    Amount: commandOrder.TotalAmount);

CommandResult refundResult =
    await refundOrderHandler.HandleAsync(refundCommand);

Console.WriteLine($"Success : {refundResult.Success}");
Console.WriteLine($"Message : {refundResult.Message}");

Console.WriteLine();
Console.WriteLine("======================================");
Console.WriteLine("          DEMO COMPLETED");
Console.WriteLine("======================================");
#endregion

#region Mediator pattern demo
ServiceCollection MediatorCollectionservices = new();

MediatorCollectionservices.AddMediatorPattern();

using ServiceProvider mediatorServiceProvider =
    MediatorCollectionservices.BuildServiceProvider();

using IServiceScope scope =
    mediatorServiceProvider.CreateScope();

IMediator mediator =
    scope.ServiceProvider.GetRequiredService<IMediator>();

Console.WriteLine("======================================");
Console.WriteLine("       MEDIATOR PATTERN DEMO");
Console.WriteLine("======================================");

Console.WriteLine();
Console.WriteLine("Submitting valid leave request...");
Console.WriteLine("--------------------------------------");

LeaveRequest validRequest = new(
    EmployeeId: 101,
    NumberOfDays: 3,
    Reason: "Family function");

LeaveResult validResult =
    await mediator.SendAsync(validRequest);

Console.WriteLine($"Approved : {validResult.Approved}");
Console.WriteLine($"Message  : {validResult.Message}");

Console.WriteLine();
Console.WriteLine("Submitting invalid leave request...");
Console.WriteLine("--------------------------------------");

LeaveRequest invalidRequest = new(
    EmployeeId: 101,
    NumberOfDays: 15,
    Reason: "Vacation");

LeaveResult invalidResult =
    await mediator.SendAsync(invalidRequest);

Console.WriteLine($"Approved : {invalidResult.Approved}");
Console.WriteLine($"Message  : {invalidResult.Message}");

Console.WriteLine();
Console.WriteLine("======================================");
Console.WriteLine("          DEMO COMPLETED");
Console.WriteLine("======================================");
#endregion

#region  Chain of Responsibility pattern demo
ServiceCollection responsibilityServices = new();

responsibilityServices.AddExpenseApproval();

using ServiceProvider responsibilityServiceProvider =
    responsibilityServices.BuildServiceProvider();

using IServiceScope responsibilityScope =
    responsibilityServiceProvider.CreateScope();

ExpenseHandler expenseHandler =
    responsibilityScope.ServiceProvider.GetRequiredService<ExpenseHandler>();

Console.WriteLine("==========================================");
Console.WriteLine("     CHAIN OF RESPONSIBILITY DEMO");
Console.WriteLine("==========================================");

ProcessExpense(
    expenseHandler,
    new ExpenseRequest(
        EmployeeId: 101,
        Amount: 5_000,
        Description: "Office supplies"));

ProcessExpense(
    expenseHandler,
    new ExpenseRequest(
        EmployeeId: 102,
        Amount: 30_000,
        Description: "Business travel"));

ProcessExpense(
    expenseHandler,
    new ExpenseRequest(
        EmployeeId: 103,
        Amount: 80_000,
        Description: "Client event"));

ProcessExpense(
    expenseHandler,
    new ExpenseRequest(
        EmployeeId: 104,
        Amount: 150_000,
        Description: "Conference"));

static void ProcessExpense(
    ExpenseHandler handler,
    ExpenseRequest request)
{
    Console.WriteLine();
    Console.WriteLine("------------------------------------------");
    Console.WriteLine($"Employee    : {request.EmployeeId}");
    Console.WriteLine($"Amount      : ₹{request.Amount:N0}");
    Console.WriteLine($"Description : {request.Description}");
    Console.WriteLine("------------------------------------------");

    string result = handler.Handle(request);

    Console.WriteLine($"Result      : {result}");
}
#endregion
#endregion

#region Structural pattern

#region Decorator pattern demo
InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._01_DecoratorPattern.Models.NotificationMessage message = new()
{
    Recipient = "customer@company.com",
    Subject = "Order Shipped",
    Body = "Your order has been shipped."
};

InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._01_DecoratorPattern.Interfaces.INotificationService notificationsService =
    new InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._01_DecoratorPattern.Decorators.PerformanceDecorator(
        new InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._01_DecoratorPattern.Decorators.RetryDecorator(
            new InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._01_DecoratorPattern.Decorators.LoggingDecorator(
                new InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._01_DecoratorPattern.Services.EmailNotificationService())));

await notificationsService.SendAsync(message);
#endregion

#region Adapter pattern demo
FileUploadRequest fileUploadRequest = new()
{
    FileName = "resume.pdf",
    FolderName = "documents",
    Content = new byte[] { 1, 2, 3, 4 },
    ContentType = "application/pdf"
};

// Change only this line to switch providers

ICloudStorageService cloudStorage =
    new AzureStorageAdapter(new AzureBlobClient());

// ICloudStorageService cloudStorage =
//     new AmazonS3Adapter(new AmazonS3Client());

// ICloudStorageService cloudStorage =
new GoogleStorageAdapter(new GoogleCloudStorageClient());

InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._02_AdapterPattern.Models.UploadResult uploadResult = cloudStorage.Upload(fileUploadRequest);

Console.WriteLine();

Console.WriteLine($"Provider : {uploadResult.Provider}");
Console.WriteLine($"Success  : {uploadResult.Success}");
Console.WriteLine($"URL      : {uploadResult.FileUrl}");
Console.WriteLine($"Message  : {uploadResult.Message}");
#endregion

#region Facade pattern demo

OrderRequest orderRequest = new()
{
    CustomerId = 101,

    ProductId = 2001,

    Quantity = 2,

    Amount = 4999,

    DeliveryAddress = "Noida, India",

    Email = "customer@company.com"
};

IOrderFacade orderFacade =
    new OrderFacade(
        new InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._03_FacadePattern.Services.InventoryService(),
        new InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._03_FacadePattern.Services.PaymentService(),
        new ShippingService(),
        new InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._03_FacadePattern.Services.InvoiceService(),
        new InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._03_FacadePattern.Services.NotificationService());

OrderResult orderResult =
    orderFacade.PlaceOrder(orderRequest);

Console.WriteLine();

Console.WriteLine($"Success : {orderResult.Success}");
Console.WriteLine($"Order   : {orderResult.OrderNumber}");
Console.WriteLine($"Message : {orderResult.Message}");
#endregion

#region Proxy pattern demo
ServiceCollection proxyServices = new();

proxyServices.AddProductImageServices();

using ServiceProvider proxyServiceProvider =
    proxyServices.BuildServiceProvider();

using IServiceScope proxyScope =
    proxyServiceProvider.CreateScope();

IProductImageService imageService =
    proxyScope.ServiceProvider.GetRequiredService<IProductImageService>();

Console.WriteLine("==========================================");
Console.WriteLine("           PROXY PATTERN DEMO");
Console.WriteLine("==========================================");

Console.WriteLine();
Console.WriteLine("First request:");
ProductImage image1 =
    await imageService.GetImageAsync(101);

Console.WriteLine($"Image URL: {image1.Url}");

Console.WriteLine();
Console.WriteLine("Second request for the same product:");
ProductImage image2 =
    await imageService.GetImageAsync(101);

Console.WriteLine($"Image URL: {image2.Url}");

Console.WriteLine();
Console.WriteLine("Request for another product:");
ProductImage image3 =
    await imageService.GetImageAsync(202);

Console.WriteLine($"Image URL: {image3.Url}");
#endregion

#endregion

#endregion