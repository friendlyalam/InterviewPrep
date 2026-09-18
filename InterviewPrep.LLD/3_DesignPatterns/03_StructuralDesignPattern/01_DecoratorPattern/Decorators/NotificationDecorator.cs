using InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._01_DecoratorPattern.Decorators;
using InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._01_DecoratorPattern.Interfaces;
using InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._01_DecoratorPattern.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._01_DecoratorPattern.Decorators
{
    //    This is the Base Decorator.
    //Every other decorator inherits from this class.
    public abstract class NotificationDecorator : INotificationService
    {
        protected readonly INotificationService NotificationService;

        protected NotificationDecorator(
            INotificationService notificationService)
        {
            NotificationService = notificationService;
        }

        public virtual async Task SendAsync(NotificationMessage message)
        {
            await NotificationService.SendAsync(message);
        }
    }
}

//Why do we need this class?

//Without it every decorator would repeat:

//private readonly INotificationService _service;

//public LoggingDecorator(INotificationService service)
//{
//    _service = service;
//}

//again...

//and again...

//and again...

//The base decorator removes duplication.