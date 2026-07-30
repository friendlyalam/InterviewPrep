using InterviewPrep.LLD.OOPS;
using InterviewPrep.LLD.OOPS.Aggregation;
using InterviewPrep.LLD.OOPS.Association;
using InterviewPrep.LLD.OOPS.Dependency;
using InterviewPrep.LLD.OOPS.Interfaces.DocumentExample;
using InterviewPrep.LLD.OOPS.Interfaces.FlightBookingSystem;
using InterviewPrep.LLD.OOPS.PartialClass;
using InterviewPrep.LLD.OOPS.Polymorphism;

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

foreach (var file in files)
{
    file.FileUploaded +=
        name => Console.WriteLine($"Event : {name} uploaded.");

    file.Upload();

    file.Preview();

    file.Download();

    Console.WriteLine();

    // Pattern Matching

    if (file is ImageFiles images)
    {
        images.GenerateThumbnail();
    }
    else if (file is VideoFiles videos)
    {
        videos.Compress();
    }
    else if (file is DocumentFile documents)
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
EmailService emailService =
            new EmailService();

OrderService orderService =
    new OrderService();

orderService.PlaceOrder(
    "Mohd Alam",
    "alam@example.com",
    emailService);
#endregion