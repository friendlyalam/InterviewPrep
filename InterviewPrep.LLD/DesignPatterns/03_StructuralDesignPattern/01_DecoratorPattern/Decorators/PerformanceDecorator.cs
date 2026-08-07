using InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._01_DecoratorPattern.Interfaces;
using InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._01_DecoratorPattern.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._01_DecoratorPattern.Decorators
{
    public sealed class PerformanceDecorator : NotificationDecorator
    {
        public PerformanceDecorator(
            INotificationService notificationService)
            : base(notificationService)
        {
        }

        public override async Task SendAsync(NotificationMessage message)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            await base.SendAsync(message);

            stopwatch.Stop();

            Console.WriteLine(
                $"Execution Time : {stopwatch.ElapsedMilliseconds} ms");
        }
    }
}
