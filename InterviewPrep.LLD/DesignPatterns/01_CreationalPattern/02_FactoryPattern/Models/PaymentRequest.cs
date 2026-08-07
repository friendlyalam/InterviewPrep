using InterviewPrep.LLD.DesignPatterns.CreationalPattern._02_FactoryPattern.Enums;

namespace InterviewPrep.LLD.DesignPatterns.CreationalPattern._02_FactoryPattern.Models
{
    public class PaymentRequests
    {
        public Guid OrderId { get; set; }

        public decimal Amount { get; set; }

        public string Currency { get; set; } = string.Empty;

        public string CustomerEmail { get; set; } = string.Empty;

        public PaymentMethod PaymentMethod { get; set; }
    }
}
