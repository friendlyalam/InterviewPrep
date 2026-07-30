

namespace InterviewPrep.LLD.SolidPrinciples.DIP.EmployeeAttendanceSystem.Exceptions
{
    public class AttendanceException : Exception
    {
        public AttendanceException(string message)
            : base(message)
        {
        }
    }
}

//Enterprise applications rarely throw

//throw new Exception(...)

//Instead,

//they create domain-specific exceptions.