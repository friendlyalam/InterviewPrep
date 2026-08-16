using InterviewPrep.LLD.SolidPrinciples.DIP.EmployeeAttendanceSystem.Exceptions;
using InterviewPrep.LLD.SolidPrinciples.DIP.EmployeeAttendanceSystem.Interfaces;
using InterviewPrep.LLD.SolidPrinciples.DIP.EmployeeAttendanceSystem.Models;
using InterviewPrep.LLD.SolidPrinciples.DIP.EmployeeAttendanceSystem.Services;

namespace InterviewPrep.LLD.SolidPrinciples.DIP.EmployeeAttendanceSystem.Services
{
    //This is the most important class.

    //  Read every line carefully.
    public class AttendanceService : IAttendanceService
    {
        private readonly INotificationService _notificationService;

        public AttendanceService(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public AttendanceResult MarkAttendance(
            Employee employee,
            AttendanceRecord attendanceRecord)
        {
            Validate(employee, attendanceRecord);

            Console.WriteLine("------------------------------------------");
            Console.WriteLine("MARKING ATTENDANCE");
            Console.WriteLine("------------------------------------------");

            Console.WriteLine($"Employee : {employee.FullName}");

            NotificationMessage notification =
                new NotificationMessage
                {
                    Recipient = employee.Email,
                    Subject = "Attendance Confirmation",
                    Message = $"Attendance marked successfully on {attendanceRecord.AttendanceDate:dd-MMM-yyyy}."
                };

            _notificationService.SendNotification(notification);

            return new AttendanceResult
            {
                IsSuccess = true,
                Message = "Attendance marked successfully.",
                AttendanceRecord = attendanceRecord
            };
        }

        private void Validate(
            Employee employee,
            AttendanceRecord attendance)
        {
            if (employee == null)
                throw new AttendanceException("Employee cannot be null.");

            if (attendance == null)
                throw new AttendanceException("Attendance cannot be null.");

            if (!attendance.IsPresent)
                throw new AttendanceException("Employee is absent.");
        }
    }
}

//Why Constructor Injection?

//Instead of writing

//private EmailNotificationService _email =
//    new EmailNotificationService();

//we write

//private readonly INotificationService _notificationService;

//Why?

//Because now

//AttendanceService

//doesn't know

//Email
//SMS
//Teams
//Slack

//It only knows

//INotificationService

//This is the Dependency Inversion Principle.
