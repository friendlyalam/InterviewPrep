using InterviewPrep.LLD.SolidPrinciples.DIP.EmployeeAttendanceSystem.Interfaces;
using InterviewPrep.LLD.SolidPrinciples.DIP.EmployeeAttendanceSystem.Models;

namespace InterviewPrep.LLD.SolidPrinciples.DIP.EmployeeAttendanceSystem.Services
{
    public class EmailNotificationService : INotificationService
    {
        public void SendNotification(NotificationMessage message)
        {
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("EMAIL NOTIFICATION");
            Console.WriteLine("------------------------------------------");

            Console.WriteLine($"To      : {message.Recipient}");
            Console.WriteLine($"Subject : {message.Subject}");
            Console.WriteLine($"Message : {message.Message}");

            Console.WriteLine("Email sent successfully.\n");
        }
    }
}
