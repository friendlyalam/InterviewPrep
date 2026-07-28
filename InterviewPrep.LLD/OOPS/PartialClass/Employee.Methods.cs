
namespace InterviewPrep.LLD.OOPS.PartialClass
{
    public partial class Employee
    {
        public void Display()
        {
            Console.WriteLine("Employee Details");
            Console.WriteLine("-------------------------");
            Console.WriteLine($"Id      : {Id}");
            Console.WriteLine($"Name    : {Name}");
            Console.WriteLine($"Salary  : {Salary}");
        }

        public void Work()
        {
            Console.WriteLine($"{Name} is working...");
        }
    }
}
