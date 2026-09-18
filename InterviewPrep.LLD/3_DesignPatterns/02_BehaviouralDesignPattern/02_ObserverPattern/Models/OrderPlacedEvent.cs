using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._02_ObserverPattern.Models
{
    public sealed class OrderPlacedEvent
    {
        public ObserverOrder Order { get; init; } = default!;
    }
}
