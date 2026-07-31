using InterviewPrep.LLD.Design.CreationalPattern._01_SingletonPattern.Consumer;
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
NotificationService notification =
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

IInventoryService inventory =
    new InventoryService();

IInvoiceService invoice =
    new InvoiceService();

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
INotificationService notificationService =
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