using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._02_ObserverPattern.Interfaces;
using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._02_ObserverPattern.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._02_ObserverPattern.Subscibers
{
    public sealed class AnalyticsSubscriber : IEventSubscriber
    {
        public void Handle(OrderPlacedEvent orderEvent)
        {
            Console.WriteLine(
                $"[Analytics] Order value recorded : {orderEvent.Order.Amount:C}");
        }
    }
}
