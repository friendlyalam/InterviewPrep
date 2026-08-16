
namespace InterviewPrep.LLD.SolidPrinciples.DIP.EmployeeAttendanceSystem.Models
{
    public class AttendanceResult
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public AttendanceRecord AttendanceRecord { get; set; }
    }
}

//Why another model?

//Instead of returning

//bool

//we return

//Business Result

//Tomorrow

//Business may ask

//Attendance Status
//Shift
//Late Entry
//Working Hours

//Easy to extend.