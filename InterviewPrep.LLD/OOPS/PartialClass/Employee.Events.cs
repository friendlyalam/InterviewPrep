
namespace InterviewPrep.LLD.OOPS.PartialClass
{
    public partial class Employee
    {
        //===========================================
        // Event
        //===========================================

        public event Action EmployeeSaved;

        //===========================================
        // Indexer
        //===========================================

        private string[] skills = new string[5];

        public string this[int index]
        {
            get => skills[index];

            set => skills[index] = value;
        }

        //===========================================
        // Nested Class
        //===========================================

        public class Address
        {
            public string City { get; set; }

            public string Country { get; set; }

            public void Display()
            {
                Console.WriteLine($"{City}, {Country}");
            }
        }

        //===========================================
        // Save Method
        //===========================================

        public void Save()
        {
            Console.WriteLine("Employee Saved Successfully");

            EmployeeSaved?.Invoke();
        }
    }
}
