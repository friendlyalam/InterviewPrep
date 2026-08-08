using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._03_CommandPattern.Models
{
    public class CommandOrder
    {
        public Guid Id { get; init; }

        public int CustomerId { get; init; }

        public int ProductId { get; init; }

        public int Quantity { get; init; }

        public decimal TotalAmount { get; init; }

        public string Status { get; set; } = "Created";

        public DateTime CreatedAt { get; init; }
    }
}

//Why Guid for Order ID?

//Instead of:

//public int Id

//we use:

//public Guid Id

//because distributed systems commonly use identifiers that can be generated independently without relying on a central auto-incrementing database value.

//For our interview demo, that's enough.