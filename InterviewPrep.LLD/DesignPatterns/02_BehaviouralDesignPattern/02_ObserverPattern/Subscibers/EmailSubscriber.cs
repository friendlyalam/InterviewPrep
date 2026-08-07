using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._02_ObserverPattern.Interfaces;
using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._02_ObserverPattern.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._02_ObserverPattern.Subscibers
{
    public sealed class EmailSubscriber : IEventSubscriber
    {
        public void Handle(OrderPlacedEvent orderEvent)
        {
            Console.WriteLine(
                $"[Email] Confirmation email sent to {orderEvent.Order.CustomerEmail}");
        }
    }
}
