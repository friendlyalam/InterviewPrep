using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._02_ObserverPattern.Models
{
    public sealed class ObserverOrder
    {
        public string OrderId { get; init; } = string.Empty;

        public int CustomerId { get; init; }

        public string CustomerEmail { get; init; } = string.Empty;

        public decimal Amount { get; init; }

        public DateTime OrderedAt { get; init; } = DateTime.UtcNow;
    }
}
