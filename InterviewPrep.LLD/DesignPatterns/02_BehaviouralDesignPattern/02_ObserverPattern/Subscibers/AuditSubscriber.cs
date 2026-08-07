using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._02_ObserverPattern.Interfaces;
using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._02_ObserverPattern.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._02_ObserverPattern.Subscibers
{
    public sealed class AuditSubscriber : IEventSubscriber
    {
        public void Handle(OrderPlacedEvent orderEvent)
        {
            Console.WriteLine(
                $"[Audit] Audit log created for Order {orderEvent.Order.OrderId}");
        }
    }
}
