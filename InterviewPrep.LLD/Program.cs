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