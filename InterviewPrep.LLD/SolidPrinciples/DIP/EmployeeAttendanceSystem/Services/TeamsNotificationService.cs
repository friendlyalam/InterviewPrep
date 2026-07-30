using InterviewPrep.LLD.SolidPrinciples.DIP.EmployeeAttendanceSystem.Interfaces;
using InterviewPrep.LLD.SolidPrinciples.DIP.EmployeeAttendanceSystem.Models;

namespace InterviewPrep.LLD.SolidPrinciples.DIP.EmployeeAttendanceSystem.Services
{
    public class TeamsNotificationService : INotificationService
    {
        public void SendNotification(NotificationMessage message)
        {
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("MICROSOFT TEAMS NOTIFICATION");
            Console.WriteLine("------------------------------------------");

            Console.WriteLine($"User    : {message.Recipient}");
            Console.WriteLine($"Message : {message.Message}");

            Console.WriteLine("Teams notification sent successfully.\n");
        }
    }
}
