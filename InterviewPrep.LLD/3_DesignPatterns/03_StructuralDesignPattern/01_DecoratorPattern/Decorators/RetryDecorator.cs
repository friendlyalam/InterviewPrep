using InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._01_DecoratorPattern.Interfaces;
using InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._01_DecoratorPattern.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._01_DecoratorPattern.Decorators
{
    public sealed class RetryDecorator : NotificationDecorator
    {
        public RetryDecorator(INotificationService notificationService)
            : base(notificationService)
        {
        }

        public override async Task SendAsync(NotificationMessage message)
        {
            const int maxRetry = 3;

            for (int attempt = 1; attempt <= maxRetry; attempt++)
            {
                try
                {
                    Console.WriteLine($"Attempt {attempt}");

                    await base.SendAsync(message);

                    return;
                }
                catch
                {
                    if (attempt == maxRetry)
                        throw;

                    Console.WriteLine("Retrying...");
                }
            }
        }
    }
}
