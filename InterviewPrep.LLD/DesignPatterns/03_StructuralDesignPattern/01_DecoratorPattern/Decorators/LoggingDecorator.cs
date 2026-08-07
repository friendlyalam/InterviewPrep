using InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._01_DecoratorPattern.Interfaces;
using InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._01_DecoratorPattern.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._01_DecoratorPattern.Decorators
{
    public sealed class LoggingDecorator : NotificationDecorator
    {
        public LoggingDecorator(INotificationService notificationService)
            : base(notificationService)
        {
        }

        public override async Task SendAsync(NotificationMessage message)
        {
            Console.WriteLine("========== LOG START ==========");

            Console.WriteLine($"Recipient : {message.Recipient}");

            Console.WriteLine($"Time      : {DateTime.Now}");

            await base.SendAsync(message);

            Console.WriteLine("=========== LOG END ===========");
        }
    }
}
