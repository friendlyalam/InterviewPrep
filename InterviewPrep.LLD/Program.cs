using InterviewPrep.LLD.OOPS;

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