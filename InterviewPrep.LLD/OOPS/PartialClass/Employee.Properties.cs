
namespace InterviewPrep.LLD.OOPS.PartialClass
{
        public partial class Employee
        {
            //=================================================
            // Fields
            //=================================================

            private int _id;
            private string _name;
            private decimal _salary;

            //=================================================
            // Properties
            //=================================================

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
                set => _salary = value;
            }

            //=================================================
            // Constructor
            //=================================================

            public Employee()
            {
                Console.WriteLine("Employee Object Created");
            }
        }
    }
