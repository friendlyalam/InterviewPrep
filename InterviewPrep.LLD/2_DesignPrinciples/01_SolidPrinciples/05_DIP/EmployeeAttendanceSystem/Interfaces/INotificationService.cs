using InterviewPrep.LLD.SolidPrinciples.DIP.EmployeeAttendanceSystem.Models;

namespace InterviewPrep.LLD.SolidPrinciples.DIP.EmployeeAttendanceSystem.Interfaces
{
    public interface INotificationService
    {
        void SendNotification(NotificationMessage message);
    }
}

//This is the heart of DIP.

//Notice we did NOT write

//void SendEmail();

//or

//void SendSms();

//Instead

//SendNotification()

//Any provider can implement it.