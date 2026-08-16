

using InterviewPrep.LLD.SolidPrinciples.DIP.EmployeeAttendanceSystem.Interfaces;
using InterviewPrep.LLD.SolidPrinciples.DIP.EmployeeAttendanceSystem.Models;

namespace InterviewPrep.LLD.SolidPrinciples.DIP.EmployeeAttendanceSystem.Services
{
    //    Notice

    //No code changes are required anywhere else.
    public class SmsNotificationService : INotificationService
    {
        public void SendNotification(NotificationMessage message)
        {
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("SMS NOTIFICATION");
            Console.WriteLine("------------------------------------------");

            Console.WriteLine($"Mobile  : {message.Recipient}");
            Console.WriteLine($"Message : {message.Message}");

            Console.WriteLine("SMS sent successfully.\n");
        }
    }
}
