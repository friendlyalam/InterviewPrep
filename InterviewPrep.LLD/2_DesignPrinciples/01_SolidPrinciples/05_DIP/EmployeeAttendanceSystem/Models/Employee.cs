

namespace InterviewPrep.LLD.SolidPrinciples.DIP.EmployeeAttendanceSystem.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string MobileNumber { get; set; } = string.Empty;
    }
}

//Why these properties?

//In real HR systems

//Employee has

//Employee Id
//Name
//Email
//Mobile

//Notification providers need

//Email

//or

//Mobile

//depending on provider.