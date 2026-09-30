using InterviewPrep.LLD.SolidPrinciples.DIP.EmployeeAttendanceSystem.Models;

namespace InterviewPrep.LLD.SolidPrinciples.DIP.EmployeeAttendanceSystem.Interfaces
{
    public interface IAttendanceService
    {
        AttendanceResult MarkAttendance(
            Employee employee,
            AttendanceRecord attendanceRecord);
    }
}

//Notice

//Attendance Service

//contains only attendance operations.

//Not

//SendEmail()

//SendSMS()

//SendTeams()

//Those belong somewhere else.