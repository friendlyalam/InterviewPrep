using InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._01_DecoratorPattern.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._01_DecoratorPattern.Interfaces
{
    public interface INotificationService
    {
        Task SendAsync(NotificationMessage message);
    }
}

//Why Async?

//Enterprise systems:

//Send Email
//Send SMS
//Send Push Notification

//All involve I/O.

//Always prefer

//Task

//instead of

//void
//Why One Method?

//The interface has a single responsibility.

//Send Notification

//Logging isn't its responsibility.

//Retry isn't its responsibility.

//Metrics aren't its responsibility.

//Decorators will add those behaviors.