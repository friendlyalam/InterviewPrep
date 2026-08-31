namespace InterviewPrep.LLD.OOPS.ClassObject
{
    public class Class
    {
        // ====================================================
        // 1. Fields (Private Variables)
        // ====================================================
        private int _id;
        private string _name;
        private decimal _salary;
        // ====================================================
        // 2. Properties
        // ====================================================
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public decimal Salary
        {
            get { return _salary; }
            set
            {
                if (value >= 0)
                    _salary = value;
            }
        }
        // ====================================================
        // 3. Constructors
        // ====================================================
        // Default Constructor
        public Class()
        {
            Console.WriteLine("Default Constructor Called");
        }

        // Parameterized Constructor
        public Class(int id, string name, decimal salary)
        {
            _id = id;
            _name = name;
            _salary = salary;
        }

        // ====================================================
        // 4. Methods
        // ====================================================
        public void Display()
        {
            Console.WriteLine("Employee Details");
            Console.WriteLine($"Id      : {_id}");
            Console.WriteLine($"Name    : {_name}");
            Console.WriteLine($"Salary  : {_salary}");
        }

        public void Work()
        {
            Console.WriteLine($"{_name} is working.");
        }
        // ====================================================
        // 5. Events
        // ====================================================
        public event Action SalaryChanged;
        public void IncreaseSalary(decimal amount)
        {
            _salary += amount;

            SalaryChanged?.Invoke();
        }
        // ====================================================
        // 6. Indexer
        // ====================================================

        private string[] skills = new string[5];

        public string this[int index]//This allows the object to be accessed like an array:
        {
            get
            {
                return skills[index];
            }

            set
            {
                skills[index] = value;
            }
        }

        // ====================================================
        // 7. Nested Class
        // ====================================================

        public class Address
        {
            public string City { get; set; }

            public string Country { get; set; }

            public void ShowAddress()
            {
                Console.WriteLine($"{City}, {Country}");
            }
        }

    }
}
