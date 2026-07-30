

namespace InterviewPrep.LLD.SolidPrinciples.DIP.EmployeeAttendanceSystem.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string MobileNumber { get; set; }
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