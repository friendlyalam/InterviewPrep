using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._02_ObserverPattern.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._02_ObserverPattern.Interfaces
{
    public interface IEventSubscriber
    {
        void Handle(OrderPlacedEvent orderEvent);
    }
}
