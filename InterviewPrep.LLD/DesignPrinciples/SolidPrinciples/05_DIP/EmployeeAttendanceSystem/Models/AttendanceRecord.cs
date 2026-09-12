
namespace InterviewPrep.LLD.SolidPrinciples.DIP.EmployeeAttendanceSystem.Models
{
    public class AttendanceRecord
    {
        public int EmployeeId { get; set; }

        public DateTime AttendanceDate { get; set; }

        public TimeSpan CheckInTime { get; set; }

        public bool IsPresent { get; set; }
    }
}

//Why separate AttendanceRecord?

//Because

//One Employee

//↓

//Many Attendance Records

//This is a real business relationship.