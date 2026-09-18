using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._03_FacadePattern.Models
{
    public sealed class OrderRequest
    {
        public int CustomerId { get; init; }

        public int ProductId { get; init; }

        public int Quantity { get; init; }

        public decimal Amount { get; init; }

        public string DeliveryAddress { get; init; } = string.Empty;

        public string Email { get; init; } = string.Empty;
    }
}
