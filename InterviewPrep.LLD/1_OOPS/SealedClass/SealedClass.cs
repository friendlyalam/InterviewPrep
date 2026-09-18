using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.LLD.OOPS.SealedClass
{
    // Interface
    public interface IPrintable
    {
        void PrintDetails();
    }

    // Base Class
    public class Person
    {
        public virtual void ShowRole()
        {
            Console.WriteLine("I am a Person");
        }
    }
    public sealed class SealedClass : Person, IPrintable
    {
        //========================================================
        // 1. Fields
        //========================================================

        private int _id;
        private string _name;
        private decimal _salary;

        //========================================================
        // 2. Constant
        //========================================================

        public const string CompanyName = "Microsoft";

        //========================================================
        // 3. Readonly Field
        //========================================================

        private readonly DateTime _joiningDate;

        //========================================================
        // 4. Properties
        //========================================================

        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public decimal Salary
        {
            get => _salary;

            set
            {
                if (value > 0)
                    _salary = value;
            }
        }

        // Read Only Property

        public DateTime JoiningDate
        {
            get => _joiningDate;
        }

        //========================================================
        // 5. Event
        //========================================================

        public event Action EmployeeSaved;

        //========================================================
        // 6. Constructors
        //========================================================

        public SealedClass()
        {
            _joiningDate = DateTime.Now;

            Console.WriteLine("Default Constructor");
        }

        public SealedClass(int id, string name, decimal salary)
        {
            _id = id;
            _name = name;
            _salary = salary;
            _joiningDate = DateTime.Now;
        }

        //========================================================
        // 7. Methods
        //========================================================

        public void Save()
        {
            Console.WriteLine("Employee Saved");

            EmployeeSaved?.Invoke();
        }

        public override void ShowRole()
        {
            Console.WriteLine("I am an Employee");
        }

        public void PrintDetails()
        {
            Console.WriteLine("--------------------------------");

            Console.WriteLine($"Id          : {_id}");

            Console.WriteLine($"Name        : {_name}");

            Console.WriteLine($"Salary      : {_salary}");

            Console.WriteLine($"Company     : {CompanyName}");

            Console.WriteLine($"Joined On   : {_joiningDate}");

            Console.WriteLine("--------------------------------");
        }

        //========================================================
        // 8. Indexer
        //========================================================

        private string[] skills = new string[5];

        public string this[int index]
        {
            get => skills[index];

            set => skills[index] = value;
        }

        //========================================================
        // 9. Nested Class
        //========================================================

        public class Address
        {
            public string City { get; set; }

            public string Country { get; set; }

            public void Display()
            {
                Console.WriteLine($"{City}, {Country}");
            }
        }

        //========================================================
        // 10. Static Method
        //========================================================

        public static void CompanyPolicy()
        {
            Console.WriteLine("Follow company coding standards.");
        }

        //========================================================
        // 11. Static Field
        //========================================================

        public static int EmployeeCount = 0;
    }
}
