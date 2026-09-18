
namespace InterviewPrep.LLD.OOPS.PartialClass
{
    public partial class Employee
    {
        public bool ValidateSalary()
        {
            return Salary > 0;
        }

        public bool ValidateName()
        {
            return !string.IsNullOrWhiteSpace(Name);
        }
    }
}
