using InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._01_DecoratorPattern.Interfaces;
using InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._01_DecoratorPattern.Models;

namespace InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._01_DecoratorPattern.Services
{
    public sealed class EmailNotificationService : INotificationService
    {
        public async Task SendAsync(NotificationMessage message)
        {
            await Task.Delay(500);

            Console.WriteLine("================================");

            Console.WriteLine("Email Sent Successfully");

            Console.WriteLine($"To      : {message.Recipient}");

            Console.WriteLine($"Subject : {message.Subject}");

            Console.WriteLine($"Body    : {message.Body}");

            Console.WriteLine("================================");
        }
    }
}

//Why Task.Delay?

//In a real application this would be:

//SMTP

//↓

//SendGrid

//↓

//AWS SES

//↓

//Azure Communication Services

//Those are asynchronous network calls.

//We simulate that using

//await Task.Delay(500);